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
          18. II. fokú tárgyalási jegyzőkönyv felvitele
          <p class="mt-10 mb-0 font-size-12">
            Tárgyalási jegyzőkönyv rögzítése
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-0">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <!-- <div class="slidePanel-inner-section border-bottom-0 py-15 pr-10"> -->
            <div class="row">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FegyelmiJogkorGyakorlojaSid"
                >
                  <k-select2
                    :options="Select2FegyelmiJogkorGyakorlojaOptions"
                    v-model="
                      $v.formData.values.FegyelmiJogkorGyakorlojaSid.$model
                    "
                    class=""
                    placeholder="Kérem válasszon..."
                  >
                  </k-select2>
                  <span class="text-help float-right">Parancsnok</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.JegyzokonyvVezetoSid"
                >
                  <!-- <select></select> -->
                  <k-select2
                    :options="Select2JegyzokonyvVezetoOptions"
                    v-model="$v.formData.values.JegyzokonyvVezetoSid.$model"
                  ></k-select2>
                  <span class="text-help float-right">Jegyzőkönyv vezető</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <!-- <select></select> -->
                <k-select2
                  :options="Select2TovabbiSzemelyekOptions"
                  v-model="formData.values.TovabbiSzemelyekList"
                ></k-select2>
                <span class="text-help float-right">További személyek</span>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group summernote"
                  :validator="$v.formData.values.Leiras"
                >
                  <!--<textarea class="leiras summernote"></textarea>-->

                  <textarea-autosize
                    v-model="$v.formData.values.Leiras.$model"
                    :min-height="30"
                    class="w-p100 mb-0"
                    name="leiras"
                    :rows="1"
                    placeholder="Kérem írja be, az eljárás alá vont fogvatartott tárgyaláson történő meghallgatásakor elmondottakat..."
                  />
                  <!-- <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    :settings="{
                      placeholder:
                        'Kérem írja be, az eljárás alá vont fogvatartott tárgyaláson történő meghallgatásakor elmondottakat...',
                    }"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote> -->
                  <span class="text-help float-right"
                    >Eljárás alá vont fogvatartott II. fokú tárgyaláson történő
                    meghallgatásakor elmondottak</span
                  >
                </k-vuelidate-error-extractor>
              </div>
            </div>
            <!-- </div> -->
            <!-- <div
        v-if="isLoading"
        class="vertical-align text-center"
        style="position:absolute; left:0; right:0; bottom:0; top:0; background-color:white;"
      >
        <div class="loader vertical-align-middle loader-ellipsis"></div>
      </div> -->
          </div>
        </div>
        <!-- <slot></slot> -->
      </div>
      <div class="modal-footer d-flex justify-content-between">
        <div class="modalorszam"></div>
        <div class="text-right">
          <b-button @click="Hide()" class="btn-raised">Mégsem</b-button>

          <b-button @click="EsemenyNyomtatasEsMentes()" class="btn-raised">
            <b-spinner small v-if="isLoadingNyomtatas"></b-spinner>
            Nyomtatás</b-button
          >

          <b-button
            @click="EsemenyVeglegesMentes()"
            class="ml-3 btn btn-warning btn-raised font-weight-700"
            :disabled="isLoading || buttonsDisabled"
          >
            <b-spinner small v-if="isLoading"></b-spinner>
            Jegyzőkönyv kész, aláírva</b-button
          >
        </div>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import $ from 'jquery';
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import settings from '../../data/settings';
import moment from 'moment';
import { required, minValue } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../utils/modal';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import { deselectDatatable } from '../../utils/common';

