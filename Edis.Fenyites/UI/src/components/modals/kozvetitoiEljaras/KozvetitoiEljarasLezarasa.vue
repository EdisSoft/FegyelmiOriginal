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
          27. Közvetítői eljárás lezárása
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
                :validator="$v.formData.values.EljarasEredmenye"
              >
                <k-select2
                  :options="Select2EljarasEredmenyeOptions"
                  v-model="$v.formData.values.EljarasEredmenye.$model"
                  class=""
                  placeholder="Válasszon"
                >
                </k-select2>
                <span class="text-help float-right"
                  >Közvetítői eljárás eredménye</span
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
        @click="EljarasLezarasaEredmenytelenul()"
        :disabled="eljarasLezarasaLoading || buttonsDisabled"
        v-if="!IsEredmenyes"
      >
        <b-spinner small v-if="eljarasLezarasaLoading"></b-spinner>

        <span v-if="kellFelfuggesztesMegszuntetesModal">
          Lezárás és tovább a felfüggesztés megszakításához
        </span>
        <span v-else>
          Lezárás és felfüggesztés megszakítása
        </span>
      </button>
      <button
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="EljarasLezarasaEredmenyesen()"
        :disabled="eljarasLezarasaLoading || buttonsDisabled"
        v-if="IsEredmenyes"
      >
        <b-spinner small v-if="eljarasLezarasaLoading"></b-spinner>
        Lezárás és tovább a megszüntető határozathoz
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
import { deselectDatatable, stringToBoolean } from '../../../utils/common';
import { mapGetters } from 'vuex';
import { hidrateForm } from '../../../utils/vueUtils';
import $ from 'jquery';
import { sortStr } from '../../../utils/sort';
import Cimkek from '../../../data/enums/Cimkek';
import ModalTipus from '../../../data/enums/modalTipus';

export default {
  name: 'kozvetitoi-eljaras-lezarasa',
  data() {
    return {
      isFormLoading: false,
      title: 'Közvetítői eljárás lezárása',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      eljarasLezarasaLoading: false,
      isMounted: false,
      fegyelmiUgyStatusz: null,
      formData: {
        eljarasEredmenyeOptions: [],
        values: {
          EljarasEredmenye: null,
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
        EljarasEredmenye: { required },
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
        var result = await apiService.GetKozvetitoiEljarasLezarasaModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });
        console.log(result);

        hidrateForm(this, this.formData.values, result);
        this.formData.eljarasEredmenyeOptions = result.EljarasEredmenyeOptions.sort(
          sortStr('text')
        );
        this.fegyelmiUgyStatusz = result.FegyelmiUgyStatusz;
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
    SaveFormData: async function() {
      try {
        var result = await apiService.SaveKozvetitoiEljarasLezarasaModalData({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
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
    async EljarasLezarasaEredmenyesen() {
      if (!this.IsFormValid()) {
        return;
      }
      this.eljarasLezarasaLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        var data = {
          fegyelmiUgyId: this.fegyelmiUgyIds[0],
          naplobejegyzesId: null,
          modalType: ModalTipus.HatarozatMegszuntetese,
        };
        eventBus.$emit('Modal:hatarozat-rogzitese', {
          state: true,
          data,
        });

        deselectDatatable();
        this.Hide();
      }
      this.eljarasLezarasaLoading = false;
    },
    async EljarasLezarasaEredmenytelenul() {
      if (!this.IsFormValid()) {
        return;
      }
      this.eljarasLezarasaLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        if (this.kellFelfuggesztesMegszuntetesModal) {
          var data = {
            fegyelmiUgyIds: this.fegyelmiUgyIds,
            naplobejegyzesIds: [],
          };
          eventBus.$emit('Modal:felfuggesztes-megszuntetes', {
            state: true,
            data,
          });
        }
        deselectDatatable();
        this.Hide();
      }
      this.eljarasLezarasaLoading = false;
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
    Select2EljarasEredmenyeOptions: function() {
      return {
        readOnly: false,
        disabled: false,
        data: this.formData.eljarasEredmenyeOptions,
        dropdownParent: $(this.$el),
      };
    },
    IsEredmenyes() {
      return stringToBoolean(this.formData.values.EljarasEredmenye) == true;
    },
    kellFelfuggesztesMegszuntetesModal() {
      return (
        this.fegyelmiUgyStatusz ==
        Cimkek.FegyelmiUgyStatusza.KivizsgalasFolyamatban
      );
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
