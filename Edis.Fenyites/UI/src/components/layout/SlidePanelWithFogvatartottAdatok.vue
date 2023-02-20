<template>
  <transition name="slidePanel">
    <div
      v-show="visible"
      v-click-outside="Hide"
      class="slidePanel slidePanel-right slidePanel-show w-p20 "
      style="overflow-y: auto;"
      :class="{ 'w-p50 animatedwidth': url }"
    >
      <div class="slidePanel-content">
        <header
          class="slidePanel-header pr-30 pl-15 pt-15 pb-0"
          v-if="fenyites"
        >
          <div
            class="slidePanel-actions"
            aria-label="actions"
            role="group"
            style="min-height:40px;"
          >
            <button
              type="button"
              class="btn btn-icon btn-pure btn-inverse slidePanel-close actions-top icon wb-close blue-grey-800 py-0 px-0"
              @click="Hide()"
              aria-hidden="true"
            ></button>
          </div>
          <a class="avatar float-left mr-20">
            <!--<a class="avatar avatar-online">-->

            <k-fogvatartott-kep
              style="min-width:40px; min-height:40px; float:left"
              class="mr-20"
              :id="fenyites.FogvatartottId"
            ></k-fogvatartott-kep>
          </a>
          <h1 class="blue-grey-800">{{ fenyites.SzuletesiNev }}</h1>
          <ol class="breadcrumb pl-0 pb-0">
            <li class="breadcrumb-item blue-grey-800">
              {{ fenyites.AktNyilvantartasiSzam }}
            </li>
            <li class="breadcrumb-item blue-grey-800">
              {{ fenyites.NyilvantartottStatusz }}
            </li>
            <li
              v-if="fenyites.FenyitesStatusz"
              class="breadcrumb-item blue-grey-800"
            >
              {{ fenyites.FenyitesStatusz }}
            </li>
            <li v-else class="breadcrumb-item blue-grey-800">
              {{ fenyites.UgyStatusz }}
            </li>
          </ol>
        </header>
        <div
          v-if="fanyUrls && fanyUrls.length > 1 && !url && !isActive"
          class="slidePanel-inner"
        >
          <button
            v-for="url in fanyUrls"
            :key="url"
            type="button"
            @click="FanyCategory(url)"
            class="btn btn-block btn-primary btn-raised"
          >
            {{ url.title }}
          </button>
        </div>
        <div style="position:relative;width:100%; height:calc(100vh - 100px)">
          <iframe
            :src="url"
            width="100%"
            style="min-width:50%vw"
            height="100%"
            frameBorder="0"
            @load="EndLoader()"
          ></iframe>
          <div
            v-if="isLoading"
            class="vertical-align text-center"
            style="position:absolute; left:0; right:0; bottom:0; top:0; background-color:white;"
          >
            <div class="vertical-align-middle">
              <p style="font-size:25px; padding-bottom:10px">
                A Fany ablak betöltése folyamatban van. A régi ügyek kifutása
                után ezt az időt már nem kell kivárni. Ezen oldal helyett
                használható a Fany is adatrögzítésre.
              </p>

              <div class="loader loader-ellipsis"></div>
            </div>
          </div>
        </div>
        <!-- <slot></slot> -->
      </div>
    </div>
  </transition>
</template>
<script>
import $ from 'jquery';
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import { FenyitesStoreTypes } from '../../store/modules/fenyites';
import settings from '../../data/settings';
export default {
  name: 'k-slide-panel-with-fogvatartott-adatok',
  props: ['id'],
  data: function() {
    return {
      visible: false,
      canClose: true,
      url: '',
      fegyelmiUgyId: null,
      fenyites: null,
      isActive: false,
      fanyUrls: [],
      isLoading: false,
      urlLoadStartTime: 0,
    };
  },
  mounted: function() {
    var vm = this;
    eventBus.$on('Sidebar:' + vm.id, ({ state, data }) => {
      if (state) {
        this.Show(data.Urls, data.FegyelmiUgyId);
      } else {
        this.Hide();
      }
    });
  },
  methods: {
    FanyCategory: function(data) {
      this.canClose = false;
      setTimeout(() => {
        this.canClose = true;
      }, 350);
      this.url = data.url;
      this.isActive = true;
    },
    Show: function(urls, fegyelmiUgyId, fejlecCim) {
      console.log(urls);
      if (urls.length == 1) {
        this.url = urls[0].url;
      } else {
        this.url = '';
      }
      this.fanyUrls = urls;
      this.visible = true;
      this.fegyelmiUgyId = fegyelmiUgyId;
      this.fenyites = this.$store.getters[
        FenyitesStoreTypes.getters.getFenyitesById
      ](fegyelmiUgyId);
      this.canClose = false;
      setTimeout(() => {
        this.canClose = true;
      }, 350);
    },
    Hide: function() {
      if (!this.canClose) {
        return;
      }
      if (this.visible) {
        $('.dataTable tr.active').removeClass('active');
      }
      this.visible = false;

      setTimeout(() => {
        this.isActive = false;
      }, 1000);
      this.url = '';
    },
    StartLoader: function() {
      this.isLoading = true;
    },
    EndLoader: function() {
      this.isLoading = false;

      if (this.url) {
        var time = new Date();

        var dist = time - this.urlLoadStartTime;

        var milliseconds = dist % 1000;
        dist = parseInt(dist / 1000);
        var seconds = dist % 60;
        dist = parseInt(dist / 60);
        var minutes = dist % 60;
        dist = parseInt(dist / 60);
        var hours = dist;

        // Will display time in 10:30:23 format
        var formattedTime =
          hours + ':' + minutes + ':' + seconds + '.' + milliseconds;

        var url =
          settings.baseUrl +
          'Api/Diagnostics/InfoLog?data=' +
          encodeURIComponent('FANY betöltési idő:' + formattedTime) +
          '  \n' +
          'url: ' +
          encodeURIComponent(this.url);
        $.post(url);
      }
    },
  },
  watch: {
    url: {
      handler: function(value) {
        if (value) {
          this.StartLoader();
          this.urlLoadStartTime = new Date();
        }
      },
      immediate: true,
    },
  },
  destroyed: function() {},
};
</script>
<style scoped>
.slide-overlay {
  background: #92929257;
  position: fixed;
  left: 0;
  z-index: 999999;
  transition: background-color 0.5s ease;
}

.page-header {
  position: relative;
  padding: 30px 30px;
  margin-top: 0;
  margin-bottom: 0;
  background: transparent;
  border-bottom: 0;
}

.slidePanel-enter-active {
  transition: all 0.5s;
}

.slidePanel-leave-active {
  transition: all 0.5s;
}

.slidePanel-enter,
.slidePanel-leave-to {
  transform: translateX(100%);
}

.slidePanelOverlay-enter-active {
  transition: all 0.5s;
}

.slidePanelOverlay-leave-active {
  transition: all 0.5s;
}

.slidePanelOverlay-enter,
.slidePanelOverlay-leave-to {
  transform: translateX(100%);
  opacity: 0;
}

.slidePanel-content {
  height: 100%;
}

.animatedwidth {
  transition: all 0.5s;
}
</style>
