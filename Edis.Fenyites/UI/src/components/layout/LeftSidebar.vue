<template>
  <div id="aktivitasfolyam" class="page-aside page-aside-chat ml-15">
    <div class="page-aside-switch">
      <i class="icon wb-chevron-left" aria-hidden="true"></i>
      <i class="icon wb-chevron-right" aria-hidden="true"></i>
    </div>

    <div
      class="page-aside-inner elochat pr-1 d-flex justify-content-start flex-column"
      ref="elochat"
    >
      <div class="panel mb-15" ref="chatpanel">
        <div class="panel-heading py-10 px-10 border-0">
          <h3 class="panel-title font-weight-400 p-0 text-warning mb-5">
            Határidős feladatok
            <span class="float-right badge badge-round white bg-warning">
              <k-animated-number
                :value="HataridosUgyek.length"
                :duration="1000"
              ></k-animated-number>
            </span>
          </h3>
          <p class="mb-0">Lejárt vagy 3 napon belül lejáró ügyek</p>
        </div>

        <div class="panel-body px-0 py-0" ref="panel1" id="HataridosFeladatok">
          <div
            class="pb-10"
            v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
            :style="[
              this.panel1height > 15
                ? { height: `${this.panel1height}px` }
                : {},
            ]"
          >
            <div>
              <transition-group
                name="beuszasbalrol"
                tag="ul"
                class="list-group list-group-dividered list-group-full pl-5 pr-15 pt-0 pb-5 mb-0"
                id="felelosLista"
              >
                <k-live-hatarido
                  :key="
                    fenyites.FegyelmiUgyId
                      ? fenyites.FegyelmiUgyId
                      : fenyites.Id
                  "
                  class="list-group-item"
                  v-for="fenyites in HataridosUgyek"
                  :ugy="fenyites"
                  :isBvop="isBvop"
                  @click.native="UgyReszletekMegtekintes(fenyites)"
                ></k-live-hatarido>
              </transition-group>
            </div>
          </div>
        </div>
      </div>
      <div class="panel" ref="chatpanel" v-if="$route.meta.layout != 'jutalom'">
        <div class="panel-heading py-10 px-10 border-0">
          <h3 class="panel-title text-info font-weight-400 p-0 mb-5">
            Aktivitás folyam
            <span class="float-right badge badge-round bg-info white">
              <k-animated-number
                :value="aktivitasFolyamVuex.length"
                :duration="1000"
              ></k-animated-number>
            </span>
          </h3>
          <p class="mb-0">Legutóbb változtatott fegyelmi ügyek</p>
        </div>

        <div class="panel-body px-0 py-0" ref="panel2" id="AktivitasFolyam">
          <div
            class="pb-10"
            style="height: 100"
            v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
            :style="[
              this.panel2height > 15
                ? { height: `${this.panel2height}px` }
                : {},
            ]"
          >
            <div>
              <transition-group
                name="beuszasbalrol"
                tag="ul"
                class="list-group list-group-dividered list-group-full pl-5 pr-15 pt-0 pb-5 mb-0"
                id="felelosLista"
              >
                <li
                  class="list-group-item px-0 mb-0 pointer"
                  @click="UgyReszletekMegtekintes(fenyites)"
                  :key="fenyites.FegyelmiUgyId"
                  v-for="fenyites in aktivitasFolyamVuex"
                >
                  <div class="media d-flex align-items-center">
                    <div class="pr-5">
                      <a class="avatar" href="javascript:void(0)">
                        <k-fogvatartott-kep
                          :id="fenyites.FogvatartottId"
                        ></k-fogvatartott-kep>
                      </a>
                    </div>
                    <div class="media-body">
                      <div class="pl-0 add-data">
                        <p class="blue-grey-700 mb-0 text-right font-size-10">
                          <k-live-timestamp
                            :value="fenyites.ErvenyessegKezd"
                          ></k-live-timestamp>
                        </p>
                      </div>
                      <p
                        class="akt-nyilv-szam blue-grey-700 mt-0 mb-0 font-size-12 fogv-nev text-capitalize"
                      >
                        {{ fenyites.FogvatartottNev }}
                      </p>
                      <p class="mt-0 mb-5">
                        <span
                          class="badge badge-outline badge-default mr-1 mt-1 font-weight-400 text-break"
                        >
                          {{ fenyites.AktNyilvantartasiSzam }}
                        </span>
                        <span
                          class="badge badge-outline badge-default mr-1 mt-1 font-weight-400 text-break"
                        >
                          {{ fenyites.UgyIntezetAzon }}/{{ fenyites.UgyEve }}/{{
                            fenyites.UgySzama
                          }}
                        </span>
                        <span
                          class="badge badge-outline badge-default mt-1 font-weight-400 text-break"
                          v-if="isBvop"
                        >
                          {{ fenyites.FegyelmiIntezet }}
                        </span>
                      </p>
                      <p class="mt-0 mb-0 font-size-11 blue-grey-700">
                        {{ fenyites.NaploTipusNev }}
                      </p>
                      <!-- <p class="mt-0 mb-0 font-size-11 blue-grey-700">
                        <span class="text-uppercase">{{
                          fenyites.RogzitoSzemelyNev
                        }}</span>
                        {{ fenyites.RogzitoSzemelyRendfokozat }}
                      </p> -->
                      <p
                        class="mt-0 mb-0 font-size-11 blue-grey-700"
                        v-if="
                          (fenyites.UgyStatuszId ==
                            fegyelmiUgyStatuszok.KivizsgalasFolyamatban ||
                            fenyites.UgyStatuszId ==
                              fegyelmiUgyStatuszok.ReintegraciosTisztiJogkorben) &&
                          fenyites.Kivizsgalo
                        "
                      >
                        Kiv.:
                        <span class="text-capitalize">{{
                          fenyites.Kivizsgalo | camelCaseString
                        }}</span>
                        <span
                          v-if="
                            fenyites.KivizsgaloRendfokozat != 'Nincs megadva'
                          "
                        >
                          {{ fenyites.KivizsgaloRendfokozat }}</span
                        >
                      </p>
                      <p
                        class="mt-0 mb-0 font-size-11 blue-grey-700"
                        v-else-if="fenyites.Elrendelo"
                      >
                        Elr.:
                        <span class="text-capitalize">{{
                          fenyites.Elrendelo
                        }}</span
                        ><span
                          v-if="
                            fenyites.ElrendeloRendfokozat != 'Nincs megadva'
                          "
                        >
                          {{ fenyites.ElrendeloRendfokozat }}</span
                        >
                      </p>
                    </div>
                  </div>
                </li>
              </transition-group>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { mapGetters, mapActions } from 'vuex';
