// https://momentjs.com/docs/#/displaying/format/
import moment from 'moment';
import Vue from 'vue';

moment.locale('hu');

Vue.filter('toMonthHour', function(value) {
  let date = moment(value);
  if (!value || !date.isValid()) return '';
  return date.format('MMMM D. h') + 'ó';
});

Vue.filter('toShortDate', function(value) {
  let date = moment(value);
  if (!value || !date.isValid()) return '';
  return date.format('YYYY.MM.DD.');
});

Vue.filter('toShortDatePontNelkul', function(value) {
  let date = moment(value);
  if (!value || !date.isValid()) return '';
  return date.format('YYYY.MM.DD');
});

Vue.filter('toShortDateAndHour', function(value) {
  let date = moment(value);
  if (!value || !date.isValid()) return '';
  return date.format('YYYY.MM.DD. HH') + ' óra';
});

Vue.filter('toDateTime', function(value) {
  let date = moment(value);
  if (!value || !date.isValid()) return '';
  return date.format('YYYY.MM.DD. HH:mm');
});

Vue.filter('toShortDate', function(value) {
  let date = moment(value);
  if (!value || !date.isValid()) return '';
  return date.format('YYYY.MM.DD.');
});

Vue.filter('toShortDateKeltezes', function(value) {
  let date = moment(value);
  if (!value || !date.isValid()) return '';

  var jen = [1];
  var en = [4, 5, 7, 9, 10, 11, 12, 14, 15, 17, 19, 21, 22, 24, 25, 27, 29, 31];
  var an = [2, 3, 6, 8, 13, 16, 18, 20, 23, 26, 28, 30];

  var nap = parseInt(date.format('DD'));

  if (jen.filter(x => x == nap).length > 0) {
    return date.format('YYYY.MM.DD-jén');
  }
  if (en.filter(x => x == nap).length > 0) {
    return date.format('YYYY.MM.DD-én');
  }
  if (an.filter(x => x == nap).length > 0) {
    return date.format('YYYY.MM.DD-án');
  }

  return '';
});

Vue.filter('toShortDateKeltezes_rare', function(value) {
  let date = moment(value);
  if (!value || !date.isValid()) return '';

  // prettier-ignore
  var re = [1, 4, 5, 7, 9, 10, 11, 12, 14, 15, 17, 19, 21, 22, 24, 25, 27, 29, 31];
  var ra = [2, 3, 6, 8, 13, 16, 18, 20, 23, 26, 28, 30];

  var nap = parseInt(date.format('DD'));

  if (re.filter(x => x == nap).length > 0) {
    return date.format('YYYY.MM.DD[-re]');
  }
  if (ra.filter(x => x == nap).length > 0) {
    return date.format('YYYY.MM.DD[-ra]');
  }

  return '';
});
