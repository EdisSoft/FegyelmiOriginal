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
    public class FenyitesVegrehajtasokViewModel : BaseViewModel
    {
        public int FegyelmiUgyId { get; set; }
        public DateTime? KezdeteIdo { get; set; }
        public DateTime? VegeIdo { get; set; }
        public double? Hossz { get; set; }
        public int? MennyisegiEgyegCimkeId { get; set; }

        public static explicit operator FenyitesVegrehajtasokViewModel(FenyitesVegrehajtasok item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<FenyitesVegrehajtasok, FenyitesVegrehajtasokViewModel>(item);
            return model;
        }

        public static explicit operator FenyitesVegrehajtasok(FenyitesVegrehajtasokViewModel model)
        {
            FenyitesVegrehajtasok entity = new FenyitesVegrehajtasok();
            entity = ValueInjecterUtilities.InjectViewModel<FenyitesVegrehajtasokViewModel, FenyitesVegrehajtasok>(model);

            return entity;
        }
    }
}
