using System.Reflection;
using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;
using System.Linq;

namespace Edis.Utilities
{
    public static class ValueInjecterUtilities
    {
        public static TViewModel InjectViewModel<TEntity, TViewModel>(TEntity entity)
            where TViewModel : new()
        {
            TViewModel model = new TViewModel();
            model.InjectFrom(entity);
            return model;
        }
        
    }
}
