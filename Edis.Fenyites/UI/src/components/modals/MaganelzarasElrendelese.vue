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
          43. Magánelzárás elrendelése
          <p class="mt-10 mb-0 font-size-12">
            Tervezze be a magánelzárás végrehajtását!
          </p>
        </h4>
      </div>
      <div class="modal-body pl-25 pt-25 pb-50">
        <div>
          <div class="row pr-10">
            <div class="col-md-12">
              <div class="unique-diabled-field">
                {{ formData.values.FegyelmiJogkorGyakorloNeve }}
              </div>
              <span class="text-help float-right"
                >Fegyelmi jogkör gyakorló</span
              >
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.TervezettDatum"
              >
                <date-picker
                  v-model="$v.formData.values.TervezettDatum.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right"
                  >Magánelzárás megkezdésének tervezett dátuma</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.Fofelugyelok"
              >
                <k-select2
                  :options="felugyelokSelectOptions"
                  v-model="$v.formData.values.Fofelugyelok.$model"
                  class=""
                >
                </k-select2>
                <span class="text-help float-right">
                  Értesítendő főfelügyelők
                </span>
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
          Magánelzárás elrendelése
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
import { sortStr } from '../../utils/sort';
import $ from 'jquery';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';

export default {
  name: 'maganelzaras-elrendelese',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      fegyelmiUgy: null,
      isMounted: false,
      rogzitesLoading: false,
      title: 'Magánelzárás elrendelése',
      formData: {
        felugyelokDefault: [],
        felugyelokOptions: [],
        values: {
          FegyelmiJogkorGyakorloId: 0,
          FegyelmiJogkorGyakorloNeve: '',
          TervezettDatum: null,
          HatarozatDatum: null,
          Fofelugyelok: [],
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
        FegyelmiJogkorGyakorloId: {
          required,
        },
        TervezettDatum: {
          required,
        },
        Fofelugyelok: {},
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
        var result = await apiService.GetMaganelzarasElrendeleseModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
        });
        hidrateForm(this, this.formData.values, result);
        this.formData.values.Fofelugyelok = [];
        this.formData.felugyelokDefault = result.FofelugyelokDefault;
        this.formData.values.Fofelugyelok = result.FofelugyelokDefault.map(
          x => x.id
        );
        this.formData.felugyelokOptions = result.FofelugyelokOptions;
        this.formData.felugyelokOptions.sort(sortStr('text'));

        this.formData.values.TervezettDatum = moment(
          this.formData.values.TervezettDatum
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
        var result = await apiService.SaveMaganelzarasElrendeleseModalData({
          data,
        });

        eventBus.$emit('Sidebar:ugyReszletek:refresh');

        this.Hide();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text:
            'Hiba történt felfüggesztés megszüntetésekor' +
            err.responseText.title,
          errorObj: err,
        });
        console.log(err);
      }
      this.rogzitesLoading = false;
    },
    GetKivizsgalasiHatarido() {
      return this.TervezettDatum;
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),

    felugyelokSelectOptions() {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.felugyelokOptions,
        dropdownParent: $(this.$el),
        multiple: true,
        readOnly: !!this.id && this.formData.felugyelokDefault.length > 0,
        disabled: !!this.id && this.formData.felugyelokDefault.length > 0,
      };
    },

    datePickerOptions() {
      var maxDate = moment()
        .add({ months: 6 })
        .endOf('d');
      if (this.formData.values.HatarozatDatum)
        maxDate = moment(this.formData.values.HatarozatDatum)
          .add({ months: 6 })
          .endOf('d');
      return {
        format: 'YYYY.MM.DD',
        //useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: maxDate,
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
