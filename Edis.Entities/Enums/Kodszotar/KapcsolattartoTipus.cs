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
        public enum KapcsolattartoTipus
        {
            Apa = 3080,
            Anya = 3081,
            Hazastars = 3082,
            GyermekKiskoru = 3083,
            Rokon = 3084,
            HozzatartozonKivuliSzemely = 3085,
            EgyenesagbeliRokonHazastarsa = 3086,
            OrokbefogadoAnya = 3087,
            OrokbefogadoApa = 3088,
            NeveloAnya = 3089,
            NeveloApa = 3090,
            OrokbefogadottGyermek = 3091,
            NeveltGyermek = 3092,
            Testver = 3093,
            Elettars = 3094,
            Jegyes = 3095,
            HazastarsEgyenesagbeliRokona = 3096,
            HazastarsTestvere = 3097,
            TestverHazastarsa = 3098,
            Unokatestver = 3099,
            Unoka = 3100,
            Nagyszulo = 3101,
            BaratVagyBaratno = 3102,
            Szomszed = 3103,
            Munkatars = 3104,
            VoltElettars = 3105,
            VoltHazastars = 3106,
            VoltHazastarsRokona = 3107,
            Gyam = 3108,
            Sogor = 3109,
            Sogorno = 3110,
            GyermekNagykoru = 3111,
            MeghatalmazottUgyved = 3112,
            KirendeltUgyved = 3113,
            Partfogo = 3114,
            Feltestver = 3115,
            JogiKepviselo = 3116,
            Konzul = 3179,
            KonzulatusiMunkatars = 3180,
            Gondnok = 4636,
            Kozjegyzo = 4647,
            Tolmacs = 4670,
            PartfogoUgyved = 4755,
            GyamhivataliMunkatars = 4760,
            HivatalosSzemely = 1100006,
            KulfoldiHivatalosSzemely = 1100007,
            Vedo = 1100008,
            Jogtanacsos = 1100009,
            Szakerto = 1100010,
            EgyhaziSzemely = 1100011,
            VallasiSzervezet = 1100012,
            EgyhaziJogiSzemely = 1100013,
            VallasiSzervezetMegbizottja = 1100014,
            Jogvedo = 1100015,
            KaritativCivilSzervezet = 1100016,
            MeghatalmazottKepviselo = 1104880
        }
        /*
         * 3112	Meghatalmazott ügyvéd
3113	Kirendelt ügyvéd
3114	Pártfogó
3116	Jogi képviselő
3179	Konzul
3180	Konzulátusi munkatárs
4636	Gondnok
4647	Közjegyző
4670	Tolmács
4755	Pártfogó ügyvéd
4760	Gyámhivatali munkatárs
1100006	Hivatalos személy
1100007	Külföldi hivatalos személy
1100009	Jogtanácsos
1100010	Szakértő
1100011	Egyházi személy
1100012	Vallási szervezet
1100013	Egyházi jogi személy
1100014	Vallási szervezet megbízottja
1100015	Jogvédő
1100016	Karitatív civil szervezet
1104880	Meghatalmazott képviselő
         */

        public static List<int> HivatalosKapcsolattarttok = new List<int>()
        {
            (int)KapcsolattartoTipus.MeghatalmazottUgyved,
            (int)KapcsolattartoTipus.KirendeltUgyved,
            (int)KapcsolattartoTipus.Partfogo,
            (int)KapcsolattartoTipus.JogiKepviselo,
            (int)KapcsolattartoTipus.Konzul,
            (int)KapcsolattartoTipus.KonzulatusiMunkatars,
            (int)KapcsolattartoTipus.Gondnok,
            (int)KapcsolattartoTipus.Kozjegyzo,
            (int)KapcsolattartoTipus.Tolmacs,
            (int)KapcsolattartoTipus.PartfogoUgyved,
            (int)KapcsolattartoTipus.GyamhivataliMunkatars,
            (int)KapcsolattartoTipus.HivatalosSzemely,
            (int)KapcsolattartoTipus.KulfoldiHivatalosSzemely,
            (int)KapcsolattartoTipus.Jogtanacsos,
            (int)KapcsolattartoTipus.Szakerto,
            (int)KapcsolattartoTipus.EgyhaziSzemely,
            (int)KapcsolattartoTipus.EgyhaziJogiSzemely,
            (int)KapcsolattartoTipus.VallasiSzervezetMegbizottja,
            (int)KapcsolattartoTipus.Jogvedo,
            (int)KapcsolattartoTipus.KaritativCivilSzervezet,
            (int)KapcsolattartoTipus.MeghatalmazottKepviselo
        };

        public static List<int> HozzatartozoKapcsolattarttok = new List<int>()
        {
            (int)KapcsolattartoTipus.Apa,
            (int)KapcsolattartoTipus.Anya,
            (int)KapcsolattartoTipus.Hazastars,
            (int)KapcsolattartoTipus.GyermekKiskoru,
            (int)KapcsolattartoTipus.GyermekNagykoru,
            (int)KapcsolattartoTipus.EgyenesagbeliRokonHazastarsa,
            (int)KapcsolattartoTipus.OrokbefogadoAnya,
            (int)KapcsolattartoTipus.OrokbefogadoApa,
            (int)KapcsolattartoTipus.NeveloAnya,
            (int)KapcsolattartoTipus.NeveloApa,
            (int)KapcsolattartoTipus.OrokbefogadottGyermek,
            (int)KapcsolattartoTipus.NeveltGyermek,
            (int)KapcsolattartoTipus.Testver,
            (int)KapcsolattartoTipus.Elettars,
            (int)KapcsolattartoTipus.Jegyes,
            (int)KapcsolattartoTipus.HazastarsEgyenesagbeliRokona,
            (int)KapcsolattartoTipus.HazastarsTestvere,
            (int)KapcsolattartoTipus.TestverHazastarsa,
            (int)KapcsolattartoTipus.Unoka,
            (int)KapcsolattartoTipus.Nagyszulo,
            (int)KapcsolattartoTipus.Gyam,
            (int)KapcsolattartoTipus.Sogor,
            (int)KapcsolattartoTipus.Sogorno,
            (int)KapcsolattartoTipus.Feltestver
        };
    }
}
