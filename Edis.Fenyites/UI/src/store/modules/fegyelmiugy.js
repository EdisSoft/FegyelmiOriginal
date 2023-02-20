import removeNamespace from '../../utils/vueUtils';
import { Object } from 'core-js';
import settings from '../../data/settings';
import store from '..';
import { UserStoreTypes } from './user';
export const FegyelmiUgyStoreTypes = {
  getters: {
    getFegyelmiUgyek: 'fegyelmiugy/getFegyelmiUgyek',
    getFegyelmiUgyById: 'fegyelmiugy/getFegyelmiUgyById',
    getFegyelmiUgyekSelected: 'fegyelmiugy/getFegyelmiUgyekSelected',
    getFegyelmiUgyekSelectedIds: 'fegyelmiugy/getFegyelmiUgyekSelectedIds',
    getFegyelmiUgyekSzuro: 'fegyelmiugy/getFegyelmiUgyekSzuro',
    getAktivitasFolyam: 'fegyelmiugy/getAktivitasFolyam',
  },
  actions: {
    addFegyelmiUgyek: 'fegyelmiugy/addFegyelmiUgyek',
    setFegyelmiUgyekSelected: 'fegyelmiugy/setFegyelmiUgyekSelected',
    addFegyelmiUgySelected: 'fegyelmiugy/addFegyelmiUgySelected',
    addFegyelmiUgy: 'fegyelmiugy/addFegyelmiUgy',
    removeFegyelmiUgySelected: 'fegyelmiugy/removeFegyelmiUgySelected',
    setFegyelmiUgyek: 'fegyelmiugy/setFegyelmiUgyek',
    removeFegyelmiUgyek: 'fegyelmiugy/removeFegyelmiUgyek',
    setFegyelmiUgyekSzuro: 'fegyelmiugy/setFegyelmiUgyekSzuro',
    addAktivitasFolyam: 'fegyelmiugy/addAktivitasFolyam',
    refreshAktivitasFolyam: 'fegyelmiugy/refreshAktivitasFolyam',
    modifyFegyelmiUgyek: 'fegyelmiugy/modifyFegyelmiUgyek',
  },
  mutations: {
    SET_FEGYELMIUGYEK: 'fegyelmiugy/SET_FEGYELMIUGYEK',
    REMOVE_FEGYELMIUGYEK: 'fegyelmiugy/REMOVE_FEGYELMIUGYEK',
    SET_FEGYELMI_UGYEK_SELECTED: 'fegyelmiugy/SET_FEGYELMI_UGYEK_SELECTED',
    REMOVE_FEGYELMI_UGY_SELECTED: 'fegyelmiugy/REMOVE_FEGYELMI_UGY_SELECTED',
    ADD_FEGYELMI_UGY_SELECTED: 'fegyelmiugy/ADD_FEGYELMI_UGY_SELECTED',
    ADD_FEGYELMI_UGY: 'fegyelmiugy/ADD_FEGYELMI_UGY',
    SET_FEGYELMI_UGYEK_SZURO: 'fegyelmiugy/SET_FEGYELMI_UGYEK_SZURO',
    SET_AKTIVITASFOLYAM: 'fegyelmiugy/SET_AKTIVITASFOLYAM',
  },
};

const _types = removeNamespace('fegyelmiugy/', FegyelmiUgyStoreTypes);
var def = Object.freeze([]);
const state = {
  fegyelmiUgyekSelected: [],
  fegyelmiUgyek: def,
  fegyelmiUgyekSzuro: -1,
  aktivitasfolyam: Object.freeze([]),
};
export var fegyelmiListStatusz = {
  inited: false,
  loading: false,
};
const getters = {
  [_types.getters.getFegyelmiUgyek]: (state) => {
    var fegyelmiUgyek = state.fegyelmiUgyek;
    return fegyelmiUgyek;
  },
  [_types.getters.getFegyelmiUgyById]: (state) => (id) => {
    var fegyelmiUgy = state.fegyelmiUgyek.find((f) => f.FegyelmiUgyId == id);
    return fegyelmiUgy;
  },
  [_types.getters.getFegyelmiUgyekSelected]: (state) => {
    var fegyelmiUgyek = state.fegyelmiUgyek.filter((f) =>
      state.fegyelmiUgyekSelected.some((s) => s == f.FegyelmiUgyId)
    );
    return fegyelmiUgyek;
  },
  [_types.getters.getFegyelmiUgyekSelectedIds]: (state) => {
    return state.fegyelmiUgyekSelected;
  },
  [_types.getters.getFegyelmiUgyekSzuro]: (state) => {
    return state.fegyelmiUgyekSzuro;
  },
  [_types.getters.getAktivitasFolyam]: (state) => {
    var aktivitasfolyam = state.aktivitasfolyam;
    return aktivitasfolyam;
  },
};

