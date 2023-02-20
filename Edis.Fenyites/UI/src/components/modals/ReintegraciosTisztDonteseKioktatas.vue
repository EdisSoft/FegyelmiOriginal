<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-grey modal-container">
      <div class="modal-header">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title font-size-20">
          44. Kioktatás reintegrációs tiszti jogkörben
          <p class="mt-10 mb-0 font-size-12">Kérem, indokolja a döntést!</p>
        </h4>
      </div>
      <div class="modal-body pt-20 pb-40 pl-25">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <div class="row">
              <div class="col-md-12 mb-10">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FenyitesKiszabasIdeje"
                >
                  <date-picker
                    v-model="$v.formData.values.FenyitesKiszabasIdeje.$model"
                    :config="datePickerOptions"
                  >
                  </date-picker>
                  <span class="text-help float-right"
                    >Fenyítés kiszabás ideje</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12 mb-10">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FegyelmiVetsegTipusaCimkeId"
                >
                  <k-select2
                    :options="fegyelmiVetsegTipusaOptions"
                    v-model="
                      $v.formData.values.FegyelmiVetsegTipusaCimkeId.$model
                    "
                    class=""
                    placeholder="Fegyelmi vétség  típusa"
                  >
                  </k-select2>
                  <span class="text-help float-right">Fegyelmi vétség</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material summernote"
                  :validator="$v.formData.values.Indoklas"
                >
                  <textarea-autosize
                    v-model="$v.formData.values.Indoklas.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                    placeholder="Indoklás..."
                  />
                  <!-- <k-summernote
                    v-model="$v.formData.values.Indoklas.$model"
                    :settings="{
                      placeholder: 'Leírás',
                    }"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote> -->
                </k-vuelidate-error-extractor>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer align-right">
        <button type="button" class="btn btn-dark mb-lg-5" @click="Hide">
          Mégsem
        </button>
        <b-button
          type="button"
          class="btn btn-dark mb-lg-5"
          @click="Nyomtatas()"
          :disabled="isNyomtatasLoading || buttonsDisabled"
        >
          <b-spinner small v-if="isNyomtatasLoading"></b-spinner>
          Nyomtatás
        </b-button>
        <b-button
          type="button"
          class="btn btn-warning mb-lg-5"
          @click="Kioktattam()"
          :disabled="isKioktattamLoading || buttonsDisabled"
        >
          <b-spinner small v-if="isKioktattamLoading"></b-spinner>
          Kioktattam
        </b-button>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService, eventBus } from '../../main';
import { AppStoreTypes } from '../../store/modules/app';
import settings from '../../data/settings';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { getUgyszam } from '../../utils/fenyitesUtils';
import { required, minValue } from 'vuelidate/lib/validators';
import moment from 'moment';
import $ from 'jquery';
import ReintegraciosTisztDontesTipus from '../../data/enums/reintegraciosTisztDontesTipus';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { hidrateForm } from '../../utils/vueUtils';
import { sortStr } from '../../utils/sort';
import { useSimpleModalHandler } from '../../utils/modal';
import { deselectDatatable } from '../../utils/common';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';

