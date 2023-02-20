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
          10. Határidő módosítási javaslat
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
                  class="form-group summernote"
                  :validator="$v.formData.values.Leiras"
                >
                  <textarea-autosize
                    v-model="$v.formData.values.Leiras.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                    placeholder="Kérem adja meg az indoklást..."
                  />
                  <!-- <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    :settings="{
                      placeholder: 'Kérem adja meg az indoklást...',
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
import { required } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';

export default {
  name: 'javaslat-uj-hataridore',
  data: function() {
    return {
      isFormLoading: false,
      isLoading: false,
      title: 'Határidő módosítási javaslat',
      fegyelmiUgyIds: [],
      formData: {
        donteshozoSzemelyOptions: [],
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
  validations: {
    formData: {
      values: {
        Leiras: {
          required,
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
        var result = await apiService.GetUjHataridoModositasiJavaslatModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
        });
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        console.log('fegyelmiUgyIds:' + fegyelmiUgyIds);
        var result = { Leiras: '' };

        hidrateForm(this, this.formData.values, result);
        await this.$nextTick();
        this.$v.$reset();

        this.isFormLoading = false;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
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

        result = await apiService.UjHataridoModositasiJavaslat({ data });

        NotificationFunctions.SuccessAlert(
          'Határidő módosítási javaslat',
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
  destroyed: function() {},
};
</script>
<style scoped>
.modal-scroll {
  height: auto;
  max-height: 315px;
}
</style>
