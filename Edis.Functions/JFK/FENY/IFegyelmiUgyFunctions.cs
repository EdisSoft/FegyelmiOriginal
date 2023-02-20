using Edis.Entities.Common;
using Edis.Entities.Enums.Fenyites;
using Edis.Entities.Fany;
using Edis.Entities.JFK.FENY;
using Edis.Functions.Base;
using Edis.ViewModels;
using Edis.ViewModels.Fany;
using Edis.ViewModels.JFK;
using Edis.ViewModels.JFK.Dokumentum;
using Edis.ViewModels.JFK.FENY;
using Edis.ViewModels.JFK.FENY.Email;
using Edis.ViewModels.JFK.FENY.FormModel;
using Edis.ViewModels.JFK.Nyomtatvany;
using Edis.ViewModels.JFK.Nyomtatvany.Pdf;
using System;
using System.Collections.Generic;

namespace Edis.Functions.JFK
{
    public interface IFegyelmiUgyFunctions : IFunctionsBase<FegyelmiUgyViewModel, FegyelmiUgy>
    {
        List<FegyelmiUgyListItemViewModel> GetFegyelmiUgyek(int intezetId);
        List<FegyelmiUgyListItemViewModel> GetFegyelmiUgyekArchiv(int intezetId, int ugyEve);
        List<FegyelmiUgyListItemViewModel> GetOsszevontFegyelmiUgyekForId(int FegyelmiUgyId);
        FegyelmiUgyListItemViewModel GetFegyelmiUgyListItemViewModelById(int intezetId, int fegyelmiUgyId);
        List<FegyelmiUgyListItemViewModel> GetFegyelmiUgyekByIds(List<int> fegyelmiUgyIds);
        void DontesFegyelmiUgyElrendeleserol(DontesFegyelmiUgyElrendeleserolViewModel model, bool elrendelem);
        FegyelmiUgyTanuMeghallgatasiJegyzokonyvModel GetFegyelmiUgyTanuMeghallgatasiJegyzokonyv(List<int> fegyelmiUgyId, List<int> naplobejegyzesIds);
        FegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvModel GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(List<int> fegyelmiUgyId, List<int> naplobejegyzesId);
        FegyelmiUgyElsoFokuTargyalasiJegyzokonyvModel GetFegyelmiUgyElsoFokuTargyalasiJegyzokonyv(List<int> fegyelmiUgyId, List<int> naplobejegyzesId);
        FegyelmiUgyHatarozatRogziteseModel GetFegyelmiUgyHatarozatRogzitese(int fegyelmiUgyId, int? naplobejegyzesId, ModalTipusok? modalType);
        void SaveFegyelmiUgyHatarozat(FegyelmiUgyHatarozatRogziteseModel model);
        List<int> SaveFegyelmiUgyTanuMeghallgatasiJegyzokonyv(FegyelmiUgyTanuMeghallgatasiJegyzokonyvModel model);
        List<int> SaveFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(FegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvModel model);
        List<int> SaveFegyelmiUgyElsoFokuTargyalasiJegyzokonyv(FegyelmiUgyElsoFokuTargyalasiJegyzokonyvModel model);
        ReintegraciosTiszt GetReintegraciosTiszt(int fogvatartottId);
        List<ReintegraciosTiszt> GetReintegraciosTisztek(int aktIntezetId);
        FegyelmiUgyViewModel GetFegyelmiUgyById(int id);
        FelfuggesztesModel GetFelfuggesztesiJavaslat(List<int> fegyelmiUgyIds);
        //FegyelmiLapViewModel GetFegyelmiLapById(int id, int? iktatasId = null);
        //List<FegyelmiLapViewModel> GetFegyelmiLapokByFegyelmiUgyIds(List<int> fegyelmiUgyIds);
        FegyelmiLapViewModel GetFegyelmiLapNyomtatvanyByIktatasId(int iktatasId);
        List<FegyelmiLapViewModel> GetFegyelmiLapNyomtatvanyokForEsemenyRogzites(List<int> fegyelmiUgyIds);
        TajekoztatoViewModel GetTajekoztatoById(int fegyelmiUgyId, bool kerem, int? iktatasId = null);
        DateTime? GetFegyelmiHatarido(int fegyelmiUgyId, bool isHataridoModositas, int? leendoStatuszCimkeId = null, bool isKozvetitoi = false);
        ReintegraciosTisztDontesModel GetReintegraciosTisztDontesModel(List<int> fegyelmiUgyId, List<int> naploIds);
        List<int> SaveReintegraciosTisztDontesFeddesModel(ReintegraciosTisztDontesModel model);
        List<int> SaveReintegraciosTisztDontesKioktatasModel(ReintegraciosTisztDontesModel model);
        int FegyelmiUgyInditasa(int esemenyId, int fogvatartottId, Intezet intezet = null, bool? bvBankbol = null);
        void SaveKarteritesIdFegyelmiUgybe(int fegyelmiUgyId, int karteritesId);
        List<int> FegyelmiUgyekInditasa(int esemenyId, List<int> fogvatartottIds, bool? bvBankbol = null, int? intezetId=null);
       
