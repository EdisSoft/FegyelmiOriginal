using System.ComponentModel.DataAnnotations;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum BizonylatBankJovairasJogcimLista
        {
            BeazonosithatalanTetel = 10324,

            //[Display(Name = "Családi pótlék")]
            CsaladiPotlek = 10371,

            CelzottPenz = 10477,

            //[Display(Name = "Visszaérkezett pénzküldemény")]
            //VisszaerkezettPenzkuldemeny = 10472,

            EgyebJutalom = 10315, 
            KapcsolattartotolErkezettFogvatartottReszere = 10323,
            Munkadij = 10320,
            TomegesMunkadij = 1032000,

            //[Display(Name = "Terápiás jutalom")]
            //TerapiasJutalom = 10474,

            //[Display(Name = "Nem védett")]
            //NemVedett = 10322,

            //[Display(Name = "Terápiás jutalom")]
            //TerapiasJutalom = 10474,

            TerapiasMunkadij = 10377,
            VedettPenzkuldemeny = 10386,
            Osztondij = 10319,
            NyugdijfolyositotolErkezett = 10322,

            TelefonVisszaterites = 10429,

            Kerekites= 10344,
            IntezetekKozottiHitelezesRendezese = 10356,
            PenztariBefizetesBanknak = 10347,
            Kartyadij = 10365,
            IMKartalanitas = 10537
        }
    }
}
