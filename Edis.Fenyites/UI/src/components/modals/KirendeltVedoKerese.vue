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
          33. Kirendelt védő kérése
          <p class="mt-10 mb-0 font-size-12">
            Ha az eljárás alá vont fogvatartott jogi képviseletet kér, töltsük
            ki annak formáját.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.TorvenyszekId"
              >
                <k-select2
                  :options="select2TorvenyszekOptions"
                  v-model="$v.formData.values.TorvenyszekId.$model"
                  class=""
                  placeholder="Válasszon törvényszéket..."
                >
                </k-select2>
                <span class="text-help float-right">Törvényszék</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.Leiras"
              >
                <textarea-autosize
                  v-model="$v.formData.values.Leiras.$model"
                  :min-height="30"
                  class="w-p100 mb-0"
                  name="leiras"
                  :rows="1"
                  placeholder="Kérem indokolja..."
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  :settings="{ placeholder: 'Kérem indokolja...' }"
                  name="leiras"
                  class="mb-0"
                ></k-summernote> -->
                <span class="text-help float-right"
                  >Indoklás részletes leírása</span
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
        class="btn bg-blue-grey-600 white mb-lg-5"
        data-dismiss="modal"
        @click="NyilatkozatNyomtatas()"
        :disabled="nyilatkozatNyomtatasLoading || buttonsDisabled"
      >
        <b-spinner small v-if="nyilatkozatNyomtatasLoading"></b-spinner>
        Nyilatkozat nyomtatás
      </button>
      <button
        type="button"
        class="btn bg-blue-grey-600 white mb-lg-5"
        data-dismiss="modal"
        @click="LevelSablonNyomtatas()"
        :disabled="levelSablonNyomtatasLoading || buttonsDisabled"
      >
        <b-spinner small v-if="levelSablonNyomtatasLoading"></b-spinner>
        Levél sablon nyomtatás
      </button>
      <button
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="JogiKepviseletRogzites()"
        :disabled="jogiKepviseletRogzitesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="jogiKepviseletRogzitesLoading"></b-spinner>
        Jogi képviselet rögzítése
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

export default {
  name: 'kirendelt-vedo-kerese',
  data() {
    return {
      isFormLoading: false,
      title: 'Kirendelt védő kérése',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      nyilatkozatNyomtatasLoading: false,
      levelSablonNyomtatasLoading: false,
      jogiKepviseletRogzitesLoading: false,
      formData: {
        torvenyszekOptions: [],
        values: {
          TorvenyszekId: null,
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
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        TorvenyszekId: {
          required,
        },
        Leiras: {
          maxLength: maxLength(1500),
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
        var result = await apiService.GetKirendeltVedoKereseModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });
        console.log(result);
        this.formData.torvenyszekOptions = result.TorvenyszekOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        hidrateForm(this, this.formData.values, result);
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
        this.Hide();
      }

      this.$v.$reset();
    },
    SaveFormData: async function(isRogzites = false) {
      try {
        var result = await apiService.SaveKirendeltVedoKereseModalData({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
            IsRogzites: isRogzites,
          },
        });
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        if (isRogzites) {
          NotificationFunctions.SuccessAlert(
            'Kirendelt védő kérése',
            'Sikeres mentés'
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
        }

        this.$v.$reset();
        return true;
      } catch (err) {
        var errorMsg = 'Hiba a mentés során: ' + err;
        NotificationFunctions.AjaxError({
          title: 'Kirendelt védő kérése',
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
        console.log(errorMsg);
      }
    },
    async NyilatkozatNyomtatas() {
      this.nyilatkozatNyomtatasLoading = true;
      console.log('NyilatkozatNyomtatas');
      var result = await this.SaveFormData(false);
      if (result) {
        await NyomtatvanyFunctions.VedoKirendeleseNyilatkozatNyomtatas({
          naplobejegyzesIds: this.naplobejegyzesIds,
        });
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
      }
      this.nyilatkozatNyomtatasLoading = false;
    },
    async LevelSablonNyomtatas() {
      this.levelSablonNyomtatasLoading = true;
      var result = await this.SaveFormData(false);
      if (result) {
        await NyomtatvanyFunctions.VedoKirendeleseNyomtatas({
          naplobejegyzesId: this.naplobejegyzesIds[0],
        });
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
      }
      this.levelSablonNyomtatasLoading = false;
    },
    async JogiKepviseletRogzites() {
      console.log('JogiKepviseletRogzites');
      this.jogiKepviseletRogzitesLoading = true;
      var success = await this.SaveFormData(true);
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.jogiKepviseletRogzitesLoading = false;
    },
  },
  computed: {
    ...mapGetters({}),
    select2TorvenyszekOptions: function() {
      return {
        data: this.formData.torvenyszekOptions,
        dropdownParent: $(this.$el),
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
