import removeNamespace from '../../utils/vueUtils';
import { Object } from 'core-js';
export const EsemenyStoreTypes = {
  getters: {
    getEsemenyek: 'esemeny/getEsemenyek',
  },
  actions: {
    addEsemenyek: 'esemeny/addEsemenyek',
    setEsemenyek: 'esemeny/setEsemenyek',
  },
  mutations: {
    SET_ESEMENYEK: 'esemeny/SET_ESEMENYEK',
  },
};

const _types = removeNamespace('esemeny/', EsemenyStoreTypes);
var def = Object.freeze([]);
const state = {
  esemenyek: def,
};

export var esemenyListStatusz = {
  inited: false,
  loading: false,
};

const getters = {
  [_types.getters.getEsemenyek]: (state) => {
    var esemenyek = state.esemenyek;
    return esemenyek;
  },
};

const actions = {
  [_types.actions.addEsemenyek]({ commit, state }, { value }) {
    // Események törlése, ha új érkezik
    var esemenyek = state.esemenyek;
    esemenyek = esemenyek.filter(
      (f) => !value.some((a) => a.EsemenyId == f.EsemenyId)
    );
    // Todo: ez mutationsba kellene
    value.forEach((element) => {
      esemenyek.push(element);
    });

    commit(_types.mutations.SET_ESEMENYEK, esemenyek);
  },
  [_types.actions.setEsemenyek]({ commit }, { value }) {
    // Események törlése, ha új érkezik
    var esemenyek = [];
    // Todo: ez mutationsba kellene
    value.forEach((element) => {
      esemenyek.push(element);
    });

    commit(_types.mutations.SET_ESEMENYEK, esemenyek);
  },
};

const mutations = {
  [_types.mutations.SET_ESEMENYEK](state, value) {
    state.esemenyek = Object.freeze(value);
    esemenyListStatusz.inited = true;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
