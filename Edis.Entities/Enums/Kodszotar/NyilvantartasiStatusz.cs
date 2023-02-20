using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        // NEM TELJES!!!
        public enum NyilvantartasiStatusz
        {
            ReintegraciosOrizetes = 4823,
            FeltetelesSzabadsagraBocsat = 3186,
            BuntetesFelbeszakitasa = 2793,
            Jelenlevo = 2775,
            Eloallitason = 2778,
            NyomozasraKiadva = 2779,
            KulkorhazbaKihelyezve = 2781,
            RovidTartEltavon = 2784,
            Eltavozason = 2785,
            Kimaradason = 2786,
            RendesMegorzesrolVissza = 2795,
            LetartoztatottEliteltnekAtfogadva = 2828,
            LetartoztatasLejart = 2829,
            ElzarasosnakElfogadva = 2836,
            MegorzesreSzallitva = 4788,
            TargyalasonSzabadlabraHelyezve = 2809
        }

        public static List<int> NemLevonhatoTartasDijNyilvantartasiStatuszok
        {
            get
            {
                return new List<int>() {
                    (int)NyilvantartasiStatusz.ReintegraciosOrizetes,
                    (int)NyilvantartasiStatusz.FeltetelesSzabadsagraBocsat,
                    (int)NyilvantartasiStatusz.BuntetesFelbeszakitasa
                };
            }
        }

        public static List<int> EloallitasonMegjelenitendoStatuszok
        {
            get
            {
                return new List<int>(){
                    (int)NyilvantartasiStatusz.Jelenlevo,
                    (int)NyilvantartasiStatusz.Eloallitason,
                    (int)NyilvantartasiStatusz.NyomozasraKiadva,
                    (int)NyilvantartasiStatusz.KulkorhazbaKihelyezve,
                    (int)NyilvantartasiStatusz.RovidTartEltavon,
                    (int)NyilvantartasiStatusz.Eltavozason,
                    (int)NyilvantartasiStatusz.Kimaradason,
                    (int)NyilvantartasiStatusz.RendesMegorzesrolVissza,
                    (int)NyilvantartasiStatusz.LetartoztatottEliteltnekAtfogadva,
                    (int)NyilvantartasiStatusz.LetartoztatasLejart,
                    (int)NyilvantartasiStatusz.ElzarasosnakElfogadva,
                    (int)NyilvantartasiStatusz.MegorzesreSzallitva
                };
            }
        }
    }
}
