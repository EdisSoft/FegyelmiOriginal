using Edis.Entities.Common;
using Edis.Functions.Base;
using Edis.ViewModels.Common;
using System.Collections.Generic;

namespace Edis.Functions.Common
{
    public interface INyomtatvanySablonFunctionBase : IFunctionsBase<NyomtatvanySablonModel, NyomtatvanySablon>
    {
        List<NyomtatvanySablonModel> GetModulNyomtatvanyok(string modelNev);
        //DocX FelhivasIntezetErtesitoNyomtatvanyKeszitesById(int intezetErtesitoFejlecId);
        //DocX ElovezetesHatarozatNyomtatvanyKeszitesByElovezetesId(int elovezetesId);
        //DocX FelhivasNyomtatvanyKeszitesById(int Id);

        //DocX FelhivasBoritekNyomtatvanyKeszitesByTertivevenyId(int tertivevenyId);
        //DocX TartozkodasihelyMegallapitasNyomtatvanyKeszitesByTartozkodasihelyId(int tartozkodasihelyId);
        //DocX ElfogatoparacsNyomtatvanyKeszitesByElfogatoparacsId(int elfogatoparancsId);
        //DocX ElovezetesIntezetErtesitoNyomtatvanyKeszitesById(int elovezetesId);
        byte[] NyomtatvanyokOsszefuzeseByteTomb(List<byte[]> nyomtatvanyok);
        //DocX PartfogasiUgyNyomtatvanyKeszitesById(int partfogasiUgyId,int nyomtatvanySablonId);

        //DocX PttrCsoportfoglalkozasTematikaNyomtatvany(int foglalkozasId);

        //DocX PttrTreningTematikaNyomtatvany(int foglalkozasId);

        //DocX PttrCsoportfoglalkozasLeirasaNyomtatvany(int foglalkozasId);

        //DocX PttrTreningLeirasaNyomtatvany(int foglalkozasId);

        //DocX PttrCsoportfoglalkozasFeljegyzesNyomtatvany(int foglalkozasId);

        //DocX PmeNyomtatvanyKeszitesByFogvatartottId(int fogvatartottId);

        //DocX KarteritesiMellekletKeszitesById(int karteritesId, int docId);

        //DocX MunkaltatoiIgazolasKeszitesByFogvatartottId(int fogvatartottId);

        //DocX TomegesTajekoztatoBesorolasrol(string munkaltatasIdk);

        //DocX TajekoztatoBesorolasrolMunkadijrol(int munkaltatasId);

        //DocX HaviMunkadijJegyzek(int fogvatartottId, int elszamolasId);

        //DocX HaviMunkadijJegyzekTomeges(int falezId, bool egyedivel);

        //DocX TomegesFizetettSzabadsagNyilvantartoLap(string fogvatartottIdk, int ev);

        //DocX FizetettSzabadsagNyilvantartoLap(int fogvatartottId, int ev);

        //DocX SzamlaTortenetNyomtatasKeszitesById(int id, int fogvatartottId, string interval);

        //DocX PenztariNyomtatvanyKeszites(int giroId);

        //DocX NapiJelenletiIv(int munkahelyId, int ev, int ho, int nap, int munkaltatasId = 0);

        //DocX OktatasiOsztondijKimutatas(int falezId);

        //DocX MunkadijElszamolasOsszesites(int ev, int honap, bool terapias, List<int> munkaltatoIdk);

        //DocX OsztondijElszamolasOsszesites(int ev, int honap);
        //DocX LetiltasNyomtatasKeszitesById(int id, int fogvatartottId);
        //DocX HarmadolosSurgetesKeszitesById(int letiltasid);

        //DocX KartalanitasHatarozat(int hatarozatId);
        //DocX KartalanitasHatarozathozatal(int hatarozatId, bool mellozes = false);
        //DocX KartalanitasBvBiroNyomtatvany(int kartalanitasId, int fogvatartottId, FanySorokModel fanySorok = null);

        //DocX ElojegyzesNyomtatasKeszitesById(int id, int elojegyzesId);

        //DocX KonyveltBizonylatNyomtatasKeszitesById(int bizonylatId);

