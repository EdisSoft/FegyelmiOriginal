using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum SzabadulasraTartalekoltOsszegMozgasTipus
        {
            FeloldasSzabaditasMiatt = 11750,
            AthelyezesSzabaditasVisszavonasMiatt = 11751,
            FeloldasTevesKonyvelesMiatt = 11700,
            AthelyezesTevesKonyvelesMiatt = 11701,
            JovairasHokoziHaviZarasMiatt = 11702,
            FeloldasGyorsitottKarteritesLevonasMiatt = 11705,
        }

        public enum IMKartalanitasiOsszegMozgasTipus
        {
            FeloldasSzabaditasMiatt = 11770,
            AthelyezesSzabaditasVisszavonasMiatt = 11771,
            JovairasIMKartalanitasMiatt = 11772,
            FeloldasIMKartalanitasParEng = 11773,
            AthelyesTevesKonyvelesMiattIMKartalanitas = 11774,
            LetiltasELojegyzes = 11775,
            LetiltasElojegyzesVisszavonas = 11776
        }
    }
}
