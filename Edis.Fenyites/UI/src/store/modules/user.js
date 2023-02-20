import removeNamespace from '../../utils/vueUtils';

const localStorageKey = 'UserSettings-JFK';

// Ez nem kell
// var savedStateObj = JSON.parse(savedState);
// if (savedStateObj) {
//   savedStateObj.pushMessageSubscription = null;
// }

export const UserStoreTypes = {
  getters: {
    getUserInfo: 'user/getUserInfo',
    vanJogosultsaga: 'user/vanJogosultsaga',
    vanReintegraciosTisztJoga: 'user/vanReintegraciosTisztJoga',
    vanJogkorGyakorloJoga: 'user/vanJogkorGyakorloJoga',
    vanEgyebSzakteruletJoga: 'user/vanEgyebSzakteruletJoga',
    vanFofelugyeloJoga: 'user/vanFofelugyeloJoga',
    archivEvek: 'user/archivEvek',
  },
  actions: {
    setUserInfo: 'user/setUserInfo',
  },
  mutations: {
    SET_USERINFO: 'user/SET_USERINFO',
  },
};

const _types = removeNamespace('user/', UserStoreTypes);

export const jogosultsagok = {
  jfkFegyjutmegtekinto: 'jfk_fegyjutmegtekinto',
  fegyelmiEgyebSzakterulet: 'fegyelmi_egyeb_szakterulet',
  fegyelmiJogkorGyakorloja: 'fegyelmi_jogkor_gyakorloja',
  fegyelmiReintegraciosTiszt: 'fegyelmi_reintegracios_tiszt',
  fofelugyelo: 'fegyelmi_fofelugyelo',
};

const state = {
  //localStorageKey: localStorageKey,
  userInfo: null,
  defautlProjectId: null,
};

const getters = {
  [_types.getters.getUserInfo]: state => {
    return state.userInfo;
  },
  [_types.getters.vanReintegraciosTisztJoga]: state => {
    return (
      state.userInfo &&
      state.userInfo.Jogosultsagok.some(
        x =>
          x == jogosultsagok.fegyelmiJogkorGyakorloja ||
          x == jogosultsagok.fegyelmiReintegraciosTiszt
      )
    );
  },

  [_types.getters.vanJogkorGyakorloJoga]: state => {
    return (
      state.userInfo &&
      state.userInfo.Jogosultsagok.some(
        x => x == jogosultsagok.fegyelmiJogkorGyakorloja
      )
    );
  },
  [_types.getters.vanEgyebSzakteruletJoga]: state => {
    return (
      state.userInfo &&
      state.userInfo.Jogosultsagok.some(
        x =>
          x == jogosultsagok.fegyelmiJogkorGyakorloja ||
          x == jogosultsagok.fegyelmiEgyebSzakterulet
      )
    );
  },
  [_types.getters.vanFofelugyeloJoga]: state => {
    return (
      state.userInfo &&
      state.userInfo.Jogosultsagok.some(x => x == jogosultsagok.fofelugyelo)
    );
  },
  [_types.getters.vanJogosultsaga]: state => {
    return state.userInfo && state.userInfo.Jogosultsagok.length > 0;
  },
  [_types.getters.archivEvek]: state => {
    if (!state.userInfo || !state.userInfo.ArchivUgyekEvei) {
      return [];
    }
    return state.userInfo.ArchivUgyekEvei;
  },
};

// async
const actions = {
  [_types.actions.setUserInfo]({ commit }, payload) {
    commit(_types.mutations.SET_USERINFO, payload);
  },
};

// sync
const mutations = {
  [_types.mutations.SET_USERINFO](state, { value }) {
    state.userInfo = value;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
