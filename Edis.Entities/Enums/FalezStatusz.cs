using System.ComponentModel;

namespace Edis.Entities.Enums
{
    public enum FalezStatusz
    {
        [Description("Elszámolt")]
        Elszamolt = 1,
        [Description("Elszámolásra vár")]
        ElszamolasraVar = 2,
        [Description("Faléz javítás alatt")]
        FalezJavitasAlatt = 3,
        [Description("Faléz lezárt")]
        FalezLezart = 4,
        [Description("Kitöltés folyamatban")]
        KitoltesFolyamatban = 5,
        [Description("Elszámolás jóváhagyásra vár")]
        ElszamolasJovahagyasraVar = 6,
    }
}
