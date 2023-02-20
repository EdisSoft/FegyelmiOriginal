import Vue from 'vue';
import Vuex from 'vuex';

import user from './modules/user';
import fenyites from './modules/fenyites';
import fegyelmiugy from './modules/fegyelmiugy';
import jutalomugy from './modules/jutalomugy';
import esemeny from './modules/esemeny';
import app from './modules/app';
import beallitasok from './modules/beallitasok';

import localStoragePlugin from './plugins/localStoragePlugin.js';
import settings from '../data/settings';
import { fogvatartottak } from './modules/fogvatartottak';

Vue.use(Vuex);

const store = new Vuex.Store({
  modules: {
    user,
    fenyites,
    fegyelmiugy,
    esemeny,
    app,
    beallitasok,
    fogvatartottak,
    jutalomugy,
  },
  strict: !settings.isProd,
  devtools: !settings.isProd,
  plugins: [],
});
export default store;
