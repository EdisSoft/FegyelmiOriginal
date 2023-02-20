using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Repositories
{
    public class TranzakcioAdatkontextus
    {
        public int SzemelyzetId { get; set; }
        public int RogzitoIntezetId { get; set; }
        public string RogzitoSzemelySid { get; set; }
        public string RogzitoSzemelyNev { get; set; }
        public string RogzitoSzemelyLoginNev { get; set; }
        public override string ToString()
        {
            return $"SzemelyzetId:{SzemelyzetId},RogzitoIntezetId:{RogzitoIntezetId},RogzitoSzemelySid:{RogzitoSzemelySid},RogzitoSzemelyNev:{RogzitoSzemelyNev},RogzitoSzemelyLoginNev:{RogzitoSzemelyLoginNev}";
        }
    }
}
