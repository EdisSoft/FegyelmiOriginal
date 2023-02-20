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
        public enum ZarolasTipus
        {
            SzabadulasraTartalekolt = 10357,
            KietkezesreZarolt = 10358,
            TelefonalasraZarolt = 10359,
            FogvatartottKeresere =10360,
            Rendszerszintu = 10361,
            MobilOvadek= 10484,
            LetiltasZarolas = 10364
        }
    }
}
