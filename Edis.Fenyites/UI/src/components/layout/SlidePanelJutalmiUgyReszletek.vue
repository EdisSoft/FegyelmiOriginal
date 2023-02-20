<template>
  <transition name="slidePanel">
    <div
      v-show="visible"
      v-click-outside="Hide"
      class="slidePanel slidePanel-right slidePanel-show w-p15 w-p50"
      style="overflow-y: auto"
      id="slidepanel-jutalmi-ugy"
    >
      <div class="slidePanel-content">
        <header class="slidePanel-header px-20 py-15 bg-blue-grey-400">
          <div
            class="slidePanel-header-tabs bg-white mb-10 px-20 py-10"
            v-if="slidepanelPlusBox"
          >
            <p class="mb-0">
              Tömeges rögzítés folyamatban. Kiválasztott ügyek:
            </p>

            <div
              class="nav-tabs-horizontal nav-tabs-animate position-relative"
              data-plugin="tabs"
            >
              <div class="d-flex justify-content-between nav-tabs-container">
                <ul class="nav nav-tabs nav-tabs-line" role="tablist">
                  <li
                    class="nav-item pointer"
                    role="presentation"
                    v-for="(jutalomUgySelected, idx) in jutalomUgyekSelected"
                    :key="jutalomUgySelected.Id"
                    @click="UpdateJutalmiUgyAdatok(jutalomUgySelected.Id, true)"
                  >
                    <a
                      :class="{ active: idx == 0 }"
                      class="nav-link pl-0 pb-1"
                      data-toggle="tab"
                      aria-controls="exampleTabsAnimateFadeOne"
                      role="tab"
                      aria-expanded="false"
                      aria-selected="false"
                    >
                      {{ jutalomUgySelected.RogzitesIdeje | toShortDate }}
                    </a>
                  </li>
                </ul>
              </div>
            </div>
            <div class="list-icon">
              <i class="wb-list"></i>
            </div>
          </div>
          <div
            class="slidePanel-actions"
            aria-label="actions"
            role="group"
            style="min-height: 40px"
          >
            <b-button
              type="button"
              class="btn btn-icon btn-pure slidePanel-close actions-top icon wb-close white py-0 px-0"
              @click="Hide()"
              variant="transparent"
              :disabled="isModalOpen"
              aria-hidden="true"
              :class="{ disabled: isModalOpen }"
            ></b-button>
          </div>
          <div v-if="jutalomUgy">
            <h1 class="white mb-5 slidepanel-title">
              Jutalmi ügy részletei <br />
              <span>
                <span v-if="jutalomUgy">{{ jutalomUgy.JutalmazasOka }}</span
                >&nbsp;
              </span>
            </h1>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Rögzítés ideje',
                html: true,
                container: '#slidepanel-jutalmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              >{{ jutalomUgy.RogzitesIdeje | toDateTime }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Jutalom típusa',
                html: true,
                container: '#slidepanel-jutalmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              >{{ jutalomUgy.JutalomTipusa }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Jutalmi ügy státusza',
                html: true,
                container: '#slidepanel-jutalmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              >{{ jutalomUgy.Statusz }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Jutalmi ügy határideje',
                html: true,
                container: '#slidepanel-jutalmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="getHatarido"
              >Határidő:
              {{ getHatarido | toShortDate }}
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Felfüggesztési javaslat',
                html: true,
                container: '#slidepanel-jutalmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="jutalomUgy.FelfuggesztesiJavaslat"
            >
              Felfüggesztési javaslat
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Felfüggesztve',
                html: true,
                container: '#slidepanel-jutalmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="jutalomUgy.Felfuggesztve"
            >
              Felfüggesztve
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Visszaküldve',
                html: true,
                container: '#slidepanel-jutalmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="jutalomUgy.Visszakuldve"
            >
              Visszaküldve
            </span>
            <span
              class="badge badge-outline badge-danger mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Jutalmi ügy lejárt',
                html: true,
                container: '#slidepanel-jutalmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="jutalomUgy.Csuszas > 0"
              >Lejárt: {{ jutalomUgy.Csuszas }} napja
            </span>
          </div>
        </header>
        <div
          class="slidePanel-inner position-relative w-p100 pl-20 pr-5"
          v-if="jutalomUgy"
        >
          <div
            class="vuebar-element slidePanelscroll"
            v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
          >
            <div>
              <div
                class="slidePanel-inner-section border-bottom-0 pt-15 pb-40 pr-5"
              >
                <div class="media mb-25" id="fogv-reszletek">
                  <div class="pr-0 pr-sm-20 align-self-center">
                    <div class="avatar bg-white img-bordered person-avatar">
                      <k-fogvatartott-kep
                        :id="jutalomUgy.FogvatartottId"
                      ></k-fogvatartott-kep>
                    </div>
                  </div>
                  <div class="media-body align-self-center">
                    <h5 class="mt-0 mb-5 text-default font-weight-400">
                      {{ jutalomUgy.NyilvantartasiSzam }}
                      {{ jutalomUgy.FogvatartottNev | camelCaseString }}
                      <!-- <small
                        >Szül.: 1974.06.27., Miskolc; anyja: Kertes Ilona</small
                      > -->
                      <!-- <p class="mt-10 mb-5 font-size-12"> -->
                      <small>
                        <span v-if="jutalomUgy.SzuletesiDatum">
                          Született:
                          {{ jutalomUgy.SzuletesiDatum | toShortDate }}
                          <span v-if="jutalomUgy.SzuletesiHely">
                            {{ jutalomUgy.SzuletesiHely }}</span
                          >
                        </span>
                        <span v-if="jutalomUgy.AnyjaNeve"
                          >, anyja neve: {{ jutalomUgy.AnyjaNeve }}
                        </span>
                      </small>
                      <!-- </p> -->
                    </h5>
                    <p>
                      <span
                        v-if="jutalomUgy.FogvatartottVegrehajtasiFok"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Végrehajtási fokozat',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ jutalomUgy.FogvatartottVegrehajtasiFok }}</span
                      >
                      <span
                        v-if="jutalomUgy.NyilvantartottStatusz != null"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Nyilvántartási státusz',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ jutalomUgy.NyilvantartottStatusz }}</span
                      >
                      <span
                        v-if="jutalomUgy.FogvatartottFogvatartasJellege"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Fogvatartás jellege',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ jutalomUgy.FogvatartottFogvatartasJellege }}
                      </span>
                      <span
                        v-if="jutalomUgy.Elhelyezes"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Elhelyezés',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ jutalomUgy.Elhelyezes }}</span
                      >
                    </p>
                    <div>
                      <div class="contextual-progress">
                        <div class="clearfix">
                          <div
                            class="progress-title blue-grey-500 font-weight-400 font-size-12"
                            v-if="jutalomUgy.ToltottIdoSzazalekban"
                          >
                            Letöltve - {{ jutalomUgy.ToltottIdoSzazalekban }} %
                          </div>
                        </div>
                        <b-progress
                          class="mb-5"
                          :value="toltottIdoSzazalekbanInt"
                          :max="100"
                          height="5px"
                          variant="warning"
                        ></b-progress>
                        <div class="clearfix">
                          <div
                            class="progress-title blue-grey-500 font-size-12"
                            v-if="jutalomUgy.IteletIdoSzovegesen"
                          >
                            {{ jutalomUgy.IteletIdoSzovegesen }}
                          </div>
                          <div
                            class="progress-label blue-grey-500 font-size-12"
                            v-if="jutalomUgy.TenylegesSzabadulasDatumaSzoveg"
                          >
                            {{ jutalomUgy.TenylegesSzabadulasDatumaSzoveg }}
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div
                  class="nav-tabs-horizontal nav-tabs-animate position-relative"
                  data-plugin="tabs"
                >
                  <div
                    class="d-flex justify-content-between nav-tabs-container"
                  >
                    <ul class="nav nav-tabs nav-tabs-line" role="tablist">
                      <li class="nav-item" role="presentation">
                        <a
                          class="nav-link active pl-1"
                          data-toggle="tab"
                          href="#naplo"
                          aria-controls="exampleTabsAnimateFadeOne"
                          role="tab"
                          aria-expanded="true"
                          aria-selected="true"
                        >
                          Jutalmi napló
                        </a>
                      </li>
                      <li
                        class="nav-item"
                        role="presentation"
                        v-for="osszevontesemeny in osszevontesemenyek"
                        :key="osszevontesemeny.JutalmiUgy.Id"
                      >
                        <a
                          class="nav-link pl-0"
                          data-toggle="tab"
                          :href="
                            '#osszevontesemeny_' +
                            osszevontesemeny.JutalmiUgy.Id
                          "
                          aria-controls="exampleTabsAnimateFadeOne"
                          role="tab"
                          aria-expanded="false"
                          aria-selected="false"
                        >
                          {{
                            osszevontesemeny.JutalmiUgy.RogzitesIdeje
                              | toShortDate
                          }}
                          <!-- /
                          {{ osszevontesemeny.JutalmiUgy.JutalomTipusa }} -->
                        </a>
                      </li>
                      <!-- <li class="nav-item" role="presentation">
                        <a
                          class="nav-link pl-0"
                          data-toggle="tab"
                          href="#nyomtatvanyok"
                          aria-controls="exampleTabsAnimateFadeOne"
                          role="tab"
                          aria-expanded="false"
                          aria-selected="false"
                          @click="IktatottNyomtatvanyokTabClick"
                        >
                          Iktatott nyomtatványok listája
                        </a>
                      </li>
                      <li class="nav-item" role="presentation">
                        <a
                          class="nav-link pl-0"
                          data-toggle="tab"
                          href="#csatolmanyok"
                          aria-controls="exampleTabsAnimateFadeOne"
                          role="tab"
                          aria-expanded="false"
                          aria-selected="false"
                          @click="CsatolmanyokTabClick"
                        >
                          Csatolmányok
                        </a>
                      </li> -->
                    </ul>
                    <b-dropdown
                      id="dropdown-dropleft"
                      dropleft
                      variant="dark"
                      class="pb-2 drop-cont"
                      no-caret
                      v-if="
                        jutalomUgy &&
                        !jutalomUgy.FoJutalmiUgyId &&
                        jutalomUgyMuveletekObj &&
                        jutalomUgy.Id > 0
                      "
                      toggle-class="btn-round btn-icon"
                      :disabled="isModalOpen"
                    >
                      <template v-slot:button-content>
                        <i class="icon wb-plus white" aria-hidden="true"></i>
                      </template>
                      <b-dropdown-item-button
                        v-for="opcionalisUgyek in jutalomUgyMuveletekObj.opcionalisUgyek"
                        :key="opcionalisUgyek.Id"
                        v-on:click="
                          OpenModal(
                            opcionalisUgyek.ModalIdToOpen,
                            opcionalisUgyek.ModalType,
                            opcionalisUgyek.FunctionToRun
                          )
                        "
                        :disabled="
                          !opcionalisUgyek.ModalIdToOpen &&
                          !opcionalisUgyek.FunctionToRun
                        "
                        ><span>{{
                          opcionalisUgyek.Text
                        }}</span></b-dropdown-item-button
                      >
                      <b-dropdown-item-button
                        v-for="fixUgyek in jutalomUgyMuveletekObj.fixUgyek"
                        :key="fixUgyek.Id"
                        :class="[fixUgyek.ColorClass]"
                        v-on:click="
                          OpenModal(
                            fixUgyek.ModalIdToOpen,
                            fixUgyek.ModalType,
                            opcionalisUgyek.FunctionToRun
                          )
                        "
                        :disabled="
                          !fixUgyek.ModalIdToOpen &&
                          !opcionalisUgyek.FunctionToRun
                        "
                      >
                        {{ fixUgyek.Text }}</b-dropdown-item-button
                      >
                    </b-dropdown>
                  </div>
                  <div class="tab-content">
                    <div
                      class="tab-pane fade show active"
                      id="naplo"
                      role="tabpanel"
                    >
                      <div
                        class="panel-group panel-group-continuous"
                        id="exampleAccordionContinuous"
                        aria-multiselectable="true"
                        role="tablist"
                        v-if="!isNaploBejegyzesLoading"
                      >
                        <component
                          v-for="naplobejegyzes in naplobejegyzesek"
                          :key="naplobejegyzes.Id"
                          :is="
                            GetNaplobejegyzesKomponensNev(
                              naplobejegyzes.TipusCimkeId
                            )
                          "
                          :naplobejegyzes="naplobejegyzes"
                          :jutalomUgy="jutalomUgy"
                          :esemeny="esemeny"
                        ></component>
                      </div>
                      <div v-else>
                        <div class="d-flex justify-content-center mt-4">
                          <b-spinner variant="secondary"></b-spinner>
                        </div>
                      </div>
                    </div>
                    <div
                      class="tab-pane fade show"
                      id="nyomtatvanyok"
                      role="tabpanel"
                    >
                      <div
                        class="panel-group panel-group-continuous"
                        id="exampleAccordionContinuous"
                        aria-multiselectable="true"
                        role="tablist"
                      >
                        <iktatott-nyomtatvanyok-table
                          ref="iktatottNyomtatvanyokTable"
                          :jutalomUgy="jutalomUgy"
                        ></iktatott-nyomtatvanyok-table>
                      </div>
                    </div>
                    <!-- <div
                      class="tab-pane fade show"
                      id="csatolmanyok"
                      role="tabpanel"
                    >
                      <k-csatolmanyok
                        :csatolmanyok="feltoltesek"
                      ></k-csatolmanyok>
                    </div> -->
                    <div
                      class="tab-pane fade show"
                      v-for="osszevontesemeny in osszevontesemenyek"
                      :key="osszevontesemeny.JutalmiUgy.Id"
                      :id="'osszevontesemeny_' + osszevontesemeny.JutalmiUgy.Id"
                      role="tabpanel"
                    >
                      <div
                        class="panel-group panel-group-continuous"
                        :id="'pg_osszevont_' + osszevontesemeny.JutalmiUgy.Id"
                        aria-multiselectable="true"
                        role="tablist"
                      >
                        <component
                          v-for="naplobejegyzes in osszevontesemeny.Naplo
                            .naplobejegyzesek"
                          :key="naplobejegyzes.Id"
                          :is="
                            GetNaplobejegyzesKomponensNev(
                              naplobejegyzes.TipusCimkeId
                            )
                          "
                          :naplobejegyzes="naplobejegyzes"
                          :jutalomUgy="osszevontesemeny.JutalmiUgy"
                          :esemeny="osszevontesemeny.Naplo.esemeny"
                        ></component>
                        <component
                          :is="GetNaplobejegyzesKomponensNev(-1)"
                          :jutalomUgy="osszevontesemeny.JutalmiUgy"
                          :esemeny="osszevontesemeny.Naplo.esemeny"
                        ></component>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <footer
        class="slidePanel-header p-15 pr-30 bg-blue-grey-400 position-fixed text-right"
      >
        <b-button
          @click="Hide()"
          class="btn-raised bg-blue-grey-600"
          :disabled="isModalOpen"
          :class="{ disabled: isModalOpen }"
          >Bezárás</b-button
        >
      </footer>
    </div>
  </transition>
