using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.Email
{
    public class ElkulonitesEmailData
    {
        public string CimzettekTitle { get; set; }
        public string EmailAddresses { get; set; }

        public DateTime Datum { get; set; }
        public string AlkalmazasUrl { get; set; }
        
        public FegyelmiUgyItem Fegyelmi { get; set; }
    }
}
