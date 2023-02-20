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
          26. Feljegyzés megbeszélésről, megállapodás rögzítése
          <p class="mt-10 mb-0 font-size-12">
            Visszautalás kivizsgálási szakaszba, új határidő megadásával
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.SertettKepviseloje"
              >
                <b-form-input
                  v-model="$v.formData.values.SertettKepviseloje.$model"
                  placeholder="Adja meg a sértett képviselőjét"
                ></b-form-input>
                <span class="text-help float-right">Sértett képviselője</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.EljarasAlaVontKepviseloje"
              >
                <b-form-input
                  v-model="$v.formData.values.EljarasAlaVontKepviseloje.$model"
                  placeholder="Adja meg az elkövető képviselőjét"
                ></b-form-input>
                <span class="text-help float-right"
                  >Eljárás alá vont képviselője</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.FeljegyzesMegbeszelesrol"
              >
                <textarea-autosize
                  v-model="$v.formData.values.FeljegyzesMegbeszelesrol.$model"
                  :min-height="30"
                  class="w-p100 mb-0"
                  name="feljegyzes"
                  :rows="1"
                  placeholder="Kérem, adja meg a megbeszélés(ek)en elhangzottakat."
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.FeljegyzesMegbeszelesrol.$model"
                  :settings="{
                    placeholder:
                      'Kérem, adja meg a megbeszélés(ek)en elhangzottakat.',
                  }"
                  class="mb-0"
                  name="feljegyzes"
                ></k-summernote> -->
                <span class="text-help float-right"
                  >Feljegyzés megbeszélésről</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.Megallapodas"
              >
                <textarea-autosize
                  v-model="$v.formData.values.Megallapodas.$model"
                  :min-height="30"
                  class="w-p100 mb-0"
                  name="megallapodas"
                  :rows="1"
                  placeholder="Kérem, adja meg, miben állapodtak meg a felek a közvetítői megbeszéléseken."
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Megallapodas.$model"
                  :settings="{
                    placeholder:
                      'Kérem, adja meg, miben állapodtak meg a felek a közvetítői megbeszéléseken.',
                  }"
                  name="megallapodas"
                  class="mb-0"
                ></k-summernote> -->
                <span class="text-help float-right">Megállapodás</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.Hatarido"
              >
                <date-picker
                  v-model="$v.formData.values.Hatarido.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right"
                  >Megállapodás teljesítési határidő</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.ReintegraciosTisztSid"
              >
                <!-- <select></select> -->
                <k-select2
                  :options="select2ReintegraciosTisztOptions"
                  v-model="$v.formData.values.ReintegraciosTisztSid.$model"
                ></k-select2>
                <span class="text-help float-right">Reintegrációs tiszt</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.EljarasAlaVontatErintoKoltsegek"
              >
                <CurrencyInput
                  v-model="
                    $v.formData.values.EljarasAlaVontatErintoKoltsegek.$model
                  "
                  class="currency-input form-control"
                />
                <span class="text-help float-right"
                  >Közvetítés során felmerült, eljárás alá vontat érintő
                  költségek</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.SertettetErintoKoltsegek"
              >
                <CurrencyInput
                  class="currency-input form-control"
                  v-model="$v.formData.values.SertettetErintoKoltsegek.$model"
                />
                <span class="text-help float-right"
                  >Közvetítés során felmerült, sértettet érintő költségek</span
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
        @click="Nyomtatas()"
        type="button"
        class="btn bg-blue-grey-600 white mb-lg-5"
      >
        <b-spinner small v-if="isLoadingNyomtatas"></b-spinner>
        Nyomtatás
      </button>
      <button
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="Megallapodas()"
        :disabled="eljarasKezdemenyezeseLoading || buttonsDisabled"
      >
        <b-spinner small v-if="eljarasKezdemenyezeseLoading"></b-spinner>
        Megállapodás rögzítése
      </button>
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
import { sortStr, sortNumber } from '../../../utils/sort';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';

