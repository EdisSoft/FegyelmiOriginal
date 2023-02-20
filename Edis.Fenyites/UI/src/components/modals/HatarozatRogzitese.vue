<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-grey">
      <div class="modal-header">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title">
          17. Határozat rögzítése fenyítéssel vagy megszüntetéssel
          <p class="mt-10 mb-0 font-size-12">
            Fenyítés kiszabása és megszűntetés
          </p>
        </h4>
      </div>
      <div class="modal-body pt-20 pl-25 pb-40 pr-5">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <!-- <div class="slidePanel-inner-section border-bottom-0 py-15 pr-10"> -->
            <div class="row pr-5">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.ElsofokonHatarozathozoSzemely"
                >
                  <input
                    type="text"
                    class="form-control"
                    disabled
                    v-model="
                      $v.formData.values.ElsofokonHatarozathozoSzemely.$model
                    "
                  />
                  <span class="text-help float-right"
                    >Fegyelmi jogkör gyakorlója</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.VetsegRendeletSzerintCimkeId"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2VetsegRendeletSzerintOptions"
                    v-model="formData.values.VetsegRendeletSzerintCimkeId"
                  ></k-select2>
                  <span class="text-help float-right"
                    >Vétség rendelet szerint</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FegyelmiVetsegTipusaCimkeId"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2FegyelmiVetsegTipusaOptions"
                    v-model="
                      $v.formData.values.FegyelmiVetsegTipusaCimkeId.$model
                    "
                  ></k-select2>
                  <span class="text-help float-right"
                    >Fegyelmi vétség típusa</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div
                :class="['col-md-' + isFenyitesTipusaMezoSzelesseg]"
                class="longtext-fix"
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesTipusaCimkeId"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2FenyitesTipusaOptions"
                    v-model="formData.values.FenyitesTipusaCimkeId"
                    :disabled="isHatarozatMegszunteteseModal"
                  ></k-select2>
                  <span class="text-help float-right"
                    >Fenyítés típusa/Megszüntetés</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div
                class="col-md-3"
                v-show="isFenyitesTipusaKietkezesCsokkentes"
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesHosszaEsTipusa"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2FenyitesTartamaKietkezesCsokkentesOptions"
                    v-model="formData.values.FenyitesHosszaEsTipusa"
                  ></k-select2>
                  <span class="text-help float-right">Fenyítés tartama</span>
                </k-vuelidate-error-extractor>
              </div>
              <div
                class="col-md-2"
                v-show="isFenyitesTipusaKietkezesCsokkentes"
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.KietkezesCsokkentes"
                >
                  <input
                    type="number"
                    class="form-control"
                    v-model="$v.formData.values.KietkezesCsokkentes.$model"
                    style="height: 2.1rem !important"
                  />
                  <span class="text-help float-right">Csökkentés mértéke</span>
                </k-vuelidate-error-extractor>
              </div>
              <div
                class="col-md-5"
                v-show="
                  isFenyitesTipusaMaganalTarthatoTargyakKorenekKorlatozasa
                "
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesHosszaEsTipusa"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="
                      Select2FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions
                    "
                    v-model="formData.values.FenyitesHosszaEsTipusa"
                  ></k-select2>
                  <span class="text-help float-right">Fenyítés tartama</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-5" v-show="isFenyitesTipusaMaganelzaras">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesHosszaEsTipusa"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2FenyitesTartamaMaganElzarasOptions"
                    v-model="formData.values.FenyitesHosszaEsTipusa"
                  ></k-select2>
                  <span class="text-help float-right">Fenyítés tartama</span>
                </k-vuelidate-error-extractor>
              </div>
              <div
                class="col-md-5"
                v-show="
                  isFenyitesTipusaBvIntProgramokonValoReszvetelKorlatozasa
                "
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesHosszaEsTipusa"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="
                      Select2FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions
                    "
                    v-model="formData.values.FenyitesHosszaEsTipusa"
                  ></k-select2>
                  <span class="text-help float-right">Fenyítés tartama</span>
                </k-vuelidate-error-extractor>
              </div>
              <div
                class="col-md-5"
                v-show="isFenyitesTipusaBvIntProgramokonValoReszvetelTiltasa"
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesHosszaEsTipusa"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="
                      Select2FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions
                    "
                    v-model="formData.values.FenyitesHosszaEsTipusa"
                  ></k-select2>
                  <span class="text-help float-right">Fenyítés tartama</span>
                </k-vuelidate-error-extractor>
              </div>
              <div
                class="col-md-5"
                v-show="isFenyitesTipusaTobbletszolgaltatasokMegvonasa"
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesHosszaEsTipusa"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="
                      Select2FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions
                    "
                    v-model="formData.values.FenyitesHosszaEsTipusa"
                  ></k-select2>
                  <span class="text-help float-right">Fenyítés tartama</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-5" v-show="isFenyitesTipusaKimaradasMegvonasa">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesHosszaEsTipusa"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2FenyitesTartamaKimaradasMegvonasaOptions"
                    v-model="formData.values.FenyitesHosszaEsTipusa"
                  ></k-select2>
                  <span class="text-help float-right">Fenyítés tartama</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-5" v-show="isFenyitesTipusaMegszuntetes">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.MegszuntetesOkaCimkeId"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2FenyitesMegszuntetesOkaOptions"
                    v-model="formData.values.MegszuntetesOkaCimkeId"
                    :disabled="isHatarozatMegszunteteseModal"
                  ></k-select2>
                  <span class="text-help float-right">Megszüntetés oka</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group summernote"
                  :validator="$v.formData.values.Leiras"
                >
                  <!--<textarea class="leiras summernote"></textarea>-->
                  <textarea-autosize
                    v-model="$v.formData.values.Leiras.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                    placeholder="Kérem adja meg az indoklást..."
                  />
                  <!-- <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    :settings="{
                      placeholder: 'Kérem adja meg az indoklást...',
                    }"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote> -->
                  <span class="text-help float-right">Határozat indoklása</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12" v-show="isFenyitesTipusaMaganelzaras">
                <div class="checkbox-custom">
                  <input
                    type="checkbox"
                    id="inputUnchecked"
                    v-model="formData.values.PanasszalElt"
                  />
                  <label
                    for="inputUnchecked"
                    class="font-weight-400 d-inline-flex align-items-end"
                    ><span class="text-help my-0 ml-10"
                      >Eljárás alá vont a döntés ellen bírósági felülvizsgálati
                      kérelemmel él</span
                    ></label
                  >
                </div>
              </div>
              <div
                class="col-md-12"
                v-show="
                  !isFenyitesTipusaMegszuntetes && !isFenyitesTipusaMaganelzaras
                "
              >
                <div class="checkbox-custom">
                  <input
                    type="checkbox"
                    id="inputUnchecked"
                    v-model="formData.values.PanasszalElt"
                  />
                  <label
                    for="inputUnchecked"
                    class="font-weight-400 d-inline-flex align-items-end"
                    ><span class="text-help my-0 ml-10"
                      >Eljárás alá vont a döntés ellen panasszal él</span
                    ></label
                  >
                </div>
              </div>
              <div
                class="col-md-12 my-10"
                v-show="
                  formData.isKarteriteses && !isFenyitesTipusaMegszuntetes
                "
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.IsSzandekosKarokozas"
                >
                  <div class="radio-buttons-wrapper mb-10">
                    <b-form-group>
                      <b-form-radio
                        v-model="$v.formData.values.IsSzandekosKarokozas.$model"
                        name="IsSzandekosKarokozas"
                        :value="true"
                        class="blue-grey-400"
                      >
                        Szándékos elkövetés
                      </b-form-radio>
                      <b-form-radio
                        v-model="$v.formData.values.IsSzandekosKarokozas.$model"
                        name="IsSzandekosKarokozas"
                        :value="false"
                        class="blue-grey-400"
                      >
                        Nem szándékos elkövetés
                      </b-form-radio>
                    </b-form-group>
                  </div>
                  <p>
                    Kötelező az egyiket kiválasztani. Szándékosság esetén a Bv
                    Bank modul kártérítési ügyénél tölthető le a feljelentési
                    dokumentum.
                  </p>
                </k-vuelidate-error-extractor>
              </div>
              <ul
                class="timeline timeline-icon my-20 px-15"
                v-if="tovabbiUgyei != ''"
              >
                <li class="timeline-period mt-0 mb-30 py-15">
                  Az eljárás alá vont kiszabott és végrehajtás alatti fegyelmi
                  ügyei
                </li>
                <div
                  v-for="(tovabbiUgyei, i) in tovabbiUgyei"
                  v-bind:key="tovabbiUgyei.Id"
                  :class="[
                    i % 2 == 0
                      ? 'timeline-item'
                      : 'timeline-item timeline-reverse',
                  ]"
                >
                  <li>
                    <div class="timeline-dot animation-scale-up">
                      <i class="fas fa-info"></i>
                    </div>
                    <div class="timeline-info">
                      <time
                        :datetime="
                          tovabbiUgyei.EsemenyDatuma | toShortDatePontNelkul
                        "
                        >{{
                          tovabbiUgyei.EsemenyDatuma | toShortDatePontNelkul
                        }}</time
                      >
                    </div>
                    <div class="timeline-content">
                      <div class="card">
                        <div>
                          <p>
                            <strong>Ügy száma:</strong>
                            {{ tovabbiUgyei.UgySzama }}
                            {{ tovabbiUgyei.IntezetNev }}
                          </p>
                        </div>
                        <div>
                          <p>
                            <strong>Státusz:</strong>
                            {{ tovabbiUgyei.UgyStatusz }}
                          </p>
                        </div>
                        <div>
                          <p>
                            <strong>Fenyítés típusa:</strong>
                            {{ tovabbiUgyei.FenyitesTipusaEsHossza }}
                          </p>
                        </div>
                      </div>
                    </div>
                  </li>
                </div>
              </ul>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer d-flex justify-content-between">
        <div class="modalorszam"></div>
        <div class="text-right">
          <b-button @click="Hide()" class="btn-raised">Mégsem</b-button>

          <b-button @click="EsemenyNyomtatasEsMentes()" class="btn-raised">
            <b-spinner small v-if="isLoadingNyomtatas"></b-spinner>
            Nyomtatás</b-button
          >

          <b-button
            @click="EsemenyVeglegesMentes()"
            class="ml-3 btn btn-warning btn-raised font-weight-700"
            :disabled="isLoading || buttonsDisabled"
          >
            <b-spinner small v-if="isLoading"></b-spinner>
            Határozat kész</b-button
          >
        </div>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import $ from 'jquery';
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import settings from '../../data/settings';
import moment from 'moment';
import {
  required,
  requiredIf,
  minValue,
  maxValue,
} from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../utils/modal';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import Cimkek from '../../data/enums/Cimkek';
import { deselectDatatable } from '../../utils/common';
import ModalTipus from '../../data/enums/modalTipus';

