import removeNamespace from '../../utils/vueUtils';
import { Object } from 'core-js';
import settings from '../../data/settings';
import store from '..';
import { UserStoreTypes } from './user';

export const JutalomUgyStoreTypes = {
  getters: {
    getJutalomUgyek: 'jutalomugy/getJutalomUgyek',
    getJutalomUgyById: 'jutalomugy/getJutalomUgyById',
    getJutalomUgyekSelected: 'jutalomugy/getJutalomUgyekSelected',
    getJutalomUgyekSelectedIds: 'jutalomugy/getJutalomUgyekSelectedIds',
    getJutalomUgyekSzuro: 'jutalomugy/getJutalomUgyekSzuro',
    getAktivitasFolyam: 'jutalomugy/getAktivitasFolyam',
  },
  actions: {
    setJutalomUgyekSelected: 'jutalomugy/setJutalomUgyekSelected',
    addJutalomUgySelected: 'jutalomugy/addJutalomUgySelected',
    addJutalomUgy: 'jutalomugy/addJutalomUgy',
    removeJutalomUgySelected: 'jutalomugy/removeJutalomUgySelected',
    setJutalomUgyek: 'jutalomugy/setJutalomUgyek',
    removeJutalomUgyek: 'jutalomugy/removeJutalomUgyek',
    setJutalomUgyekSzuro: 'jutalomugy/setJutalomUgyekSzuro',
    addAktivitasFolyam: 'jutalomugy/addAktivitasFolyam',
    refreshAktivitasFolyam: 'jutalomugy/refreshAktivitasFolyam',
    updateJutalomUgyek: 'jutalomugy/updateJutalomUgyek',
    modifyJutalomUgyek: 'jutalomugy/modifyJutalomUgyek',
    addJutalomUgyek: 'jutalomugy/addJutalomUgyek',
  },
  mutations: {
    SET_JUTALOM_UGYEK: 'jutalomugy/SET_JUTALOM_UGYEK',
    REMOVE_JUTALOM_UGYEK: 'jutalomugy/REMOVE_JUTALOM_UGYEK',
    SET_JUTALOM_UGYEK_SELECTED: 'jutalomugy/SET_JUTALOM_UGYEK_SELECTED',
    REMOVE_JUTALOM_UGY_SELECTED: 'jutalomugy/REMOVE_JUTALOM_UGY_SELECTED',
    ADD_JUTALOM_UGY_SELECTED: 'jutalomugy/ADD_JUTALOM_UGY_SELECTED',
    ADD_JUTALOM_UGY: 'jutalomugy/ADD_JUTALOM_UGY',
    SET_JUTALOM_UGYEK_SZURO: 'jutalomugy/SET_JUTALOM_UGYEK_SZURO',
    SET_AKTIVITASFOLYAM: 'jutalomugy/SET_AKTIVITASFOLYAM',
    UPDATE_JUTALOM_UGYEK: 'jutalomugy/UPDATE_JUTALOM_UGYEK',
  },
};

const _types = removeNamespace('jutalomugy/', JutalomUgyStoreTypes);
var def = Object.freeze([]);
const state = {
  jutalomUgyekSelected: [],
  jutalomUgyek: def,
  jutalomUgyekSzuro: -1,
  aktivitasfolyam: Object.freeze([]),
};
export var jutalmiListStatusz = {
  inited: false,
  loading: false,
};
const getters = {
  [_types.getters.getJutalomUgyek]: (state) => {
    var jutalomUgyek = state.jutalomUgyek;
    console.log(jutalomUgyek, 'jutalomUgyek');
    return jutalomUgyek;
  },
  [_types.getters.getJutalomUgyById]: (state) => (id) => {
    var jutalomUgy = state.jutalomUgyek.find((f) => f.Id == id);
    return jutalomUgy;
  },
  [_types.getters.getJutalomUgyekSelected]: (state) => {
    var jutalomUgyek = state.jutalomUgyek.filter((f) =>
      state.jutalomUgyekSelected.some((s) => s == f.Id)
    );
    return jutalomUgyek;
  },
  [_types.getters.getJutalomUgyekSelectedIds]: (state) => {
    return state.jutalomUgyekSelected;
  },
  [_types.getters.getJutalomUgyekSzuro]: (state) => {
    return state.jutalomUgyekSzuro;
  },
  [_types.getters.getAktivitasFolyam]: (state) => {
    var aktivitasfolyam = state.aktivitasfolyam;
    return aktivitasfolyam;
  },
};

