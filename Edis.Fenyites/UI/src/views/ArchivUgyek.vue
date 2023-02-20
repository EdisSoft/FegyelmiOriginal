<template>
  <div>
    <div class="page-header py-10 px-15 d-flex align-items-center">
      <h1 class="page-title text-dark mr-15 font-weight-normal">
        Archív ügyek
      </h1>
      <span
        class="rounded-pill text-default border border-default font-size-12 px-10 py-5"
      >
        Fogvatartottak ellen kezdeményezett és elrendelt fegyelmi ügyek listája
      </span>
    </div>

    <div class="page-content pb-20 pb-sm-0 px-15">
      <div class="row">
        <div class="col-xxl-12 col-xl-12 col-lg-12 pr-10">
          <div
            class="vuebar-element withheader mb-xl-0"
            ref="vuebarscroll"
            v-bar="{
              preventParentScroll: true,
              scrollThrottle: 30,
              resizeRefresh: true,
            }"
          >
            <div class="panel mb-0">
              <div
                class="panel-body badge-container-selected-wrapper p-sm-10 p-xl-20"
              >
                <div class="szurok">
                  <szuro-badgek
                    :badgek="szurok"
                    :selected.sync="selectedSzurok"
                    mapObj="fo"
                  >
                  </szuro-badgek>
                </div>
                <div class="pt-10">
                  <fegyelmi-ugyek-table
                    ref="fegyTable"
                    :fegyelmiUgyek="SzurtFegyelmiUgyek"
                    :isArchive="true"
                  ></fegyelmi-ugyek-table>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <k-slide-panel-fegyelmi-ugy-reszletek
      ref="ugyreszletekSlidePanelRef"
      id="ugyReszletek"
    ></k-slide-panel-fegyelmi-ugy-reszletek>
    <k-slide-panel-with-fogvatartott-adatok
      ref="fanySlidePanelWithFogvatartottAdatokRef"
      id="fanyFogvatartottAdatok"
    ></k-slide-panel-with-fogvatartott-adatok>
    <!-- A modalok az App.vue-ban vannak -->
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import $ from 'jquery';
import moment from 'moment';
import Defer from '../mixins/Defer';
import { UserStoreTypes } from '../store/modules/user';
import { getUgyszam } from '../utils/fenyitesUtils';
import { eventBus, apiService } from '../main';
import FegyelmiUgyekTable from '../components/tables/FegyelmiUgyekTable';

import { FegyelmiUgyFunctions } from '../functions/FegyelmiUgyFunctions';
import { AppStoreTypes } from '../store/modules/app';
import StatisztikaSzurok from '../data/enums/statisztikaSzurok';
import Cimkek from '../data/enums/Cimkek';
import { ConfirmModalFunctions } from '../functions/ConfirmModalFunctions';
import { NotificationFunctions } from '../functions/notificationFunctions';
import Intezetek from '../data/enums/intezetek';

