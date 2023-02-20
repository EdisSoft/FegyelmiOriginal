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
          32. Felfüggesztés megszüntetése
          <p class="mt-10 mb-0 font-size-12">
            Az ügy visszakerül az eredeti státuszba. <br />
            Válassza ki a kivizsgálót, és hagyja jóvá, vagy csökkentse a
            javasolt határidőt.
          </p>
        </h4>
      </div>
      <div class="modal-body pl-25 pt-25 pb-50">
        <div>
          <div class="row pr-10">
            <div class="col-md-12">
              <div class="unique-diabled-field">
                Kivizsgálás folyamatban
              </div>
              <span class="text-help float-right">Státusz</span>
            </div>
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
                <span class="text-help float-right">Kivizsgáló</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.KivizsgalasiHatarido"
              >
                <date-picker
                  v-model="$v.formData.values.KivizsgalasiHatarido.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right">Új határidő</span>
              </k-vuelidate-error-extractor>
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
          v-on:click="Mentes"
          :disabled="rogzitesLoading || buttonsDisabled"
        >
          <b-spinner small v-if="rogzitesLoading"></b-spinner>
          Eljárás folytatása
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

export default {
  name: 'felfuggesztes-megszuntetes',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      fegyelmiUgy: null,
      isMounted: false,
      title: 'Felfüggesztés megszüntetése',
      hatarido: '',
      rogzitesLoading: false,
      formData: {
        kivizsgaloSzemelyOptions: [],
        values: {
          KivizsgaloSzemelySid: 0,
          KivizsgalasiHatarido: null,
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
        KivizsgaloSzemelySid: {
          required,
        },
        KivizsgalasiHatarido: {
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
        } else {
          this.fegyelmiUgyIds = [data.fegyelmiUgyId];
        }
        this.LoadFegyelmiUgyData(data.fegyelmiUgyIds);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFelfuggesztesMegszuntetes({
          ids: fegyelmiUgyIds,
        });
        console.log('Result ids: ' + result.FegyelmiUgyIds);
        this.formData.kivizsgaloSzemelyOptions = result.KivizsgaloSzemelyOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        hidrateForm(this, this.formData.values, result);
        this.hatarido = moment(
          this.formData.values.KivizsgalasiHatarido
        ).format('YYYY.MM.DD');
        this.formData.values.KivizsgalasiHatarido = moment(
          this.formData.values.KivizsgalasiHatarido
        ).format('YYYY.MM.DD HH:mm');
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
    Mentes: async function() {
      try {
        this.rogzitesLoading = true;
        if (this.$v.$invalid) {
          this.$v.$touch();
          return;
        }
        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
        };
        var result = await apiService.SaveFelfuggesztesMegszuntetes({
          data,
        });

        if (result.result == true) {
          var hataridoStr =
            this.formData.values.KivizsgalasiHatarido > this.hatarido &&
            this.hatarido != ''
              ? this.hatarido
              : this.formData.values.KivizsgalasiHatarido;
          NotificationFunctions.SuccessAlert(
            'Eljárás folytatása ' + hataridoStr + '-i határidővel.',
            result.message
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
        }

        this.Hide();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Hiba történt felfüggesztés megszüntetésekor',
          errorObj: err,
        });
        console.log(err);
      }
      this.rogzitesLoading = false;
    },
    GetKivizsgalasiHatarido() {
      return this.KivizsgalasiHatarido;
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
        placeholder: 'Kivizsgáló személy',
      };
    },
    datePickerOptions() {
      return {
        format: 'YYYY.MM.DD',
        //useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        defaultDate: moment(),
        maxDate: moment()
          .add({ months: 1 })
          .endOf('d'),
        minDate: moment().startOf('d'),
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
