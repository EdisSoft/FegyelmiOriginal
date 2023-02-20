<template>
  <div :class="[isLoading == true ? 'isformloading' : 'bg-white']">
    <div v-show="!isLoading">
      <slot />
    </div>
    <div v-show="isLoading" class="spinner-container text-center">
      <b-spinner large></b-spinner>
    </div>
  </div>
</template>
<script>
export default {
  name: 'basic-loader',
  data() {
    return {};
  },
  methods: {},
  computed: {},
  props: {
    isLoading: {
      type: Boolean,
    },
  },
};
</script>
<style scoped>
</style>