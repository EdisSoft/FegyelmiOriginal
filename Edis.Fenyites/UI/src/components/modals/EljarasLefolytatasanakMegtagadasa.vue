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
          2. Eljárás lefolytatásának megtagadása
        </h4>
      </div>
      <div class="modal-body pl-25 pt-25 pb-50">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <div class="row">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material static-select2"
                  :validator="$v.formData.values.DonteshozoSzemelySid"
                >
                  <k-select2
                    :options="Select2DontestHozoOptions"
                    v-model="$v.formData.values.DonteshozoSzemelySid.$model"
                  ></k-select2>
                  <span class="text-help float-right"
                    >Döntést hozó személy</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group summernote"
                  :validator="$v.formData.values.Leiras"
                >
                  <!-- <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote> -->

                  <textarea-autosize
                    v-model="$v.formData.values.Leiras.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                  />

                  <span class="text-help float-right"
                    >Megtagadás indoklása</span
                  >
                </k-vuelidate-error-extractor>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <b-button @click="Hide()" class="btn-raised">Mégsem</b-button>

        <b-button
          @click="Megtagadas()"
          class="ml-3 btn btn-warning btn-raised font-weight-700"
          :disabled="isLoading || buttonsDisabled"
        >
          <b-spinner small v-if="isLoading"></b-spinner>
          Megtagadom</b-button
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
  name: 'k-eljaras-megtagadasa',
  data: function() {
    return {
      isFormLoading: false,
      isLoading: false,
      //fegyelmiUgyId: 0,
      fegyelmiUgyIds: [],
      title: 'Megtagadás',
      formData: {
        donteshozoSzemelyOptions: [],
        values: {
          Leiras: '',
          DonteshozoSzemelySid: '',
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
  validations: {
    formData: {
      values: {
        DonteshozoSzemelySid: {
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
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.LoadFegyelmiUgyData(data.fegyelmiUgyIds);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyIds) {
      this.isFormLoading = true;
      try {
        console.log('fegyelmiUgyIds:' + fegyelmiUgyIds);
        var result = await apiService.GetEljarasLefolytatasanakMegtagadasa({
          fegyelmiUgyId: fegyelmiUgyIds,
        });

        hidrateForm(this, this.formData.values, result);

        this.formData.donteshozoSzemelyOptions = result.DonteshozoSzemelyek.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
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
    Megtagadas: async function() {
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
        var result = await apiService.SaveEljarasLefolytatasanakMegtagadasa({
          data,
        });
        NotificationFunctions.SuccessAlert(
          'Eljárás lefolytatásának megtagadása',
          'Eljárás lefolytatása megtagadva'
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        this.Hide();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Hiba történt a megtagadás során',
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
        readOnly: true,
        disabled: true,
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
