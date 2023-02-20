<template>
  <basic-loader :isLoading="isFormLoading">
    <div id="DontesKozvetitoiEljarasrol">
      <div class="modal-primary">
        <div class="modal-header bg-blue-grey-400">
          <button
            type="button"
            class="close icon wb-close text-white"
            data-dismiss="modal"
            aria-label="Close"
            @click="Hide()"
          ></button>
          <h4 class="modal-title">
            25. Döntés közvetítői eljárásról
            <p class="mt-10 mb-0 font-size-12">
              Eljárás elrendelése vagy visszautasítása
            </p>
          </h4>
        </div>
        <div class="modal-body px-25 pt-20 pb-40">
          <div>
            <div class="row">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.KozvetitoId"
                >
                  <k-select2
                    :options="Select2KozvetitoOptions"
                    v-model="$v.formData.values.KozvetitoId.$model"
                    class=""
                    placeholder="Válassza ki a közvetítőt..."
                  >
                  </k-select2>
                  <span class="text-help float-right">Közvetítő</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.Datum"
                >
                  <date-picker
                    v-model="$v.formData.values.Datum.$model"
                    :config="datePickerOptions"
                  >
                  </date-picker>
                  <span class="text-help float-right"
                    >Közvetítői eljárás lefolytatási határidő</span
                  >
                </k-vuelidate-error-extractor>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <button
          type="button"
          class="btn bg-blue-grey-600 white mb-lg-5"
          data-dismiss="modal"
          @click="Hide()"
        >
          Mégsem
        </button>
        <button
          id="visszautasitas-btn"
          type="button"
          @click="OpenVisszautasitasPopup"
          class="btn savebtn white mb-lg-5"
          :disabled="visszautasitasLoading || buttonsDisabled"
        >
          <b-spinner small v-if="visszautasitasLoading"></b-spinner>
          Eljárás lefolytatásának visszautasítása
        </button>
        <b-popover
          target="visszautasitas-btn"
          triggers="null"
          :show.sync="visszautasitasPopup"
          placement="top"
          container="DontesKozvetitoiEljarasrol"
          ref="confirmPopover"
          custom-class="ujResztvevoPopover"
        >
          <template slot="title">
            <b-button
              @click="visszautasitasPopup = false"
              class="close"
              aria-label="Close"
            >
              <span class="d-inline-block white" aria-hidden="true"
                >&times;</span
              >
            </b-button>
            Megerősítés
          </template>
          <div class="pb-5">
            <div
              class="form-group form-material  mb-10"
              data-plugin="formMaterial"
            >
              Biztosan visszautasítja a közvetítői eljárást?
            </div>
            <div class="text-right">
              <b-button
                size="sm"
                @click="visszautasitasPopup = false"
                variant="default"
                class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700 mr-5"
                >Nem</b-button
              >
              <b-button
                size="sm"
                @click="Visszautasitas()"
                variant="warning"
                class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
                >Igen</b-button
              >
            </div>
          </div>
        </b-popover>
        <button
          type="button"
          class="btn savebtn white mb-lg-5"
          @click="EljarasKezdemenyezese()"
          :disabled="eljarasKezdemenyezeseLoading || buttonsDisabled"
        >
          <b-spinner small v-if="eljarasKezdemenyezeseLoading"></b-spinner>
          Közvetítői eljárás elrendelése
        </button>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import { useSimpleModalHandler } from '../../../utils/modal';
import { eventBus, apiService } from '../../../main';
import { required, maxLength, requiredIf } from 'vuelidate/lib/validators';
import moment from 'moment';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { deselectDatatable } from '../../../utils/common';
import { mapGetters } from 'vuex';
import { hidrateForm } from '../../../utils/vueUtils';
import $ from 'jquery';
import { sortStr } from '../../../utils/sort';

export default {
  name: 'dontes-kozvetitoi-eljarasrol',
  data() {
    return {
      isFormLoading: false,
      title: 'Döntés közvetítői eljárásról',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      eljarasKezdemenyezeseLoading: false,
      visszautasitasLoading: false,
      visszautasitasPopup: false,
      isMounted: false,
      isKozvetitoIdRequired: false,
      formData: {
        maxDatum: null,
        kozvetitoOptions: [],
        values: {
          KozvetitoId: null,
          Datum: null,
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
  mounted: function() {
    this.isMounted = true;
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        KozvetitoId: {
          required: requiredIf(function() {
            return this.isKozvetitoIdRequired;
          }),
        },
        Datum: { required },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        this.naplobejegyzesIds = data.naplobejegyzesIds || [];
        this.LoadFormData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetDontesKozvetitoiEljarasrolModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });
        console.log(result);

        hidrateForm(this, this.formData.values, result);
        this.formData.kozvetitoOptions = result.KozvetitoOptions.sort(
          sortStr('text')
        );
        this.formData.maxDatum = result.MaxDatum;
        this.formData.values.Datum = moment(this.formData.values.Datum).format(
          'YYYY.MM.DD'
        );
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

      this.$v.$reset();
    },
    SaveFormData: async function(isVisszautasitas) {
      try {
        var result = await apiService.SaveDontesKozvetitoiEljarasrolModalData({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
            IsVisszautasitas: isVisszautasitas,
          },
        });
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        eventBus.$emit('Sidebar:ugyReszletek:refresh');

        this.$v.$reset();
        return true;
      } catch (err) {
        var errorMsg = 'Hiba a mentés során: ' + err;
        NotificationFunctions.AjaxError({
          title: this.title,
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
        console.log(errorMsg);
      }
    },
    async EljarasKezdemenyezese() {
      this.isKozvetitoIdRequired = true;
      if (!this.IsFormValid()) {
        return;
      }
      this.eljarasKezdemenyezeseLoading = true;
      var success = await this.SaveFormData(false);
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.eljarasKezdemenyezeseLoading = false;
    },
    async Visszautasitas() {
      this.isKozvetitoIdRequired = false;
      if (!this.IsFormValid()) {
        return;
      }
      this.visszautasitasPopup = false;
      this.visszautasitasLoading = true;
      var success = await this.SaveFormData(true);
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.visszautasitasLoading = false;
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
    OpenVisszautasitasPopup() {
      this.isKozvetitoIdRequired = false;
      if (!this.IsFormValid()) {
        return;
      }
      this.visszautasitasPopup = true;
    },
  },
  computed: {
    ...mapGetters({}),
    Select2KozvetitoOptions: function() {
      return {
        readOnly: false,
        disabled: false,
        data: this.formData.kozvetitoOptions,
        dropdownParent: $(this.$el),
      };
    },
    datePickerOptions() {
      let maxDate = this.formData.maxDatum;
      if (!maxDate) {
        maxDate = moment().toISOString();
      }
      return {
        format: 'YYYY.MM.DD',
        defaultDate: this.formData.values.Datum,
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment(maxDate).endOf('d'),
        minDate: moment().startOf('d'),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
  },
  watch: {
    id: {
      handler: function(newValue, oldValue) {},
      deep: true,
      immediate: true,
    },
  },
  components: {},
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

.close {
  opacity: 1 !important;
}

.checkbox-custom input[type='checkbox'],
.checkbox-custom input[type='radio'],
.checkbox-custom label::before,
.checkbox-custom label::after {
  width: 32px;
  height: 32px;
  top: 0;
}

.checkbox-custom label::after {
  line-height: 32px;
  font-size: 16px;
}
.checkbox-custom label {
  min-height: 32px;
}
</style>