        EljarasLefolytatasanakMegtagadasModel GetEljarasLefolytatasanakMegtagadasa();
        void EljarasLefolytatasanakMegtagadasa(EljarasLefolytatasanakMegtagadasModel model);
        void HataridoModositasiJavaslat(LeirasMegadasFormModel model);
        void FelfuggesztesiJavaslat(FelfuggesztesModel model);

        MeghallgatasiJegyzokonyv EljarasAlaVontMeghallgatasDokumentumAdatok(int naploId);
        MeghallgatasiJegyzokonyv EljarasAlaVontMeghallgatasDokumentumAdatokByIktatasId(int iktatasId);
        MeghallgatasiJegyzokonyv TanuMeghallgatasDokumentumAdatok(int naploId);
        MeghallgatasiJegyzokonyv TanuMeghallgatasDokumentumAdatokByIktatasId(int iktatasId);
        MeghallgatasiJegyzokonyv SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok(int naploId);
        MeghallgatasiJegyzokonyv SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatokByIktatasId(int iktatasId);
        TargyalasiJegyzokonyv ElsoFokuTargyalasiDokumentumAdatok(int naploId);
        TargyalasiJegyzokonyv ElsoFokuTargyalasiDokumentumAdatokByIktatasId(int iktatasId);
        int SaveFegyelmiUgyUjResztvevo(int fegyelmiUgyId, int fogvatartottId);

        HelysziniSzemleModel GetFegyelmiUgyDataHelysziniSzemleModel(List<int> fegyelmiUgyIds, List<int> naploId);
        List<int> FegyelmiUgyHelysziniSzemleMentes(HelysziniSzemleModel model);
        OsszefoglaloJelentesModel GetOsszefoglaloJelentesModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveOsszefoglaloJelentes(OsszefoglaloJelentesModel model);

        SzakteruletiVelemenyModel GetFegyelmiUgyDataSzakteruletiVelemenyModelModel(List<int> fegyelmiUgyIds, List<int> naploId);
        List<int> FegyelmiUgySzakteruletiVelemenyModelMentes(SzakteruletiVelemenyModel model);

        FegyelmiUgyOsszevonasModel GetFegyelmiUgyDataOsszevonashoz(int fegyelmiUgyId);
        void FegyelmiUgyOsszevonasMentes(int fegyelmiUgyId, List<int> alUgyList);


        ElsoFokuFegyelmiTargyalasElokeszitesModel GetElsoFokuFegyelmiTargyalasElokeszitese(int fegyelmiUgyId);
        ElsoFokuFegyelmiTargyalasElokeszitesModel GetMasodFokuFegyelmiTargyalasElokeszitese(int fegyelmiUgyId);

