// Be van kapcsolva a típusellenőrzés. Ha gond van vele szedd ki ezt a két kommentet

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
import { JutalomUgyStoreTypes } from '../store/modules/jutalomugy';
import moment from 'moment';
import $ from 'jquery';

class JutalmiUgy {
  /**
   *
   * @param {Object[]} jutalomUgyek
   */
  GetJutalmiUgyMuveletekAsObject(jutalomUgyek) {
    //#region változók
    let statuszok = Cimkek.JutalomStatusz;
    let szakterulet = Cimkek.JutalmazasJogkor;
    // let felfuggesztve = jutalomUgyek.every(e => e.Felfuggesztve);
    // let nincsFelfuggesztve = jutalomUgyek.every(e => !e.Felfuggesztve);
    // let nincsElkulonitve = jutalomUgyek.every(e => !e.FegyelmiElkulonitesFL);
    let lezarva = jutalomUgyek.some((e) => e.Lezarva);

    let isMultiselect = jutalomUgyek.length > 1;

    let userInfo = store.getters[UserStoreTypes.getters.getUserInfo];
    let szakteruletId = -1;
    if (userInfo.Jogkor) {
      szakteruletId = userInfo.Jogkor.Id;
    }
    let reintTisztnekElkuldve = jutalomUgyek.every(
      (e) => e.StatuszId == statuszok.ReintegaciosTisztnekElkuldve
    );
    let osztalyvezetonekElkuldve = jutalomUgyek.every(
      (e) => e.StatuszId == statuszok.OsztalyvezetonekElkuldve
    );
    let parancsnoknakElkuldve = jutalomUgyek.every(
      (e) => e.StatuszId == statuszok.ParancsnoknakElkuldve
    );
    let osszevonva = jutalomUgyek.every(
      (e) => e.StatuszId == statuszok.Osszevonva
    );
    let engedelyezve = jutalomUgyek.every(
      (e) => e.StatuszId == statuszok.Engedelyezve
    );
    let elutasitva = jutalomUgyek.every(
      (e) => e.StatuszId == statuszok.Elutasitva
    );

    let isReintTiszt = szakteruletId == szakterulet.ReintegraciosTiszt;
    let isOsztalyvezeto = szakteruletId == szakterulet.Osztalyvezeto;
    let isParancsnok = szakteruletId == szakterulet.Parancsnok;

    let javaslatTevoSzemelySid = jutalomUgyek.every(
      (e) => e.JavaslatTevoSid == userInfo.SzemelyzetSid
    );

    let opcionalisUgyek = [];
    let muveletId = +new Date() - 100;
    let sorrendId = 0;

    //#endregion

    // if (
    //   (isElkulonitesElrendelve != true &&
    //     !feddes &&
    //     !haromNaposHatarido &&
    //     lezarva) ||
    //   jutalomUgyek.some(e => e.Fany)
    // ) {
    //   return null;
    // }

    //#region OpcionalisUgyek
    /**
     * @type {JutalomUgyMuvelet[]}
     */

    if (!lezarva && !osszevonva) {
      // opcionalisUgyek.push({
      //   Id: muveletId++,
      //   Text: 'Szabadszöveges naplóbejegyzés létrehozása',
      //   ColorClass: 'text-dark',
      //   ModalIdToOpen: 'szabadszoveges-naplobejegyzes-rogzitese',
      //   FunctionToRun: null,
      //   ModalType: null,
      //   Sorrend: 60,
      // });
      if (!isMultiselect) {
        if (reintTisztnekElkuldve && (isReintTiszt || isOsztalyvezeto)) {
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Javaslat vagy döntés',
            ColorClass: 'text-dark',
            ModalIdToOpen: 'jutalom-dontes-vagy-javaslat',
            FunctionToRun: null,
            ModalType: null,
            Sorrend: 80,
          });
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Összevonás',
            ColorClass: 'text-dark',
            ModalIdToOpen: 'jutalom-ugy-osszevonas',
            FunctionToRun: null,
            ModalType: null,
            Sorrend: 100,
          });
        }
        if (osztalyvezetonekElkuldve) {
          if (isOsztalyvezeto) {
            opcionalisUgyek.push({
              Id: muveletId++,
              Text: 'Javaslat vagy döntés',
              ColorClass: 'text-dark',
              ModalIdToOpen: 'jutalom-dontes-vagy-javaslat',
              FunctionToRun: null,
              ModalType: null,
              Sorrend: 80,
            });
          }
          if (isParancsnok) {
            opcionalisUgyek.push({
              Id: muveletId++,
              Text: 'Döntés',
              ColorClass: 'text-dark',
              ModalIdToOpen: 'jutalom-dontes-vagy-javaslat',
              FunctionToRun: null,
              ModalType: null,
              Sorrend: 80,
            });
          }
        }
        if (parancsnoknakElkuldve) {
          if (isParancsnok) {
            opcionalisUgyek.push({
              Id: muveletId++,
              Text: 'Döntés',
              ColorClass: 'text-dark',
              ModalIdToOpen: 'jutalom-dontes-vagy-javaslat',
              FunctionToRun: null,
              ModalType: null,
              Sorrend: 80,
            });
          }
        }

        if (javaslatTevoSzemelySid) {
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Téves rögzítés',
            ColorClass: 'text-dark',
            FunctionToRun: 'OpenJutalomVisszavonasConfirmModal',
            ModalType: null,
            Sorrend: 110,
          });
        }

        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Vélemény',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'jutalom-velemeny-megadasa',
          FunctionToRun: null,
          ModalType: null,
          Sorrend: 90,
        });
      }

      if (engedelyezve || elutasitva) {
        if (isReintTiszt || isOsztalyvezeto) {
          opcionalisUgyek.push({
            Id: muveletId++,
            Text: 'Kihirdetés',
            ColorClass: 'text-dark',
            FunctionToRun: 'OpenJutalomKihirdetesConfirmModal',
            ModalType: null,
            Sorrend: 100,
            DontShowSidePanel: true,
          });
        }
      } else {
        opcionalisUgyek.push({
          Id: muveletId++,
          Text: 'Véleményre felkérés',
          ColorClass: 'text-dark',
          ModalIdToOpen: 'jutalom-felkeres-velemenyezesre',
          FunctionToRun: null,
          ModalType: null,
          Sorrend: 70,
        });
      }
    }

    //#endregion
    opcionalisUgyek.sort(sortNumber('Sorrend'));
    if (opcionalisUgyek.length == 0) {
      return null;
    }
    return { opcionalisUgyek };
  }

  GetJutalomUgyekFromStore(ids) {
    let ugyek = store.getters[
      JutalomUgyStoreTypes.getters.getJutalomUgyek
    ].filter((f) => ids.some((s) => s == f.Id));
    return ugyek;
  }

  GetBootstrapJutalomUgyMuveletekMenu(jutalomUgy) {
    let jutalomId = jutalomUgy[0].JutalomId;
    let menuObj = this.GetJutalmiUgyMuveletekAsObject(jutalomUgy);
    if (!menuObj) {
      return null;
    }

    var dropdown = `
    <div class="dropdown">
    <button type="button" class="btn btn-icon btn-dark btn-outline btn-round dataTable-dropdown" id="dropdownMenu2${jutalomUgy}"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >
      <i class="icon wb-more-horizontal" aria-hidden="true"></i>
    </button>
    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenu2${jutalomUgy}">`;

    menuObj.opcionalisUgyek.forEach((element) => {
      let additionalClass = element.ColorClass || '';
      dropdown += `<button class="dropdown-item dt-jutalom-ugy-muvelet ${additionalClass}" ${
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
      NotificationFunctions.ErrorAlert(
        'Hiba',
        'A nyomtatvány letöltés nem sikerült'
      );
    });
  }
  GetSajatUgyeim(jutalomUgyek, user) {
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
    let sajatUgyeim = jutalomUgyek.filter((f) => {
      let fofelugyeloVagyok = false;
      if (f.Fofelugyelok) {
        fofelugyeloVagyok = f.Fofelugyelok.split(',').find((x) => x == userSid)
          ? true
          : false;
      }

      let reintegraciosFeltetel = false;
      let jogkorGyakFeltetel = false;

      return reintegraciosFeltetel || jogkorGyakFeltetel;
    });
    return sajatUgyeim;
  }
  GetVegrehajtasAlattiUgyek(jutalomUgyek) {
    return jutalomUgyek.filter(
      (f) =>
        f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.FenyitesVegrehajtasAlatt
    );
  }
  GetVegrehajtasraVaroUgyek(jutalomUgyek) {
    return jutalomUgyek.filter(
      (f) => f.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.FenyitesKiszabva
    );
  }
  GetKesesbenlevoUgyek(jutalomUgyek) {
    let now = moment().startOf('d').toDate();
    return jutalomUgyek.filter((f) => f.Hatarido && now > new Date(f.Hatarido));
  }
  GetHetenEsedekesUgyek(jutalomUgyek) {
    let now = new Date();
    let sameWeek = isSameWeek;
    return jutalomUgyek.filter(
      (f) => f.Hatarido && sameWeek(now, new Date(f.Hatarido))
    );
  }
  GetSzallitasraElojegyezveUgyek(jutalomUgyek) {
    return jutalomUgyek.filter((f) => f.SzallitasraElojegyezve);
  }
  async CanModalOpen(data, { fegyelmiUgyIds }) {
    return true;
  }

  // async GetKozvetitoiEljarasValidationData(dataFromModal) {
  //   var fegyelmi = store.getters[
  //     JutalomUgyStoreTypes.getters.getFegyelmiUgyById
  //   ](dataFromModal.FegyelmiUgyIds[0]);

  //   var resztvevo = await apiService.GetEsemenyResztvevoById({
  //     resztvevoId: dataFromModal.SertettId,
  //   });

  //   var elkoveto = await apiService.GetFogvatartottById({
  //     fogvatartottId: fegyelmi.FogvatartottId,
  //   });

  //   var sertett = await apiService.GetFogvatartottById({
  //     fogvatartottId: resztvevo.FogvatartottId,
  //   });

  //   var naplobejegyzesekAndEsemeny = await apiService.GetNaploBejegyzesekByFegyelmiUgyId(
  //     {
  //       fegyelmiUgyId: dataFromModal.FegyelmiUgyIds[0],
  //     }
  //   );

  //   var result = {
  //     Fegyelmi: fegyelmi,
  //     Sertett: sertett,
  //     Elkoveto: elkoveto,
  //     NaplobejegyzesekAndEsemeny: naplobejegyzesekAndEsemeny,
  //   };

  //   return result;
  // }

  // async CanKozvetitoiEljarasBeSaved(dataFromModal) {
  //   // lekérjuk a szükséges adatokat
  //   var warningMessage = 'Közvetítői eljárás nem kezdeményezhető, ha \n';
  //   var dataForValidation = await this.GetKozvetitoiEljarasValidationData(
  //     dataFromModal
  //   );
  //   // Elkövető vagy sértett letartóztatott
  //   var areResztvevokOk = true;
  //   if (
  //     dataForValidation.Elkoveto.ElozetesLetartoztatasId != null ||
  //     dataForValidation.Sertett.ElozetesLetartoztatasId != null
  //   ) {
  //     warningMessage += ' az elkövető vagy a sértett letartóztatott';
  //     areResztvevokOk = false;
  //   }

  //   // Buntető feljelentes naplobejegyzes =>
  //   var isNaploOk = true;
  //   dataForValidation.NaplobejegyzesekAndEsemeny.naplobejegyzesek.forEach(
  //     element => {}
  //   );

  //   if (!isNaploOk) {
  //     warningMessage += ' büntető feljelentés van felvéve az ügyben \n';
  //   }
  //   // fegyelmi vetség check
  //   var isFegyelmiVetsegOk = true;
  //   var vetsegNev = '';
  //   var vetsegek = Cimkek.FegyelmiVetsegTipusa;
  //   var tiltottFegyelmiVetsegek = [
  //     vetsegek.Verekedes,
  //     vetsegek.FogvatartottTarsBantalmazasa8NaponBelulGyogyulo,
  //     vetsegek.FogvatartottTarsBantalmazasa8NaponTulGyogyulo,
  //     vetsegek.Vesztegetes,
  //     vetsegek.Fogolyszokes,
  //     vetsegek.Fogolyzendules,
  //   ];
  //   var esemenyJellegCimkeId =
  //     dataForValidation.NaplobejegyzesekAndEsemeny.esemeny.JellegCimkeId;

  //   tiltottFegyelmiVetsegek.forEach(e => {
  //     if (esemenyJellegCimkeId == e) {
  //       isFegyelmiVetsegOk = false;
  //       switch (esemenyJellegCimkeId) {
  //         case 146:
  //           vetsegNev = 'verekedés';
  //           break;
  //         case 113:
  //           vetsegNev = 'fogvatartott bántalmazása';
  //           break;
  //         case 114:
  //           vetsegNev = 'fogvatartott bántalmazása';
  //           break;
  //         case 177:
  //           vetsegNev = 'vesztegetés';
  //           break;
  //         case 109:
  //           vetsegNev = 'fogolyszökés';
  //           break;
  //         case 112:
  //           vetsegNev = 'fogolyzendülés';
  //           break;
  //       }
  //       warningMessage += vetsegNev + ' a vétség típusa';
  //     }
  //   });

  //   var result = {
  //     warningMessage: warningMessage,
  //     canBeSaved: areResztvevokOk && isFegyelmiVetsegOk && isNaploOk,
  //   };
  //   return result;
  // }

  // async OpenJogiKepviseletModal(fegyelmiUgyIds, napoTipusCimkeId) {
  //   let serverResult = await apiService.VanNaploTipusAjutalomUgyeknek({
  //     fegyelmiUgyIds,
  //     napoTipusCimkeId,
  //   });
  //   if (serverResult.length == 0) {
  //     return true;
  //   }

  //   let result = await NotificationFunctions.ConfirmModal(
  //     `Fegyelmi ügyhöz már rögzítve lett jogi képviselet, javasolt az új felvétel helyett
  //     azt a naplóbejegyzést szerkeszteni. Vegyünk fel még egy azonos típusú
  //     naplóbejegyzést az alábbi ügyhöz: ${serverResult.join(', ')}?`,
  //     {
  //       title: 'Megerősítés',
  //     }
  //   );
  //   if (!result) {
  //     return false;
  //   }
  //   return true;
  // }

  GetJutalomUgyekByStatisztikaSzuro(jutalomUgyek, user, szuro) {
    switch (szuro) {
      case StatisztikaSzurok.HetenEsedekes:
        return this.GetHetenEsedekesUgyek(jutalomUgyek);
      case StatisztikaSzurok.Kesesben:
        return this.GetKesesbenlevoUgyek(jutalomUgyek);
      case StatisztikaSzurok.Nyitott:
        return jutalomUgyek;
      case StatisztikaSzurok.Sajat:
        return this.GetSajatUgyeim(jutalomUgyek, user);
      case StatisztikaSzurok.SzallitasraElojegyezve:
        return this.GetSzallitasraElojegyezveUgyek(jutalomUgyek);
      case StatisztikaSzurok.VegrehajtasAlatt:
        return this.GetVegrehajtasAlattiUgyek(jutalomUgyek);
      case StatisztikaSzurok.VegrehajtasraVaro:
        return this.GetVegrehajtasraVaroUgyek(jutalomUgyek);
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
  FegyelmiUgyTovabbiMezokKitoltese(jutalomUgyek) {
    let jutalomUgyekModositott = jutalomUgyek.map((v) => {
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
    return jutalomUgyekModositott;
  }
  GetJutalomSzuroBadgek(isBvop) {
    let props = [
      {
        propId: 'Statusz',
        propNev: 'Statusz',
        order: 2,
        mapObj: 'fo',
        class: 'badge-outline badge-success',
        tooltip: 'Státusz',
      },
      {
        propId: 'JutalmazasOka',
        propNev: 'JutalmazasOka',
        order: 3,
        mapObj: 'fo',
        class: 'badge-outline badge-info text-default',
        tooltip: 'Jutalmom oka',
      },
    ];
    if (isBvop) {
      props.push({
        propId: 'IntezetId',
        propNev: 'Intezet',
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
    eventBus.$emit('Sidebar:jutalmiUgyReszletek:close');
  }
}
export const JutalmiUgyFunctions = new JutalmiUgy();
