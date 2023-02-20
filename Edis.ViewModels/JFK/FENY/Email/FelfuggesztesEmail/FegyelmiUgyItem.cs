using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.Email
{
    public class FegyelmiUgyItem
    {
        public int FegyelmiUgyId { get; set; }
        public int FegyelmiUgyIntezetId { get; set; }
        public string FogvatartottNyilvantartasiSzam { get; set; }
        public string FogvatartottNev { get; set; }
        public int FogvatartottId { get; set; }
        public int FogvatartottAktualisIntezetId { get; set; }

        public string Ugyszam { get; set; }
        public string FegyelmiVetseg { get; set; }

        public string FenyitesTipusa { get; set; }
        public string FenyitesTartam { get; set; }
        //public DateTime FenyitesKezdete { get; set; }
        //public int FenyitesHossza { get; set; }
        
        public string HatarozatSzovege { get; set; }
        public int FegyelmiUgyStatuszCimkeId { get; set; }
        public string EsemenyLeirasa { get; set; }

        public DateTime EsemenyDatuma { get; set; }
        public string KivizsgaloSid { get; set; }
        public string KivizsgaloBeosztas { get; set; }
        public string KivizsgaloNev { get; set; }
        public string ElrendeloSid { get; set; }
        public string ElrendeloBeosztas { get; set; }
        public string ElrendeloNev { get; set; }
        public int? NevelesiCsoportId { get; set; }
        public string FofelugyeloSid { get; set; }
        public string FofelugyeloNev { get; set; }
        public DateTime? ElrendelesRogzitesIdeje { get; set; }
        public DateTime? SzallitasIdeje { get; set; }
        public int NaploId { get; set; }
        public string HatarozatotHozottSid { get; set; }
        public string HatarozatotHozottNev { get; set; }

    }
}
