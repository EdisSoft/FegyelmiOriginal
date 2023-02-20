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
    public interface IFunctionsBase<TModel, TEntity> where TModel : class where TEntity : class
    {
        //DbSet<TEntity> Table { get; }
        ModelQueries<TModel> ModelQuery { get; }
        void Save();

        TEntity FindByAzonosito<TEntity>(string azonosito) where TEntity : BaseEntity, IAzonositovalRendelkezo;

        int Create(TModel model);
        IQueryable<TModel> GetAll();
        void Modify(TModel model);
        void Delete(int id);
        TModel FindById(int id);

    }
}
