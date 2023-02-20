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
        public enum KarteritesTipus
        {
            [Display(Name = "Normál kártértési eljárás")]
            NormalKarteritesiEljaras = 1102623,
            [Display(Name = "Mobil kártértési eljárás")]
            MobilKarteritesiEljaras = 1102624,
            [Display(Name = "Gyorsított kártértési eljárás")]
            GyorsitottKarteritesiEljaras = 1102625,
            [Display(Name = "Gyorsított kártértési eljárás mobiltelefonra")]
            GyorsitottKarteritesiEljarasMobil = 1104887
        }
    }
}
