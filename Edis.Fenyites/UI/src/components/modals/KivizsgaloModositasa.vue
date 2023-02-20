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
          22. Kivizsgáló módosítása
        </h4>
      </div>
      <div class="modal-body pl-25 pt-25 pb-50">
        <div>
          <div class="row pr-10">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.KivizsgaloSzemelySid"
              >
                <k-select2
                  :options="KivizsgaloSzemelyOptions"
                  v-model="$v.formData.values.KivizsgaloSzemelySid.$model"
                  class=""
                  placeholder="Kivizsgáló személy"
                >
                </k-select2>
                <span class="text-help float-right">Kivizsgáló személy</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <b-form-input
                readonly="readonly"
                disabled="disabled"
                :value="formData.values.KivizsgalasiHatarido | toShortDate"
                placeholder=""
              ></b-form-input>
              <span class="text-help float-right">Kivizsgálási határidő</span>
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
          v-on:click="KivizsgaloModositasa"
          :disabled="modositasIsLoading"
        >
          <b-spinner small v-if="modositasIsLoading"></b-spinner>
          Kivizsgáló módosítása
        </button>
      </div>
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
  name: 'fegyelmi-ugy-kivizsgalo-modositasa',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      modositasIsLoading: false,
      fegyelmiUgy: null,
      title: 'Kivizsgáló módosítása',
      formData: {
        kivizsgaloSzemelyOptions: [],
        dontestHozoSzemelyOptions: [],
        values: {
          KivizsgaloSzemelySid: 0,
          DontestHozoSzemelySid: 0,
          KivizsgalasiHatarido: null,
          JogiKepviseletetKer: false,
        },
        checked1: false,
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
        KivizsgaloSzemelySid: {
          required,
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        console.log({ data });

        if (data.fegyelmiUgyIds) {
          this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        }
        this.LoadFegyelmiUgyData(data.fegyelmiUgyIds);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFegyelmiUgyDataKivizsgaloModositashoz({
          ids: fegyelmiUgyIds,
        });
        //console.log('Result ids: ' + result.FegyelmiUgyIds);

        this.formData.kivizsgaloSzemelyOptions = result.KivizsgaloSzemelyOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        hidrateForm(this, this.formData.values, result);
        //console.log('khatáridő: ', this.formData.values.KivizsgalasiHatarido);
        // this.formData.values.KivizsgalasiHatarido = moment(
        //   this.formData.values.KivizsgalasiHatarido
        // ); //.format('YYYY.MM.DD HH:mm');
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
    KivizsgaloModositasa: async function() {
      try {
        if (this.$v.$invalid) {
          this.$v.$touch();
          return;
        }
        this.modositasIsLoading = true;
        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
        };
        var result = await apiService.FegyelmiUgyKivizsgaloModositasaMentes({
          data,
        });
        if (result.isSuccess == true) {
          NotificationFunctions.SuccessAlert(
            'Fegyelmi ügy kivizsgáló módosítása',
            result.message
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
        } else {
          NotificationFunctions.WarningAlert(
            'Fegyelmi ügy kivizsgáló módosítása',
            result.message
          );
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Hiba történt a fegyelmi ügy kivizsgáló módosítása során',
          errorObj: err,
        });
        console.log(err);
      }
      this.modositasIsLoading = false;
    },
    GetUgyszam() {
      return getUgyszam(this.fegyelmiUgy);
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),
    KivizsgaloSzemelyOptions: function() {
      return {
        data: this.formData.kivizsgaloSzemelyOptions,
        dropdownParent: $(this.$el),
        placeholder: 'Válasszon kivizsgálót...',
      };
    },
    datePickerOptions() {
      return {
        format: 'YYYY.MM.DD',
        //useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment()
          .add({ months: 1 })
          .endOf('d'),
        minDate: moment()
          .add({ days: 1 })
          .startOf('d'),
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
</style>
