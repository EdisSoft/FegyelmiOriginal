using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY
{
   public class OsszefoglaloJelentesNyomtatasModelTeljes
    {
        public List<OsszefoglaloJelentesNyomtatasModel> Naplok { get; set; }
        public string IntezetNev { get; set; }
        public string Iktatoszam { get; set; }
        public string Ugyszam { get; set; }
    }
}
