<template>
  <div>
    <div class="page-header py-10 px-15 d-flex align-items-center">
      <h1 class="page-title text-dark mr-15 font-weight-normal">
        Jutalmi ügyek
      </h1>
      <span
        class="rounded-pill text-default border border-default font-size-12 px-10 py-5"
      >
        Folyamatban lévő jutalmai ügyek listája. Egy kattintással
        megtekinthetjük a részleteit, és új ügyet is kezdeményezhetünk.
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
                <transition name="fade" tag="div">
                  <div
                    class="pb-2 pb-sm-0 badge-container-selected"
                    v-show="jutalomUgyekSelected.length > 0"
                  >
                    <div class="panel mb-0">
                      <div class="panel-body px-4 py-2">
                        <div class="row">
                          <div class="col-10 d-flex align-items-center">
                            <transition-group
                              name="list-complete"
                              tag="div"
                              class="d-flex flex-wrap"
                            >
                              <span
                                v-for="jutalom in jutalomUgyekSelected"
                                :key="jutalom.Id"
                                class="list-complete-item badge badge-outline mr-2 my-1 pointer badge-ugy-selected blue-grey-500 font-italic py-0"
                              >
                                <span
                                  class="checkbox-custom checkbox-default mr-2 my-0 py-1 px-0"
                                  @click="
                                    RemoveJutalomUgySelected({
                                      value: jutalom.Id,
                                    })
                                  "
                                >
                                  <input
                                    type="checkbox"
                                    id="inputCheckbox"
                                    name="inputCheckbox"
                                    checked
                                  />
                                  <label for="inputCheckbox"></label> </span
                                ><span
                                  class="ml-4"
                                  @click="
                                    UgyReszletekMegtekintes(
                                      jutalom.Id,
                                      null,
                                      null,
                                      jutalom
                                    )
                                  "
                                  >{{ GetUgyszam(jutalom) }}</span
                                >
                              </span>
                            </transition-group>
                          </div>
                          <div class="col-2 text-right">
                            <b-dropdown
                              id="dropdown-dropleft"
                              dropleft
                              variant="outline-dark"
                              class="pb-0"
                              no-caret
                              v-if="jutalmiUgyekSelectedDropdown"
                              toggle-class="btn-round btn-icon"
                              :disabled="isModalOpen"
                            >
                              <template v-slot:button-content>
                                <i
                                  class="icon wb-more-horizontal"
                                  aria-hidden="true"
                                ></i>
                              </template>
                              <b-dropdown-item-button
                                v-for="opcionalisUgyek in jutalmiUgyekSelectedDropdown.opcionalisUgyek"
                                :key="opcionalisUgyek.Id"
                                v-on:click="
                                  UgyReszletekMegtekintesMultiple(
                                    opcionalisUgyek
                                  )
                                "
                                :disabled="
                                  !opcionalisUgyek.ModalIdToOpen &&
                                  !opcionalisUgyek.FunctionToRun
                                "
                              >
                                <span :class="[opcionalisUgyek.ColorClass]">
                                  {{ opcionalisUgyek.Text }}
                                </span>
                              </b-dropdown-item-button>
                              <b-dropdown-divider
                                v-if="
                                  jutalmiUgyekSelectedDropdown.opcionalisUgyek
                                    .length != 0
                                "
                              ></b-dropdown-divider>
                              <b-dropdown-item-button
                                v-for="fixUgyek in jutalmiUgyekSelectedDropdown.fixUgyek"
                                :key="fixUgyek.Id"
                                :class="[fixUgyek.ColorClass]"
                                v-on:click="
                                  UgyReszletekMegtekintesMultiple(
                                    opcionalisUgyek
                                  )
                                "
                                :disabled="
                                  !opcionalisUgyek.ModalIdToOpen &&
                                  !opcionalisUgyek.FunctionToRun
                                "
                              >
                                {{ fixUgyek.Text }}
                              </b-dropdown-item-button>
                            </b-dropdown>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </transition>
                <div
                  class="d-flex"
                  :style="{
                    'margin-top':
                      jutalomUgyekSelected.length > 0 ? '70px !important' : '0',
                  }"
                >
                  <div class="szurok">
                    <szuro-badgek
                      :badgek="szurok"
                      :selected.sync="selectedSzurok"
                      mapObj="fo"
                    >
                    </szuro-badgek>
                  </div>

                  <div class="position-relative pb-0 pr-30 pl-10">
                    <button
                      v-if="IntezetCheck()"
                      @click="JutalomEloterjesztes()"
                      type="button"
                      class="btn btn-outline btn-default btn-raised text-nowrap text-warning border border-warning"
                      id="uj-esemeny-btn"
                    >
                      <i
                        class="icon wb-plus text-warning"
                        aria-hidden="true"
                      ></i>
                      Jutalom előterjesztése
                    </button>
                  </div>
                </div>
                <div
                  class="panel-body panel-body pt-0 pb-sm-20 px-sm-20 pb-xl-30 px-xl-30"
                >
                  <div class="pt-10">
                    <jutalom-ugyek-table
                      v-model="jutalomId"
                      :jutalomUgyek="SzurtJutalomUgyek"
                    ></jutalom-ugyek-table>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="transparent-modal-bg" v-if="deferT(2)">
        <base-modal modalId="esemeny-rogzites" dialog-class="">
          <esemeny-rogzites></esemeny-rogzites>
        </base-modal>
      </div>
      <k-slide-panel-fegyelmi-ugy-reszletek
        ref="ugyreszletekSlidePanelRef"
        id="ugyReszletek"
      ></k-slide-panel-fegyelmi-ugy-reszletek>
      <k-slide-panel-jutalmi-ugy-reszletek
        ref="jutalmiUgyreszletekSlidePanelRef"
        id="jutalmiUgyReszletek"
      ></k-slide-panel-jutalmi-ugy-reszletek>
    </div>
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';
import $ from 'jquery';
import moment from 'moment';
import { EsemenyStoreTypes } from '../store/modules/esemeny';
import { JutalomUgyStoreTypes } from '../store/modules/jutalomugy';
import Defer from '../mixins/Defer';
import { AppStoreTypes } from '../store/modules/app';
import settings from '../data/settings';
import { UserStoreTypes } from '../store/modules/user';
import { apiService, eventBus } from '../main';
import IFrameUrls from '../data/enums/iframeUrls';
import Intezetek from '../data/enums/intezetek';
import { timeout } from '../utils/common';
import { JutalmiUgyFunctions } from '../functions/JutalmiUgyFunctions';