        //DocX TartozasIgazolasKeszitesFogvatartottnakById(int fogvatartottId, int nyomtatvanyId);

        //DocX NegyedEvesZarasLetoltes(int zarasId);


        //DocX TelefonSzerzodes(int fogvatartottId);

        //DocX SzabadulasiEsElhalalozasiNyilatkozat(int fogvatartottId);

        //DocX BfbHatarozat(int fogvatartottId, int ulesId);


        //DocX TobbletInformacioJutalomFenyites(int intezetId, List<int> fogvatartottIds);

        //DocX OkmanySzelvenyLetoltese(int szelvenyId);
        //DocX ErtekSzelvenyLetoltese(int szelvenyId);
        //DocX TargySzelvenyLetoltese(int szelvenyId);
        //DocX ErtekSzelvenyAtadasiJegyzokonyv(List<int> szelvenyIdLista, int celIntezetId);
        //DocX OkmanySzelvenyAtadasiJegyzokonyv(List<int> szelvenyIdLista, int celIntezetId);
        //DocX TargySzelvenyAtadasiJegyzokonyv(List<int> szelvenyIdLista, int celIntezetId);

        //DocX MegsemmisitesiJegyzokonyvTalaltTargy(int targyCikkId);
        //DocX MegsemmisitesiJegyzokonyvTalaltErtek(int targyCikkId);
        //DocX JegyzokonyvSerultOkmanyBoritek(int szelvenyId);
        //DocX JegyzokonyvSerultErtekBoritek(int szelvenyId);
        //DocX JegyzokonyvSerultTargyBoritek(int szelvenyId);
        //DocX JegyzokonyvHamisKulfoldiFizetoeszkoz(int szelvenyId);
        //DocX KimutatasMegorzesCeljaboltAtvettLetetbeNemHelyezhetoTargyakrol(int szelvenyId);
        //DocX JegyzokonyvAtvettLofegyver(int szelvenyId);
        //DocX TechnikaiEszkozTartasiEngedely(int szelvenyId);
        //DocX FogvatartottolAtvettPenz(int szelvenyId);
        //DocX ErtekAtadasiJegyzokonyv(List<int> masIntezetCikkIds, List<int> talaltCikkIds, int celIntezetId);
        //DocX OkmanyAtadasiJegyzokonyv(List<int> masIntezetCikkIds, int celIntezetId);
        //DocX TargyAtadasiJegyzokonyv(List<int> masIntezetCikkIds, List<int> talaltCikkIds, int celIntezetId);
        //DocX OktatasiOsztondijJegyzek(int elszamolasId);
        //DocX OktatasiOsztondijJegyzek(OktatasElszamolasDoksiModel elszamolas);

        //DocX NapiBfbHatarozatTomeges(int intezetId, int ulesId, List<int> fogvatartottIds);

        //DocX FeltoltoKartyaVasarlasiTetelOsszesito(int kietkezoVasarlasId);
        //DocX TermekVasarlasiTetelOsszesito(int kietkezoVasarlasId);
        //DocX Ugyfelmappa(int ugyfelId);
        //DocX Ugyfelmappa(ViewModels.RIN.UgyfelViewModel ugyfel);
        //DocX EszaKilepes(int ugyfelId);
        //DocX EszaKilepes(ViewModels.RIN.UgyfelViewModel ugyfel);
        //DocX BevetelezesBeerkeztetesiBizonylat(int bevetelezesId, string nev);
        //DocX SelejtezesiBizonylatFifos(int selejtezesId, string nev);
        //DocX SelejtezesiBizonylatUtolsoBeszerAr(int selejtezesId, string nev);
        //DocX SajatFelhasznalasiBizonylat(int sajFelhaszId, string nev);
        //DocX VisszaruBizonylat(int visszaruId, string nev);
        //DocX KulsoPartnerBizonylat(int kulsoPartnerId, string nev);
        //DocX RaktarkoziMozgasBizonylat(int raktarkoziMozgasId, string nev);

