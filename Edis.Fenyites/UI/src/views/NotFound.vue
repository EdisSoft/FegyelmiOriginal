<template>
  <div class="page-error vertical-align text-center">
    <div class="page-content vertical-align-middle">
      <header>
        <h1 class="animation-slide-top">404</h1>
        <p>Az oldal nem található!</p>
      </header>
      <!-- <p class="error-advise">Kérjük ellenőrizze az url-t!</p> -->
      <b-button variant="primary" pill to="/">Vissza a főoldalra</b-button>
      <footer class="page-copyright">
        <p>Konasoft</p>
      </footer>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  name: 'not-found',
  data() {
    return {};
  },
  mounted() {},
  created() {},
  methods: {},
  computed: {
    ...mapGetters({}),
  },
  watch: {},
  components: {},
};
</script>
<style scoped>
.page-error {
  font-family: Roboto, sans-serif !important;
  background-color: white;
  height: 100%;
}
.page-error header h1 {
  font-size: 10em;
  font-weight: 400;
}
.page-error .error-mark {
  margin-bottom: 33px;
}

.page-error header h1 {
  color: #37474f;
  font-size: 10em;
  font-weight: 400;
}

.page-error header p {
  color: #76838f;
  margin-bottom: 30px;
  font-size: 24px;
  /* text-transform: uppercase; */
}

.page-error h2 {
  margin-bottom: 30px;
}

.page-error .error-advise {
  margin-bottom: 25px;
  color: #a9afb5;
  font-size: 18px;
}
</style>
