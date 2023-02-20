using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum NemBehallgathatoKapcsolattartok
        {
            MeghatalmazottUgyved = 3112,
            KirendeltUgyved = 3113,
            Konzul = 3179,
            KonzulatusiMunkatars = 3180,
            IdegenrendeszetiOrizetes = 0
        }
    }
}
