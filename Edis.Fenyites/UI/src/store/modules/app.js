import removeNamespace from '../../utils/vueUtils';
import { FejlecTipus } from '../../data/enums/fejlecTipus';
export const AppStoreTypes = {
  getters: {
    getDokumentumok: 'app/getDokumentumok',
    getVedoDokumentumok: 'app/getVedoDokumentumok',
    isModalOpen: 'app/isModalOpen',
    isSlidePanelFegyelmiUgyReszletekOpen:
      'app/isSlidePanelFegyelmiUgyReszletekOpen',
    isSlidePanelJutalmiUgyReszletekOpen:
      'app/isSlidePanelJutalmiUgyReszletekOpen',
    getN2020NotificationCounter: 'app/getN2020NotificationCounter',
    getFonix3Url: 'app/getFonix3Url',
  },
  actions: {
    setDokumentumok: 'app/setDokumentumok',
    setVedoDokumentumok: 'app/setVedoDokumentumok',
    setModalOpen: 'app/setModalOpen',
    setSlidePanelFegyelmiUgyReszletekOpen:
      'app/setSlidePanelFegyelmiUgyReszletekOpen',
    setSlidePanelJutalmiUgyReszletekOpen:
      'app/setSlidePanelJutalmiUgyReszletekOpen',
    setN2020NotificationCounter: 'app/setN2020NotificationCounter',
    setFonix3Url: 'app/setFonix3Url',
  },
  mutations: {
    SET_DOKUMENTUMOK: 'app/SET_DOKUMENTUMOK',
    SET_VEDO_DOKUMENTUMOK: 'app/SET_VEDO_DOKUMENTUMOK',
    SET_MODAL_OPEN: 'app/SET_MODAL_OPEN',
    SET_N2020_NOTIFICATION_COUNTER: 'app/SET_N2020_NOTIFICATION_COUNTER',
    SET_SLIDE_PANEL_FEGYELMI_UGY_RESZLETEK_OPEN:
      'app/SET_SLIDE_PANEL_FEGYELMI_UGY_RESZLETEK_OPEN',
    SET_SLIDE_PANEL_JUTALMI_UGY_RESZLETEK_OPEN:
      'app/SET_SLIDE_PANEL_JUTALMI_UGY_RESZLETEK_OPEN',
    SET_FONIX3_URL: 'app/SET_FONIX3_URL',
  },
};

const _types = removeNamespace('app/', AppStoreTypes);

const state = {
  dokumentumok: false,
  vedoDokumentumok: false,
  isModalOpen: false,
  isSlidePanelFegyelmiUgyReszletekOpen: false,
  isSlidePanelJutalmiUgyReszletekOpen: false,
  n2020NotificationCounter: 0,
  fonix3Url: '',
  fejlecTipus: FejlecTipus.Fegyelmi,
};

const getters = {
  [_types.getters.getDokumentumok]: (state, getters, rootState) => {
    return state.dokumentumok;
  },
  [_types.getters.getVedoDokumentumok]: (state, getters, rootState) => {
    return state.vedoDokumentumok;
  },
  [_types.getters.isModalOpen]: (state, getters, rootState) => {
    return state.isModalOpen;
  },
  [_types.getters.isSlidePanelFegyelmiUgyReszletekOpen]: state => {
    return state.isSlidePanelFegyelmiUgyReszletekOpen;
  },
  [_types.getters.isSlidePanelJutalmiUgyReszletekOpen]: state => {
    return state.isSlidePanelJutalmiUgyReszletekOpen;
  },
  [_types.getters.getN2020NotificationCounter]: state => {
    return state.n2020NotificationCounter;
  },
  [_types.getters.getFonix3Url]: (state, getters, rootState) => {
    return state.fonix3Url;
  },
};

const actions = {
  [_types.actions.setDokumentumok]({ commit, state }, { value }) {
    commit(_types.mutations.SET_DOKUMENTUMOK, value);
  },
  [_types.actions.setVedoDokumentumok]({ commit, state }, { value }) {
    commit(_types.mutations.SET_VEDO_DOKUMENTUMOK, value);
  },
  [_types.actions.setModalOpen]({ commit, state }, { value }) {
    commit(_types.mutations.SET_MODAL_OPEN, value);
  },
  [_types.actions.setSlidePanelFegyelmiUgyReszletekOpen](
    { commit, state },
    { value }
  ) {
    commit(_types.mutations.SET_SLIDE_PANEL_FEGYELMI_UGY_RESZLETEK_OPEN, value);
  },
  [_types.actions.setSlidePanelJutalmiUgyReszletekOpen](
    { commit, state },
    { value }
  ) {
    commit(_types.mutations.SET_SLIDE_PANEL_JUTALMI_UGY_RESZLETEK_OPEN, value);
  },
  [_types.actions.setN2020NotificationCounter]({ commit, state }, { value }) {
    commit(_types.mutations.SET_N2020_NOTIFICATION_COUNTER, value);
  },
  [_types.actions.setFonix3Url]({ commit, state }, { value }) {
    commit(_types.mutations.SET_FONIX3_URL, value);
  },
};

const mutations = {
  [_types.mutations.SET_DOKUMENTUMOK](state, value) {
    state.dokumentumok = value;
  },
  [_types.mutations.SET_VEDO_DOKUMENTUMOK](state, value) {
    state.vedoDokumentumok = value;
  },
  [_types.mutations.SET_MODAL_OPEN](state, value) {
    state.isModalOpen = value;
  },
  [_types.mutations.SET_SLIDE_PANEL_FEGYELMI_UGY_RESZLETEK_OPEN](state, value) {
    state.isSlidePanelFegyelmiUgyReszletekOpen = value;
  },
  [_types.mutations.SET_SLIDE_PANEL_JUTALMI_UGY_RESZLETEK_OPEN](state, value) {
    state.isSlidePanelJutalmiUgyReszletekOpen = value;
  },
  [_types.mutations.SET_N2020_NOTIFICATION_COUNTER](state, value) {
    state.n2020NotificationCounter = value;
  },
  [_types.mutations.SET_FONIX3_URL](state, value) {
    state.fonix3Url = value;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
