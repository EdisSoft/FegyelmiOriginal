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
          44. II. fok: határozat helybenhagyása, módosítása
          <p class="mt-10 mb-0 font-size-12">
            Fenyítés kiszabása, megszüntetés vagy új eljárás elrendelése
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
              <div class="col-md-12" v-show="isBvBiroJogkor">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.TorvenyszekId"
                >
                  <k-select2
                    :options="select2TorvenyszekOptions"
                    v-model="$v.formData.values.TorvenyszekId.$model"
                    class=""
                    placeholder="Válasszon törvényszéket..."
                  >
                  </k-select2>
                  <span class="text-help float-right"
                    >Másodfokon határozatot hozó törvényszék</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12" v-show="isParancsnokiJogkor">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="
                    $v.formData.values.MasodfokonHatarozatotHozoParancsnok
                  "
                >
                  <input
                    type="text"
                    class="form-control"
                    disabled
                    v-model="
                      $v.formData.values.MasodfokonHatarozatotHozoParancsnok
                        .$model
                    "
                  />
                  <span class="text-help float-right"
                    >Másodfokon határozatot hozó parancsnok</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.HatarozatDatuma"
                >
                  <date-picker
                    v-model="$v.formData.values.HatarozatDatuma.$model"
                    :config="datePickerOptions"
                  >
                  </date-picker>
                  <span class="text-help float-right">Határozat dátuma</span>
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
                    :readonly="isHelybenhagyas"
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
              <div
                class="col-md-5"
                v-show="isFenyitesTipusaHatalyonKivulHelyezes"
              >
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="
                    $v.formData.values.HatalyonKivulHelyezesOkaCimkeId
                  "
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2FenyitesHatalyonKivulHelyezesOkaOptions"
                    v-model="formData.values.HatalyonKivulHelyezesOkaCimkeId"
                  ></k-select2>
                  <span class="text-help float-right"
                    >Hatályon kívül helyezés típusa</span
                  >
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
                  />
                  <!-- <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote> -->
                  <span class="text-help float-right">Határozat indoklása</span>
                </k-vuelidate-error-extractor>
              </div>
              <div
                class="col-md-12 my-10"
                v-show="
                  formData.isKarteriteses &&
                  !isFenyitesTipusaHatalyonKivulHelyezes
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
            </div>
            <!-- </div> -->
            <!-- <div
        v-if="isLoading"
        class="vertical-align text-center"
        style="position:absolute; left:0; right:0; bottom:0; top:0; background-color:white;"
      >
        <div class="loader vertical-align-middle loader-ellipsis"></div>
      </div> -->
          </div>
        </div>
        <!-- <slot></slot> -->
      </div>
      <div class="modal-footer d-flex justify-content-between">
        <div class="modalorszam"></div>
        <div class="text-right">
          <b-button @click="Hide()" class="btn-raised">Mégsem</b-button>

          <b-button
            @click="HatarozatNyomtatasEsMentes()"
            class="btn-raised"
            :disabled="isLoadingNyomtatas || buttonsDisabled"
          >
            <b-spinner small v-if="isLoadingNyomtatas"></b-spinner>
            Nyomtatás</b-button
          >

          <b-button
            @click="HatarozatVeglegesMentes()"
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
import { sortStr } from '../../utils/sort';
import { hidrateForm } from '../../utils/vueUtils';
import ModalTipus from '../../data/enums/modalTipus';