const actions = {
  [_types.actions.addFegyelmiUgyek]({ commit, state }, { value }) {
    // Fenyítések törlése, ha új érkezi k
    var fegyelmiUgyek = state.fegyelmiUgyek;
    fegyelmiUgyek = fegyelmiUgyek.filter(
      (f) => !value.some((a) => a.FegyelmiUgyId == f.FegyelmiUgyId)
    );
    // Todo: ez mutationsba kellene
    value.forEach((element) => {
      if (element.Kep) {
        element.Kep = element.Kep.replace('~/', settings.baseUrl);
      }
      fegyelmiUgyek.push(element);
    });

    commit(_types.mutations.SET_FEGYELMIUGYEK, fegyelmiUgyek);
  },
  [_types.actions.modifyFegyelmiUgyek]({ commit, state }, { value }) {
    // Fenyítések törlése, ha új érkezi k
    console.log('modifyFegyelmiUgyek');
    var fegyelmiUgyek = state.fegyelmiUgyek;
    var userRogzitoIntezetId =
      store.getters[UserStoreTypes.getters.getUserInfo].RogzitoIntezet.Id;
    fegyelmiUgyek = fegyelmiUgyek.filter(
      (f) =>
        !value.some(
          (a) =>
            a.FegyelmiUgyId == f.FegyelmiUgyId &&
            (a.FegyelmiIntezetId == userRogzitoIntezetId ||
              a.FogvatartottAktualisBvIntezet == userRogzitoIntezetId)
        )
    );
    // Todo: ez mutationsba kellene
    value.forEach((element) => {
      if (element.Kep) {
        element.Kep = element.Kep.replace('~/', settings.baseUrl);
      }
      fegyelmiUgyek.push(element);
    });

    commit(_types.mutations.SET_FEGYELMIUGYEK, fegyelmiUgyek);
  },

  [_types.actions.setFegyelmiUgyek]({ commit }, { value }) {
    // Fenyítések törlése, ha új érkezik
    var fegyelmiUgyek = [];

    value.forEach((element) => {
      if (element.Kep) {
        element.Kep = element.Kep.replace('~/', settings.baseUrl);
      }
      fegyelmiUgyek.push(element);
    });

    commit(_types.mutations.SET_FEGYELMIUGYEK, fegyelmiUgyek);
  },

  [_types.actions.removeFegyelmiUgyek]({ commit, state }, { value }) {
    // Fenyítések törlése, ha új érkezik
    var fegyelmiUgyek = state.fegyelmiUgyek
      .slice()
      .filter((f) => !value.some((s) => s == f.FegyelmiUgyId));
    //   .filter(f => f.FogvatartottId > ct);
    //ct += 50;
    if (state.fegyelmiUgyek.length != fegyelmiUgyek.length) {
      //console.log(state.fegyelmiUgyek.length, fegyelmiUgyek.length);
      commit(_types.mutations.SET_FEGYELMIUGYEK, fegyelmiUgyek);
    }
  },
  [_types.actions.setFegyelmiUgyekSelected]({ commit }, { value }) {
    commit(_types.mutations.SET_FEGYELMI_UGYEK_SELECTED, value);
  },
  [_types.actions.removeFegyelmiUgySelected]({ commit }, { value }) {
    commit(_types.mutations.REMOVE_FEGYELMI_UGY_SELECTED, value);
  },
  [_types.actions.addFegyelmiUgySelected]({ commit }, { value }) {
    commit(_types.mutations.ADD_FEGYELMI_UGY_SELECTED, value);
  },
  [_types.actions.addFegyelmiUgy]({ commit }, { value }) {
    commit(_types.mutations.ADD_FEGYELMI_UGY, value);
  },
  [_types.actions.setFegyelmiUgyekSzuro]({ commit }, { value }) {
    commit(_types.mutations.SET_FEGYELMI_UGYEK_SZURO, value);
  },
  [_types.actions.addAktivitasFolyam]({ commit, state }, { value }) {
    // Fenyítések törlése, ha új érkezik

    var aktivitasfolyam = state.aktivitasfolyam.slice();
    aktivitasfolyam = aktivitasfolyam.filter(
      (f) => !value.some((a) => a.FegyelmiUgyId == f.FegyelmiUgyId)
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
    // Fenyítések törlése, ha új érkezik
    var aktivitasfolyam = [];
    aktivitasfolyam = aktivitasfolyam.filter(
      (f) => !value.some((a) => a.FegyelmiUgyId == f.FegyelmiUgyId)
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
};

const mutations = {
  [_types.mutations.SET_FEGYELMIUGYEK](state, value) {
    state.fegyelmiUgyek = Object.freeze(value);
    fegyelmiListStatusz.inited = true;
  },
  [_types.mutations.SET_FEGYELMI_UGYEK_SELECTED](state, value) {
    state.fegyelmiUgyekSelected = Object.freeze([...value]);
  },
  [_types.mutations.SET_FEGYELMI_UGYEK_SZURO](state, value) {
    if (state.fegyelmiUgyekSzuro == value) {
      state.fegyelmiUgyekSzuro = -1;
    } else {
      state.fegyelmiUgyekSzuro = value;
    }
  },
  [_types.mutations.ADD_FEGYELMI_UGY_SELECTED](state, value) {
    let arr = [...state.fegyelmiUgyekSelected];
    if (!state.fegyelmiUgyekSelected.some((s) => s == value)) {
      arr.push(value);
    }
    state.fegyelmiUgyekSelected = Object.freeze(arr);
  },
  [_types.mutations.ADD_FEGYELMI_UGY](state, value) {
    let fegyelmiUgyekLista = state.fegyelmiUgyek.filter(
      (f) => f.FegyelmiUgyId != value.FegyelmiUgyId
    );
    fegyelmiUgyekLista.push(value);
    state.fegyelmiUgyek = Object.freeze(fegyelmiUgyekLista);
  },
  [_types.mutations.REMOVE_FEGYELMI_UGY_SELECTED](state, value) {
    let newArray = state.fegyelmiUgyekSelected.filter((f) => f != value);
    state.fegyelmiUgyekSelected = Object.freeze(newArray);
  },
  [_types.mutations.SET_AKTIVITASFOLYAM](state, value) {
    state.aktivitasfolyam = Object.freeze(value);
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
