<template>
  <div>
    <div class="page-header">
      <h1 class="page-title" @click="Hello">Home</h1>
      <h1 class="page-title" @click="DateTimeStringTeszt">
        DateTimeStringTeszt
      </h1>
      <h1 class="page-title" @click="DateTimeTeszt">DateTimeTeszt</h1>
    </div>
    <div class="page-content">
      <b-link to="/notfound/">Fenyités</b-link>
    </div>
  </div>
</template>

<script>
import { sendToSocket } from '../utils/socketConnection';
export default {
  name: 'home',
  components: {},
  methods: {
    Hello() {
      sendToSocket('Hello');
    },
    DateTimeStringTeszt() {
      var date = new Date().toUTCString();
      sendToSocket('DateTimeStringTeszt', [date]);
    },
    DateTimeTeszt() {
      var date = new Date();
      sendToSocket('DateTimeTeszt', [date]);
    },
  },
};
</script>