export default {
  name: 'reintegracios-tiszt-dontese-kioktatas',
  data() {
    return {
      fegyelmiUgy: null,
      fegyelmiUgyIds: [],
      dontesTipus: null,
      isNyomtatasLoading: false,
      isKioktattamLoading: false,
      title: 'Kioktatás',
      isFormLoading: false,
      formData: {
        fenyitesKiszabasIdejeMinDate: null,
        reintegraciosReszlegOptions: [],
        fegyelmiVetsegTipusaOptions: [],
        values: {
          NaplobejegyzesIds: [],
          ReintegraciosReszlegId: null,
          FenyitesKiszabasIdeje: null,
          FegyelmiVetsegTipusaCimkeId: null,
          Indoklas: '',
          IsTudomasulVette: true,
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
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        ReintegraciosReszlegId: {},
        FegyelmiVetsegTipusaCimkeId: {
          required,
        },
        FenyitesKiszabasIdeje: {
          required,
        },
        Indoklas: {
          required,
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        console.log({ data });
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.dontesTipus = data.modalType;
        this.formData.values.NaplobejegyzesIds = data.naplobejegyzesIds;
        this.fegyelmiUgy = this.$store.getters[
          FegyelmiUgyStoreTypes.getters.getFegyelmiUgyById
        ](data.fegyelmiUgyIds);
        this.LoadFormData();
      } else {
        this.Hide();
      }
    },
    async LoadFormData() {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetReintegraciosTisztDontesModalData({
          fegyelmiUgyIds: this.fegyelmiUgyIds,
          naploIds: this.formData.values.NaplobejegyzesIds,
        });
        console.log(result);

        hidrateForm(this, this.formData.values, result);

        this.formData.values.FenyitesKiszabasIdeje = moment(
          this.formData.values.FenyitesKiszabasIdeje
        ).format('YYYY.MM.DD HH:mm');

        this.formData.reintegraciosReszlegOptions =
          result.ReintegraciosReszlegOptions;

        this.formData.reintegraciosReszlegOptions.sort(sortStr('text'));

        this.formData.fegyelmiVetsegTipusaOptions =
          result.FegyelmiVetsegTipusaOptions;

        this.formData.fenyitesKiszabasIdejeMinDate =
          result.FenyitesKiszabasIdejeMinDate;

        this.formData.fegyelmiVetsegTipusaOptions.sort(sortStr('text'));

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
    async Mentes(isAlairta = false, isNyomtatas = false) {
      try {
        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
          Alairta: isAlairta,
        };
        console.log(data);
        let result = await apiService.ReintegraciosTisztDontesKioktatasModalMentes(
          {
            data,
          }
        );
        this.formData.values.NaplobejegyzesIds = result.naplobejegyzesIds;
        if (isNyomtatas) {
          NotificationFunctions.SuccessAlert(
            'Kioktatás reintegrációs tiszti jogkörben',
            'Sikeres nyomtatvány rögzítés'
          );
        } else {
          NotificationFunctions.SuccessAlert(
            'Kioktatás reintegrációs tiszti jogkörben',
            'A fogvatartott kioktatása megtörént, az ügy lezárult'
          );
        }

        if (isAlairta) eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        return result;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Kioktatás létrehozása nem sikerült',
          errorObj: err,
        });
        console.log(err);
      }
    },
    async Nyomtatas() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        return;
      }
      this.isNyomtatasLoading = true;

      let result = await this.Mentes(false, true);
      // console.log('id: ' + this.fegyelmiUgyIds[0]);
      // console.log('napló: ' + this.formData.values.NaplobejegyzesIds[0]);

      if (result) {
        NyomtatvanyFunctions.KioktatasReintegraciosJogkorbenNyomtatas({
          fegyelmiUgyId: this.fegyelmiUgyIds[0],
          naploId: this.formData.values.NaplobejegyzesIds[0],
        });
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
      }

      this.isNyomtatasLoading = false;
    },
    async Kioktattam() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        return;
      }
      this.isKioktattamLoading = true;
      let result = await this.Mentes(true, false);
      if (result) {
        this.Hide();
      }
      this.isKioktattamLoading = false;
    },
    GetUgyszam() {
      return getUgyszam(this.fegyelmiUgy);
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),
    reintegraciosReszlegOptions: function() {
      return {
        data: this.formData.reintegraciosReszlegOptions,
        dropdownParent: $(this.$el),
      };
    },
    datePickerOptions() {
      let minDate = this.formData.fenyitesKiszabasIdejeMinDate;
      if (!minDate) {
        minDate = moment()
          .subtract({ days: 2 })
          .toISOString();
      }

      return {
        format: 'YYYY.MM.DD',
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment().endOf('d'),
        minDate: moment(minDate),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
    fegyelmiVetsegTipusaOptions: function() {
      return {
        data: this.formData.fegyelmiVetsegTipusaOptions,
        dropdownParent: $(this.$el),
      };
    },
    isGombHide() {
      if (this.dontesTipus == 2 || this.dontesTipus == 3) {
        return true;
      } else {
        return false;
      }
    },
    dontesTipusStr() {
      switch (parseInt(this.dontesTipus)) {
        case ReintegraciosTisztDontesTipus.Feddes:
          return 'Feddés';

        // Már külön modal
        // case ReintegraciosTisztDontesTipus.Visszakuldes:
        //   return 'Visszaküldes';

        case ReintegraciosTisztDontesTipus.Kioktatas:
          return 'Kioktatás';

        default:
          return '';
      }
    },
  },
  components: {},
  watch: {
    fegyelmiUgyIds: {
      handler: function(newValue, oldValue) {},
      deep: true,
      immediate: true,
    },
  },
  props: {},
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
.modal-header {
  height: 88px;
}
/* .modal-footer {
  height: 75px;
} */
.modal-scroll {
  height: 340px;
}
</style>
