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
          8. Személyi állományi tanú meghallgatási jegyzőkönyv felvétele
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <!-- <div class="slidePanel-inner-section border-bottom-0 py-15 pr-10"> -->
            <div class="row">
              <div class="col-md-10">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.TanuSid"
                >
                  <k-select2
                    :options="Select2TanuOptions"
                    v-model="$v.formData.values.TanuSid.$model"
                    class=""
                    placeholder="Válassza ki a tanút..."
                  >
                  </k-select2>
                  <span class="text-help float-right">Tanú</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-xl-2 col-sm-2 pl-10 text-center">
                <b-button
                  type="button"
                  class="btn btn-icon btn-dark btn-round"
                  id="popover-manual-2"
                  :disabled="!tanukSzerkesztheto"
                  @click="AddTanu"
                >
                  <i class="icon wb-plus white" aria-hidden="true"></i>
                </b-button>
                <b-popover
                  style="width:auto"
                  placement="bottomleft"
                  target="popover-manual-2"
                  custom-class="modal-popover"
                  :show.sync="ujTanuFelvetelOpen"
                  triggers="null"
                >
                  <k-select2
                    :options="UjTanuFelvetelSelectOptions"
                    v-model="ujTanuId"
                  >
                  </k-select2>
                  <div class="text-right mt-10">
                    <b-button
                      size="sm"
                      variant="default"
                      @click="AddTanu"
                      class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700 mr-5"
                      >Mégse</b-button
                    >
                    <b-button
                      size="sm"
                      variant="warning"
                      @click="SaveTanu"
                      class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
                      >Tanú felvétele</b-button
                    >
                  </div>
                </b-popover>
              </div>
            </div>
            <div class="row">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.MeghallgatoSid"
                >
                  <k-select2
                    :options="Select2MeghallgatoOptions"
                    v-model="$v.formData.values.MeghallgatoSid.$model"
                    class=""
                    placeholder="Meghallgató"
                  >
                  </k-select2>
                  <span class="text-help float-right">Meghallgató</span>
                </k-vuelidate-error-extractor>
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
                  />

                  <!-- <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote> -->
                  <span class="text-help float-right">Jegyzőkönyv</span>
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
      <div class="modal-footer">
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
  name: 'k-jegyzokonyv-szemelyiallomanyitanumeghallgatas',
  data: function() {
    return {
      isFormLoading: false,
      ujTanuId: '',
      ujTanuFelvetelOpen: false,
      isLoadingNyomtatas: false,
      isLoading: false,
      title: 'Személyi állományi tanú meghallgatási jegyzőkönyv',
      isMounted: false,
      formData: {
        tanuOptions: [],
        meghallgatoOptions: [],
        intezetiUsersOptions: [],
        values: {
          NaplobejegyzesIds: [],
          FegyelmiUgyIds: [],
          Leiras: '',
          TanuSid: '',
          MeghallgatoSid: '',
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
        TanuSid: {
          required,
        },
        MeghallgatoSid: {
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
      console.log('InitEvents');
      if (state) {
        console.log({ data });
        this.formData.values.FegyelmiUgyIds = data.fegyelmiUgyIds;
        this.LoadFegyelmiUgyData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(
          { data: { fegyelmiUgyIds, naplobejegyzesIds } }
        );

        this.formData.intezetiUsersOptions = result.IntezetiUsersOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        this.formData.tanuOptions = result.TanuOptions.sort(function(a, b) {
          return ('' + a.text).localeCompare(b.text);
        });

        this.formData.meghallgatoOptions = result.MeghallgatoOptions.sort(
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
        console.log(err);
        this.Hide();
      }
    },
    async EsemenyNyomtatasEsMentes() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isLoadingNyomtatas = true;
      let result = await this.SaveEsemeny(false);

      await NyomtatvanyFunctions.SzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvNyomtatas(
        { naplobejegyzesIds: result.naplobejegyzesIds }
      );
      eventBus.$emit('Sidebar:ugyReszletek:refresh');
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
    SaveEsemeny: async function(tanuAlairta) {
      var result = null;
      try {
        result = await apiService.SaveFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(
          {
            data: {
              ...this.formData.values,
              AlairtaFl: tanuAlairta,
            },
          }
        );
        this.formData.values.NaplobejegyzesIds = result.naplobejegyzesIds;
        if (tanuAlairta) {
          NotificationFunctions.SuccessAlert(
            'Személyi állományi tanú meghallgatási jegyzőkönyv rögzítése',
            'Sikeres mentés'
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Személyi állományi tanú meghallgatási jegyzőkönyv rögzítése',
          text: 'A jegyzőkönyv mentése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
      return result;
    },
    AddTanu: function() {
      if (this.ujTanuFelvetelOpen) {
        this.ujTanuFelvetelOpen = false;
        this.ujTanuId = '';
      } else {
        this.ujTanuFelvetelOpen = true;
      }
    },
    SaveTanu: async function() {
      if (!this.ujTanuId) {
        NotificationFunctions.WarningAlert(
          'Új tanú felvétele',
          'Válasszon ki egy tanút'
        );
        return;
      }

      var result = null;
      try {
        result = await apiService.SaveFegyelmiUgyUjAllomanyiResztvevo({
          data: {
            fegyelmiUgyIds: this.formData.values.FegyelmiUgyIds,
            allomanyiTanuSid: this.ujTanuId,
          },
        });
        this.formData.values.TanuSid = result.resztvevoId;
        result = await apiService.GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(
          {
            data: {
              fegyelmiUgyIds: this.formData.values.FegyelmiUgyIds,
              naplobejegyzesIds: this.naplobejegyzesIds,
            },
          }
        );
        this.formData.tanuOptions = result.TanuOptions.sort(function(a, b) {
          return ('' + a.text).localeCompare(b.text);
        });
        this.ujTanuFelvetelOpen = false;
        this.ujTanuId = '';
        NotificationFunctions.SuccessAlert(
          'Új tanú felvétele',
          'A tanú felvétel mentve'
        );
      } catch (err) {
        var errorMsg = 'Hiba az új tanú mentése során: ' + err;
        NotificationFunctions.ErrorAlert(
          'Új tanú felvétele',
          'A tanú felvétel mentése sikertelen',
          err
        );
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
    tanukSzerkesztheto() {
      return (
        (!this.formData.values.NaplobejegyzesIds ||
          this.formData.values.NaplobejegyzesIds.length == 0) &&
        !this.isLoadingNyomtatas &&
        !this.isLoading
      );
    },
    Select2TanuOptions: function() {
      return {
        readOnly: !this.tanukSzerkesztheto,
        disabled: !this.tanukSzerkesztheto,
        data: this.formData.tanuOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2MeghallgatoOptions: function() {
      return {
        data: this.formData.meghallgatoOptions,
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
    UjTanuFelvetelSelectOptions() {
      if (!this.isMounted || !this.ujTanuFelvetelOpen) {
        return;
      }
      //var id = this.formData.Eszlelo ? [this.formData.Eszlelo.Id] : [];
      console.log(this.formData.intezetiUsersOptions);
      return {
        data: this.formData.intezetiUsersOptions,
        dropdownParent: $(this.$el),
        //multiple: false,
        //allowClear: true,
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
  height: auto;
}
</style>
