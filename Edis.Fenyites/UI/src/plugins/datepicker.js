import Vue from 'vue';
import datePicker from 'vue-bootstrap-datetimepicker';
import 'bootstrap/dist/css/bootstrap.css';
import 'pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css';
Vue.use(datePicker);
import $ from 'jquery';

$.extend(true, $.fn.datetimepicker.defaults, {
  tooltips: {
    today: 'Mai nap',
    clear: 'Kiválasztás törlése',
    close: 'Dátumválasztó bezárása',
    selectMonth: 'Hónapválasztó',
    prevMonth: 'Előző hónap',
    nextMonth: 'Következő hónap',
    selectYear: 'Évválasztó',
    prevYear: 'Előző év',
    nextYear: 'Következő év',
    selectDecade: 'Évtizedválasztó',
    prevDecade: 'Előző évtized',
    nextDecade: 'Következő évtized',
    prevCentury: 'Előző század',
    nextCentury: 'Következő század',
    pickHour: 'Óraválasztó',
    incrementHour: 'Következő óra',
    decrementHour: 'Előző óra',
    pickMinute: 'Percválasztó',
    incrementMinute: 'Következő perc',
    decrementMinute: 'Előző perc',
    pickSecond: 'Másodpercválasztó',
    incrementSecond: 'Következő másodperc',
    decrementSecond: 'Előző másodperc',
    togglePeriod: 'Toggle Period',
    selectTime: 'Időválasztó',
  },
});
