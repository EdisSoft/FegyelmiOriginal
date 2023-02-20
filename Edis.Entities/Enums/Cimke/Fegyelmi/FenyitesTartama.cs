using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Cimke
{
    public static class FenyitesTartama
    {
        #region Fenyítés típusa szerint max tartama
        public const int MaxKietkezesCsokkentese = 6;        
        public const int MaxMaganalTarthatoTargyakKorlatozasa = 6;
        public const int MaxProgaromokonValoResztvetelKorlatozasa = 3;
        public const int MaxProgaromokonValoResztvetelTiltasa = 3;
        public const int MaxTobbletSzolgaltatasokMegvonasa = 3;
        #endregion

        #region Bv végrehajtási fokozat szerinti kimaradás megvonás max tartama
        public const int MaxKimaradasMegvonas = 3;
        #endregion

        #region Bv végrehajtási fokozat szerinti magánelzárás max tartama
        public const int MaxMaganelzarasFiatalkoruFoghazDolgozik = 5;
        public const int MaxMaganelzarasFiatalkoruFoghazNemDolgozik = 5;

        public const int MaxMaganelzarasFiatalkoruBortonDolgozik = 10;
        public const int MaxMaganelzarasFiatalkoruBortonNemDolgozik = 10;
        
        public const int MaxMaganelzarasFoghazDolgozik = 5;
        public const int MaxMaganelzarasFoghazNemDolgozik = 10;

        public const int MaxMaganelzarasBortonDolgozik = 15;
        public const int MaxMaganelzarasBortonNemDolgozik = 20;

        public const int MaxMaganelzarasFegyhazDolgozik = 20;
        public const int MaxMaganelzarasFegyhazNemDolgozik = 25;

        public const int MaxMaganelzarasKozerdekuMunkaAtvaltoztatasaFoghazDolgozik = 5;
        public const int MaxMaganelzarasKozerdekuMunkaAtvaltoztatasaFoghazNemDolgozik = 5;

        public const int MaxMaganelzarasPenzbuntHelySzabVesztesFoghazDolgozik = 5;
        public const int MaxMaganelzarasPenzbuntHelySzabVesztesFoghazNemDolgozik = 5;

        public const int MaxMaganelzarasElzarasDolgozik = 5;
        public const int MaxMaganelzarasElzarasNemDolgozik = 5;

        public const int MaxMaganelzarasLetartoztatottDolgozik = 10;
        public const int MaxMaganelzarasLetartoztatottNemDolgozik = 15;

        public const int MaxMaganelzarasNincs = 0;
        #endregion        

    }
}
