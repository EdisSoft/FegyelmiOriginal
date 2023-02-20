using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.Email
{
    public class VegszallitottFogvatartottEmailItem
    {
        public List<ErtesitendoSzemely> ErtesitendoSzemelyek { get; set; }

        public List<FegyelmiUgyItem> FegyelmiList { get; set; }

        public VegszallitottFogvatartottEmailItem()
        {
            ErtesitendoSzemelyek = new List<ErtesitendoSzemely>();
            FegyelmiList = new List<FegyelmiUgyItem>();
        }
        public VegszallitottFogvatartottEmailData GetEmailData()
        {
            VegszallitottFogvatartottEmailData data = new VegszallitottFogvatartottEmailData()
            {
                FegyelmiList = FegyelmiList,
                CimzettekTitle = (ErtesitendoSzemelyek.Count == 1 && !ErtesitendoSzemelyek[0].Email.Contains(";")) ? (string.IsNullOrEmpty(ErtesitendoSzemelyek[0].SzemelyNev) ? "Címzett" : (ErtesitendoSzemelyek[0].SzemelyNev + " " + ErtesitendoSzemelyek[0].SzemelyBeosztas)) : "Címzettek",
                Datum = DateTime.Now.ToShortDateString(),
                EmailAddresses = string.Join(";", ErtesitendoSzemelyek.Select(x => x.Email))
            };
            return data;
        }
    }
}
