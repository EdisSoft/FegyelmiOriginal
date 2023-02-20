<template>
  <div class="h-p100">
    <jfk-header></jfk-header>
    <!--<div class="animsition site-navbar-small page-aside-fixed page-aside-left page-project h-p100" style="animation-duration: 800ms; opacity: 1;">-->
    <div class="page">
      <kifuto-left-sidebar> </kifuto-left-sidebar>

      <div class="page-main h-p100" v-bind:style="'margin-left: 360px;'">
        <div class="bg"></div>
        <slot />
      </div>
    </div>
    <footer class="site-footer ml-0">
      <div class="site-footer-legal">
        © {{ new Date().getFullYear() }}
        <a href="http://fonix2/jfk" class="text-warning">JFK</a>
      </div>
      <div class="site-footer-right">
        Crafted with <i class="red-600 wb wb-heart"></i> by
        <a href="" class="text-warning">Konasoft</a>
      </div>
    </footer>
    <!--</div>-->
  </div>
</template>

<script>
import JfkHeader from '../../components/layout/JfkHeader.vue';
import { mapGetters, mapActions } from 'vuex';
import { UserStoreTypes } from '../../store/modules/user';
import LeftSidebar from '../../components/layout/LeftSidebar.vue';
import KifutoLeftSidebar from '../../components/layout/KifutoLeftSidebar.vue';

export default {
  name: 'kifuto-ugyek-layout',
  data: function() {
    return {
      currenturl: '',
    };
  },
  computed: {
    ...mapGetters({
      userInfo: UserStoreTypes.getters.getUserInfo,
      vanJogosultsaga: UserStoreTypes.getters.vanJogosultsaga,
    }),
  },
  mounted: function() {},
  watch: {
    '$route.params': {
      handler: function(current, prev) {
        this.currenturl = this.$route.name;
      },
      immediate: true,
      deep: true,
    },
  },
  components: {
    JfkHeader,
    KifutoLeftSidebar,
  },
};
</script>
<style scoped>
.page-aside-chat {
  width: 360px !important;
  top: 66px !important;
  height: calc(100% - 7.3rem) !important;
}
</style>
