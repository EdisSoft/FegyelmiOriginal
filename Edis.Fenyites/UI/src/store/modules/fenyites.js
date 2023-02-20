import removeNamespace from '../../utils/vueUtils';
import { Object } from 'core-js';
import settings from '../../data/settings';
var ct = 150;
export const FenyitesStoreTypes = {
  getters: {
    getFenyitesek: 'fenyites/getFenyitesek',
    getFenyitesById: 'fenyites/getFenyitesById',
  },
  actions: {
    addFenyitesek: 'fenyites/addFenyitesek',
    removeFenyitesek: 'fenyites/removeFenyitesek',
  },
  mutations: {
    SET_FENYITESEK: 'fenyites/SET_FENYITESEK',
    REMOVE_FENYITESEK: 'fenyites/REMOVE_FENYITESEK',
  },
};

const _types = removeNamespace('fenyites/', FenyitesStoreTypes);
var def = Object.freeze([]);
const state = {
  fenyitesek: def,
};

const getters = {
  [_types.getters.getFenyitesek]: state => {
    var fenyitesek = state.fenyitesek;
    return fenyitesek;
  },
  [_types.getters.getFenyitesById]: state => id => {
    var fenyites = state.fenyitesek.find(f => f.FegyelmiUgyId == id);
    return fenyites;
  },
};

const actions = {
  [_types.actions.addFenyitesek]({ commit, state }, { value }) {
    // Fenyítések törlése, ha új érkezik
    var fenyitesek = state.fenyitesek;
    fenyitesek = fenyitesek.filter(
      f => !value.some(a => a.FegyelmiUgyId == f.FegyelmiUgyId)
    );
    // Todo: ez mutationsba kellene
    value.forEach(element => {
      if (element.Kep) {
        element.Kep = element.Kep.replace('~/', settings.baseUrl);
      }
      fenyitesek.push(element);
    });

    commit(_types.mutations.SET_FENYITESEK, fenyitesek);
  },

  [_types.actions.removeFenyitesek]({ commit, state }, { value }) {
    // Fenyítések törlése, ha új érkezik
    var fenyitesek = state.fenyitesek
      .slice()
      .filter(f => !value.some(s => s == f.FegyelmiUgyId));
    //   .filter(f => f.FogvatartottId > ct);
    //ct += 50;
    if (state.fenyitesek.length != fenyitesek.length) {
      //console.log(state.fenyitesek.length, fenyitesek.length);
      commit(_types.mutations.SET_FENYITESEK, fenyitesek);
    }
  },
};

const mutations = {
  [_types.mutations.SET_FENYITESEK](state, value) {
    state.fenyitesek = Object.freeze(value);
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
