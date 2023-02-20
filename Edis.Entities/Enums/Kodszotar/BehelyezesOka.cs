using Edis.Entities.Attributes;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        /// <summary>
        /// Behelyezés oka típus kódszótár csoport: 198
        /// </summary>
        /// 
        public const string GyogyitoTerapia = "Gyógyító terápiás";
        public const string DrogPrevencios = "Drog Prevenciós";
        public const string PszichoSzocialis = "Pszicho-szociális";
        public const string HSRreszlegbe = "HSR részleg";
        public const string AlacsonyBiztKock = "Alacsony biztonsági kockázatú";
        public const string TarsKotProgram = "Társadalmi kötődés program";
        public const string APACReszlegbe = "APAC";
        public const string SexOffenderReszlegbe = "Sex Offender";
        public const string ElsoBuntenyesReszlegbe = "Első bűntényes";
        public enum BehelyezesOka
        {
            [GroupName(GyogyitoTerapia)]
            AlkoholKGy = 2727,
            [GroupName(GyogyitoTerapia)]
            SzemZavar = 2728,
            [GroupName(GyogyitoTerapia)]
            OrvosiJav = 2729,
            [GroupName(GyogyitoTerapia)]
            Pszichiat = 3241,
            [GroupName(GyogyitoTerapia)]
            GyogyElme = 4527,
            [GroupName(GyogyitoTerapia)]
            KorlBeszamitasKep = 4528,
            [GroupName(GyogyitoTerapia)]
            IntezetiParaHatas = 4529,

            [GroupName(DrogPrevencios)]
            DrogProbl = 2730,
            [GroupName(DrogPrevencios)]
            EltFuggos = 2731,
            [GroupName(DrogPrevencios)]
            EltHaszn = 2732,
            [GroupName(DrogPrevencios)]
            EltFelvil = 2733,

            [GroupName(PszichoSzocialis)]
            SzemKorulm = 4815,
            [GroupName(PszichoSzocialis)]
            BuncsJellege = 4816,
            [GroupName(PszichoSzocialis)]
            EgyebVeszely = 4817,

            [GroupName(HSRreszlegbe)]
            Hsr = 4848,

            [GroupName(AlacsonyBiztKock)]
            AlacsBiztReszleg = 4852,

            [GroupName(TarsKotProgram)]
            TarsKotProg = 4860,

            [GroupName(APACReszlegbe)]
            APAC = 1105106,

            [GroupName(SexOffenderReszlegbe)]
            SexOffender = 1105107,

            [GroupName(ElsoBuntenyesReszlegbe)]
            ElsoBuntenyes = 1106908
        }
    }
}
