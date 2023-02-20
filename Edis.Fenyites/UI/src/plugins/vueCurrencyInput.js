import Vue from 'vue';
import VueCurrencyInput from 'vue-currency-input';

const pluginOptions = {
  globalOptions: {
    currency: { prefix: '', suffix: ' Ft' },
    decimalLength: 0,
    distractionFree: {
      hideNegligibleDecimalDigits: false,
      hideCurrencySymbol: false,
      hideGroupingSymbol: false,
    },
  },
};
Vue.use(VueCurrencyInput, pluginOptions);
