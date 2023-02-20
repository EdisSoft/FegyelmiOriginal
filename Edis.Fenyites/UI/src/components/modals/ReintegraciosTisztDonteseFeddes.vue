<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-grey modal-container">
      <div class="modal-header">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title font-size-20">
          6. Feddés reintegrációs tiszti jogkörben
          <p class="mt-10 mb-0 font-size-12">Kérem, indokolja a döntést!</p>
        </h4>
      </div>
      <div class="modal-body pt-20 pb-40 pl-25">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <div class="row">
              <div class="col-md-12 mb-10">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesKiszabasIdeje"
                >
                  <date-picker
                    v-model="$v.formData.values.FenyitesKiszabasIdeje.$model"
                    :config="datePickerOptions"
                  >
                  </date-picker>
                  <span class="text-help float-right"
                    >Fenyítés kiszabás ideje</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12 mb-10">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FegyelmiVetsegTipusaCimkeId"
                >
                  <k-select2
                    :options="fegyelmiVetsegTipusaOptions"
                    v-model="
                      $v.formData.values.FegyelmiVetsegTipusaCimkeId.$model
                    "
                    class=""
                    placeholder="Kérem adja meg a fegyelmi vétség típusát..."
                  >
                  </k-select2>
                  <span class="text-help float-right"
                    >Fegyelmi vétség típusa</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material summernote"
                  :validator="$v.formData.values.Indoklas"
                >
                  <textarea-autosize
                    v-model="$v.formData.values.Indoklas.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                    placeholder="Indoklás..."
                  />
                  <!-- <k-summernote
                    v-model="$v.formData.values.Indoklas.$model"
                    :settings="{
                      placeholder: 'Indoklás...',
                    }"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote> -->
                  <span class="text-help float-right">Indoklás</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <div class="checkbox-custom">
                  <input
                    type="checkbox"
                    id="inputUnchecked2"
                    v-model="formData.values.IsNemVetteTudomasul"
                  />
                  <label
                    for="inputUnchecked2"
                    class="font-weight-400 d-inline-flex align-items-end"
                    ><span class="text-help my-0 ml-10"
                      >Eljárás alá vont a fenyítést nem vette tudomásul</span
                    ></label
                  >
                </div>
              </div>
              <div
                class="col-md-12 my-20"
                v-show="
                  formData.isKarteriteses && !fogvatartottNemVetteTudomasul
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
          </div>
        </div>
      </div>
      <div class="modal-footer d-flex">
        <button type="button" class="btn btn-dark mb-lg-5" @click="Hide">
          Mégsem
        </button>
        <button
          type="button"
          class="btn btn-dark mb-lg-5"
          @click="Nyomtatas()"
          :disabled="
            isMentesLoadingTudomasulVette ||
            isFormLoadingNyomtatas ||
            buttonsDisabled
          "
        >
          <b-spinner small v-if="isFormLoadingNyomtatas"></b-spinner>
          Nyomtatás
        </button>
        <b-button
          type="button"
          class="btn btn-warning mb-lg-5"
          @click="FenyitesKiszabas()"
          :disabled="
            isFormLoadingFenyitesKiszabas ||
            isFormLoadingNyomtatas ||
            buttonsDisabled
          "
        >
          <b-spinner small v-if="isFormLoadingFenyitesKiszabas"></b-spinner>
          Feddés kiszabása
        </b-button>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService, eventBus } from '../../main';
import { AppStoreTypes } from '../../store/modules/app';
import settings from '../../data/settings';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { getUgyszam } from '../../utils/fenyitesUtils';
import { required, minValue } from 'vuelidate/lib/validators';
import moment from 'moment';
import $ from 'jquery';
import ReintegraciosTisztDontesTipus from '../../data/enums/reintegraciosTisztDontesTipus';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { hidrateForm } from '../../utils/vueUtils';
import { sortStr } from '../../utils/sort';
import { useSimpleModalHandler } from '../../utils/modal';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import { deselectDatatable } from '../../utils/common';
import requiredIf from 'vuelidate/lib/validators/requiredIf';
import Cimkek from '../../data/enums/Cimkek';