        //DocX EgyeniJelenletiIv(ViewModels.RIN.UgyfelViewModel ugyfel);
        //DocX EgyeniJelenletiIv(int ugyfelId);
        //DocX CsoportosJelenletiIv(List<ViewModels.RIN.UgyfelViewModel> ugyfelek, bool isHozzatartozoi);
        //DocX CsoportosJelenletiIv(List<int> ugyfelIds, bool isHozzatartozoi);
        //DocX KapcsolatfelvetelEredmenytelensege(ViewModels.RIN.UgyfelViewModel ugyfel);
        //DocX KapcsolatfelvetelEredmenytelensege(int ugyfelId);
        //DocX EFT(ViewModels.RIN.UgyfelViewModel ugyfel);
        //DocX EFT(int ugyfelId);
        //DocX Felulvizsgalat(int ugyfelId, int felulvizsgalatId);
        //DocX KimutatasokNovekedesiTetelek(DateTime start, DateTime end, List<int> tipusKszIds, string nev);
        //DocX KimutatasokCsokkentesiTetelek(DateTime start, DateTime end, List<int> tipusKszIds, string nev);
        //DocX KimutatasokNovekedesiBizonylat (DateTime start, DateTime end, List<int> tipusKszIds, string nev, List<string> tablazatSzuro);
        //DocX PenzugyiLeadasBizonylat(int penzugyiLeadasId, string nev);
        //DocX OsszesitettKimutatas(int penzugyiLeadasId, string nev);        
        //DocX LeltarIvBizonylat(int leltarIvId, string nev);
        //DocX LeltarKimutatasok(int leltarIvId, string nev);
        //DocX FogvatartottNyomtatvany(int fogvatartottId);
        //DocX KockazatiFeljegyzesNyomtatvany(int fogvatartottId);
        //DocX RendkivuliEsemenyNyomtatvany(int fogvatartottId);
        //bool OetIntezetiParameterVizsgalat(int szelvenyId);
        //bool OetIntezetiParameterVizsgalatIntezetAlapjan(int jelenlegiIntezetId);
        //DocX LeltarTalaltMennyisegBizonylat(int leltarIvId, string nev);

        //#region Fegyelmi fenyítés

        //List<NyomtatvanySablonModel> GetFegyelmiNyomtatvanyokList();
        //DocX EljarasAlaVontMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz(int fegyelmiUgyId);
        //DocX TanuMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz(int fegyelmiUgyId);
        //DocX JegyzokonyvTanuSzemelyiAllomanyiTagMeghallgatasarol(int fegyelmiUgyId);
        //DocX FegyelmiTargyalasiJegyzokonyv(int fegyelmiUgyId);
        //DocX JegyzokonyvFogvatartottakSzembesiteserol(int fegyelmiUgyId);
        //DocX JegyzokonyvAlairasMegtagadasarol(int fegyelmiUgyId);
        //DocX FanyErtesitoLap(int fegyelmiUgyId);
        //DocX FegyelmiElkulonitesiLap(int fegyelmiUgyId);
        //DocX TajekoztatoAFegyelmiEljarassalOsszefuggoJogokrolKotelezettsegekrol(int fegyelmiUgyId);
        //DocX VegrehajtasiLap(int fegyelmiUgyId);
        //DocX NyilatkozatKozvetitoiEljarasonValoReszvetelrolEljarasAlaVontFogvatartott(int fegyelmiUgyId);
        //DocX NyilatkozatKozvetitoiEljarasonValoReszvetelrolSertett(int fegyelmiUgyId);
        //DocX FeljegyzesKozvetitoiMegbeszelesrol(int fegyelmiUgyId);
        //DocX MegallapodasKozvetitoiEljarasEredmenyerol(int fegyelmiUgyId);
        //DocX NyilatkozatVedoErtesitesehez(int fegyelmiUgyId);
        //DocX NyilatkozatVedoKirendelesehez(int fegyelmiUgyId);
        //DocX VedoKiertesitese(int fegyelmiUgyId);
        //DocX VedoKirendelese(int fegyelmiUgyId);
        //DocX VedoTelefononTortenoTajekoztatasa(int fegyelmiUgyId);


        //DocX ErtesitoLap(int fegyelmiUgyId, int? iktatasId = null);
        //DocX ElkulonitesiLap(int fegyelmiUgyId, int? iktatasId = null);
        //#endregion

        byte[] NyomtatvanySablonDocxById(int id);

    }
}