        void SaveElsoFokuTargyalasKituzese(ElsoFokuFegyelmiTargyalasElokeszitesModel model);
        void SaveReintegraciosTisztDontesVisszakuldesModel(ReintegraciosTisztDontesModelVisszakuldes model);
        ReintegraciosTisztDontesModelVisszakuldes GetReintegraciosTisztDontesVisszakuldesModel(List<int> fegyelmiUgyIds);
        FegyelmiHatarozatViewModel GetHatarozatById(int fegyelmiUgyId, int dokumentumTipusCimkeId, int? iktatasId = null, int? naploId = null);
        void SaveMasodFokuTargyalasKituzese(ElsoFokuFegyelmiTargyalasElokeszitesModel model);
        KirendeltVedoKereseModel GetKirendeltVedoKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        MeghatalmazottVedoKereseModel GetMeghatalmazottVedoKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        void ThrowExceptionIfFegyelmiUgyNemModosithato(List<int> fegyelmiUgyIds);
        List<int> SaveMeghatalmazottVedoKereseModalData(MeghatalmazottVedoKereseModel model);
        List<int> SaveKirendeltVedoKereseModalData(KirendeltVedoKereseModel model);
        List<FegyelmiUgyViewModel> GetFegyelmiUgyekByNaploTipus(List<int> fegyelmiUgyIds, int naploTipusCimkeId);
        HataridoModositasModel GetHataridoModositasModalData(List<int> fegyelmiUgyIds);
        bool SaveHataridoModositasModalData(HataridoModositasModel model);
        FelfuggesztesModel GetFelfuggesztesModalData(List<int> fegyelmiUgyIds);
        bool SaveFelfuggesztesMegszuntetes(FelfuggesztesMegszunteteseViewModel model);
        bool SaveFelfuggesztesModalData(FelfuggesztesModel model);
        VedoTelefonosTajekoztatasaModel GetVedoTelefonosTajekoztatasaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveVedoTelefonosTajekoztatasaModalData(VedoTelefonosTajekoztatasaModel model);
        KioktatasReintegraciosTisztiJogkorbenViewModel GetKioktatasNyomtatvanyById(int fegyelmiUgyId, int? iktatasId = null, int? naploId = null);
        List<VedoKirendeleseNyilatkozatViewModel> GetVedoKirendeleseNyilatkozatNyomtatvanyokByNaplok(List<int> naploIds);
        List<VedoKirendeleseNyilatkozatViewModel> GetVedoKirendeleseNyilatkozatNyomtatvanyokByIktatasok(List<int> iktatasIds);
        List<VedoKirendeleseViewModel> GetVedoKirendeleseNyomtatvanyokByNaplok(List<int> naploIds);
        List<VedoKirendeleseViewModel> GetVedoKirendeleseNyomtatvanyokByIktatasok(List<int> iktatasIds);
        List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel> GetMeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyokByNaplok(List<int> naploIds);
        List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel> GetMeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyokByIktatasok(List<int> iktatasIds);
        List<MeghatalmazottVedoKirendeleseViewModel> GetMeghatalmazottVedoKirendeleseNyomtatvanyokByNaplok(List<int> naploIds);
        List<MeghatalmazottVedoKirendeleseViewModel> GetMeghatalmazottVedoKirendeleseNyomtatvanyokByIktatasok(List<int> iktatasIds);
        List<VedoTelefonosTajekoztatasaViewModel> GetVedoTelefonosTajekoztatasaNyomtatvanyokByNaplok(List<int> naploIds);
        List<VedoTelefonosTajekoztatasaViewModel> GetVedoTelefonosTajekoztatasaNyomtatvanyokByIktatasok(List<int> iktatasIds);
        FegyelmiUgyMasodFokuTargyalasiJegyzokonyvModel GetMasodFokuTargyalasiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveMasodFokuTargyalasiJegyzokonyv(FegyelmiUgyMasodFokuTargyalasiJegyzokonyvModel model);

        string SaveFegyelmiUgyUjAllomanyiResztvevo(List<int> fegyelmiUgyIds, string allomanyiSzemelySid);
        List<MasodfokuTargyalasiJegyzokonyvViewModel> GetMasodfokuTargyalasiJegyzokonyvNyomtatvanyokByNaplok(List<int> naploIds);
        List<MasodfokuTargyalasiJegyzokonyvViewModel> GetMasodfokuTargyalasiJegyzokonyvNyomtatvanyokByIktatasok(List<int> iktatasIds);
        SzakteruletiVelemenyFelkeresModel GetSzakteruletiVelemenyKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveSzakteruletiVelemenyKereseModalData(SzakteruletiVelemenyFelkeresModel model);

