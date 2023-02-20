<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-primary" id="maganelzarasVegrehajtasaModal">
      <div class="modal-header bg-blue-grey-400">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title">
          20. Magánelzárás végrehajtva
          <p class="mt-10 mb-0 font-size-12">
            Adjuk meg a zárkát ahova a fogvatartottat visszahelyezzük, és annak
            időpontját.
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
                <span class="text-help float-right"
                  >Visszahelyezés zárkába</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.KihelyezesTenylegesIdeje"
              >
                <date-picker
                  v-model="$v.formData.values.KihelyezesTenylegesIdeje.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right"
                  >Kihelyezés tényleges időpontja</span
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
        id="vegrehajtasBtn"
        class="btn savebtn white mb-lg-5"
        @click="MaganelzarasVegrehajtva()"
        :disabled="maganelzarasVegrehajtvaLoading || buttonsDisabled"
      >
        <b-spinner small v-if="maganelzarasVegrehajtvaLoading"></b-spinner>
        Magánelzárás végrehajtva

        <b-popover
          target="vegrehajtasBtn"
          triggers="null"
          :show.sync="zarkabaHelyezesConfirm.isShow"
          placement="topleft"
          container="maganelzarasVegrehajtasaModal"
          ref="confirmPopover"
          custom-class="ujResztvevoPopover"
        >
          <template slot="title">
            <b-button
              @click="CloseZarkabaHelyezesConfirm()"
              class="close"
              aria-label="Close"
            >
              <span class="d-inline-block white" aria-hidden="true"
                >&times;</span
              >
            </b-button>
            Megerősítés
          </template>
          <div class="pb-5">
            <div
              class="form-group form-material mb-10"
              data-plugin="formMaterial"
            >
              {{ zarkabaHelyezesConfirm.confirmText }}
            </div>
            <div class="text-right">
              <b-button
                size="sm"
                @click="CloseZarkabaHelyezesConfirm()"
                variant="default"
                class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700 mr-5"
                >Nem</b-button
              >
              <b-button
                size="sm"
                @click="MentesFolytatas()"
                variant="warning"
                class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
                >Igen</b-button
              >
            </div>
          </div>
        </b-popover>
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
import { UserStoreTypes } from '../../store/modules/user';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';
// import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import $ from 'jquery';
import { sortStr } from '../../utils/sort';
import moment from 'moment';
import minLength from 'vuelidate/lib/validators/minLength';
import { treeselectHun } from '../../plugins/vueTreeSelect';

