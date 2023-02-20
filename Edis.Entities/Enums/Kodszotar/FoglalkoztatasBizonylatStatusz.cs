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
            11630  Folyamatban lévő
            11631	Sztornózott
            11632	Könyvelt
         */
        public enum FoglalkoztatasBizonylatStatusz
        {
            FolyamatbanLevo = 11630,
            Sztornozott = 11631,
            Konyvelt = 11632
        }
    }
}
