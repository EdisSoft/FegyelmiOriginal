using System;

namespace Edis.Entities.Enums
{
    [Flags]
    public enum CelHelyTipus : int
    {
        Novedek = 1,
        Fogyatek = 1 << 1,
        Belso = 1 << 2,
        Kulso = 1 << 3,
        Munkahely = 1 << 4,
        TartozkodasiHely = 1 << 5,
        VisszaZarkaba = 1 << 6,
        MegtagadtaMunkat = 1 << 7,
        MegtagadtaSzabadLevegot = 1 << 8,
        UjBefogadas = 1 << 9,
        MasIntezetbolAtfogadasVegleg = 1 << 10,
        MasIntezetbolAtfogadasMegorzes = 1 << 11,
        KulkorhazbolVisszafogadas = 1 << 12,
        IntezetelhagyasrolVisszafogadas = 1 << 13,
        MasikObjektumbolAtfogadas = 1 << 14,
        MasikReszlegrolAtfogadas = 1 << 15,
        PotnyomozasrolVisszafogadas = 1 << 16,
        JogszeruElhagyasVisszaerk = 1 << 17,
        Szabadulas = 1 << 18,
        VeglegAtszallitas = 1 << 19,
        MegorzesreAtszallitas = 1 << 20,
        KulkorhazbaKihelyezes = 1 << 21,
        JogszeruIntezetelhagyas = 1 << 22,
        MasikObjektumbaAthelyezes = 1 << 23,
        MasikReszlegreAthelyezes = 1 << 24,
        Elhalalozas = 1 << 25,
        PotnyomozasraKiadas = 1 << 26,
        ZarkanelkuliveTetel = 1 << 27
    }
}
