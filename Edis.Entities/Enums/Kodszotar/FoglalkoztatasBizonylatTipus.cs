using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {

        /*
        11620	Munkadíj
        11621	Ösztöndíj
        11622	Jutalom
        11623   Terápiás munkadij
         */
        public enum FoglalkoztatasBizonylatTipus
        {
            Munkadij = 11620,
            Osztondij = 11621,
            Jutalom = 11622,
            TerapiasMunkadij = 11623
        }
    }
}
