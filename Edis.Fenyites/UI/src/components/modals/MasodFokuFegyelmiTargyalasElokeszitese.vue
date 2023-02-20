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
          23. II. fokú fegyelmi tárgyalás előkészítése
          <p class="mt-10 mb-0 font-size-12">
            Fegyelmi tárgyalás időpontjának, és a határozathozó jogkörének
            megadása
          </p>
        </h4>
      </div>
      <div class="modal-body pl-25 pt-20 pb-40">
        <div>
          <div class="row pr-10">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material static-select2"
                :validator="$v.formData.values.HatarozatHozoSzemelyCimkeId"
              >
                <k-select2
                  :options="hatarozatHozoSzemelyOptions"
                  v-model="
                    $v.formData.values.HatarozatHozoSzemelyCimkeId.$model
                  "
                  class=""
                  placeholder="Határozat hozó jögköre"
                  disabled
                >
                </k-select2>
                <span class="text-help float-right"
                  >Határozat hozó jögköre</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.TargyalasIdopontja"
              >
                <date-picker
                  v-model="$v.formData.values.TargyalasIdopontja.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right"
                  >Fegyelmi tárgyalás időpontja</span
                >
              </k-vuelidate-error-extractor>
            </div>
          </div>
        </div>
      </div>
      <div
        class="modal-footer bg-blue-grey-400 py-15 d-flex justify-content-between"
      >
        <div class="modalorszam">
          <span>15</span>
        </div>
        <div class="text-right">
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
            v-on:click="TargyalasiIdopontKituzese"
            :disabled="isLoading || buttonsDisabled"
          >
            <b-spinner small v-if="isLoading"></b-spinner>
            II. fokú fegyelmi tárgyalás időpontját kitűzöm
          </button>
        </div>
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
  name: 'k-II-foku-fegyelmi-targyalas-elokeszitese',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyId: 0,
      title: 'II. fokú fegyelmi tárgyalás előkészítése',
      isLoading: false,
      fegyelmiUgy: null,
      formData: {
        hatarozatHozoSzemelyOptions: [],
        targyalasMaxIdopontja: null,
        values: {
          FegyelmiUgyId: 0,
          HatarozatHozoSzemelyCimkeId: 0,
          TargyalasIdopontja: null,
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
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        HatarozatHozoSzemelyCimkeId: {
          required,
        },
        TargyalasIdopontja: {
          required,
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        this.fegyelmiUgyId = data.fegyelmiUgyId;
        this.LoadFegyelmiUgyData(data.fegyelmiUgyId);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyId) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetMasodFokuFegyelmiTargyalasElokeszitese(
          {
            id: fegyelmiUgyId,
          }
        );
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        this.isFormLoading = false;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        console.log(err);
        this.Hide();
      }

      console.log('Result id: ' + result.FegyelmiUgyId);
      this.formData.targyalasMaxIdopontja = result.TargyalasMaxIdopontja;
      this.formData.hatarozatHozoSzemelyOptions = result.HatarozatHozoSzemelyOptions.sort(
        function(a, b) {
          return ('' + a.text).localeCompare(b.text);
        }
      );

      hidrateForm(this, this.formData.values, result);

      this.formData.values.TargyalasIdopontja = moment(
        this.formData.values.TargyalasIdopontja
      ).format('YYYY.MM.DD HH:mm');

      this.$v.$reset();
    },
    TargyalasiIdopontKituzese: async function() {
      //todo
      try {
        if (this.$v.$invalid) {
          this.$v.$touch();
          return;
        }
        this.isLoading = true;
        var result = await apiService.SaveMasodFokuTargyalasKituzese({
          data: this.formData.values,
        });

        NotificationFunctions.SuccessAlert(
          'II. fokú tárgyalás kitűzve',
          result.message
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        this.Hide();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Hiba az II. fokú tárgyalás kitűzésekor',
          errorObj: err,
        });
        console.log(err);
      }
      this.isLoading = false;
    },
    GetUgyszam() {
      return getUgyszam(this.fegyelmiUgy);
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),
    hatarozatHozoSzemelyOptions: function() {
      return {
        data: this.formData.hatarozatHozoSzemelyOptions,
        dropdownParent: $(this.$el),
        placeholder: 'Határozatot hozó személy',
      };
    },

    datePickerOptions() {
      return {
        format: 'YYYY.MM.DD HH:mm',
        //useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        minDate: moment(),
        maxDate: moment(
          this.formData.targyalasMaxIdopontja || new Date().toISOString()
        ),
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
    'formData.values.TargyalasIdopontja': function(newDatum, oldDatum) {
      if ($('[data-action="togglePicker"] > .fa-clock-o').length > 0) {
        $('[data-action="togglePicker"]')[0].click();
      }
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
