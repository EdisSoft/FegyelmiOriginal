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
          48. Közvetítői eljárás kezdeményezése
          <p class="mt-10 mb-0 font-size-12">
            Javaslat közvetítői eljárás lefolytatására
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.SertettId"
              >
                <k-select2
                  :options="Select2SertettOptions"
                  v-model="$v.formData.values.SertettId.$model"
                  class=""
                  placeholder="Sértett"
                >
                </k-select2>
                <span class="text-help float-right">Sértett</span>
              </k-vuelidate-error-extractor>
            </div>
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
                :validator="$v.formData.values.Leiras"
              >
                <textarea-autosize
                  v-model="$v.formData.values.Leiras.$model"
                  :min-height="30"
                  class="w-p100 mb-0"
                  name="leiras"
                  :rows="1"
                  placeholder="Kérem, adja meg az eljárással kapcsolatos javaslatát."
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  :settings="{
                    placeholder:
                      'Kérem, adja meg az eljárással kapcsolatos javaslatát.',
                  }"
                  name="leiras"
                  class="mb-0"
                ></k-summernote> -->
                <span class="text-help float-right"
                  >Reintegrációs tiszt javaslata</span
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
        class="btn bg-blue-grey-600 white mb-lg-5"
        :disabled="nyomtatasLoading || buttonsDisabled"
        @click="Nyomtatas()"
      >
        <b-spinner small v-if="nyomtatasLoading"></b-spinner>
        Nyomtatás
      </button>
      <button
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="EljarasKezdemenyezese()"
        :disabled="eljarasKezdemenyezeseLoading || buttonsDisabled"
      >
        <b-spinner small v-if="eljarasKezdemenyezeseLoading"></b-spinner>
        Eljáras kezdeményezése
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { useSimpleModalHandler } from '../../../utils/modal';
import { eventBus, apiService } from '../../../main';
import { required, maxLength } from 'vuelidate/lib/validators';
import moment from 'moment';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { deselectDatatable } from '../../../utils/common';
import { mapGetters } from 'vuex';
import { hidrateForm } from '../../../utils/vueUtils';
import $ from 'jquery';
import { sortStr } from '../../../utils/sort';

import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';
import { FegyelmiUgyFunctions } from '../../../functions/FegyelmiUgyFunctions';

export default {
  name: 'kozvetitoi-eljaras-kezdemenyezese',
  data() {
    return {
      isFormLoading: false,
      title: 'Közvetítői eljárás kezdeményezése',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      eljarasKezdemenyezeseLoading: false,
      nyomtatasLoading: false,
      isMounted: false,
      formData: {
        sertettOptions: [],
        values: {
          SertettId: null,
          SertettKepviseloje: '',
          EljarasAlaVontKepviseloje: '',
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
    this.isMounted = true;
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        SertettId: { required },
        SertettKepviseloje: {
          maxLength: maxLength(100),
        },
        EljarasAlaVontKepviseloje: {
          maxLength: maxLength(100),
        },
        Leiras: {
          required,
          maxLength: maxLength(4000),
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
        var result = await apiService.GetKozvetitoiEljarasKezdemenyezeseModalData(
          {
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          }
        );
        console.log(result);

        hidrateForm(this, this.formData.values, result);
        this.formData.zarkabaHelyezesOptions = result.ZarkabaHelyezesOptions;
        this.formData.sertettOptions = result.SertettOptions.sort(
          sortStr('text')
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

    SaveFormData: async function(isRogzites) {
      var dataForValidation = {
        FegyelmiUgyIds: this.fegyelmiUgyIds,
        SertettId: this.formData.values.SertettId,
      };
      var validationResult = await FegyelmiUgyFunctions.CanKozvetitoiEljarasBeSaved(
        dataForValidation
      );
      if (validationResult.canBeSaved) {
        try {
          var result = await apiService.SaveKozvetitoiEljarasKezdemenyezeseModalData(
            {
              data: {
                ...this.formData.values,
                FegyelmiUgyIds: this.fegyelmiUgyIds,
                NaplobejegyzesIds: this.naplobejegyzesIds,
                IsRogzites: isRogzites,
              },
            }
          );
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
      } else {
        NotificationFunctions.WarningAlert(validationResult.warningMessage);
      }
    },
    async EljarasKezdemenyezese() {
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
      this.nyomtatasLoading = true;
      var success = await this.SaveFormData(false);
      if (success) {
        NyomtatvanyFunctions.KozvetitoiEljarasonReszvetelNyomtatas({
          naplobejegyzesIds: this.naplobejegyzesIds,
        });
      }
      this.nyomtatasLoading = false;
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
    Select2SertettOptions: function() {
      return {
        readOnly: false,
        disabled: false,
        data: this.formData.sertettOptions,
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
