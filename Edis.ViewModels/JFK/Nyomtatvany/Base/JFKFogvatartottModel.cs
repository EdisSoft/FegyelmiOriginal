using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.Nyomtatvany
{
    public class JFKFogvatartottModel
    {
        public string FogvatartottNeve { get; set; }
        public string AnyjaNeve { get; set; }
        public DateTime? SzuletesiIdo { get; set; }
        public string SzuletesHelye { get; set; }
        public string NyilvantartasiSzama { get; set; }
    }
}
