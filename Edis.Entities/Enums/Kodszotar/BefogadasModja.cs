using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum BefogadasModja
        {
            ElozetesEliteltUjBefogadas = 3032,
            OrizetreUjBefogadas = 3033,
            IdeiglenesUjBefogadas = 3034,
            MegorzesreUjBefogadas = 3035,
            ElzarasosUjBefogadas = 3036,
            ElozetesEliteltOrizetbol = 3040,
            ElzarasosOrizetbol = 3041,
            MegorzesesOrizetbol = 3042
        }

        public static List<int> UjBefogadasStatuszok
        {
            get
            {
                return new List<int>() {
                    (int)BefogadasModja.ElozetesEliteltUjBefogadas,
                    (int)BefogadasModja.OrizetreUjBefogadas,
                    (int)BefogadasModja.IdeiglenesUjBefogadas,
                    (int)BefogadasModja.MegorzesreUjBefogadas,
                    (int)BefogadasModja.ElzarasosUjBefogadas,
                    (int)BefogadasModja.ElozetesEliteltOrizetbol,
                    (int)BefogadasModja.ElzarasosOrizetbol,
                    (int)BefogadasModja.MegorzesesOrizetbol,
                };
            }
        }

    }
}
