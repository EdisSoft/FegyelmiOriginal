using Edis.Entities.Attributes;

namespace Edis.Entities.Enums.Cimke.Aktivitas
{
    public enum TevekenysegTipus
    {
        TartozkodasiHelyAllitas = 40386,
        ZarkaNelkuliveTetel = 40473,
        ZarkabaHelyezes = 40476,
        KivonultatasMunkahelyNelkul = 40494,
        FogvatartottakAtadasa = 41253,

        FelvetelNapiBeosztasba = 40572,
        EltavolitasNapiBeosztasbol = 40573,
        AthelyezesMasikBeosztasba = 40574,

        ValaszthatoTevekenysegLetrehozasa = 40699,
        ValaszthatoTevekenysegModositasa = 40700,
        ValaszthatoTevekenysegTorlese = 40722,

        TevekenysegLetrehozasa = 40701,
        TevekenysegModositasa = 40723,
        TevekenysegTipusModositasa = 40702,
        TevekenysegLeirasModositasa = 40703,
        TevekenysegTorlese = 40704,

        BITNaploLetrehozasa = 40705,
        BITNaploLezarasa = 40706,
        BITNaploTorlese = 41769,


        FegyverSzakanyagLetrehozasa = 40707,
        FegyverSzakanyagModositasa = 40708,
        FegyverSzakanyagTorlese = 40709,
        FegyverSzakanyagKitoltese = 40710,

        EgyebSzakanyagLetrehozasa = 40711,
        EgyebSzakanyagModositasa = 40712,
        EgyebSzakanyagTorlese = 40713,
        EgyebSzakanyagKitoltese = 40714,

        EligazitasKerdesModositasa = 40715,
        SzolgalatAlattiEsemenyekModositasa = 40716,
        JelentesreKotelezettModositasa = 40717,
        LetszamKitoltese = 40718,
        OsztalyLetszamaModositasa = 40719,
        SzolgalatKezdeteModositasa = 40720,
        SzolgalatVegeModositasa = 40721,

        LetszamadatKategoriaLetrehozasa = 40788,
        LetszamadatKategoriaTorlese = 40789,

        BITNaploMegtekintese = 40790,
        BITNaploJovahagyasa = 40791,
        BITNaploMegjegyzes = 40821,

        AllomanytagSorrendModositasa = 40822,

        Timeline = 40859,

        EloallitasiNaploLetoltese = 40995,
        EloallitasiNaploMegnyitasa = 40994,
        KockazatiFeljegyzesLetoltese = 40991,
        ModalMentese = 40993,
        ModalMegnyitasa = 40992,
        EloallitasokLekerdezese = 40990,
        AlapadatokMegnyitasa = 41061,
        AlapadatokMentese = 41064,
        VegrehajtasiInformaciokMegnyitasa = 41062,
        VegrehajtasiInformaciokMentese = 41065,
        ZaroErtekelesMegnyitasa = 41063,
        ZaroErtekelesMentese = 41066,

        // KFFE
        KFENaploLetrehozasa = 41018,
        KFENaploTorlese = 41770,
        KFFENaploLetrehozasa = 41019,
        KFFENaploTorlese = 41771,
        KFENaploMegjegyzes = 41025,
        KFFENaploMegjegyzes = 41010,
        KFENaploLezarasa = 41022,
        KFFENaploLezarasa = 41011,
        KFENaploLetszamRogzites = 41035,
        KFENaploLetszamJovahagyasa = 41036,
        KFENaploJovahagyasa = 41023,
        KFFENaploJovahagyasa = 41024,
        KFENaploMegtekintese = 41020,
        KFFENaploMegtekintese = 41021,
        KFENaploEszkozLetrehozasa = 41031,
        KFENaploEszkozModositasa = 41032,
        KFENaploEszkozTorlese = 41033,
        KFENaploEszkozKitoltese = 41034,
        KFENaploMeghagyasRogzitese = 41030,
        KFENaploMeghagyasModositasa = 41028,
        KFENaploMeghagyasTorlese = 41029,
        KFENaploAtvevoSzemelyModositasa = 41026,
        KFFENaploAtvevoSzemelyModositasa = 41027,
        KFFESzolgalatKezdeteModositasa = 41258,
        KFFEJelentesIntezkedesModositasa = 41259,
        KFFENapirendiPontVegrehajtva = 41260,
        KFFENapirendiPontNincsVegrehajtva = 41261,
        KFFENapirendiPontVegrehajtvaVisszavonas = 41262,
        KFFENapirendiPontNincsVegrehajtvaVisszavonas = 41263,
        KFFENapirendiPontLetrehozasa = 41264,
        KFFENapirendiPontModositasa = 41265,
        KFFENapirendiPontTorlese = 41266,
        KFFEAtvetelkorModositasa = 41267,
        KFFEAktivitasPDFLetoltese = 41268,
        KFFENaploPDFLetoltese = 41269,


        // MSZ Napló
        MSZNaploTorlese = 41772,
        MSZNaploFelugyeloHozzadasa = 41013,
        MSZNaploFogvatartottMegkulonboztetese = 41014,
        MSZNaploMegjegyzes = 41015,
        MSZNaploLezarasa = 41016,
        MSZNaploFogvatartottMegkulonboztetesenekVisszavonasa = 41017,
        MSZNaploJelentesModositasa = 41037,
        MSZNaploJelentesLetrehozasa = 41040,
        MSZNaploJelentesTorlese = 41041,
        MSZNaploLetrehozasa = 41043,
        MSZNaploSzolgalatKezdetenekModositasa = 41045,
        MSZNaploSzolgalatVegenekModositasa = 41046,
        MSZNaploFbfModositasa = 41047,
        MSZNaploAktivitasNyomtatvanyGeneralas = 41049,
        MSZNaploNyomtatvanyGeneralas = 41050,
        MSZNaploJovahagyasa = 41039,
        MSZNaploFelugyeloLezaras = 41055,
        MSZNaploTartHelyModositasa = 41056,
        MSZNaploFelugyeloSzolgalatModositasa = 41887,
        MSZNaploFogvatartottakEllenorzese = 42370,
        MSZNaploFogvatartottakTakaritasRogzitese = 42371,
        MSZNaploTartHelyTomegesModositasa = 44401,
        MSZNaploFelugyeletAtadasa = 44402,