        void JogiKepviseletVisszavonasa(List<int> fegyelmiUgyIds);
        SzembesitesiJegyzokonyvModel GetSzembesitesiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveSzembesitesiJegyzokonyv(SzembesitesiJegyzokonyvModel model);


        void UpdateFenyitesVegrahajtvaTipusuUgyek(object o);
        FegyelmiUgyHatarozatRogziteseMasodfokonModel GetHatarozatRogziteseMasodfokon(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);

        List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel> GetMasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyokByNaplok(List<int> naploIds);
        List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel> GetMasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyokByIktatasok(List<int> iktatasIds);
        List<MasodfokuFegyelmiHatarozatViewModel> GetMasodfokuFegyelmiHatarozatNyomtatvanyokByNaplok(List<int> naploIds);
        List<MasodfokuFegyelmiHatarozatViewModel> GetMasodfokuFegyelmiHatarozatNyomtatvanyokByIktatasok(List<int> iktatasIds);
        List<MasodfokuFegyelmiHatarozatViewModel> GetMasodfokuFegyelmiHatarozatNyomtatvanyokByFegyelmiUgyIds(List<int> fegyelmiUgyIds);
        List<SzembesitesiJegyzokonyvViewModel> GetSzembesitesiJegyzokonyvNyomtatvanyokByNaplok(List<int> naploIds);
        List<SzembesitesiJegyzokonyvViewModel> GetSzembesitesiJegyzokonyvNyomtatvanyokByIktatasok(List<int> iktatasIds);

        List<AktivitasFolyamModel> GetAktivitasFolyamList(int? intezetId, List<int> fegyelmiUgyIds = null);
        List<KSelect2ItemModel> FindSzakteruletiVezetokForSelect(string term);
        void SaveMasodFokuHatarozat(FegyelmiUgyHatarozatRogziteseMasodfokonModel model);
        List<int> SaveKozvetitoiEljarasKezdemenyezeseModalData(KozvetitoiEljarasKezdemenyezeseModel model);
        KozvetitoiEljarasKezdemenyezeseModel GetKozvetitoiEljarasKezdemenyezeseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        MaganelzarasMegkezdesenekRogziteseModel GetMaganelzarasMegkezdesenekRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveMaganelzarasMegkezdesenekRogziteseModalData(MaganelzarasMegkezdesenekRogziteseModel model);
        List<int> GetArchivEvek(int intezetId);

        List<MaganelzarasMegkezdeseNyomtatasViewModel> GetMaganelzarasMegkezdesenekRogziteseNyomtatasByFegyelmiUgyIds(List<int> fegyelmiUgyIds);
        List<MaganelzarasMegkezdeseNyomtatasViewModel> GetMaganelzarasMegkezdesenekRogziteseNyomtatasByNaploIds(List<int> naploIds);
        List<MaganelzarasMegkezdeseNyomtatasViewModel> GetMaganelzarasMegkezdesenekRogziteseNyomtatasByIktatasIds(List<int> iktatasIds);

        DontesKozvetitoiEljarasrolModel GetDontesKozvetitoiEljarasrolModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveDontesKozvetitoiEljarasrolModalData(DontesKozvetitoiEljarasrolModel model);

