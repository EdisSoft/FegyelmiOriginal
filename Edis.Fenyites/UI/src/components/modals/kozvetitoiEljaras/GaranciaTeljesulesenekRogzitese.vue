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
          30. Garancia teljesülésének rögzítése
          <p class="mt-10 mb-0 font-size-12">
            A megállapodás foglaltak határidőre teljesülésének rögzítése
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-20">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.IsTeljesult"
              >
                <div class="radio-buttons-wrapper mb-10">
                  <b-form-radio
                    v-model="$v.formData.values.IsTeljesult.$model"
                    name="IsTeljesult"
                    :value="true"
                    class="blue-grey-400"
                  >
                    A megállapodásban foglalt vállalások határidőre teljesültek
                  </b-form-radio>
                  <b-form-radio
                    v-model="$v.formData.values.IsTeljesult.$model"
                    name="IsTeljesult"
                    :value="false"
                    class="blue-grey-400"
                  >
                    A megállapodásban foglalt vállalások nem teljesültek
                  </b-form-radio>
                </div>
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
        @click="GaranciaTeljesulese()"
        :disabled="garanciaTeljesuleseLoading || buttonsDisabled"
      >
        <b-spinner small v-if="garanciaTeljesuleseLoading"></b-spinner>
        Vállalások eredményének rögzítése
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { useSimpleModalHandler } from '../../../utils/modal';
import { eventBus, apiService } from '../../../main';
import { required, maxLength } from 'vuelidate/lib/validators';
import moment from 'moment';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { deselectDatatable } from '../../../utils/common';
import { mapGetters } from 'vuex';
import { hidrateForm } from '../../../utils/vueUtils';
import $ from 'jquery';
import { sortStr } from '../../../utils/sort';

export default {
  name: 'garancia-teljesulesenek-rogzitese',
  data() {
    return {
      isFormLoading: false,
      title: 'Garancia teljesülésének rögzítése',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      garanciaTeljesuleseLoading: false,
      isMounted: false,
      formData: {
        values: {
          IsTeljesult: null,
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
        IsTeljesult: { required },
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
        var result = await apiService.GetGaranciaTeljesulesenekRogziteseModalData(
          {
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          }
        );

        hidrateForm(this, this.formData.values, result);
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
        var result = await apiService.SaveGaranciaTeljesulesenekRogziteseModalData(
          {
            data: {
              ...this.formData.values,
              FegyelmiUgyIds: this.fegyelmiUgyIds,
              NaplobejegyzesIds: this.naplobejegyzesIds,
            },
          }
        );
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(
          this.title,
          this.IsTeljesult
            ? 'A megállapodás határidőre teljesült.'
            : 'A megállapodás határidőre nem teljesült.'
        );
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
    async GaranciaTeljesulese() {
      if (!this.IsFormValid()) {
        return;
      }
      this.garanciaTeljesuleseLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.garanciaTeljesuleseLoading = false;
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
