using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Edis.Repositories.Utils
{
    public static class DbContextExtensions
    {
        public static bool IsDisposed(this DbContext context)
        {
            var result = true;

            var typeDbContext = typeof(DbContext);
            var typeInternalContext = typeDbContext.Assembly.GetType("System.Data.Entity.Internal.InternalContext");

            var fi_InternalContext = typeDbContext.GetField("_internalContext", BindingFlags.NonPublic | BindingFlags.Instance);
            var pi_IsDisposed = typeInternalContext.GetProperty("IsDisposed");

            var ic = fi_InternalContext.GetValue(context);

            if (ic != null)
            {
                result = (bool)pi_IsDisposed.GetValue(ic);
            }

            return result;
        }
    }

    public static class DbExtensions
    {

        public static string ExecuteJsonQuery(this DbContext db, string query)
        {
            return ExecuteJsonQuery(db, query, null);
        }

        public static string ExecuteJsonQuery(this DbContext db, string query, params SqlParameter[] sqlParams)
        {
            using (var command = db.Database.Connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                if (sqlParams != null) command.Parameters.AddRange(sqlParams);


                db.Database.Connection.Open();
                StringBuilder result = new StringBuilder();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Append((string)reader[0]);
                    }


                    return result.ToString();
                }
            }
        }

        public static List<T> ExecuteReader<T>(this DbContext context, string query)
        {
            return ExecuteReader<T>(context, query, null);
        }

        /// <summary>
        /// execute Raw SQL queries: Non-model types
        /// https://github.com/aspnet/EntityFrameworkCore/issues/1862
        /// </summary>
        public static List<T> ExecuteReader<T>(this DbContext context, string query, params SqlParameter[] sqlParams)
        {
            var conn = context.Database.Connection;
            try
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (sqlParams != null) command.Parameters.AddRange(sqlParams);
                    if (context.Database.CurrentTransaction == null)
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }
                    else
                    {
                        command.Transaction = context.Database.CurrentTransaction.UnderlyingTransaction;
                    }
                    using (var result = command.ExecuteReader())
                    {
                        var list = new List<T>();
                        var mapper = new DataReaderMapper<T>(result);
                        while (result.Read())
                        {
                            list.Add(mapper.MapFrom(result));
                        }
                        return list;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (context.Database.CurrentTransaction == null)
                    conn.Close();
            }

        }

        public static void ExecuteNonQuery(this DbContext context, string query, params SqlParameter[] sqlParams)
        {
            var conn = context.Database.Connection;
            try
            {
                using (var command = conn.CreateCommand())
                {
                    if (context.Database.CurrentTransaction == null)
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }
                    else
                    {
                        command.Transaction = context.Database.CurrentTransaction.UnderlyingTransaction;
                    }
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (sqlParams != null) command.Parameters.AddRange(sqlParams);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (context.Database.CurrentTransaction == null)
                    conn.Close();
            }

        }

        public static T ExecuteScalar<T>(this DbContext context, string query, params SqlParameter[] sqlParams)
        {
            var conn = context.Database.Connection;
            try
            {
                using (var command = conn.CreateCommand())
                {
                    if (context.Database.CurrentTransaction == null)
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }
                    else
                    {
                        command.Transaction = context.Database.CurrentTransaction.UnderlyingTransaction;
                    }
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (sqlParams != null) command.Parameters.AddRange(sqlParams);
                    T result = (T)command.ExecuteScalar();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (context.Database.CurrentTransaction == null)
                    conn.Close();
            }


        }

        public class DataReaderMapper<T>
        {
            Dictionary<int, Either<FieldInfo, PropertyInfo>> mappings;
            bool IsPrimitiveish;
            public DataReaderMapper(IDataReader reader)
            {
                Type U = Nullable.GetUnderlyingType(typeof(T));
                this.IsPrimitiveish = (
                    typeof(T).IsPrimitive ||
                    (U != null && U.IsPrimitive)
                );
                if (!this.IsPrimitiveish)
                {
                    this.mappings = Mappings(reader);
                }
            }

            public class MapMismatchException : Exception
            {
                public MapMismatchException(string arg) : base(arg) { }
            }

            private class JoinInfo
            {
                public Either<FieldInfo, PropertyInfo> info;
                public String name;
            }
            // int keys are column indices (ordinals)
            static Dictionary<int, Either<FieldInfo, PropertyInfo>> Mappings(IDataReader reader)
            {
                var columns = Enumerable.Range(0, reader.FieldCount);
                var fieldsAndProps = typeof(T).FieldsAndProps()
                    .Select(fp => fp.Match(
                        f => new JoinInfo { info = f, name = f.Name },
                        p => new JoinInfo { info = p, name = p.Name }
                    ));
                var joined = columns
                      .Join(fieldsAndProps, reader.GetName, x => x.name, (index, x) => new
                      {
                          index,
                          x.info
                      }, StringComparer.InvariantCultureIgnoreCase).ToList();
                //if (columns.Count() != joined.Count() || fieldsAndProps.Count() != joined.Count())
                //{
                //    throw new MapMismatchException($"Expected to map every column in the result. " +
                //        $"Instead, {columns.Count()} columns and {fieldsAndProps.Count()} fields produced only {joined.Count()} matches. " +
                //        $"Hint: be sure all your columns have _names_, and the names match up.");
                //}
                return joined
                     .ToDictionary(x => x.index, x => x.info);
            }

            public T MapFrom(IDataRecord record)
            {
                if (IsPrimitiveish)
                {
                    // Primitive values will always have a single column, indexed by 0                                        
                    return record.GetValueOrDefault<T>(0);
                }
                var element = Activator.CreateInstance<T>();
                foreach (var map in mappings)
                    map.Value.Match(
                        f => f.SetValue(element, ChangeType(record[map.Key], f.FieldType)),
                        p => p.SetValue(element, ChangeType(record[map.Key], p.PropertyType))
                    );

                return element;
            }

            static object ChangeType(object value, Type targetType)
            {
                if (value == null || value == DBNull.Value)
                    return null;

                return Convert.ChangeType(value, Nullable.GetUnderlyingType(targetType) ?? targetType);
            }
        }

        private static T GetValueOrDefault<T>(this IDataRecord row, string fieldName)
        {
            int ordinal = row.GetOrdinal(fieldName);
            return row.GetValueOrDefault<T>(ordinal);
        }

        private static T GetValueOrDefault<T>(this IDataRecord row, int ordinal)
        {
            return (T)(row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal));
        }

    }

    public abstract class Either<L, R>
    {
        public abstract T Match<T>(Func<L, T> f, Func<R, T> g);
        public abstract void Match(Action<L> f, Action<R> g);

        public abstract Either<T, R> Map<T>(Func<L, T> f);

        // private ctor ensures no external classes can inherit
        // (not sure why that's important, but it's in the SO answer)
        private Either() { }
        public sealed class Left : Either<L, R>
        {
            public readonly L Item;
            public Left(L item) : base() { this.Item = item; }
            public override T Match<T>(Func<L, T> f, Func<R, T> g)
            {
                return f(Item);
            }
            public override void Match(Action<L> f, Action<R> g)
            {
                f(Item);
            }
            public override Either<T, R> Map<T>(Func<L, T> f)
            {
                return f(Item);
            }
        }
        public static implicit operator Either<L, R>(L ell)
        {
            return new Left(ell);
        }

        public sealed class Right : Either<L, R>
        {
            public readonly R Item;
            public Right(R item) { this.Item = item; }
            public override T Match<T>(Func<L, T> f, Func<R, T> g)
            {
                return g(Item);
            }
            public override void Match(Action<L> f, Action<R> g)
            {
                g(Item);
            }
            public override Either<T, R> Map<T>(Func<L, T> f)
            {
                return Item;
            }


        }
        public static implicit operator Either<L, R>(R arr)
        {
            return new Right(arr);
        }
    }

    public static class FieldAndPropsExtension
    {
        public static IEnumerable<Either<FieldInfo, PropertyInfo>> FieldsAndProps(this Type T)
        {
            var lst = new List<Either<FieldInfo, PropertyInfo>>();
            lst.AddRange(T.GetFields().Select(field => new Either<FieldInfo, PropertyInfo>.Left(field)));
            lst.AddRange(T.GetProperties().Select(prop => new Either<FieldInfo, PropertyInfo>.Right(prop)));
            return lst;
        }
    }
}
