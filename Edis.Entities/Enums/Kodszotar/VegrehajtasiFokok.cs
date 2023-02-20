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
        // NEM TELJES!!!
        public enum VegrehajtasiFokok
        {
            ElozetesKenyszergyogykezeles = 2759,
            Foghaz = 2750,
            Borton = 2751,
            Fegyhaz = 2752,
            PenzbuntHelySzabVesztesFoghaz = 2754,
            KozerdekuMunkaAtvaltoztatasaFoghaz = 2755,
            FiatalkoruFoghaz = 2757,
            FiatalkoruBorton = 2758,
            IdeiglenesKenyszerGyogykezelt = 2759,
            Kenyszergyogykezelt = 2760,
            ElozetesIFokuIteletig = 2761,
            ElozetesNemJogerosenElitelt = 2762,
            Elzaras = 2763,
            FiatalkoruElozetesElsofokuIteletig = 2765,
            FiatalkoruElozetesNemjogerosenElitelt = 2766,
            IdegenRendeszetiOrizetes = 2767,
            AtadasiAtveteliLetartoztatott = 2768,
            BvAtveteliLetartoztatott = 2769,
            BuntetujogiElzaras = 4780,
            FiatalkoruBuntetojogiElzaras = 1002615
        }


        public static List<int> FiatalkoruVegrehajtasiFokok
        {
            get
            {
                return new List<int>() {
                    (int)VegrehajtasiFokok.FiatalkoruBorton,
                    (int)VegrehajtasiFokok.FiatalkoruBuntetojogiElzaras,
                    (int)VegrehajtasiFokok.FiatalkoruElozetesElsofokuIteletig,
                    (int)VegrehajtasiFokok.FiatalkoruElozetesNemjogerosenElitelt,
                    (int)VegrehajtasiFokok.FiatalkoruFoghaz
                };
            }
        }

        public static List<int> NemLevonhatoTartasDijVegrehajtasiFokok
        {
            get
            {
                return new List<int>() {
                    (int)VegrehajtasiFokok.Kenyszergyogykezelt,
                    (int)VegrehajtasiFokok.IdeiglenesKenyszerGyogykezelt,
                    (int)VegrehajtasiFokok.IdegenRendeszetiOrizetes
                };
            }
        }

        public static List<int> Elazarasok
        {
            get
            {
                return new List<int>()
                {
                    VegrehajtasiFokok.BuntetujogiElzaras.CastToInt(),
                    VegrehajtasiFokok.Elzaras.CastToInt(),
                    VegrehajtasiFokok.FiatalkoruBuntetojogiElzaras.CastToInt()
                };
            }
        }

        public static List<int> Letartoztatottak
        {
            get
            {
                return new List<int>()
                {
                    VegrehajtasiFokok.ElozetesIFokuIteletig.CastToInt(),
                    VegrehajtasiFokok.ElozetesNemJogerosenElitelt.CastToInt(),
                    VegrehajtasiFokok.FiatalkoruElozetesElsofokuIteletig.CastToInt(),
                    VegrehajtasiFokok.FiatalkoruElozetesNemjogerosenElitelt.CastToInt(),
                    VegrehajtasiFokok.AtadasiAtveteliLetartoztatott.CastToInt(),
                    VegrehajtasiFokok.BvAtveteliLetartoztatott.CastToInt()
                };
            }
        }
    }
}
