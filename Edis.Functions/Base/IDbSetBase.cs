using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Base
{
    public interface IDbSetBase<TEntity> where TEntity : class
    {
        DbSet<TEntity> Table { get; }
    }
}
