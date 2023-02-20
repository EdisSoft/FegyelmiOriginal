<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-grey">
      <div class="modal-header">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title">
          13. Felfüggesztési javaslat
          <p class="mt-10 mb-0 font-size-12">
            Kérem indokolja a kérést
          </p>
        </h4>
      </div>
      <div class="modal-body pl-25 pt-25 pb-40 pr-25">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <div class="row">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FelfuggesztesOkaCimkeId"
                >
                  <k-select2
                    :options="felfuggesztesOkaOptions"
                    v-model="$v.formData.values.FelfuggesztesOkaCimkeId.$model"
                    class=""
                  >
                  </k-select2>
                  <span class="text-help float-right">Felfüggesztés oka</span>
                </k-vuelidate-error-extractor>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <b-button @click="Hide()" class="btn-raised">Mégsem</b-button>

        <b-button
          @click="SaveFormData()"
          class="ml-3 btn btn-warning btn-raised font-weight-700"
          :disabled="isLoading || buttonsDisabled"
        >
          <b-spinner small v-if="isLoading"></b-spinner>
          Javaslat rögzítése</b-button
        >
      </div>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import $ from 'jquery';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import { required, minValue } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';
import { sortStr } from '../../utils/sort';

export default {
  name: 'javaslat-felfuggesztesre',
  data: function() {
    return {
      isFormLoading: false,
      isLoading: false,
      title: 'Felfüggesztési javaslat',
      fegyelmiUgyIds: [],
      formData: {
        felfuggesztesOkaOptions: [],
        values: {
          FelfuggesztesOkaCimkeId: null,
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
  validations: {
    formData: {
      values: {
        FelfuggesztesOkaCimkeId: {
          required,
          minValueSelect: minValue(1),
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.LoadFormData(data.fegyelmiUgyIds);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function(fegyelmiUgyIds) {
      this.isFormLoading = true;
      try {
        console.log('fegyelmiUgyIds:' + fegyelmiUgyIds);
        var result = await apiService.GetFelfuggesztesiJavaslat({
          fegyelmiUgyIds: fegyelmiUgyIds,
        });
        hidrateForm(this, this.formData.values, result);
        this.formData.felfuggesztesOkaOptions = result.FelfuggesztesOkaOptions.sort(
          sortStr('text')
        );
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
    SaveFormData: async function() {
      try {
        if (this.$v.$invalid) {
          this.$v.$touch();
          this.$nextTick(() => {
            var element = this.$el.querySelector(
              '.form-group.error:first-child'
            );
            element.scrollIntoView();
          });
          return;
        }
        this.isLoading = true;
        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
        };
        var result = null;

        result = await apiService.UjFelfuggesztesiJavaslat({ data });

        NotificationFunctions.SuccessAlert(
          this.modalTitle,
          'A javaslat mentése sikerült'
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        this.Hide();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Javaslat mentése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
      this.isLoading = false;
    },
  },
  computed: {
    Select2DontestHozoOptions: function() {
      return {
        data: this.formData.donteshozoSzemelyOptions,
        dropdownParent: $(this.$el),
      };
    },
    felfuggesztesOkaOptions: function() {
      return {
        data: this.formData.felfuggesztesOkaOptions,
        dropdownParent: $(this.$el),
        placeholder: 'Kérem válassza ki a felfüggesztés okát...',
      };
    },
  },
  destroyed: function() {},
};
</script>
<style scoped>
.modal-scroll {
  height: auto;
  max-height: 315px;
}
</style>
