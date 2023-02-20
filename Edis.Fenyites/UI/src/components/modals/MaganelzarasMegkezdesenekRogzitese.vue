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
          19. Magánelzárás megkezdésének rögzítése
          <p class="mt-10 mb-0 font-size-12">
            Adjuk meg az elhelyezést, és a tényleges időpontot.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.ZarkabaHelyezes"
              >
                <treeselect
                  v-model="$v.formData.values.ZarkabaHelyezes.$model"
                  :multiple="false"
                  :disable-branch-nodes="true"
                  :clearable="false"
                  placeholder="Válasszon..."
                  v-bind="treeselectHun"
                  :options="formData.zarkabaHelyezesOptions"
                  @open="OpenTreeselect"
                  @close="DestroyTreeselect"
                />
                <span class="text-help float-right">Zárkába helyezés</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.BehelyezesTenylegesIdeje"
              >
                <date-picker
                  v-model="$v.formData.values.BehelyezesTenylegesIdeje.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right"
                  >Behelyezés tényleges ideje</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <span class="float-left"
                >Szabadulás napja: {{ szabadulasNapja }}</span
              >
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
        id="maganelzarasBtn"
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="Maganelzaras()"
        :disabled="szakteruletiVelemenyLoading || buttonsDisabled"
      >
        <b-spinner small v-if="szakteruletiVelemenyLoading"></b-spinner>
        Magánelzárás végrehajtása
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import fegyelmiugy, {
  FegyelmiUgyStoreTypes,
} from '../../store/modules/fegyelmiugy';
import { required, minValue, maxLength } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import $ from 'jquery';
import { sortStr } from '../../utils/sort';
import { UserStoreTypes } from '../../store/modules/user';
import moment from 'moment';
import minLength from 'vuelidate/lib/validators/minLength';
import { treeselectHun } from '../../plugins/vueTreeSelect';
import { date } from 'jszip/lib/defaults';
import { dateSameDay } from '../../utils/date';

export default {
  name: 'maganelzaras-megkezdesenek-rogzitese',
  data() {
    return {
      isFormLoading: false,
      title: 'Magánelzárás megkezdése',
      fegyelmiUgyIds: [],
      treeselectHun,
      naplobejegyzesIds: [],
      szakteruletiVelemenyLoading: false,
      nyomtatasLoading: false,
      isMounted: false,
      szabadulasNapja: null,
      maganelzarasHossza: null,
      letoltottPercek: null,
      fenyitesHossza: null,
      hatarozatDatuma: null,
      fanyConfirm: {
        isShow: false,
        confirmText: '',
      },
      formData: {
        zarkabaHelyezesOptions: [],
        values: {
          ZarkabaHelyezes: null,
          BehelyezesTenylegesIdeje: null,
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
  mounted: function () {
    this.isMounted = true;
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        ZarkabaHelyezes: {
          required,
        },
        BehelyezesTenylegesIdeje: {
          required,
        },
      },
    },
  },
  methods: {
    SzabadulasSzamolas: function (date) {
      this.szabadulasNapja = moment(
        this.addMinutes(date, this.maganelzarasHossza)
      ).format('YYYY.MM.DD HH:mm');
    },
    addMinutes: function (date, minutes) {
      var result = moment(date, 'YYYY.MM.DD HH:mm');
      result.add({ m: minutes });
      return result;
    },
    InitEvents: function ({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        this.naplobejegyzesIds = data.naplobejegyzesIds || [];
        var fegyUgy = this.$store.getters[
          FegyelmiUgyStoreTypes.getters.getFegyelmiUgyById
        ](data.fegyelmiUgyIds);

        this.LoadFormData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
        if (fegyUgy) {
          this.fenyitesHossza = fegyUgy.FenyitesHossza;
        }
      } else {
        this.Hide();
      }
    },

    OpenTreeselect: function () {
      // this.$nextTick(() => {
      //   $('.vue-treeselect__menu').slimScroll({
      //     minHeight: '100px',
      //     Height: '100px',
      //     maxHeight: '100px',
      //     wheelStep: 5,
      //     color: '#8349f5',
      //     position: 'right',
      //     distance: '5px',
      //   });
      // });
    },

    DestroyTreeselect: function () {
      //$('.vue-treeselect__menu').slimScroll({ destroy: true });
    },
    LoadFormData: async function (fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result =
          await apiService.GetMaganelzarasMegkezdesenekRogziteseModalData({
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          });
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        console.log(result);
        this.letoltottPercek =
          await apiService.GetLetoltottMaganelzarasPercekByFegyelmiUgyId({
            fegyelmiUgyId: fegyelmiUgyIds,
          });
        hidrateForm(this, this.formData.values, result);
        this.formData.values.BehelyezesTenylegesIdeje = moment(
          this.formData.values.BehelyezesTenylegesIdeje
        ).format('YYYY.MM.DD HH:mm');
        this.formData.zarkabaHelyezesOptions = result.ZarkabaHelyezesOptions;
        this.maganelzarasHossza =
          this.fenyitesHossza * 24 * 60 - this.letoltottPercek;
        this.SzabadulasSzamolas(this.formData.values.BehelyezesTenylegesIdeje);
        this.isFormLoading = false;
      } catch (err) {
        console.log(err);
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        this.Hide();
      }

      this.$v.$reset();
    },
    SaveFormData: async function (nocheckVegrehajtasFok = false) {
      try {
        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
          NaplobejegyzesIds: this.naplobejegyzesIds,
          MaganelzarasVegeDatum: this.szabadulasNapja,
          NocheckVegrehajtasiFok: nocheckVegrehajtasFok,
        };

        var result =
          await apiService.SaveMaganelzarasMegkezdesenekRogziteseModalData({
            data,
          });

        if (
          result &&
          result.Status == 3 &&
          result.Details &&
          result.Details.Kerdes
        ) {
          var behelyez = await NotificationFunctions.ConfirmModal(
            result.Message
          );
          if (behelyez) await this.SaveFormData(true);
          return;
        }

        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        eventBus.$emit('Sidebar:ugyReszletek:refresh');

        this.$v.$reset();
        return true;
      } catch (err) {
        console.log('hiba: ' + err.responseText);
        var errorMsg = 'Hiba a mentés során';
        if (err.responseText && err.responseText.title) {
          errorMsg = err.responseText.title;
        }
        NotificationFunctions.AjaxError({
          title: this.title,
          text: errorMsg,
          errorObj: err,
        });
      }
    },
    async Maganelzaras() {
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
    ...mapGetters({
      vanFofelugyeloJoga: UserStoreTypes.getters.vanFofelugyeloJoga,
    }),
    datePickerOptions() {
      let minDate = moment().add(-21, 'days');
      if (!minDate) {
        minDate = moment().toISOString();
      }
      let maxDate = moment().endOf('day');
      return {
        format: 'YYYY.MM.DD HH:mm',
        //useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        //minDate: minDate,
        maxDate: maxDate,
        defaultDate: moment(),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
  },
  watch: {
    id: {
      handler: function (newValue, oldValue) {},
      deep: true,
      immediate: true,
    },
    'formData.values.BehelyezesTenylegesIdeje': function (newDatum, oldDatum) {
      if (newDatum) {
        this.SzabadulasSzamolas(newDatum);
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