export default {
  name: 'k-hatarozat-rogzitese',
  data: function () {
    return {
      isFormLoading: false,
      title: 'Határozat rögzítése',
      isLoadingNyomtatas: false,
      isLoading: false,
      fegyelmiUgyId: 0,
      modalType: null,
      tovabbiUgyei: null,
      formData: {
        fegyelmiVetsegTipusaOptions: [],
        vetsegRendeletSzerintOptions: [],
        fenyitesTipusaOptions: [],
        fenyitesTartamaKietkezesCsokkentesOptions: [],
        fenyitesTartamaMaganElzarasOptions: [],
        fenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions: [],
        fenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions: [],
        fenyitesTartamaProgaromokonValoResztvetelTiltasaOptions: [],
        fenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions: [],
        fenyitesTartamaKimaradasMegvonasaOptions: [],
        fenyitesMegszuntetesOkaOptions: [],
        isKarteriteses: null,
        values: {
          NaplobejegyzesId: null,
          FegyelmiUgyId: 0,
          Leiras: '',
          FegyelmiVetsegTipusaCimkeId: null,
          VetsegRendeletSzerintCimkeId: null,
          FenyitesTipusaCimkeId: null,
          FenyitesHosszaEsTipusa: null,
          ElsofokonHatarozathozoSzemely: '',
          KietkezesCsokkentes: null,
          MegszuntetesOkaCimkeId: null,
          PanasszalElt: false,
          IsSzandekosKarokozas: null,
        },
      },
    };
  },
  setup(props, context) {
    let { buttonsDisabled, Hide, modalOpts } = useSimpleModalHandler(
      props,
      context
    );
    return { buttonsDisabled, Hide, modalOpts };
  },
  mounted: function () {
    this.InitEvents(this.modalOpts);
  },
  validations() {
    let validationObj = {
      formData: {
        values: {
          ElsofokonHatarozathozoSzemely: {
            required,
          },
          FegyelmiVetsegTipusaCimkeId: {
            required,
          },
          VetsegRendeletSzerintCimkeId: {
            required,
          },
          FenyitesTipusaCimkeId: {
            required,
          },
          MegszuntetesOkaCimkeId: {
            required: requiredIf(function () {
              if (
                this.formData.values.FenyitesTipusaCimkeId ==
                Cimkek.FenyitesTipusa.Megszuntetes
              ) {
                return true;
              } else {
                return false;
              }
            }),
          },
          FenyitesHosszaEsTipusa: {
            required: requiredIf(function () {
              if (
                this.formData.values.FenyitesTipusaCimkeId !=
                  Cimkek.FenyitesTipusa.Feddes &&
                this.formData.values.FenyitesTipusaCimkeId !=
                  Cimkek.FenyitesTipusa.Megszuntetes
              ) {
                return true;
              } else {
                return false;
              }
            }),
          },
          Leiras: {
            required,
          },
          KietkezesCsokkentes: {
            required: requiredIf(function () {
              return (
                this.formData.values.FenyitesTipusaCimkeId ==
                Cimkek.FenyitesTipusa.KietkezesCsokkentes
              );
            }),
          },
          IsSzandekosKarokozas: {
            required: requiredIf(function () {
              return this.formData.isKarteriteses == true;
            }),
          },
        },
      },
    };
    if (
      this.formData.values.FenyitesTipusaCimkeId ==
      Cimkek.FenyitesTipusa.KietkezesCsokkentes
    ) {
      validationObj.formData.values.KietkezesCsokkentes = {
        required,
        minValue: minValue(1),
        maxValue: maxValue(50),
      };
    }
    return validationObj;
  },
  methods: {
    InitEvents: function ({ state, data }) {
      if (state) {
        this.fegyelmiUgyId = data.fegyelmiUgyId;
        this.modalType = data.modalType;
        this.LoadFegyelmiUgyData(
          data.fegyelmiUgyId,
          data.naplobejegyzesId,
          data.modalType
        );
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function (
      fegyelmiUgyId,
      naplobejegyzesId,
      modalType
    ) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFegyelmiUgyHatarozatRogzitese({
          data: { fegyelmiUgyId, naplobejegyzesId, modalType },
        });
        if (fegyelmiUgyId) {
          var res =
            await apiService.GetFolyamatbanEsKiszabvaFegyelmiUgyekByFegyelmiUgyId(
              {
                fegyelmiUgyId: fegyelmiUgyId,
              }
            );
          this.tovabbiUgyei = res;
        }

        this.formData.isKarteriteses = result.IsKarterites;

        this.formData.fegyelmiVetsegTipusaOptions =
          result.FegyelmiVetsegTipusaOptions.sort(function (a, b) {
            return ('' + a.text).localeCompare(b.text);
          });

        this.formData.vetsegRendeletSzerintOptions =
          result.VetsegRendeletSzerintOptions.sort(function (a, b) {
            return ('' + a.text).localeCompare(b.text);
          });

        this.formData.fenyitesTipusaOptions = result.FenyitesTipusaOptions.sort(
          function (a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );
        this.formData.fenyitesTartamaKietkezesCsokkentesOptions =
          result.FenyitesTartamaKietkezesCsokkentesOptions.sort(function (
            a,
            b
          ) {
            return ('' + a.text).localeCompare(b.text);
          });

        var numberCollator = new Intl.Collator(undefined, {
          numeric: true,
          sensitivity: 'base',
        });
        this.formData.fenyitesTartamaMaganElzarasOptions =
          result.FenyitesTartamaMaganElzarasOptions.sort(
            numberCollator.compare
          );

        this.formData.fenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions =
          result.FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions.sort(
            function (a, b) {
              return ('' + a.text).localeCompare(b.text);
            }
          );
        this.formData.fenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions =
          result.FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions.sort(
            numberCollator.compare
          );

        this.formData.fenyitesTartamaProgaromokonValoResztvetelTiltasaOptions =
          result.FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions.sort(
            function (a, b) {
              return ('' + a.text).localeCompare(b.text);
            }
          );

        this.formData.fenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions =
          result.FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions.sort(
            function (a, b) {
              return ('' + a.text).localeCompare(b.text);
            }
          );
        this.formData.fenyitesTartamaKimaradasMegvonasaOptions =
          result.FenyitesTartamaKimaradasMegvonasaOptions.sort(function (a, b) {
            return ('' + a.text).localeCompare(b.text);
          });
        this.formData.fenyitesMegszuntetesOkaOptions =
          result.FenyitesMegszuntetesOkaOptions.sort(function (a, b) {
            return ('' + a.text).localeCompare(b.text);
          });
        for (const key in this.formData.values) {
          if (
            this.formData.values.hasOwnProperty(key) &&
            result.hasOwnProperty(key)
          ) {
            this.$set(this.formData.values, key, result[key]);
          }
        }
        this.$v.$reset();
        //$("#testx").select2({
        //  data: this.formData.tovabbiSzemelyekOptions,
        //  //dropdownParent: $(this.$el),
        //  multiple: true,
        //  tags: true,
        //});
        this.isFormLoading = false;
      } catch (err) {
        console.log(err);
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
        } else {
          NotificationFunctions.AjaxError({
            title: 'Hiba',
            text: 'Adatok letöltése sikertelen',
            errorObj: err,
          });
        }
        this.Hide();
      }
    },
    async EsemenyNyomtatasEsMentes() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isLoadingNyomtatas = true;
      let result = await this.SaveEsemeny(false);
      if (!result) {
        this.isLoadingNyomtatas = false;
        return;
      }

      this.formData.values.NaplobejegyzesId = result.NaploId;

      if (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.Megszuntetes
      ) {
        await NyomtatvanyFunctions.FegyelmiHatarozatMegszuntetesNyomtatas({
          fegyelmiUgyId: this.formData.values.FegyelmiUgyId,
          naploId: this.formData.values.NaplobejegyzesId,
        });
      } else {
        await NyomtatvanyFunctions.FegyelmiHatarozatNyomtatas({
          fegyelmiUgyId: this.formData.values.FegyelmiUgyId,
          naploId: this.formData.values.NaplobejegyzesId,
        });
      }
      eventBus.$emit('Sidebar:ugyReszletek:refresh');
      this.isLoadingNyomtatas = false;
    },
    async EsemenyVeglegesMentes() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isLoading = true;
      await this.SaveEsemeny(true);
      this.isLoading = false;
    },
    IsFormValid() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        this.$nextTick(() => {
          var element = this.$el.querySelector('.form-group.error:first-child');
          element.scrollIntoView();
        });
        return false;
      }
      return true;
    },
    SaveEsemeny: async function (alert) {
      var result = null;
      try {
        result = await apiService.SaveFegyelmiUgyHatarozatRogzitese({
          data: {
            ...this.formData.values,
            isVegleges: alert,
          },
        });
        if (alert) {
          NotificationFunctions.SuccessAlert(
            'Határozat rögzítése',
            'Sikeres mentés'
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Határozat rögzítése',
          text: 'A határozat mentése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
      return result;
    },
    StartLoader: function () {
      this.isLoading = true;
    },
    EndLoader: function () {
      this.isLoading = false;
    },
  },
  watch: {
    'formData.values.FenyitesTipusaCimkeId': function (newValue, oldValue) {
      if (oldValue != null) this.formData.values.FenyitesHosszaEsTipusa = null;
    },
    isFenyitesTipusaMegszuntetes(f) {
      if (f) {
        this.formData.values.IsSzandekosKarokozas = false;
      }
    },
  },
  computed: {
    Select2FegyelmiVetsegTipusaOptions: function () {
      return {
        data: this.formData.fegyelmiVetsegTipusaOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2VetsegRendeletSzerintOptions: function () {
      return {
        data: this.formData.vetsegRendeletSzerintOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2FenyitesTipusaOptions: function () {
      return {
        data: this.formData.fenyitesTipusaOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2FenyitesTartamaKietkezesCsokkentesOptions: function () {
      return {
        data: this.formData.fenyitesTartamaKietkezesCsokkentesOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2FenyitesTartamaMaganElzarasOptions: function () {
      return {
        data: this.formData.fenyitesTartamaMaganElzarasOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions:
      function () {
        return {
          data: this.formData
            .fenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions,
          dropdownParent: $(this.$el),
        };
      },
    Select2FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions:
      function () {
        return {
          data: this.formData
            .fenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions,
          dropdownParent: $(this.$el),
        };
      },
    Select2FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions:
      function () {
        return {
          data: this.formData
            .fenyitesTartamaProgaromokonValoResztvetelTiltasaOptions,
          dropdownParent: $(this.$el),
        };
      },
    Select2FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions: function () {
      return {
        data: this.formData
          .fenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2FenyitesTartamaKimaradasMegvonasaOptions: function () {
      return {
        data: this.formData.fenyitesTartamaKimaradasMegvonasaOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2FenyitesMegszuntetesOkaOptions: function () {
      return {
        data: this.formData.fenyitesMegszuntetesOkaOptions,
        dropdownParent: $(this.$el),
      };
    },
    isFenyitesTipusaKietkezesCsokkentes() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.KietkezesCsokkentes
      );
    },
    isFenyitesTipusaBvIntProgramokonValoReszvetelKorlatozasa() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.BvIntProgramokonValoReszvetelKorlatozasa
      );
    },
    isFenyitesTipusaBvIntProgramokonValoReszvetelTiltasa() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.BvIntProgramokonValoReszvetelTiltasa
      );
    },
    isFenyitesTipusaMaganalTarthatoTargyakKorenekKorlatozasa() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.MaganalTarthatoTargyakKorenekKorlatozasa
      );
    },
    isFenyitesTipusaMaganelzaras() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.Maganelzaras
      );
    },
    isFenyitesTipusaTobbletszolgaltatasokMegvonasa() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.TobbletszolgaltatasokMegvonasa
      );
    },
    isFenyitesTipusaKimaradasMegvonasa() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.KimaradasMegvonas
      );
    },
    isFenyitesTipusaMegszuntetes() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.Megszuntetes
      );
    },
    isFenyitesTipusaMezoSzelesseg() {
      if (
        this.formData.values.FenyitesTipusaCimkeId == null ||
        this.formData.values.FenyitesTipusaCimkeId ==
          Cimkek.FenyitesTipusa.Feddes
      ) {
        return 12;
      } else {
        return 7;
      }
    },
    SummernoteConfig: function () {
      return {
        toolbar: [
          // [groupName, [list of button]]
          ['style', ['bold', 'italic', 'underline', 'clear']],
          //['font', ['strikethrough', 'superscript', 'subscript']],
          ['forecolor', ['forecolor']],
          ['para', ['ul', 'ol']],
          ['fullscreen', ['fullscreen']],
        ],
        disableResizeEditor: true,
        disableDragAndDrop: true,
      };
    },
    getData() {
      return JSON.stringify(this.formData.values, null, 2);
    },
    isHatarozatMegszunteteseModal() {
      return this.modalType == ModalTipus.HatarozatMegszuntetese;
    },
  },

  destroyed: function () {},
};
</script>
<style scoped>
.modal-scroll {
  height: 410px;
}
.vb-content {
  padding-right: 20px !important;
}

.timeline {
  width: 100%;
}
.timeline-period {
  font-size: 16px;
  text-transform: none;
}
.timeline-info,
.timeline-reverse .timeline-info {
  float: none;
  margin-bottom: 15px;
}
.timeline-dot {
  background-color: #8349f5;
}
.timeline-content p {
  font-size: 13px;
  margin-bottom: 5px;
}
</style>