export default {
  name: 'hatarozat-rogzitese-masod-foku',
  data: function () {
    return {
      isFormLoading: false,
      title: 'II. fok: határozat',
      isLoadingNyomtatas: false,
      modalType: 0,
      isLoading: false,
      naplobejegyzesIds: [],
      fegyelmiUgyIds: [],
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
        fenyitesHatalyonKivulHelyezesOkaOptions: [],
        torvenyszekOptions: [],
        elsofokuTargyalasIdopontja: null,
        isKarteriteses: null,
        values: {
          TorvenyszekId: null,
          Leiras: '',
          FegyelmiVetsegTipusaCimkeId: null,
          HatarozatHozoJogkoreCimkeId: null,
          VetsegRendeletSzerintCimkeId: null,
          FenyitesTipusaCimkeId: null,
          FenyitesHosszaEsTipusa: null,
          MasodfokonHatarozatotHozoParancsnok: '',
          KietkezesCsokkentes: null,
          HatalyonKivulHelyezesOkaCimkeId: null,
          HatarozatDatuma: null,
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
          TorvenyszekId: {
            required: requiredIf(function () {
              return this.isBvBiroJogkor;
            }),
          },
          MasodfokonHatarozatotHozoParancsnok: {},
          FegyelmiVetsegTipusaCimkeId: {
            required,
          },
          VetsegRendeletSzerintCimkeId: {
            required,
          },
          FenyitesTipusaCimkeId: {
            required,
          },
          HatalyonKivulHelyezesOkaCimkeId: {
            required: requiredIf(function () {
              if (
                this.formData.values.FenyitesTipusaCimkeId ==
                Cimkek.FenyitesTipusa.HatalyonKivulHelyezes
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
                  Cimkek.FenyitesTipusa.HatalyonKivulHelyezes
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
          HatarozatDatuma: {
            required,
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
    if (!this.isBvBiroJogkor) {
      validationObj.formData.values.TorvenyszekId = {};
    }
    return validationObj;
  },
  methods: {
    InitEvents: function ({ state, data }) {
      console.log('InitEvents', data);
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyId || [];
        this.naplobejegyzesIds = data.naplobejegyzesId || [];
        this.modalType = data.modalType;
        this.LoadFormData(data.fegyelmiUgyId, data.naplobejegyzesId);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function (fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        console.log('fegyelmiUgyIds:' + fegyelmiUgyIds);
        var result = await apiService.GetHatarozatRogziteseMasodfokonModalData({
          data: {
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          },
        });
        if (result) {
          this.formData.isKarteriteses = result.IsKarterites;

          this.formData.fegyelmiVetsegTipusaOptions =
            result.FegyelmiVetsegTipusaOptions.sort(sortStr('text'));

          this.formData.vetsegRendeletSzerintOptions =
            result.VetsegRendeletSzerintOptions.sort(sortStr('text'));

          this.formData.fenyitesTipusaOptions =
            result.FenyitesTipusaOptions.sort(sortStr('text'));
          this.formData.fenyitesTartamaKietkezesCsokkentesOptions =
            result.FenyitesTartamaKietkezesCsokkentesOptions.sort(
              sortStr('text')
            );

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
              sortStr('text')
            );
          this.formData.fenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions =
            result.FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions.sort(
              numberCollator.compare
            );

          this.formData.fenyitesTartamaProgaromokonValoResztvetelTiltasaOptions =
            result.FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions.sort(
              sortStr('text')
            );

          this.formData.fenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions =
            result.FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions.sort(
              sortStr('text')
            );
          this.formData.fenyitesTartamaKimaradasMegvonasaOptions =
            result.FenyitesTartamaKimaradasMegvonasaOptions.sort(
              sortStr('text')
            );
          this.formData.fenyitesHatalyonKivulHelyezesOkaOptions =
            result.FenyitesHatalyonKivulHelyezesOkaOptions.sort(
              sortStr('text')
            );
          this.formData.torvenyszekOptions = result.TorvenyszekOptions.sort(
            sortStr('text')
          );
          this.formData.elsofokuTargyalasIdopontja =
            result.ElsofokuTargyalasIdopontja;

          console.log(
            'this.formData.elsofokuTargyalasIdopontja:' +
              this.formData.elsofokuTargyalasIdopontja
          );
          this.formData.HatarozatDatuma = moment(
            this.formData.values.HatarozatDatuma
          ).format('YYYY.MM.DD');

          hidrateForm(this, this.formData.values, result);
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
    async HatarozatNyomtatasEsMentes() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isLoadingNyomtatas = true;
      let result = await this.SaveEsemeny(false);

      if (result) {
        // ToDo: nincs már megszüntetés, kellhet ide új nyomtatvány?
        if (
          this.formData.values.FenyitesTipusaCimkeId ==
          Cimkek.FenyitesTipusa.Megszuntetes
        ) {
          await NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatMegszunteteseNyomtatas(
            {
              naplobejegyzesIds: this.naplobejegyzesIds,
            }
          );
        } else {
          await NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatNyomtatas({
            naplobejegyzesIds: this.naplobejegyzesIds,
          });
        }
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
      }
      this.isLoadingNyomtatas = false;
    },
    async HatarozatVeglegesMentes() {
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
        result = await apiService.SaveHatarozatRogziteseMasodfokonModalData({
          data: {
            ...this.formData.values,
            IsVegleges: alert,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
            IsHelybenhagyas:
              this.modalType == ModalTipus.HatarozatHelybenhagyasa,
          },
        });
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        if (alert) {
          NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: this.title,
          text: 'A jegyzőkönyv mentése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
      return result;
    },
  },
  computed: {
    datePickerOptions() {
      let minDate = moment().toISOString();
      if (this.formData.elsofokuTargyalasIdopontja) {
        minDate = moment(this.formData.elsofokuTargyalasIdopontja)
          .add({ d: 1 })
          .startOf('d');
      }
      return {
        format: 'YYYY.MM.DD',
        locale: moment.locale('hu'),
        defaultDate: moment(),
        dayViewHeaderFormat: 'YYYY. MMMM',
        minDate: minDate,
        maxDate: moment(),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
    Select2FegyelmiVetsegTipusaOptions: function () {
      return {
        data: this.formData.fegyelmiVetsegTipusaOptions,
        dropdownParent: $(this.$el),
        readOnly: this.isHelybenhagyas,
        disabled: this.isHelybenhagyas,
      };
    },
    Select2VetsegRendeletSzerintOptions: function () {
      return {
        data: this.formData.vetsegRendeletSzerintOptions,
        dropdownParent: $(this.$el),
        readOnly: this.isHelybenhagyas,
        disabled: this.isHelybenhagyas,
      };
    },
    Select2FenyitesTipusaOptions: function () {
      return {
        data: this.formData.fenyitesTipusaOptions,
        dropdownParent: $(this.$el),
        readOnly: this.isHelybenhagyas,
        disabled: this.isHelybenhagyas,
      };
    },
    Select2FenyitesTartamaKietkezesCsokkentesOptions: function () {
      return {
        data: this.formData.fenyitesTartamaKietkezesCsokkentesOptions,
        dropdownParent: $(this.$el),
        readOnly: this.isHelybenhagyas,
        disabled: this.isHelybenhagyas,
      };
    },
    Select2FenyitesTartamaMaganElzarasOptions: function () {
      return {
        data: this.formData.fenyitesTartamaMaganElzarasOptions,
        dropdownParent: $(this.$el),
        readOnly: this.isHelybenhagyas,
        disabled: this.isHelybenhagyas,
      };
    },
    Select2FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions:
      function () {
        return {
          data: this.formData
            .fenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions,
          dropdownParent: $(this.$el),
          readOnly: this.isHelybenhagyas,
          disabled: this.isHelybenhagyas,
        };
      },
    Select2FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions:
      function () {
        return {
          data: this.formData
            .fenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions,
          dropdownParent: $(this.$el),
          readOnly: this.isHelybenhagyas,
          disabled: this.isHelybenhagyas,
        };
      },
    Select2FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions:
      function () {
        return {
          data: this.formData
            .fenyitesTartamaProgaromokonValoResztvetelTiltasaOptions,
          dropdownParent: $(this.$el),
          readOnly: this.isHelybenhagyas,
          disabled: this.isHelybenhagyas,
        };
      },
    select2TorvenyszekOptions: function () {
      return {
        data: this.formData.torvenyszekOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions: function () {
      return {
        data: this.formData
          .fenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions,
        dropdownParent: $(this.$el),
        readOnly: this.isHelybenhagyas,
        disabled: this.isHelybenhagyas,
      };
    },
    Select2FenyitesTartamaKimaradasMegvonasaOptions: function () {
      return {
        data: this.formData.fenyitesTartamaKimaradasMegvonasaOptions,
        dropdownParent: $(this.$el),
        readOnly: this.isHelybenhagyas,
        disabled: this.isHelybenhagyas,
      };
    },
    Select2FenyitesHatalyonKivulHelyezesOkaOptions: function () {
      return {
        data: this.formData.fenyitesHatalyonKivulHelyezesOkaOptions,
        dropdownParent: $(this.$el),
        readOnly: this.isHelybenhagyas,
        disabled: this.isHelybenhagyas,
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
    isFenyitesTipusaHatalyonKivulHelyezes() {
      return (
        this.formData.values.FenyitesTipusaCimkeId ==
        Cimkek.FenyitesTipusa.HatalyonKivulHelyezes
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
    isHelybenhagyas() {
      return this.modalType == ModalTipus.HatarozatHelybenhagyasa;
    },
    isBvBiroJogkor() {
      return (
        this.formData.values.HatarozatHozoJogkoreCimkeId ==
        Cimkek.HatarozatHozoJogkore.BVBiro
      );
    },
    isParancsnokiJogkor() {
      return (
        this.formData.values.HatarozatHozoJogkoreCimkeId ==
        Cimkek.HatarozatHozoJogkore.Parancsnok
      );
    },
    getData() {
      return JSON.stringify(this.formData.values, null, 2);
    },
  },
  watch: {
    isFenyitesTipusaHatalyonKivulHelyezes(f) {
      if (f) {
        this.formData.values.IsSzandekosKarokozas = false;
      } else {
        this.formData.values.IsSzandekosKarokozas = null;
      }
    },
  },

  destroyed: function () {},
};
</script>
<style scoped>
.modal-scroll {
  height: 315px;
}
.vb-content {
  padding-right: 20px !important;
}
</style>
