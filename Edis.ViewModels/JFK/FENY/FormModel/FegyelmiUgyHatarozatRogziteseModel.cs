namespace Edis.ViewModels.JFK.FENY
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class FegyelmiUgyHatarozatRogziteseModel
    {        
        public int FegyelmiUgyId { get; set; }
        [AllowHtml]
        public string Leiras { get; set; }
        public int? NaplobejegyzesId { get; set; }

        public bool PanasszalElt { get; set; }        

        public string ElsofokonHatarozathozoSzemely { get; set; }
        public int? FegyelmiVetsegTipusaCimkeId { get; set; }
        public int? VetsegRendeletSzerintCimkeId { get; set; }
        public int? FenyitesTipusaCimkeId { get; set; }
        public int? MegszuntetesOkaCimkeId { get; set; }        
        public byte? KietkezesCsokkentes { get; set; }
        public bool IsVegleges { get; set; }
        public bool IsKarterites { get; set; }
        public bool? IsSzandekosKarokozas { get; set; }

        public string FenyitesHosszaEsTipusa { get; set; }

        public int? FenyitesHossza
        {
            get
            {                
                if (Int32.TryParse(FenyitesHosszaEsTipusa?.Split(' ').ElementAtOrDefault(0), out int result))
                    return result;
                return null;
            }
        }

        public string FenyitesHosszaMennyisegiEgyseg
        {
            get
            {
                return FenyitesHosszaEsTipusa?.Split(' ').ElementAtOrDefault(1).ToString();                    
            }
        }

        public List<KSelect2ItemModel> FegyelmiVetsegTipusaOptions { get; set; }
        public List<KSelect2ItemModel> VetsegRendeletSzerintOptions { get; set; }        
        public List<KSelect2ItemModel> FenyitesTipusaOptions { get; set; }
        public List<KSelect2ItemModel> FenyitesTartamaKietkezesCsokkentesOptions { get; set; }
        public List<KSelect2ItemModel> FenyitesTartamaMaganElzarasOptions { get; set; }
        public List<KSelect2ItemModel> FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions { get; set; }
        public List<KSelect2ItemModel> FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions { get; set; }
        public List<KSelect2ItemModel> FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions { get; set; }
        public List<KSelect2ItemModel> FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions { get; set; }
        public List<KSelect2ItemModel> FenyitesMegszuntetesOkaOptions { get; set; }
        public List<KSelect2ItemModel> FenyitesTartamaKimaradasMegvonasaOptions { get; set; }              
    }
}
