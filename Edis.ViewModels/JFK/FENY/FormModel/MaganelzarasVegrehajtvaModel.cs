using Edis.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class MaganelzarasVegrehajtvaModel
    {
        public List<int> FegyelmiUgyIds { get; set; }
        public List<int> NaplobejegyzesIds { get; set; }
        public string ZarkabaHelyezes { get; set; }
        public List<IdLabelWithChildren> ZarkabaHelyezesOptions { get; set; }        
        public DateTime KihelyezesTenylegesIdeje { get; set; }
        public bool NocheckVegrehajtasiFok { get; set; }

    }
}
