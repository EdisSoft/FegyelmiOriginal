<template>
  <transition :name="transitionName" mode="out-in"
    ><span class="basic-span" :key="GetKey()">{{ timeText }}</span></transition
  >
</template>
<script>
import moment from 'moment';
import { dateAdd, dateSubtract } from '../../utils/date';
var livetimestampCounter = 0;

export default {
  name: 'k-live-timestamp',
  props: ['value'],
  data: function() {
    return {
      currentSettimeout: null,
      timeText: '-',
      livetimestampCounter: livetimestampCounter++,
      transitionName: 'empty',
    };
  },
  mounted: function() {},
  watch: {
    value: {
      handler: function() {
        this.transitionName = 'empty';
        this.CalculateTimeText();
        var vm = this;
        setTimeout(function() {
          vm.transitionName = 'slide-fade';
        }, 1000);
      },
      immediate: true,
    },
  },
  methods: {
    GetKey() {
      return this.livetimestampCounter + this.timeText;
    },
    CalculateTimeText: function() {
      var vm = this;
      // 2h
      let maxRefreshTime = 2 * 3600000;
      if (vm.currentSettimeout) clearTimeout(vm.currentSettimeout);

      var now = new Date();
      var date = new Date(moment(this.value));
      if (isNaN(date.getTime())) {
        vm.timeText = '';
        return;
      }
      var oneSecond = 1000;
      var oneMinute = 60 * oneSecond;
      var oneHour = 60 * oneMinute;
      var oneDay = 24 * oneHour;

      var changeDate = new Date(date);

      if (now - date < oneMinute) {
        //changeDate.setMinutes(changeDate.getMinutes() + 1);
        changeDate = dateAdd(changeDate, 'm', 1);
        vm.currentSettimeout = setTimeout(function() {
          vm.CalculateTimeText();
        }, changeDate - now);
        vm.timeText = 'most';
        return;
      }

      if (now - date < 5 * oneMinute) {
        //changeDate.setMinutes(changeDate.getMinutes() + 5);
        changeDate = dateAdd(changeDate, 'm', 5);
        vm.currentSettimeout = setTimeout(function() {
          vm.CalculateTimeText();
        }, changeDate - now);
        vm.timeText = 'nemrég';
        return;
      }

      if (now - date < oneHour) {
        // changeDate.setMinutes(
        //   changeDate.getMinutes() + parseInt((now - date) / oneMinute) + 1
        // );
        changeDate = dateAdd(
          changeDate,
          'm',
          parseInt((now - date) / oneMinute) + 1
        );
        vm.currentSettimeout = setTimeout(function() {
          vm.CalculateTimeText();
        }, changeDate - now);
        vm.timeText = parseInt((now - date) / oneMinute) + ' perce';
        return;
      }

      if (now - date < 2 * oneHour) {
        changeDate.setMinutes(changeDate.getMinutes() + 120);
        changeDate = dateAdd(changeDate, 'm', 120);
        vm.currentSettimeout = setTimeout(function() {
          vm.CalculateTimeText();
        }, changeDate - now);
        vm.timeText = 'egy órája';
        return;
      }

      if (now - date < 24 * oneHour) {
        //changeDate.setHours(changeDate.getHours() + 2);
        changeDate = dateAdd(changeDate, 'h', 2);
        vm.currentSettimeout = setTimeout(function() {
          vm.CalculateTimeText();
        }, changeDate - now);
        vm.timeText = parseInt((now - date) / oneHour) + ' órája';
        return;
      }

      var yesterday = new Date(moment().format('YYYY-MM-DD'));
      //console.log('date', date, 'yesterday', yesterday);
      //yesterday.setDate(yesterday.getDate() - 1);
      yesterday = dateSubtract(yesterday, 'd', 1);
      if (yesterday - date < 0) {
        //changeDate.setHours(changeDate.getHours() + 2);
        changeDate = dateAdd(changeDate, 'h', 2);
        vm.currentSettimeout = setTimeout(function() {
          vm.CalculateTimeText();
        }, date - yesterday);
        vm.timeText = 'tegnap';
        return;
      }

      var today = new Date(moment().format('YYYY-MM-DD'));
      changeDate = new Date(today);
      var dateToday = new Date(moment(date).format('YYYY-MM-DD'));

      if (today - dateToday < 30 * oneDay) {
        //changeDate.setDate(changeDate.getDate() + 1);
        changeDate = dateAdd(changeDate, 'd', 1);
        vm.currentSettimeout = setTimeout(function() {
          vm.CalculateTimeText();
        }, maxRefreshTime);
        vm.timeText = parseInt((today - dateToday) / oneDay) + ' napja';
        return;
      }

      if (today - dateToday < 365 * oneDay) {
        //changeDate.setFullYear(changeDate.getFullYear() + 1);
        changeDate = dateAdd(changeDate, 'y', 1);
        vm.currentSettimeout = setTimeout(function() {
          vm.CalculateTimeText();
        }, maxRefreshTime);
        vm.timeText = parseInt((today - dateToday) / oneDay / 30) + ' hónapja';
        return;
      }

      //changeDate.setFullYear(changeDate.getFullYear() + 1);
      changeDate = dateAdd(changeDate, 'y', 1);
      vm.currentSettimeout = setTimeout(function() {
        vm.CalculateTimeText();
      }, maxRefreshTime);
      vm.timeText = parseInt((today - dateToday) / oneDay / 365) + ' éve';
      return;
    },
  },
  destroyed: function() {
    if (this.currentSettimeout) clearTimeout(this.currentSettimeout);
  },
};
</script>

<style scoped>
.slide-fade-enter-active {
  transition: all 0.5s ease;
}

.slide-fade-leave-active {
  transition: all 0.5s ease;
}

.slide-fade-enter,
.slide-fade-leave-to {
  opacity: 0;
}
</style>
