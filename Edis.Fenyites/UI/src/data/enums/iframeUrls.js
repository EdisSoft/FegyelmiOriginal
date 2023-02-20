import Kodszotar from './kodszotar';
import { KonalyticsInit } from '../../utils/konalytics';
import store from '../../store';
import { BeallitasokStoreTypes } from '../../store/modules/beallitasok';

const IFrameUrls = Object.freeze({
  UjFenyites: function() {
    var fanyBaseUrl =
      store.getters[BeallitasokStoreTypes.getters.getFanyBaseUrl];
    return (
      fanyBaseUrl +
      '/Pages/Funkciok/JFKAlap.aspx?JFK=true&oldal=RendkivuliEsemenyKarbantartasaRendkivuliEsemenyFelvitel&jogosultsag=AFRekReF&AktualisIntezetAzon='
    );
  },

  GetFanyArchiveUrl: function(aktualisIntezetAzon, fegyelmiUgy) {
    var fanyBaseUrl =
      store.getters[BeallitasokStoreTypes.getters.getFanyBaseUrl];
    var baseUrl = fanyBaseUrl + '/Pages/Funkciok/JFKAlap.aspx?JFK=true';

    baseUrl += '&AktualisIntezetAzon=' + aktualisIntezetAzon;
    baseUrl += '&AktualisFogvatartottId=' + fegyelmiUgy.FogvatartottId;
    baseUrl += '&AktualisFegyelmiUgyId=' + fegyelmiUgy.FegyelmiUgyId;
    baseUrl +=
      '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiUgyReszletek&jogosultsag=AFFAFeUgR';
    var urls = [];
    urls.push({
      title: 'Archív ügy',
      url: baseUrl,
    });
    return urls;
  },

  GetFanyUrl: function(aktualisIntezetAzon, fegyelmiUgy) {
    function addDays(date, days) {
      var result = new Date(date);
      result.setDate(result.getDate() + days);
      return result;
    }

    var fanyBaseUrl =
      store.getters[BeallitasokStoreTypes.getters.getFanyBaseUrl];
    var baseUrl = fanyBaseUrl + '/Pages/Funkciok/JFKAlap.aspx?JFK=true';

    baseUrl += '&AktualisIntezetAzon=' + aktualisIntezetAzon;
    baseUrl += '&AktualisFogvatartottId=' + fegyelmiUgy.FogvatartottId;
    baseUrl += '&AktualisFegyelmiUgyId=' + fegyelmiUgy.FegyelmiUgyId;

    var urls = [];

    if (
      new Set([
        '01',
        '04',
        '08',
        '10',
        '11',
        '12',
        '16',
        '17',
        '18',
        '21',
        '22',
        '23',
        '24',
        '30',
        '31',
      ]).has(fegyelmiUgy.NyilvantartottStatuszAzon) &&
      new Set(['07', '08', '17', '21', '25', '26', '2A']).has(
        fegyelmiUgy.UgyStatuszAzon
      )
    ) {
      urls.push({
        title: 'Kivizsgálás rögzítése',
        url:
          baseUrl +
          '&oldal=FegyelmiUgyekKivizsgalasaKivizsgalasRogzitese&jogosultsag=AFfukKr',
      });
    }

    if (
      new Set(['01', '04', '08', '10', '11', '12', '16', '17', '18', '31']).has(
        fegyelmiUgy.NyilvantartottStatuszAzon
      ) &&
      new Set(['25']).has(fegyelmiUgy.UgyStatuszAzon)
    ) {
      urls.push({
        title: 'Tárgyalás előkészítése vagy eljárás felfüggesztése',
        url:
          baseUrl +
          '&oldal=FegyelmiTargyalasElokesziteseEljarasFelfuggesztese&jogosultsag=AFFteJe',
      });
    }

    if (
      new Set([
        '01',
        '04',
        '08',
        '10',
        '11',
        '12',
        '16',
        '17',
        '18',
        '21',
        '22',
        '23',
        '24',
        '30',
        '31',
      ]).has(fegyelmiUgy.NyilvantartottStatuszAzon) &&
      (new Set(['06', '07', '12', '15', '24', '41', '42', '43']).has(
        fegyelmiUgy.UgyStatuszAzon
      ) ||
        (fegyelmiUgy.UgyStatuszAzon == '09' &&
          fegyelmiUgy.KezdemenyezesDatum.addDays(14) > new Date()))
    ) {
      urls.push({
        title: 'Eljárási mód kiválasztása',
        url:
          baseUrl +
          '&oldal=DontesFegyelmiUgyrolFegyelmiUgyModositas&jogosultsag=AFRekReF',
      });
    }

    if (
      new Set(['01', '04', '08', '10', '11', '12', '16', '17', '18', '31']).has(
        fegyelmiUgy.NyilvantartottStatuszAzon
      ) &&
      new Set(['08', '10', '24', '25', '26']).has(fegyelmiUgy.UgyStatuszAzon)
    ) {
      urls.push({
        title: 'Kivizsgálás határidő módosítási javaslat',
        url:
          baseUrl +
          '&oldal=FegyelmiTargyalasElokesziteseUjHataridoKituzese&jogosultsag=AFFteUhk',
      });
    }

    if (
      new Set(['01', '04', '08', '10', '11', '12', '16', '17', '18', '31']).has(
        fegyelmiUgy.NyilvantartottStatuszAzon
      ) &&
      new Set(['08', '10', '24', '25', '26']).has(fegyelmiUgy.UgyStatuszAzon)
    ) {
      urls.push({
        title: 'Fegyelmi tárgyalás előkészítése',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiUgyReszletek&jogosultsag=AFFteRe',
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['07', '08', '17', '21', '25', '26', '2A']).has(
        fegyelmiUgy.UgyStatuszAzon
      )
    ) {
      urls.push({
        title: 'Fegyelmi ügy kivizsgálása',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiUgyReszletek&jogosultsag=AFFukFuR',
      });
    }

    if (
      new Set(['01', '04', '08', '10', '11', '12', '16', '17', '18', '31']).has(
        fegyelmiUgy.NyilvantartottStatuszAzon
      ) &&
      new Set(['05', '10', '13', '28', '2F']).has(fegyelmiUgy.UgyStatuszAzon) &&
      (fegyelmiUgy.fegyelmiUgy ||
        new Set(['3', '6']).has(fegyelmiUgy.HatarozatJogkorAzon))
    ) {
      urls.push({
        title: 'Fegyelmi eljárás elsőfokon',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiUgyReszletek&jogosultsag=AFFeeR',
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['12', '13', '14', '28', '2B', '2D', '2G']).has(
        fegyelmiUgy.UgyStatuszAzon
      ) &&
      (fegyelmiUgy.fegyelmiUgy ||
        new Set(['3', '4']).has(fegyelmiUgy.FegyelmiMasodfokJogkorAzon))
    ) {
      urls.push({
        title: 'Fegyelmi eljárás másodfokon',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiUgyReszletek&jogosultsag=AFFemEdR',
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['02', '03', '14', '19', '20', '2B', '2D', '45', '2H']).has(
        fegyelmiUgy.UgyStatuszAzon
      ) &&
      !new Set(['04', '05', '08', '16', '17']).has(
        fegyelmiUgy.FenyitesStatuszAzon
      ) &&
      fegyelmiUgy.FenyitesId &&
      (fegyelmiUgy.FenyitesEljarasFoka == 2 ||
        fegyelmiUgy.FenyitesStatuszAzon == '2D')
    ) {
      urls.push({
        title: 'Bírósági felülvizsgálat fegyelmi ügyben',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiUgyReszletek&jogosultsag=AFTdfuR',
      });
    }

    if (
      new Set(['01', '04', '08', '10', '11', '12', '16', '17', '18', '31']).has(
        fegyelmiUgy.NyilvantartottStatuszAzon
      )
    ) {
      urls.push({
        title: 'Feljelentés kezdeményezése',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiUgyReszletek&jogosultsag=AFFkFuR',
      });
    }

    if (
      new Set(['01', '04', '08', '10', '11', '12', '16', '17', '18', '31']).has(
        fegyelmiUgy.NyilvantartottStatuszAzon
      ) &&
      new Set(['15']).has(fegyelmiUgy.UgyStatuszAzon)
    ) {
      urls.push({
        title: 'Fegyelmi ügy intézés reintegrációs jogkörben',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiUgyReszletek&jogosultsag=AFFknjR',
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['13']).has(fegyelmiUgy.FenyitesStatuszAzon) &&
      new Set(['2', '3']).has(fegyelmiUgy.FenyitesTipusAzon)
    ) {
      urls.push({
        title: 'Fenyítés elévülése',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiFenyitesReszletek&jogosultsag=AFFeEFeR&FunkcioMod=0&AktualisFegyelmiFenyitesId=' +
          fegyelmiUgy.FenyitesId,
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['01', '03', '13']).has(fegyelmiUgy.FenyitesStatuszAzon) &&
      new Set(['2']).has(fegyelmiUgy.FenyitesTipusAzon)
    ) {
      urls.push({
        title: 'Kiétkezés csökkentés végrehajtása',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiFenyitesReszletek&jogosultsag=AFFKcsFfR&AktualisFegyelmiFenyitesId=' +
          fegyelmiUgy.FenyitesId,
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['01', '03', '13']).has(fegyelmiUgy.FenyitesStatuszAzon) &&
      new Set(['3']).has(fegyelmiUgy.FenyitesTipusAzon)
    ) {
      urls.push({
        title: 'Magánelzárás végrehajtása',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiFenyitesReszletek&jogosultsag=AFFMeFfR&AktualisFegyelmiFenyitesId=' +
          fegyelmiUgy.FenyitesId,
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['01', '03', '13']).has(fegyelmiUgy.FenyitesStatuszAzon) &&
      new Set(['4', '5', '6', '7', '8']).has(fegyelmiUgy.FenyitesTipusAzon)
    ) {
      urls.push({
        title: 'Fenyítés végrehajtása',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiFenyitesReszletek&jogosultsag=AFFVegrRe&AktualisFegyelmiFenyitesId=' +
          fegyelmiUgy.FenyitesId,
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['02']).has(fegyelmiUgy.FenyitesStatuszAzon)
    ) {
      urls.push({
        title: 'Fenyítés törlése',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiFenyitesReszletek&jogosultsag=AFFVegrTo&AktualisFegyelmiFenyitesId=' +
          fegyelmiUgy.FenyitesId,
      });
    }

    if (
      parseInt(fegyelmiUgy.NyilvantartottStatuszAzon) < 40 &&
      new Set(['13']).has(fegyelmiUgy.FenyitesStatuszAzon) &&
      new Set(['3']).has(fegyelmiUgy.FenyitesTipusAzon)
    ) {
      urls.push({
        title: 'Fenyítés végrehajthatatlanná tétele',
        url:
          baseUrl +
          '&oldal=FogvatartottAdatai/FogvatartottAdataiFegyelmiFenyitesReszletek&jogosultsag=AFFeEFeR&FunkcioMod=1&AktualisFegyelmiFenyitesId=' +
          fegyelmiUgy.FenyitesId,
      });
    }

    //urls.push({
    //  title: "Kivizsgálás rögzítése",
    //  url: baseUrl + '&oldal=FegyelmiUgyekKivizsgalasaKivizsgalasRogzitese&jogosultsag=AFfukKr',
    //});

    return urls;

    //var urls =  [
    //  {
    //    page: 977,
    //    title: "",
    //    url: 'TODO 977',
    //    statusIds: new Set([])
    //  },
    //  {
    //    page: 975,
    //    title: "",
    //    url: 'TODO 975',
    //    statusIds: new Set([])
    //  },
    //  {
    //    page: 251,
    //    title: "Tárgyalás előkészítése vagy eljárás felfüggesztése",
    //    url: baseUrl + '&oldal=FegyelmiTargyalasElokesziteseEljarasFelfuggesztese&jogosultsag=AFFteJe',
    //    statusIds: new Set([
    //      Kodszotar.FegyelmiUgyStatusz.FelfuggesztesreJavasolva,
    //      Kodszotar.FegyelmiUgyStatusz.FelfuggesztveAFegyelmiEljaras,
    //      Kodszotar.FegyelmiUgyStatusz.KituzomAFegyelmiTargyalasIdopontjat,
    //      Kodszotar.FegyelmiUgyStatusz.KivizsgalasiHataridoModositasiJavaslat,
    //      Kodszotar.FegyelmiUgyStatusz.KivizsgalasBefejezve
    //    ])
    //  },
    //  {
    //    page: 262,
    //    title: "Kivizsgálás rögzítése",
    //    url: baseUrl + '&oldal=FegyelmiUgyekKivizsgalasaKivizsgalasRogzitese&jogosultsag=AFfukKr',
    //    statusIds: new Set([
    //      Kodszotar.FegyelmiUgyStatusz.ElrendelemAzEljarasLefolytatasat,
    //      Kodszotar.FegyelmiUgyStatusz.FelfuggesztesreJavasolva,
    //      Kodszotar.FegyelmiUgyStatusz.KivizsgalasiHataridoModositasiJavaslat,
    //      Kodszotar.FegyelmiUgyStatusz.KivizsgalasBefejezve,
    //      Kodszotar.FegyelmiUgyStatusz.KivizsgalasFolyamatban,
    //      Kodszotar.FegyelmiUgyStatusz.SzakteruletiVelemenyElkeszult,
    //      Kodszotar.FegyelmiUgyStatusz.SzakteruletiVelemenytKerek,
    //    ])
    //  },
    //  {
    //    page: 151,
    //    title: "Döntés az ügyről vagy ügy módosítás",
    //    url: baseUrl + '&oldal=DontesFegyelmiUgyrolFegyelmiUgyModositas&jogosultsag=AFRekReF',
    //    statusIds: new Set([
    //      Kodszotar.FegyelmiUgyStatusz.ElrendelemAzEljarasLefolytatasat,
    //      Kodszotar.FegyelmiUgyStatusz.FelfuggesztveAFegyelmiEljaras,
    //      Kodszotar.FegyelmiUgyStatusz.KeremAzEljarasLefolytatasat,
    //      Kodszotar.FegyelmiUgyStatusz.MegszuntetveUjEljarasIndul,
    //      Kodszotar.FegyelmiUgyStatusz.MegtagadomEzEljarasElredeleset,
    //      Kodszotar.FegyelmiUgyStatusz.VisszakuldveEgyebOk,
    //      Kodszotar.FegyelmiUgyStatusz.VisszakuldveFogvatartottNemIsmeriEl,
    //      Kodszotar.FegyelmiUgyStatusz.VisszakuldveSulyosabbAzUgy
    //    ])
    //  },
    //  {
    //    page: 257,
    //    title: "Tárgyalás előkészítése vagy új határidő kitűzése",
    //    url: baseUrl + '&oldal=FegyelmiTargyalasElokesziteseUjHataridoKituzese&jogosultsag=AFFteUhk',
    //    statusIds: new Set([
    //      Kodszotar.FegyelmiUgyStatusz.FelfuggesztesreJavasolva,
    //      Kodszotar.FegyelmiUgyStatusz.FelfuggesztveAFegyelmiEljaras,
    //      Kodszotar.FegyelmiUgyStatusz.KituzomAFegyelmiTargyalasIdopontjat,
    //      Kodszotar.FegyelmiUgyStatusz.KivizsgalasiHataridoModositasiJavaslat,
    //      Kodszotar.FegyelmiUgyStatusz.KivizsgalasBefejezve,
    //    ])
    //  }
    //];

    //var list=urls.filter(x => x.statusIds.has(fegyelmiUgyStatuszKszId)).map(x => { return { url: x.url, title: x.title } })
    //return list;
  },
});

export default IFrameUrls;
