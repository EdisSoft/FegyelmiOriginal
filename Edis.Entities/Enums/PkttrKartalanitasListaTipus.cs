using Edis.Entities.Attributes;
using System.ComponentModel;

namespace Edis.Entities.Enums
{
    public enum PkttrKartalanitasListaTipus
    {
        [Title("Folyamatban lévő kártalanítások")]
        [Description("Az összes intézetben elindított nem lezárt kártalanítást tartalmazza.")]
        FolyamatbanLevo,
        [Title("Lejárt határidejű kártalanítások")]
        [Description("Az itt található kártalanítások határideje lejárt.")]
        LejartHatarideju,
        [Title("Sürgős kártalanítások")]
        [Description("Az itt található ügyek határideje hamarosan lejár.")]
        Surgos,
        [Title("BV bírónak küldésre vár")]
        [Description("Azon kártalanítások, melyeket a BV bíróhoz el kell küldeni.")]
        BvBironakKuldes,
        [Title("Idei évben lezárt kártalanítások")]
        [Description("Az idei évben került lezárásra a kártalanítás.")]
        IdeiEvbenLezart,
        [Title("Korábbi évben lezárt kártalanítások")]
        [Description("A korábbi évben került lezárásra a kártalanítások.")]
        KorabbiEvbenLezart,

        [Title("Adatszolgáltatásra váró kártalanítási eljárások")]
        [Description("Más intézetek felé adatszolgáltatási kötelezettséggel járó kártalanítások.")]
        AdatszolgVaro,
        [Title("Adatszolgáltatásra váró lejárt kártalanítási eljárások")]
        [Description("Az itt található kártalanítások határideje lejárt.")]
        AdatszolgVaroLejart,
        [Title("Adatszolgáltatásra váró sürgős kártalanítási eljárások")]
        [Description("Az itt található ügyek határideje hamarosan lejár.")]
        AdatszolgVaroSurgos,

        [Title("Más intézetre váró kártalanítási eljárások")]
        [Description("Más intézetektől még nem jött adatszolgáltatás a kártalanításra.")]
        MasIntezetreVaro,
        [Title("Más intézetre váró lejárt kártalanítási eljárások")]
        [Description("Az itt található kártalanítások határideje lejárt.")]
        MasIntezetreVaroLejart,
        [Title("Más intézetre váró sürgős kártalanítási eljárások")]
        [Description("Az itt található ügyek határideje hamarosan lejár.")]
        MasIntezetreVaroSurgos
    }
}
