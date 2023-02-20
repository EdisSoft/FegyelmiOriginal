import Vue from 'vue';
import { formatNumber } from '../utils/common';

Vue.filter('formatNumber', function(value) {
  if (!value) {
    return 0;
  }
  let numStr = formatNumber(value);
  return numStr;
});
