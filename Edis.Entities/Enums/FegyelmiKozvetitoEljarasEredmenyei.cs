using System.ComponentModel;

namespace Edis.Entities.Enums
{
    public enum FegyelmiKozvetitoEljarasGarancialisEredmenyei
    {
        [Description("Garancia teljesült, eredményes lezárás")]
        GaranciaTeljesultEredmenyesLezaras = 1,
        [Description("Garancia teljesült, de nem számít eredményesnek")]
        GaranciaTeljesultNemSzamitEredmenyesnek = 0,
    }

    public enum FegyelmiKozvetitoEljarasGaranciaNemTeljesultEredmenyei
    {
        [Description("Garancia nem teljesült, de eredményesnek számít")]
        GaranciaNemTeljesultEredmenyesnekSzamit = 1,
        [Description("Garancia nem teljesült, eredménytelen lezárás")]
        GaranciaNemTeljesultEredmenytelenLezaras = 0,
    }
}
