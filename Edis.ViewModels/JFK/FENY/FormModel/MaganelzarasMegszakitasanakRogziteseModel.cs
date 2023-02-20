using Edis.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class MaganelzarasMegszakitasanakRogziteseModel
    {
        public List<int> fegyelmiUgyIds { get; set; }
        public List<int> naplobejegyzesIds { get; set; }
        public string VisszahelyezesZarkaba { get; set; }
        public List<IdLabelWithChildren> VisszahelyezesZarkabaOptions { get; set; }
        public DateTime MaganzarkabolKihelyezesTenylegesIdeje { get; set; }
        public bool NocheckVegrehajtasiFok { get; set; }

        [AllowHtml]
        public string Indoklas { get; set; }
    }
}
