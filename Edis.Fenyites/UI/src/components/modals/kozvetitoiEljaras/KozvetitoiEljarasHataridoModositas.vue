<template>
  <basic-loader :isLoading="isFormLoading">
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
          46. Határidő módosítása
          <p class="mt-10 mb-0 font-size-12">
            Közvetítői eljárás új határidejének megadása
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
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
                  >Meghosszabbított közvetítői eljárás határideje</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.Leiras"
              >
                <textarea-autosize
                  v-model="$v.formData.values.Leiras.$model"
                  :min-height="30"
                  class="w-p100 mb-0"
                  name="leiras"
                  :rows="1"
                  placeholder="Indoklás"
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  :settings="{ placeholder: 'Indoklás...' }"
                  name="leiras"
                  class="mb-0"
                ></k-summernote> -->
                <span class="text-help float-right"
                  >Határidő hosszabbítás indoklása</span
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
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="Rogzites()"
        :disabled="rogzitesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="rogzitesLoading"></b-spinner>
        Kivizsgálás folytatása határidő módosítással
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';

import $ from 'jquery';
import moment from 'moment';
import { useSimpleModalHandler } from '../../../utils/modal';
import { required, maxLength } from 'vuelidate/lib/validators';
import { apiService, eventBus } from '../../../main';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { deselectDatatable } from '../../../utils/common';
import { hidrateForm } from '../../../utils/vueUtils';

export default {
  name: 'kozvetitoi-eljaras-hatarido-modositas',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      rogzitesLoading: false,
      formData: {
        MaxDatum: null,
        MinDatum: null,
        values: {
          Datum: null,
          Leiras: '',
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
    console.log('asdsda', this.modalOpts);
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        Datum: {
          required,
        },
        Leiras: {
          required,
          maxLength: maxLength(1500),
        },
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
        var result = await apiService.GetKozvetitoiEljarasHataridoModositasModalData(
          {
            fegyelmiUgyIds: fegyelmiUgyIds,
          }
        );
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        if (moment(result.MaxDatum).isBefore(moment().endOf('d'))) {
          NotificationFunctions.WarningAlert(
            'Határidő módosítása',
            'Nem hosszabbítható meg a határidő, mert már a meghosszabbítással is múltbéli lenne: ' +
              this.$options.filters.toShortDate(result.MaxDatum)
          );
          this.Hide();
          return;
        }

        this.formData.MaxDatum = result.MaxDatum;
        this.formData.MinDatum = result.MinDatum;
        hidrateForm(this, this.formData.values, result);

        this.formData.values.Datum = moment(this.formData.values.Datum).format(
          'YYYY.MM.DD'
        );
        this.isFormLoading = false;
      } catch (err) {
        console.log(err);
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        this.Hide();
      }

      this.$v.$reset();
    },
    SaveFormData: async function() {
      try {
        var result = await apiService.SaveKozvetitoiEljarasHataridoModositasModalData(
          {
            data: {
              ...this.formData.values,
              FegyelmiUgyIds: this.fegyelmiUgyIds,
            },
          }
        );
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(
          'Határidő módosítása',
          'Sikeres mentés'
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        this.Hide();
        this.$v.$reset();
        return true;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Határidő módosítása',
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
        console.log(err);
      }
    },
    async Rogzites() {
      if (!this.IsFormValid()) {
        return;
      }
      this.rogzitesLoading = true;
      await this.SaveFormData();
      this.rogzitesLoading = false;
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
  },
  computed: {
    ...mapGetters({}),
    datePickerOptions() {
      let maxDate = this.formData.MaxDatum;
      if (!maxDate) {
        maxDate = moment().toISOString();
      }
      let minDate = this.formData.MinDatum;
      if (!minDate) {
        minDate = moment().toISOString();
      }
      return {
        format: 'YYYY.MM.DD',
        defaultDate: this.formData.values.Datum,
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment(maxDate).endOf('d'),
        minDate: moment(minDate).startOf('d'),
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
