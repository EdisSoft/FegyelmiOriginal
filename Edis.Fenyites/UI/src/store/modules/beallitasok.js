import removeNamespace from '../../utils/vueUtils';
import { Object } from 'core-js';
import settings from '../../data/settings';
var ct = 150;
export const BeallitasokStoreTypes = {
  getters: {
    getVirKimutatasFegyelmiUrl: 'beallitasok/getVirKimutatasFegyelmiUrl ',
    getVirKimutatasJutalomUrl: 'beallitasok/getVirKimutatasJutalomUrl',
    getFanyBaseUrl: 'beallitasok/getFanyBaseUrl',
  },
  actions: {
    setVirKimutatasFegyelmiUrl: 'beallitasok/setVirKimutatasFegyelmiUrl ',
    setVirKimutatasJutalomUrl: 'beallitasok/setVirKimutatasJutalomUrl',
    setFanyBaseUrl: 'beallitasok/setFanyBaseUrl',
  },
  mutations: {
    SET_VirKimutatasFegyelmiUrl: 'beallitasok/SET_VirKimutatasFegyelmiUrl ',
    SET_VirKimutatasJutalomUrl: 'beallitasok/SET_VirKimutatasJutalomUrl',
    SET_FanyBaseUrl: 'beallitasok/SET_FanyBaseUrl',
  },
};

const _types = removeNamespace('beallitasok/', BeallitasokStoreTypes);
const state = {
  virKimutatasFegyelmiUrl: '',
  virKimutatasJutalomUrl: '',
  fanyBaseUrl: '',
};

const getters = {
  [_types.getters.getVirKimutatasFegyelmiUrl]: (state, getters, rootState) => {
    return state.virKimutatasFegyelmiUrl;
  },
  [_types.getters.getVirKimutatasJutalomUrl]: (state, getters, rootState) => {
    return state.virKimutatasJutalomUrl;
  },
  [_types.getters.getFanyBaseUrl]: (state, getters, rootState) => {
    return state.fanyBaseUrl;
  },
};

const actions = {
  [_types.actions.setVirKimutatasFegyelmiUrl]({ commit, state }, { value }) {
    commit(_types.mutations.SET_VirKimutatasFegyelmiUrl, value);
  },
  [_types.actions.setVirKimutatasJutalomUrl]({ commit, state }, { value }) {
    commit(_types.mutations.SET_VirKimutatasJutalomUrl, value);
  },
  [_types.actions.setFanyBaseUrl]({ commit, state }, { value }) {
    commit(_types.mutations.SET_FanyBaseUrl, value);
  },
};

const mutations = {
  [_types.mutations.SET_VirKimutatasFegyelmiUrl](state, value) {
    state.virKimutatasFegyelmiUrl = value;
  },
  [_types.mutations.SET_VirKimutatasJutalomUrl](state, value) {
    state.virKimutatasJutalomUrl = value;
  },
  [_types.mutations.SET_FanyBaseUrl](state, value) {
    state.fanyBaseUrl = value;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
