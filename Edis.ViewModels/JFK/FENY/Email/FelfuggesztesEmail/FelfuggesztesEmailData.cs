using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.Email
{
    public class FelfuggesztesEmailData
    {
        public string CimzettekTitle { get; set; }
        public string EmailAddresses { get; set; }

        public string Datum { get; set; }
        public string AlkalmazasUrl { get; set; }


        public List<FegyelmiUgyItem> FelfuggesztettFegyelmiList { get; set; }
        public List<FegyelmiUgyItem> AktivraAllitottFegyelmiList { get; set; }
    }
}
