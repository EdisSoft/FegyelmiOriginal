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
          37. Büntető feljelentés rögzítése és módosítása
          <p class="mt-10 mb-0 font-size-12">
            Adja meg a feljelentés dátumát minősítését és az elintézés módját.
            Később az űrlapot módosíthatja.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.FenyitesKiszabasIdeje"
              >
                <date-picker
                  v-model="$v.formData.values.FenyitesKiszabasIdeje.$model"
                  :readonly="naplobejegyzesIds.length > 0"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right">Feljelentés dátuma</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.FeljelentesMinositeseId"
              >
                <k-select2
                  :options="select2FeljelentesMinositeseOptions"
                  v-model="$v.formData.values.FeljelentesMinositeseId.$model"
                  class=""
                  placeholder="Válasszon..."
                >
                </k-select2>
                <span class="text-help float-right"
                  >Feljelentés minősítése</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.ElintezesModjaId"
              >
                <k-select2
                  :options="select2ElintezesModjaOptions"
                  v-model="$v.formData.values.ElintezesModjaId.$model"
                  class=""
                  placeholder="Válasszon..."
                >
                </k-select2>
                <span class="text-help float-right"
                  >Feljelentés elintézés módja</span
                >
              </k-vuelidate-error-extractor>
            </div>
          </div>


        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <div class="row">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group summernote"
                  :validator="$v.formData.values.Leiras"
                >
                  <!-- <textarea-autosize
                    v-model="$v.formData.values.Leiras.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                  /> -->
                  <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote>
                  <span class="text-help float-right"
                    >Bűncselekmény tényállásának leírása és bizonyítási eszközei</span
                  >
                </k-vuelidate-error-extractor>
              </div>
            </div>
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
          class="btn btn-dark"
          @click="Nyomtatas()"
          :disabled="nyomtatasLoading || buttonsDisabled"
        >
          <b-spinner small v-if="nyomtatasLoading"></b-spinner>
          Nyomtatás
        </button>
      <button
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="Rogzites()"
        :disabled="rogzitesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="rogzitesLoading"></b-spinner>
        Feljelentés rögzítése
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
import { sortStr } from '../../utils/sort';
import moment from 'moment';
import minLength from 'vuelidate/lib/validators/minLength';

export default {
  name: 'bunteto-feljelentes-rogzitese',
  data() {
    return {
      title: 'Büntető feljelentés rögzítése',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      isFormLoading: false,
      rogzitesLoading: false,
      nyomtatasLoading: false,
      isMounted: false,
      formData: {
        elintezesModjaOptions: [],
        feljelentesMinositeseOptions: [],
        values: {
          FeljelentesMinositeseId: null,
          ElintezesModjaId: null,
          FenyitesKiszabasIdeje: null,
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
        FeljelentesMinositeseId: {
          required,
        },
        ElintezesModjaId: {
          required,
        },
        FenyitesKiszabasIdeje: {
          required,
        },
        Leiras: {
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
    LoadFormData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetBuntetoFeljelentesRogziteseModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });

        console.log(result);
        hidrateForm(this, this.formData.values, result);

        this.formData.feljelentesMinositeseOptions = result.FeljelentesMinositeseOptions.sort(
          sortStr('text')
        );
        this.formData.elintezesModjaOptions = result.ElintezesModjaOptions.sort(
          sortStr('text')
        );
        this.formData.values.FenyitesKiszabasIdeje = moment(
          this.formData.values.FenyitesKiszabasIdeje
        ).format('YYYY.MM.DD');
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
        var result = await apiService.SaveBuntetoFeljelentesRogziteseModalData({
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
    async Rogzites() {
      if (!this.IsFormValid()) {
        return;
      }
      this.rogzitesLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.rogzitesLoading = false;
    },
    async Nyomtatas() {
      if (!this.IsFormValid()) {
        return;
      }
      this.nyomtatasLoading = true;
      var success = await this.SaveFormData();

      if (success) {
        try {
          if (this.naplobejegyzesIds) {
            await NyomtatvanyFunctions.BuntetoFeljelentesDocxNyomtatas({
              naplobejegyzesId: this.naplobejegyzesIds[0],
            });
            eventBus.$emit('Sidebar:ugyReszletek:refresh');
          }
        } catch (error) {
          console.log(error);
        }
      }
      this.nyomtatasLoading = false;
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
    select2FeljelentesMinositeseOptions: function() {
      return {
        data: this.formData.feljelentesMinositeseOptions,
        dropdownParent: $(this.$el),
        readOnly: this.naplobejegyzesIds.length > 0,
        disabled: this.naplobejegyzesIds.length > 0,
      };
    },
    select2ElintezesModjaOptions: function() {
      return {
        data: this.formData.elintezesModjaOptions,
        dropdownParent: $(this.$el),
      };
    },
    datePickerOptions() {
      return {
        format: 'YYYY.MM.DD',
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment().endOf('d'),
        minDate: moment()
          .add({ w: -1 })
          .startOf('d'),
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

.modal-scroll {
  height: auto;
  max-height: 315px;
}
</style>
