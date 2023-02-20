using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum RezsimTipusok
        {
            [Display(Name = "Nincs megadva")]
            NincsMegadva = 0,
            [Display(Name = "Enyhébb")]
            Enyhebb = 1002618,
            [Display(Name = "Általános")]
            Altalanos = 1002619,
            [Display(Name = "Szigorúbb")]
            Szigorubb = 1002620,
            [Display(Name = "Nincs")]
            Nincs = 1002621
        }
    }
}
