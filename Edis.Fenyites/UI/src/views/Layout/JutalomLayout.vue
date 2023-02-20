<template>
  <div class="h-p100">
    <!--<div class="animsition site-navbar-small page-aside-fixed page-aside-left page-project h-p100" style="animation-duration: 800ms; opacity: 1;">-->
    <div class="page">
      <left-sidebar v-if="vanJogosultsaga"> </left-sidebar>

      <div
        class="page-main h-p100"
        :class="{ 'page-aside-right-margin': vanJogosultsaga }"
      >
        <div class="bg"></div>
        <slot />
      </div>
      <jutalmi-statisztika v-if="vanJogosultsaga"></jutalmi-statisztika>
    </div>
    <footer
      class="site-footer ml-0 bg-blue-grey-600 position-relative d-flex align-items-center justify-content-between"
    >
      <div class="site-footer-legal text-white w-p100">
        © {{ new Date().getFullYear() }}
        <a href="http://fonix2/jfk" class="text-white">JFK</a>
        Crafted with <i class="text-info wb wb-heart-outline"></i> by
        <a
          class="text-white mr-5 konalink"
          target="_blank"
          href="https://konasoft.hu"
        >
          Konasoft&trade;
        </a>
        <a
          type="button"
          target="_blank"
          style="margin-top: -6px"
          class="btn btn-icon btn-xs social-facebook"
          href="https://www.facebook.com/konasoft.hu/"
        >
          <i class="icon bd-facebook" aria-hidden="true"></i> </a
        >&nbsp;
        <div id="bemutato" class="nav-item d-inline-block">
          <a
            type="button"
            target="_blank"
            style="margin-top: -6px"
            class="btn btn-icon btn-xs"
            href="./JFK.pdf"
          >
            <img src="../../assets/images/logos/usermanual.png" class="h-20" />
          </a>
        </div>
        <!-- &nbsp;
        <a
          v-if="VirKimutatasUrl"
          type="button"
          target="_blank"
          style="margin-top: -6px;"
          class="btn btn-icon btn-xs social-xing"
          v-bind:href="VirKimutatasUrl"
        >
          <i class="fa fa-file-excel-o" aria-hidden="true"></i>
        </a> -->
      </div>
      <div
        id="n2020"
        class="site-footer-right text-white w-p100 justify-content-end d-flex"
        v-if="userInfo.PersonalHelpdeskLoginUrl"
      >
        <div class="nav-item d-inline-block n2020-icon">
          <a
            class="footer-button d-flex pl-1 text-white"
            target="_blank"
            :href="userInfo.PersonalHelpdeskLoginUrl"
          >
            <img
              :src="require('../../assets/images/logos/PHD_logo.png')"
              alt="PHD"
              class="h-20 n2020-icon mr-5"
            /><span class="">Helpdesk</span>
          </a>
        </div>
      </div>
    </footer>
    <!--</div>-->
  </div>
</template>

<script>
import $ from 'jquery';
import { mapGetters, mapActions } from 'vuex';
import { UserStoreTypes } from '../../store/modules/user';
import LeftSidebar from '../../components/layout/LeftSidebar.vue';
import KifutoLeftSidebar from '../../components/layout/KifutoLeftSidebar.vue';
import { BeallitasokStoreTypes } from '../../store/modules/beallitasok';
import { apiService } from '../../main';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { timeout } from '../../utils/common';
import { AppStoreTypes } from '../../store/modules/app';
import { jutalmiListStatusz } from '../../store/modules/jutalomugy';

export default {
  name: 'jutalom-layout',
  data: function () {
    return {
      currenturl: '',
      urlWithToken: null,
      // n2020NotificationCounter:'',
    };
  },
  computed: {
    ...mapGetters({
      userInfo: UserStoreTypes.getters.getUserInfo,
      vanJogosultsaga: UserStoreTypes.getters.vanJogosultsaga,
      // VirKimutatasUrl: BeallitasokStoreTypes.getters.getVirKimutatasUrl,
      n2020NotificationCounter:
        AppStoreTypes.getters.getN2020NotificationCounter,
    }),
  },
  mounted: function () {
    this.GetAdatok();
  },
  methods: {
    async GetAdatok() {
      if (jutalmiListStatusz.inited || jutalmiListStatusz.loading) {
        return;
      }
      jutalmiListStatusz.loading = true;
      try {
        await apiService.GetJutalomUgyek();
      } catch (error) {
        console.error(error);
      }
      jutalmiListStatusz.loading = false;
    },
  },
  watch: {
    '$route.params': {
      handler: function (current, prev) {
        this.currenturl = this.$route.name;
      },
      immediate: true,
      deep: true,
    },
  },
  components: {
    LeftSidebar,
  },
};
</script>
<style>
#n2020 {
  cursor: pointer;
}
/* .n2020-icon {
  margin-top: -5px;
} */

.footer-button {
  -webkit-appearance: unset;
}

#iframeInitContainer {
  overflow: hidden !important;
  bottom: 43px;
  right: 30px;
  position: absolute;
  z-index: 10000;
  background-color: white;
  box-shadow: 10px 0px 5px grey;
}

#iframeInitContainer.donotshow {
  max-height: 0 !important;
  max-width: 0 !important;
  width: 0;
  height: 0;
  transition: all 500ms;
}

#iframeInitContainer.doshow {
  width: 375px;
  height: 715px !important;
  max-height: 715px !important;
  max-width: 375px !important;
  transition: all 500ms;
}

.badge-danger {
  background-color: #f45d5d !important;
  color: white;
}

/* #iframeBody {
  height: calc(100vh - 43px) !important;
} */

@media screen and (max-height: 800px) {
  #iframeInitContainer.doshow {
    height: calc(100vh - 156px) !important;
    max-height: calc(100vh - 156px) !important;
  }
  #iframeInitContainer.doshow iframe {
    height: 563px !important;
  }
}
.n2020-gradient {
  background-image: linear-gradient(260deg, #1accb4, #299bcb) !important;
}

.doshow .panel-actions {
  right: 10px;
}
.bigscreen#iframeInitContainer.doshow {
  width: 50% !important;
  max-width: 50% !important;
}

.bigscreen.doshow #iframeBody {
  width: 100% !important;
  max-width: 100% !important;
}
</style>