        FenyitesNemVegrehajthatoModel GetFenyitesNemVegrehajthatoModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveFenyitesNemVegrehajthatoModalData(FenyitesNemVegrehajthatoModel model);
        MaganelzarasEllenjavallataModel GetMaganelzarasEllenjavallataModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveMaganelzarasEllenjavallataModalData(MaganelzarasEllenjavallataModel model);
        List<int> SaveMaganelzarasVegrehajtvaModalData(MaganelzarasVegrehajtvaModel model);
        MaganelzarasVegrehajtvaModel GetMaganelzarasVegrehajtvaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        IndoklassalMegszuntetesModel GetIndoklassalMegszuntetesModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveIndoklassalMegszuntetesModalData(IndoklassalMegszuntetesModel model);
        MaganelzarasMegszakitasanakRogziteseModel GetMaganelzarasMegszakitasanakRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveMaganelzarasMegszakitasanakRogziteseModalData(MaganelzarasMegszakitasanakRogziteseModel model);
        string GetElhelyezesNevByString(string val);
        KozvetitoiEljarasGaranciaTeljesulesenekRogziteseModel GetKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData(KozvetitoiEljarasGaranciaTeljesulesenekRogziteseModel model);

        FeljegyzesEsMegallapodasModel GetFeljegyzesEsMegallapodasModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveFeljegyzesEsMegallapodasModalData(FeljegyzesEsMegallapodasModel model);

        List<MegallapodasEsFeljegyzesNyomtatasViewModel> GetMegallapodasEsFeljegyzesNyomtatasByNaploIds(List<int> naploIds);
        List<MegallapodasEsFeljegyzesNyomtatasViewModel> GetMegallapodasEsFeljegyzesNyomtatasByIktatasIds(List<int> iktatasIds);

        KozvetitoiEljarasHataridoModositasKereseModel GetKozvetitoiEljarasHataridoModositasKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveKozvetitoiEljarasHataridoModositasKereseModalData(KozvetitoiEljarasHataridoModositasKereseModel model);
        KozvetitoiEljarasLezarasaModel GetKozvetitoiEljarasLezarasaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveKozvetitoiEljarasLezarasaModalData(KozvetitoiEljarasLezarasaModel model);

        KozvetitoiEljarasGaranciaHataridoModositasaModel GetKozvetitoiEljarasHataridoModositasModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveKozvetitoiEljarasHataridoModositasModalData(KozvetitoiEljarasGaranciaHataridoModositasaModel model);
        void FelfuggesztesMegszunteteseConfrimModal(List<int> fegyelmiUgyIds);
        FegyelmiElkulonitesElrendeleseFelulvizsgalataMegszunteteseModel GetFegyelmiElkulonitesElrendelesFelulvizsgataMegszuntetese(List<int> fegyelmiUgyIds, List<int> naploIds);
        List<int> SaveElkulonitesElrendelesVagyElkulonitesMegszuntetes(FegyelmiElkulonitesElrendeleseFelulvizsgalataMegszunteteseModel model);
        List<FelfuggesztesEmailData> UpdateAutomatikusFelfuggesztesTipusuUgyek(object o);

        FeljelentesRogziteseModel GetBuntetoFeljelentesRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds);
        List<int> SaveBuntetoFeljelentesRogziteseModalData(FeljelentesRogziteseModel model);
        List<KozvetitoiEljarasonReszvetelNyomtatasViewModel> KozvetitoiEljarasonReszvetelNyomtatasByNaploIds(List<int> naploIds);
        List<KozvetitoiEljarasonReszvetelNyomtatasViewModel> KozvetitoiEljarasonReszvetelNyomtatasByIktatasIds(List<int> iktatasIds);

        List<FelfuggesztesEmailData> FegyelmiUgyValtozasEmailAdatok(List<int> felfuggesztettUgyek, List<int> aktivraAllitottUgyek);
        List<int> SaveMaganelzarasElrendeleseModalData(MaganelzarasElrendeleseModel model);

        List<ElkulonitesEmailData> FegyelmiUgyElkulonitesErtesitoAdatok();
        List<int> FenyitesVegrahajtasaKesz(List<int> fegyelmiUgyIds);
        List<OsszefoglaloJelentesNyomtatasModel> GetOsszefoglalojelentesNyomtatasAdat(int fegyelmiUgyId);
        void NotifyUseresOnFegyelmiUgyValtozas(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList);
        List<FegyelmiUgy> GetFegyelmiUgyekForTimeline(int? fogvatartottId);
        FegyelmiUgy GetFegyelmiUgyEntityById(int fegyelmiUgyId);
        OsszefoglaloJelentesNyomtatasModelTeljes GetOsszefoglalojelentesNyomtatasByNaploId(int naplobejegyzesId);
        OsszefoglaloJelentesNyomtatasModelTeljes GetOsszefoglalojelentesNyomtatasByIktatasIds(int iktatasId);
        void UpdateSzabadultFogvatartottUgyek(object o);