export default {
  name: 'maganelzaras-vegrehajtva',
  data() {
    return {
      isFormLoading: false,
      title: 'Magánelzárás végrehajtva',
      fegyelmiUgyIds: [],
      treeselectHun,
      naplobejegyzesIds: [],
      maganelzarasVegrehajtvaLoading: false,
      isMounted: false,
      zarkabaHelyezesConfirm: {
        isShow: false,
        confirmText: '',
      },
      fanyConfirm: {
        isShow: false,
        confirmText: '',
      },
      formData: {
        zarkabaHelyezesOptions: [],
        values: {
          ZarkabaHelyezes: null,
          KihelyezesTenylegesIdeje: null,
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
        ZarkabaHelyezes: {},
        KihelyezesTenylegesIdeje: {
          required,
        },
      },
    },
  },
  methods: {
    InitEvents: function ({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        this.naplobejegyzesIds = data.naplobejegyzesIds || [];
        this.LoadFormData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },

    OpenTreeselect: function () {
      // this.$nextTick(() => {
      //   $('.vue-treeselect__menu').slimScroll({
      //     minHeight: '65px',
      //     Height: '65px',
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
        var result = await apiService.GetMaganelzarasVegrehajtvaModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });

        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        console.log(result);

        hidrateForm(this, this.formData.values, result);

        this.formData.values.KihelyezesTenylegesIdeje = moment(
          this.formData.values.KihelyezesTenylegesIdeje
        ).format('YYYY.MM.DD HH:mm');

        this.formData.zarkabaHelyezesOptions = result.ZarkabaHelyezesOptions;
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
    SaveFormData: async function () {
      try {
        var checkZarka = await apiService.CheckMaganelzarasVegrehajtvaModalData(
          {
            data: {
              ...this.formData.values,
              FegyelmiUgyIds: this.fegyelmiUgyIds,
              NaplobejegyzesIds: this.naplobejegyzesIds,
            },
          }
        );

        if (checkZarka.noFerfibe) {
          this.zarkabaHelyezesConfirm.confirmText =
            'Női fogvatartottat akar férfi zárkába helyezni. Folytatja?';
          this.ZarkabaHelyezesConfirm();
        } else if (checkZarka.ferfiNoibe) {
          this.zarkabaHelyezesConfirm.confirmText =
            'Férfi fogvatartottat akar női zárkába helyezni. Folytatja?';
          this.ZarkabaHelyezesConfirm();
        } else if (checkZarka.dohanyzoZarkaba) {
          this.zarkabaHelyezesConfirm.confirmText =
            'Nem dohányzó fogvatartottat akar dohányzó zárkába helyezni. Folytatja?';
          this.ZarkabaHelyezesConfirm();
        } else if (checkZarka.nemDohanyzoZarkaba) {
          this.zarkabaHelyezesConfirm.confirmText =
            'Dohányzó fogvatartottat akar nem dohányzó zárkába helyezni. Folytatja?';
          this.ZarkabaHelyezesConfirm();
        } else {
          return await this.MentesFolytatas();
        }

        //   var result = await apiService.SaveMaganelzarasVegrehajtvaModalData({
        //     data: {
        //       ...this.formData.values,
        //       FegyelmiUgyIds: this.fegyelmiUgyIds,
        //       NaplobejegyzesIds: this.naplobejegyzesIds,
        //     },
        //   });
        //   this.naplobejegyzesIds = result.naplobejegyzesIds;
        //   NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        //   eventBus.$emit('Sidebar:ugyReszletek:refresh');

        //   this.$v.$reset();
        //   return true;
      } catch (err) {
        var errorMsg = 'Hiba a mentés során: ' + err;
        NotificationFunctions.AjaxError({
          title: this.title,
          text: err.responseText.title,
          errorObj: err,
        });
        console.log(errorMsg);
      }
    },

    async MentesFolytatas(nocheckVegrehajtasFok = false) {
      try {
        var result = await apiService.SaveMaganelzarasVegrehajtvaModalData({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
            NocheckVegrehajtasiFok: nocheckVegrehajtasFok,
          },
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
          if (behelyez) {
            return await this.MentesFolytatas(true);
          }
        }

        this.naplobejegyzesIds = result.naplobejegyzesIds;

        NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        eventBus.$emit('Sidebar:ugyReszletek:refresh');

        this.$v.$reset();
        return true;
      } catch (err) {
        var errorMsg = 'Hiba a mentés során: ' + err;
        NotificationFunctions.AjaxError({
          title: this.title,
          text: err.responseText.title,
          errorObj: err,
        });
        console.log(errorMsg);
      }
    },

    ZarkabaHelyezesConfirm: function () {
      this.$root.$emit('bv::hide::popover');
      this.zarkabaHelyezesConfirm.isShow = true;

      // this.zarkabaHelyezesConfirm.confirmText =
      //   'Kinyomtatja az új fegyelmi ügyekhez tartozó fegyelmi lapokat?';
    },
    CloseZarkabaHelyezesConfirm: function () {
      this.zarkabaHelyezesConfirm.isShow = false;
      //this.MentesFolytatas();
      //this.Hide();
    },

    async MaganelzarasVegrehajtva() {
      if (!this.IsFormValid()) {
        return;
      }
      this.maganelzarasVegrehajtvaLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.maganelzarasVegrehajtvaLoading = false;
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
      let minDate = moment().add(-8, 'hours');
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
