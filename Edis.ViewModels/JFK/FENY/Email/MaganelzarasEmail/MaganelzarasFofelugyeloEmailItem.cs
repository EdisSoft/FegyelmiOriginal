using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.Email
{
    public class MaganelzarasFofelugyeloEmailItem
    {
        public List<ErtesitendoSzemely> ErtesitendoSzemelyek { get; set; }
        public FegyelmiUgyItem Fegyelmi { get; set; }

        public MaganelzarasFofelugyeloEmailItem()
        {
            ErtesitendoSzemelyek = new List<ErtesitendoSzemely>();
        }

        public MaganelzarasFofelugyeloEmailData GetEmailData()
        {
            MaganelzarasFofelugyeloEmailData data = new MaganelzarasFofelugyeloEmailData()
            {
                Fegyelmi = Fegyelmi,
                CimzettekTitle = ErtesitendoSzemelyek.Count == 1 ? (ErtesitendoSzemelyek[0].SzemelyNev + " " + ErtesitendoSzemelyek[0].SzemelyBeosztas) : "Címzettek",
                Datum = DateTime.Now.ToShortDateString(),
                EmailAddresses = string.Join(";", ErtesitendoSzemelyek.Select(x => x.Email))
            };
            return data;
        }
    }
}
