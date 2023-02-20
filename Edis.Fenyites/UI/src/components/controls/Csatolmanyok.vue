<template>
  <div class="row mt-15 mx-0">
    <div
      class="csatolmany-card pointer mr-2 mb-10"
      v-for="file in csatolmanyokInfo"
      :key="file.Id"
    >
      <img
        class="file_card"
        :src="file.Icon"
        v-on:click="DownloadFile(file.DownloadUrl)"
      />
      <span
        v-on:click="DownloadFile(file.DownloadUrl)"
        class="slider-image-caption text-threeline-truncate text-white"
        >{{ file.FileName }}</span
      >
    </div>
  </div>
</template>
<script>
import { FegyelmiUgyFunctions } from '../../functions/FegyelmiUgyFunctions';
export default {
  name: 'k-csatolmanyok',
  data() {
    return {};
  },
  methods: {
    DownloadFile(url) {
      window.open(url);
    },
  },
  computed: {
    csatolmanyokInfo() {
      if (!this.csatolmanyok || this.csatolmanyok.length == 0) {
        return [];
      }
      let csatolmanyokInfo = FegyelmiUgyFunctions.GetCsatolmanyInfo(
        this.csatolmanyok
      );
      return csatolmanyokInfo;
    },
  },
  props: {
    csatolmanyok: {
      type: Array,
      required: true,
    },
  },
};
</script>
<style scoped></style>
