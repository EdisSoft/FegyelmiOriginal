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
            11640	Ellenőrzést nem igényel
            11641	Ellenőrzésre vár
            11642	Ellenőrizve
        */
        public enum FoglalkoztatasBizonylatTetelEllenorzesStatusz
        {
            EllenorzestNemIgenyel = 11640,
            EllenorzesreVar = 11641,
            Ellenorizve = 11642
        }
    }
}
