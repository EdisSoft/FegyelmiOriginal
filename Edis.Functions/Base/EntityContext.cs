using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Base
{
    public class EntityContext<Tm, Te>
         where Tm : class
         where Te : class, new()
    {


        DbContext context;
        IQueryable<Te> query;

        public EntityContext(DbContext context)
        {
            this.context = context;
            query = context.Set<Te>().AsQueryable();
        }

        public List<Tm> ToList()
        {
            var list = query.ToList();
            context.Configuration.LazyLoadingEnabled = false;
            List<Tm> models = new List<Tm>();
            foreach (var item in list)
            {
                models.Add((Tm)((dynamic)item));
            }
            return models;
        }

        public Tm FirstOrDefault(Expression<Func<Tm, bool>> expr)
        {

            Te entity = null;
            if (expr == null)
                entity = query.FirstOrDefault();
            else
                entity = query.FirstOrDefault(ExpressionHelper.Convert<Tm, Te>(expr));
            context.Configuration.LazyLoadingEnabled = false;
            if (entity == null)
                return null;
            else
                return (Tm)((dynamic)entity);
        }

        public Tm First(Expression<Func<Tm, bool>> expr)
        {

            Te entity = null;
            if (expr == null)
                entity= query.First();
            else
                entity = query.First(ExpressionHelper.Convert<Tm, Te>(expr));
            context.Configuration.LazyLoadingEnabled = false;

            return (Tm)((dynamic)entity);
        }

        public Tm SingleOrDefault(Expression<Func<Tm, bool>> expr)
        {

            Te entity = null;
            if (expr == null)
                entity = query.SingleOrDefault();
            else
                entity = query.SingleOrDefault(ExpressionHelper.Convert<Tm, Te>(expr));
            context.Configuration.LazyLoadingEnabled = false;
            if (entity == null)
                return null;
            else
                return (Tm)((dynamic)entity);
        }

        public Tm Single(Expression<Func<Tm, bool>> expr)
        {

            Te entity = null;
            if (expr == null)
                entity = query.Single();
            else
                entity = query.Single(ExpressionHelper.Convert<Tm, Te>(expr));
            context.Configuration.LazyLoadingEnabled = false;

            return (Tm)((dynamic)entity);
        }

        public Tm LastOrDefault(Expression<Func<Tm, bool>> expr)
        {

            Te entity = null;
            if (expr == null)
                entity = query.LastOrDefault();
            else
                entity = query.LastOrDefault(ExpressionHelper.Convert<Tm, Te>(expr));
            context.Configuration.LazyLoadingEnabled = false;
            if (entity == null)
                return null;
            else
                return (Tm)((dynamic)entity);
        }

        public Tm Last(Expression<Func<Tm, bool>> expr)
        {

            Te entity = null;
            if (expr == null)
                entity = query.Last();
            else
                entity = query.Last(ExpressionHelper.Convert<Tm, Te>(expr));
            context.Configuration.LazyLoadingEnabled = false;

            return (Tm)((dynamic)entity);
        }

        public int Count(Expression<Func<Tm, bool>> expr)
        {
            int c = 0;
            if (expr == null)
                c = query.Count();
            else
                c = query.Count(ExpressionHelper.Convert<Tm, Te>(expr));
            return c;
        }

        public void Where(Expression<Func<Tm, bool>> expr)
        {
            query = query.Where(ExpressionHelper.Convert<Tm, Te>(expr));
        }

        public void Include(Expression<Func<Tm, object>> expr)
        {
            query = query.Include(ExpressionHelper.Convert<Tm, Te>(expr));
        }
        public void OrderBy(Expression<Func<Tm, object>> expr)
        {
            query = query.OrderBy(ExpressionHelper.Convert<Tm, Te>(expr));
        }

        public void ThenBy(Expression<Func<Tm, object>> expr)
        {
            query = ((IOrderedQueryable<Te>)query).ThenBy(ExpressionHelper.Convert<Tm, Te>(expr));
        }

        public void OrderByDescending(Expression<Func<Tm, object>> expr)
        {
            query = query.OrderByDescending(ExpressionHelper.Convert<Tm, Te>(expr));
        }

        public void ThenByDescending(Expression<Func<Tm, object>> expr)
        {
            query = ((IOrderedQueryable<Te>)query).ThenByDescending(ExpressionHelper.Convert<Tm, Te>(expr));
        }

        public void AsNoTracking()
        {
            query = query.AsNoTracking();
        }

        public bool Any(Expression<Func<Tm, bool>> expr)
        {
            bool c = false;
            if (expr == null)
                c = query.Any();
            else
                c = query.Any(ExpressionHelper.Convert<Tm, Te>(expr));
            return c;
        }
    }
}
