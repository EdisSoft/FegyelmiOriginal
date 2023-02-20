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
          3. Döntés fegyelmi ügy elrendeléséről vagy reintegrációs tiszti
          jogkörbe utalás
        </h4>
      </div>
      <div class="modal-body pl-25 pt-25 pb-25">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <div class="row pr-10">
              <div class="col-md-12 px-5">
                <k-vuelidate-error-extractor
                  class="form-group form-material static-select2"
                  :validator="$v.formData.values.DontestHozoSzemelySid"
                >
                  <k-select2
                    :options="DontestHozoSzemelyOptions"
                    v-model="$v.formData.values.DontestHozoSzemelySid.$model"
                    class="donteshozoSelect"
                    placeholder="Döntést hozó személy"
                  >
                  </k-select2>
                  <span class="text-help float-right"
                    >Fegyelmi jogkör gyakorló</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12 px-5">
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
              <div class="col-md-12 px-5">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.KivizsgalasiHatarido"
                >
                  <date-picker
                    v-model="$v.formData.values.KivizsgalasiHatarido.$model"
                    :config="datePickerOptions"
                  >
                  </date-picker>
                  <span class="text-help float-right"
                    >Kivizsgálási határidő</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12 px-5">
                <div class="checkbox-custom">
                  <input
                    type="checkbox"
                    id="inputUnchecked"
                    v-model="formData.values.JogiKepviseletetKer"
                  />
                  <label
                    for="inputUnchecked"
                    class="font-weight-400 d-inline-flex align-items-end"
                  >
                    <span class="text-help my-0 ml-10"
                      >Jogi képviseletet kér</span
                    >
                  </label>
                </div>
              </div>
              <ul
                class="timeline timeline-icon my-20 px-5"
                v-if="fogvatartottNyitottFegyelmiUgyek != ''"
              >
                <li class="timeline-period mt-0 mb-30 py-15">
                  Az eljárás alá vont nyitott fegyelmi ügyei
                </li>
                <div
                  v-for="(fogvatartottNyitottFegyelmiUgy,
                  i) in fogvatartottNyitottFegyelmiUgyek"
                  v-bind:key="fogvatartottNyitottFegyelmiUgy.Id"
                  :class="[
                    i % 2 == 0
                      ? 'timeline-item'
                      : 'timeline-item timeline-reverse',
                  ]"
                >
                  <li>
                    <div class="timeline-dot animation-scale-up">
                      <i class="fas fa-info"></i>
                    </div>
                    <div class="timeline-info">
                      <time
                        :datetime="
                          fogvatartottNyitottFegyelmiUgy.EsemenyDatuma
                            | toShortDatePontNelkul
                        "
                        >{{
                          fogvatartottNyitottFegyelmiUgy.EsemenyDatuma
                            | toShortDatePontNelkul
                        }}</time
                      >
                    </div>
                    <div class="timeline-content">
                      <div class="card">
                        <div>
                          <p>
                            <strong>Hely:</strong>
                            {{ fogvatartottNyitottFegyelmiUgy.IntezetNev }}
                          </p>
                        </div>
                        <div>
                          <p>
                            <strong>Oka:</strong>
                            {{ fogvatartottNyitottFegyelmiUgy.VetsegTipusa }}
                          </p>
                        </div>
                        <div>
                          <p>
                            <strong>Ügyszám:</strong>
                            {{ fogvatartottNyitottFegyelmiUgy.UgySzama }}
                          </p>
                        </div>
                      </div>
                    </div>
                  </li>
                </div>
              </ul>
              <ul
                class="timeline timeline-icon my-20 px-5"
                v-if="fogvatartottZartFegyelmiUgyek != ''"
              >
                <li class="timeline-period mt-0 mb-30 py-15">
                  Az eljárás alá vont "Állami tulajdon rongálása, lopása" vétség
                  alá eső, lezárt fegyelmi ügyei az elmúlt 1 évből:
                </li>
                <div
                  v-for="(fogvatartottZartFegyelmiUgy,
                  i) in fogvatartottZartFegyelmiUgyek"
                  v-bind:key="fogvatartottZartFegyelmiUgy.Id"
                  :class="[
                    i % 2 == 0
                      ? 'timeline-item'
                      : 'timeline-item timeline-reverse',
                  ]"
                >
                  <li>
                    <div class="timeline-dot animation-scale-up">
                      <i class="fas fa-info"></i>
                    </div>
                    <div class="timeline-info">
                      <time
                        :datetime="
                          fogvatartottZartFegyelmiUgy.EsemenyDatuma
                            | toShortDatePontNelkul
                        "
                        >{{
                          fogvatartottZartFegyelmiUgy.EsemenyDatuma
                            | toShortDatePontNelkul
                        }}</time
                      >
                    </div>
                    <div class="timeline-content">
                      <div class="card">
                        <div>
                          <p>
                            <strong>Hely:</strong>
                            {{ fogvatartottZartFegyelmiUgy.IntezetNev }}
                          </p>
                        </div>
                        <div>
                          <p>
                            <strong>Oka:</strong>
                            {{ fogvatartottZartFegyelmiUgy.VetsegTipusa }}
                          </p>
                        </div>
                        <div>
                          <p>
                            <strong>Ügyszám:</strong>
                            {{ fogvatartottZartFegyelmiUgy.UgySzama }}
                          </p>
                        </div>
                      </div>
                    </div>
                  </li>
                </div>
              </ul>
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
          v-on:click="ReintegraciosJogkorbe"
          :disabled="
            reintegraciosJogkorbeIsLoading ||
              buttonsDisabled ||
              formData.values.JogiKepviseletetKer ||
              elrendelesIsLoading
          "
        >
          <b-spinner small v-if="reintegraciosJogkorbeIsLoading"></b-spinner>
          Reintegrációs jogkörbe
        </button>
        <button
          type="button"
          class="btn savebtn white mb-lg-5"
          v-on:click="ElrendelemKivizsgalast"
          :disabled="
            elrendelesIsLoading ||
              reintegraciosJogkorbeIsLoading ||
              buttonsDisabled
          "
        >
          <b-spinner small v-if="elrendelesIsLoading"></b-spinner>
          Elrendelem a kivizsgálást
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
import fegyelmiugy, {
  FegyelmiUgyStoreTypes,
} from '../../store/modules/fegyelmiugy';
import { getUgyszam } from '../../utils/fenyitesUtils';
import { required, minValue } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import moment from 'moment';
import $ from 'jquery';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';