const actions = {
  [_types.actions.addJutalomUgyek]({ commit, state }, { value }) {
    var jutalomUgyek = state.jutalomUgyek;
    console.log('addJutalomUgyek');
    jutalomUgyek = jutalomUgyek.filter((f) => !value.some((a) => a.Id == f.Id));
    value.forEach((element) => {
      if (element.Kep) {
        element.Kep = element.Kep.replace('~/', settings.baseUrl);
      }
      jutalomUgyek.push(element);
    });

    commit(_types.mutations.SET_JUTALOM_UGYEK, jutalomUgyek);
  },
  [_types.actions.setJutalomUgyek]({ commit }, { value }) {
    var jutalomUgyek = [];

    value.forEach((element) => {
      if (element.Kep) {
        element.Kep = element.Kep.replace('~/', settings.baseUrl);
      }
      jutalomUgyek.push(element);
    });

    commit(_types.mutations.SET_JUTALOM_UGYEK, jutalomUgyek);
  },

  [_types.actions.modifyJutalomUgyek]({ commit, state }, { value }) {
    console.log('modifyJutalomUgyek');
    var jutalomUgyek = state.jutalomUgyek;
    var userRogzitoIntezetId =
      store.getters[UserStoreTypes.getters.getUserInfo].RogzitoIntezet.Id;
    jutalomUgyek = jutalomUgyek.filter(
      (f) =>
        !value.some(
          (a) =>
            a.Id == f.Id &&
            (a.F2IntezetId == userRogzitoIntezetId ||
              a.FogvatartottF2AktualisBvIntezetId == userRogzitoIntezetId)
        )
    );
    // Todo: ez mutationsba kellene
    value.forEach((element) => {
      if (element.Kep) {
        element.Kep = element.Kep.replace('~/', settings.baseUrl);
      }
      jutalomUgyek.push(element);
    });

    commit(_types.mutations.SET_JUTALOM_UGYEK, jutalomUgyek);
  },

  [_types.actions.removeJutalomUgyek]({ commit, state }, { value }) {
    // Fenyítések törlése, ha új érkezik
    console.log('removeJutalomUgyek');
    var jutalomUgyek = state.jutalomUgyek
      .slice()
      .filter((f) => !value.some((s) => s == f.Id));
    //   .filter(f => f.FogvatartottId > ct);
    //ct += 50;
    if (state.jutalomUgyek.length != jutalomUgyek.length) {
      //console.log(state.jutalomUgyek.length, jutalomUgyek.length);
      commit(_types.mutations.SET_JUTALOM_UGYEK, jutalomUgyek);
    }
  },
  [_types.actions.setJutalomUgyekSelected]({ commit }, { value }) {
    commit(_types.mutations.SET_JUTALOM_UGYEK_SELECTED, value);
  },
  [_types.actions.removeJutalomUgySelected]({ commit }, { value }) {
    commit(_types.mutations.REMOVE_JUTALOM_UGY_SELECTED, value);
  },
  [_types.actions.addJutalomUgySelected]({ commit }, { value }) {
    commit(_types.mutations.ADD_JUTALOM_UGY_SELECTED, value);
  },
  [_types.actions.addJutalomUgy]({ commit }, { value }) {
    commit(_types.mutations.ADD_JUTALOM_UGY, value);
  },
  [_types.actions.setJutalomUgyekSzuro]({ commit }, { value }) {
    commit(_types.mutations.SET_JUTALOM_UGYEK_SZURO, value);
  },
  [_types.actions.addAktivitasFolyam]({ commit, state }, { value }) {
    // Fenyítések törlése, ha új érkezik

    var aktivitasfolyam = state.aktivitasfolyam.slice();
    aktivitasfolyam = aktivitasfolyam.filter(
      (f) => !value.some((a) => a.Id == f.Id)
    );
    value.forEach(function (a) {
      aktivitasfolyam.push(a);
    });

    aktivitasfolyam = aktivitasfolyam.sort(function (a, b) {
      return new Date(b.ErvenyessegKezd) - new Date(a.ErvenyessegKezd);
    });

    aktivitasfolyam = aktivitasfolyam.slice(0, 30);

    commit(_types.mutations.SET_AKTIVITASFOLYAM, aktivitasfolyam);
  },
  [_types.actions.refreshAktivitasFolyam]({ commit }, { value }) {
    var aktivitasfolyam = [];
    aktivitasfolyam = aktivitasfolyam.filter(
      (f) => !value.some((a) => a.Id == f.Id)
    );
    value.forEach(function (a) {
      aktivitasfolyam.push(a);
    });

    aktivitasfolyam = aktivitasfolyam.sort(function (a, b) {
      return new Date(b.ErvenyessegKezd) - new Date(a.ErvenyessegKezd);
    });
    aktivitasfolyam = aktivitasfolyam.slice(0, 30);

    commit(_types.mutations.SET_AKTIVITASFOLYAM, aktivitasfolyam);
  },
  // F2020 tábla update alapján
  async [_types.actions.updateJutalomUgyek](
    { commit, state, rootGetters },
    { value }
  ) {
    console.log('updateJutalomUgyek start');
    if (value == null || value.length == 0) {
      console.log('updateJutalomUgyek end, value null');
      return null;
    }
    // ToDo: majd ha kell userinfo jogosultság és intézet vizsgálathoz
    // if (!rootGetters[UserInfoStoreTypes.getters.getUserInfo]) {
    //   return;
    // }
    // let userAktualisIntezetId =
    //   rootGetters[UserInfoStoreTypes.getters.getUserInfo].Intezet.Id;

    let jutalmazasok = state.jutalomUgyek.slice();
    // ToDo: minek került ez be?
    // if (value.length == 0) {
    //   return null;
    // }
    value.forEach(async function (jutalmazas) {
      var index = jutalmazasok.findIndex((f) => f.Id == jutalmazas.Id);
      if (index == -1) {
        // ToDo: későbbi feltétel vizsgálat helye
        /*if (
          jutalmazas.Jelenlevo == true &&
          (userAktualisIntezetId ==
            jutalmazas.Jelenlegijutalmazas.NyilvantartoIntezet.Id ||
            userAktualisIntezetId ==
              jutalmazas.Jelenlegijutalmazas.FogvatartoIntezet.Id ||
            userAktualisIntezetId == IntezetEnums.BVOP)
        ) {*/
        jutalmazasok.unshift(jutalmazas);
        //}
        return;
      } else {
        /*if (
          jutalmazas.Jelenlevo == true &&
          jutalmazas.ToroltFl == false &&
          (userAktualisIntezetId ==
            jutalmazas.Jelenlegijutalmazas.NyilvantartoIntezet.Id ||
            userAktualisIntezetId ==
              jutalmazas.Jelenlegijutalmazas.FogvatartoIntezet.Id ||
            userAktualisIntezetId == IntezetEnums.BVOP)
        ) {
        jutalmazasok.splice(index, 1, jutalmazas);
        } else {
          jutalmazasok = jutalmazasok.filter(f => f.Id != jutalmazas.Id);
        }*/
        if (jutalmazas.ToroltFl == true) {
          jutalmazasok = jutalmazasok.filter((f) => f.Id != jutalmazas.Id);
        } else {
          jutalmazasok.splice(index, 1, jutalmazas);
        }
      }
    });
    commit(_types.mutations.UPDATE_JUTALOM_UGYEK, jutalmazasok);
    console.log('updateJutalomUgyek end');
  },
};

