using Edis.Entities.Fany;
using Edis.Entities.JFK.FENY;
using Edis.Entities.JFK.Jutalmazas;
using Edis.Utilities;
using Edis.ViewModels.Base;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY
{
    public class FogvatartottListaItemFegyelmiViewModel : BaseViewModel
    {
      
        public string KeresoAzonKod { get; set; }

        public string KeresoNev { get; set; }

        public string NyilvantartasiAzonosito { get; set; }

        public DateTime SzuletesiDatum { get; set; }

        public string SzuletesiCsaladiNev { get; set; }

        public string SzuletesiUtonev { get; set; }

        public string ViseltCsaladiNev { get; set; }

        public string ViseltUtonev { get; set; }

        public string AnyjaNeve { get; set; }

        public string NyilvantartasiStatusz { get; set; }

        public int IntezetId { get; set; }

        public string IntezetAzon { get; set; }

        public int? ObjektumId { get; set; }

        public int? KorletId { get; set; }

        public int? ZarkaId { get; set; }

        public string ObjektumNev { get; set; }

        public string KorletNev { get; set; }

        public string ZarkaAzon { get; set; }
        //[Column("GyogyitoCsoportok")]
        //public string GyogyitoCsoportok { get; set; }

        public DateTime? AktSzabadDatum { get; set; }


        public static explicit operator FogvatartottListaItemFegyelmiViewModel(FogvatartottListaItemFegyelmiView item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<FogvatartottListaItemFegyelmiView, FogvatartottListaItemFegyelmiViewModel>(item);

          
            return model;
        }

        public static explicit operator FogvatartottListaItemFegyelmiView(FogvatartottListaItemFegyelmiViewModel model)
        {
            FogvatartottListaItemFegyelmiView entity = new FogvatartottListaItemFegyelmiView();
            entity = ValueInjecterUtilities.InjectViewModel<FogvatartottListaItemFegyelmiViewModel, FogvatartottListaItemFegyelmiView>(model);

            return entity;
        }
    }
}
