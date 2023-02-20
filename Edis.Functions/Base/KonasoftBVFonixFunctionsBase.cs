using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Edis.Repositories.Contexts;
using Edis.Entities.Base;
using System.Reflection;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Edis.ViewModels.Base;
using Edis.Repositories.Utils;

namespace Edis.Functions.Base
{
    public class KonasoftBVFonixFunctionsBase<TModel, TEntity> : IDisposable, IKonasoftBVFonixFunctionsBase<TModel, TEntity>
        where TEntity : class, new()
        where TModel : class
    {

        private KonasoftBVFonixContext _KonasoftBVFonixContext;
        public KonasoftBVFonixContext KonasoftBVFonixContext 
        { 
            get
            {
                if (_KonasoftBVFonixContext == null || _KonasoftBVFonixContext.IsDisposed())
                {
                    _KonasoftBVFonixContext = Repositories.Contexts.KonasoftBVFonixContext.GetContextInstance();
                }
                return _KonasoftBVFonixContext;
            }
            set
            {
                if (value?.IsDisposed() ?? false)
                {
                    _KonasoftBVFonixContext = Repositories.Contexts.KonasoftBVFonixContext.GetContextInstance();
                }
                else
                {
                    _KonasoftBVFonixContext = value;
                }
            }
        }

        public DbContextTransaction BeginTransaction(IsolationLevel level)
        {
            return this.KonasoftBVFonixContext.Database.BeginTransaction(level);
        }

        public ModelQueries<TModel> ModelQuery
        {
            get
            {
                var ctx = new EntityContext<TModel, TEntity>(KonasoftBVFonixContext);
                return new ModelQueries<TModel>(ctx);
            }
        }

        public DbContextTransaction BeginTransaction()
        {
            return this.KonasoftBVFonixContext.Database.BeginTransaction();
        }

        public void Save()
        {
            KonasoftBVFonixContext.SaveChanges();
        }

        protected KonasoftBVFonixFunctionsBase()
        {
            this.KonasoftBVFonixContext = KonasoftBVFonixContext.GetContextInstance();
        }

        public void Dispose()
        {
            //this.KonasoftBVFonixContext.Dispose();
        }



        public TEntity FindByAzonosito<TEntity>(string azonosito) where TEntity : BaseEntity, IAzonositovalRendelkezo
        {
            return KonasoftBVFonixContext.Set<TEntity>().SingleOrDefault(x => x.Azonosito == azonosito);
        }

        public virtual int Create(TModel model)
        {
            if (model == null)
            {
                throw new Exception(string.Format("FunctionBase:Create: Given model is null"));
            }

            TEntity entity = (TEntity)((dynamic)model);//new TEntity();
            //entity.InjectFrom(model);

            var entityLists = typeof(TEntity)
                     .GetProperties(BindingFlags.NonPublic |
                                    BindingFlags.Public |
                                    BindingFlags.Instance).Where(p => p.IsDefined(typeof(EntityOperation), true)).ToList();

            foreach (var item in entityLists)
            {
                var val = item.GetValue(entity);
                if (val is IEnumerable && ((EntityOperation)item.GetCustomAttribute(typeof(EntityOperation), false)).ContainsCreate)
                    foreach (var listItem in val as IEnumerable)
                    {
                        KonasoftBVFonixContext.Entry(listItem).State = EntityState.Unchanged;
                    }

            }

            KonasoftBVFonixContext.Set<TEntity>().Add(entity);
            KonasoftBVFonixContext.SaveChanges();

            var properties = entity.GetType().GetProperties();
            var key = properties.FirstOrDefault(p => p.GetCustomAttributes(false)
                .Any(a => a is KeyAttribute));

            int retVal = -1;
            int.TryParse(key.GetValue(entity).ToString(), out retVal);

            return retVal;
        }

        public virtual IQueryable<TModel> GetAll()
        {
            //foreach (var item in Context.Set<TEntity>)
            //{
            //    addMethod.Invoke(o, new[] { ToModel(modelType,type,item)});
            //}

            //List<TModel> list = new List<TModel>();
            //foreach (var item in Context.Set<TEntity>())
            //{
            //    list.Add((TModel)((dynamic)item));
            //}
            //return list.AsQueryable<TModel>();

            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            return KonasoftBVFonixContext.Set<TEntity>().ToList().Select(x => (dynamic)x).Select(x => (TModel)x).AsQueryable();


        }

        public virtual void Modify(TModel model)
        {
            if (model == null)
            {
                throw new Exception(string.Format("FunctionBase:Update: Given model is null"));
            }

            // find primary key attribute
            var key = typeof(TModel).GetProperties().Single(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            // get id value
            var id = model.GetType().GetProperty(key.Name).GetValue(model);

            var entity = KonasoftBVFonixContext.Set<TEntity>().Find(id);

            //TEntity entity = (TEntity)((dynamic)model);

            var modelType = typeof(TModel);
            var entityType = typeof(TEntity);

            var properties = typeof(TModel)
                    .GetProperties(BindingFlags.NonPublic |
                                   BindingFlags.Public |
                                   BindingFlags.Instance).Where(p => !p.IsDefined(typeof(ReadOnlyAttribute), true)).ToList();


            //var properties = entityType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public |
            //                         BindingFlags.Instance).Where(p => p.IsDefined(typeof(EntityOperation), true)).ToList();

            foreach (var modelProperty in properties)
            {
                var val = modelType.GetProperty(modelProperty.Name).GetValue(model);

                var entityPropertyType = entityType.GetProperty(modelProperty.Name);
                if (entityPropertyType != null)
                {
                    if (entityPropertyType.PropertyType == modelProperty.PropertyType)
                    {
                        if(entityPropertyType.CanWrite)
                        entityPropertyType.SetValue(entity, modelProperty.GetValue(model));
                    }
                }
            }

            KonasoftBVFonixContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            DeleteAndReturn(id);
        }

        public virtual TModel FindById(int id)
        {
            if (id <= 0)
            {
                throw new Exception(string.Format("FunctionBase:FindById: invalid id"));
            }

            var entity = KonasoftBVFonixContext.Set<TEntity>().Find(id);
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            if (entity == null)
                throw new Exception(string.Format("FunctionBase:FindById: entity not exits, id: " + id));

            return (TModel)((dynamic)entity);
        }

        public TEntity DeleteAndReturn(int id)
        {
            if (id <= 0)
            {
                throw new Exception(string.Format("FunctionBase:Delete: invalid id"));
            }

            var entity = KonasoftBVFonixContext.Set<TEntity>().Find(id);

            if (entity == null)
                throw new Exception(string.Format("FunctionBase:Delete: entity not exits, id: " + id));
            var entityType = typeof(TEntity);

            if (entityType.GetProperty("TOROLT_FL") != null)
            {
                entityType.GetProperty("TOROLT_FL").SetValue(entity, true);
            }
            else
            {
                KonasoftBVFonixContext.Set<TEntity>().Remove(entity);
            }

            KonasoftBVFonixContext.SaveChanges();
            return entity;
        }
    }
}
