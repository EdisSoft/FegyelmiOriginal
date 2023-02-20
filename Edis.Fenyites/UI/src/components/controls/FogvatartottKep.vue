<template>
  <img :src="getImageUrl" @error="imageNotFoundError($event)" />
</template>
<script>
import settings from '../../data/settings';
export default {
  name: 'k-fogvatartott-kep',
  props: ['id'],
  data: function() {
    return {
      noImage:
        'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mPcGg0AAcsBEuRzwtUAAAAASUVORK5CYII=',
    };
  },
  mounted: function() {},
  methods: {
    imageNotFoundError(e) {
      e.target.src = this.noImage;
    },
  },
  computed: {
    getImageUrl() {
      return (
        settings.baseUrl +
        'kozoskepek/' +
        this.id +
        '.jpg?t=' +
        settings.timeStamp
      );
    },
  },
};
</script>
