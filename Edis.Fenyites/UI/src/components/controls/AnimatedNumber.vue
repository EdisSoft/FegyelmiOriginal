<template>
  <div>{{ temp.number | formatNumber }}</div>
</template>
<script>
import anime from 'animejs';
export default {
  name: 'k-animated-number',
  data: function() {
    return {
      temp: {
        number: 0,
      },
      animation: null,
    };
  },
  watch: {
    value: {
      handler: function(newValue, oldValue) {
        this.animation = anime({
          targets: this.temp,
          number: newValue,
          round: 1,
          easing: this.easing,
          duration: this.duration,
        });
      },
      deep: true,
      immediate: true,
    },
  },
  props: {
    value: {
      type: Number,
      default: 0,
    },
    duration: {
      type: Number,
      default: 1000,
    },
    easing: {
      type: String,
      default: 'easeOutCubic',
    },
  },
};
</script>
