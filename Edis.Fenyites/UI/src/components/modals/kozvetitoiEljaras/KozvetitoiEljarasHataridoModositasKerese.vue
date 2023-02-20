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
          45. Határidő módosítás kérése
          <p class="mt-10 mb-0 font-size-12">
            Kérem indokolja a kérést
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
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
                  placeholder="Indoklás"
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  :settings="{ placeholder: 'Indoklás...' }"
                  name="leiras"
                  class="mb-0"
                ></k-summernote>
                <span class="text-help float-right">Indoklás</span> -->
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
        class="btn savebtn white mb-lg-5"
        @click="Rogzites()"
        :disabled="rogzitesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="rogzitesLoading"></b-spinner>
        Kérés elküldése
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';

import $ from 'jquery';
import moment from 'moment';
import { useSimpleModalHandler } from '../../../utils/modal';
import { required, maxLength } from 'vuelidate/lib/validators';
import { apiService, eventBus } from '../../../main';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { deselectDatatable } from '../../../utils/common';
import { hidrateForm } from '../../../utils/vueUtils';

export default {
  name: 'kozvetitoi-eljaras-hatarido-modositas-kerese',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      title: 'Határidő módosítás kérése',
      rogzitesLoading: false,
      formData: {
        values: {
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
        Leiras: {
          required,
          maxLength: maxLength(4000),
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
        var result = await apiService.GetKozvetitoiEljarasHataridoModositasKereseModalData(
          {
            fegyelmiUgyIds: fegyelmiUgyIds,
          }
        );
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }

        this.formData.MaxDatum = result.MaxDatum;
        hidrateForm(this, this.formData.values, result);

        this.formData.values.Datum = moment(this.formData.values.Datum).format(
          'YYYY.MM.DD'
        );
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
    SaveFormData: async function() {
      try {
        var result = await apiService.SaveKozvetitoiEljarasHataridoModositasKereseModalData(
          {
            data: {
              ...this.formData.values,
              FegyelmiUgyIds: this.fegyelmiUgyIds,
            },
          }
        );
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(
          'Határidő módosítás kérése',
          'Sikeres mentés'
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        this.Hide();
        this.$v.$reset();
        return true;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Határidő módosítás kérése',
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
        console.log(err);
      }
    },
    async Rogzites() {
      if (!this.IsFormValid()) {
        return;
      }
      this.rogzitesLoading = true;
      await this.SaveFormData();
      this.rogzitesLoading = false;
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
