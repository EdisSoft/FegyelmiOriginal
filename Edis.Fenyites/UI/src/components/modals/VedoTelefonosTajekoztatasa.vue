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
          34. Védő telefonos tájékoztatása
          <p class="mt-10 mb-0 font-size-12">
            Adjuk meg az eljárás alá vont fogvatartott jogi képviseletének
            formáját.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.TajekoztatastNyujto"
              >
                <k-select2
                  :options="tajekoztatastNyujtoOptions"
                  v-model="$v.formData.values.TajekoztatastNyujto.$model"
                  class=""
                >
                </k-select2>
                <span class="text-help float-right">Tájékoztatást nyújtó</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.Tajekoztatott"
              >
                <b-form-input
                  v-model="$v.formData.values.Tajekoztatott.$model"
                  placeholder="Kit tájékoztatunk?"
                ></b-form-input>
                <span class="text-help float-right">Tájékoztatott neve</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.Telefonszam"
              >
                <b-form-input
                  v-model="$v.formData.values.Telefonszam.$model"
                  placeholder="Hívott telefonszám"
                ></b-form-input>
                <span class="text-help float-right"
                  >Tájékoztatott telefonszáma</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.TajekoztatasIdeje"
              >
                <date-picker
                  v-model="$v.formData.values.TajekoztatasIdeje.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right">Tájékoztatás ideje</span>
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
                  placeholder="Kérem adja meg a tájékoztatás rövid tartalmát..."
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  :settings="{
                    placeholder:
                      'Kérem adja meg a tájékoztatás rövid tartalmát',
                  }"
                  name="leiras"
                  class="mb-0"
                ></k-summernote> -->

                <span class="text-help float-right">Tájékoztatás tartama </span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.TajekoztatasSikertelensegenekOka"
              >
                <b-form-input
                  v-model="
                    $v.formData.values.TajekoztatasSikertelensegenekOka.$model
                  "
                  placeholder="Sikertelenség oka"
                ></b-form-input>
                <span class="text-help float-right"
                  >Tájékoztatás sikertelenségének oka</span
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
        data-dismiss="modal"
        @click="Nyomtatas()"
        :disabled="nyomtatasLoading || buttonsDisabled"
      >
        <b-spinner small v-if="nyomtatasLoading"></b-spinner>
        Nyomtatás
      </button>
      <button
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="Rogzites()"
        :disabled="rogzitesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="rogzitesLoading"></b-spinner>
        Tájékoztatás rögzítése
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import {
  required,
  minValue,
  maxLength,
  minLength,
} from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import $ from 'jquery';
import moment from 'moment';
import { deselectDatatable } from '../../utils/common';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import { sortStr } from '../../utils/sort';
import settings from '../../data/settings';

export default {
  name: 'vedo-telefonos-tajekoztatasa',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      nyomtatasLoading: false,
      title: 'Védő telefonos tájékoztatása',
      rogzitesLoading: false,
      formData: {
        tajekoztatastNyujtoOptions: [],
        values: {
          TajekoztatastNyujto: null,
          Tajekoztatott: '',
          Telefonszam: '',
          TajekoztatasIdeje: moment(),
          Leiras: '',
          TajekoztatasSikertelensegenekOka: '',
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
  validations() {
    var rules = {
      formData: {
        values: {
          TajekoztatastNyujto: {
            required,
            minValueSelect: minLength(1),
          },
          Tajekoztatott: {
            required,
            maxLength: maxLength(70),
          },
          Telefonszam: {
            required,
            maxLength: maxLength(20),
          },
          TajekoztatasIdeje: {
            required,
          },
          Leiras: {
            required,
            maxLength: maxLength(2000),
          },
          TajekoztatasSikertelensegenekOka: {},
        },
      },
    };
    if (this.formData.values.TajekoztatasSikertelensegenekOka.length > 0) {
      rules.formData.values.TajekoztatasSikertelensegenekOka = {
        maxLength: maxLength(200),
      };
    }
    return rules;
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
        var result = await apiService.GetVedoTelefonosTajekoztatasaModalData({
          data: {
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          },
        });
        this.formData.tajekoztatastNyujtoOptions = result.TajekoztatastNyujtoOptions.sort(
          sortStr('text')
        );
        hidrateForm(this, this.formData.values, result);

        this.formData.values.TajekoztatasIdeje = moment(
          this.formData.values.TajekoztatasIdeje
        ).format('YYYY.MM.DD HH:mm');
        this.isFormLoading = false;
      } catch (err) {
        console.log(err);
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
        } else {
          if (result.isSuccess == false) {
            NotificationFunctions.WarningAlert(this.title, result.message);
          } else {
            NotificationFunctions.AjaxError({
              title: 'Hiba',
              text: 'Adatok letöltése sikertelen',
              errorObj: err,
            });
          }
        }
        this.Hide();
      }

      this.$v.$reset();
    },
    SaveFormData: async function(isRogzites = false) {
      try {
        var result = await apiService.SaveVedoTelefonosTajekoztatasaModalData({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
            IsRogzites: isRogzites,
          },
        });
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(
          'Védő telefonos tájékoztatása',
          'Sikeres mentés'
        );
        if (isRogzites) eventBus.$emit('Sidebar:ugyReszletek:refresh');

        this.$v.$reset();
        return true;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Védő telefonos tájékoztatása',
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
        console.log(err);
      }
    },
    async Nyomtatas() {
      if (!this.IsFormValid()) return;
      this.nyomtatasLoading = true;
      let result = await this.SaveFormData(false);
      if (result) {
        // await NyomtatvanyFunctions.VedoTelefonosTajekoztatasaNyomtatas({
        //   naplobejegyzesIds: this.naplobejegyzesIds,
        // });
        await NyomtatvanyFunctions.VedoTelefonosTajekoztatasaNyomtatasDocX({
          naplobejegyzesId: this.naplobejegyzesIds[0],
        });
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
      }
      this.nyomtatasLoading = false;
    },
    async Rogzites() {
      if (!this.IsFormValid()) {
        return;
      }
      this.rogzitesLoading = true;
      let success = await this.SaveFormData(true);
      if (success) {
        deselectDatatable();
        this.Hide();
      }
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
    tajekoztatastNyujtoOptions: function() {
      return {
        data: this.formData.tajekoztatastNyujtoOptions,
        dropdownParent: $(this.$el),
        placeholder: 'Kérem válasszon...',
      };
    },
    datePickerOptions() {
      return {
        format: 'YYYY.MM.DD HH:mm',
        defaultDate: moment(),
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment().endOf('d'),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
        isModalVisible: this.isModalVisible,
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
