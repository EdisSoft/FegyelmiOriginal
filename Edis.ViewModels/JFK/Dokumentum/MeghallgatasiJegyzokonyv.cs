using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.Dokumentum
{
    public class MeghallgatasiJegyzokonyv
    {
        public string IntezetNev { get; set; }
        public string Iktatoszam { get; set; }
        public string UgySzam { get; set; }
        public string MeghallgatasEve { get; set; }
        public string MeghallgatasHava { get; set; }
        public string MeghallgatasNapja { get; set; }
        
        public string MeghallgatottNev { get; set; }
        public string MeghallgatottRang { get; set; }
        public string MeghallgatottAzon { get; set; }
        public string MeghallgatottSzulIdeje { get; set; }
        public string MeghallgatottSzulHelye { get; set; }
        public string MeghallgatottAnyjaNeve { get; set; }
        //public string Elfogult { get; set; }
        public string MeghallgatoNev { get; set; }
        public string MeghallgatoRang { get; set; }
        public string JegyzoNev { get; set; }
        public string JegyzoRang { get; set; }
        public string EgyebJelenlevo { get; set; }
        public string JegyzoKonyvText { get; set; }
        public string JegyzoKonyvZarasOra { get; set; }
        public string JegyzoKonyvZarasPerc { get; set; }
        public string DonteshozoNev { get; set; }
        public string DonteshozoRang { get; set; }
        public string MeghallgatasOraja { get; set; }
        public string MeghallgatasPerce { get; set; }
    }
}
