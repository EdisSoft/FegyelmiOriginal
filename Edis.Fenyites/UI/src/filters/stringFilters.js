import Vue from 'vue';
import { camelCaseString, camelCaseNameOnly } from '../utils/common';

Vue.filter('camelCaseString', function(value) {
  return camelCaseString(value);
});
Vue.filter('camelCaseNameOnly', function(value) {
  return camelCaseNameOnly(value);
});
Vue.filter('lowerCaseString', function(value) {
  if (typeof value == 'string') {
    return value.toLocaleLowerCase();
  }
  return '';
});