        List<TargyiErtesitesEmailData> FegyelmiUgyTobbletszolgaltatasEmailAdatok();
        Tuple<List<FogvatartottFegyelmiUgyAdatokModel>, List<FogvatartottFegyelmiUgyAdatokModel>> GetOsszesFegyelmiUgyByFegyelmiUgyId(int fegyelmiUgyId);
        FegyelmiUgyKivizsgaloModositasaViewModel GetFegyelmiUgyDataKivizsgaloModositashoz(List<int> fegyelmiUgyIds);
        string SaveFegyelmiUgyKivizsgaloModositasa(FegyelmiUgyKivizsgaloModositasaViewModel model);
        void UjSzabadszovegesFegyelmiNaplobejegyzesFelvitele(FegyelmiNaplobejegyzesFelviteleModel model);
        FegyelmiNaplobejegyzesFelviteleModel GetSzabadszovegesFegyelmiNaplobejegyzesSzerkesztese(List<int> naploIds);
        void SzabadszovegesFegyelmiNaplobejegyzesSzerkesztese(int naploId, int fegyelmiUgyId, string jegyzokonyvTartalma);
        List<MaganelzarasFofelugyeloEmailData> MaganelzarasFofelugyelokEmailAdatok();
        List<FogvatartottFegyelmiUgyAdatokModel> GetFolyamatbanEsKiszabvaFegyelmiUgyekByFegyelmiUgyId(int fegyelmiUgyId);
        OsszefoglaloJelentesDocxNyomtatasModel GetOsszefoglalojelentesDocxNyomtatasByNaploId(int naplobejegyzesId);
        OsszefoglaloJelentesDocxNyomtatasModel GetOsszefoglalojelentesDocxNyomtatasByIktatasId(int iktatasId);
        BuntetoFeljelentesDocxNyomtatasModel GetBuntetoFeljelentesDocxNyomtatasByNaploId(int naplobejegyzesId);
        BuntetoFeljelentesDocxNyomtatasModel GetBuntetoFeljelentesDocxNyomtatasByIktatasId(int iktatasId);
        List<KSelect2ItemModel> GetJelenlevoIntezetiFogvatartottak(int intezetId, string queryString);
        List<FegyelmiUgyListItemViewModel> GetFegyelmiUgyekByFogvatartottId(int fogvatartottId);
        List<int> GetFegyelmiUgyeIdsByEsemenyId(int esemenyId);
        List<FegyelmiUgy> GetFegyelmiUgyekByEsemenyId(int esemenyId);
        List<BvBankFegyelmiUgyListaModel> GetNyitottFegyelmiUgyekForBvBankByFogvatartottId(int fogvatartottId);
        FegyelmiUgy GetFegyelmiUgyEsemennyel(int fegyelmiUgyId);
        List<KarjelentoLapDocxNyomtatasModel> GetKarjelentoLapDocxNyomtatasByEsemenyId(int esemenyId, int? fegyelmiUgyId = null);
        KarjelentoLapDocxNyomtatasModel GetKarjelentoLapDocxNyomtatasByIktatasId(int iktatasId);
        void OsszevontFegyelmiUgyekLezarasaByFegyelmiUgyId(int fegyelmiUgyId);
        ZarkaViewModel GetZarkaById(int id);
        FogvatartottNezet GetFogvatartottZarkahoz(int fogvatartottId);
        MasodfokuFegyelmiHatarozatViewModel GetMasodfokuArchivHatarozat(int fegyelmiUgyId);
        FegyelmiNaplobejegyzesFelviteleModel GetUgyesziHatalyonKivulHelyezes(List<int> naploIds);
        List<int> SaveHatalyonKivulHelyezes(FegyelmiNaplobejegyzesFelviteleModel model);
    }
}
