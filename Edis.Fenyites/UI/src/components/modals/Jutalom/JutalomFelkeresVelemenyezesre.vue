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
          3. {{ title }}
          <p class="mt-10 mb-0 font-size-12">
            Adja meg a címzett szakterületeket.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.CimzettSzakteruletek"
              >
                <k-select2
                  :options="cimzettSzakteruletekOptions"
                  v-model="$v.formData.values.CimzettSzakteruletek.$model"
                  class=""
                >
                </k-select2>
                <span class="text-help float-right">Címzett szakterületek</span>
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
        @click="FelkeresVelemenyezesre()"
        :disabled="felkeresVelemenyezesreLoading || buttonsDisabled"
      >
        <b-spinner small v-if="felkeresVelemenyezesreLoading"></b-spinner>
        Felkérés véleményzetésre
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../../main';
import { eventBus } from '../../../main';
import { FegyelmiUgyStoreTypes } from '../../../store/modules/fegyelmiugy';
import { required, minValue, maxLength } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../../utils/modal';
import { hidrateForm } from '../../../utils/vueUtils';
import { deselectDatatable } from '../../../utils/common';
import $ from 'jquery';
import moment from 'moment';

export default {
  name: 'jutalom-felkeres-velemenyezesre',
  data() {
    return {
      isFormLoading: false,
      title: 'Felkérés véleményezésre',
      jutalomId: null,

      felkeresVelemenyezesreLoading: false,
      isMounted: false,
      formData: {
        // maxHatarido: null,
        szakteruletiVezetokdefaultValue: [],
        szakteruletek: [],
        values: {
          JutalomId: null,
          CimzettSzakteruletek: [],
          Hatarido: null,
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
        CimzettSzakteruletek: {
          minValueSelect: required,
        },
        Hatarido: {
          required,
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        this.jutalomId = data.jutalomId || [];
        this.LoadFormData(data.jutalomId);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function(jutalomId) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetJutalomFelkeresVelemenyezesreModalData(
          {
            jutalomId: jutalomId,
          }
        );
        console.log(result);
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        hidrateForm(this, this.formData.values, result);

        if (this.formData.values.Hatarido) {
          this.formData.values.Hatarido = moment(
            this.formData.values.Hatarido
          ).format('YYYY.MM.DD');
        }
        // this.formData.maxHatarido = moment(result.MaxHatarido);
        // this.formData.jutalomId = result.JutalomId;
        this.formData.szakteruletek = result.Szakteruletek;
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
        var result = await apiService.SaveJutalomFelkeresVelemenyezesreModalData(
          {
            data: {
              ...this.formData.values,
              // FegyelmiUgyIds: this.fegyelmiUgyIds,
              // NaplobejegyzesIds: this.naplobejegyzesIds,
            },
          }
        );
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        eventBus.$emit('Sidebar:jutalmiUgyReszletek:refresh');
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
    async FelkeresVelemenyezesre() {
      if (!this.IsFormValid()) {
        return;
      }
      this.felkeresVelemenyezesreLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.felkeresVelemenyezesreLoading = false;
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
    cimzettSzakteruletekOptions() {
      return {
        data: this.formData.szakteruletek,
        dropdownParent: $(this.$el),
        multiple: true,
        tags: true,
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
        // maxDate: moment(
        //   this.formData.maxHatarido || new Date().toISOString()
        // ).endOf('d'),
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