export default {
  name: 'reintegracios-tiszt-dontese-feddes',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      dontesTipus: null,
      title: 'Feddés',
      isMentesLoadingTudomasulVette: false,
      isMentesLoading: false,
      isFormLoadingNyomtatas: false,
      isFormLoadingFenyitesKiszabas: false,
      formData: {
        fenyitesKiszabasIdejeMinDate: null,
        reintegraciosReszlegOptions: [],
        fegyelmiVetsegTipusaOptions: [],
        isKarteriteses: null,
        values: {
          NaplobejegyzesIds: [],
          ReintegraciosReszlegId: null,
          FenyitesKiszabasIdeje: null,
          FegyelmiVetsegTipusaCimkeId: null,
          Indoklas: '',
          IsNemVetteTudomasul: false,
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
  created() {},
  validations: {
    formData: {
      values: {
        ReintegraciosReszlegId: {},
        FegyelmiVetsegTipusaCimkeId: {
          required,
        },
        FenyitesKiszabasIdeje: {
          required,
        },
        Indoklas: {
          required,
        },
        IsSzandekosKarokozas: {
          required: requiredIf(function () {
            return this.formData.isKarteriteses == true;
          }),
        },
      },
    },
  },
  methods: {
    InitEvents: function ({ state, data }) {
      if (state) {
        console.log({ data });
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.formData.values.NaplobejegyzesIds = [data.naplobejegyzesId];
        this.dontesTipus = data.modalType;
        this.LoadFormData();
      } else {
        this.Hide();
      }
    },
    async LoadFormData() {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetReintegraciosTisztDontesModalData({
          fegyelmiUgyIds: this.fegyelmiUgyIds,
          naploIds: this.formData.values.NaplobejegyzesIds,
        });
        console.log(result);

        hidrateForm(this, this.formData.values, result);

        this.formData.isKarteriteses = result.IsKarterites;

        this.formData.values.FenyitesKiszabasIdeje = moment(
          this.formData.values.FenyitesKiszabasIdeje
        ).format('YYYY.MM.DD HH:mm');

        this.formData.reintegraciosReszlegOptions =
          result.ReintegraciosReszlegOptions;

        this.formData.reintegraciosReszlegOptions.sort(sortStr('text'));

        this.formData.fegyelmiVetsegTipusaOptions =
          result.FegyelmiVetsegTipusaOptions;

        this.formData.fenyitesKiszabasIdejeMinDate =
          result.FenyitesKiszabasIdejeMinDate;

        this.formData.fegyelmiVetsegTipusaOptions.sort(sortStr('text'));

        this.$v.$reset();
        this.isFormLoading = false;
      } catch (err) {
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
        } else {
          NotificationFunctions.AjaxError({
            title: 'Hiba',
            text: 'Adatok letöltése sikertelen',
            errorObj: err,
          });
        }
        console.log(err);
        this.Hide();
      }
    },

    async Nyomtatas() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        console.log(this.$v);
        return;
      }
      this.isFormLoadingNyomtatas = true;
      let result = await this.Mentes(false, true);

      let naplobejegyzesIds = result.naplobejegyzesIds;
      //this.isFormLoadingNyomtatas = false;
      //console.log(naplobejegyzesIds);

      if (result) {
        await NyomtatvanyFunctions.HatarozatReintegraciosNyomtatas({
          fegyelmiUgyId: this.fegyelmiUgyIds[0],
          naploId: naplobejegyzesIds[0],
        });
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
      }
      this.isFormLoadingNyomtatas = false;
    },
    async FenyitesKiszabas() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        return;
      }
      this.isFormLoadingFenyitesKiszabas = true;

      await this.Mentes(true, false);
      this.isFormLoadingFenyitesKiszabas = false;
      eventBus.$emit('Sidebar:ugyReszletek:refresh');
    },
    async Mentes(isVegleges, isNyomtatvany = false) {
      try {
        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
          Vegleges: isVegleges,
        };
        let result = await apiService.ReintegraciosTisztDontesFeddesModalMentes(
          {
            data,
          }
        );
        this.formData.values.NaplobejegyzesIds = result.naplobejegyzesIds;
        if (isVegleges) {
          deselectDatatable();
          this.Hide();
        }
        if (isNyomtatvany) {
          NotificationFunctions.SuccessAlert(
            'Feddés reintegrációs tiszti jogkörben',
            'Sikeres nyomtatvány rögzítés'
          );
        } else {
          NotificationFunctions.SuccessAlert(
            'Feddés reintegrációs tiszti jogkörben',
            'A fogvatartott feddése megtörént'
          );
        }
        return result;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Feddés létrehozása nem sikerült',
          errorObj: err,
        });
        console.log(err);
      }
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),
    reintegraciosReszlegOptions: function () {
      return {
        data: this.formData.reintegraciosReszlegOptions,
        dropdownParent: $(this.$el),
      };
    },
    datePickerOptions() {
      let minDate = this.formData.fenyitesKiszabasIdejeMinDate;
      if (!minDate) {
        minDate = moment().subtract({ days: 2 }).toISOString();
      }

      return {
        format: 'YYYY.MM.DD',
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment().endOf('d'),
        minDate: moment(minDate),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
    fegyelmiVetsegTipusaOptions: function () {
      return {
        data: this.formData.fegyelmiVetsegTipusaOptions,
        dropdownParent: $(this.$el),
      };
    },
    isGombHide() {
      if (this.dontesTipus == 2 || this.dontesTipus == 3) {
        return true;
      } else {
        return false;
      }
    },
    dontesTipusStr() {
      switch (parseInt(this.dontesTipus)) {
        case ReintegraciosTisztDontesTipus.Feddes:
          return 'Feddés';

        // Már külön modal
        // case ReintegraciosTisztDontesTipus.Visszakuldes:
        //   return 'Visszaküldes';

        case ReintegraciosTisztDontesTipus.Kioktatas:
          return 'Kioktatás';

        default:
          return '';
      }
    },
    fogvatartottNemVetteTudomasul() {
      return this.formData.values.IsNemVetteTudomasul == true;
    },
  },
  components: {},
  watch: {
    fegyelmiUgyIds: {
      handler: function (newValue, oldValue) {},
      deep: true,
      immediate: true,
    },
    fogvatartottNemVetteTudomasul(f) {
      if (f) {
        this.formData.values.IsSzandekosKarokozas = false;
      }
    },
  },
  props: {},
};
</script>
<style scoped>
/* .doc-list {
  text-align: center;
} */

.el-center {
  transform: translateY(-50%);
  top: 50%;
  position: relative;
}
.media {
  height: 100%;
}
.list-item-border-bot {
  border-bottom: 1px solid #bcbdbe;
}
.list-item-border-botw {
  border-bottom: 1px solid white;
}
.list-item-border-bot:hover {
  background-color: #dee2e6;
}
.modal-header {
  height: 88px;
}
/* .modal-footer {
  height: 75px;
} */
.modal-scroll {
  height: 340px;
}
</style>
