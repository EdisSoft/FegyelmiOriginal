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
        public enum LetiltasTipusa
        {
            [Display(Name = "Gyermektartásdíj")]
            GyermekTartasdij = 10284,
            [Display(Name = "Végrehajtási költség")]
            VegrehajtasiKoltseg = 10285,
            [Display(Name = "Jogszabályon alapuló egyéb tartásdíj")]
            JogszabalyonAlapuloEgyebTartasdij = 10286,
            [Display(Name = "Állam javára fizetendő összeg")]
            AllamJavaraFizetendoOsszeg = 10805,
            [Display(Name = "Munkavállalói munkabér és járandósság")]
            MunkavallaloiMunkaberEsJarandosag = 10806,
            [Display(Name = "Adó, társadalombiztosítási követelés és más köztartozás")]
            AdoTarsadalombiztositasiKovetelesEsMasKoztartozas = 10807,
            [Display(Name = "Egyéb követelés")]
            EgyebKoveteles = 10808,
            [Display(Name = "Rendbírság")]
            Rendbirsag = 10809,
            [Display(Name = "Természetes személy sértett javára megállapított polgári jogi igény")]
            TermeszetesSzemelySertettJavaraMegallapitottPolgariJogiIgeny = 10810
        }
    }
}
