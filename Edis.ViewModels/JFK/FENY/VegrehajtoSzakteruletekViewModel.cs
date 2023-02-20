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
    public class VegrehajtoSzakteruletekViewModel : BaseViewModel
    {
        public int IntezetId { get; set; }
        public string LetetesCimzettLista { get; set; }
        public string RendezvenySzervezoCimzettLista { get; set; }

        public static explicit operator VegrehajtoSzakteruletekViewModel(VegrehajtoSzakteruletek item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<VegrehajtoSzakteruletek, VegrehajtoSzakteruletekViewModel>(item);
            return model;
        }

        public static explicit operator VegrehajtoSzakteruletek(VegrehajtoSzakteruletekViewModel model)
        {
            VegrehajtoSzakteruletek entity = new VegrehajtoSzakteruletek();
            entity = ValueInjecterUtilities.InjectViewModel<VegrehajtoSzakteruletekViewModel, VegrehajtoSzakteruletek>(model);

            return entity;
        }
    }
}
