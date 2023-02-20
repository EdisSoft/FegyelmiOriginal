import '@babel/polyfill';

import Vue from 'vue';
// import 'core-js';
import $ from 'jquery';
window.$ = window.JQuery = window.jQuery = $;

import './plugins';
import './components';
import './directives';
import './filters';
import './views/Layout';

import App from './App.vue';
import router from './router';
import store from './store';

import './registerServiceWorker';
import { safeGetProp } from './utils/common';
import httpContext from './utils/httpContext';

import ApiService from './services/ApiService';
import HttpStatusCode from './data/enums/httpStatusCode';
import settings from './data/settings';
import { GetSocketConnectionId, sendToSocket } from './utils/socketConnection';

Vue.config.productionTip = false;
Vue.config.devtools = !settings.isProd;
var _httpContext = httpContext;

if (!settings.isProd) {
  // Debug
  // let mockHttpContext = require('./mock/mockHttpContext').default;
  // _httpContext = mockHttpContext;
  // settings.isMock = true;
}

export const apiService = new ApiService(_httpContext);

const _eventBus = new Vue();
export const eventBus = _eventBus;

Vue.prototype.$get = safeGetProp;
Vue.prototype.$isDebug = !settings.isProd;
Vue.prototype.$placeholderImage =
  'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mPcGg0AAcsBEuRzwtUAAAAASUVORK5CYII=';

const _jfkApp = new Vue({
  router,
  store,
  created() {},
  render: (h) => h(App),
});
init();

async function init() {
  try {
    console.log(router);
    var getAppData = await apiService.GetAppData();
    var socketConnectionId = GetSocketConnectionId();
    let data = [socketConnectionId, getAppData.UserData.RogzitoIntezet.Id];
    if (socketConnectionId != null) {
      sendToSocket('SetIntezetIdToUser', data);
    }
  } catch (e) {
    console.log(e);
    if (e.jqXHR && e.jqXHR.status == HttpStatusCode.INTERNAL_SERVER_ERROR)
      console('A szerver nem elérhető');
  }
  _jfkApp.$mount('#app');
}

export const jfkApp = _jfkApp;

import('./socket');
import('./iframe');
