using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        // csoport id: 1022
        public enum TranzakcioStatusz
        {
            /******** Folyamatban lévő tranzakció státuszok  *********/

            /// <summary>
            /// Folyamatban lévő tranzakció
            /// </summary>
            Elojegyzett = 10304,

            /// <summary>
            /// Folyamatban lévő tranzakció
            /// </summary>
            Kifizetheto = 10305,

            /// <summary>
            /// Folyamatban lévő tranzakció
            /// </summary>
            KifizetesAlatt = 10306,

            /// <summary>
            /// Folyamatban lévő tranzakció
            /// </summary>
            KonyvelesAlatt = 10309,


            /******** Lezárt sikeres tranzakció státuszok  *********/

            /// <summary>
            /// Lezárt sikeres tranzakció
            /// </summary>
            Konyvelt = 10307,

            /******** Lezárt sikertelen tranzakció státuszok  *********/

            /// <summary>
            /// Lezárt sikertelen tranzakció
            /// </summary>
            Sztornozott = 10308,

            /// <summary>
            /// Lezárt sikertelen tranzakció
            /// </summary>
            VisszavontElojegyzes = 10310,

            /// <summary>
            /// Lezárt sikertelen tranzakció
            /// </summary>
            VisszavontBizonylattetel = 10311,

            AtvezetesreElokeszitve=10447,

            NemTeljesult=10448
        }
    }

    public static class TranzakcioCsoportositasok
    {
        public static List<int> FolyamatbanLevo = new List<int>()
        {
            (int)KodszotarEnums.TranzakcioStatusz.Elojegyzett,
            (int)KodszotarEnums.TranzakcioStatusz.Kifizetheto,
            (int)KodszotarEnums.TranzakcioStatusz.KifizetesAlatt,
            (int)KodszotarEnums.TranzakcioStatusz.KonyvelesAlatt,
            (int)KodszotarEnums.TranzakcioStatusz.AtvezetesreElokeszitve,
        };
        public static List<int> Konyvelheto = new List<int>()
        {
            (int)KodszotarEnums.TranzakcioStatusz.Kifizetheto,
            (int)KodszotarEnums.TranzakcioStatusz.KifizetesAlatt
        };
        public static List<int> ElojegyzesnelKivalaszthato = new List<int>()
        {
            (int)KodszotarEnums.TranzakcioStatusz.Elojegyzett,
            (int)KodszotarEnums.TranzakcioStatusz.Kifizetheto
        };
    }
}
