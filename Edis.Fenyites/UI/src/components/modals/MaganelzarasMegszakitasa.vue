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
          29. Magánelzárás megszakítása
          <p class="mt-10 mb-0 font-size-12">
            A zárka megadása opcionális, az indoklás kötelező.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.VisszahelyezesZarkaba"
              >
                <treeselect
                  v-model="$v.formData.values.VisszahelyezesZarkaba.$model"
                  :multiple="false"
                  :disable-branch-nodes="true"
                  placeholder="Válasszon..."
                  v-bind="treeselectHun"
                  :options="formData.VisszahelyezesZarkabaOptions"
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
                :validator="
                  $v.formData.values.MaganzarkabolKihelyezesTenylegesIdeje
                "
              >
                <date-picker
                  v-model="
                    $v.formData.values.MaganzarkabolKihelyezesTenylegesIdeje
                      .$model
                  "
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right"
                  >Megszakítás tényleges időpontja</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group summernote"
                :validator="$v.formData.values.Indoklas"
              >
                <textarea-autosize
                  v-model="$v.formData.values.Indoklas.$model"
                  :min-height="30"
                  class="w-p100 mb-0"
                  name="indoklas"
                  :rows="1"
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Indoklas.$model"
                  name="indoklas"
                  class="mb-0"
                ></k-summernote> -->
                <span class="text-help float-right">Indoklás</span>
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
        id="megszakitasBtn"
        class="btn savebtn white mb-lg-5"
        @click="MaganelzarasMegszakitasa()"
        :disabled="megszakitasLoading || buttonsDisabled"
      >
        <b-spinner small v-if="megszakitasLoading"></b-spinner>
        Magánelzárás megszakítása
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
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import $ from 'jquery';
import { UserStoreTypes } from '../../store/modules/user';
import { sortStr } from '../../utils/sort';
import moment from 'moment';
import minLength from 'vuelidate/lib/validators/minLength';
import { treeselectHun } from '../../plugins/vueTreeSelect';

export default {
  name: 'maganelzaras-megszakitasa',
  data() {
    return {
      isFormLoading: false,
      title: 'Magánelzárás megszakítása',
      fegyelmiUgyIds: [],
      treeselectHun,
      naplobejegyzesIds: [],
      megszakitasLoading: false,
      isMounted: false,
      fofelugyeloiJog: '',
      behelyezesIdeje: null,
      fanyConfirm: {
        isShow: false,
        confirmText: '',
      },
      formData: {
        VisszahelyezesZarkabaOptions: [],
        values: {
          VisszahelyezesZarkaba: null,
          MaganzarkabolKihelyezesTenylegesIdeje: null,
          Indoklas: '',
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
        VisszahelyezesZarkaba: {},
        MaganzarkabolKihelyezesTenylegesIdeje: {
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
        this.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        this.naplobejegyzesIds = data.naplobejegyzesIds || [];
        this.LoadFormData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },

    OpenTreeselect: function() {
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

    DestroyTreeselect: function() {
      //$('.vue-treeselect__menu').slimScroll({ destroy: true });
    },
    LoadFormData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetMaganelzarasMegszakitasaModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });

        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        console.log(result);

        hidrateForm(this, this.formData.values, result);

        this.formData.values.MaganzarkabolKihelyezesTenylegesIdeje = moment(
          this.formData.values.MaganzarkabolKihelyezesTenylegesIdeje
        ).format('YYYY.MM.DD HH:mm');
        this.formData.VisszahelyezesZarkabaOptions =
          result.VisszahelyezesZarkabaOptions;
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
    SaveFormData: async function(nocheckVegrehajtasFok = false) {
      try {
        var result = await apiService.SaveMaganelzarasMegszakitasaModalData({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
            NocheckVegrehajtasiFok: nocheckVegrehajtasFok,
          },
        });

        console.log('Itt haladt el a futás.');

        //debugger;
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

        if (this.formData.values.VisszahelyezesZarkaba != null) {
          NotificationFunctions.SuccessAlert(
            'A fogvatartott behelyezése megtörtént.'
          );
        } else {
          NotificationFunctions.SuccessAlert(
            'A fogvatartottat kihelyeztük a zárkából.'
          );
        }
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
    async MaganelzarasMegszakitasa() {
      if (!this.IsFormValid()) {
        return;
      }
      this.megszakitasLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.megszakitasLoading = false;
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
    SummernoteConfig: function() {
      return {
        toolbar: [
          // [groupName, [list of button]]
          ['style', ['bold', 'italic', 'underline', 'clear']],
          //['font', ['strikethrough', 'superscript', 'subscript']],
          ['forecolor', ['forecolor']],
          ['para', ['ul', 'ol']],
          ['fullscreen', ['fullscreen']],
        ],
        disableResizeEditor: true,
        disableDragAndDrop: true,
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
