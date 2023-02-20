import settings from '../data/settings';
import store from '../store';

import { UserStoreTypes } from '../store/modules/user';
import { FenyitesStoreTypes } from '../store/modules/fenyites';
import { FegyelmiUgyStoreTypes } from '../store/modules/fegyelmiugy';
import { EsemenyStoreTypes } from '../store/modules/esemeny';
import { AppStoreTypes } from '../store/modules/app';
import { KonalyticsInit, ka } from './../utils/konalytics';
import { GetSocketConnectionId } from '../utils/socketConnection';
import {
  GetGeoLocation,
  CorrectSummerAndWinterTimePeriod,
} from '../utils/common';

import moment from 'moment';
import { eventBus } from '../main';
import { BeallitasokStoreTypes } from '../store/modules/beallitasok';
import { FogvatartottakStoreTypes } from '../store/modules/fogvatartottak';
import { JutalomUgyStoreTypes } from '../store/modules/jutalomugy';

class ApiService {
  /**
   *
   * @param {import('../mock/mockHttpContext').HttpContext} http
   */

  constructor(http) {
    this.http = http;
  }

  async SendActivity(activitesArray) {
    var geoInfo = await GetGeoLocation();
    var userInfo = await store.getters[UserStoreTypes.getters.getUserInfo];
    var tempArr = [];
    if (!Array.isArray(activitesArray)) {
      activitesArray = [activitesArray];
    }
    for (var index in activitesArray) {
      var activity = {
        Datum: CorrectSummerAndWinterTimePeriod(activitesArray[index].Datum),
        LetrehozasDatuma: CorrectSummerAndWinterTimePeriod(new Date()),
        ModulCid: activitesArray[index].ModulCid,
        SzemelyId: activitesArray[index].SzemelyId,
        TevekenysegCid: activitesArray[index].TevekenysegCid,
        Sid: userInfo.SzemelyzetSid,
        IntezetId: userInfo.RogzitoIntezet.Id,
        TovabbiInfo: JSON.stringify(activitesArray[index].TovabbiInfo),
        Osszegzes: activitesArray[index].Osszegzes,
        GEO: JSON.stringify({
          longitude: geoInfo.longitude,
          latitude: geoInfo.latitude,
        }),
      };
      console.log(activity);
      tempArr.push(activity);
    }

    //sending the activities to the Fonix3
    await this.SendAct({ aktivitasDataList: tempArr });
  }

