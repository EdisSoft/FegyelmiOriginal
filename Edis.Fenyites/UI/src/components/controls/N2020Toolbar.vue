<template>
  <iframe :src="src" style="display:none"></iframe>
</template>
<script>
import { apiService } from '../../main.js';

export default {
  name: 'k-n2020toolbar',

  data: function() {
    return {
      src: '',
    };
  },
  mounted: function() {
    var vm = this;
    apiService.GetToolbarData().then(function(data) {
      vm.src = data.url;
    });

    //iFrame áltaal küldött üzenetek itt érkeznek meg
    window.onmessage = function(e) {
      if (e.data) {
        switch (e.data.action) {
          case 'openN2020MobileIframe':
            console.log('k-n2020toolbar - window.onmessage', e.data);
            break;
        }
      }
    };
  },

  methods: {},
  destroyed: function() {},
};
</script>
