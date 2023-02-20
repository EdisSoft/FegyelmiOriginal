using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Base
{
    public class ModelQueries<Tm>
    {
        object context;

        public ModelQueries(object ctx)
        {
            context = ctx;
        }

        public ModelQueries<Tm> Select(Expression<Func<Tm, bool>> expr)
        {
            context.GetType().GetMethod("Select").Invoke(context, new object[] { expr });
            return this;
        }

        public ModelQueries<Tm> Where(Expression<Func<Tm, bool>> expr)
        {
            context.GetType().GetMethod("Where").Invoke(context, new object[] { expr });
            return this;
        }

        public ModelQueries<Tm> Include(Expression<Func<Tm, object>> expr)
        {
            context.GetType().GetMethod("Include").Invoke(context, new object[] { expr });
            return this;
        }

        public ModelQueries<Tm> OrderBy(Expression<Func<Tm, object>> expr)
        {
            context.GetType().GetMethod("OrderBy").Invoke(context, new object[] { expr });
            return this;
        }

        public ModelQueries<Tm> GroupBy(Expression<Func<Tm, object>> expr)
        {
            context.GetType().GetMethod("GroupBy").Invoke(context, new object[] { expr });
            return this;
        }

        public ModelQueries<Tm> ThenBy(Expression<Func<Tm, object>> expr)
        {
            context.GetType().GetMethod("ThenBy").Invoke(context, new object[] { expr });
            return this;
        }

        public ModelQueries<Tm> OrderByDescending(Expression<Func<Tm, object>> expr)
        {
            context.GetType().GetMethod("OrderByDescending").Invoke(context, new object[] { expr });
            return this;
        }

        public ModelQueries<Tm> ThenByDescending(Expression<Func<Tm, object>> expr)
        {
            context.GetType().GetMethod("ThenByDescending").Invoke(context, new object[] { expr });
            return this;
        }

        public List<Tm> ToList()
        {
            var list = context.GetType().GetMethod("ToList").Invoke(context, null) as List<Tm>;
            return list;
        }
        
        public Tm FirstOrDefault(Expression<Func<Tm, bool>> expr=null)
        {
            Tm model = (Tm)context.GetType().GetMethod("FirstOrDefault").Invoke(context, new object[] { expr });
            return model;
        }

        public Tm First(Expression<Func<Tm, bool>> expr = null)
        {
            Tm model = (Tm)context.GetType().GetMethod("First").Invoke(context, new object[] { expr });
            return model;
        }

        public Tm SingleOrDefault(Expression<Func<Tm, bool>> expr = null)
        {
            Tm model = (Tm)context.GetType().GetMethod("SingleOrDefault").Invoke(context, new object[] { expr });
            return model;
        }

        public Tm Single(Expression<Func<Tm, bool>> expr = null)
        {
            Tm model = (Tm)context.GetType().GetMethod("Single").Invoke(context, new object[] { expr });
            return model;
        }

        public Tm LastOrDefault(Expression<Func<Tm, bool>> expr = null)
        {
            Tm model = (Tm)context.GetType().GetMethod("LastOrDefault").Invoke(context, new object[] { expr });
            return model;
        }

        public Tm Last(Expression<Func<Tm, bool>> expr = null)
        {
            Tm model = (Tm)context.GetType().GetMethod("Last").Invoke(context, new object[] { expr });
            return model;
        }

        public ModelQueries<Tm> AsNoTracking()
        {
            context.GetType().GetMethod("AsNoTracking").Invoke(context, null);
            return this;
        }

        public int Count(Expression<Func<Tm, bool>> expr = null)
        {
            var count = (int)context.GetType().GetMethod("Count").Invoke(context, new object[] { expr });
            return count;
        }

        public bool Any(Expression<Func<Tm, bool>> expr = null)
        {
            var result = (bool)context.GetType().GetMethod("Any").Invoke(context, new object[] { expr });
            return result;
        }

    }
}