  async SendAct({ aktivitasDataList, mock = true } = {}) {
    var fonix3Url = await store.getters[AppStoreTypes.getters.getFonix3Url];
    var url = fonix3Url + 'Api/Aktivitas/SaveAktivitas';
    var data = { aktivitasDataList: aktivitasDataList };
    return await this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  async GetFogvatartottById({ fogvatartottId, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Fogvatartott/GetFogvatartottById';
    let data = { fogvatartottId: fogvatartottId };
    return await this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  async GetEsemenyResztvevoById({ resztvevoId, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Esemeny/GetEsemenyResztvevoById';
    let data = { resztvevoId: resztvevoId };
    return await this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  GetToolbarData({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Home/GetToolbarData';
    return this.http.post({ url, mock }).then((result) => {
      //store.dispatch(UserStoreTypes.actions.setUserInfo, {
      //  value: result.UserData,
      //});
      return result;
    });
  }
  GetUser({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Home/GetUser';
    return this.http.post({ url, mock }).then((result) => {
      if (result) {
        store.dispatch(UserStoreTypes.actions.setUserInfo, {
          value: result.UserData,
        });
      }
      return result;
    });
  }

  IntezetValtas({ intezetId, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Session/IntezetValtas';
    let data = { intezetId };
    return this.http
      .post({ data, url, mock })
      .then((result) => {
        if (!result) {
          return;
        }
        store.dispatch(UserStoreTypes.actions.setUserInfo, {
          value: result.UserData,
        });
        ka('event', {
          category: 'click',
          action: 'IntezetValtas',
          label: 'Intézet váltás - IntezetId: ' + intezetId,
        });
        return result;
      })
      .then(() => {
        eventBus.$emit('IntezetValasztas');
      });
  }

  async GetAppData({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Home/GetAppData';
    let result = await this.http.post({ url, mock });
    if (result) {
      await store.dispatch(UserStoreTypes.actions.setUserInfo, {
        value: result.UserData,
      });
      await store.dispatch(AppStoreTypes.actions.setDokumentumok, {
        value: result.Dokumentumok,
      });
      await store.dispatch(AppStoreTypes.actions.setVedoDokumentumok, {
        value: result.VedoDokumentumok,
      });
      await store.dispatch(
        BeallitasokStoreTypes.actions.setVirKimutatasFegyelmiUrl,
        {
          value: result.AlkalmazasBeallitasok.VirKimutatasFegyelmiUrl,
        }
      );
      await store.dispatch(
        BeallitasokStoreTypes.actions.setVirKimutatasJutalomUrl,
        {
          value: result.AlkalmazasBeallitasok.VirKimutatasJutalomUrl,
        }
      );
      await store.dispatch(BeallitasokStoreTypes.actions.setFanyBaseUrl, {
        value: result.AlkalmazasBeallitasok.FanyBaseUrl,
      });
      await store.dispatch(AppStoreTypes.actions.setFonix3Url, {
        value: result.AlkalmazasBeallitasok.FonixUrl,
      });
    }

    try {
      if (result.KonalyticsData) {
        KonalyticsInit(result.KonalyticsData);
      } else {
        console.log('Konalytics indításához nem érkeztek adatok a szervertől');
      }
    } catch (error) {
      console.log('Konalytics indítása sikertelen', error);
    }

    return result;
  }
  // GetFenyitesek({ mock = true } = {}) {
  //   if (store.getters[UserStoreTypes.getters.vanJogosultsaga]) {
  //     var url = settings.baseUrl + 'Api/Fenyites/GetFenyitesek';
  //     return this.http.post({ url, mock }).then(result => {
  //       if (result) {
  //         store.dispatch(FenyitesStoreTypes.actions.addFenyitesek, {
  //           value: Object.freeze(result),
  //         });
  //       }
  //       return result;
  //     });
  //   }
  // }
  async GetFogvatartottFegyelmiUgyei({ fogvatartottId, mock = true } = {}) {
    if (store.getters[UserStoreTypes.getters.vanJogosultsaga]) {
      var url =
        settings.baseUrl + 'Api/Fogvatartott/GetFogvatartottFegyelmiUgyei';

      let data = { fogvatartottId };
      try {
        let result = await this.http.post({ url, data, mock });
        console.log(result);
        return result;
      } catch (error) {
        console.log(error);
      }
    }
  }
  async GetIntezetiFogvatartottak({ data, mock = true } = {}) {
    if (store.getters[UserStoreTypes.getters.vanJogosultsaga]) {
      var url = settings.baseUrl + 'Api/Fogvatartott/GetIntezetiFogvatartottak';

      try {
        // store.dispatch(FogvatartottakStoreTypes.intezetiFogvatartottak.set, {
        //   value: [],
        // });
        let result = await this.http.post({ data, url, mock });
        // store.dispatch(FogvatartottakStoreTypes.intezetiFogvatartottak.set, {
        //   value: result,
        // });
        return result;
      } catch (error) {
        console.log(error);
      }
    }
  }
  GetRSALoginData({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Home/GetRSALoginData';
    return this.http.post({ url, mock }).then((result) => {
      return result;
    });
  }
  GetDokumentumokNyomtatva({ id, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Dokumentum/GetDokumentumLetoltesek';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyId: id,
      },
      mock,
    });
  }
  GetFenyitesekByIds({ ids, mock = true } = {}) {
    if (store.getters[UserStoreTypes.getters.vanJogosultsaga]) {
      var url = settings.baseUrl + 'Api/Fenyites/GetFenyitesekByFegyelmiUgyIds';
      return this.http
        .post({
          url,
          data: {
            fegyelmiUgyIds: ids,
          },
        })
        .then((result) => {
          store.dispatch(FenyitesStoreTypes.actions.addFenyitesek, {
            value: Object.freeze(result),
          });
          return result;
        });
    }
  }
  GetFanyFenyitesByFegyelmiUgyId({ id, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Fenyites/GetFanyFenyitesByFegyelmiUgyId';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyId: id,
      },
      mock,
    });
  }
  ChangeIktatottNyomtatvanyStatusz({ id, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/Nyomtatvany/ChangeIktatottNyomtatvanyStatusz';
    return this.http.post({
      url,
      data: {
        iktatottNyomtatvanyId: id,
      },
      mock,
    });
  }
  GetEsemenyAdatok({ id, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/Nyomtatvany/ChangeIktatottNyomtatvanyStatusz';
    return this.http.post({
      url,
      data: {
        iktatottNyomtatvanyId: id,
      },
      mock,
    });
  }
  async GetEsemenyek({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Esemeny/GetEsemenyek';
    await store.dispatch(EsemenyStoreTypes.actions.setEsemenyek, {
      value: Object.freeze([]),
    });
    let result = await this.http.post({ url, mock });
    await store.dispatch(EsemenyStoreTypes.actions.setEsemenyek, {
      value: Object.freeze(result),
    });
    return result;
  }
  // GetEsemenyek({ mock = true } = {}) {
  //   var url = settings.baseUrl + 'Api/Esemeny/GetEsemenyek';
  //   return this.http.post({
  //     url,
  //     data: {},
  //     mock,
  //   });
  // }
  GetEsemeny({ id, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Esemeny/GetEsemeny';
    return this.http.post({
      url,
      data: {
        esemenyId: id,
      },
      mock,
    });
  }
  GetEsemenyReszletek({ id, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Esemeny/GetEsemenyReszletek';
    return this.http.post({
      url,
      data: {
        esemenyId: id,
      },
      mock,
    });
  }
  AddResztvevo(data, { mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Esemeny/AddResztvevo';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }
  DeleteResztvevo(data, { mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Esemeny/DeleteResztvevo';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }
  FegyelmiUgyInditasa(data, { mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/FegyelmiUgyInditasa';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }
  SaveEsemeny({ values, mock = true }) {
    var url = settings.baseUrl + 'Api/Esemeny/SaveEsemeny';
    return this.http.post({
      url,
      data: values,
      mock,
    });
  }
  FindFogvatartottakForSelect({ data, mock = true }) {
    var url = settings.baseUrl + 'Api/Esemeny/FindFogvatartottakForSelect';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  FindFogvatartottakForSelectJutalom({ data, mock = true }) {
    var url = settings.baseUrl + 'Api/Jutalom/FindFogvatartottakForSelect';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  FindSzakteruletiVezetokForSelect({ data, mock = true }) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/FindSzakteruletiVezetokForSelect';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  async GetFegyelmiUgyek({ data, mock = true } = {}) {
    if (store.getters[UserStoreTypes.getters.vanJogosultsaga]) {
      await store.dispatch(FegyelmiUgyStoreTypes.actions.setFegyelmiUgyek, {
        value: Object.freeze([]),
      });
      var url = settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyek';
      let result = await this.http.post({
        url,
        data,
        mock,
      });
      if (result) {
        await store.dispatch(FegyelmiUgyStoreTypes.actions.setFegyelmiUgyek, {
          value: Object.freeze(result),
        });
        return result;
      }
    }
  }
  GetOsszevontFegyelmiUgyekForFegyelmiUgy({ fegyelmiUgyId, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetOsszevontFegyelmiUgyekForFegyelmiUgy';
    let data = {
      fegyelmiUgyId,
    };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetFegyelmiUgyListItemViewModelById({ fegyelmiUgyId, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyListItemViewModelById';
    let data = {
      fegyelmiUgyId,
    };
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetFegyelmiUgyTanuMeghallgatasiJegyzokonyv({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFegyelmiUgyTanuMeghallgatasiJegyzokonyv';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv({
    data,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetFegyelmiUgyElsoFokuTargyalasiJegyzokonyv({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFegyelmiUgyElsoFokuTargyalasiJegyzokonyv';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  // GetFegyelmiUgyHatarozatRogzitese({ data, mock = true } = {}) {
  //   var url =
  //     settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyHatarozatRogzitese';
  //   return this.http.post({
  //     url,
  //     data,
  //     mock,
  //   });
  // }
  NotifyUseresOnFegyelmiUgyValtozas({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/teszt/NotifyUseresOnFegyelmiUgyValtozas';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  SaveFegyelmiUgyTanuMeghallgatasiJegyzokonyv({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveFegyelmiUgyTanuMeghallgatasiJegyzokonyv';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveFegyelmiUgyUjResztvevo({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/SaveFegyelmiUgyUjResztvevo';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveFegyelmiUgyUjAllomanyiResztvevo({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveFegyelmiUgyUjAllomanyiResztvevo';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv({
    data,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }
  SaveFegyelmiUgyElsoFokuTargyalasiJegyzokonyv({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveFegyelmiUgyElsoFokuTargyalasiJegyzokonyv';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  GetFegyelmiUgyDataHelysziniSzemlehez({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyDataHelysziniSzemlehez';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds: fegyelmiUgyIds,
        naplobejegyzesIds: naplobejegyzesIds,
      },
      mock,
    });
  }
  GetKirendeltVedoKereseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetKirendeltVedoKereseModalData';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds: fegyelmiUgyIds,
        naplobejegyzesIds: naplobejegyzesIds,
      },
      mock,
    });
  }
  GetMeghatalmazottVedoKereseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetMeghatalmazottVedoKereseModalData';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds: fegyelmiUgyIds,
        naplobejegyzesIds: naplobejegyzesIds,
      },
      mock,
    });
  }

  FegyelmiUgyHelysziniSzemleMentes({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/FegyelmiUgyHelysziniSzemleMentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveKirendeltVedoKereseModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveKirendeltVedoKereseModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveMeghatalmazottVedoKereseModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveMeghatalmazottVedoKereseModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetFegyelmiUgyDataSzakteruletiVelemenyhez({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFegyelmiUgyDataSzakteruletiVelemenyhez';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds: fegyelmiUgyIds,
        naplobejegyzesIds: naplobejegyzesIds,
      },
      mock,
    });
  }

  FegyelmiUgySzakteruletiVelemenyMentes({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/FegyelmiUgySzakteruletiVelemenyMentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  TargyalasiIdopontKituzese({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveElsoFokuFegyelmiTargyalasElokeszitese';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveMasodFokuTargyalasKituzese({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveMasodFokuTargyalasKituzese';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetElsoFokuTargyalasElokeszitese({ id, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetElsoFokuFegyelmiTargyalasElokeszitese';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyId: id,
      },
      mock,
    });
  }
  GetMasodFokuFegyelmiTargyalasElokeszitese({ id, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetMasodFokuFegyelmiTargyalasElokeszitese';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyId: id,
      },
      mock,
    });
  }
  GetFegyelmiUgyDataElrendeleshez({ ids, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyDataElrendeleshez';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds: ids,
      },
      mock,
    });
  }
  GetFegyelmiUgyDataKivizsgaloModositashoz({ ids, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFegyelmiUgyDataKivizsgaloModositashoz';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds: ids,
      },
      mock,
    });
  }
  FegyelmiUgyKivizsgaloModositasaMentes({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/FegyelmiUgyKivizsgaloModositasaMentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetReintegraciosTisztDontesModalData({
    fegyelmiUgyIds,
    naploIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetReintegraciosTisztDontesModalData';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds: fegyelmiUgyIds,
        naploIds,
      },
      mock,
    });
  }
  ReintegraciosTisztDontesFeddesModalMentes({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/ReintegraciosTisztDontesFeddesModalMentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  ReintegraciosTisztDontesKioktatasModalMentes({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/ReintegraciosTisztDontesKioktatasModalMentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetEsemenyRogzitesModalData({ esemenyId, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Esemeny/GetEsemenyRogzitesModalData';
    let data = { esemenyId };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  EsemenyRogzitesModalMentes({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Esemeny/EsemenyRogzitesModalMentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  FegyelmiUgyElrendelemKivizsgalast({ data, mock = true }) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/ElrendelemKivizsgalast';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  FegyelmiUgyReintegraciosJogkorbe({ data, mock = true }) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/ReintegraciosJogkorbe';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetNaploBejegyzesekByFegyelmiUgyId({ fegyelmiUgyId, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/NaploBejegyzes/GetNaplobejegyzesekByFegyelmiUgyId';
    let data = { fegyelmiUgyId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        console.log(result);
        return result;
      });
  }
  FegyelmiLapNyomtatas({ iktatasId, mock = true }) {
    var url =
      settings.baseUrl + 'Api/Nyomtatvany/FegyelmiLapNyomtatvanyByIktatasId';
    let data = { iktatasId: iktatasId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  FegyelmiLapokNyomtatasaEsemenyRogzitesnel({ fegyelmiUgyIds, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/FegyelmiLapNyomtatvanyokForEsemenyRogzites';
    let data = { fegyelmiUgyIds: fegyelmiUgyIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  TajekoztatoNyomtatas({ fegyelmiUgyId, kerem, iktatasId, mock = true }) {
    var url = settings.baseUrl + 'Api/Nyomtatvany/TajekoztatoNyomtatvany';
    let data = { fegyelmiUgyId, kerem, iktatasId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetEljarasLefolytatasanakMegtagadasa({ fegyelmiUgyId, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetEljarasLefolytatasanakMegtagadasa';
    let data = { fegyelmiUgyId: fegyelmiUgyId };
    return this.http
      .post({
        url,
        data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  SaveEljarasLefolytatasanakMegtagadasa({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveEljarasLefolytatasanakMegtagadasa';
    return this.http
      .post({
        url,
        data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  UjHataridoModositasiJavaslat({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/UjHataridoModositasiJavaslat';
    return this.http
      .post({
        url,
        data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  //JUNG
  SzabadszovegesFegyelmiNaplobejegyzesFelvitele({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/UjFegyelmiNaplobejegyzesLetrehozasa';
    return this.http
      .post({
        url,
        data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  // JUNG END
  HatalyonKivulHelyezesRogzites({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/SaveHatalyonKivulHelyezes';
    return this.http
      .post({
        url,
        data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  UjFelfuggesztesiJavaslat({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/UjFelfuggesztesiJavaslat';
    return this.http
      .post({
        url,
        data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetFelfuggesztesiJavaslat({ fegyelmiUgyIds, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/GetFelfuggesztesiJavaslat';
    let data = { fegyelmiUgyIds };
    return this.http
      .post({
        url,
        data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  EljarasAlaVontMeghallgatasDokumentumAdatok({
    naplobejegyzesIds,
    iktatasIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/Dokumentum/EljarasAlaVontMeghallgatasDokumentumAdatok';
    return this.http
      .post({
        url,
        data: {
          naplobejegyzesIds,
          iktatasIds,
        },
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  TanuMeghallgatasDokumentumAdatok({
    naplobejegyzesIds,
    iktatasIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/Dokumentum/TanuMeghallgatasDokumentumAdatok';
    return this.http
      .post({
        url,
        data: {
          naplobejegyzesIds,
          iktatasIds,
        },
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok({
    naplobejegyzesIds,
    iktatasIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/Dokumentum/SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok';
    return this.http
      .post({
        url,
        data: {
          naplobejegyzesIds,
          iktatasIds,
        },
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  ElsoFokuTargyalasiDokumentumAdatok({
    naplobejegyzesIds,
    iktatasIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/Dokumentum/ElsoFokuTargyalasiDokumentumAdatok';
    return this.http
      .post({
        url,
        data: {
          naplobejegyzesIds,
          iktatasIds,
        },
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetOsszefoglaloJelentesModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetOsszefoglaloJelentesModalData';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds,
        naplobejegyzesIds,
      },
      mock,
    });
  }
  GetFegyelmiNaplobejegyzesSzerkeszteseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFegyelmiNaplobejegyzesSzerkesztese';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds,
        naplobejegyzesIds,
      },
      mock,
    });
  }
  GetUgyesziHatalyonKivulHelyezesModalData({
    fegyelmiUgyIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetUgyesziHatalyonKivulHelyezesModalData';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds,
      },
      mock,
    });
  }
  SaveOsszefoglaloJelentes({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/SaveOsszefoglaloJelentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetFegyelmiUgyDataOsszevonashoz({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyDataOsszevonashoz';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  FegyelmiUgyOsszevonasMentes({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/FegyelmiUgyOsszevonasMentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetReintegraciosTisztDontesVisszakuldesModalData({
    fegyelmiUgyIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetReintegraciosTisztDontesVisszakuldesModalData';
    let data = { fegyelmiUgyIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  ReintegraciosTisztDontesVisszakuldesModalMentes({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/ReintegraciosTisztDontesVisszakuldesModalMentes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  FegyelmiHatarozatNyomtatas({
    fegyelmiUgyId,
    iktatasId,
    naploId,
    mock = true,
  }) {
    var url = settings.baseUrl + 'Api/Nyomtatvany/FegyelmiHatarozatNyomtatvany';
    let data = { fegyelmiUgyId, iktatasId, naploId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  FegyelmiHatarozatMegszuntetesNyomtatas({
    fegyelmiUgyId,
    iktatasId,
    naploId,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/FegyelmiHatarozatMegszuntetesNyomtatvany';
    let data = { fegyelmiUgyId, iktatasId, naploId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  HatarozatReintegraciosNyomtatas({
    fegyelmiUgyId,
    iktatasId,
    naploId,
    mock = true,
  }) {
    var url =
      settings.baseUrl + 'Api/Nyomtatvany/HatarozatReintegraciosNyomtatvany';
    let data = { fegyelmiUgyId, iktatasId, naploId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  GetFegyelmiUgyHatarozatRogzitese({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyHatarozatRogzitese';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveFegyelmiUgyHatarozatRogzitese({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveFegyelmiUgyHatarozatRogzitese';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  VanNaploTipusAFegyelmiUgyeknek({
    fegyelmiUgyIds,
    napoTipusCimkeId,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/VanNaploTipusAFegyelmiUgyeknek';
    let data = { fegyelmiUgyIds, napoTipusCimkeId };
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetHataridoModositasModalData({ fegyelmiUgyIds, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetHataridoModositasModalData';
    let data = { FegyelmiUgyIds: fegyelmiUgyIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetUjHataridoModositasiJavaslatModalData({
    fegyelmiUgyIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetUjHataridoModositasiJavaslatModalData';
    let data = { FegyelmiUgyIds: fegyelmiUgyIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  SaveHataridoModositasModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveHataridoModositasModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetFelfuggesztesMegszuntetes({ ids, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/GetFelfuggesztesMegszuntetes';
    return this.http.post({
      url,
      data: {
        fegyelmiUgyIds: ids,
      },
      mock,
    });
  }

  SaveFelfuggesztesMegszuntetes({ data, mock = true }) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveFelfuggesztesMegszuntetes';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetFelfuggesztesModalData({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/GetFelfuggesztesModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  SaveFelfuggesztesModalData({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/SaveFelfuggesztesModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetVedoTelefonosTajekoztatasaModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetVedoTelefonosTajekoztatasaModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  SaveVedoTelefonosTajekoztatasaModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveVedoTelefonosTajekoztatasaModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  KioktatasReintegraciosJogkorbenNyomtatas({
    fegyelmiUgyId,
    iktatasId,
    naploId,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/KioktatasReintegraciosJogkorbenNyomtatvany';
    let data = { fegyelmiUgyId, iktatasId, naploId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  VedoKirendeleseNyilatkozatNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/VedoKirendeleseNyilatkozatNyomtatvanyByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  VedoKirendeleseNyilatkozatokNyomtatasByIktatasIds({
    iktatasIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/VedoKirendeleseNyilatkozatNyomtatvanyByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  // VedoKirendeleseNyomtatasByNaploIds({ naplobejegyzesIds, mock = true }) {
  //   var url =
  //     settings.baseUrl + 'Api/Nyomtatvany/VedoKirendeleseNyomtatvanyByNaploIds';
  //   let data = { naplobejegyzesIds };
  //   return this.http
  //     .post({
  //       url,
  //       data: data,
  //       mock,
  //     })
  //     .then(result => {
  //       return result;
  //     });
  // }

  // VedoKirendeleseNyomtatasByIktatasIds({ iktatasIds, mock = true }) {
  //   var url =
  //     settings.baseUrl +
  //     'Api/Nyomtatvany/VedoKirendeleseNyomtatvanyByIktatasIds';
  //   let data = { iktatasIds };
  //   return this.http
  //     .post({
  //       url,
  //       data: data,
  //       mock,
  //     })
  //     .then(result => {
  //       return result;
  //     });
  // }

  MeghatalmazottVedoKirendeleseNyilatkozatNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  MeghatalmazottVedoKirendeleseNyilatkozatokNyomtatasByIktatasIds({
    iktatasIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  // MeghatalmazottVedoKirendeleseNyomtatasByNaploIds({
  //   naplobejegyzesIds,
  //   mock = true,
  // }) {
  //   var url =
  //     settings.baseUrl +
  //     'Api/Nyomtatvany/MeghatalmazottVedoKirendeleseNyomtatvanyByNaploIds';
  //   let data = { naplobejegyzesIds };
  //   return this.http
  //     .post({
  //       url,
  //       data: data,
  //       mock,
  //     })
  //     .then(result => {
  //       return result;
  //     });
  // }

  // MeghatalmazottVedoKirendeleseNyomtatasByIktatasIds({
  //   iktatasIds,
  //   mock = true,
  // }) {
  //   var url =
  //     settings.baseUrl +
  //     'Api/Nyomtatvany/MeghatalmazottVedoKirendeleseNyomtatvanyByIktatasIds';
  //   let data = { iktatasIds };
  //   return this.http
  //     .post({
  //       url,
  //       data: data,
  //       mock,
  //     })
  //     .then(result => {
  //       return result;
  //     });
  // }
  GetSzakteruletiVelemenyKereseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetSzakteruletiVelemenyKereseModalData';
    let data = { fegyelmiUgyIds, naplobejegyzesIds };

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({
    //       MaxHatarido: moment()
    //         .add({ d: 10 })
    //         .toISOString(),
    //       SzakteruletiVezetokdefaultValue: [mockUsers[0]],
    //       Hatarido: moment()
    //         .add({ d: 2 })
    //         .toISOString(),
    //       Leiras: 'Mock',
    //     });
    //   }, 500);
    // });
    //-

    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  SaveSzakteruletiVelemenyKereseModalData({ data, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveSzakteruletiVelemenyKereseModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({ naplobejegyzesIds: [] });
    //   }, 500);
    // });
    //-

    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetMasodFokuTargyalasiJegyzokonyvModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetMasodFokuTargyalasiJegyzokonyvModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }

  SaveMasodFokuTargyalasiJegyzokonyvModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveMasodFokuTargyalasiJegyzokonyvModalData';

    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  VedoTelefonosTajekoztatasaNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/VedoTelefonosTajekoztatasaNyomtatvanyByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  VedoTelefonosTajekoztatasaNyomtatasByIktatasIds({ iktatasIds, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/VedoTelefonosTajekoztatasaNyomtatvanyByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetHatarozatRogziteseMasodfokonModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetHatarozatRogziteseMasodfokonModalData';

    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveHatarozatRogziteseMasodfokonModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveHatarozatRogziteseMasodfokonModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetSzembesitesiJegyzokonyvModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetSzembesitesiJegyzokonyvModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  SaveSzembesitesiJegyzokonyvModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveSzembesitesiJegyzokonyvModalData';

    return this.http.post({
      url,
      data: data,
      mock,
    });
    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({ naplobejegyzesIds: [] });
    //   }, 500);
    // });
    //-

    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetKozosNaploBejegyzesIds({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/NaploBejegyzes/GetKozosNaploBejegyzesIds';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  JogiKepviseletVisszavonasa({ fegyelmiUgyIds, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/JogiKepviseletVisszavonasa';
    let data = { fegyelmiUgyIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  FelfuggesztesMegszunteteseConfrimModal({ fegyelmiUgyIds, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveFelfuggesztesMegszunteteseConfrimModal';
    let data = { fegyelmiUgyIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetFegyelmiHatarido({ fegyelmiUgyIds, mock = true } = {}) {
    console.log('GetFegyelmiHatarido fegyelmiUGyIds', fegyelmiUgyIds);
    var url = settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiHatarido';
    let data = { fegyelmiUgyIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetMaganelzarasMegkezdesenekRogziteseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    maganelzarasVegeDatum,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetMaganelzarasMegkezdesenekRogziteseModalData';

    // Mock: start
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({
    //       // az id-t így kell generálni: objektumid_részlegid_zárkaid_ágyid, így is került visszaküldésre. Példa lent
    //       ZarkabaHelyezes: '1_2_2_1',
    //       ZarkabaHelyezesOptions: [
    //         {
    //           id: '1',
    //           label: 'Objektum 1',
    //           children: [
    //             {
    //               id: '1_1',
    //               label: 'Reszleg 1',
    //               children: [
    //                 {
    //                   id: '1_1_1',
    //                   label: 'Zarka 1',
    //                   children: [
    //                     {
    //                       id: '1_1_1_1',
    //                       label: 'Agy 1',
    //                     },
    //                   ],
    //                 },
    //               ],
    //             },
    //             {
    //               id: '1_2',
    //               label: 'Reszleg 2',
    //               children: [
    //                 {
    //                   id: '1_2_2',
    //                   label: 'Zarka 2',
    //                   children: [
    //                     {
    //                       id: '1_2_2_1',
    //                       label: 'Agy 1',
    //                     },
    //                   ],
    //                 },
    //               ],
    //             },
    //           ],
    //         },
    //         {
    //           id: '2',
    //           label: 'Objektum 2',
    //           children: [
    //             {
    //               id: '2_3',
    //               label: 'Reszleg 3',
    //               children: [
    //                 {
    //                   id: '2_3_3',
    //                   label: 'Zarka 3',
    //                   children: [
    //                     {
    //                       id: '2_3_3_1',
    //                       label: 'Agy 1',
    //                     },
    //                     {
    //                       id: '2_3_3_2',
    //                       label: 'Agy 2',
    //                     },
    //                     {
    //                       id: '2_3_3_3',
    //                       label: 'Agy 3',
    //                     },
    //                     {
    //                       id: '2_3_3_4',
    //                       label: 'Agy 4',
    //                     },
    //                   ],
    //                 },
    //               ],
    //             },
    //             {
    //               id: '2_4',
    //               label: 'Reszleg 4',
    //               children: [
    //                 {
    //                   id: '2_4_4',
    //                   label: 'Zarka 4',
    //                   children: [
    //                     {
    //                       id: '2_4_4_4',
    //                       label: 'Agy 4',
    //                     },
    //                   ],
    //                 },
    //               ],
    //             },
    //           ],
    //         },
    //       ],
    //       BehelyezesTenylegesIdeje: moment()
    //         .add({ d: 2 })
    //         .toISOString(),
    //     });
    //   }, 500);
    // });
    //-
    let data = { fegyelmiUgyIds, naplobejegyzesIds, maganelzarasVegeDatum };
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  SaveMaganelzarasMegkezdesenekRogziteseModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveMaganelzarasMegkezdesenekRogziteseModalData';

    // Mock: start
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({ naplobejegyzesIds: [] });
    //   }, 500);
    // });
    //-

    return this.http.post({
      url,
      data,
      mock,
    });
  }

  MaganelzarasMegkezdesenekRogziteseNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MaganelzarasMegkezdesenekRogziteseNyomtatasByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  MaganelzarasMegkezdesenekRogziteseNyomtatasByIktatasIds({
    iktatasIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MaganelzarasMegkezdesenekRogziteseNyomtatasByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  MaganelzarasMegkezdesenekRogziteseNyomtatasByFegyelmiUgyIds({
    fegyelmiUgyIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MaganelzarasMegkezdesenekRogziteseNyomtatasByFegyelmiUgyIds';
    let data = { fegyelmiUgyIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  // GetFenyitesVegrehajthatatlannaTeteleModalData({
  //   fegyelmiUgyIds,
  //   naplobejegyzesIds,
  //   mock = true,
  // } = {}) {
  //   var url =
  //     settings.baseUrl +
  //     'Api/FegyelmiUgy/GetFenyitesVegrehajthatatlannaTeteleModalData';

  //   // TODO: backend
  //   return new Promise((res, rej) => {
  //     setTimeout(() => {
  //       res({
  //         Leiras: 'Mock',
  //       });
  //     }, 500);
  //   });
  //   //-
  //   let data = { fegyelmiUgyIds, naplobejegyzesIds };
  //   return this.http.post({
  //     url,
  //     data,
  //     mock,
  //   });
  // }
  // SaveFenyitesVegrehajthatatlannaTeteleModalData({ data, mock = true } = {}) {
  //   var url =
  //     settings.baseUrl +
  //     'Api/FegyelmiUgy/SaveFenyitesVegrehajthatatlannaTeteleModalData';

  //   // TODO: backend
  //   return new Promise((res, rej) => {
  //     setTimeout(() => {
  //       res({ naplobejegyzesIds: [] });
  //     }, 500);
  //   });
  //   //-

  //   return this.http.post({
  //     url,
  //     data,
  //     mock,
  //   });
  // }
  GetMaganelzarasMegszakitasaModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetMaganelzarasMegszakitasaModalData';
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveMaganelzarasMegszakitasaModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveMaganelzarasMegszakitasaModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetMaganelzarasVegrehajtvaModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetMaganelzarasVegrehajtvaModalData';

    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  CheckMaganelzarasVegrehajtvaModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/CheckMaganelzarasVegrehajtvaModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveMaganelzarasVegrehajtvaModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveMaganelzarasVegrehajtvaModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetKozvetitoiEljarasKezdemenyezeseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetKozvetitoiEljarasKezdemenyezeseModalData';

    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveKozvetitoiEljarasKezdemenyezeseModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveKozvetitoiEljarasKezdemenyezeseModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetDontesKozvetitoiEljarasrolModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetDontesKozvetitoiEljarasrolModalData';

    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveDontesKozvetitoiEljarasrolModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveDontesKozvetitoiEljarasrolModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetFeljegyzesEsMegallapodasModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFeljegyzesEsMegallapodasModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res(
    //       {
    //         MaxDatum: moment()
    //           .add({ d: 5 })
    //           .toISOString(),
    //         ReintegraciosTisztOptions: [
    //           {
    //             id: 'S-1-5-21-3684209801-3913094555-2457220517-3674',
    //             text: 'Gábor Szigeti',
    //           },
    //           {
    //             id: '2',
    //             text: 'Nem Gábor Szigeti',
    //           },
    //         ],
    //         ReintegraciosTisztSid:
    //           'S-1-5-21-3684209801-3913094555-2457220517-3674',
    //         SertettKepviseloje: 'Mock 1',
    //         EljarasAlaVontKepviseloje: 'Mock 2',
    //         FeljegyzesMegbeszelesrol: 'Mock 3',
    //         Megallapodas: 'Mock 4',
    //         Hatarido: moment()
    //           .add({ d: 1 })
    //           .toISOString(),
    //         EljarasAlaVontatErintoKoltsegek: 0,
    //         SertettetErintoKoltsegek: null,
    //       },
    //       500
    //     );
    //   });
    // });
    //-
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveFeljegyzesEsMegallapodasModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveFeljegyzesEsMegallapodasModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({ naplobejegyzesIds: [] });
    //   }, 500);
    // });
    //-

    return this.http.post({
      url,
      data,
      mock,
    });
  }

  MasodfokuTargyalasiJegyzokonyvNyomtatatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MasodfokuTargyalasiJegyzokonyvNyomtatatvanyByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  MasodfokuTargyalasiJegyzokonyvNyomtatatasByIktatasIds({
    iktatasIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MasodfokuTargyalasiJegyzokonyvNyomtatatvanyByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetGaranciaTeljesulesenekRogziteseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res(
    //       {
    //         IsTeljesult: true,
    //       },
    //       500
    //     );
    //   });
    // });
    //-
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveGaranciaTeljesulesenekRogziteseModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({ naplobejegyzesIds: [] });
    //   }, 500);
    // });
    //-

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetKozvetitoiEljarasLezarasaModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetKozvetitoiEljarasLezarasaModalData';

    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveKozvetitoiEljarasLezarasaModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveKozvetitoiEljarasLezarasaModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetIndoklassalMegszuntetesModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetIndoklassalMegszuntetesModalData';

    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveIndoklassalMegszuntetesModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveIndoklassalMegszuntetesModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetFegyelmiElkulonitesElrendeleseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFegyelmiElkulonitesElrendeleseMegszuntetese';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res(
    //       {
    //         ElrendeloOptions: [
    //           {
    //             id: 'S-1-5-21-3684209801-3913094555-2457220517-3674',
    //             text: 'Gábor Szigeti',
    //           },
    //         ],
    //         ElrendeloId: 'S-1-5-21-3684209801-3913094555-2457220517-3674',
    //         ZarkabaHelyezes: '1_2_2_1',
    //         ZarkabaHelyezesOptions: [
    //           {
    //             id: '1',
    //             label: 'Objektum 1',
    //             children: [
    //               {
    //                 id: '1_1',
    //                 label: 'Reszleg 1',
    //                 children: [
    //                   {
    //                     id: '1_1_1',
    //                     label: 'Zarka 1',
    //                     children: [
    //                       {
    //                         id: '1_1_1_1',
    //                         label: 'Agy 1',
    //                       },
    //                     ],
    //                   },
    //                 ],
    //               },
    //               {
    //                 id: '1_2',
    //                 label: 'Reszleg 2',
    //                 children: [
    //                   {
    //                     id: '1_2_2',
    //                     label: 'Zarka 2',
    //                     children: [
    //                       {
    //                         id: '1_2_2_1',
    //                         label: 'Agy 1',
    //                       },
    //                     ],
    //                   },
    //                 ],
    //               },
    //             ],
    //           },
    //           {
    //             id: '2',
    //             label: 'Objektum 2',
    //             children: [
    //               {
    //                 id: '2_3',
    //                 label: 'Reszleg 3',
    //                 children: [
    //                   {
    //                     id: '2_3_3',
    //                     label: 'Zarka 3',
    //                     children: [
    //                       {
    //                         id: '2_3_3_1',
    //                         label: 'Agy 1',
    //                       },
    //                       {
    //                         id: '2_3_3_2',
    //                         label: 'Agy 2',
    //                       },
    //                       {
    //                         id: '2_3_3_3',
    //                         label: 'Agy 3',
    //                       },
    //                       {
    //                         id: '2_3_3_4',
    //                         label: 'Agy 4',
    //                       },
    //                     ],
    //                   },
    //                 ],
    //               },
    //               {
    //                 id: '2_4',
    //                 label: 'Reszleg 4',
    //                 children: [
    //                   {
    //                     id: '2_4_4',
    //                     label: 'Zarka 4',
    //                     children: [
    //                       {
    //                         id: '2_4_4_4',
    //                         label: 'Agy 4',
    //                       },
    //                     ],
    //                   },
    //                 ],
    //               },
    //             ],
    //           },
    //         ],

    //         ElkulonitesOka: 'Mock',
    //         ElkulonitesOkaSzerkesztheto: true,
    //         ElrendelesIdeje: moment()
    //           .add({ h: 1 })
    //           .toISOString(),
    //         ElrendelesIdejeSzerkesztheto: true,
    //         Rendelkezesek: 'Mock',
    //         IsFelulvizsgalva: true,
    //         MegszuntetesIdeje: moment()
    //           .add({ d: 1 })
    //           .toISOString(),
    //       },
    //       500
    //     );
    //   });
    // });
    //-
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  SaveFegyelmiElkulonitesElrendeleseModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveElkulonitesElrendelesEsMegszuntetese';
    return this.http.post({
      url,
      data,
      mock,
    });
  }

  MasodfokuFegyelmiHatarozatMegszunteteseNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  MasodfokuFegyelmiHatarozatMegszunteteseNyomtatasByIktatasIds({
    iktatasIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  MasodfokuFegyelmiHatarozatNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MasodfokuFegyelmiHatarozatNyomtatvanyByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        console.log(result);
        return result;
      });
  }

  MasodfokuFegyelmiHatarozatNyomtatasByIktatasIds({ iktatasIds, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MasodfokuFegyelmiHatarozatNyomtatvanyByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  MasodfokuFegyelmiHatarozatNyomtatasByFegyelmiUgyIds({
    fegyelmiUgyIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MasodfokuFegyelmiHatarozatNyomtatvanyByFegyelmiUgyIds';
    let data = { fegyelmiUgyIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  GetFegyelmiElkulonitesVegrehajtasaModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFegyelmiElkulonitesVegrehajtasaModalData';

    // TODO: backend
    return new Promise((res, rej) => {
      setTimeout(() => {
        res({
          // az id-t így kell generálni: objektumid_részlegid_zárkaid_ágyid, így is került visszaküldésre. Példa lent
          ZarkabaHelyezes: null,
          ZarkabaHelyezesOptions: [
            {
              id: '1',
              label: 'Objektum 1',
              children: [
                {
                  id: '1_1',
                  label: 'Reszleg 1',
                  children: [
                    {
                      id: '1_1_1',
                      label: 'Zarka 1',
                      children: [
                        {
                          id: '1_1_1_1',
                          label: 'Agy 1',
                        },
                      ],
                    },
                  ],
                },
                {
                  id: '1_2',
                  label: 'Reszleg 2',
                  children: [
                    {
                      id: '1_2_2',
                      label: 'Zarka 2',
                      children: [
                        {
                          id: '1_2_2_1',
                          label: 'Agy 1',
                        },
                      ],
                    },
                  ],
                },
              ],
            },
            {
              id: '2',
              label: 'Objektum 2',
              children: [
                {
                  id: '2_3',
                  label: 'Reszleg 3',
                  children: [
                    {
                      id: '2_3_3',
                      label: 'Zarka 3',
                      children: [
                        {
                          id: '2_3_3_1',
                          label: 'Agy 1',
                        },
                        {
                          id: '2_3_3_2',
                          label: 'Agy 2',
                        },
                        {
                          id: '2_3_3_3',
                          label: 'Agy 3',
                        },
                        {
                          id: '2_3_3_4',
                          label: 'Agy 4',
                        },
                      ],
                    },
                  ],
                },
                {
                  id: '2_4',
                  label: 'Reszleg 4',
                  children: [
                    {
                      id: '2_4_4',
                      label: 'Zarka 4',
                      children: [
                        {
                          id: '2_4_4_4',
                          label: 'Agy 4',
                        },
                      ],
                    },
                  ],
                },
              ],
            },
          ],
          ElrendeloOptions: [
            {
              id: 'S-1-5-21-3684209801-3913094555-2457220517-3674',
              text: 'Gábor Szigeti',
            },
          ],
          ElrendeloId: 'S-1-5-21-3684209801-3913094555-2457220517-3674',
        });
      }, 500);
    });
    //-
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveFegyelmiElkulonitesVegrehajtasaModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveFegyelmiElkulonitesVegrehajtasaModalData';

    // TODO: backend
    return new Promise((res, rej) => {
      setTimeout(() => {
        res({ naplobejegyzesIds: [] });
      }, 500);
    });
    //-

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetHataridoTullepesMiattiMegszuntetesModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetHataridoTullepesMiattiMegszuntetesModalData';

    // TODO: backend
    return new Promise((res, rej) => {
      setTimeout(() => {
        res({
          HatarozatotHozoOptions: [
            {
              id: 'S-1-5-21-3684209801-3913094555-2457220517-3674',
              text: 'Gábor Szigeti',
            },
          ],
          HatarozatotHozoId: 'S-1-5-21-3684209801-3913094555-2457220517-3674',
          MegszunesOkaOptions: [
            {
              id: 1,
              text: 'Megszűnés oka 1',
            },
            {
              id: 2,
              text: 'Megszűnés oka 2',
            },
            {
              id: 3,
              text: 'Megszűnés oka 3',
            },
          ],
          MegszunesOkaId: 2,
          Leiras: 'Mock',
        });
      }, 500);
    });
    //-
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveHataridoTullepesMiattiMegszuntetesModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveHataridoTullepesMiattiMegszuntetesModalData';

    // TODO: backend
    return new Promise((res, rej) => {
      setTimeout(() => {
        res({ naplobejegyzesIds: [] });
      }, 500);
    });
    //-

    return this.http.post({
      url,
      data,
      mock,
    });
  }

  GetaktivitasFolyamList({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/GetaktivitasFolyamList';
    return this.http.post({
      url,
      mock,
    });
  }

  SzembesitesiJegyzokonyvNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/SzembesitesiJegyzokonyvNyomtatvanyByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  SzembesitesiJegyzokonyvNyomtatasByIktatasIds({ iktatasIds, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/SzembesitesiJegyzokonyvNyomtatvanyByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetBuntetoFeljelentesRogziteseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetBuntetoFeljelentesRogziteseModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({
    //       ElintezesModjaOptions: [
    //         {
    //           id: 1,
    //           text: 'ElintezesModjaOptions 1',
    //         },
    //         {
    //           id: 2,
    //           text: 'ElintezesModjaOptions 2',
    //         },
    //         {
    //           id: 3,
    //           text: 'ElintezesModjaOptions 3',
    //         },
    //         {
    //           id: 4,
    //           text: 'ElintezesModjaOptions 4',
    //         },
    //       ],
    //       ElintezesModjaId: 2,
    //       FeljelentesMinositeseOptions: [
    //         {
    //           id: 1,
    //           text: 'FeljelentesMinositeseOptions 1',
    //         },
    //         {
    //           id: 2,
    //           text: 'FeljelentesMinositeseOptions 2',
    //         },
    //         {
    //           id: 3,
    //           text: 'FeljelentesMinositeseOptions 3',
    //         },
    //       ],
    //       FeljelentesMinositeseId: 2,
    //       FenyitesKiszabasIdeje: moment()
    //         .add({ d: -1 })
    //         .toISOString(),
    //     });
    //   }, 500);
    // });
    //-
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveBuntetoFeljelentesRogziteseModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveBuntetoFeljelentesRogziteseModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({ naplobejegyzesIds: [] });
    //   }, 500);
    // });
    //-

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetKozvetitoiEljarasHataridoModositasKereseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetKozvetitoiEljarasHataridoModositasKereseModalData';
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveKozvetitoiEljarasHataridoModositasKereseModalData({
    data,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveKozvetitoiEljarasHataridoModositasKereseModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetKozvetitoiEljarasHataridoModositasModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetKozvetitoiEljarasHataridoModositasModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({
    //       MaxDatum: moment()
    //         .add({ d: 10 })
    //         .toISOString(),

    //       Datum: moment()
    //         .add({ d: 1 })
    //         .toISOString(),
    //       Leiras: 'Mock',
    //     });
    //   }, 500);
    // });
    //-
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveKozvetitoiEljarasHataridoModositasModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveKozvetitoiEljarasHataridoModositasModalData';

    // TODO: backend
    // return new Promise((res, rej) => {
    //   setTimeout(() => {
    //     res({ naplobejegyzesIds: [] });
    //   }, 500);
    // });
    //-

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetFegyelmiUgyekArchiv({ ev, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyekArchiv';

    let data = { ev: ev };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetJutalmiUgyekArchiv({ ev, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Jutalom/GetJutalmiUgyekArchiv';

    let data = { ev: ev };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SzakteruletiVelemenyFileDelete({ fileId, mock = true } = {}) {
    let data = { fileId };
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/FegyelmiUgySzakteruletiVelemenyFileDelete';
    return this.http.post({
      url,
      data: data,
      mock,
    });
  }

  GetVelemenykeres({ velemenykeresId, mock = true }) {
    console.log('Véleménykérés: ' + velemenykeresId);
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyIdsByVelemenykeresId';
    let data = { velemenykeresId: velemenykeresId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetFenyitesVegrehajthatatlannaTeteleModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFenyitesNemVegrehajthatoModalData';
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveFenyitesVegrehajthatatlannaTeteleModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveFenyitesNemVegrehajthatoModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetMaganelzarasEllenjavallataModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetMaganelzarasEllenjavallataModalData';
    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveMaganelzarasEllenjavallataModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/SaveMaganelzarasEllenjavallataModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  //MegallapodasEsFeljegyzesNyomtatas
  MegallapodasEsFeljegyzesNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MegallapodasEsFeljegyzesNyomtatasByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  MegallapodasEsFeljegyzesNyomtatasByIktatasIds({ iktatasIds, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MegallapodasEsFeljegyzesNyomtatasByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetRendszerbeallitasok({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Session/GetRendszerbeallitasokModalData';

    let data = {};
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveRendszerbeallitasok({ data, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Session/SaveRendszerbeallitasokModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  RefreshAktivitasFolyam({ mock = true } = {}) {
    if (store.getters[UserStoreTypes.getters.vanJogosultsaga]) {
      var url = settings.baseUrl + 'Api/FegyelmiUgy/GetaktivitasFolyamList';
      return this.http
        .post({
          url,
          mock,
        })
        .then((result) => {
          store.dispatch(FegyelmiUgyStoreTypes.actions.refreshAktivitasFolyam, {
            value: Object.freeze(result),
          });
          return result;
        });
    }
  }
  KozvetitoiEljarasonReszvetelNyomtatasByNaploIds({
    naplobejegyzesIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/KozvetitoiEljarasonReszvetelNyomtatasByNaploIds';
    let data = { naplobejegyzesIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  KozvetitoiEljarasonReszvetelNyomtatasByIktatasIds({
    iktatasIds,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/KozvetitoiEljarasonReszvetelNyomtatasByIktatasIds';
    let data = { iktatasIds };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  GetMaganelzarasElrendeleseModalData({
    fegyelmiUgyIds,
    naplobejegyzesIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetMaganelzarasElrendeleseModalData';

    let data = { fegyelmiUgyIds, naplobejegyzesIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveMaganelzarasElrendeleseModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/SaveMaganelzarasElrendeleseModalData';

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  FenyitesVegrahajtasaKesz({ fegyelmiUgyIds, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/FegyelmiUgy/FenyitesVegrahajtasaKesz';
    let data = { fegyelmiUgyIds };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetOsszefoglalojelentesNyomtatasAdat({ fegyelmiUgyId, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/Teszt/GetOsszefoglalojelentesNyomtatasAdat';
    let data = { fegyelmiUgyId };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  // OsszefoglaloJelentesNyomtatasByFegyelmiUgyId({ fegyelmiUgyId, mock = true }) {
  //   var url =
  //     settings.baseUrl +
  //     'Api/Nyomtatvany/OsszefoglaloJelentesNyomtatasByFegyelmiUgyId';
  //   let data = { fegyelmiUgyId: fegyelmiUgyId };
  //   return this.http
  //     .post({
  //       url,
  //       data: data,
  //       mock,
  //     })
  //     .then(result => {
  //       return result;
  //     });
  //}
  OsszefoglaloJelentesNyomtatasByNaplobejegyzesId({
    naplobejegyzesId,
    mock = true,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/OsszefoglaloJelentesNyomtatasByNaplobejegyzesId';
    let data = { naplobejegyzesId: naplobejegyzesId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }
  OsszefoglaloJelentesNyomtatasByIktatasId({ iktatasId, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/OsszefoglaloJelentesNyomtatasByIktatasId';
    let data = { iktatasId: iktatasId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  GetFogvatartottOsszesFegyelmiUgy({ fegyelmiUgyId, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/FegyelmiUgy/GetFogvatartottOsszesFegyelmiUgy';
    let data = { fegyelmiUgyId: fegyelmiUgyId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  GetLetoltottMaganelzarasPercekByFegyelmiUgyId({
    fegyelmiUgyId,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetLetoltottMaganelzarasPercekByFegyelmiUgyId';
    let data = { fegyelmiUgyId: fegyelmiUgyId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  GetFolyamatbanEsKiszabvaFegyelmiUgyekByFegyelmiUgyId({
    fegyelmiUgyId,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl +
      'Api/FegyelmiUgy/GetFolyamatbanEsKiszabvaFegyelmiUgyekByFegyelmiUgyId';
    let data = { fegyelmiUgyId: fegyelmiUgyId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  NaploBejegyzesInaktivalasa({ naploBejegyzesId, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/NaploBejegyzes/NaploBejegyzesInaktivalasa';
    let data = { naploBejegyzesId: naploBejegyzesId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  NaploBejegyzesAktivalasa({ naploBejegyzesId, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/NaploBejegyzes/NaploBejegyzesAktivalasa';
    let data = { naploBejegyzesId: naploBejegyzesId };
    return this.http
      .post({
        url,
        data: data,
        mock,
      })
      .then((result) => {
        return result;
      });
  }

  //JUTALOM

  async GetNaploBejegyzesekByJutalomUgyId({ jutalomId, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/Jutalom/GetNaploBejegyzesekByJutalomUgyId ';
    let result = await this.http.post({
      data: { jutalomId: jutalomId },
      url,
      mock,
    });

    return result;
  }
  async GetJutalomUgyListItemViewModelById({ jutalomId, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/Jutalom/GetJutalomUgyListItemViewModelById';
    let result = await this.http.post({
      data: { jutalomId: jutalomId },
      url,
      mock,
    });

    return result;
  }
  async GetJutalomReszletei({ jutalomId, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Jutalom/GetJutalomReszletei ';
    let result = await this.http.post({
      data: { id: jutalomId },
      url,
      mock,
    });

    return result;
  }
  async GetJutalomEloterjesztesModalData({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Jutalom/GetJutalomEloterjesztesModalData';
    let result = await this.http.post({ url, mock });

    return result;
  }
  async SaveJutalomEloterjesztesModalData({ data, mock = true } = {}) {
    var url =
      settings.baseUrl + 'Api/Jutalom/SaveJutalomEloterjesztesModalData';
    let result = await this.http.post({ data, url, mock });

    return result;
  }
  // async GetToolbarData({ mock = true } = {}) {
  //   var url = settings.baseUrl + 'Api/Home/GetAppDataLegacy';
  //   let result = await this.http.post({ url, mock });

  //   return result;
  // }
  // ToDo: beüzemelni, mi kell aktivitás folyamba
  // RefreshAktivitasFolyam({ mock = true } = {}) {
  //   var url = settings.baseUrl + 'Api/Jutalom/GetAktivitasFolyamList';
  //   return this.http
  //     .post({
  //       url,
  //       mock,
  //     })
  //     .then(result => {
  //       store.dispatch(JutalomUgyStoreTypes.actions.refreshAktivitasFolyam, {
  //         value: Object.freeze(result),
  //       });
  //       return result;
  //     });
  // }
  async GetJutalomUgyek({ mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Jutalom/GetJutalomUgyek';
    await store.dispatch(JutalomUgyStoreTypes.actions.setJutalomUgyek, {
      value: Object.freeze([]),
    });
    let result = await this.http.post({ url, mock });
    await store.dispatch(JutalomUgyStoreTypes.actions.setJutalomUgyek, {
      value: Object.freeze(result),
    });
    return result;
  }

  GetFogvatartottakForSelect({ data, mock = true }) {
    var url = settings.baseUrl + 'Api/Jutalom/GetFogvatartottakForSelect';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetJutalomFelkeresVelemenyezesreModalData({ jutalomId, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/Jutalom/GetJutalomFelkeresVelemenyezesreModalData';
    let data = { jutalomId };

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveJutalomFelkeresVelemenyezesreModalData({ data, mock = true }) {
    var url =
      settings.baseUrl +
      'Api/Jutalom/SaveJutalomFelkeresVelemenyezesreModalData';
    //return {};
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetJutalomDontesVagyJavaslatModalData({ jutalomId, mock = true }) {
    var url =
      settings.baseUrl + 'Api/Jutalom/GetJutalomDontesVagyJavaslatModalData';
    let data = { jutalomId };

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveJutalomDontesVagyJavaslatModalData({ data, mock = true }) {
    var url =
      settings.baseUrl + 'Api/Jutalom/SaveJutalomDontesVagyJavaslatModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  GetJutalomVelemenyezesModalData({ jutalomId, mock = true }) {
    var url = settings.baseUrl + 'Api/Jutalom/GetJutalomVelemenyezesModalData';
    let data = { jutalomId };

    return this.http.post({
      url,
      data,
      mock,
    });
  }
  SaveJutalomVelemenyezesModalData({ data, mock = true }) {
    var url = settings.baseUrl + 'Api/Jutalom/SaveJutalomVelemenyezesModalData';
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  async GetFogvatartottJutalmai({ fogvatartottId, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Fogvatartott/GetFogvatartottJutalmai';

    let data = { fogvatartottId };

    let result = await this.http.post({ url, data, mock });
    console.log(result);
    return result;
  }
  GetJutalomUgyOsszevonasModalData({ jutalomId, mock = true }) {
    var url = settings.baseUrl + 'Api/Jutalom/GetJutalomUgyOsszevonasModalData';
    let data = { jutalomId };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  async SaveJutalomUgyOsszevonasModalData({
    jutalomId,
    osszevonandoJutalomIds,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/Jutalom/SaveJutalomUgyOsszevonasModalData';

    let data = { jutalomId, osszevonandoJutalomIds };

    let result = await this.http.post({ url, data, mock });
    return result;
  }

  async GetFogvatartottFegyelmiUgyeiJutalomhoz({ id, mock = true }) {
    var url =
      settings.baseUrl + 'Api/Jutalom/GetFogvatartottFegyelmiUgyeiJutalomhoz';
    let data = { id };
    return this.http.post({
      url,
      data,
      mock,
    });
  }
  async GetOsszevontJutalmiUgyekForJutalmiUgy({
    jutalmiUgyId,
    mock = true,
  } = {}) {
    var url =
      settings.baseUrl + 'Api/Jutalom/GetOsszevontJutalmiUgyekForJutalmiUgy';

    let data = { jutalmiUgyId };

    let result = await this.http.post({ url, data, mock });
    return result;
  }
  async KihirdetesMentes({ UgyIds, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Jutalom/KihirdetesMentes';

    let data = { UgyIds };

    let result = await this.http.post({ url, data, mock });
    return result;
  }
  async VisszavonasMentes({ UgyIds, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Jutalom/VisszavonasMentes';

    let data = { UgyIds };

    let result = await this.http.post({ url, data, mock });
    return result;
  }
  async VanNyitottFegyelmiVagyJutalom({ fogvatartasIds, mock = true } = {}) {
    var url = settings.baseUrl + 'Api/Jutalom/VanNyitottFegyelmiVagyJutalom';

    let data = { fogvatartasIds };

    let result = await this.http.post({ url, data, mock });
    return result;
  }
}

export default ApiService;