import { eventBus } from '../../main';
import { UserStoreTypes } from '../../store/modules/user';
import Intezetek from '../../data/enums/intezetek';
import { getUgyszam } from '../../utils/fenyitesUtils';
import IFrameUrls from '../../data/enums/iframeUrls';
import { apiService } from '../../main';
import moment from 'moment';
import { sortDate } from '../../utils/sort';
import Cimkek from '../../data/enums/Cimkek';
import { defaultToolTipOptions } from '../../plugins/bootstrapVue';
import $ from 'jquery';
import { timeout } from '../../utils/common';
import { JutalomUgyStoreTypes } from '../../store/modules/jutalomugy';

export default {
  name: 'left-sidebar',
  data: function () {
    return {
      keresoSzoveg: '',
      fenyitesek: [],
      eventTrackingTimeOut: null,
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#AktivitasFolyam',
      },
      panel1height: null,
      panel2height: null,
      fegyelmiUgyStatuszok: Cimkek.FegyelmiUgyStatusza,
      currenturl: '',
    };
  },
  methods: {
    ...mapActions({
      setAktivitasFolyamVuex: FegyelmiUgyStoreTypes.actions.addAktivitasFolyam,
    }),
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
    async combined(reRun) {
      await this.$nextTick();

      // Az animáció rendezgezi a sorokat, újra meg jell hívni miután végzett
      if (reRun) {
        timeout(1001).then(() => {
          this.combined(false);
        });

        // return;
      }

      // Fix 50%-50%
      this.panel2height = this.$el.clientHeight / 2 - 78;
      this.panel1height = this.getPanel1Height;
    },

    UgyReszletekMegtekintes(data) {
      console.log('UgyReszletekMegtekintes', data);
      if (data.FegyelmiUgyId) {
        let args = {
          fegyelmiUgyId: data.FegyelmiUgyId,
          fegyelmiUgy: data,
        };

        eventBus.$emit('Sidebar:ugyReszletek', {
          state: true,
          data: args,
        });
      } else {
        var args = { jutalomId: data.JutalomId, jutalomUgy: data };

        eventBus.$emit('Sidebar:jutalmiUgyReszletek', {
          state: true,
          data: args,
        });
      }
    },
  },
  computed: {
    ...mapGetters({
      fenyitesekVuex: FegyelmiUgyStoreTypes.getters.getFegyelmiUgyek,
      jutalmakVuex: JutalomUgyStoreTypes.getters.getJutalomUgyek,
      fegyelmiaktivitasFolyamVuex:
        FegyelmiUgyStoreTypes.getters.getAktivitasFolyam,
      jutalomAktivitasFolyamVuex:
        JutalomUgyStoreTypes.getters.getAktivitasFolyam,
      userInfo: UserStoreTypes.getters.getUserInfo,
    }),
    isBvop() {
      if (!this.userInfo) {
        return false;
      }
      return this.userInfo.RogzitoIntezet.Id == Intezetek.BVOP;
    },
    aktivitasFolyamVuex() {
      var list = [
        ...this.fegyelmiaktivitasFolyamVuex,
        ...this.jutalomAktivitasFolyamVuex,
      ];
      console.log(
        'this.jutalomAktivitasFolyamVuex',
        this.jutalomAktivitasFolyamVuex
      );
      return list;
    },
    szurtAdatok: function () {
      var data = this.fenyitesek.slice();
      var rogzitoIntezetId = this.userInfo.RogzitoIntezet.Id;
      var bvopId = Intezetek.BVOP;
      if (this.keresoSzoveg) {
        var searchRegex = new RegExp(this.keresoSzoveg, 'i');
        data = data.filter(function (item) {
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
          (f) =>
            rogzitoIntezetId == bvopId || f.UgyIntezetId == rogzitoIntezetId
        );
      }
      data.sort(function (a, b) {
        var aDate = a.UtolsoModositasDatum;
        var bDate = b.UtolsoModositasDatum;
        return new Date(bDate).getTime() - new Date(aDate).getTime();
      });
      return data;
    },
    szurtAdatokDisplay: function () {
      return this.szurtAdatok.slice(0, 50);
    },
    HataridosUgyek: function () {
      var hatarDatum = moment().startOf('d').add(4, 'days').toDate();

      let isFegyelmi = this.$route.meta.layout == 'fenyites';
      var list = [];
      if (isFegyelmi) {
        list = this.fenyitesekVuex.filter(
          (x) =>
            x.StatuszId != Cimkek.FegyelmiUgyStatusza.Osszevonva &&
            x.Hatarido &&
            new Date(x.Hatarido).getTime() < hatarDatum.getTime()
        );
      } else {
        list = this.jutalmakVuex.filter(
          (x) =>
            x.StatuszId != Cimkek.JutalomStatusz.Osszevonva &&
            x.Hatarido &&
            new Date(x.Hatarido).getTime() < hatarDatum.getTime()
        );
      }

      list.sort(sortDate('Hatarido'));

      return list;
    },
    getPanel1Height() {
      if (this.currenturl == 'JutalomUgyek') {
        return this.$el.clientHeight - 87;
      } else {
        return this.$el.clientHeight / 2 - 82;
      }
    },
  },
  mounted() {
    this.currenturl = this.$route.name;
  },
  updated() {},
  beforeDestroy() {},
  watch: {
    fenyitesekVuex: {
      handler: function (value) {
        setTimeout(() => {
          this.fenyitesek = Object.freeze(value);
          this.$nextTick(() => {
            this.combined(true);
          });
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
.page-aside {
  position: absolute;
  top: 15px !important;
  /* z-index: 2000; */
  background: none;
  border-right: none;
  height: 100% !important;
}
.page-aside-chat {
  width: 255px !important;
}
.vuebar-element.chatscroll {
  height: calc(50vh - 166px);
  /*width: 100%;*/
  max-width: 100%;
  /*background: #dfe9fe;*/
}

.panel-title {
  font-size: 15px;
}

.panel-title span.badge {
  background-color: #f4cf5d;
}

.panel-heading p {
  font-size: 10px;
}

.panel {
  box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.1);
}
.page-aside .list-group-item {
  border-bottom: 1px solid #dee2e6;
  border-radius: 0;
  padding: 6px 0;
}
.page-aside .list-group-item:last-child {
  border-bottom: none;
}
.akt-nyilv-szam {
  color: #4f5960;
}
.page-aside .list-group-item:hover,
.page-aside .list-group-item:focus {
  color: unset;
}

.media .badge {
  font-size: 10px;
  padding: 2px 5px;
  color: #666;
  border-color: #ccc;
}

.media-body p {
  line-height: 14px;
}
.media-body .fogv-nev {
  /* max-width: 100px; */
  margin-bottom: 3px !important;
  /* word-break: break-all; */
}
.media-body {
  overflow: none !important;
}
.add-data {
  float: right;
  margin-left: 10px;
}
.media {
  position: relative;
}
.page-aside .list-group-item {
  overflow: unset;
  text-overflow: unset;
  white-space: unset;
}
</style>
