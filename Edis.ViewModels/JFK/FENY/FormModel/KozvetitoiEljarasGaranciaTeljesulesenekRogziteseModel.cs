using Edis.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class KozvetitoiEljarasGaranciaTeljesulesenekRogziteseModel
    {
        public List<int> fegyelmiUgyIds { get; set; }
        public List<int> naplobejegyzesIds { get; set; }
        public bool IsTeljesult { get; set; }
        //public List<IdLabelWithChildren> ZarkabaHelyezesOptions { get; set; }
        //public DateTime BehelyezesTenylegesIdeje { get; set; }
        //public bool Vegleges { get; set; }
    }
}
