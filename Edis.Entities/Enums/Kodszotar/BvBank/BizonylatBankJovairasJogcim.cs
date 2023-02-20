using System.ComponentModel.DataAnnotations;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum BizonylatBankJovairasJogcim
        {
            [Display(Name = "Beazonosíthatatlan tétel")]
            BeazonosithatalanTetel = 10324,

            [Display(Name = "Befogadáskori befizetés fogvatartott részére")]
            BefogadaskoriBefizetesFogvatartottReszere = 10316,

            [Display(Name = "Befogadáskori befizetés célzott pénzként")]
            BefogadaskoriBefizetesCelzottPenzkent = 10470,

            [Display(Name = "Befogadáskori befizetés pénzbírságra")]
            BefogadaskoriBefizetesPenzbirsagra = 10471,

            [Display(Name = "Családi pótlék")]
            CsaladiPotlek = 10371,

            [Display(Name = "Célzott Pénz")]
            CelzottPenz = 10477,

            [Display(Name = "Visszaérkezett pénzküldemény")]
            VisszaerkezettPenzkuldemeny = 10472,

            [Display(Name = "Egyéb (jutalom)")]
            EgyebJutalom = 10315,

            [Display(Name = "Eltávról visszahozott")]
            EltavrolVisszahozott = 10367,

            [Display(Name = "Eltávról visszahozott célzott pénzként")]
            EltavrolVisszahozottCelzottPenzkent = 10473,

            [Display(Name = "Előjegyzett bevétel")]
            ElojegyzettBevetel = 10373,

            [Display(Name = "Kapcsolattartótól érkezett fogvatartott részére")]
            KapcsolattartotolErkezettFogvatartottReszere = 10323,

            [Display(Name = "Munkadíj")]
            Munkadij = 10320,

            [Display(Name = "Tömeges munkadíj")]
            TomegesMunkadij = 1032000,

            [Display(Name = "Nem védett")]
            NemVedett = 10322,

            [Display(Name = "Pénzküldés más fogvatartottnak")]
            PenzkuldesMasFogvatartottnak = 10352,

            [Display(Name = "Pénzküldés más fogvatartottnak célzottan")]
            PenzkuldesMasFogvatartottnakCelzottan = 10476,

            [Display(Name = "Talált pénz fogvatartott részére")]
            TalaltPenzFogvatartottReszere = 10317,

            [Display(Name = "Terápiás jutalom")]
            TerapiasJutalom = 10474,

            [Display(Name = "Terápiás munkadíj")]
            TerapiasMunkadij = 10377,

            [Display(Name = "Téves könyvelés rendezése")]
            TevesKonyvelesRendezese = 10380,

            [Display(Name = "Védett pénzküldemény")]
            VedettPenzkuldemeny = 10386,

            [Display(Name = "IM kártalanítás")]
            IMKartalanitas = 10537,

            [Display(Name = "Ösztöndíj")]
            Osztondij = 10319,

            [Display(Name = "Fogvatartottnak megítélt kártérítés")]
            FogvatartottnakMegiteltKarterites = 10475,

            [Display(Name = "Valutaváltás fogvatartott részére")]
            ValutaValtasFogvatartottReszere = 10318,

            [Display(Name = "Valutaváltás célzott pénzként")]
            ValutavaltasCelzottPenzkent = 10539,

            [Display(Name = "Nyugdíjfolyósítótól érkezett")]
            NyugdijfolyositotolErkezett = 10322,

            TelefonVisszaterites = 10429,

            Kerekites= 10344,
            IntezetekKozottiHitelezesRendezese = 10356,
            PenztariBefizetesBanknak = 10347,
            KpFelvetelKartyarol = 10348,
            KpFelvetelBankbol = 10362
        }
    }
}
