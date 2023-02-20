using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY
{
    public class SzabadZarkaModel
    {
        public string Intezet { get; set; }
        public int IntezetId { get; set; }
        public string Objektum { get; set; }
        public int ObjektumId { get; set; }
        public string Reszleg { get; set; }
        public int ReszlegId { get; set; }
        public string Zarka { get; set; }
        public int AgyakSzama { get; set; }
        public long Agy { get; set; }
        public int ZarkaId { get; set; }
    }
}