export default {
  name: 'feljegyzes-es-megallapodas',
  data() {
    return {
      isFormLoading: false,
      title: 'Feljegyzés megbeszélésről, megállapodás rögzítése',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      eljarasKezdemenyezeseLoading: false,
      isLoadingNyomtatas: false,
      isMounted: false,
      formData: {
        maxDatum: null,
        minDatum: null,
        reintegraciosTisztOptions: [],
        values: {
          SertettKepviseloje: '',
          ReintegraciosTisztSid: null,
          EljarasAlaVontKepviseloje: '',
          FeljegyzesMegbeszelesrol: '',
          Megallapodas: '',
          Hatarido: null,
          EljarasAlaVontatErintoKoltsegek: null,
          SertettetErintoKoltsegek: null,
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
        SertettKepviseloje: {
          //required,
          maxLength: maxLength(100),
        },
        EljarasAlaVontKepviseloje: {
          //required,
          maxLength: maxLength(100),
        },
        FeljegyzesMegbeszelesrol: {
          maxLength: maxLength(4000),
          required: requiredIf(function() {
            let result = false;
            if (!this.formData.values.Megallapodas) {
              result = true;
            }
            return result;
          }),
        },
        Megallapodas: {
          required: requiredIf(function() {
            let result = false;
            if (
              this.formData.values.Hatarido ||
              this.formData.values.ReintegraciosTisztSid
            ) {
              result = true;
            }
            return result;
          }),
          maxLength: maxLength(4000),
        },
        Hatarido: {
          required: requiredIf(function() {
            let result = false;
            if (this.formData.values.Megallapodas) {
              result = true;
            }
            return result;
          }),
        },
        ReintegraciosTisztSid: {
          required: requiredIf(function() {
            let result = false;
            if (this.formData.values.Megallapodas) {
              result = true;
            }
            return result;
          }),
        },
        EljarasAlaVontatErintoKoltsegek: {},
        SertettetErintoKoltsegek: {},
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
        var result = await apiService.GetFeljegyzesEsMegallapodasModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });
        console.log(result);

        hidrateForm(this, this.formData.values, result);
        this.formData.maxDatum = result.MaxDatum;
        this.formData.minDatum = result.MinDatum;

        this.formData.reintegraciosTisztOptions = result.ReintegraciosTisztOptions.sort(
          sortStr('text')
        );
        if (this.formData.values.Hatarido) {
          this.formData.values.Hatarido = moment(
            this.formData.values.Hatarido
          ).format('YYYY.MM.DD');
        } else {
          this.formData.values.Hatarido = null;
        }

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
    SaveFormData: async function(vegleges) {
      try {
        var formdata = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
          NaplobejegyzesIds: this.naplobejegyzesIds,
          Vegleges: vegleges,
        };
        if (formdata.Megallapodas) {
          formdata.Megallapodas = formdata.Megallapodas.replace(
            /\t/g,
            '&nbsp;&nbsp;&nbsp;&nbsp;'
          );
        }
        if (formdata.FeljegyzesMegbeszelesrol) {
          formdata.FeljegyzesMegbeszelesrol = formdata.FeljegyzesMegbeszelesrol.replace(
            /\t/g,
            '&nbsp;&nbsp;&nbsp;&nbsp;'
          );
        }
        console.log('Feljegyzés', formdata.FeljegyzesMegbeszelesrol);

        var result = await apiService.SaveFeljegyzesEsMegallapodasModalData({
          data: formdata,
        });
        console.log({
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
          NaplobejegyzesIds: this.naplobejegyzesIds,
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
    async Megallapodas() {
      if (!this.IsFormValid()) {
        return;
      }
      this.eljarasKezdemenyezeseLoading = true;
      var success = await this.SaveFormData(true);
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.eljarasKezdemenyezeseLoading = false;
    },
    async Nyomtatas() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isLoadingNyomtatas = true;
      let result = await this.SaveFormData(false);
      this.isLoadingNyomtatas = false;
      NyomtatvanyFunctions.MegallapodasEsFeljegyzesNyomtatas({
        naplobejegyzesIds: this.naplobejegyzesIds,
      });
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
      let maxDate = this.formData.maxDatum;
      if (!maxDate) {
        maxDate = moment().toISOString();
      }
      let minDate = this.formData.minDatum;
      if (!minDate) {
        minDate = moment().toISOString();
      }
      console.log(minDate, maxDate);
      let opt = {
        format: 'YYYY.MM.DD',
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
      if (this.formData.values.Hatarido) {
        opt.defaultDate = this.formData.values.Hatarido;
      }
      return opt;
    },
    select2ReintegraciosTisztOptions: function() {
      return {
        data: this.formData.reintegraciosTisztOptions,
        dropdownParent: $(this.$el),
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
