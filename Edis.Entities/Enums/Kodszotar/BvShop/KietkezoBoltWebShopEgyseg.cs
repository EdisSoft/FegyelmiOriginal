using Edis.Entities.Attributes;
using System.Collections.Generic;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public const string Suly = "Súly";
        public const string Urtartalom = "Űrtartalom";
        public const string Egyeb = "Egyéb";

        public static List<string> GetMertekegysegTipusokList()
        {
            List<string> mertekegysegTipusok = new List<string>() { Suly, Urtartalom, Egyeb };
            return mertekegysegTipusok;
        }
        public enum KietkezoBoltWebshopEgyseg
        {
            [GroupName(Egyeb)]
            Darab = 1105260,
            [GroupName(Egyeb)]
            Csomag = 1105261,
            [GroupName(Egyeb)]
            Tekercs = 1105262,
            [GroupName(Suly)]
            Gramm = 1105263,
            [GroupName(Suly)]
            Kg = 1105267,
            [GroupName(Urtartalom)]
            Ml = 1105264,
            [GroupName(Urtartalom)]
            Liter = 1105268,
            [GroupName(Egyeb)]
            Szal = 1105265,
            [GroupName(Egyeb)]
            Doboz = 1105266

        }

        public enum KietkezoBoltWebshopEgysegEgyeb
        {
            Darab = KietkezoBoltWebshopEgyseg.Darab,
            Csomag = KietkezoBoltWebshopEgyseg.Csomag,
            Szal = KietkezoBoltWebshopEgyseg.Szal,
            Doboz = KietkezoBoltWebshopEgyseg.Doboz,
            Tekercs = KietkezoBoltWebshopEgyseg.Tekercs
        }

        public enum KietkezoBoltWebshopEgysegUrtartalom
        {
            Ml = KietkezoBoltWebshopEgyseg.Ml,
            //Dl = KietkezoBoltWebshopEgyseg.Dl,
            Liter = KietkezoBoltWebshopEgyseg.Liter
        }

        public enum KietkezoBoltWebshopEgysegSuly
        {
            //Dkg = KietkezoBoltWebshopEgyseg.Dkg,
            Kg = KietkezoBoltWebshopEgyseg.Kg,
            Gramm = KietkezoBoltWebshopEgyseg.Gramm
        }

    }
}