export default {
  name: 'k-jegyzokonyv-masodfokutargyalasi',
  data: function() {
    return {
      isFormLoading: false,
      isLoadingNyomtatas: false,
      isLoading: false,
      title: 'II. fokú tárgyalási jegyzőkönyv felvitele',
      formData: {
        fegyelmiJogkorGyakorlojaOptions: [],
        jegyzokonyvVezetoOptions: [],
        tovabbiSzemelyekOptions: [],
        values: {
          NaplobejegyzesIds: [],
          FegyelmiUgyIds: [],
          Leiras: '',
          TanuId: 0,
          TovabbiSzemelyekList: [],
          JegyzokonyvVezetoSid: '',
          FegyelmiJogkorGyakorlojaSid: '',
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
        FegyelmiJogkorGyakorlojaSid: {
          required,
        },
        JegyzokonyvVezetoSid: {
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
        console.log({ data });
        this.formData.values.FegyelmiUgyIds = data.fegyelmiUgyIds;
        this.formData.values.NaplobejegyzesIds = data.naplobejegyzesIds;
        this.LoadFegyelmiUgyData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetMasodFokuTargyalasiJegyzokonyvModalData(
          { data: { fegyelmiUgyIds, naplobejegyzesIds } }
        );

        this.formData.jegyzokonyvVezetoOptions = result.JegyzokonyvVezetoOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        this.formData.tovabbiSzemelyekOptions = result.TovabbiSzemelyekOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        this.formData.fegyelmiJogkorGyakorlojaOptions = result.FegyelmiJogkorGyakorlojaOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );
        for (const key in this.formData.values) {
          if (
            this.formData.values.hasOwnProperty(key) &&
            result.hasOwnProperty(key)
          ) {
            this.$set(this.formData.values, key, result[key]);
          }
        }

        this.$v.$reset();
        //$("#testx").select2({
        //  data: this.formData.tovabbiSzemelyekOptions,
        //  //dropdownParent: $(this.$el),
        //  multiple: true,
        //  tags: true,
        //});
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
        this.Hide();
      }
    },
    async EsemenyNyomtatasEsMentes() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isLoadingNyomtatas = true;
      let result = await this.SaveEsemeny(false);
      if (result) {
        await NyomtatvanyFunctions.FegyelmiTargyalasiJegyzokonyvMasodfokNyomtatas(
          {
            naplobejegyzesIds: this.formData.values.NaplobejegyzesIds,
          }
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
      }
      this.isLoadingNyomtatas = false;
    },
    async EsemenyVeglegesMentes() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isLoading = true;
      await this.SaveEsemeny(true);
      this.isLoading = false;
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
    SaveEsemeny: async function(alert) {
      var result = null;
      try {
        result = await apiService.SaveMasodFokuTargyalasiJegyzokonyvModalData({
          data: {
            ...this.formData.values,
          },
        });
        this.formData.values.NaplobejegyzesIds = result.naplobejegyzesIds;
        if (alert) {
          NotificationFunctions.SuccessAlert(
            'II. fokú tárgyalási jegyzőkönyv rögzítése',
            'Sikeres mentés'
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'II. fokú tárgyalási jegyzőkönyv rögzítése',
          text: 'A jegyzőkönyv mentése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
      return result;
    },
    StartLoader: function() {
      this.isLoading = true;
    },
    EndLoader: function() {
      this.isLoading = false;
    },
  },
  computed: {
    Select2FegyelmiJogkorGyakorlojaOptions: function() {
      return {
        data: this.formData.fegyelmiJogkorGyakorlojaOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2JegyzokonyvVezetoOptions: function() {
      return {
        data: this.formData.jegyzokonyvVezetoOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2TovabbiSzemelyekOptions: function() {
      return {
        data: this.formData.tovabbiSzemelyekOptions,
        dropdownParent: $(this.$el),
        multiple: true,
        tags: true,
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
    UjTanuFelvetelSelectOptions() {
      if (!this.isMounted) {
        return;
      }
      //var id = this.formData.Eszlelo ? [this.formData.Eszlelo.Id] : [];

      return {
        placeholder: 'Tanú neve',
        apiHandler: apiService.FindFogvatartottakForSelect.bind(apiService),
        multiple: false,
        dropdownParent: $(this.$el),
        minimumInputLength: 5,
        allowClear: true,
      };
    },
    getData() {
      return JSON.stringify(this.formData.values, null, 2);
    },
  },

  destroyed: function() {},
};
</script>
<style scoped>
.modal-scroll {
  height: 315px;
}
</style>
