<template>
  <transition name="slidePanel">
    <div
      v-show="visible"
      v-click-outside="Hide"
      class="slidePanel slidePanel-right slidePanel-show w-p15 w-p50"
      style="overflow-y: auto;"
      id="slidepanel-fegyelmi-ugy"
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
                    v-for="(fegyelmiUgySelected, idx) in fegyelmiUgyekSelected"
                    :key="fegyelmiUgySelected.FegyelmiUgyId"
                    @click="
                      UpdateFegyelmiUgyAdatok(
                        fegyelmiUgySelected.FegyelmiUgyId,
                        false
                      )
                    "
                  >
                    <a
                      :class="{ active: idx == 0 }"
                      class="nav-link pl-0 pb-1"
                      data-toggle="tab"
                      aria-controls="exampleTabsAnimateFadeOne"
                      role="tab"
                      aria-expanded="false"
                      aria-selected="false"
                      >{{ fegyelmiUgySelected.UgyIntezetAzon }}/{{
                        fegyelmiUgySelected.UgyEve
                      }}/{{ fegyelmiUgySelected.UgySzama }}
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
            style="min-height: 40px;"
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
          <div v-if="fegyelmiUgy">
            <h1 class="white mb-5 slidepanel-title">
              {{ fegyelmiUgy.UgyIntezetAzon }}/{{ fegyelmiUgy.UgyEve }}/{{
                fegyelmiUgy.UgySzama
              }}
              számú fegyelmi ügy naplója <br />
              <span>
                <span v-if="fegyelmiUgy">{{ fegyelmiUgy.Jelleg }}</span
                >&nbsp;
              </span>
            </h1>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Esemény napja és ideje',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              >{{ fegyelmiUgy.EsemenyDatuma | toDateTime }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Fegyelmi ügy típusa',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              >{{ fegyelmiUgy.FegyelmiUgyTipus }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Fegyelmi ügy státusza',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              >{{ fegyelmiUgy.UgyStatusz }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Fegyelmi ügy határideje',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
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
                title: 'Jogi képviseletet kért',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.VanJogiKepviselet"
            >
              Jogi képviseletet kért
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Felfüggesztési javaslat',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.FelfuggesztesiJavaslat"
            >
              Felfüggesztési javaslat
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Felfüggesztve',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.Felfuggesztve"
            >
              Felfüggesztve
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Határidő módosítási javaslat',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.HataridoModositasJavaslat"
            >
              Határidő módosítási javaslat
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Tárgyalás kitűzésre vár',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="isTargyalasraVar"
            >
              Tárgyalás kitűzésre vár
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Visszaküldve',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.Visszakuldve"
            >
              Visszaküldve
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Szállításra előjegyezve',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.SzallitasraElojegyezve"
            >
              Szállításra előjegyezve
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Szakterületi véleményre vár',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.SzakteruletiVelemenyreVarFL"
            >
              Szakterületi véleményre vár
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Közvetítői eljárás kezdeményezve',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.KozvetitoiEljarasKezdemenyezve"
            >
              Közvetítői eljárás kezdeményezve
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Közvetítői eljárás',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.KozvetitoiEljarasban"
            >
              Közvetítői eljárásban
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Elkülönítés',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.FegyelmiElkulonitesFL"
            >
              Elkülönítve
            </span>
            <span
              v-if="fegyelmiUgy.KarteritesAzonosito"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Kártérítési adatok',
                html: true,
                container: '#fogv-reszletek',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              >{{ fegyelmiUgy.KarteritesAzonosito }} -
              {{ fegyelmiUgy.KarteritesStatusz }}
            </span>
            <span
              class="badge badge-outline badge-danger mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Fegyelmi ügy lejárt',
                html: true,
                container: '#slidepanel-fegyelmi-ugy',
                delay: { show: 500, hide: 100 },
                trigger: 'hover',
              }"
              v-if="fegyelmiUgy.Csuszas > 0"
              >Lejárt: {{ fegyelmiUgy.Csuszas }} napja
            </span>
          </div>
        </header>
        <div
          class="slidePanel-inner position-relative w-p100 pl-20 pr-5"
          v-if="fegyelmiUgy"
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
                        :id="fegyelmiUgy.FogvatartottId"
                      ></k-fogvatartott-kep>
                    </div>
                  </div>
                  <div class="media-body align-self-center">
                    <h5 class="mt-0 mb-5 text-default font-weight-400">
                      {{ fegyelmiUgy.AktNyilvantartasiSzam }}
                      {{ fegyelmiUgy.FogvatartottNev | camelCaseString }}
                      <!-- <small
                        >Szül.: 1974.06.27., Miskolc; anyja: Kertes Ilona</small
                      > -->
                      <!-- <p class="mt-10 mb-5 font-size-12"> -->
                      <small>
                        <span v-if="fegyelmiUgy.SzuletesiDatum">
                          Született:
                          {{ fegyelmiUgy.SzuletesiDatum | toShortDate }}
                          <span v-if="fegyelmiUgy.SzuletesiHely">
                            {{ fegyelmiUgy.SzuletesiHely }}</span
                          >
                        </span>
                        <span v-if="fegyelmiUgy.AnyjaNeve"
                          >, anyja neve: {{ fegyelmiUgy.AnyjaNeve }}
                        </span>
                      </small>
                      <!-- </p> -->
                    </h5>
                    <p>
                      <span
                        v-if="fegyelmiUgy.FogvatartottVegrehajtasiFok"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Végrehajtási fokozat',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ fegyelmiUgy.FogvatartottVegrehajtasiFok }}</span
                      >
                      <span
                        v-if="fegyelmiUgy.NyilvantartottStatusz != null"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Nyilvántartási státusz',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ fegyelmiUgy.NyilvantartottStatusz }}</span
                      >
                      <span
                        v-if="fegyelmiUgy.FogvatartottFogvatartasJellege"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Fogvatartás jellege',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ fegyelmiUgy.FogvatartottFogvatartasJellege }}
                      </span>
                      <!-- <span
                        v-if="fegyelmiUgy.KarteritesAzonosito"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Kártérítési adatok',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ fegyelmiUgy.KarteritesAzonosito }} -
                        {{ fegyelmiUgy.KarteritesStatusz }}
                      </span> -->
                      <span
                        v-if="fegyelmiUgy.Elhelyezes"
                        class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                        v-b-tooltip="{
                          title: 'Elhelyezés',
                          html: true,
                          container: '#fogv-reszletek',
                          delay: { show: 500, hide: 100 },
                          trigger: 'hover',
                        }"
                        >{{ fegyelmiUgy.Elhelyezes }}/{{
                          fegyelmiUgy.Korlet
                        }}/{{ fegyelmiUgy.Zarka }}</span
                      >
                    </p>
                    <div>
                      <div class="contextual-progress">
                        <div class="clearfix">
                          <div
                            class="progress-title blue-grey-500 font-weight-400 font-size-12"
                            v-if="fegyelmiUgy.ToltottIdoSzazalekban"
                          >
                            Letöltve - {{ fegyelmiUgy.ToltottIdoSzazalekban }} %
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
                            v-if="fegyelmiUgy.IteletIdoSzovegesen"
                          >
                            {{ fegyelmiUgy.IteletIdoSzovegesen }}
                          </div>
                          <div
                            class="progress-label blue-grey-500 font-size-12"
                            v-if="fegyelmiUgy.TenylegesSzabadulasDatumaSzoveg"
                          >
                            {{ fegyelmiUgy.TenylegesSzabadulasDatumaSzoveg }}
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
                          Fegyelmi napló
                        </a>
                      </li>
                      <li
                        class="nav-item"
                        role="presentation"
                        v-for="osszevontesemeny in osszevontesemenyek"
                        :key="osszevontesemeny.FegyelmiUgy.FegyelmiUgyId"
                      >
                        <a
                          class="nav-link pl-0"
                          data-toggle="tab"
                          :href="
                            '#osszevontesemeny_' +
                              osszevontesemeny.FegyelmiUgy.FegyelmiUgyId
                          "
                          aria-controls="exampleTabsAnimateFadeOne"
                          role="tab"
                          aria-expanded="false"
                          aria-selected="false"
                        >
                          {{ osszevontesemeny.FegyelmiUgy.UgyIntezetAzon }}/{{
                            osszevontesemeny.FegyelmiUgy.UgyEve
                          }}/{{ osszevontesemeny.FegyelmiUgy.UgySzama }}
                        </a>
                      </li>
                      <li class="nav-item" role="presentation">
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
                      </li>
                    </ul>
                    <b-dropdown
                      id="dropdown-dropleft"
                      dropleft
                      variant="dark"
                      class="pb-2 drop-cont"
                      no-caret
                      v-if="fegyelmiUgy && fegyelmiUgyMuveletekObj"
                      toggle-class="btn-round btn-icon"
                      :disabled="isModalOpen"
                    >
                      <template v-slot:button-content>
                        <i class="icon wb-plus white" aria-hidden="true"></i>
                      </template>
                      <b-dropdown-item-button
                        v-for="opcionalisUgyek in fegyelmiUgyMuveletekObj.opcionalisUgyek"
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
                        v-for="fixUgyek in fegyelmiUgyMuveletekObj.fixUgyek"
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
                          :fegyelmiUgy="fegyelmiUgy"
                          :esemeny="esemeny"
                        ></component>
                        <component
                          :is="GetNaplobejegyzesKomponensNev(-1)"
                          :fegyelmiUgy="fegyelmiUgy"
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
                          :fegyelmiUgy="fegyelmiUgy"
                        ></iktatott-nyomtatvanyok-table>
                      </div>
                    </div>
                    <div
                      class="tab-pane fade show"
                      id="csatolmanyok"
                      role="tabpanel"
                    >
                      <k-csatolmanyok
                        :csatolmanyok="feltoltesek"
                      ></k-csatolmanyok>
                    </div>
                    <div
                      class="tab-pane fade show"
                      v-for="osszevontesemeny in osszevontesemenyek"
                      :key="osszevontesemeny.FegyelmiUgy.FegyelmiUgyId"
                      :id="
                        'osszevontesemeny_' +
                          osszevontesemeny.FegyelmiUgy.FegyelmiUgyId
                      "
                      role="tabpanel"
                    >
                      <div
                        class="panel-group panel-group-continuous"
                        :id="
                          'pg_osszevont_' +
                            osszevontesemeny.FegyelmiUgy.FegyelmiUgyId
                        "
                        aria-multiselectable="true"
                        role="tablist"
                      >
                        <!-- ide jön a napló -->

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
                          :fegyelmiUgy="osszevontesemeny.FegyelmiUgy"
                          :esemeny="osszevontesemeny.Naplo.esemeny"
                        ></component>
                        <component
                          :is="GetNaplobejegyzesKomponensNev(-1)"
                          :fegyelmiUgy="osszevontesemeny.FegyelmiUgy"
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
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import ReintegraciosTisztDontesTipus from '../../data/enums/reintegraciosTisztDontesTipus';
import { AppStoreTypes } from '../../store/modules/app';
import { FegyelmiUgyFunctions } from '../../functions/FegyelmiUgyFunctions';
import Cimkek from '../../data/enums/Cimkek';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import { ConfirmModalFunctions } from '../../functions/ConfirmModalFunctions';
import { timeout } from '../../utils/common';
export default {
  name: 'k-slide-panel-fegyelmi-ugy-reszletek',
  props: ['id'],
  data: function() {
    return {
      fegyelmiUgyId: 0,
      fegyelmiUgyIds: [],
      fegyelmiUgy: null,
      fegyelmiUgyElrendelesModalId: null,
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
  mounted: function() {
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
      setStateVuex: AppStoreTypes.actions.setSlidePanelFegyelmiUgyReszletekOpen,
    }),
    async OnSidebarOpen({ state, data }) {
      if (state) {
        data.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        console.log(
          `Sidebar fegyelmiUgyId: ${
            data.fegyelmiUgyId
          }, Ids: ${data.fegyelmiUgyIds.join(', ')}`,
          data
        );
        data.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        this.fegyelmiUgy = data.fegyelmiUgy;
        this.fegyelmiUgyId = data.fegyelmiUgyId;
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        if (data.fegyelmiUgyIds && !data.fegyelmiUgyId) {
          this.fegyelmiUgyId = data.fegyelmiUgyIds[0];
        }
        if (!data.fegyelmiUgyIds || data.fegyelmiUgyIds.length == 0) {
          this.fegyelmiUgyIds = [data.fegyelmiUgyId];
        }
        let canModalOpen = await FegyelmiUgyFunctions.CanModalOpen(
          data.modalName,
          { fegyelmiUgyIds: this.fegyelmiUgyIds }
        );
        if (!canModalOpen) {
          return;
        }
        this.LoadFegyelmiUgyAdatok(data.fegyelmiUgyId);
        this.LoadOsszevontFegyelmiUgyek(this.fegyelmiUgyId);
        if (data.modalName || data.functionToRun) {
          this.OpenModal(
            data.modalName,
            data.modalType,
            data.functionToRun,
            true
          );
        }
      } else {
        console.log('Sidebar:ugyReszletek Hide');
        this.Hide();
      }
    },
    async OnSideBarRefresh() {
      console.log('Sidebar:' + this.id + ':refresh', this.fegyelmiUgyId);
      let id = this.fegyelmiUgyId;
      await timeout(250);
      this.UpdateFegyelmiUgyAdatok(id);
    },
    async OnDokumentumokRefresh() {
      if (this.$refs.iktatottNyomtatvanyokTable) {
        this.$refs.iktatottNyomtatvanyokTable.GetDokumentumok(
          this.fegyelmiUgyId
        );
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
        fegyelmiUgyId: this.fegyelmiUgyId, // ToDo: hotfix, később kiszedni
        fegyelmiUgyIds: this.fegyelmiUgyIds,
        modalType,
      };
      if (!modalOpenChecked) {
        let canModalOpen = await FegyelmiUgyFunctions.CanModalOpen(modalName, {
          fegyelmiUgyIds: this.fegyelmiUgyIds,
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
        ConfirmModalFunctions[functionToRun](this.fegyelmiUgyIds);
      } else {
        throw 'OpenModal - Nincs megadva modalName vagy functionToRun paraméter.';
      }
    },
    /*FanyCategory: function(event) {
        this.isActive = true;
      },*/
    LoadFegyelmiUgyAdatok: async function(fegyelmiUgyId) {
      this.isNaploBejegyzesLoading = true;
      try {
        this.Show(fegyelmiUgyId);
        this.naplobejegyzesek = [];
        this.esemeny = null;
        if (this.$refs.iktatottNyomtatvanyokTable) {
          this.$refs.iktatottNyomtatvanyokTable.GetDokumentumok(fegyelmiUgyId);
        }
        let result = await apiService.GetNaploBejegyzesekByFegyelmiUgyId({
          fegyelmiUgyId,
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
    LoadOsszevontFegyelmiUgyek: async function(fegyelmiUgyId) {
      try {
        this.osszevontesemenyek = [];
        let result = await apiService.GetOsszevontFegyelmiUgyekForFegyelmiUgy({
          fegyelmiUgyId,
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
    async GetFegyelmiUgyFromServer(fegyelmiUgyId) {
      try {
        let result = await apiService.GetFegyelmiUgyListItemViewModelById({
          fegyelmiUgyId,
        });
        this.fegyelmiUgy = result;
      } catch (errorObj) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok lekérdezése sikertelen',
          errorObj,
        });
        console.log(errorObj);
      }
    },
    Show: function() {
      //this.url = url;
      this.visible = true;
      this.canClose = false;
      $('a[href="#naplo"]').trigger('click');
      this.setStateVuex({ value: true });
      setTimeout(() => {
        this.canClose = true;
      }, 350);
    },
    Hide: function() {
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
    StartLoader: function() {
      this.isLoading = true;
    },
    EndLoader: function() {
      this.isLoading = false;
    },
    OpenFegyelmiUgyElrendelesModal(id) {
      console.log('OpenFegyelmiUgyElrendelesModal', id);
      this.fegyelmiUgyElrendelesModalId = id;
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
        case -1:
          return 'esemeny-felvetel-naplobejegyzes';
        case Cimkek.NaploTipus.UgyKezdemenyezese:
          return 'ugy-kezdemenyezes-naplobejegyzes';
        case Cimkek.NaploTipus.UgyMegtagadasa:
          return 'ugy-megtagadasa-naplobejegyzes';
        case Cimkek.NaploTipus.ElrendelemAzEljarasLefolytatasat:
          return 'ugy-elrendelese-naplobejegyzes';
        case Cimkek.NaploTipus.ReintegraciosTisztiJogkorbeUtalom:
          return 'ugy-reintegracios-jogkorbe-utalas-naplobejegyzes';
        case Cimkek.NaploTipus.FogvatartottJogiKepviseletetKer:
          return 'jogi-kepviselet-naplobejegyzes';
        case Cimkek.NaploTipus.Feddes:
          return 'feddes-naplobejegyzes';
        case Cimkek.NaploTipus.Kioktatas:
          return 'kioktatas-naplobejegyzes';
        case Cimkek.NaploTipus.Visszakuldes:
          return 'visszakuldes-naplobejegyzes';
        case Cimkek.NaploTipus.TanuMeghallgatas:
        case Cimkek.NaploTipus.EljarasAlaVontMeghallgatasa:
          return 'tanu-meghallgatasi-jegyzokonyv-naplobejegyzes';
        case Cimkek.NaploTipus.SzemelyiAllomanyiTanuMeghallgatas:
          return 'szemelyi-allomanyi-tanu-meghallgatasi-jegyzokonyv-naplobejegyzes';
        case Cimkek.NaploTipus.HataridoModositasiJavaslat:
          return 'hatarido-modositasi-javaslat-naplobejegyzes';
        case Cimkek.NaploTipus.FelfuggesztesiJavaslat:
          return 'felfuggesztesi-javaslat-naplobejegyzes';
        case Cimkek.NaploTipus.OsszefoglaloJelentes:
          return 'osszefogalo-jelentes-naplobejegyzes';
        case Cimkek.NaploTipus.HelysziniSzemle:
          return 'helyszini-szemle-naplobejegyzes';
        case Cimkek.NaploTipus.SzakteruletiVelemeny:
          return 'szakteruleti-velemeny-naplobejegyzes';
        case Cimkek.NaploTipus.TagyalasElokeszitese:
          return 'elso-foku-targyalas-kituzese-naplo-bejegyzes';
        case Cimkek.NaploTipus.I_fokuTargyalasiJegyzokonyv:
          return 'elso-foku-targyalasi-jegyzokonyv-naplobejegyzes';
        case Cimkek.NaploTipus.UgyOsszevonasa:
          return 'ugy-osszevonas-naplobejegyzes';
        case Cimkek.NaploTipus.MasodfokuTargyalasElokeszites:
          return 'masod-foku-targyalas-kituzese-naplo-bejegyzes';
        case Cimkek.NaploTipus.HatarozatRogzitese:
          return 'hatarozat-rogzitese-naplobejegyzes';
        case Cimkek.NaploTipus.KirendeltVedoKerese:
          return 'kirendelt-vedo-kerese-naplobejegyzes';
        case Cimkek.NaploTipus.MeghatalmazottVedoKerese:
          return 'meghatalmazott-vedo-kerese-naplobejegyzes';
        case Cimkek.NaploTipus.HataridoModositas:
          return 'hatarido-modositas-naplobejegyzes';
        case Cimkek.NaploTipus.Felfuggesztes:
          return 'felfuggesztes-naplo-bejegyzes';
        case Cimkek.NaploTipus.VedoTelefonosTajekoztatasa:
          return 'vedo-telefonos-tajekoztatasa-naplobejegyzes';
        case Cimkek.NaploTipus.FelfuggesztesMegszuntetese:
          return 'felfuggesztes-megszuntetese-naplobejegyzes';
        case Cimkek.NaploTipus.SzakteruletiVelemenyKerese:
          return 'szakteruleti-velemeny-kerese-naplobejegyzes';
        case Cimkek.NaploTipus.MasodfokuTargyalasiJegyzokonyv:
          return 'masod-foku-targyalasi-jegyzokonyv-naplobejegyzes';
        case Cimkek.NaploTipus.HatarozatRogziteseMasodFokon:
          return 'hatarozat-rogzitese-masod-fokon-naplo-bejegyzes';
        case Cimkek.NaploTipus.UjEljarasLefolytatasa:
          return 'uj-eljaras-lefolytatasa-naplobejegyzes';
        case Cimkek.NaploTipus.SzembesitesiJegyzokonyv:
          return 'szembesitesi-jegyzokonyv-naplobejegyzes';
        case Cimkek.NaploTipus.JogiKepviseletVisszavonasa:
          return 'jogi-kepviselet-visszavonasa-naplo-bejegyzes';
        case Cimkek.NaploTipus.MaganelzarasMegkezdesenekRogzitese:
          return 'maganelzaras-megkezdesenek-rogzitese-naplobejegyzes';
        case Cimkek.NaploTipus.FenyitesVegrehajthatatlannaTetele:
          return 'fenyites-vegrehajthatatlanna-tetele-naplobejegyzes';
        case Cimkek.NaploTipus.MaganelzarasIdeiglenesenEllenjavallt:
          return 'maganelzaras-ideiglenesen-ellenjavallt-naplobejegyzes';
        case Cimkek.NaploTipus.MaganelzarasMegszakitasa:
          return 'maganelzaras-megszakitasa-naplobejegyzes';
        case Cimkek.NaploTipus.MaganelzarasVegrehajtva:
          return 'maganelzaras-vegrehajtva-naplobejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasKezdemenyezese:
          return 'kozvetitoi-eljaras-kezdemenyezese-naplobejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasElrendeles:
          return 'kozvetitoi-eljaras-elrendeles-naplobejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasMegtagadas:
          return 'kozvetitoi-eljaras-megtagadas-naplobejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasFeljegyzes:
          return 'kozvetitoi-eljaras-feljegyzes-naplobejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasMegallapodas:
          return 'kozvetitoi-eljaras-megallapodas-naplobejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasMegallapodasTeljesult:
          return 'kozvetitoi-eljaras-megallapodas-teljesult-naplobejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasLezaras:
          return 'kozvetitoi-eljaras-lezarasa-naplobejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasIndoklassalMegszuntetes:
          return 'kozvetitoi-eljaras-indoklassal-megszuntetes-naplobejegyzes';
        case Cimkek.NaploTipus.FegyelmiElkulonitesElrendelese:
          return 'fegyelmi-elkulonites-elrendelese-naplo-bejegyzes';
        case Cimkek.NaploTipus.FegyelmiElkulonitesVegrehajtva:
          return 'fegyelmi-elkulonites-vegrehajtva-naplo-bejegyzes';
        case Cimkek.NaploTipus.HataridoTullepesMiattiMegszuntetesNaplobejegyzes:
          return 'hatarido-tullepes-miatti-megszuntetes-naplobejegyzes';
        case Cimkek.NaploTipus.BuntetoFeljelentesRogziteseNaploBejegyzes:
          return 'bunteto-feljelentes-rogzitese-naplo-bejegyzes';
        case Cimkek.NaploTipus.KozvetitoiEljarasHataridoModositasNaplobejegyzes:
          return 'kozvetitoi-eljaras-hatarido-modositas-naplobejegyzes';
        case Cimkek.NaploTipus
          .KozvetitoiEljarasHataridoModositasKereseNaplobejegyzes:
          return 'kozvetitoi-eljaras-hatarido-modositas-kerese-naplobejegyzes';
        case Cimkek.NaploTipus.MaganelzarasElrendelese:
          return 'maganelzaras-elrendelese-naplobejegyzes';
        case Cimkek.NaploTipus.FenyitesVegrehajtasaKesz:
          return 'fenyites-vegrehajtasa-kesz-naplobejegyzes';
        case Cimkek.NaploTipus.VegrehajthatosagElevultNaplobejegyzes:
          return 'fegyelmi-ugy-vegrehajthatosag-elevult-naplobejegyzes';
        case Cimkek.NaploTipus.AutomatikusLezaras:
          return 'automatikus-lezaras-naplobejegyzes';
        case Cimkek.NaploTipus.AutomatikusVegrehajtasMegkezdese:
          return 'automatikus-vegrehajtas-megkezdes-naplobejegyzes';
        case Cimkek.NaploTipus.AutomatikusVegrehajtasBefejezese:
          return 'automatikus-vegrehajtas-befejezes-naplobejegyzes';
        case Cimkek.NaploTipus.KivizsgaloModositasa:
          return 'kivizsgalo-modositasa-naplobejegyzes';
        case Cimkek.NaploTipus.SzabadszovegesNaplobejegyzes:
          return 'szabadszavas-fegyelmi-naplobejegyzes';
        default:
          console.error(`Ügytípushoz nem található komponens: ${ugyTipus}`);
          return null;
      }
    },
    UpdateFegyelmiUgyAdatok(id, updateFromServer = true) {
      this.LoadFegyelmiUgyAdatok(id);

      if (updateFromServer) {
        //console.log('updateFromServer', id);
        this.GetFegyelmiUgyFromServer(id);
      } else {
        this.fegyelmiUgy = this.fegyelmiUgyek.filter(
          x => x.FegyelmiUgyId == id
        )[0];
        this.fegyelmiUgyId = id;
        //console.log('nem updateFromServer', this.fegyelmiUgy);
      }
      this.LoadOsszevontFegyelmiUgyek(id);
    },
    IktatottNyomtatvanyokTabClick: function() {
      this.OnDokumentumokRefresh();
    },
    CsatolmanyokTabClick: function() {},
  },
  computed: {
    ...mapGetters({
      fegyelmiUgyek: FegyelmiUgyStoreTypes.getters.getFegyelmiUgyek,
      // fegyelmiUgyek: FegyelmiUgyStoreTypes.getters.getFegyelmiUgyek,
      // dokumentumok: AppStoreTypes.getters.getDokumentumok,
      // vedoDokumentumok: AppStoreTypes.getters.getVedoDokumentumok,
      isModalOpen: AppStoreTypes.getters.isModalOpen,
      userInfo: UserStoreTypes.getters.getUserInfo,
      fegyelmiUgyekSelected:
        FegyelmiUgyStoreTypes.getters.getFegyelmiUgyekSelected,
    }),
    getHatarido() {
      if (this.fegyelmiUgy == null) return null;

      if (
        this.fegyelmiUgy.UgyStatuszId ==
          Cimkek.FegyelmiUgyStatusza.ReintegraciosTisztiJogkorben ||
        this.fegyelmiUgy.UgyStatuszId ==
          Cimkek.FegyelmiUgyStatusza.KivizsgalasFolyamatban
      ) {
        return this.fegyelmiUgy.KivizsgalasiHatarido;
      } else {
        return this.fegyelmiUgy.Hatarido;
      }
    },
    isTargyalasraVar() {
      if (!this.fegyelmiUgy) {
        return false;
      }
      return (
        (this.fegyelmiUgy.UgyStatusz == Cimkek.FegyelmiUgyStatusza &&
          !this.fegyelmiugy.ElsofokuTargyalasIdopontja) ||
        (this.fegyelmiUgy.UgyStatuszId ==
          Cimkek.FegyelmiUgyStatusza.IIFokuTargyalas &&
          !this.fegyelmiUgy.MasodfokuTargyalasIdopontja)
      );
    },
    fegyelmiUgyMuveletekObj() {
      if (!this.fegyelmiUgy) {
        return null;
      }
      let fegyelmiUgyMuveletekObj = FegyelmiUgyFunctions.GetFegyelmiUgyMuveletekAsObject(
        [this.fegyelmiUgy]
      );
      return fegyelmiUgyMuveletekObj;
    },
    feltoltesek() {
      if (!this.esemeny) {
        return [];
      }
      return this.esemeny.Feltoltesek;
    },
    slidepanelPlusBox() {
      if (!this.fegyelmiUgyIds) return false;
      if (this.fegyelmiUgyIds.length > 1) return true;
      return false;
      //return this.fegyelmiUgyekSelected.some(
      //  f => f.FegyelmiUgyId == this.fegyelmiUgyId
      //);
    },
    //fegyelmiUgyId() {
    //  if (!this.fegyelmiUgy) {
    //    return null;
    //  }

    //  return this.fegyelmiUgy.FegyelmiUgyId;
    //},
    toltottIdoSzazalekbanInt() {
      if (this.fegyelmiUgy && this.fegyelmiUgy.ToltottIdoSzazalekban) {
        return parseInt(this.fegyelmiUgy.ToltottIdoSzazalekban, 10);
      }
      return 0;
    },
  },
  watch: {
    isActive: {
      handler: function(value) {
        if (value) {
          //this.StartLoader();
        }
      },
      immediate: true,
    },
    fegyelmiUgyId: {
      handler: function(value) {
        if (value) {
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
