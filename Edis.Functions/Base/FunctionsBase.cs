using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Repositories.Contexts;
using Edis.Entities.Base;
using System.Reflection;
using System.Collections;
using Omu.ValueInjecter;
using System.ComponentModel.DataAnnotations;
using Edis.Entities.Fany;
using Edis.Functions.Common;
using Edis.IoC;
using Edis.ViewModels.Base;

namespace Edis.Functions.Base
{
    //public class FunctionsBase<TModel, TEntity> : IDisposable, IFunctionsBase<TModel, TEntity>
    //    where TEntity : class, new()
    //    where TModel : class
    //{
    //    //protected FanyContext FanyContext { get; set; }

    //    public ModelQueries<TModel> ModelQuery
    //    {
    //        get
    //        {
    //            var ctx = new EntityContext<TModel, TEntity>(FanyContext);
    //            return new ModelQueries<TModel>(ctx);
    //        }
    //    }

    //    public DbContextTransaction BeginTransaction(IsolationLevel level)
    //    {
    //        return this.FanyContext.Database.BeginTransaction(level);
    //    }

    //    public DbContextTransaction BeginTransaction()
    //    {
    //        return this.FanyContext.Database.BeginTransaction();
    //    }

    //    public void Save()
    //    {
    //        FanyContext.SaveChanges();
    //    }

    //    protected FunctionsBase()
    //    {
    //        this.FanyContext = new FanyContext();
    //    }

    //    public void Dispose()
    //    {
    //        this.FanyContext.Dispose();
    //    }

    //    public TEntity FindByAzonosito<TEntity>(string azonosito) where TEntity : BaseEntity, IAzonositovalRendelkezo
    //    {
    //        return FanyContext.Set<TEntity>().SingleOrDefault(x => x.Azonosito == azonosito);
    //    }

    //    public virtual int Create(TModel model)
    //    {
    //        if (model == null)
    //        {
    //            throw new Exception(string.Format("FunctionBase:Create: Given model is null"));
    //        }

    //        TEntity entity = (TEntity)((dynamic)model);//new TEntity();
    //        //entity.InjectFrom(model);

    //        var entityLists = typeof(TEntity)
    //                 .GetProperties(BindingFlags.NonPublic |
    //                                BindingFlags.Public |
    //                                BindingFlags.Instance).Where(p => p.IsDefined(typeof(EntityOperation), true)).ToList();

    //        foreach (var item in entityLists)
    //        {
    //            var val = item.GetValue(entity);
    //            if (val is IEnumerable && ((EntityOperation)item.GetCustomAttribute(typeof(EntityOperation), false)).ContainsCreate)
    //                foreach (var listItem in val as IEnumerable)
    //                {
    //                    FanyContext.Entry(listItem).State = EntityState.Unchanged;
    //                }

    //        }

    //        FanyContext.Set<TEntity>().Add(entity);
    //        FanyContext.SaveChanges();

    //        var properties = entity.GetType().GetProperties();
    //        var key = properties.FirstOrDefault(p => p.GetCustomAttributes(false)
    //            .Any(a => a is KeyAttribute));

    //        int retVal = -1;
    //        int.TryParse(key.GetValue(entity).ToString(), out retVal);

    //        return retVal;
    //    }

    //    public IQueryable<TModel> GetAll()
    //    {
    //        //foreach (var item in Context.Set<TEntity>)
    //        //{
    //        //    addMethod.Invoke(o, new[] { ToModel(modelType,type,item)});
    //        //}

    //        //List<TModel> list = new List<TModel>();
    //        //foreach (var item in Context.Set<TEntity>())
    //        //{
    //        //    list.Add((TModel)((dynamic)item));
    //        //}
    //        //return list.AsQueryable<TModel>();


    //        return FanyContext.Set<TEntity>().ToList().Select(x => (dynamic)x).Select(x => (TModel)x).AsQueryable();


    //    }

    //    public void Modify(TModel model)
    //    {
    //        if (model == null)
    //        {
    //            throw new Exception(string.Format("FunctionBase:Update: Given model is null"));
    //        }

    //        // find primary key attribute
    //        var key = typeof(TModel).GetProperties().Single(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
    //        // get id value
    //        var id = model.GetType().GetProperty(key.Name).GetValue(model);

    //        var entity = FanyContext.Set<TEntity>().Find(id);

    //        //TEntity entity = (TEntity)((dynamic)model);

    //        var modelType = typeof(TModel);
    //        var entityType = typeof(TEntity);

    //        var properties = typeof(TModel)
    //                .GetProperties(BindingFlags.NonPublic |
    //                               BindingFlags.Public |
    //                               BindingFlags.Instance).Where(p => !p.IsDefined(typeof(ReadOnlyAttribute), true)).ToList();


    //        //var properties = entityType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public |
    //        //                         BindingFlags.Instance).Where(p => p.IsDefined(typeof(EntityOperation), true)).ToList();

    //        foreach (var modelProperty in properties)
    //        {
    //            var val = modelType.GetProperty(modelProperty.Name).GetValue(model);

    //            var entityPropertyType = entityType.GetProperty(modelProperty.Name);
    //            if (entityPropertyType != null)
    //            {
    //                if (entityPropertyType.PropertyType == modelProperty.PropertyType)
    //                {
    //                    entityPropertyType.SetValue(entity, modelProperty.GetValue(model));
    //                }
    //            }
    //        }

    //        FanyContext.SaveChanges();
    //    }

    //    public virtual void Delete(int id)
    //    {
    //        if (id <= 0)
    //        {
    //            throw new Exception(string.Format("FunctionBase:Delete: invalid id"));
    //        }

    //        var entity = FanyContext.Set<TEntity>().Find(id);

    //        if (entity == null)
    //            throw new Exception(string.Format("FunctionBase:Delete: entity not exits, id: " + id));
    //        var entityType = typeof(TEntity);
    //        var properties = entityType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public |
    //                                BindingFlags.Instance).Where(p => p.IsDefined(typeof(EntityOperation), true)).ToList();

    //        foreach (var item in properties)
    //        {
    //            if (!((EntityOperation)item.GetCustomAttribute(typeof(EntityOperation), false)).ContainsDelete)
    //                continue;

    //            var property = entityType.GetProperty(item.Name).GetValue(entity);

    //            var clearMethod = property.GetType().GetMethod("Clear");
    //            if (clearMethod != null)
    //                clearMethod.Invoke(property, null);
    //        }


    //        FanyContext.Entry(entity).State = EntityState.Deleted;
    //        FanyContext.SaveChanges();
    //    }

    //    public virtual TModel FindById(int id)
    //    {
    //        if (id <= 0)
    //        {
    //            throw new Exception(string.Format("FunctionBase:FindById: invalid id"));
    //        }

    //        var entity = FanyContext.Set<TEntity>().Find(id);

    //        if (entity == null)
    //            throw new Exception(string.Format("FunctionBase:FindById: entity not exits, id: " + id));

    //        return (TModel)((dynamic)entity);
    //    }


    //}
}
