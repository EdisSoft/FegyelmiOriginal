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
          9. Helyszíni szemle
        </h4>
      </div>
      <div class="modal-body pl-25 pt-20 pb-40">
        <div>
          <div class="row pr-10">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.Datuma"
              >
                <date-picker
                  v-model="$v.formData.values.Datuma.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right">Szemle dátuma</span>
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
                  placeholder="Kérem írja le a helyszínt..."
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  :settings="{ placeholder: 'Kérem írja le a helyszínt...' }"
                  name="leiras"
                  class="mb-0"
                ></k-summernote> -->
                <span class="text-help float-right">Helyszín leírása</span>
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
        v-on:click="Save"
        :disabled="mentesFolyamatban || buttonsDisabled"
      >
        <b-spinner small v-if="mentesFolyamatban"></b-spinner>
        Kész
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { AppStoreTypes } from '../../store/modules/app';
import { eventBus } from '../../main';
import settings from '../../data/settings';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { getUgyszam } from '../../utils/fenyitesUtils';
import { required, minValue } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import moment from 'moment';
import $ from 'jquery';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';

export default {
  name: 'helyszini-szemle',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      fegyelmiUgy: null,
      title: 'Helyszíni szemle',
      mentesFolyamatban: false,
      formData: {
        values: {
          NaplobejegyzesIds: [],
          Datuma: null,
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
    console.log(this.modalOpts);
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        Datuma: {
          required,
        },
        Leiras: {
          required,
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.formData.values.NaplobejegyzesIds = data.naplobejegyzesIds;
        this.LoadFegyelmiUgyData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFegyelmiUgyDataHelysziniSzemlehez({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        this.isFormLoading = false;
      } catch (err) {
        console.log('error' + err);
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });

        this.Hide();
      }

      //console.log('Result id: ' + result.FegyelmiUgyId);

      //this.formData.kivizsgaloSzemelyOptions = result.KivizsgaloSzemelyOptions.sort(
      //  function(a, b) {
      //    return ('' + a.text).localeCompare(b.text);
      //  }
      //);

      //this.formData.dontestHozoSzemelyOptions = result.DontestHozoSzemelyOptions.sort(
      //  function(a, b) {
      //    return ('' + a.text).localeCompare(b.text);
      //  }
      //);

      hidrateForm(this, this.formData.values, result);

      this.formData.values.Datuma = moment(this.formData.values.Datuma).format(
        'YYYY.MM.DD'
      );

      this.$v.$reset();
    },
    Save: async function() {
      this.mentesFolyamatban = true;
      try {
        if (this.$v.$invalid) {
          this.$v.$touch();
          this.mentesFolyamatban = false;
          return;
        }

        var result = await apiService.FegyelmiUgyHelysziniSzemleMentes({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
          },
        });
        if (result.success == true) {
          NotificationFunctions.SuccessAlert(
            'Helyszíni szemle mentés',
            result.message
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
          this.$v.$reset();
        } else {
          NotificationFunctions.WarningAlert(
            'Helyszíni szemle mentés',
            result.message
          );
        }
        this.mentesFolyamatban = false;
      } catch (err) {
        var errorMsg = 'Hiba a mentés során: ' + err;
        NotificationFunctions.AjaxError({
          title: 'Helyszíni szemle mentés',
          text: '',
          errorObj: err,
        });
        console.log(errorMsg);
        this.mentesFolyamatban = false;
      }
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),

    datePickerOptions() {
      //let minDate = this.formData.ElrendelesDatuma;
      //if (!minDate) {
      //  minDate = moment()
      //    .subtract({ days: 2 })
      //    .toISOString();
      //}

      return {
        format: 'YYYY.MM.DD',
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment().endOf('d'),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
  },
  watch: {
    id: {
      handler: function(newValue, oldValue) {
        if (newValue && oldValue != newValue) {
          this.fegyelmiUgy = this.$store.getters[
            FegyelmiUgyStoreTypes.getters.getFegyelmiUgyById
          ](newValue);
        }
      },
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
