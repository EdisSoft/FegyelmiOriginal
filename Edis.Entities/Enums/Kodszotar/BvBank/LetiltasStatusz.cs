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
        public enum LetiltasStatusz
        {
            [Display(Name = "Aktív")]
            Aktiv = 10287,
            [Display(Name = "Kifutott")]
            Kifutott = 10288,
            [Display(Name = "Visszavont")]
            Visszavont = 10289,
            [Display(Name = "Felfüggesztett")]
            Felfuggesztett = 10426,
            [Display(Name = "Szabadítás miatt megszüntetve")]
            SzabaditasMiattMegszuntetve = 11651
        }
    }
}
