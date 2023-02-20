using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        /// <summary>
        /// Napjelleg kódszótár csoport: 1012
        /// </summary>
        public enum KorlatozasTipus
        {
            FokozottOrzesEletFSzvVelFenyegetett = 3133,
            CsomagUgyesziellenorzessel = 3134,
            LevelUgyesziEllenorzessel = 3135,
            LatogatasUgyesziEllenorzessel = 3136,
            CsomagTiltas = 3137,
            LevelTiltas = 3138,
            LatogatasTiltas = 3139,
            FokozottOrzes = 3140,
            CsomagEllenorzessel = 3141,
            LevelEllenorzessel = 3142,
            HozzatartKapcsInt = 3143,
            HozzatartozóvalKapcsTiltas = 3144,
            HozzatartKivulKapcsEnged = 3145,
            HozzatartKivulKapcsTiltas = 3146,
            LatogatasOPI = 3147,
            LatogatasIntPkI = 3148,
            LatogatasBvOvI = 3149,
            LatogatasNevI = 3150,
            CsomagOPI = 3151,
            CsomagIntPkI = 3152,
            CsomagBvOvI = 3153,
            CsomagNevI = 3154,
            LevelOpI = 3155,
            LevelIntPkI = 3156,
            LevelBvOvI = 3157,
            LevelNevI = 3158,
            TelefonalasBvOvI = 3159,
            TelefonalasIntPkI = 3160,
            TelefonalasTiltas = 3161,
            TelefonalasNevI = 3162,
            TelefonalasOpI = 3163,
            TelefonalasUgyesziEllenorzessel = 3164,
            LatogatasEllenorzessel = 3165,
            FokozottOrzesSzokesetolTamadasatólKellTart = 3166,
            FokozottOrzesOnEsKozveszelyes = 3167,
            TelefonalasEllenorzessel = 3168,
            Tavoltartas = 3206,
            LatogatasZartFulkebenTelefononKeresztul = 70059
        }

        public static class KorlatozasCsoportositasok
        {
            public static List<int> TelefonalasTiltott = new List<int>()
            {
                (int)KorlatozasTipus.TelefonalasBvOvI,
                (int)KorlatozasTipus.TelefonalasEllenorzessel,
                (int)KorlatozasTipus.TelefonalasIntPkI,
                (int)KorlatozasTipus.TelefonalasNevI,
                (int)KorlatozasTipus.TelefonalasOpI,
                (int)KorlatozasTipus.TelefonalasTiltas,
                (int)KorlatozasTipus.TelefonalasUgyesziEllenorzessel,
            };
        }
    }
}
