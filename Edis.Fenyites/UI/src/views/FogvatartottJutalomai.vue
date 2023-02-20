<template>
  <div>
    <div class="page-header py-10 px-15 d-flex align-items-center">
      <h1 class="page-title text-dark mr-15 font-weight-normal">
        {{ fogvatartottNev }} ügyei
      </h1>
      <span
        class="rounded-pill text-default border border-default font-size-12 px-10 py-5"
      >
        A lenti listában a fogvatartott teljes jutalmi helyzete elérhető
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
                  <jutalom-ugyek-table
                    ref="jutTable"
                    :jutalomUgyek="SzurtFegyelmiUgyek"
                    :isFogvKereso="true"
                  ></jutalom-ugyek-table>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <k-slide-panel-jutalmi-ugy-reszletek
      ref="jutalmiUgyreszletekSlidePanelRef"
      id="jutalmiUgyReszletek"
    ></k-slide-panel-jutalmi-ugy-reszletek>
    <!-- A modalok az App.vue-ban vannak -->
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import $ from 'jquery';
import moment from 'moment';
import Defer from '../mixins/Defer';
import { UserStoreTypes } from '../store/modules/user';
import { eventBus, apiService } from '../main';
import FegyelmiUgyekTable from '../components/tables/FegyelmiUgyekTable';

import { AppStoreTypes } from '../store/modules/app';
import StatisztikaSzurok from '../data/enums/statisztikaSzurok';
import Cimkek from '../data/enums/Cimkek';
import { ConfirmModalFunctions } from '../functions/ConfirmModalFunctions';
import { NotificationFunctions } from '../functions/notificationFunctions';
import Intezetek from '../data/enums/intezetek';
import { FogvatartottakStoreTypes } from '../store/modules/fogvatartottak';
import { FegyelmiUgyStoreTypes } from '../store/modules/fegyelmiugy';
import { JutalmiUgyFunctions } from '../functions/JutalmiUgyFunctions';
import { FegyelmiUgyFunctions } from '../functions/FegyelmiUgyFunctions';

export default {
  name: 'fogvatartottJutalomai',
  data: function() {
    return {
      fegyelmiUgyek: [],
      selectedUgyStatuszok: [],
      selectedSzurok: [],
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
      this.GetFogvatartottJutalmai(this.$route.query.fogvatartottId);
    },
    async GetFogvatartottJutalmai(fogvatartottId) {
      try {
        let result = await apiService.GetFogvatartottJutalmai({
          fogvatartottId,
        });
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
  },
  computed: {
    ...mapGetters({
      userInfo: UserStoreTypes.getters.getUserInfo,
      fegyelmiUgyekVuex: FegyelmiUgyStoreTypes.getters.getFegyelmiUgyek,
    }),
    szuroProps() {
      return JutalmiUgyFunctions.GetJutalomSzuroBadgek();
    },
    SzurtFegyelmiUgyek: function() {
      if (!this.delayed) {
        // return [];
      }
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
    fogvatartottNev() {
      let selectedFogvatartott = this.$store.getters[
        FogvatartottakStoreTypes.fogvatartottKereses.get
      ];
      if (selectedFogvatartott) {
        return selectedFogvatartott.text;
      }
      return 'Fogvatartott';
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
  },
  components: {},
  watch: {
    '$route.query': {
      handler: function(value) {
        let fogvatartottId = value.fogvatartottId || null;
        this.GetFogvatartottJutalmai(fogvatartottId);
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