</template>

<script>
import $ from 'jquery';
import { mapGetters, mapActions } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import settings from '../../data/settings';
import { async } from 'q';
import moment from 'moment';
import { required, minValue } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { debuglog } from 'util';
import { UserStoreTypes } from '../../store/modules/user';
import { JutalomUgyStoreTypes } from '../../store/modules/jutalomugy';
import ReintegraciosTisztDontesTipus from '../../data/enums/reintegraciosTisztDontesTipus';
import { AppStoreTypes } from '../../store/modules/app';
import { JutalmiUgyFunctions } from '../../functions/JutalmiUgyFunctions';
import Cimkek from '../../data/enums/Cimkek';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import { ConfirmModalFunctions } from '../../functions/ConfirmModalFunctions';
import { timeout } from '../../utils/common';
export default {
  name: 'k-slide-panel-jutalmi-ugy-reszletek',
  props: ['id'],
  data: function () {
    return {
      jutalomId: 0,
      jutalomIds: [],
      jutalomUgy: null,
      jutalomUgyElrendelesModalId: null,
      visible: false,
      canClose: true,
      isActive: false,
      isLoading: false,
      isNaploBejegyzesLoading: false,
      naplobejegyzesek: [],
      esemeny: null,
      osszevontesemenyek: [], //TODO
    };
  },
  mounted: function () {
    eventBus.$on('Sidebar:' + this.id, this.OnSidebarOpen);
    eventBus.$on('Sidebar:' + this.id + ':refresh', this.OnSideBarRefresh);
    eventBus.$on(
      'Sidebar:' + this.id + ':dokumentumokRefresh',
      this.OnDokumentumokRefresh
    );
    eventBus.$on('Sidebar:' + this.id + ':close', this.Hide);
  },
  beforeDestroy() {
    eventBus.$off('Sidebar:' + this.id, this.OnSidebarOpen);
    eventBus.$off('Sidebar:' + this.id + ':refresh', this.OnSideBarRefresh);
    eventBus.$off(
      'Sidebar:' + this.id + ':dokumentumokRefresh',
      this.OnDokumentumokRefresh
    );
    eventBus.$off('Sidebar:' + this.id + ':close', this.Hide);
  },
  methods: {
    ...mapActions({
      setStateVuex: AppStoreTypes.actions.setSlidePanelJutalmiUgyReszletekOpen,
    }),
    async OnSidebarOpen({ state, data }) {
      if (state) {
        data.jutalomIds = data.jutalomIds || [];
        console.log(
          `Sidebar jutalomId: ${data.jutalomId}, Ids: ${data.jutalomIds.join(
            ', '
          )}`,
          data
        );
        data.jutalomIds = data.jutalomIds || [];
        this.jutalomUgy = data.jutalomUgy;
        console.log('AAAA', this.jutalomUgy);
        this.jutalomId = data.jutalomId;
        this.jutalomIds = data.jutalomIds;
        if (data.jutalomIds && !data.jutalomId) {
          this.jutalomId = data.jutalomIds[0];
        }
        if (!data.jutalomIds || data.jutalomIds.length == 0) {
          this.jutalomIds = [data.jutalomId];
        }
        let canModalOpen = await JutalmiUgyFunctions.CanModalOpen(data, {
          jutalomIds: this.jutalomIds,
        });
        if (!canModalOpen) {
          return;
        }
        if (!data.dontShowSidePanel) {
          this.LoadJutalmiUgyAdatok(data.jutalomId);
          this.LoadOsszevontJutalmiUgyek(this.jutalomId);
        }

        if (data.modalName || data.functionToRun) {
          this.OpenModal(
            data.modalName,
            data.modalType,
            data.functionToRun,
            true
          );
        }
      } else {
        console.log('Sidebar:jutalmiUgyReszletek Hide');
        this.Hide();
      }
    },
    async OnSideBarRefresh() {
      console.log('Sidebar:' + this.id + ':refresh', this.jutalomId);
      let id = this.jutalomId;
      await timeout(250);
      this.UpdateJutalmiUgyAdatok(id);
    },
    async OnDokumentumokRefresh() {
      if (this.$refs.iktatottNyomtatvanyokTable) {
        this.$refs.iktatottNyomtatvanyokTable.GetDokumentumok(this.jutalomId);
      }
    },
    async OpenModal(
      modalName,
      modalType,
      functionToRun,
      modalOpenChecked = false
    ) {
      console.log('OpenModal');
      var args = {
        jutalomId: this.jutalomId, // ToDo: hotfix, később kiszedni
        jutalomIds: this.jutalomIds,
        modalType,
      };
      if (!modalOpenChecked) {
        let canModalOpen = await JutalmiUgyFunctions.CanModalOpen(modalName, {
          jutalomIds: this.jutalomIds,
        });
        if (!canModalOpen) {
          return;
        }
      }
      if (modalName) {
        eventBus.$emit('Modal:' + modalName, {
          state: true,
          data: args,
        });
      } else if (ConfirmModalFunctions[functionToRun] != null) {
        ConfirmModalFunctions[functionToRun](this.jutalomIds);
      } else {
        throw 'OpenModal - Nincs megadva modalName vagy functionToRun paraméter.';
      }
    },
    /*FanyCategory: function(event) {
        this.isActive = true;
      },*/
    LoadJutalmiUgyAdatok: async function (jutalomId) {
      this.isNaploBejegyzesLoading = true;
      // if (jutalomId < 0) {
      //   return;
      // }
      try {
        this.Show(jutalomId);
        this.naplobejegyzesek = [];
        this.esemeny = null;
        if (this.$refs.iktatottNyomtatvanyokTable) {
          this.$refs.iktatottNyomtatvanyokTable.GetDokumentumok(jutalomId);
        }
        let result = await apiService.GetNaploBejegyzesekByJutalomUgyId({
          jutalomId,
        });

        this.naplobejegyzesek = result.naplobejegyzesek;
        this.esemeny = result.esemeny;

        this.isNaploBejegyzesLoading = false;
      } catch (err) {
        let isAbort = NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok lekérdezése sikertelen',
          errorObj: err,
        });
        this.isNaploBejegyzesLoading = isAbort;
        console.log(err);
      }
    },
    LoadOsszevontJutalmiUgyek: async function (jutalomId) {
      try {
        this.osszevontesemenyek = [];
        let result = await apiService.GetOsszevontJutalmiUgyekForJutalmiUgy({
          jutalmiUgyId: jutalomId,
        });
        this.osszevontesemenyek = result;
      } catch (err) {
        let isAbort = NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Összevont ügyek lekérdezése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
    },
    async GetJutalmiUgyFromServer(jutalomId) {
      try {
        //TODO: Ennek egyet kellene visszaadni
        let result = await apiService.GetJutalomReszletei({
          jutalomId,
        });
        this.jutalomUgy = result;
      } catch (errorObj) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok lekérdezése sikertelen',
          errorObj,
        });
        console.log(errorObj);
      }
    },
    Show: function () {
      //this.url = url;
      this.visible = true;
      this.canClose = false;
      $('a[href="#naplo"]').trigger('click');
      this.setStateVuex({ value: true });
      setTimeout(() => {
        this.canClose = true;
      }, 350);
    },
    Hide: function () {
      //debugger;
      if (!this.canClose) {
        return;
      }
      if (this.visible) {
        $('.dataTable tr.active').removeClass('active');
      }
      this.setStateVuex({ value: false });
      this.visible = false;
      this.isActive = false;
      if (this.$v) {
        this.$v.$reset();
      }
    },
    StartLoader: function () {
      this.isLoading = true;
    },
    EndLoader: function () {
      this.isLoading = false;
    },
    OpenFegyelmiUgyElrendelesModal(id) {
      console.log('OpenFegyelmiUgyElrendelesModal', id);
      this.jutalomUgyElrendelesModalId = id;
      this.$root.$emit('bv::show::modal', 'fegyelmi-ugy-elrendelese');
    },
    OpenReintegraciosTisztDonteseModal(tipus) {
      let dontesTipus = 1;
      switch (tipus) {
        case 'feddes':
          dontesTipus = ReintegraciosTisztDontesTipus.Feddes;
          break;
        case 'visszakuldes':
          dontesTipus = ReintegraciosTisztDontesTipus.Visszakuldes;
          break;
        case 'kioktatas':
          dontesTipus = ReintegraciosTisztDontesTipus.Kioktatas;
          break;

        default:
          break;
      }
      this.OpenModal('reintegracios-tiszt-dontese', dontesTipus);
    },
    GetNaplobejegyzesKomponensNev(ugyTipus) {
      switch (parseInt(ugyTipus)) {
        case Cimkek.JutalmazasNaploTipus.Eloterjesztes:
          return 'eloterjesztes-naplobejegyzes';
        case Cimkek.JutalmazasNaploTipus.Dontes:
          return 'jutalom-dontes-naplobejegyzes';
        case Cimkek.JutalmazasNaploTipus.Javaslat:
          return 'jutalom-javaslat-naplobejegyzes';
        case Cimkek.JutalmazasNaploTipus.Velemeny:
          return 'jutalom-velemeny-naplobejegyzes';
        case Cimkek.JutalmazasNaploTipus.Osszevonas:
          return 'jutalom-osszevonas-naplobejegyzes';
        case Cimkek.JutalmazasNaploTipus.VegrehajtasAlatt:
          return 'jutalom-vegrehajtas-alatt-naplobejegyzes';
        case Cimkek.JutalmazasNaploTipus.Vegrehajtva:
          return 'jutalom-vegrehajtva-naplobejegyzes';
        case Cimkek.JutalmazasNaploTipus.AutomatikusLezaras:
          return 'jutalom-automatikus-lezaras-naplobejegyzes';
        case Cimkek.JutalmazasNaploTipus.Kihirdetve:
          return 'jutalom-kihirdetve-naplobejegyzes';
        default:
          console.error(`Ügytípushoz nem található komponens: ${ugyTipus}`);
          return null;
      }
    },
    UpdateJutalmiUgyAdatok(id, updateFromServer = true) {
      this.LoadJutalmiUgyAdatok(id);

      if (updateFromServer) {
        //console.log('updateFromServer', id);
        this.GetJutalmiUgyFromServer(id);
      } else {
        this.jutalomUgy = this.jutalomUgyek.filter(
          (x) => x.JutalmiUgyId == id
        )[0];
        this.jutalomId = id;
        //console.log('nem updateFromServer', this.jutalomUgy);
      }
      this.LoadOsszevontJutalmiUgyek(id);
    },
    IktatottNyomtatvanyokTabClick: function () {
      this.OnDokumentumokRefresh();
    },
    CsatolmanyokTabClick: function () {
      // korábbi analitika helye
    },
  },
  computed: {
    ...mapGetters({
      jutalomUgyek: JutalomUgyStoreTypes.getters.getJutalomUgyek,
      // jutalomUgyek: JutalomUgyStoreTypes.getters.getJutalomUgyek,
      // dokumentumok: AppStoreTypes.getters.getDokumentumok,
      // vedoDokumentumok: AppStoreTypes.getters.getVedoDokumentumok,
      isModalOpen: AppStoreTypes.getters.isModalOpen,
      userInfo: UserStoreTypes.getters.getUserInfo,
      jutalomUgyekSelected:
        JutalomUgyStoreTypes.getters.getJutalomUgyekSelected,
    }),
    getHatarido() {
      if (this.jutalomUgy == null) {
        return null;
      } else {
        return this.jutalomUgy.Hatarido;
      }
    },
    // isTargyalasraVar() {
    //   if (!this.jutalomUgy) {
    //     return false;
    //   }
    //   return (
    //     (this.jutalomUgy.UgyStatusz == Cimkek.FegyelmiUgyStatusza &&
    //       !this.fegyelmiugy.ElsofokuTargyalasIdopontja) ||
    //     (this.jutalomUgy.UgyStatuszId ==
    //       Cimkek.FegyelmiUgyStatusza.IIFokuTargyalas &&
    //       !this.jutalomUgy.MasodfokuTargyalasIdopontja)
    //   );
    // },
    jutalomUgyMuveletekObj() {
      if (!this.jutalomUgy) {
        return null;
      }
      let jutalomUgyMuveletekObj =
        JutalmiUgyFunctions.GetJutalmiUgyMuveletekAsObject([this.jutalomUgy]);
      return jutalomUgyMuveletekObj;
    },
    feltoltesek() {
      if (!this.esemeny) {
        return [];
      }
      return this.esemeny.Feltoltesek;
    },
    slidepanelPlusBox() {
      if (!this.jutalomIds) return false;
      if (this.jutalomIds.length > 1) return true;
      return false;
      //return this.jutalomUgyekSelected.some(
      //  f => f.JutalmiUgyId == this.jutalomId
      //);
    },
    //jutalomId() {
    //  if (!this.jutalomUgy) {
    //    return null;
    //  }

    //  return this.jutalomUgy.JutalmiUgyId;
    //},
    toltottIdoSzazalekbanInt() {
      if (this.jutalomUgy && this.jutalomUgy.ToltottIdoSzazalekban) {
        return parseInt(this.jutalomUgy.ToltottIdoSzazalekban, 10);
      }
      return 0;
    },
  },
  watch: {
    isActive: {
      handler: function (value) {
        if (value) {
          //this.StartLoader();
        }
      },
      immediate: true,
    },
    jutalomId: {
      handler: function (value) {
        if (value) {
        }
      },
      immediate: true,
    },
  },
  destroyed: function () {},
};
</script>
<style scoped>
.slide-overlay {
  background: #92929257;
  position: fixed;
  left: 0;
  z-index: 999;
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

.slidePanel {
  width: 700px !important;
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

.slidePanel header {
  z-index: 99;
  position: sticky;
  top: 0;
}
.slidePanel footer {
  bottom: 0;
  width: 700px;
}

.slidePanel .list-group li:last-child {
  margin-bottom: 25px;
}

.slidePanel .popover button.close {
  opacity: 1;
}

.avatar {
  width: 120px;
}

.progress-bar {
  background-color: rgba(118, 131, 143, 0.9);
}
.progress-xs {
  height: 4px !important;
  border-radius: 1px;
}

.nav-tabs-line .nav-link.active,
.nav-tabs-line .nav-link.active:hover,
.nav-tabs-line .nav-link.active:focus,
.nav-tabs-line .nav-item.show .nav-link,
.nav-tabs-line .nav-item.show .nav-link:hover,
.nav-tabs-line .nav-item.show .nav-link:focus,
.nav-tabs-line .nav-item.open .nav-link,
.nav-tabs-line .nav-item.open .nav-link:hover,
.nav-tabs-line .nav-item.open .nav-link:focus {
  color: #8349f5;
  background-color: transparent;
  border-bottom: 2px solid #8349f5;
}
í .nav-tabs-horizontal .dropdown {
  position: absolute;
  right: 0px;
  top: 0px;
  z-index: 2;
}
.slidePanel-header-tabs {
  box-shadow: 2px 2px 6px rgba(0, 0, 0, 0.4);
  margin-left: -20px;
  position: relative;
}
.slidePanel-header-tabs p {
  color: #c79d1a;
  font-size: 16px;
  text-shadow: 1px 1px 4px rgba(0, 0, 0, 0.2);
}
.slidePanel-header-tabs .nav-item a {
  font-size: 12px;
}
.slidePanel-header-tabs .nav-item .active,
.slidePanel-header-tabs .nav-item .active:hover {
  color: #c79d1a;
}
.slidePanel-header-tabs .list-icon {
  position: absolute;
  left: -20px;
  bottom: 0;
}
@media (max-width: 1200px) and (min-width: 1000px) {
  .slidePanel {
    width: 540px !important;
    -webkit-transition: width 1s;
    transition: width 1s;
  }
  .slidePanel footer {
    bottom: 0;
    width: 540px;
    -webkit-transition: width 1s;
    transition: width 1s;
  }
}
</style>
