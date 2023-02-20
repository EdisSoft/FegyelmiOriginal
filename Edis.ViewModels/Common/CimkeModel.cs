using Edis.Entities.Common;
using Edis.Utilities;
using Edis.ViewModels.Base;

namespace Edis.ViewModels.Common
{
    public class CimkeModel : BaseViewModel
    {
        public string Nev { get; set; }
        
        public string FelhoId { get; set; }

        public static explicit operator CimkeModel(Cimke item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<Cimke, CimkeModel>(item);
            return model;
        }
    }
}
