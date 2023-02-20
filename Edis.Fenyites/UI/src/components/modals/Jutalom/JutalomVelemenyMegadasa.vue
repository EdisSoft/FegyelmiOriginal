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
          4. {{ title }}
          <!-- <p class="mt-10 mb-0 font-size-12">
            Döntés saját jögkörben, illetve javaslat tétel más jögkörben
            elérhető jutalomra.
          </p> -->
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group summernote"
                :validator="$v.formData.values.SzakVelemeny"
              >
                <k-summernote
                  v-model="$v.formData.values.SzakVelemeny.$model"
                  name="leiras"
                  class="mb-0"
                ></k-summernote>
                <span class="text-help float-right">SzakVélemény</span>
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
        :disabled="mentesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="mentesLoading"></b-spinner>
        Vélemény kész
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../../main';
import { eventBus } from '../../../main';
import { required, minValue, maxLength } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../../utils/modal';
import { hidrateForm } from '../../../utils/vueUtils';
import { deselectDatatable } from '../../../utils/common';
import $ from 'jquery';
import moment from 'moment';

export default {
  name: 'jutalom-velemeny-megadasa',
  data() {
    return {
      isFormLoading: false,
      title: 'Vélemény megadása',
      jutalomIds: [],

      mentesLoading: false,
      isMounted: false,
      formData: {
        jutalomTipusokOptions: [],
        values: {
          JutalomId: -1,
          SzakVelemeny: '',
          CsatolmanyUrl: '',
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
        SzakVelemeny: {
          required,
        },
        CsatolmanyUrl: {
          //   required,
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        console.log(data);
        this.jutalomIds = data.jutalomIds || [];
        this.LoadFormData(data.jutalomIds);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function(jutalomIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetJutalomVelemenyezesModalData({
          jutalomId: jutalomIds[0],
        });
        hidrateForm(this, this.formData.values, result);
        this.isFormLoading = false;
        await this.$nextTick();
        this.$v.$reset();
      } catch (err) {
        console.log(err);
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        this.Hide();
      }
    },
    SaveFormData: async function() {
      try {
        console.log('data', this.formData.values);
        var result = await apiService.SaveJutalomVelemenyezesModalData({
          data: {
            ...this.formData.values,
            JutalomId: this.jutalomIds[0],
          },
        });
        NotificationFunctions.SuccessAlert(result.title, result.message);
        eventBus.$emit('Sidebar:jutalmiUgyReszletek:refresh');
        await this.$nextTick();
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
      this.mentesLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.mentesLoading = false;
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
    OnUploadError({ file, response }) {
      if (response == 'Megszakítva') {
        return;
      }
      NotificationFunctions.ErrorAlert(
        'Hiba',
        response.message || `${file.name} feltöltése sikertelen.`
      );
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
