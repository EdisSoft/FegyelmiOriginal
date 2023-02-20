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
          34. Meghatalmazott védő kérése
          <p class="mt-10 mb-0 font-size-12">
            Ha az eljárás alá vont fogvatartott jogi képviseletet kér, töltsük
            ki annak formáját.
          </p>
        </h4>
      </div>
      <div class="modal-body pl-25 pt-20 pb-40">
        <div>
          <div class="row pr-10">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.VedoNeve"
              >
                <b-form-input
                  v-model="$v.formData.values.VedoNeve.$model"
                  placeholder="Védő neve..."
                ></b-form-input>
                <span class="text-help float-right">
                  Védő neve
                </span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.VedoCime"
              >
                <b-form-input
                  v-model="$v.formData.values.VedoCime.$model"
                  placeholder="Védő címe..."
                ></b-form-input>
                <span class="text-help float-right">
                  Védő címe
                </span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.VedoElerhetosege"
              >
                <b-form-input
                  v-model="$v.formData.values.VedoElerhetosege.$model"
                  placeholder="Védő elérhetősége..."
                ></b-form-input>
                <span class="text-help float-right">
                  Védő elérhetősége (telefon vagy fax szám)
                </span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.Titulus"
              >
                <div class="radio-buttons-wrapper mb-10">
                  <b-form-radio
                    v-model="$v.formData.values.Titulus.$model"
                    v-for="titulusOption in formData.titulusOptions"
                    :key="titulusOption.id"
                    name="titulus"
                    :value="titulusOption.id"
                    class="blue-grey-400"
                  >
                    {{ titulusOption.text }}
                  </b-form-radio>
                </div>
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
                  placeholder="Indoklás részletes leírása..."
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  :settings="{ placeholder: 'Indoklás részletes leírása...' }"
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
  name: 'meghatalmazott-vedo-kerese',
  data() {
    return {
      isFormLoading: false,
      title: 'Meghatalmazott védő kérése',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      nyilatkozatNyomtatasLoading: false,
      levelSablonNyomtatasLoading: false,
      jogiKepviseletRogzitesLoading: false,
      formData: {
        titulusOptions: [],
        values: {
          VedoNeve: '',
          VedoCime: '',
          VedoElerhetosege: '',
          Titulus: 0,
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
        VedoNeve: {
          required,
        },
        VedoCime: {
          required,
        },
        VedoElerhetosege: {},
        Titulus: {},
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
        var result = await apiService.GetMeghatalmazottVedoKereseModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
          naplobejegyzesIds: naplobejegyzesIds,
        });
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        console.log(result);
        this.formData.titulusOptions = result.TitulusOptions;
        hidrateForm(this, this.formData.values, result);
        this.isFormLoading = false;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        this.Hide();
      }

      this.$v.$reset();
    },
    SaveFormData: async function(isRogzites = false) {
      try {
        var result = await apiService.SaveMeghatalmazottVedoKereseModalData({
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
            'Meghatalmazott védő kérése',
            'Sikeres mentés'
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
        }

        this.$v.$reset();

        return true;
      } catch (err) {
        var errorMsg = 'Hiba a mentés során: ' + err;
        NotificationFunctions.AjaxError({
          title: 'Meghatalmazott védő kérése',
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
        console.log(errorMsg);
      }
    },
    async NyilatkozatNyomtatas() {
      if (!this.IsFormValid()) return;
      this.nyilatkozatNyomtatasLoading = true;
      await this.SaveFormData(false);
      await NyomtatvanyFunctions.MeghatalmazottVedoKirendeleseNyilatkozatNyomtatas(
        {
          naplobejegyzesIds: this.naplobejegyzesIds,
        }
      );
      eventBus.$emit('Sidebar:ugyReszletek:refresh');
      this.nyilatkozatNyomtatasLoading = false;
    },
    async LevelSablonNyomtatas() {
      if (!this.IsFormValid()) return;
      this.levelSablonNyomtatasLoading = true;
      await this.SaveFormData(false);
      await NyomtatvanyFunctions.MeghatalmazottVedoKirendeleseNyomtatas({
        naplobejegyzesId: this.naplobejegyzesIds[0],
      });
      eventBus.$emit('Sidebar:ugyReszletek:refresh');
      this.levelSablonNyomtatasLoading = false;
    },
    async JogiKepviseletRogzites() {
      if (!this.IsFormValid()) return;
      this.jogiKepviseletRogzitesLoading = true;
      let success = await this.SaveFormData(true);
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.jogiKepviseletRogzitesLoading = false;
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
.radio-buttons-wrapper {
  padding: 5px 0;
}
</style>
