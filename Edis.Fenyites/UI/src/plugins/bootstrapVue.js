import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import '../css/remark/bootstrap-extend.css';
import 'bootstrap/dist/js/bootstrap.bundle';

import './polyfills/focusinPolyfill';

Vue.use(BootstrapVue, {
  BTooltip: {
    delay: { show: 500, hide: 100 },
  },
});

export const defaultToolTipOptions = {
  html: true,
  trigger: 'hover',
};
