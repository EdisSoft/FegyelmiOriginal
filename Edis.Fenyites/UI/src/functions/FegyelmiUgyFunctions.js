import store from '../store';
import { UserStoreTypes, jogosultsagok } from '../store/modules/user';
import Cimkek from '../data/enums/Cimkek/index';
import ReintegraciosTisztDontesTipus from '../data/enums/reintegraciosTisztDontesTipus';
import {
  fileDownload,
  getAttachmentIcon,
  camelCaseString,
  safeGetProp,
  timeout,
} from '../utils/common';
import { NotificationFunctions } from './notificationFunctions';
import ModalTipus from '../data/enums/modalTipus';
import { dateSameDay, isSameWeek, addDays } from '../utils/date';
import StatisztikaSzurok from '../data/enums/statisztikaSzurok';
import { apiService, eventBus, jfkApp } from '../main';
import { AppStoreTypes } from '../store/modules/app';
import settings from '../data/settings';
import { sortNumber } from '../utils/sort';
import { FegyelmiUgyStoreTypes } from '../store/modules/fegyelmiugy';
import moment from 'moment';
import $ from 'jquery';

class FegyelmiUgy {
  /**
   *
   * @param {Object[]} fegyelmiUgyek
   */
  GetFegyelmiUgyMuveletekAsObject(fegyelmiUgyek) {
    //#region változók
    let fegyelmiUgyStatuszok = Cimkek.FegyelmiUgyStatusza;
    let fenyitesTipusok = Cimkek.FenyitesTipusa;
    let felfuggesztve = fegyelmiUgyek.every((e) => e.Felfuggesztve);
    let nincsFelfuggesztve = fegyelmiUgyek.every((e) => !e.Felfuggesztve);
    let nincsElkulonitve = fegyelmiUgyek.every((e) => !e.FegyelmiElkulonitesFL);
    let lezarva = fegyelmiUgyek.some((e) => e.Lezarva);

    let isMultiselect = fegyelmiUgyek.length > 1;

    let hasSertett = fegyelmiUgyek.every((e) => e.HasSertettFL);

    let eljarasAlaVontMeghallgatva = fegyelmiUgyek.some(
      (e) => e.EljarasAlaVontatMeghallgattukFL == true
    );

    let vanJogosultsaga = store.getters[UserStoreTypes.getters.vanJogosultsaga];
    let vanJogkorGyakorloJoga =
      store.getters[UserStoreTypes.getters.vanJogkorGyakorloJoga];
    let vanReintegraciosTisztJoga =
      store.getters[UserStoreTypes.getters.vanReintegraciosTisztJoga];
    let vanEgyebSzakteruletJoga =
      store.getters[UserStoreTypes.getters.vanEgyebSzakteruletJoga];
    let vanFofelugyeloJoga =
      store.getters[UserStoreTypes.getters.vanFofelugyeloJoga];
    let userInfo = store.getters[UserStoreTypes.getters.getUserInfo];

    let vanJogkorGyakVagyReintjoga =
      vanReintegraciosTisztJoga || vanJogkorGyakorloJoga;
    let kezdemenyezve = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.Kezdemenyezve
    );
    let nemKezdemenyezve = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId != fegyelmiUgyStatuszok.Kezdemenyezve
    );
    let ugyFoka2 = fegyelmiUgyek.every((e) => e.UgyFoka == 2);
    let ugyFokaNull = fegyelmiUgyek.every((e) => e.UgyFoka == null);
    let ugyDontesKilencNaponBelul = fegyelmiUgyek.every((e) =>
      moment(e.DontesDatuma).add(9, 'day').isAfter(new Date())
    );
    let ugyHatarozatHatvanNaponBelul = fegyelmiUgyek.every((e) =>
      moment(e.HatarozatDatuma).add(60, 'day').isAfter(new Date())
    );
    let kivizsgalasFolyamatban = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.KivizsgalasFolyamatban
    );
    let elsofokuTargyalas = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.IFokuTargyalas
    );
    let kivizsgalasFolyamatbanVagyelsofokuTargyalas = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.KivizsgalasFolyamatban ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.IFokuTargyalas
    );
    let elsofokuTargyalasIdoponttal = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.IFokuTargyalas &&
        e.ElsofokuTargyalasIdopontja
    );
    let masodfokuTargyalasIdoponttal = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.IIFokuTargyalas &&
        e.MasodfokuTargyalasIdopontja
    );
    let hatalyonKivulHelyezheto = fegyelmiUgyek.every(
      (e) =>
        e.HatarozatJogerosFl &&
        e.Lezarva == true &&
        e.UgyStatuszId != fegyelmiUgyStatuszok.Megszuntetve &&
        e.UgyStatuszId != fegyelmiUgyStatuszok.HatalyonKivulHelyezve &&
        e.UgyStatuszId != fegyelmiUgyStatuszok.FogvatartottKioktatva
    );

    let kozvetitoiEljarasAlatt = fegyelmiUgyek.every(
      (e) => e.KozvetitoiEljarasban
    );
    let KozvetitoiEljarasKezdemenyezve = fegyelmiUgyek.every(
      (e) => e.KozvetitoiEljarasKezdemenyezve
    );
    let KozvetitoiEljarasNincsKezdemenyezve = fegyelmiUgyek.every(
      (e) => !e.KozvetitoiEljarasKezdemenyezve
    );
    let hasBuntetoFeljelentes = fegyelmiUgyek.every((e) => e.FeljelentesFL);

    let reintegraciosTisztiJogkorben = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.ReintegraciosTisztiJogkorben
    );
    let kezdemenyezveVagyReintVagyKivFolyamatban = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.Kezdemenyezve ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.ReintegraciosTisztiJogkorben ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.KivizsgalasFolyamatban
    );
    let reintVagyKivFolyamatban = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.ReintegraciosTisztiJogkorben ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.KivizsgalasFolyamatban
    );

    let dontesDatumaTargynap = fegyelmiUgyek.every((e) =>
      dateSameDay(new Date(e.DontesDatuma), new Date())
    );

    let reintVagykivizsFolyVagyElsoFokTargyVagyMasodFokTargy =
      fegyelmiUgyek.every(
        (e) =>
          e.UgyStatuszId == fegyelmiUgyStatuszok.ReintegraciosTisztiJogkorben ||
          e.UgyStatuszId == fegyelmiUgyStatuszok.KivizsgalasFolyamatban ||
          e.UgyStatuszId == fegyelmiUgyStatuszok.IFokuTargyalas ||
          e.UgyStatuszId == fegyelmiUgyStatuszok.IIFokuTargyalas
      );
    let nemBVBiroiJogkorben = fegyelmiUgyek.every(
      (e) => e.HatarozatHozoJogkoreCimkeId != 251
    );

    let rogzitettMegallapodasEsHatarido = fegyelmiUgyek.every(
      (e) => e.KozvetitoiGaranciaHatarido != null
    );

    let garanciaTeljesultFl = fegyelmiUgyek.every((e) => e.GaranciaTeljesultFl);

    let garanciaTeljesultNincsMegadva = fegyelmiUgyek.every(
      (e) => e.GaranciaTeljesultFl == null
    );

    let garanciaTeljesultKiVanToltve = fegyelmiUgyek.every(
      (e) => e.GaranciaTeljesultFl != null
    );

    let garanciaNemTeljesultFl = fegyelmiUgyek.every(
      (e) => e.GaranciaTeljesultFl == false
    );
    let garanciaTeljesultNincsKitoltve = fegyelmiUgyek.every(
      (e) => e.GaranciaTeljesultFl == null
    );

    let isElkulonitesElrendelve = fegyelmiUgyek.every(
      (e) => e.FegyelmiElkulonitesFL
    );
    let alkalomVanMegadva = fegyelmiUgyek.every(
      (e) =>
        e.FenyitesHosszaMennyisegiEgysegCimkeId ==
        Cimkek.MennyisegiEgyseg.Alkalom
    );

    let reintVagyKivFolyVagyFenyKiszabVagyVegrAlatt = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.ReintegraciosTisztiJogkorben ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.KivizsgalasFolyamatban ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesKiszabva ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesVegrehajtasAlatt
    );

    // TODO: backend, KozvetitoiFL még nincs
    let kivizsgalasFolyamatbanVagykozvetitoiEljarasAlatt = fegyelmiUgyek.every(
      (e) =>
        e.KozvetitoiFL ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.KivizsgalasFolyamatban
    );
    let masodfokuTargyalas = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.IIFokuTargyalas
    );
    var fenyitesKiszabva = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesKiszabva
    );
    var fenyitesKiszabvaVagyMegszakitva = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesKiszabva ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesVegrehajtasMegszakitva
    );
    var fenyitesKiszabvaVagyAlattVagyMegszakitva = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesKiszabva ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesVegrehajtasAlatt ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesVegrehajtasMegszakitva
    );

    let fenyitesVegrehajtasAlatt = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesVegrehajtasAlatt
    );
    let fenyitesVegrehajtasAlattVagyMegszakitva = fegyelmiUgyek.every(
      (e) =>
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesVegrehajtasAlatt ||
        e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesVegrehajtasMegszakitva
    );
    let jogikepviseletet = fegyelmiUgyek.every(
      (e) => e.VanJogiKepviselet || e.JogiKepviseletetKer
    );
    let hatarozatHozoJogkoreFegyelmiJogkorGyakorloja = fegyelmiUgyek.every(
      (e) =>
        e.HatarozatHozoJogkoreCimkeId ==
        Cimkek.HatarozatHozoJogkore.FegyelmiJogkorGyakorloja
    );
    let hatarozatHozoJogkoreParancsnok = fegyelmiUgyek.every(
      (e) =>
        e.HatarozatHozoJogkoreCimkeId == Cimkek.HatarozatHozoJogkore.Parancsnok
    );
    let fenyitesVegrehajtvaMaganelzarasban = fegyelmiUgyek.every(
      (e) => e.MaganelzarasbanVegrehajtottNapokSzama >= e.FenyitesHossza
    );

    let maganelzaras = fegyelmiUgyek.every(
      (e) => e.FenyitesTipusCimkeId == Cimkek.FenyitesTipusa.Maganelzaras
    );

    let letezikMaganelzarasVegeDatum = fegyelmiUgyek.every((e) =>
      e.MaganelzarasVegeDatum ? true : false
    );

    let maganelzarasDatumaTargynap = fegyelmiUgyek.every((e) =>
      dateSameDay(new Date(e.MaganelzarasVegeDatum), new Date())
    );

    let maganelzarasDatumTargynapUtan = fegyelmiUgyek.every(
      (e) => new Date(e.MaganelzarasVegeDatum) <= new Date()
    );

    let feddes = fegyelmiUgyek.every(
      (e) => e.FenyitesTipusCimkeId == Cimkek.FenyitesTipusa.Feddes
    );

    let fenyitesVegrehajtva = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.FenyitesVegrehajtva
    );

    let megszuntetes = fegyelmiUgyek.every(
      (e) => e.FenyitesTipusCimkeId == Cimkek.FenyitesTipusa.Megszuntetes
    );

    let megszuntetve = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.Megszuntetve
    );

    let hatalyonKivul = fegyelmiUgyek.every(
      (e) => e.UgyStatuszId == fegyelmiUgyStatuszok.hatalyonKivul
    );

    let haromNaposHatarido = fegyelmiUgyek.every(
      (e) =>
        moment(new Date()).format('YYYY.MM.DD HH:mm') <=
        moment(addDays(e.HatarozatDatuma, 3)).format('YYYY.MM.DD HH:mm')
    );

    let hatarozatotHozoSzemelySid = fegyelmiUgyek.every(
      (e) => e.HatarozatotHozoSzemelySid == userInfo.SzemelyzetSid
    );

    let kozvetitoiSikeresFL = fegyelmiUgyek.every((e) => e.KozvetitoiSikeresFL);

    let isBvopIntezet = userInfo.RogzitoIntezet.Id == 135;

    let opcionalisUgyek = [];
    let muveletId = +new Date() - 100;
    let sorrendId = 0;

    //#endregion

    if (
      /*(isElkulonitesElrendelve != true &&
        !feddes &&
        !megszuntetes &&
        lezarva) */
      hatalyonKivul ||
      fegyelmiUgyek.some((e) => e.Fany)
    ) {
      return null;
    }

    //#region OpcionalisUgyek
    /**
     * @type {FegyelmiUgyMuvelet[]}
     */

    if (nincsFelfuggesztve && kezdemenyezve && vanJogkorGyakorloJoga) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Döntés az ügy elrendeléséről',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'fegyelmi-ugy-elrendelese',
      });
    }
    if (
      reintVagyKivFolyamatban &&
      vanJogkorGyakorloJoga &&
      dontesDatumaTargynap
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Eljárási mód változtatása',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'fegyelmi-ugy-elrendelese',
      });
    }
    if (
      nincsFelfuggesztve &&
      nincsElkulonitve &&
      kezdemenyezve &&
      vanJogkorGyakorloJoga
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Megtagadom az elrendelését',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'eljaras-megtagadasa',
      });
    }

    if (
      nincsFelfuggesztve &&
      (kivizsgalasFolyamatban || reintegraciosTisztiJogkorben)
    ) {
      if (vanReintegraciosTisztJoga) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Felfüggesztési javaslat',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'javaslat-felfuggesztesre',
          Sorrend: 100,
        });
      }
    }

    if (nincsFelfuggesztve && kivizsgalasFolyamatban) {
      if (
        vanReintegraciosTisztJoga ||
        vanEgyebSzakteruletJoga ||
        vanJogkorGyakorloJoga
      ) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Szakterületi vélemény',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'szakteruleti-velemeny',
          Sorrend: 40,
        });
      }
      if (vanReintegraciosTisztJoga) {
        if (!isMultiselect) {
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Meghallgatási jegyzőkönyv',
            ColorClass: 'text-dark',
            Sorrend: 1,
            ModalIdToOpen: 'jegyzokonyv-tanu-meghallgatas',
          });
        }

        if (
          fegyelmiUgyek.length == 1 &&
          eljarasAlaVontMeghallgatva &&
          vanJogkorGyakVagyReintjoga
        ) {
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Összefoglaló jelentés',
            ColorClass: 'text-dark font-weight-bold',
            ModalIdToOpen: 'osszefoglalo-jelentes',
            Sorrend: 1100,
          });
        }
        if (vanReintegraciosTisztJoga) {
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Helyszíni szemle',
            ColorClass: 'text-dark',
            ModalIdToOpen: 'helyszini-szemle',
            Sorrend: 50,
          });
        }

        if (vanJogkorGyakVagyReintjoga) {
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Személyi állományi tanú meghallgatás',
            ColorClass: 'text-dark',
            ModalIdToOpen: 'jegyzokonyv-szemelyi-allomanyi-tanu-meghallgatas',
            Sorrend: 20,
          });
        }
      }
    }
    if (
      nincsFelfuggesztve &&
      kivizsgalasFolyamatbanVagyelsofokuTargyalas &&
      vanJogkorGyakVagyReintjoga &&
      !elsofokuTargyalas
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Szembesítés fogvatartottak között',
        Sorrend: 30,
        ColorClass: 'text-dark',
        ModalIdToOpen: 'szembesitesi-jegyzokonyv',
      });
    }
    if (
      nincsFelfuggesztve &&
      kivizsgalasFolyamatban &&
      vanJogkorGyakVagyReintjoga
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Szakterületi vélemény kérése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'szakteruleti-velemeny-kerese',
        Sorrend: 45,
      });
    }

    if (
      kivizsgalasFolyamatban &&
      felfuggesztve &&
      vanJogkorGyakorloJoga &&
      !kozvetitoiEljarasAlatt &&
      !isMultiselect
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Felfüggesztés megszüntetése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'felfuggesztes-megszuntetes',
        Sorrend: 80,
      });
    }

    if (nincsFelfuggesztve && elsofokuTargyalasIdoponttal) {
      if (vanJogkorGyakVagyReintjoga) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'I. fokú tárgyalási jegyzőkönyv',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'jegyzokonyv-elso-foku-targyalasi',
        });
      }
      if (vanJogkorGyakorloJoga && !isMultiselect) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Határozat rögzítése',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'hatarozat-rogzitese',
          ModalType: ModalTipus.HatarozatRogzitese,
        });
      }
    }
    if (
      vanJogkorGyakorloJoga &&
      hatarozatotHozoSzemelySid &&
      ugyHatarozatHatvanNaponBelul &&
      ((feddes && fenyitesVegrehajtva && !ugyFoka2 && !ugyFokaNull) ||
        (fenyitesKiszabva && !ugyFoka2 && !ugyFokaNull) ||
        masodfokuTargyalas ||
        (megszuntetve && megszuntetes))
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Elsőfokú határozat javítása',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'hatarozat-rogzitese',
        ModalType: ModalTipus.HatarozatRogzitese,
      });
    }

    if (
      nincsFelfuggesztve &&
      masodfokuTargyalas &&
      vanJogkorGyakorloJoga &&
      nemBVBiroiJogkorben &&
      !lezarva
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'II. fokú fegyelmi tárgyalás előkészítése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'II-foku-fegyelmi-targyalas-elokeszitese',
      });
    }
    if (
      masodfokuTargyalas &&
      masodfokuTargyalasIdoponttal &&
      hatarozatHozoJogkoreParancsnok &&
      felfuggesztve != true &&
      !isMultiselect &&
      (vanReintegraciosTisztJoga || vanJogkorGyakorloJoga) &&
      !lezarva
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'II. fokú tárgyalási jegyzőkönyv',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'jegyzokonyv-masodfokutargyalasi',
      });
    }
    if (
      nincsFelfuggesztve &&
      masodfokuTargyalas &&
      !isMultiselect &&
      !lezarva
    ) {
      if (vanJogkorGyakorloJoga) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'II. fok: határozat helybenhagyása',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'hatarozat-rogzitese-masod-foku',
          ModalType: ModalTipus.HatarozatHelybenhagyasa,
        });
      }
      if (vanJogkorGyakorloJoga) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'II. fok: határozat módosítása',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'hatarozat-rogzitese-masod-foku',
          ModalType: ModalTipus.HatarozatModositasa,
        });
      }
    }

    if (
      vanJogkorGyakorloJoga &&
      hatarozatotHozoSzemelySid &&
      ugyHatarozatHatvanNaponBelul &&
      ((feddes && fenyitesVegrehajtva && ugyFoka2) ||
        (fenyitesKiszabva && ugyFoka2) ||
        hatalyonKivul)
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Másodfokú határozat javítása',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'hatarozat-rogzitese-masod-foku',
        ModalType: ModalTipus.HatarozatModositasa,
      });
    }

    if (vanJogkorGyakorloJoga && hatalyonKivulHelyezheto) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Ügyészi hatályon kívül helyezés',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'ugyeszi-hatalyon-kivul-helyezes',
        Sorrend: 70,
      });
    }

    if (
      nincsFelfuggesztve &&
      kivizsgalasFolyamatbanVagykozvetitoiEljarasAlatt
    ) {
      if (vanReintegraciosTisztJoga) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Határidő módosítási javaslat',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'javaslat-uj-hataridore',
          Sorrend: 60,
        });
      }
    }

    if (!lezarva) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Szabadszöveges naplóbejegyzés létrehozása',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'szabadszoveges-naplobejegyzes-rogzitese',
        Sorrend: 60,
      });
    }
    if (
      nincsFelfuggesztve &&
      reintegraciosTisztiJogkorben &&
      vanReintegraciosTisztJoga
    ) {
      if (eljarasAlaVontMeghallgatva) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Feddés',
          ColorClass: 'text-dark',
          Sorrend: 2,
          ModalIdToOpen: 'reintegracios-tiszt-dontese-feddes',
        });
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Kioktatás',
          ColorClass: 'text-dark',
          Sorrend: 3,
          ModalIdToOpen: 'reintegracios-tiszt-dontese-kioktatas',
        });
      }
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Visszaküldés',
        ColorClass: 'text-dark',
        Sorrend: 5,
        ModalIdToOpen: 'reintegracios-tiszt-dontese-visszakuldes',
      });
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Meghallgatási jegyzőkönyv',
        ColorClass: 'text-dark',
        Sorrend: 1,
        ModalIdToOpen: 'jegyzokonyv-tanu-meghallgatas',
      });
    }
    if (
      vanReintegraciosTisztJoga &&
      hatarozatotHozoSzemelySid &&
      ugyFokaNull &&
      !reintegraciosTisztiJogkorben &&
      ugyDontesKilencNaponBelul
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Reintegrációs tiszti határozat javítása',
        ColorClass: 'text-dark',
        Sorrend: 2,
        ModalIdToOpen: 'reintegracios-tiszt-dontese-feddes',
      });
    }
    if (nincsFelfuggesztve && elsofokuTargyalas) {
      if (vanJogkorGyakorloJoga) {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Visszaküldés kivizsgálásra',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'hatarido-modositas',
        });
        if (fegyelmiUgyek.length == 1) {
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Ügyek összevonása',
            ColorClass: 'text-dark',
            ModalIdToOpen: 'ugy-osszevonas',
          });
        } else {
          if (
            fegyelmiUgyek.every(
              (e) => e.FogvatartottId == fegyelmiUgyek[0].FogvatartottId
            )
          ) {
            for (var i in fegyelmiUgyek) {
              opcionalisUgyek.push({
                Id: muveletId++,
                Text: `Ügyek összevonása a ${fegyelmiUgyek[i].UgyIntezetAzon}/${fegyelmiUgyek[i].UgyEve}/${fegyelmiUgyek[i].UgySzama} ügy alatt`,
                ColorClass: 'text-dark',
                ModalIdToOpen: 'ugy-osszevonas',
                Options: {
                  foUgy: fegyelmiUgyek[i].FegyelmiUgyId,
                  alUgyek: fegyelmiUgyek
                    .filter(
                      (x) => x.FegyelmiUgyId != fegyelmiUgyek[i].FegyelmiUgyId
                    )
                    .map((x) => x.FegyelmiUgyId),
                },
              });
            }
          }
        }

        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'I. fokú fegyelmi tárgyalás előkészítése',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'I-foku-fegyelmi-targyalas-elokeszitese',
        });
      }
    }
    if (
      nincsFelfuggesztve &&
      reintVagykivizsFolyVagyElsoFokTargyVagyMasodFokTargy &&
      vanReintegraciosTisztJoga &&
      !lezarva
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Kirendelt védő kérése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'kirendelt-vedo-kerese',
        Sorrend: 103,
      });
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Meghatalmazott védő kérése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'meghatalmazott-vedo-kerese',
        Sorrend: 105,
      });
    }
    if (
      nincsFelfuggesztve &&
      (reintegraciosTisztiJogkorben || kivizsgalasFolyamatban) &&
      vanReintegraciosTisztJoga
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Kivizsgáló módosítása',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'fegyelmi-ugy-kivizsgalo-modositasa',
        Sorrend: 1030,
      });
    }

    if (nincsFelfuggesztve && vanJogkorGyakorloJoga && kivizsgalasFolyamatban) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Határidő módosítás',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'hatarido-modositas',
        Sorrend: 70,
      });
    }
    if (
      nincsFelfuggesztve &&
      vanJogkorGyakorloJoga &&
      (elsofokuTargyalasIdoponttal ||
        masodfokuTargyalasIdoponttal ||
        kivizsgalasFolyamatban ||
        elsofokuTargyalas) &&
      !lezarva
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Védő telefonos tájékoztatása',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'vedo-telefonos-tajekoztatasa',
      });
    }
    if (
      nincsFelfuggesztve &&
      (kivizsgalasFolyamatban ||
        reintegraciosTisztiJogkorben ||
        elsofokuTargyalas) &&
      vanJogkorGyakorloJoga
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Felfüggesztés',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'felfuggesztes',
        Sorrend: 101,
      });
    }
    if (
      jogikepviseletet &&
      vanJogkorGyakVagyReintjoga &&
      (kivizsgalasFolyamatban ||
        reintegraciosTisztiJogkorben ||
        elsofokuTargyalas ||
        masodfokuTargyalas ||
        kezdemenyezve)
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Jogi képviselet visszavonása',
        ColorClass: 'text-dark',
        ModalIdToOpen: null,
        FunctionToRun: 'OpenJogiKepviseletVisszavonasaConfirmModal',
        Sorrend: 120,
      });
    }
    if (
      nincsFelfuggesztve &&
      fenyitesKiszabvaVagyMegszakitva &&
      vanFofelugyeloJoga &&
      maganelzaras
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Magánelzárás megkezdésének rögzítése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'maganelzaras-megkezdesenek-rogzitese',
        Sorrend: 1000,
      });
    }
    if (
      nincsFelfuggesztve &&
      fenyitesVegrehajtasAlatt &&
      vanFofelugyeloJoga &&
      maganelzaras
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Magánelzárás megszakítása',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'maganelzaras-megszakitasa',
      });
    }
    if (
      nincsFelfuggesztve &&
      maganelzaras &&
      !fenyitesVegrehajtasAlatt &&
      fenyitesKiszabvaVagyAlattVagyMegszakitva &&
      vanJogkorGyakorloJoga
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Fenyítés nem végrehajtható',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'fenyites-vegrehajthatatlanna-tetele',
        Sorrend: 1000,
      });
    }
    if (
      nincsFelfuggesztve &&
      maganelzaras &&
      fenyitesKiszabva &&
      vanJogkorGyakorloJoga
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Magánelzárás ideiglenesen ellenjavallt',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'maganelzaras-ideiglenesen-ellenjavallt',
        Sorrend: 1000,
      });
    }
    if (
      (!letezikMaganelzarasVegeDatum ||
        (letezikMaganelzarasVegeDatum &&
          (maganelzarasDatumaTargynap || maganelzarasDatumTargynapUtan))) &&
      nincsFelfuggesztve &&
      fenyitesVegrehajtasAlatt &&
      vanFofelugyeloJoga &&
      maganelzaras
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Magánelzárás végrehajtva',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'maganelzaras-vegrehajtva',
        Sorrend: 1000,
      });
    }
    if (
      nincsFelfuggesztve &&
      reintVagyKivFolyVagyFenyKiszabVagyVegrAlatt &&
      vanJogkorGyakVagyReintjoga &&
      !ugyFoka2 &&
      KozvetitoiEljarasNincsKezdemenyezve &&
      hasSertett
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Közvetítői eljárás kezdeményezése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'kozvetitoi-eljaras-kezdemenyezese',
        Sorrend: 1000,
      });
    }
    if (
      nincsFelfuggesztve &&
      reintVagyKivFolyVagyFenyKiszabVagyVegrAlatt &&
      vanJogkorGyakorloJoga &&
      KozvetitoiEljarasKezdemenyezve
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Döntés közvetítői eljárásról',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'dontes-kozvetitoi-eljarasrol',
        Sorrend: 1000,
      });
    }
    if (vanJogkorGyakVagyReintjoga && kozvetitoiEljarasAlatt) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Feljegyzés megbeszélésről, megállapodás rögzítése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'feljegyzes-es-megallapodas',
        Sorrend: 1000,
      });
    }
    // if (
    //   nincsFelfuggesztve &&
    //   vanJogkorGyakVagyReintjoga &&
    //   kozvetitoiEljarasAlatt
    // ) {
    //   opcionalisUgyek.push({
    //     Id: muveletId++,
    //     Text: 'Feljegyzés megbeszélésről, megállapodás rögzítése',
    //     ColorClass: 'text-dark',
    //     ModalIdToOpen: 'feljegyzes-es-megallapodas',
    //     Sorrend: 1000,
    //   });
    // }
    if (
      //nincsFelfuggesztve &&
      vanJogkorGyakVagyReintjoga &&
      kozvetitoiEljarasAlatt &&
      rogzitettMegallapodasEsHatarido
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Garancia teljesülésének rögzítése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'garancia-teljesulesenek-rogzitese',
        Sorrend: 1000,
      });
    }
    if (
      (!lezarva || lezarva == null) &&
      (!kozvetitoiEljarasAlatt || kozvetitoiEljarasAlatt == null) &&
      !kivizsgalasFolyamatban &&
      felfuggesztve &&
      vanJogkorGyakorloJoga &&
      !isMultiselect
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Felfüggesztés megszüntetése',
        ColorClass: 'text-dark',
        ModalIdToOpen: null,
        FunctionToRun: 'OpenFelfuggesztesMegszunteteseConfirmModal',
        Sorrend: 120,
      });
    }
    if (
      vanJogkorGyakorloJoga &&
      kozvetitoiEljarasAlatt &&
      garanciaTeljesultKiVanToltve
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Közvetítői eljárás lezárása',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'kozvetitoi-eljaras-lezarasa',
        Sorrend: 1000,
      });
    }
    if (
      vanJogkorGyakorloJoga &&
      kozvetitoiEljarasAlatt &&
      garanciaTeljesultNincsMegadva
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Közvetítői eljárás megszüntetése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'indoklassal-megszuntetes',
        Sorrend: 1000,
      });
    }

    if (vanJogkorGyakorloJoga && kozvetitoiSikeresFL == true) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Közvetítői eljárás határozat rögzítése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'hatarozat-rogzitese',
        Sorrend: 1000,
        ModalType: ModalTipus.HatarozatMegszuntetese,
      });
    }
    if (
      vanJogkorGyakorloJoga &&
      (kezdemenyezveVagyReintVagyKivFolyamatban ||
        isElkulonitesElrendelve ||
        elsofokuTargyalas)
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Fegyelmi elkülönítés elrendelése, megszüntetése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'fegyelmi-elkulonites-elrendelese',
        Sorrend: 1000,
      });
      //   opcionalisUgyek.push({
      //     Id: muveletId++,
      //     Text: 'Fegyelmi elkülönítés végrehajtása',
      //     ColorClass: 'text-dark',
      //     ModalIdToOpen: 'fegyelmi-elkulonites-vegrehajtasa',
      //     Sorrend: 1000,
      //   });
    }
    if (
      vanJogkorGyakorloJoga &&
      ((!hasBuntetoFeljelentes && kezdemenyezveVagyReintVagyKivFolyamatban) ||
        hasBuntetoFeljelentes)
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Büntető feljelentés rögzítése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'bunteto-feljelentes-rogzitese',
        Sorrend: 1000,
      });
    }
    if (vanJogkorGyakVagyReintjoga && kozvetitoiEljarasAlatt) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Közvetítői eljárás határidő módosítás kérése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'kozvetitoi-eljaras-hatarido-modositas-kerese',
        Sorrend: 1000,
      });
    }
    if (
      vanJogkorGyakorloJoga &&
      kozvetitoiEljarasAlatt &&
      garanciaTeljesultNincsKitoltve
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Közvetítői eljárás határidő módosítás',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'kozvetitoi-eljaras-hatarido-modositas',
        Sorrend: 1000,
      });
    }
    // if (vanJogkorGyakVagyReintjoga && kozvetitoiEljarasAlatt) {
    //   opcionalisUgyek.push({
    //     Id: muveletId++,
    //     Text: 'Határidő módosítás kérése',
    //     ColorClass: 'text-dark',
    //     ModalIdToOpen: 'kozvetitoi-eljaras-hatarido-modositas-kerese',
    //     Sorrend: 1000,
    //   });
    // }

    if (
      vanJogkorGyakorloJoga &&
      fenyitesKiszabvaVagyMegszakitva &&
      maganelzaras
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Magánelzárás elrendelése',
        ColorClass: 'text-dark',
        ModalIdToOpen: 'maganelzaras-elrendelese',
        Sorrend: 1000,
      });
    }
    if (
      vanJogkorGyakorloJoga &&
      fenyitesVegrehajtasAlattVagyMegszakitva &&
      alkalomVanMegadva
    ) {
      opcionalisUgyek.push({
        Id: muveletId++,
        Text: 'Fenyítés végrehajtása kész',
        ColorClass: 'text-dark',
        FunctionToRun: 'OpenFenyitesVegrahajtasaKeszConfirmModal',
        Sorrend: 1000,
      });
    }

    //#endregion
    opcionalisUgyek.sort(sortNumber('Sorrend'));
    if (opcionalisUgyek.length == 0 || isBvopIntezet) {
      return null;
    }
    return { opcionalisUgyek };
  }
  GetEsemenyMuveletekAsObject(fegyelmiUgyek) {
    //#region változók
    let opcionalisUgyek = [];
    let muveletId = +new Date() - 100;
    let sorrendId = 0;

    //#endregion

    // if (
    //   (isElkulonitesElrendelve != true &&
    //     !feddes &&
    //     !megszuntetes &&
    //     lezarva) ||
    //   fegyelmiUgyek.some(e => e.Fany)
    // ) {
    //   return null;
    // }

    //#region OpcionalisUgyek
    /**
     * @type {FegyelmiUgyMuvelet[]}
     */
    opcionalisUgyek.push({
      Id: muveletId++,
      Text: 'Fegyelmi lap',
      ColorClass: 'text-dark',
      FunctionToRun: 'FegyelmiLapNyomtatas',
      Sorrend: 1000,
    });

    opcionalisUgyek.push({
      Id: muveletId++,
      Text: 'Jegyzőkönyv aláírás megtagadásáról',
      ColorClass: 'text-dark',
      FunctionToRun: 'AlairasMegtagadasaNyomtatas',
      Sorrend: 1000,
    });

    //#endregion
    opcionalisUgyek.sort(sortNumber('Sorrend'));
    if (opcionalisUgyek.length == 0) {
      return null;
    }
    return { opcionalisUgyek };
  }
  GetFegyelmiUgyekFromStore(ids) {
    let ugyek = store.getters[
      FegyelmiUgyStoreTypes.getters.getFegyelmiUgyek
    ].filter((f) => ids.some((s) => s == f.FegyelmiUgyId));
    return ugyek;
  }

  GetBootstrapFegyelmiUgyMuveletekMenu(fegyelmiUgy) {
    let fegyelmiUgyId = fegyelmiUgy[0].FegyelmiUgyId;
    let menuObj = this.GetFegyelmiUgyMuveletekAsObject(fegyelmiUgy);
    if (!menuObj) {
      return null;
    }

    var dropdown = `
    <div class="dropdown">
    <button type="button" class="btn btn-icon btn-dark btn-outline btn-round dataTable-dropdown" id="dropdownMenu2${fegyelmiUgyId}"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >
      <i class="icon wb-more-horizontal" aria-hidden="true"></i>
    </button>
    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenu2${fegyelmiUgyId}">`;

    menuObj.opcionalisUgyek.forEach((element) => {
      let additionalClass = element.ColorClass || '';
      dropdown += `<button class="dropdown-item dt-fegyelmi-ugy-muvelet ${additionalClass}" ${
        element.ModalIdToOpen || element.FunctionToRun ? '' : 'disabled'
      } data-modal-id="${element.ModalIdToOpen || ''}" data-modal-tipus="${
        element.ModalType || ''
      }" data-function-to-run="${element.FunctionToRun || ''}" type="button"> ${
        element.Text
      } </button>`;
    });
    if (menuObj.opcionalisUgyek.length > 1) dropdown += '</div>';
    return dropdown;
  }

  GetBootstrapEsemenyMuveletekMenu(fegyelmiUgy) {
    let fegyelmiUgyId = fegyelmiUgy[0].FegyelmiUgyId;
    let menuObj = this.GetEsemenyMuveletekAsObject(fegyelmiUgy);
    if (!menuObj) {
      return null;
    }

    var dropdown = `
    <div class="dropdown">
    <button type="button" class="btn btn-icon btn-dark btn-outline btn-round dataTable-dropdown" id="dropdownMenu2${fegyelmiUgyId}"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >
      <i class="icon wb-more-horizontal" aria-hidden="true"></i>
    </button>
    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenu2${fegyelmiUgyId}">`;

    menuObj.opcionalisUgyek.forEach((element) => {
      let additionalClass = element.ColorClass || '';
      dropdown += `<button class="dropdown-item dt-fegyelmi-esemeny-muvelet ${additionalClass}" ${
        element.ModalIdToOpen || element.FunctionToRun ? '' : 'disabled'
      } data-modal-id="${element.ModalIdToOpen || ''}" data-modal-tipus="${
        element.ModalType || ''
      }" data-function-to-run="${element.FunctionToRun || ''}" type="button"> ${
        element.Text
      } </button>`;
    });
    if (menuObj.opcionalisUgyek.length > 1) dropdown += '</div>';
    return dropdown;
  }

  NyomtatvanyLetoltes(url) {
    fileDownload(url).catch(function (e) {
      var x = e.responseJSON;
      if (
        e.responseJSON.validationError == true &&
        e.responseJSON.errors.length > 0
      ) {
        NotificationFunctions.ErrorAlert('Hiba', e.responseJSON.errors[0].Text);
      } else {
        NotificationFunctions.ErrorAlert(
          'Hiba',
          'A nyomtatvány letöltés nem sikerült. '
        );
      }
    });
  }
  GetSajatUgyeim(fegyelmiUgyek, user) {
    if (!user || !user.Jogosultsagok) {
      return [];
    }
    let userSid = user.SzemelyzetSid;
    let isReintegracios = user.Jogosultsagok.some(
      (s) => s == jogosultsagok.fegyelmiReintegraciosTiszt
    );
    let isJogkorGyakorlo = user.Jogosultsagok.some(
      (s) => s == jogosultsagok.fegyelmiJogkorGyakorloja
    );
    let isFofelugyelo = user.Jogosultsagok.some(
      (s) => s == jogosultsagok.fofelugyelo
    );

    let isDateSameDay = dateSameDay;
    let now = new Date();
    let sajatUgyeim = fegyelmiUgyek.filter((f) => {
      let fofelugyeloVagyok = false;
      if (f.Fofelugyelok) {
        fofelugyeloVagyok = f.Fofelugyelok.split(',').find((x) => x == userSid)
          ? true
          : false;
      }

      let reintegraciosFeltetel =
        isReintegracios &&
        f.KivizsgaloSid == userSid &&
        (f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.KivizsgalasFolyamatban ||
          f.UgyStatuszId ==
            Cimkek.FegyelmiUgyStatusza.ReintegraciosTisztiJogkorben);

      let elsofokuTargyalasIdopontja = new Date(f.ElsofokuTargyalasIdopontja);
      let masodfokuTargyalasIdopontja = new Date(f.MasodfokuTargyalasIdopontja);
      let maganelzarasIdopontja = new Date(f.MaganelzarasVegeDatum);
      let maganelzarasElrendelesIdopontja = new Date(
        f.MaganelzarasElrendelesDatum
      );

      let jogkorGyakFeltetel =
        (isJogkorGyakorlo &&
          f.HatarozatHozoJogkoreCimkeId != Cimkek.HatarozatHozoJogkore.BVBiro &&
          (f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.Kezdemenyezve ||
            (f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IFokuTargyalas &&
              (!f.ElsofokuTargyalasIdopontja ||
                elsofokuTargyalasIdopontja.getTime() < now.getTime() ||
                isDateSameDay(now, elsofokuTargyalasIdopontja))) ||
            (f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IIFokuTargyalas &&
              (!f.MasodfokuTargyalasIdopontja ||
                masodfokuTargyalasIdopontja.getTime() < now.getTime() ||
                isDateSameDay(now, masodfokuTargyalasIdopontja))) ||
            (f.UgyStatuszId ==
              Cimkek.FegyelmiUgyStatusza.KivizsgalasFolyamatban &&
              (f.FelfuggesztesiJavaslat || f.HataridoModositasJavaslat)) ||
            (f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.FenyitesKiszabva &&
              f.FenyitesTipusCimkeId == Cimkek.FenyitesTipusa.Maganelzaras &&
              !f.MaganelzarasElrendelesDatum &&
              f.HatarozatotHozoSzemelySid == userSid)) &&
          f.HatarozatJogerosFl != 1) ||
        f.KozvetitoiEljarasKezdemenyezve == 1;

      let fofelugyeloFeltetel =
        isFofelugyelo &&
        f.FenyitesTipusCimkeId == Cimkek.FenyitesTipusa.Maganelzaras &&
        ((f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.FenyitesKiszabva &&
          isDateSameDay(now, maganelzarasElrendelesIdopontja) &&
          fofelugyeloVagyok) ||
          (f.UgyStatuszId ==
            Cimkek.FegyelmiUgyStatusza.FenyitesVegrehajtasAlatt &&
            isDateSameDay(now, maganelzarasIdopontja)));

      return reintegraciosFeltetel || jogkorGyakFeltetel || fofelugyeloFeltetel;
    });
    return sajatUgyeim;
  }
  GetVegrehajtasAlattiUgyek(fegyelmiUgyek) {
    return fegyelmiUgyek.filter(
      (f) =>
        f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.FenyitesVegrehajtasAlatt
    );
  }
  GetVegrehajtasraVaroUgyek(fegyelmiUgyek) {
    return fegyelmiUgyek.filter(
      (f) => f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.FenyitesKiszabva
    );
  }
  GetFegyelmiUgyIdsByEsemenyId(esemenyId) {
    let tesztUgyIds = store.getters[
      FegyelmiUgyStoreTypes.getters.getFegyelmiUgyek
    ]
      //.filter(f => f.EsemenyId == esemenyId)
      .map((m) => m.FegyelmiUgyId);
    console.log('storeban lévő ügyek:' + tesztUgyIds);
    let ugyIds = store.getters[FegyelmiUgyStoreTypes.getters.getFegyelmiUgyek]
      .filter((f) => f.EsemenyId == esemenyId)
      .map((m) => m.FegyelmiUgyId);
    return ugyIds;
  }
  GetKesesbenlevoUgyek(fegyelmiUgyek) {
    let now = moment().startOf('d').toDate();
    return fegyelmiUgyek.filter(
      (f) => f.Hatarido && now > new Date(f.Hatarido)
    );
  }
  GetHetenEsedekesUgyek(fegyelmiUgyek) {
    let now = new Date();
    let sameWeek = isSameWeek;
    return fegyelmiUgyek.filter(
      (f) => f.Hatarido && sameWeek(now, new Date(f.Hatarido))
    );
  }
  GetSzallitasraElojegyezveUgyek(fegyelmiUgyek) {
    return fegyelmiUgyek.filter((f) => f.SzallitasraElojegyezve);
  }
  async CanModalOpen(modalName, { fegyelmiUgyIds }) {
    switch (modalName) {
      case 'kirendelt-vedo-kerese':
        return await this.OpenJogiKepviseletModal(
          fegyelmiUgyIds,
          Cimkek.NaploTipus.KirendeltVedoKerese
        );
      case 'meghatalmazott-vedo-kerese':
        return await this.OpenJogiKepviseletModal(
          fegyelmiUgyIds,
          Cimkek.NaploTipus.MeghatalmazottVedoKerese
        );
    }
    return true;
  }

  async GetKozvetitoiEljarasValidationData(dataFromModal) {
    var fegyelmi = store.getters[
      FegyelmiUgyStoreTypes.getters.getFegyelmiUgyById
    ](dataFromModal.FegyelmiUgyIds[0]);

    var resztvevo = await apiService.GetEsemenyResztvevoById({
      resztvevoId: dataFromModal.SertettId,
    });

    var elkoveto = await apiService.GetFogvatartottById({
      fogvatartottId: fegyelmi.FogvatartottId,
    });

    var sertett = await apiService.GetFogvatartottById({
      fogvatartottId: resztvevo.FogvatartottId,
    });

    var naplobejegyzesekAndEsemeny =
      await apiService.GetNaploBejegyzesekByFegyelmiUgyId({
        fegyelmiUgyId: dataFromModal.FegyelmiUgyIds[0],
      });

    var result = {
      Fegyelmi: fegyelmi,
      Sertett: sertett,
      Elkoveto: elkoveto,
      NaplobejegyzesekAndEsemeny: naplobejegyzesekAndEsemeny,
    };

    return result;
  }

  async CanKozvetitoiEljarasBeSaved(dataFromModal) {
    // lekérjuk a szükséges adatokat
    var warningMessage = 'Közvetítői eljárás nem kezdeményezhető, ha \n';
    var dataForValidation = await this.GetKozvetitoiEljarasValidationData(
      dataFromModal
    );
    // Elkövető vagy sértett letartóztatott
    var areResztvevokOk = true;
    if (
      dataForValidation.Elkoveto.ElozetesLetartoztatasId != null ||
      dataForValidation.Sertett.ElozetesLetartoztatasId != null
    ) {
      warningMessage += ' az elkövető vagy a sértett letartóztatott';
      areResztvevokOk = false;
    }

    // Buntető feljelentes naplobejegyzes =>
    var isNaploOk = true;
    dataForValidation.NaplobejegyzesekAndEsemeny.naplobejegyzesek.forEach(
      (element) => {
        if (
          element.TipusCimkeId ==
          Cimkek.NaploTipus.BuntetoFeljelentesRogziteseNaploBejegyzes
        ) {
          isNaploOk = false;
        }
      }
    );

    if (!isNaploOk) {
      warningMessage += ' büntető feljelentés van felvéve az ügyben \n';
    }
    // fegyelmi vetség check
    var isFegyelmiVetsegOk = true;
    var vetsegNev = '';
    var vetsegek = Cimkek.FegyelmiVetsegTipusa;
    var tiltottFegyelmiVetsegek = [
      vetsegek.Verekedes,
      vetsegek.FogvatartottTarsBantalmazasa8NaponBelulGyogyulo,
      vetsegek.FogvatartottTarsBantalmazasa8NaponTulGyogyulo,
      vetsegek.Vesztegetes,
      vetsegek.Fogolyszokes,
      vetsegek.Fogolyzendules,
    ];
    var esemenyJellegCimkeId =
      dataForValidation.NaplobejegyzesekAndEsemeny.esemeny.JellegCimkeId;

    tiltottFegyelmiVetsegek.forEach((e) => {
      if (esemenyJellegCimkeId == e) {
        isFegyelmiVetsegOk = false;
        switch (esemenyJellegCimkeId) {
          case 146:
            vetsegNev = 'verekedés';
            break;
          case 113:
            vetsegNev = 'fogvatartott bántalmazása';
            break;
          case 114:
            vetsegNev = 'fogvatartott bántalmazása';
            break;
          case 177:
            vetsegNev = 'vesztegetés';
            break;
          case 109:
            vetsegNev = 'fogolyszökés';
            break;
          case 112:
            vetsegNev = 'fogolyzendülés';
            break;
        }
        warningMessage += vetsegNev + ' a vétség típusa';
      }
    });

    var result = {
      warningMessage: warningMessage,
      canBeSaved: areResztvevokOk && isFegyelmiVetsegOk && isNaploOk,
    };
    return result;
  }

  async OpenJogiKepviseletModal(fegyelmiUgyIds, napoTipusCimkeId) {
    let serverResult = await apiService.VanNaploTipusAFegyelmiUgyeknek({
      fegyelmiUgyIds,
      napoTipusCimkeId,
    });
    if (serverResult.length == 0) {
      return true;
    }

    let result = await NotificationFunctions.ConfirmModal(
      `Fegyelmi ügyhöz már rögzítve lett jogi képviselet, javasolt az új felvétel helyett 
      azt a naplóbejegyzést szerkeszteni. Vegyünk fel még egy azonos típusú 
      naplóbejegyzést az alábbi ügyhöz: ${serverResult.join(', ')}?`,
      {
        title: 'Megerősítés',
      }
    );
    if (!result) {
      return false;
    }
    return true;
  }

  GetFegyelmiUgyekByStatisztikaSzuro(fegyelmiUgyek, user, szuro) {
    switch (szuro) {
      case StatisztikaSzurok.HetenEsedekes:
        return this.GetHetenEsedekesUgyek(fegyelmiUgyek);
      case StatisztikaSzurok.Kesesben:
        return this.GetKesesbenlevoUgyek(fegyelmiUgyek);
      case StatisztikaSzurok.Nyitott:
        return fegyelmiUgyek;
      case StatisztikaSzurok.Sajat:
        return this.GetSajatUgyeim(fegyelmiUgyek, user);
      case StatisztikaSzurok.SzallitasraElojegyezve:
        return this.GetSzallitasraElojegyezveUgyek(fegyelmiUgyek);
      case StatisztikaSzurok.VegrehajtasAlatt:
        return this.GetVegrehajtasAlattiUgyek(fegyelmiUgyek);
      case StatisztikaSzurok.VegrehajtasraVaro:
        return this.GetVegrehajtasraVaroUgyek(fegyelmiUgyek);
      default:
        throw 'GetStatisztikaBySzuro - Hiányzó zsűrő';
        break;
    }
  }
  GetCsatolmanyInfo(csatolmanyok) {
    if (!csatolmanyok) {
      return [];
    }
    return csatolmanyok.map((m) => {
      let obj = {
        ...m,
        Icon: getAttachmentIcon(m.FileName),
        DownloadUrl:
          settings.baseUrlCsatolmanyKezelo +
          `Csatolmany/Get?uploadUrl=${encodeURI(m.Url)}`,
      };
      return obj;
    });
  }

  // propId: 11,
  // propNev: 'Rezsim',
  // order: 11,
  // mapObj: 'egyeb',
  // class: 'badge-outline badge-info text-default',
  // tooltip: 'Rezsim',
  CreateSzurok(arrayOfobj, szuroProps, selectedSzurok) {
    let szurok = new Map();
    arrayOfobj.forEach((obj) => {
      szuroProps.forEach((szuroProp) => {
        let element = {
          Id: obj[szuroProp.propId],
          Nev: obj[szuroProp.propNev],
          count: 0,
        };
        if (!element.Id || !element.Nev) {
          return;
        }

        element.szuro = { ...szuroProp };
        let key = element.szuro.propNev + element.Id;
        let mentettElement = szurok.get(key);
        if (!mentettElement) {
          let ujElement = { ...element };
          ujElement.count = 1;
          szurok.set(key, ujElement);
        } else {
          mentettElement.count++;
          szurok.set(key, mentettElement);
        }
      });
    });
    let szurokArr = Array.from(szurok.values());
    let hianyzoSzurok = selectedSzurok.filter(
      (f) => !szurokArr.some((s) => s.szuro.propNev + '' + s.Id == f.key)
    );

    hianyzoSzurok.forEach((element) => {
      szurokArr.push({ ...element.value });
    });

    return szurokArr;
  }
  ValidateSzurok(selectedSzurok, obj) {
    // Ha true, nem jelenik meg az elem a listában
    let tempBool = false;

    for (let i = 0; i < selectedSzurok.length; i++) {
      const selectedSzuro = selectedSzurok[i];
      let prefix = selectedSzuro.szuro.prefix;

      if (prefix) {
        tempBool = true;
        for (let i = 0; i < prefix.length; i++) {
          if (
            obj[prefix[i]][selectedSzuro.szuro.propNev] &&
            obj[selectedSzuro.szuro.propId] == selectedSzuro.Id
          ) {
            tempBool = false;
          }
        }

        if (tempBool) {
          return tempBool;
        }
      } else {
        if (obj[selectedSzuro.szuro.propId] != selectedSzuro.Id && !tempBool) {
          tempBool = true;
        }
      }
    }
    return tempBool;
  }
  FegyelmiUgyTovabbiMezokKitoltese(fegyelmiUgyek) {
    let fegyelmiUgyekModositott = fegyelmiUgyek.map((v) => {
      let elem = { ...v };

      // IIFokuTargyalas ügystátusz esetén nem kell megjelenjen a 'Tárgyalás kitűzésre vár' szűrőcímke
      if (
        v.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IFokuTargyalas &&
        !v.ElsofokuTargyalasIdopontja
      ) {
        elem.TargyalasKituzesreVar = 'Tárgyalás kitűzésre vár';
        elem.TargyalasKituzesreVarId = -1;
      }

      if (
        !elem.FenyitesTipusCimkeId &&
        elem.UgyStatuszId != Cimkek.FegyelmiUgyStatusza.Osszevonva
      ) {
        elem.FenyitesTipus = 'Nincs fenyítés kiszabva';
        elem.FenyitesTipusCimkeId = -1;
      }

      if (v.Felfuggesztve) {
        elem.FelfuggesztveSzoveg = 'Felfüggesztve';
        elem.FelfuggesztveSzovegId = -1;
      }

      return elem;
    });
    return fegyelmiUgyekModositott;
  }
  GetFegyelmiUgySzuroBadgek(isBvop) {
    let props = [
      {
        propId: 'UgyStatuszId',
        propNev: 'UgyStatusz',
        order: 2,
        mapObj: 'fo',
        class: 'badge-outline badge-success',
        tooltip: 'Ügy státusza',
      },
      {
        propId: 'TargyalasKituzesreVarId',
        propNev: 'TargyalasKituzesreVar',
        order: 3,
        mapObj: 'fo',
        class: 'badge-outline badge-warning text-default',
        tooltip: 'Tárgyalás kitűzésre vár',
      },
      {
        propId: 'FelfuggesztveSzovegId',
        propNev: 'FelfuggesztveSzoveg',
        order: 4,
        mapObj: 'fo',
        class: 'badge-outline badge-warning text-default',
        tooltip: 'Felfüggesztve',
      },
      {
        propId: 'FenyitesTipusCimkeId',
        propNev: 'FenyitesTipus',
        order: 5,
        mapObj: 'fo',
        class: 'badge-outline badge-info text-default',
        tooltip: 'Fenyítés típusa',
      },
    ];
    if (isBvop) {
      props.push({
        propId: 'FegyelmiIntezetId',
        propNev: 'FegyelmiIntezet',
        order: 1,
        mapObj: 'fo',
        class: 'badge-outline badge-default',
        tooltip: 'Intézet',
      });
    }
    return props;
  }
  async CloseModalsAndPanels() {
    // @ts-ignore
    eventBus.$emit('Modal:all:hide');
    // @ts-ignore
    eventBus.$emit('Sidebar:ugyReszletek:close');
  }
}
export const FegyelmiUgyFunctions = new FegyelmiUgy();