export default {
  name: 'fegyelmi-ugy-elrendelese',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      fogvatartottNyitottFegyelmiUgyek: null,
      fogvatartottZartFegyelmiUgyek: null,
      elrendelesIsLoading: false,
      title: 'Döntés az ügy elrendeléséről',
      reintegraciosJogkorbeIsLoading: false,
      vetsegTipusa: null,
      fegyelmiUgy: null,
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
        DontestHozoSzemelySid: {
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
        var fegyUgy = this.$store.getters[
          FegyelmiUgyStoreTypes.getters.getFegyelmiUgyById
        ](data.fegyelmiUgyIds);
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
        var result = await apiService.GetFegyelmiUgyDataElrendeleshez({
          ids: fegyelmiUgyIds,
        });
        console.log('Result ids: ' + result.FegyelmiUgyIds);

        if (fegyelmiUgyIds) {
          var res = await apiService.GetFogvatartottOsszesFegyelmiUgy({
            fegyelmiUgyId: fegyelmiUgyIds,
          });
          this.fogvatartottNyitottFegyelmiUgyek = res.nyitottak;
          this.fogvatartottZartFegyelmiUgyek = res.zartak;
          this.vetsegTipusa = res.zartak.map(x => x.VetsegTipusa)[0];
        }
        this.formData.kivizsgaloSzemelyOptions = result.KivizsgaloSzemelyOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        this.formData.dontestHozoSzemelyOptions = result.DontestHozoSzemelyOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        hidrateForm(this, this.formData.values, result);

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
    ElrendelemKivizsgalast: async function() {
      try {
        console.log('fegylmiugy', this.fegyelmiUgy);

        if (this.$v.$invalid) {
          this.$v.$touch();
          return;
        }
        this.elrendelesIsLoading = true;
        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
        };
        var result = await apiService.FegyelmiUgyElrendelemKivizsgalast({
          data,
        });
        if (result.isSuccess == true) {
          NotificationFunctions.SuccessAlert(
            'Döntés fegyelmi ügy elrendeléséről',
            result.message
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
        } else {
          NotificationFunctions.WarningAlert(
            'Döntés fegyelmi ügy elrendeléséről',
            result.message
          );
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: err.responseText.title,
          errorObj: err,
        });
        //this.Hide();
        console.log(err);
      }
      this.elrendelesIsLoading = false;
    },
    ReintegraciosJogkorbe: async function() {
      try {
        if (this.$v.$invalid) {
          this.$v.$touch();
          return;
        }
        this.reintegraciosJogkorbeIsLoading = true;
        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
        };
        var result = await apiService.FegyelmiUgyReintegraciosJogkorbe({
          data,
        });
        if (result.isSuccess == true) {
          NotificationFunctions.SuccessAlert(
            'Döntés fegyelmi ügy elrendeléséről',
            result.message
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
        } else {
          NotificationFunctions.WarningAlert(
            'Döntés fegyelmi ügy elrendeléséről',
            result.message
          );
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Hiba történt a fegyelmi ügy elrendelése során',
          errorObj: err,
        });
        console.log(err);
      }
      this.reintegraciosJogkorbeIsLoading = false;
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
    DontestHozoSzemelyOptions: function() {
      return {
        data: this.formData.dontestHozoSzemelyOptions,
        dropdownParent: $(this.$el),
        placeholder: 'Döntést hozó személy',
        readOnly: true,
        disabled: true,
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
.timeline {
  width: 100%;
}
.timeline-period {
  font-size: 16px;
  text-transform: none;
}
.timeline-info,
.timeline-reverse .timeline-info {
  float: none;
  margin-bottom: 15px;
}
.timeline-dot {
  background-color: #8349f5;
}
.timeline-content p {
  font-size: 13px;
  margin-bottom: 5px;
}
.vuebar-element.modal-scroll {
  height: calc(100vh - 500px);
  /*width: 100%;*/
  max-width: 100%;
  padding-right: 20px;
}
.vb-content {
  padding-left: 20px;
}
</style>
