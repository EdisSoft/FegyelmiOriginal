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
          12. Összefoglaló jelentés készítése<br />
          <p class="mt-10 mb-0 font-size-12">
            Meghallgatások, szembesítések és szemlék alapján összefoglaló<br />
            jelentés készítése a fegyelmi jogkör gyakorlójának
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-25 pb-40">
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
                  <!-- <textarea-autosize
                    v-model="$v.formData.values.Leiras.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                  /> -->
                  <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote>
                  <span class="text-help float-right"
                    >Összefoglaló jelentés</span
                  >
                </k-vuelidate-error-extractor>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <b-button @click="Hide()" class="btn-raised">Mégsem</b-button>
        <button
          type="button"
          class="btn btn-dark"
          @click="Nyomtatas()"
          :disabled="isFormLoadingNyomtatas || buttonsDisabled"
        >
          <b-spinner small v-if="isFormLoadingNyomtatas"></b-spinner>
          Nyomtatás
        </button>
        <b-button
          @click="SaveFormDataRogzites(false)"
          class="ml-3 btn btn-warning btn-raised font-weight-700"
          :disabled="isLoadingRogzites || buttonsDisabled"
        >
          <b-spinner small v-if="isLoadingRogzites"></b-spinner>
          Összefoglaló jelentés rögzítése
        </b-button>
        <b-button
          @click="SaveFormDataKesz(true)"
          class="ml-3 btn btn-warning btn-raised font-weight-700"
          :disabled="isLoadingKesz || buttonsDisabled"
        >
          <b-spinner small v-if="isLoadingKesz"></b-spinner>
          Összefoglaló jelentés kész
        </b-button>
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
import ModalTipus from '../../data/enums/modalTipus';
import { deselectDatatable } from '../../utils/common';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';

export default {
  name: 'k-osszefoglalo-jelentes',
  data: function() {
    return {
      isFormLoading: false,
      isLoadingRogzites: false,
      isLoadingKesz: false,
      isFormLoadingNyomtatas: false,
      title: 'Összefoglaló jelentés készítése',
      fegyelmiUgyIds: [],
      modalType: -1,
      formData: {
        values: {
          NaplobejegyzesIds: [],
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
        console.log({ data });
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.modalType = data.modalType;
        this.LoadFormData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        console.log('fegyelmiUgyIds:' + fegyelmiUgyIds);
        var result = await apiService.GetOsszefoglaloJelentesModalData({
          fegyelmiUgyIds,
          naplobejegyzesIds,
        });
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        hidrateForm(this, this.formData.values, result);

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
    async SaveFormDataRogzites(isAlairta) {
      this.isLoadingRogzites = true;
      await this.SaveFormData(isAlairta);
      this.isLoadingRogzites = false;
    },
    async SaveFormDataKesz(isAlairta) {
      this.isLoadingKesz = true;
      await this.SaveFormData(isAlairta);
      this.isLoadingKesz = false;
      this.Hide();
    },
    async Nyomtatas() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        console.log(this.$v);
        return;
      }
      this.isFormLoadingNyomtatas = true;
      let result = await this.SaveFormData(false);

      //let naplobejegyzesIds = result.naplobejegyzesIds;
      //this.isFormLoadingNyomtatas = false;
      //console.log(naplobejegyzesIds);

      if (this.formData.values.NaplobejegyzesIds) {
        await NyomtatvanyFunctions.OsszefoglaloJelentesDocxNyomtatas({
          naplobejegyzesId: this.formData.values.NaplobejegyzesIds[0],
        });
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
      }
      this.isFormLoadingNyomtatas = false;
      // NotificationFunctions.SuccessAlert(
      //   'Összefoglaló jelentés',
      //   'Az összefoglaló jelentés nyomtatása megtörtént'
      // );
    },

    async SaveFormData(isAlairta) {
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

        let data = {
          ...this.formData.values,
          FegyelmiUgyIds: this.fegyelmiUgyIds,
          IsAlairta: isAlairta,
        };
        var result = await apiService.SaveOsszefoglaloJelentes({ data });
        this.formData.values.NaplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(
          'Összefoglaló jelentés',
          'Az összefoglaló jelentés mentése megtörtént'
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Az összefoglaló jelentés mentése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
    },
  },
  computed: {
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
