using Edis.Entities.JFK.FENY;
using Edis.Utilities;
using Edis.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY
{
    public class SzakteruletiVelemenyKeresekViewModel : BaseViewModel
    {
        public string FegyelmiUgyIdLista { get; set; }
        public string SzakvelemenyKeroje { get; set; }
        public string CimzettLista { get; set; }
        public string Tema { get; set; }
        public string GeneraltLink { get; set; }
        public DateTime? Velemenyezve { get; set; }
        public DateTime HatarIdo { get; set; }

        public static explicit operator SzakteruletiVelemenyKeresekViewModel(SzakteruletiVelemenyKeresek item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<SzakteruletiVelemenyKeresek, SzakteruletiVelemenyKeresekViewModel>(item);
            return model;
        }

        public static explicit operator SzakteruletiVelemenyKeresek(SzakteruletiVelemenyKeresekViewModel model)
        {
            SzakteruletiVelemenyKeresek entity = new SzakteruletiVelemenyKeresek();
            entity = ValueInjecterUtilities.InjectViewModel<SzakteruletiVelemenyKeresekViewModel, SzakteruletiVelemenyKeresek>(model);

            return entity;
        }
    }
}
