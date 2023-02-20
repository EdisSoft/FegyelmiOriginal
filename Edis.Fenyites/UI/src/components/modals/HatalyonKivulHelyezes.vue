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
        <h4 class="modal-title">38. Hatályon kívül helyezés</h4>
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
                    placeholder="Ügyészi határozat..."
                  />
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
          Hatályon kívül helyezés</b-button
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
  name: 'ugyeszi-hatalyon-kivul-helyezes',
  data: function () {
    return {
      isFormLoading: false,
      title: 'Ügyészi hatályon kívül helyezés',
      isLoading: false,
      fegyelmiUgyIds: [],
      formData: {
        donteshozoSzemelyOptions: [],
        naplobejegyzesIds: [],
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
  mounted: function () {
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
    InitEvents: function ({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.formData.naplobejegyzesIds = data.naplobejegyzesIds;
        this.LoadFormData(data.fegyelmiUgyIds, this.formData.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function (fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        console.log('fegyelmiUgyIds:' + fegyelmiUgyIds);
        console.log('naplobejegyzesIds:' + naplobejegyzesIds);
        var result = { Leiras: '' };
        result = await apiService.GetUgyesziHatalyonKivulHelyezesModalData({
          fegyelmiUgyIds: fegyelmiUgyIds,
        });

        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        hidrateForm(this, this.formData.values, result);
        await this.$nextTick();
        this.$v.$reset();
        this.isFormLoading = false;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok lekérdezése sikertelen',
          errorObj: err,
        });
        console.log(err);
        this.Hide();
      }
    },
    SaveFormData: async function () {
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
          NaplobejegyzesIds: this.formData.naplobejegyzesIds,
        };
        var result = null;

        result = await apiService.HatalyonKivulHelyezesRogzites({ data });

        NotificationFunctions.SuccessAlert(
          'Ügyészség',
          'Hatályon kívül helyezte'
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        this.Hide();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Hatályon kívül helyezés sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
      this.isLoading = false;
    },
    UgyesziHatalyonKivulHelyezesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
        //naploId: this.naplobejegyzes.Id,
        leiras: this.naplobejegyzes.Leiras,
      };
      eventBus.$emit('Modal:ugyeszi-hatalyon-kivul-helyezes', {
        state: true,
        data,
      });
    },
  },
  computed: {
    SummernoteConfig: function () {
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
  destroyed: function () {},
};
</script>
<style scoped>
.modal-scroll {
  height: auto;
  max-height: 315px;
}
</style>
