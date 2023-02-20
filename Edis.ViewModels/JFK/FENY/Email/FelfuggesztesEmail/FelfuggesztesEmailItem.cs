using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.Email
{
    public class FelfuggesztesEmailItem
    {
        //public int SzemelyId { get; set; }
        public List<ErtesitendoSzemely> ErtesitendoSzemelyek { get; set; }

        public List<FegyelmiUgyItem> FelfuggesztettFegyelmiList { get; set; }
        public List<FegyelmiUgyItem> AktivraAllitottFegyelmiList { get; set; }

        public FelfuggesztesEmailItem()
        {
            ErtesitendoSzemelyek = new List<ErtesitendoSzemely>();
            FelfuggesztettFegyelmiList = new List<FegyelmiUgyItem>();
            AktivraAllitottFegyelmiList = new List<FegyelmiUgyItem>();
        }

        public FelfuggesztesEmailData GetEmailData()
        {
            FelfuggesztesEmailData data = new FelfuggesztesEmailData()
            {
                AktivraAllitottFegyelmiList = AktivraAllitottFegyelmiList,
                FelfuggesztettFegyelmiList = FelfuggesztettFegyelmiList,
                CimzettekTitle = ErtesitendoSzemelyek.Count == 1 ? (ErtesitendoSzemelyek[0].SzemelyNev + " " + ErtesitendoSzemelyek[0].SzemelyBeosztas) : "Címzettek",
                Datum = DateTime.Now.ToShortDateString(),
                EmailAddresses = string.Join(";", ErtesitendoSzemelyek.Select(x => x.Email))
            };
            return data;
        }
    }
}
