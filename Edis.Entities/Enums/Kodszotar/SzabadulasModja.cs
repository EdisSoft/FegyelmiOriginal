using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum SzabadulasModja
        {
           Meghalt = 2985,
           MegorzesreSzallitva = 4789,
           RendesMegorzesrolVisszaszallitva = 2962,
           VeglegAtszallitva = 2961
        }

        public static List<int> SzabadulasModjaNemKell
        {
            get
            {
                return new List<int>() {
                    (int)SzabadulasModja.Meghalt,
                    (int)SzabadulasModja.MegorzesreSzallitva,
                    (int)SzabadulasModja.RendesMegorzesrolVisszaszallitva,
                    (int)SzabadulasModja.VeglegAtszallitva,
                };
            }
        }
    }
}