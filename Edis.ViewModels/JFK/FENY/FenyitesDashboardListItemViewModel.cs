using Edis.Entities.JFK.FENY;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY
{
    public class FenyitesDashboardListItemViewModel
    {
        public int FogvatartottId { get; set; }
        public int FegyelmiUgyId { get; set; }

        public int? FenyitesId { get; set; }

        public int? FenyitesEljarasFoka { get; set; }
        public string SzuletesiNev { get; set; }
        public string AktNyilvantartasiSzam { get; set; }
        public int FogvSzemelyId { get; set; }
        public string Elhelyezes { get; set; }
        public string NyilvantartottStatusz { get; set; }

        public string NyilvantartottStatuszAzon { get; set; }
        public int UgyEve { get; set; }
        public int UgySzama { get; set; }
        public string UgyStatusz { get; set; }
        public string UgyStatuszAzon { get; set; }
        public string FenyitesStatusz { get; set; }

        public string FenyitesStatuszAzon { get; set; }
        public string VetsegTipusNev { get; set; }
        public string UgyIntezet { get; set; }
        public string UgyIntezetAzon { get; set; }

        public int UgyIntezetId { get; set; }
        public DateTime KezdemenyezesDatum { get; set; }
        public DateTime? HataridoDatum { get; set; }
        public string Kivizsgalo { get; set; }
        public string KivizsgaloSid { get; set; }
        public string Elrendelo { get; set; }
        public string ElrendeloSid { get; set; }
        public string UtolsoModositoSzemely { get; set; }
        public string UtolsoModositoSzemelySid { get; set; }
        public DateTime? UtolsoModositasDatum { get; set; }
        public string UgyIndoklas { get; set; }
        public bool Lejart { get; set; }
        public bool AHetenJarLe { get; set; }
        public string FenyitesTipus { get; set; }

        public string FenyitesTipusAzon { get; set; }

        public DateTime? FogvKepDatuma { get; set; }
        public string  Kep { get; set; }
        public string RelevansIntezet { get; set; }
        public int RelevansIntezetId { get; set; }

        public int UgyStatuszKszId { get; set; }

        public string HatarozatJogkorAzon { get; set; }

        public string FegyelmiMasodfokJogkorAzon { get; set; }
        public DateTime? LegkozelebbiSzallitasDatuma { get; set; }


        public static explicit operator FenyitesDashboardListItemViewModel(FenyitesDashboardListItem item)
        {
            FenyitesDashboardListItemViewModel model = new FenyitesDashboardListItemViewModel();
            model = ValueInjecterUtilities.InjectViewModel<FenyitesDashboardListItem, FenyitesDashboardListItemViewModel>(item);
           


            return model;
        }

        public static explicit operator FenyitesDashboardListItem(FenyitesDashboardListItemViewModel model)
        {
            FenyitesDashboardListItem entity = new FenyitesDashboardListItem();
            entity = ValueInjecterUtilities.InjectViewModel<FenyitesDashboardListItemViewModel, FenyitesDashboardListItem>(model);

            return entity;
        }
    }
}
