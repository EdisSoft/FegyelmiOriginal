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
        public enum BvTartozasStatusz
        {
            [Display(Name = "Aktív")]
            Aktiv = 10294,
            [Display(Name = "Kifizetve")]
            Kifizetve = 10295,
            [Display(Name = "Megszüntetve")]
            Megszuntetve = 10296,
            [Display(Name = "Automatikus leírás")]
            AutomatikusLeiras = 10858,
            [Display(Name = "Leírva")]
            Leirva = 10855,
            [Display(Name = "Elévült")]
            Elevult = 10856,
            [Display(Name = "Kérelem alapján történt leírás")]
            KerelemLapAlapjanTortenoLeirás = 10859,
            [Display(Name = "NAV-nak átadva")]
            NavnakAtadva = 1124305
        }
    }
}
