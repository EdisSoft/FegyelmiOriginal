using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.Dokumentum
{
    public class TargyalasiJegyzokonyv
    {
        public string IntezetNev { get; set; }
        public string Iktatoszam { get; set; }
        public string UgySzam { get; set; }
        public string MeghallgatasEve { get; set; }
        public string MeghallgatasHava { get; set; }
        public string MeghallgatasNapja { get; set; }
        
        public string EljarasAlaVontNev { get; set; }        
        public string EljarasAlaVontAzon { get; set; }
        public string EljarasAlaVontSzulIdeje { get; set; }
        public string EljarasAlaVontSzulHelye { get; set; }
        public string EljarasAlaVontAnyjaNeve { get; set; }        
        public string FegyelmiJogkorGyakorloNev { get; set; }
        public string FegyelmiJogkorGyakorloRang { get; set; }
        public string JegyzoNev { get; set; }
        public string JegyzoRang { get; set; }
        public string EgyebJelenlevo { get; set; }
        public string JegyzoKonyvText { get; set; }
        public string JegyzoKonyvZarasOra { get; set; }
        public string JegyzoKonyvZarasPerc { get; set; }
        public string MeghallgatasOraja { get; set; }
        public string MeghallgatasPerce { get; set; }
        public string JegyzokonyvTartalma { get; set; }

    }
}