        // E-napló
        EllenorzesiNaploRogzitese = 41270,
        BelepoSzemelyGepjarmuRogzitese = 41271,
        InditonaploRogzitese = 41272,
        JelentesreKotelezettEsemenyRogzitese = 41273,
        EllenorzesiNaploOlvasasa = 41276,
        BelepoSzemelyGepjarmuKileptetese = 41277,
        GepjarmuErkeztetese = 41278,
        BelepoSzemelyGepjarmuErvenytelenitese = 41279,
        KorletfelugyeloiEllenorzesiNaploOlvasasa = 41280,
        BelepesOkmanyolvasoval = 42314,
        OkmanyolvasoCsatlakoztatva = 42315,
        SzemelyTorleseKeresobol = 42983,
        IgazolvanyTorleseKeresobol = 42984,

        // Covid-19
        Covid19AdatszolgaltatasRogzitese = 41274,
        Covid19AdatszolgaltatasAllomanyiAdatokRogzitese = 41284,

        // Éjféli
        EjfeliAdatszolgaltatasRogzitese = 41275,

        FogvatartottMunkahelyiEllenorzese = 41005,
        FogvatartottMunkahelyiEllenorzesVeglegesitese = 41006,

        // OÉT Default Controller
        OETBejelentkezesIntezetbe = 41522,
        OETKezdolapMegtekintes = 41523,
        OETIntezetiParameterAllitas = 41524,
        OETNyomtatas = 41525,

        SzelvenyCikkLista = 41526,
        ErtekCikkLista = 41527,
        ErtekCikkRogzites = 41528,
        ErtekCikkSzerkesztes = 41529,
        OkmanyCikkLista = 41530,
        OkmanyCikkRogzites = 41531,
        OkmanyCikkSzerkesztes = 41532,
        TargyCikkLista = 41533,
        TargyCikkRogzites = 41534,
        TargyCikkSzerkesztes = 41535,

        OETFogvatartottLista = 41536,
        OETFogvatartottKarton = 41537,
        OETSzelvenyLista = 41538,

        RaktarKezelesKezdoOldal = 41539,
        RaktarLista = 41540,
        RaktarRogzites = 41541,
        RaktarSzerkesztes = 41542,
        RaktariHelyMegtekintes = 41543,
        RaktariHelyRogzites = 41544,
        RaktariHelySzerkesztes = 41545,
        RaktariHelyLista = 41546,

        SzallitasKezdoOldal = 41547,
        OkmanySzelvenySzallitasLista = 41548,
        OkmanyCikkSzallitasLista = 41549,
        OkmanySzelvenySzallitas = 41550,
        OkmanyCikkSzallitas = 41551,
        ErtekSzelvenySzallitasLista = 41552,
        ErtekCikkSzallitasLista = 41553,
        ErtekSzelvenySzallitas = 41554,
        ErtekCikkSzallitas = 41555,
        TargySzelvenySzallitasLista = 41556,
        TargyCikkSzallitasLista = 41557,
        TargySzelvenySzallitas = 41558,
        TargyCikkSzallitas = 41559,

        ZsakKezelesKezdoOldal = 41560,
        ZsakKezelesNaplo = 41561,
        ZsakKezelesKuldes = 41562,
        ZsakKezelesFogadas = 41563,

        TalaltCikkKezdoOldal = 41564,
        TalaltTargyCikkRogzites = 41565,
        TalaltTargyCikkSzerkesztes = 41566,
        TalaltErtekCikkRogzites = 41567,
        TalaltErtekCikkSzerkesztes = 41568,

        SzelvenyLista = 41569,
        KorabbiSzelvenyLista = 41570,
        ElmultEvbenLezartSzelvenyKezdoOldal = 41571,
        OkmanySzelvenyRogzites = 41572,
        OkmanySzelvenySzerkesztes = 41573,
        ErtekSzelvenyRogzites = 41574,
        ErtekSzelvenySzerkesztes = 41575,
        TargySzelvenyRogzites = 41576,
        TargySzelvenySzerkesztes = 41577,

        // Navigátor fogvt.-i karton
        FogvatartottiKartonMegnyitasa = 41705,

        //FogvatartottiKartya használat
        FogvatartottiKatyaSikeres = 43102,
        FogvatartottiKatyaSikertelen = 43103,

        //BVSHOP
        FogvatartottBeazonositas = 43171,


        // Online 
        FogvatartottakKeresese = 43173,
        FogvatartottiKartonMegtekintese = 43174,
        FogvatartottakKereseseInterface = 43214,

        FogvatartottLekerdezes = 43205,
        //TelQueue
        TestTopup = 43358,
        TopUp = 43359,
        CancelTopup = 43360,
        //Pénzügy (italautomata, kiétbolt)
        ZarolasKeres = 43361,
        TerhelesKeres = 43362,
        //NEAK
        KeziJelentes = 43363,
        NagyJelentes = 43364,
        //BvBank
        GiroGeneralas = 43365
    }
}
