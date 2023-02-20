using Edis.Entities.Attributes;
using System.ComponentModel;

namespace Edis.Entities.Enums
{
    public enum PkttrPanaszListaTipus
    {
        [Title("Folyamatban lévő panaszok")]
        [Description("Az összes intézetben elindított nem lezárt panaszt tartalmazza.")]
        FolyamatbanLevo,
        [Title("Lejárt határidejű panaszok")]
        [Description("Az itt található panaszok határideje lejárt.")]
        LejartHatarideju,
        [Title("Sürgős panaszok")]
        [Description("Az itt található ügyek határideje hamarosan lejár.")]
        Surgos,
        [Title("Átszállítási kérelem alatt álló panaszok")]
        [Description("A parancsnokság felé átszállítási kérelem miatt továbbított paszanok.")]
        AtszallitasiKerelemAlatt,
        [Title("BFB javaslatra váró panaszok")]
        [Description("BFB-nek javaslatott kell tenni a panasszal kapcsolatban a parancsnok felé.")]
        BfbJavaslatraVar,
        [Title("Parancsnoki döntésre váró panaszok")]
        [Description("BFB javaslat elkészült azonban a parancsnok még nem véleményezte.")]
        ParancsnokiDontesreVar,
        [Title("Fogvatartott jóváhagyására váró panaszok")]
        [Description("Átszállítási határozat és parancsnoki hatarozat esetén.")]
        FogvatartottJovahagyasaraVar,
        [Title("BV bírónak küldésre vár")]
        [Description("Azon panaszok, melyeket a BV bíróhoz el kell küldeni.")]
        BvBironakKuldes,
        [Title("BV bíró döntés hozatal")]
        [Description("Azon panaszok, melyek a BV bíró döntésére várnak.")]
        BvBironakElkuldve,
        [Title("Idei évben lezárt panaszok")]
        [Description("Az idei évben került lezárásra a panasz.")]
        IdeiEvbenLezart,

        [Title("Sürgős ügyek")]
        [Description("Az itt található átszállítási kérelmek határideje # napon belül lejár.")]
        SurgosIndex,
        [Title("Összes ügy")]
        [Description("Itt található az összes folyamatban lévő átszállítási kérelem.")]
        OsszesIndex,

        [Title("Elfogadott átszállítási határozatok")]
        [Description("Az elmúlt egy hétben hozott átszállítási határozatok.")]
        AtszallitasHatarozat,
        [Title("Mellőzött átszállítási határozatok")]
        [Description("Az elmúlt egy hétben mellőzött átszállítási határozatok.")]
        MellozottHatarozat
    }
}
