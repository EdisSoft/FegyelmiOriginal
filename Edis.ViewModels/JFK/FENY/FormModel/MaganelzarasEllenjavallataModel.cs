using Edis.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class MaganelzarasEllenjavallataModel
    {
        public List<int> fegyelmiUgyIds { get; set; }
        public List<int> naplobejegyzesIds { get; set; }
        [AllowHtml]
        public string Leiras { get; set; }
        public DateTime Hatarido { get; set; }
        //public List<IdLabelWithChildren> ZarkabaHelyezesOptions { get; set; }
        //public DateTime BehelyezesTenylegesIdeje { get; set; }
        //public bool Vegleges { get; set; }
    }
}
