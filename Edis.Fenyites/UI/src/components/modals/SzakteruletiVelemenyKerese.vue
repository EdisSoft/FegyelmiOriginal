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
          41. Szakterületi vélemény kérése
          <p class="mt-10 mb-0 font-size-12">
            Adja meg a címzetti kört, és a felkérés tárgyát.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.CimzettSzakteruletiVezetok"
              >
                <k-select2-ajax
                  :options="cimzettSzakteruletiVezetokOptions"
                  :defaultValue="formData.szakteruletiVezetokdefaultValue"
                  v-model="$v.formData.values.CimzettSzakteruletiVezetok.$model"
                  class=""
                >
                </k-select2-ajax>
                <span class="text-help float-right">Címzett</span>
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
                <span class="text-help float-right">Határidő</span>
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
                  placeholder="Szakterületi vezetőknek címzett felkérés tárgya..."
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  :settings="{
                    placeholder:
                      'Szakterületi vezetőknek címzett felkérés tárgya...',
                  }"
                  name="leiras"
                  class="mb-0"
                ></k-summernote> -->
                <span class="text-help float-right">Szakvélemény tárgya</span>
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
        @click="SzakteruletiVelemenyKerese()"
        :disabled="szakteruletiVelemenyLoading || buttonsDisabled"
      >
        <b-spinner small v-if="szakteruletiVelemenyLoading"></b-spinner>
        Szakterületi vélemény kérése
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { required, minValue, maxLength } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';
// import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import $ from 'jquery';
import { sortStr } from '../../utils/sort';
import moment from 'moment';
import minLength from 'vuelidate/lib/validators/minLength';

export default {
  name: 'szakteruleti-velemeny-kerese',
  data() {
    return {
      isFormLoading: false,
      title: 'Szakterületi vélemény bekérése',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      szakteruletiVelemenyLoading: false,
      isMounted: false,
      formData: {
        maxHatarido: null,
        szakteruletiVezetokdefaultValue: [],
        values: {
          CimzettSzakteruletiVezetok: [],
          Hatarido: null,
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
        CimzettSzakteruletiVezetok: {
          minValueSelect: required,
        },
        Hatarido: {
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
        var result = await apiService.GetSzakteruletiVelemenyKereseModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });
        console.log(result);
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        hidrateForm(this, this.formData.values, result);

        this.formData.values.Hatarido = moment(
          this.formData.values.Hatarido
        ).format('YYYY.MM.DD');
        this.formData.maxHatarido = moment(result.MaxHatarido);
        this.formData.szakteruletiVezetokdefaultValue =
          result.SzakteruletiVezetokdefaultValue;
        this.isFormLoading = false;
        await this.$nextTick();
        this.$v.$reset();
      } catch (err) {
        console.log(err);
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        this.Hide();
      }
    },
    SaveFormData: async function() {
      try {
        console.log('data', this.formData.values);
        var result = await apiService.SaveSzakteruletiVelemenyKereseModalData({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
          },
        });
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        await this.$nextTick();
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
    async SzakteruletiVelemenyKerese() {
      if (!this.IsFormValid()) {
        return;
      }
      this.szakteruletiVelemenyLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.szakteruletiVelemenyLoading = false;
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
    cimzettSzakteruletiVezetokOptions: function() {
      if (!this.isMounted) {
        return;
      }
      return {
        placeholder: 'Szakterületi vezetők',
        apiHandler: apiService.FindSzakteruletiVezetokForSelect.bind(
          apiService
        ),
        multiple: true,
        dropdownParent: $(this.$el),
        minimumInputLength: 1,
        readOnly: false,
        disabled: false,
        isSzakteruleti: true,
      };
    },
    datePickerOptions() {
      return {
        format: 'YYYY.MM.DD',
        useCurrent: false,
        defaultDate: this.formData.values.Hatarido || moment(),
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        minDate: moment().startOf('d'),
        maxDate: moment(
          this.formData.maxHatarido || new Date().toISOString()
        ).endOf('d'),
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
