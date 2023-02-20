using System.ComponentModel.DataAnnotations;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum BizonylatPenztarJovairasJogcimLista
        {
            BeazonosithatalanTetel = 10324,
            BefogadaskoriBefizetesFogvatartottReszere = 10316,
            BefogadaskoriBefizetesCelzottPenzkent = 10470,
            BefogadaskoriBefizetesPenzbirsagra = 10471,
            CelzottPenz = 10477,
            EgyebJutalom = 10315,
            EltavrolVisszahozott = 10367,
            EltavrolVisszahozottCelzottPenzkent = 10473,
            KapcsolattartotolErkezettFogvatartottReszere = 10323,
            Munkadij = 10320,

            //[Display(Name = "Nem védett")]
            //NemVedett = 10322,

            TalaltPenzFogvatartottReszere = 10317,

            //[Display(Name = "Terápiás jutalom")]
            //TerapiasJutalom = 10474,

            TerapiasMunkadij = 10377,
            VedettPenzkuldemeny = 10386,
            Osztondij = 10319,

            //[Display(Name = "Fogvatartottnak megítélt kártérítés")]
            //FogvatartottnakMegiteltKarterites = 10475,

            ValutaValtasFogvatartottReszere = 10318,
            ValutavaltasCelzottPenzkent = 10539,
            NyugdijfolyositotolErkezett = 10322,
            KpFelvetelKartyarol = 10348,
            KpFelvetelBankbol = 10362,
            //KartyaraUtalasBankbol = 10349
        }
    }
}
