﻿//namespace Edis.Entities.Enums
//{
//    public enum DeletedFilters
//    {
//        #region FANY
//        Kapcsolattarto,
//        Fogvatartott,
//        FogvatartottNezet,
//        FogvatartottSzemely,
//        FogvatartottSzemelyesAdatai,
//        Szemelyzet,
//        SzemelyzetSzerepkor,
//        SzemelyzetCsoport,
//        SzemelyzetJogosultsag,
//        Intezet,
//        Kodszotar,
//        Helyseg,
//        FogvatartottFenykep,
//        IntezetiObjektum,
//        FogvatartottiKartya,
//        Korlet,
//        Zarka,
//        #endregion

//        #region FTTR
//        FttrTertiveveny,
//        FttrPartner,
//        FttrFelhivasiLap,
//        FttrLakcim,
//        FttrItelet,
//        FttrLink,
//        FttrElovezetesElrendelese,
//        FttrTartozkodasiHelyMegallapitas,
//        FttrFelhivasiIdopont,
//        FttrFelhivasiLapMegjegyzes,
//        FttrFelhivasiLapHozzaferes,
//        FttrFelhivasiLapKedvenc,
//        FttrFuggobenTartas,
//        FttrElfogatoParancs,
//        FttrIntezetErtesitoFejlec,
//        FttrIntezetErtesito,
//        FttrIntezetiParameter,
//        FttrMunkanapKivetel,
//        FttrAlkalmazasParameter,
//        #endregion

//        #region PTTR
//        PttrPartfogolt,
//        PttrPartfogasiUgy,
//        PttrPartfogas,
//        PttrPartfogoltSzemely,
//        PttrUgycsoportUgyOsszerendeles,
//        PttrUgySema,
//        PttrPartfogasHozzaferes,
//        PttrEgyuttmukodoHatosag,
//        PttrPartfogasKedvenc,
//        PttrNaptar,
//        PttrIntezetiFejlec,
//        PttrPartfogo,
//        PttrFelfogadasiHely,

//        PttrCsoportfoglalkozas,

//        PttrCsoportfoglalkozasResztvevok,
//        PttrCsoportfoglalkozasIdopont,
//        #endregion

//        #region Prediktiv
//        PredBlokkok,
//        PredFogvatartottErtekeles,
//        PredKerdesek,
//        PredKedvenc,
//        PredElozmeny,
//        PredValaszLehetosegek,
//        PredFigyelmeztetesek,
//        PredBesorolasBeallitasok,
//        PredValaszok,
//        PredMuveletStatuszok,
//        PredErtesitoEmail,
//        PredBlokkKerdes,
//        #endregion

//        #region BV bolt
//        PlKietkezoboltCikktorzsFeltoltes,
//        PlSzolgaltato,
//        PlKietkezoBolt,
//        #endregion

//        #region BvBank
//        IntezetiParameter,
//        EgyeniSzamla,
//        KeresesiElozmenyek,
//        HaviKietkezesiKeret,
//        OrszagosParameter,
//        Letiltasok,
//        LetiltasFelfuggesztes,
//        LetiltasLevonasok,
//        KietkezesCsokkentes,
//        KietkezesNoveles,
//        UtalasiHelyek,
//        KarteritesLevonasok,
//        Karteritesek,
//        BelsoTranzakciok,
//        KulsoTranzakciok,
//        KarterithetoTetelek,
//        KarteritendoTetelek,
//        WizardKarteritesiFolyamatStatuszVezerlok,
//        WizardKarteritesiFolyamatok,
//        BvTartozasok,
//        BvTartozasLevonasok,
//        AtfutoSzamlaTetelek,
//        AtfutoSzamlaTetelTranzakciok,
//        BelsoAtvazetesiTranzakciok,
//        NaploCelzottPenz,
//        NaploSzabaditasraTartakeoltOsszeg,
//        NaploMunkadijbolSzuksegletiCikkreFordithatoPenz,
//        NaploVedettPenz,
//        Zarolasok,
//        NaploZarolasok,
//        Megjegyzesek,
//        Rabtartasdij,
//        NaploAtfutoSzamlaTetelek,
//        Bizonylatok,
//        Szolgaltato,
//        Uzenetek,
//        UzenetOlvasas,
//        Giro,
//        TerhelesElojegyzes,
//        FoglalkoztatasBizonylat,
//        FoglalkoztatasBizonylatTetelek,
//        FoglalkoztatasBizonylatImport,
//        FogvatartottBentToltottNapok,
//        KarteritesSzuneteltetesNaplo,
//        #endregion

//        #region Foglalkoztatas
//        Munkahely,
//        MunkahelyView,
//        Foglalkoztatas,
//        Munkaltatas,
//        BesorolasiAdatok,
//        BesorolasiAdatIntervallum,
//        BesorolasiAdatPotlek,
//        Munkaltato,
//        MunkanapAthelyezes,
//        Munkakor1,
//        MunkakorView,
//        DemoValidate,
//        Oktatas,
//        OktatasResztvevo,
//        MunkaraAlkalmassag,
//        FelmentesMunkaAlol,
//        FogvatartottFoglalkoztatasView,
//        ElojegyzettMunkaltatasView,
//        BefogadoBizMegjel,
//        NapiMunkaido,
//        NapiMunkaidoIntervallum,
//        MunkahelyVezeto,
//        Szabadsag,
//        Falez,
//        HaviElszamolas,
//        TeljesitmenyberesCikktorzs,
//        NapiTeljesites,
//        HaviElszamolasOktatas,
//        NapiOktatasJelenlet,
//        FalezOktatas,
//        OktatasiNap,
//        #endregion

//        #region Menza
//        EtkezesLog,
//        NapiEtkezesLog,
//        #endregion

//        #region Kartalanitas
//        EgyebPanasz,
//        Adatszolgaltatas,
//        Panasz,
//        PanaszBvBiroDontes,
//        PanaszTovabbiUgyek,
//        PanaszTovabbiUgyekTipus,
//        ZarkaLetszam,
//        ZarkaFogvatartott,
//        #endregion

//        NyomtatvanySablon,
//        FttrSzemelyzetParameter
//    }
//}
