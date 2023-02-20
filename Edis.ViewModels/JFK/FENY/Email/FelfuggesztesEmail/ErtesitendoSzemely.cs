using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.Email
{
    public class ErtesitendoSzemely
    {
        public string FegyelmiKey { get; set; }
        //public int SzemelyId { get; set; }
        public string SzemelySid { get; set; }

        public string SzemelyNev { get; set; }
        public string SzemelyBeosztas { get; set; }
        public string Email { get; set; }

        public List<int> FegyelmiIdList { get; set; }

        public ErtesitendoSzemely()
        {
            FegyelmiIdList = new List<int>();
        }
    }
}
