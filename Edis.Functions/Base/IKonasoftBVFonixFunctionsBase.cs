using Edis.Entities.Base;
using System.Linq;

namespace Edis.Functions.Base
{
    public interface IKonasoftBVFonixFunctionsBase<TModel, TEntity> where TModel : class where TEntity : class
    {
        //DbSet<TEntity> Table { get; }

        ModelQueries<TModel> ModelQuery { get; }
        void Save();

        TEntity FindByAzonosito<TEntity>(string azonosito) where TEntity : BaseEntity, IAzonositovalRendelkezo;

        int Create(TModel model);
        IQueryable<TModel> GetAll();
        void Modify(TModel model);
        void Delete(int id);
        TEntity DeleteAndReturn(int id);
        TModel FindById(int id);

    }
}