export default {
  name: 'jutalomUgyek',
  data: function () {
    return {
      selectedSzurok: [],
      delayed: false,
      jutalomId: null,
    };
  },
  mounted: async function () {
    this.$store.dispatch(JutalomUgyStoreTypes.actions.setJutalomUgyekSelected, {
      value: [],
    });
    await this.$nextTick();
    var parentwidth = $('.badge-container-selected-wrapper').width();
    console.log(parentwidth);
    $('.badge-container-selected').width(parentwidth);
    setTimeout(() => {
      this.delayed = true;
    }, 250);

    if (this.$route.query.ugyid) {
      var ugy = await apiService.GetJutalomUgyListItemViewModelById({
        jutalomId: this.$route.query.ugyid,
      });
      eventBus.$emit('Sidebar:jutalmiUgyReszletek', {
        state: true,
        data: {
          jutalomUgy: ugy,
          jutalomId: ugy.JutalomId,
        },
      });
      let query = Object.assign({}, this.$route.query);
      delete query.ugyid;
      this.$router.replace({ query });
    }
  },
  methods: {
    ...mapActions({
      RemoveJutalomUgySelected:
        JutalomUgyStoreTypes.actions.removeJutalomUgySelected,
      SetJutalomUgyekSelected:
        JutalomUgyStoreTypes.actions.setJutalomUgyekSelected,
    }),
    async JutalomEloterjesztes() {
      var args = {
        ugyId: null,
      };
      eventBus.$emit('Sidebar:jutalmiUgyReszletek', {
        state: false,
      });
      await timeout(50);
      eventBus.$emit('Modal:jutalom-eloterjesztes', {
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
    UgyReszletekMegtekintes(jutalomId, modalName, modalType, jutalomUgy) {
      var args = { jutalomId, modalName, modalType, jutalomUgy };

      eventBus.$emit('Sidebar:jutalmiUgyReszletek', {
        state: true,
        data: args,
      });
    },
    GetUgyszam(j) {
      let date = this.$options.filters.toDateTime(j.RogzitesIdeje);
      return `${date} ${j.JutalomTipusa}`;
    },
    UgyReszletekMegtekintesMultiple({
      ModalIdToOpen,
      ModalType,
      Options,
      FunctionToRun,
      DontShowSidePanel,
    }) {
      let ids = this.jutalomUgyekSelected.map((m) => m.Id);
      var args = {
        jutalomId: ids[0],
        jutalomIds: ids,
        modalName: ModalIdToOpen,
        modalType: ModalType,
        functionToRun: FunctionToRun,
        dontShowSidePanel: DontShowSidePanel,
        jutalomUgy: this.jutalomUgyekSelected[0],
      };

      if (ModalIdToOpen || FunctionToRun) {
        eventBus.$emit('Sidebar:jutalmiUgyReszletek', {
          state: true,
          data: args,
        });
      }
    },
  },
  computed: {
    ...mapGetters({
      jutalomUgyek: JutalomUgyStoreTypes.getters.getJutalomUgyek,
      jutalomUgyekSelected:
        JutalomUgyStoreTypes.getters.getJutalomUgyekSelected,
      userInfo: UserStoreTypes.getters.getUserInfo,
      vanJogosultsaga: UserStoreTypes.getters.vanJogosultsaga,
      isModalOpen: AppStoreTypes.getters.isModalOpen,
    }),
    szuroProps() {
      return JutalmiUgyFunctions.GetJutalomSzuroBadgek(this.isBvop);
    },
    isBvop() {
      if (!this.userInfo) {
        return false;
      }
      return this.userInfo.RogzitoIntezet.Id == Intezetek.BVOP;
    },
    SzurtJutalomUgyek: function () {
      var jellegek = new Set();

      jellegek = new Set(this.selectedJellegek);

      var filteredList = [];
      var jutalomUgyekTmp = this.jutalomUgyek.slice();
      let selectedSzurok = this.selectedSzurok.map((m) => m.value);
      for (var i in jutalomUgyekTmp) {
        var v = jutalomUgyekTmp[i];

        if (JutalmiUgyFunctions.ValidateSzurok(selectedSzurok, v)) {
          continue;
        }
        filteredList.push(v);
      }
      return filteredList;
    },
    jutalmiUgyekSelectedDropdown() {
      let fegyelmiUgyMuveletekObj =
        JutalmiUgyFunctions.GetJutalmiUgyMuveletekAsObject(
          this.jutalomUgyekSelected
        );
      return fegyelmiUgyMuveletekObj;
    },
    szurok() {
      if (!this.delayed) {
        return [];
      }
      let szurtJutalomUgyek = this.SzurtJutalomUgyek;
      let szuroProps = this.szuroProps;
      let selectedSzurok = this.selectedSzurok;
      let szurok = JutalmiUgyFunctions.CreateSzurok(
        szurtJutalomUgyek,
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
