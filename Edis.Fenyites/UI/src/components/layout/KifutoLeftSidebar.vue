<template>
  <div class="page-aside page-aside-chat "  id="felelosLista">
    <div class="page-aside-switch">
      <i class="icon wb-chevron-left" aria-hidden="true"></i>
      <i class="icon wb-chevron-right" aria-hidden="true"></i>
    </div>

    <div class="page-aside-inner elochat" v-if="currenturl == 'Fenyites'">
      <div class="panel panel-bordered" ref="chatpanel">
        <div class="panel-heading">
          <h3 class="panel-title">
            Aktivitás folyam
            <span class="float-right badge badge-round badge-warning">
              <k-animated-number
                :value="szurtAdatok.length"
                :duration="1000"
              ></k-animated-number>
            </span>
          </h3>
          <div class="row no-gutters mt-10 mr-20 ml-20  mb-10">
            <div
              id="felelosKereses"
              class="input-search input-search-dark col-xl-12 col-md-12"
            >
              <input
                type="text"
                class="form-control "
                placeholder="Keress..."
                name=""
                v-model="keresoSzoveg"
              />
              <button class="input-search-btn">
                <i class="icon wb-search" aria-hidden="true"></i>
              </button>
            </div>
          </div>
        </div>

        <div class="panel-body" style="padding:0!important; height:100%">
          <div
            class="vuebar-element chatscroll"
            v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
          >
            <div  >
              <section class="page-aside-section">
                <!--<h5 class="page-aside-title">Ma</h5>-->
                <transition-group 
                  name="beuszasbalrol"
                  tag="ul"
                  class="list-group list-group-dividered list-group-full"
                  
                >
                  <li
                    @click="toggleSideBar(true, fenyites)"
                    class="list-group-item pointer"
                    :key="fenyites.FegyelmiUgyId"
                    v-for="fenyites in szurtAdatokDisplay"
                  >
                    <div class="media">
                      <div class="pr-20">
                        <a class="avatar">
                          <!--<a class="avatar avatar-online">-->
                          <k-fogvatartott-kep
                            :id="fenyites.FogvatartottId"
                          ></k-fogvatartott-kep>
                        </a>
                      </div>
                      <div class="media-body">
                        <h5 class="mt-0 mb-0">
                          {{ fenyites.SzuletesiNev }}
                          <small
                            class="text-muted float-right"
                            v-b-tooltip.hover
                          >
                            <k-live-timestamp
                              :value="fenyites.UtolsoModositasDatum"
                            ></k-live-timestamp>
                          </small>
                        </h5>
                        <span class="badge badge-outline badge-default">{{
                          fenyites.AktNyilvantartasiSzam
                        }}</span
                        ><span class="badge badge-outline badge-default"
                          >{{ fenyites.UgyIntezetAzon
                          }}{{ fenyites.UgySzama }}/{{ fenyites.UgyEve }}</span
                        >
                        <!-- <small style="margin-right:30px">{{
                          fenyites.AktNyilvantartasiSzam
                        }}</small>
                        <small>
                          {{ fenyites.UgyIntezetAzon
                          }}{{ fenyites.UgySzama }}/{{ fenyites.UgyEve }}
                        </small>
                        <br /> -->
                        <div
                          class="chatComment animation-fade text-twoline-truncate mt-5"
                        >
                          {{ fenyites.UgyStatusz }}
                        </div>

                        <small> {{ fenyites.UtolsoModositoSzemely }} </small>
                      </div>
                    </div>
                  </li>
                </transition-group>
              </section>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { FenyitesStoreTypes } from '../../store/modules/fenyites';
import { mapGetters, mapActions } from 'vuex';
import { eventBus } from '../../main';
import { UserStoreTypes } from '../../store/modules/user';
import Intezetek from '../../data/enums/intezetek';
import { getUgyszam } from '../../utils/fenyitesUtils';
import IFrameUrls from '../../data/enums/iframeUrls';

export default {
  name: 'kifuto-left-sidebar',
  data: function() {
    return {
      keresoSzoveg: '',
      fenyitesek: [],
      eventTrackingTimeOut: null,
      currenturl: '',
    };
  },
  methods: {
    toggleSideBar(state, fenyites) {
      var args = {};
      args.FegyelmiUgyId = fenyites.FegyelmiUgyId;
      args.Urls = IFrameUrls.GetFanyUrl(
        this.userInfo.RogzitoIntezet.Azonosito,
        fenyites
      );
      if (args.Urls.length == 0) return;
      eventBus.$emit('Sidebar:fanyFogvatartottAdatok', { state, data: args });
    },
  },
  computed: {
    ...mapGetters({
      fenyitesekVuex: FenyitesStoreTypes.getters.getFenyitesek,
      userInfo: UserStoreTypes.getters.getUserInfo,
    }),

    szurtAdatok: function() {
      var data = this.fenyitesek.slice();
      var rogzitoIntezetId = this.userInfo.RogzitoIntezet.Id;
      var bvopId = Intezetek.BVOP;
      if (this.keresoSzoveg) {
        var searchRegex = new RegExp(this.keresoSzoveg, 'i');
        data = data.filter(function(item) {
          if (
            rogzitoIntezetId != bvopId &&
            item.UgyIntezetId != rogzitoIntezetId
          ) {
            return false;
          }

          if (searchRegex.test(getUgyszam(item))) {
            return true;
          }

          if (searchRegex.test(item.UgyStatusz)) return true;
          if (searchRegex.test(item.UtolsoModositoSzemely)) return true;
          if (searchRegex.test(item.SzuletesiNev)) return true;
          if (searchRegex.test(item.AktNyilvantartasiSzam)) return true;

          return false;
        });
      } else {
        data = data.filter(
          f => rogzitoIntezetId == bvopId || f.UgyIntezetId == rogzitoIntezetId
        );
      }
      data.sort(function(a, b) {
        var aDate = a.UtolsoModositasDatum;
        var bDate = b.UtolsoModositasDatum;
        return new Date(bDate).getTime() - new Date(aDate).getTime();
      });
      return data;
    },
    szurtAdatokDisplay: function() {
      return this.szurtAdatok.slice(0, 50);
    },
  },
  mounted: function() {
    this.currenturl = this.$route.name;
  },
  updated() {},
  beforeDestroy() {},
  watch: {
    fenyitesekVuex: {
      handler: function(value) {
        setTimeout(() => {
          this.fenyitesek = Object.freeze(value);
        }, 150);
      },
      immediate: true,
    },
  },
};
</script>
<style scoped>
h5 {
  font-size: 1rem;
}
.vuebar-element.chatscroll {
  height: calc(100vh - 230px);
  max-width: 100%;
}
</style>
