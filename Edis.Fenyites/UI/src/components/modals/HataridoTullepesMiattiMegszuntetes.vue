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
          49. Határidő túllépés miatt megszüntetés
          <p class="mt-10 mb-0 font-size-12">
            Egyszerűsített határozat, adja meg a megszüntetés okát, és az
            indoklást.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material static-select2"
                :validator="$v.formData.values.HatarozatotHozoId"
              >
                <k-select2
                  :options="select2HatarozatotHozoOptions"
                  v-model="$v.formData.values.HatarozatotHozoId.$model"
                  class=""
                  placeholder="Válasszon..."
                >
                </k-select2>
                <span class="text-help float-right"
                  >Határozatot hozó személy</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.MegszunesOkaId"
              >
                <k-select2
                  :options="select2MegszunesOkaOptions"
                  v-model="$v.formData.values.MegszunesOkaId.$model"
                  class=""
                  placeholder="Válasszon..."
                >
                </k-select2>
                <span class="text-help float-right">Megszüntetés oka</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.Leiras"
              >
                <b-form-input
                  v-model="$v.formData.values.Leiras.$model"
                  placeholder="Kérem adja meg az indoklást..."
                ></b-form-input>
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
        class="btn bg-blue-grey-600 white mb-lg-5"
        :disabled="nyomtatasLoading || buttonsDisabled"
        @click="Nyomtatas()"
      >
        <b-spinner small v-if="nyomtatasLoading"></b-spinner>
        Nyomtatás
      </button>
      <button
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="Megszuntetes()"
        :disabled="megszuntetesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="megszuntetesLoading"></b-spinner>
        Határozat kész
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
import { treeselectHun } from '../../plugins/vueTreeSelect';

export default {
  name: 'hatarido-tullepes-miatti-megszuntetes',
  data() {
    return {
      isFormLoading: false,
      title: 'Határidő túllépés miatti megszüntetés',
      fegyelmiUgyIds: [],
      treeselectHun,
      naplobejegyzesIds: [],
      megszuntetesLoading: false,
      nyomtatasLoading: false,
      isMounted: false,
      formData: {
        hatarozatotHozoOptions: [],
        megszunesOkaOptions: [],
        values: {
          MegszunesOkaId: null,
          HatarozatotHozoId: null,
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
        MegszunesOkaId: {
          required,
        },
        HatarozatotHozoId: {
          required,
        },
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
        var result = await apiService.GetHataridoTullepesMiattiMegszuntetesModalData(
          {
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          }
        );
        console.log(result);

        hidrateForm(this, this.formData.values, result);

        this.formData.megszunesOkaOptions = result.MegszunesOkaOptions.sort(
          sortStr('text')
        );
        this.formData.hatarozatotHozoOptions = result.HatarozatotHozoOptions.sort(
          sortStr('text')
        );
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
        var result = await apiService.SaveHataridoTullepesMiattiMegszuntetesModalData(
          {
            data: {
              ...this.formData.values,
              FegyelmiUgyIds: this.fegyelmiUgyIds,
              NaplobejegyzesIds: this.naplobejegyzesIds,
            },
          }
        );
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
    async Megszuntetes() {
      if (!this.IsFormValid()) {
        return;
      }
      this.megszuntetesLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.megszuntetesLoading = false;
    },
    async Nyomtatas() {
      if (!this.IsFormValid()) {
        return;
      }
      this.nyomtatasLoading = true;
      var success = await this.SaveFormData();

      if (success) {
        try {
          //TODO: backend Nyomtatás
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
    select2HatarozatotHozoOptions: function() {
      return {
        data: this.formData.hatarozatotHozoOptions,
        dropdownParent: $(this.$el),
        readOnly: true,
        disabled: true,
      };
    },
    select2MegszunesOkaOptions: function() {
      return {
        data: this.formData.megszunesOkaOptions,
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