export default {
  name: 'fegyelmiUgyek',
  data: function() {
    return {
      fegyelmiUgyek: [],
      selectedUgyStatuszok: [],
      selectedSzurok: [],
      targyalasraVarSzures: false,
      delayed: false,
    };
  },
  async mounted() {
    await this.$nextTick();
    var parentwidth = $('.badge-container-selected-wrapper').width();
    $('.badge-container-selected').width(parentwidth);
    setTimeout(() => {
      this.delayed = true;
    }, 250);
    eventBus.$on('IntezetValasztas', this.OnIntezetValasztas);
  },
  beforeDestroy() {
    eventBus.$off('IntezetValasztas', this.OnIntezetValasztas);
  },
  methods: {
    ...mapActions({}),
    OnIntezetValasztas() {
      this.GetArchivFegyelmiUgyek(this.ugyEve);
    },
    GetUgyszam(f) {
      return getUgyszam(f);
    },
    UgyStatuszKigyujtes: function(datalist) {
      var tagsObj = {};
      $(datalist).each(function(i, v) {
        if (tagsObj.hasOwnProperty(v.UgyStatusz)) {
          tagsObj[v.UgyStatusz].count++;
        } else {
          tagsObj[v.UgyStatusz] = {
            id: v.UgyStatuszId,
            name: v.UgyStatusz,
            count: 1,
          };
        }
      });
      var list = [];
      for (var index in tagsObj) {
        list.push(tagsObj[index]);
      }
      return list;
    },
    TargyalasKituzesreVarKigyujtes: function(datalist) {
      var counter = 0;
      $(datalist).each(function(i, v) {
        if (
          (v.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IFokuTargyalas &&
            !v.ElsofokuTargyalasIdopontja) ||
          (v.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IIFokuTargyalas &&
            !v.MasodfokuTargyalasIdopontja)
        ) {
          counter++;
        }
      });
      var list = [];
      if (counter > 0) {
        list.push({
          id: 1,
          name: 'Tárgyalás kitűzésre vár',
          count: counter,
        });
      }

      return list;
    },
    JellegekKigyujtes: function(datalist) {
      var tagsObj = {};
      $(datalist).each(function(i, v) {
        if (v.Jelleg) {
          if (tagsObj.hasOwnProperty(v.Jelleg)) {
            tagsObj[v.Jelleg].count++;
          } else {
            tagsObj[v.Jelleg] = {
              id: v.Jelleg,
              name: v.Jelleg,
              count: 1,
            };
          }
        }
      });
      var list = [];
      for (var index in tagsObj) {
        list.push(tagsObj[index]);
      }
      return list;
    },
    ChangeUgyStatuszokSelectedTag(id, name, isKonaliticsEnabled = true) {
      var index = this.selectedUgyStatuszok.findIndex(x => x.id == id);
      if (index == -1) {
        this.selectedUgyStatuszok.push({ id, name });
      } else {
        this.selectedUgyStatuszok.splice(index, 1);
      }
      if (this.$refs.fegyTable) {
        $(this.$refs.fegyTable.$refs.datatable.$el)
          .DataTable()
          .search('');
      }
    },
    ChangeTargyalasKituzesreVarSelectedTag(isKonaliticsEnabled = true) {
      this.targyalasraVarSzures = !this.targyalasraVarSzures;
      if (this.$refs.fegyTable) {
        $(this.$refs.fegyTable.$refs.datatable.$el)
          .DataTable()
          .search('');
      }
    },
    Contains: function(list, value) {
      return list.find(x => x.id == value) != null;
    },

    async GetArchivFegyelmiUgyek(ev) {
      try {
        let result = await apiService.GetFegyelmiUgyekArchiv({ ev: ev });
        this.fegyelmiUgyek = Object.freeze(result);
      } catch (error) {
        console.log(error);
        NotificationFunctions.AjaxError({
          title: 'Archív ügyek',
          text: 'Ügyek letöltése sikertelen',
          errorObj: error,
        });
      }
    },
    ResetSelectedBadges() {
      this.selectedUgyStatuszok = [];
    },
  },
  computed: {
    ...mapGetters({
      userInfo: UserStoreTypes.getters.getUserInfo,
      archivEvek: UserStoreTypes.getters.archivEvek,
      vanJogkorGyakorloJoga: UserStoreTypes.getters.vanJogkorGyakorloJoga,
      vanReintegraciosTisztJoga:
        UserStoreTypes.getters.vanReintegraciosTisztJoga,
      isModalOpen: AppStoreTypes.getters.isModalOpen,
    }),
    SzurtFegyelmiUgyek: function() {
      if (!this.delayed) {
        // return [];
      }

      var ugyStatuszok = new Set();
      ugyStatuszok = new Set(this.selectedUgyStatuszok.map(m => m.id));

      var filteredList = [];
      var fegyelmiUgyekTmp = this.fegyelmiUgyek.slice();
      let userId = this.$get(this.userInfo, 'SzemelyzetSid');
      var selectedSzurok = this.selectedSzurok.map(m => m.value);
      for (var i in fegyelmiUgyekTmp) {
        var v = fegyelmiUgyekTmp[i];
        if (FegyelmiUgyFunctions.ValidateSzurok(selectedSzurok, v)) {
          continue;
        }
        filteredList.push(v);
      }
      return filteredList;
    },
    szuroProps() {
      return FegyelmiUgyFunctions.GetFegyelmiUgySzuroBadgek(this.isBvop);
    },
    isBvop() {
      if (!this.userInfo) {
        return false;
      }
      return this.userInfo.RogzitoIntezet.Id == Intezetek.BVOP;
    },
    szurok() {
      let szurtFegyelmiUgyek = this.SzurtFegyelmiUgyek;

      let szuroProps = this.szuroProps;
      let selectedSzurok = this.selectedSzurok;
      let szurok = FegyelmiUgyFunctions.CreateSzurok(
        szurtFegyelmiUgyek,
        szuroProps,
        this.selectedSzurok
      );
      // eslint-disable-next-line
      this.szurokKey++;
      return szurok;
    },
    ugyEve() {
      return this.$route.query.ev || this.archivEvek[0];
    },
  },
  components: {
    FegyelmiUgyekTable: FegyelmiUgyekTable,
  },
  watch: {
    '$route.query': {
      handler: function(value) {
        let ev = value.ev || null;
        this.GetArchivFegyelmiUgyek(ev);
      },
      immediate: true,
    },
  },
  mixins: [Defer()],
};
</script>
<style scoped>
.szurok {
  transition: 0.5s;
}
</style>
