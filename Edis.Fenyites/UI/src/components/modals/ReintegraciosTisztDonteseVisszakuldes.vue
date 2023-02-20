<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-grey modal-container">
      <div class="modal-header" style="height: auto !important;">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title font-size-20">
          42. Visszaküldés a fegyelmi jogkör gyakorlójának
          <p class="mt-10 mb-0 font-size-12">
            Kérem válassza ki a visszaküldés okát, és ha szükséges az indoklást
            is!
          </p>
        </h4>
      </div>
      <div class="modal-body pt-20 pb-40 px-25">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <div class="row">
              <div class="col-md-12 mb-10">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.VisszakuldesOka"
                >
                  <k-select2
                    :options="visszakuldesOkaOptions"
                    v-model="$v.formData.values.VisszakuldesOka.$model"
                    class=""
                    placeholder="Válassza ki a visszaküldés okát"
                  >
                  </k-select2>
                  <span class="text-help float-right">Visszaküldés oka</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material summernote"
                  :validator="$v.formData.values.Indoklas"
                >
                  <textarea-autosize
                    v-model="$v.formData.values.Indoklas.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                    placeholder="Ha szeretne további indoklást tenni, itt megteheti..."
                  />
                  <!-- <k-summernote
                  v-model="$v.formData.values.Indoklas.$model"
                  :settings="{
                    placeholder:
                      'Ha szeretne további indoklást tenni, itt megteheti...',
                  }"
                  name="leiras"
                  class="mb-0"
                ></k-summernote> -->
                  <span class="text-help float-right">Indoklás</span>
                </k-vuelidate-error-extractor>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer align-right">
        <button type="button" class="btn btn-dark mb-lg-5" @click="Hide">
          Mégsem
        </button>
        <b-button
          type="button"
          class="btn btn-warning mb-lg-5"
          @click="Mentes()"
          :disabled="isMentesLoading || buttonsDisabled"
        >
          <b-spinner small v-if="isMentesLoading"></b-spinner>
          Visszaküldés
        </b-button>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService, eventBus } from '../../main';
import { AppStoreTypes } from '../../store/modules/app';
import settings from '../../data/settings';
import { required, minValue } from 'vuelidate/lib/validators';
import moment from 'moment';
import $ from 'jquery';
import ReintegraciosTisztDontesTipus from '../../data/enums/reintegraciosTisztDontesTipus';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { hidrateForm } from '../../utils/vueUtils';
import { sortStr } from '../../utils/sort';
import { useSimpleModalHandler } from '../../utils/modal';
import Cimkek from '../../data/enums/Cimkek';
import { deselectDatatable } from '../../utils/common';

export default {
  name: 'k-reintegracios-tiszt-dontese-visszakuldes',
  data() {
    return {
      fegyelmiUgyIds: [],
      dontesTipus: null,
      title: 'Visszaküldés',
      isMentesLoading: false,
      isFormLoading: false,
      formData: {
        visszakuldesOkaOptions: [],
        values: {
          VisszakuldesOka: null,
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
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations() {
    let rules = {
      formData: {
        values: {
          VisszakuldesOka: {
            required,
          },
          Indoklas: {},
        },
      },
    };
    if (
      this.formData.values.VisszakuldesOka ==
      Cimkek.VisszakuldesOka.EgyebOkMiatt
    ) {
      rules.formData.values.Indoklas = { required };
    }
    return rules;
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        console.log({ data });
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.LoadFormData();
      } else {
        this.Hide();
      }
    },
    async LoadFormData() {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetReintegraciosTisztDontesVisszakuldesModalData(
          {
            fegyelmiUgyIds: this.fegyelmiUgyIds,
          }
        );
        console.log(result);

        hidrateForm(this, this.formData.values, result);

        this.formData.visszakuldesOkaOptions = result.VisszakuldesOkaOptions;

        this.formData.visszakuldesOkaOptions.sort(sortStr('text'));

        this.$v.$reset();
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
        console.log(err);
        this.Hide();
      }
    },
    async Mentes(isTudomasulVette) {
      if (this.$v.$invalid) {
        this.$v.$touch();
        return;
      }

      this.isMentesLoading = true;

      try {
        let data = {
          ...this.formData.values,
          fegyelmiUgyIds: this.fegyelmiUgyIds,
        };
        let result = await apiService.ReintegraciosTisztDontesVisszakuldesModalMentes(
          {
            data,
          }
        );
        NotificationFunctions.SuccessAlert(
          'Visszaküldes reintegrációs tiszti jogkörben',
          'A visszaküldes mentése megtörtént'
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        this.Hide();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'A visszaküldes mentése nem sikerült',
          errorObj: err,
        });
        console.log(err);
      }

      this.isMentesLoading = false;
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),
    visszakuldesOkaOptions: function() {
      return {
        data: this.formData.visszakuldesOkaOptions,
        placeholder: 'Válassza ki a visszaküldés okát',
        dropdownParent: $(this.$el),
      };
    },
  },
  components: {},
  watch: {
    fegyelmiUgyIds: {
      handler: function(newValue, oldValue) {},
      deep: true,
      immediate: true,
    },
  },
  props: {},
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
.modal-header {
  height: 88px;
}
/* .modal-footer {
  height: 75px;
} */
.modal-scroll {
  height: auto;
}
</style>
