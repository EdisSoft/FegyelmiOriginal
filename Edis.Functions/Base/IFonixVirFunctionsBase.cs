using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Base
{
    public interface IFonixVirFunctionsBase<TModel, TEntity> where TModel : class where TEntity : class
    {
        //DbSet<TEntity> Table { get; }

    }
}
