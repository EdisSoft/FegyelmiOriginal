<template>
  <div>
    <div class="page-header py-10 px-15 d-flex align-items-center">
      <h1 class="page-title text-dark mr-15 font-weight-normal">Események</h1>
      <span
        class="rounded-pill text-default border border-default font-size-12 px-10 py-5"
      >
        Vélhetően fogvatartott által elkövetett rendkívüli események listája.
        Egy kattintással megtekinthetjük a részleteit, és eljárást is
        kezdeményezhetünk.
      </span>
    </div>
    <div class="page-content pb-20 pb-sm-0 px-15">
      <div class="row">
        <div class="col-xxl-12 col-xl-12 col-lg-12 pr-10" v-if="defer(2)">
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
              <div class="panel-heading d-flex justify-content-between">
                <h3 class="panel-title pt-20 pb-0 pr-10 pl-30">
                  <div class="szurok">
                    <szuro-badgek
                      :badgek="szurok"
                      :selected.sync="selectedSzurok"
                      mapObj="fo"
                    >
                    </szuro-badgek>
                  </div>
                </h3>
                <div class="position-relative pt-20 pb-0 pr-30 pl-10">
                  <button
                    v-if="IntezetCheck()"
                    @click="UjEsemeny()"
                    type="button"
                    class="btn btn-outline btn-default btn-raised text-nowrap text-warning border border-warning"
                    id="uj-esemeny-btn"
                  >
                    <i class="icon wb-plus text-warning" aria-hidden="true"></i>
                    Új eseményt veszek fel
                  </button>
                </div>
              </div>
              <div
                class="panel-body panel-body pt-0 pb-sm-20 px-sm-20 pb-xl-30 px-xl-30"
              >
                <div class="pt-10">
                  <esemenyek-table
                    v-model="esemenyId"
                    :esemenyek="SzurtEsemenyek"
                  ></esemenyek-table>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- <div class="transparent-modal-bg" v-if="deferT(2)">
      <base-modal modalId="esemeny-rogzites" dialog-class="">
        <esemeny-rogzites></esemeny-rogzites>
      </base-modal>
    </div> -->
    <k-slide-panel-fegyelmi-ugy-reszletek
      ref="ugyreszletekSlidePanelRef"
      id="ugyReszletek"
    ></k-slide-panel-fegyelmi-ugy-reszletek>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import $ from 'jquery';
import moment from 'moment';
import {
  esemenyListStatusz,
  EsemenyStoreTypes,
} from '../store/modules/esemeny';
import Defer from '../mixins/Defer';
import { AppStoreTypes } from '../store/modules/app';
import settings from '../data/settings';
import { UserStoreTypes } from '../store/modules/user';
import { apiService, eventBus } from '../main';
import IFrameUrls from '../data/enums/iframeUrls';
import Intezetek from '../data/enums/intezetek';
import { timeout } from '../utils/common';
import { FegyelmiUgyFunctions } from '../functions/FegyelmiUgyFunctions';

export default {
  name: 'fegyelmiEsemenyek',
  data: function () {
    return {
      selectedSzurok: [],
      delayed: false,
      szuroProps: [
        {
          propId: 'Jelleg',
          propNev: 'Jelleg',
          order: 2,
          mapObj: 'fo',
          class: 'badge-outline badge-info',
          tooltip: 'Ügy jellege',
        },
      ],
      esemenyId: null,
    };
  },
  async mounted() {
    this.GetAdatok();
    setTimeout(() => {
      this.delayed = true;
    }, 250);
    if (this.$route.query.esemenyid) {
      var fegyelmiEsemeny = await apiService.GetEsemenyReszletek({
        id: this.$route.query.esemenyid,
      });
      var args = {
        esemenyId: fegyelmiEsemeny.EsemenyId,
      };
      eventBus.$emit('Sidebar:ugyReszletek', {
        state: false,
      });
      await timeout(50);
      eventBus.$emit('Modal:esemeny-rogzites', {
        state: true,
        data: args,
      });
      let query = Object.assign({}, this.$route.query);
      delete query.esemenyid;
      this.$router.replace({ query });
    }
  },
  methods: {
    async GetAdatok() {
      if (esemenyListStatusz.inited || esemenyListStatusz.loading) {
        return;
      }
      esemenyListStatusz.loading = true;
      try {
        await apiService.GetEsemenyek();
      } catch (error) {
        console.error(error);
      }
      esemenyListStatusz.loading = false;
    },
    async UjEsemeny() {
      var args = {
        esemenyId: null,
      };
      eventBus.$emit('Sidebar:ugyReszletek', {
        state: false,
      });
      await timeout(50);
      eventBus.$emit('Modal:esemeny-rogzites', {
        state: true,
        data: args,
      });
    },
    IntezetCheck() {
      var rogzitoIntezetId = this.userInfo.RogzitoIntezet.Id;
      var bvopId = Intezetek.BVOP;
      if (rogzitoIntezetId != bvopId) return true;
      else return false;
    },
  },
  computed: {
    ...mapGetters({
      esemenyek: EsemenyStoreTypes.getters.getEsemenyek,
      userInfo: UserStoreTypes.getters.getUserInfo,
      vanJogosultsaga: UserStoreTypes.getters.vanJogosultsaga,
    }),

    SzurtEsemenyek: function () {
      var jellegek = new Set();

      jellegek = new Set(this.selectedJellegek);

      var filteredList = [];
      var esemenyekTmp = this.esemenyek.slice();
      let selectedSzurok = this.selectedSzurok.map((m) => m.value);
      for (var i in esemenyekTmp) {
        var v = esemenyekTmp[i];

        if (FegyelmiUgyFunctions.ValidateSzurok(selectedSzurok, v)) {
          continue;
        }
        filteredList.push(v);
      }
      return filteredList;
    },
    szurok() {
      if (!this.delayed) {
        return [];
      }
      let szurtEsemenyek = this.SzurtEsemenyek;
      let szuroProps = this.szuroProps;
      let selectedSzurok = this.selectedSzurok;
      let szurok = FegyelmiUgyFunctions.CreateSzurok(
        szurtEsemenyek,
        szuroProps,
        this.selectedSzurok
      );

      return szurok;
    },
  },

  mixins: [Defer()],
};
</script>
<style scoped>
.dropdown-menu {
  width: 200px;
}

.ujuenyitesbtn,
.ujuenyitesbtn + .dataTables_filter {
  display: inline-block;
}
.page-header-actions {
  right: 20px;
}
.panel-title + div > button {
  color: #c5980f;
  border: solid 1px #f4cf5d;
}
.counter-number {
  font-size: 36px;
}
.pb-21 {
  padding-bottom: 21px !important;
}

@media (max-width: 1236px) {
  .vuebar-element.withheader {
    height: calc(100vh - 244px);
    max-width: 100%;
  }
}
/* .uj-fenyites i {
  -webkit-transition: -webkit-transform 0.3s ease-out;
  -moz-transition: -moz-transform 0.3s ease-out;
  transition: transform 0.3s ease-out;
}

.uj-fenyites:hover i {
  -webkit-transform: rotate(90deg);
  transform: rotate(90deg);
  line-height: 1;
  width: 22.5px;
  height: 15px;
  margin-left: 1px;
  margin-top: 3px;
  margin-bottom: 3px;
  margin-right: 0;
}

.uj-fenyites:hover {
  line-height: 0.5;
} */
</style>
