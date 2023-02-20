using Edis.Entities.Base;
using Edis.Entities.Fany;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Fany
{

    public class FogvatartottSzemelyesAdataiViewModel
    {


        #region jellemzők

        public string CsaladiNev { get; set; }

        public string Utonev { get; set; }

        public string SzuletesiCsaladiNev { get; set; }

        public string SzuletesiUtonev { get; set; }

        public string SzuletesiHelyNeve { get; set; }

        public DateTime SzuletesiDatum { get; set; }

        public string AnyjaNeve { get; set; }

        public string SzemelyiAzonositoJel { get; set; }

        public string SzemelyigazolvanySzam { get; set; }

        public string AdoazonositoJel { get; set; }

        public string TajSzam { get; set; }

        public string UtlevelSzam { get; set; }

        public int? AllandoLakcimIranyitoszam { get; set; }

        public string AllandoLakcimHelysegnev { get; set; }

        public string AllandoLakcimUtca { get; set; }

        public string AllandoLakcimHazszam { get; set; }

        public int? BejelentettLakcimIranyitoszam { get; set; }

        public string BejelentettLakcimHelysegnev { get; set; }

        public string BejelentettLakcimUtca { get; set; }

        public string BejelentettLakcimHazszam { get; set; }

        public int? GyermekekSzama { get; set; }

        public int FogvatartottId { get; set; }
        
        public int FogvatartottSzemelyId { get; set; }
        
        public int NemId { get; set; }
        
        public int? SzuletesiOrszagId { get; set; }
        
        public int? AllampolgarsagId { get; set; }
        
        public int? SzuletesiHelysegId { get; set; }
        
        public int? AllandoLakcimOrszagId { get; set; }
        
        public int? AllandoLakcimHelysegId { get; set; }
        
        public int? BejelentettLakcimOrszagId { get; set; }
        
        public int? BejelentettLakcimHelysegId { get; set; }
        
        public int? IskolaiVegzettsegId { get; set; }
        
        public int? SzakkepzettsegId { get; set; }
        
        public int? CsaladiAllapotId { get; set; }
        
        public int? TajSzamTipusId { get; set; }
        
        public int? IrategyesitesFogvatartottId { get; set; }


        public KodszotarModel Nem { get; set; }
        public virtual KodszotarModel Allampolgarsag { get; set; }
        public virtual KodszotarModel IskolaiVegzettseg { get; set; }

        #endregion jellemzők

        public static explicit operator FogvatartottSzemelyesAdataiViewModel(FogvatartottSzemelyesAdatai item)
        {
            FogvatartottSzemelyesAdataiViewModel model = new FogvatartottSzemelyesAdataiViewModel();
            model = ValueInjecterUtilities.InjectViewModel<FogvatartottSzemelyesAdatai, FogvatartottSzemelyesAdataiViewModel>(item);

            if (item.Nem != null)
                model.Nem = (KodszotarModel)item.Nem;

            if (item.Allampolgarsag != null)
                model.Allampolgarsag = (KodszotarModel)item.Allampolgarsag;

            if (item.IskolaiVegzettseg != null)
                model.IskolaiVegzettseg = (KodszotarModel)item.IskolaiVegzettseg;


            return model;
        }



        public static explicit operator FogvatartottSzemelyesAdatai(FogvatartottSzemelyesAdataiViewModel model)
        {
            FogvatartottSzemelyesAdatai entity = new FogvatartottSzemelyesAdatai();
            entity = ValueInjecterUtilities.InjectViewModel<FogvatartottSzemelyesAdataiViewModel, FogvatartottSzemelyesAdatai>(model);

            return entity;
        }
    }
}