const mutations = {
  [_types.mutations.SET_JUTALOM_UGYEK](state, value) {
    state.jutalomUgyek = Object.freeze(value);
    jutalmiListStatusz.inited = true;
  },
  [_types.mutations.SET_JUTALOM_UGYEK_SELECTED](state, value) {
    state.jutalomUgyekSelected = Object.freeze([...value]);
  },
  [_types.mutations.SET_JUTALOM_UGYEK_SZURO](state, value) {
    if (state.jutalomUgyekSzuro == value) {
      state.jutalomUgyekSzuro = -1;
    } else {
      state.jutalomUgyekSzuro = value;
    }
  },
  [_types.mutations.ADD_JUTALOM_UGY_SELECTED](state, value) {
    let arr = [...state.jutalomUgyekSelected];
    if (!state.jutalomUgyekSelected.some((s) => s == value)) {
      arr.push(value);
    }
    state.jutalomUgyekSelected = Object.freeze(arr);
  },
  [_types.mutations.ADD_JUTALOM_UGY](state, value) {
    let jutalomUgyekLista = state.jutalomUgyek.filter((f) => f.Id != value.Id);
    jutalomUgyekLista.push(value);
    state.jutalomUgyek = Object.freeze(jutalomUgyekLista);
  },
  [_types.mutations.REMOVE_JUTALOM_UGY_SELECTED](state, value) {
    let newArray = state.jutalomUgyekSelected.filter((f) => f != value);
    state.jutalomUgyekSelected = Object.freeze(newArray);
  },
  [_types.mutations.SET_AKTIVITASFOLYAM](state, value) {
    state.aktivitasfolyam = Object.freeze(value);
  },
  [_types.mutations.UPDATE_JUTALOM_UGYEK](state, value) {
    state.jutalomUgyek = Object.freeze(value);
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
