using System.ComponentModel.DataAnnotations;

namespace Edis.Entities.Enums.Kodszotar
{
    /*
        11610	Intézeti
        11611	KFT
    */

    public static partial class KodszotarEnums
    {
        public enum FoglalkoztatasBizonylatMunkaltatoTipus
        {
            [Display(Name = "Intézeti")]
            Intezeti = 11610,

            [Display(Name = "Kft")]
            Kft = 11611,
            
            KulsoMunkaltato = 11612
        }
    }
}
