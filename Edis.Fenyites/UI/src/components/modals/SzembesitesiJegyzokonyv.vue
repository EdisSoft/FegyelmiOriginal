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
          7. Szembesítési jegyzőkönyv rögzítése<br />
          <p class="mt-10 mb-0 font-size-12">
            Kérem töltse ki a jegyzőkönyvet, és írassa alá a
            fogvatartottakkal.<br />
            A kész gombra kattintás után a jegyzőkönyv nem módosítható.
          </p>
        </h4>
      </div>
      <div
        class="modal-body pl-25 pt-25 pb-50 pr-5"
        id="tanu-meghallgatas-modal"
      >
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <!-- <div class="slidePanel-inner-section border-bottom-0 py-15 pr-10"> -->
            <div class="row pr-5">
              <div class="col-xl-12 col-sm-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.SzembesitettFogvatartottIds"
                >
                  <k-select2
                    :options="SzembesitettFogvatartottOptions"
                    v-model="
                      $v.formData.values.SzembesitettFogvatartottIds.$model
                    "
                    class=""
                    placeholder="Válassza ki a szembesített fogvatartottakat"
                  >
                  </k-select2>
                  <span class="text-help float-right">
                    Szembesített fogvatartottak
                  </span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.MeghallgatoSid"
                >
                  <k-select2
                    :options="Select2MeghallgatoOptions"
                    v-model="$v.formData.values.MeghallgatoSid.$model"
                    class=""
                    placeholder="Válassza ki a meghallgalót"
                  >
                  </k-select2>
                  <span class="text-help float-right">Meghallgató</span>
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
                    placeholder="A szembesítésen elhangzottak..."
                  />
                  <!-- <k-summernote
                    v-model="$v.formData.values.Leiras.$model"
                    :settings="{
                      placeholder: 'A szembesítésen elhangzottak...',
                    }"
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
import minLength from 'vuelidate/lib/validators/minLength';
import maxLength from 'vuelidate/lib/validators/maxLength';
import { hidrateForm } from '../../utils/vueUtils';
export default {
  name: 'szembesitesi-jegyzokonyv',
  data: function() {
    return {
      isFormLoading: false,
      title: 'Szembesítési jegyzőkönyv',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      ujSzembesitettFogvatartottIds: 0,
      isLoadingNyomtatas: false,
      isLoading: false,
      isMounted: false,
      formData: {
        szembesitettFogvatartottOptions: [],
        meghallgatoOptions: [],
        jegyzokonyvVezetoOptions: [],
        tovabbiSzemelyekOptions: [],
        values: {
          Leiras: '',
          SzembesitettFogvatartottIds: [],
          TovabbiSzemelyekList: [],
          JegyzokonyvVezetoSid: '',
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
        SzembesitettFogvatartottIds: {
          required,
          minFogvatartott: minLength(2),
          maxFogvatartott: maxLength(2),
        },
        MeghallgatoSid: {
          required,
        },
        JegyzokonyvVezetoSid: {},
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
        this.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        this.naplobejegyzesIds = data.naplobejegyzesIds || [];
        this.LoadFegyelmiUgyData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        console.log('fegyelmiUgyIds:' + fegyelmiUgyIds);
        var result = await apiService.GetSzembesitesiJegyzokonyvModalData({
          data: { fegyelmiUgyIds, naplobejegyzesIds },
        });
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
        this.formData.szembesitettFogvatartottOptions = result.SzembesitettFogvatartottOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );
        this.formData.meghallgatoOptions = result.MeghallgatoOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );
        hidrateForm(this, this.formData.values, result);
        //this.Show();
        this.$v.$reset();
        this.key++;
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
      if (result) {
        NyomtatvanyFunctions.SzembesitesiJegyzokonyvNyomtatas({
          naplobejegyzesIds: this.formData.values.NaplobejegyzesIds,
        });
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
    SaveEsemeny: async function(fogvatartottAlairta) {
      var result = null;
      try {
        result = await apiService.SaveSzembesitesiJegyzokonyvModalData({
          data: {
            FegyelmiUgyIds: this.fegyelmiUgyIds,
            NaplobejegyzesIds: this.naplobejegyzesIds,
            ...this.formData.values,
            AlairtaFl: fogvatartottAlairta,
          },
        });

        this.formData.values.NaplobejegyzesIds = result.naplobejegyzesIds;
        if (fogvatartottAlairta) {
          NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: this.title,
          text: 'Jegyzőkönyv mentése sikertelen',
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
    SzembesitettFogvatartottOptions: function() {
      return {
        data: this.formData.szembesitettFogvatartottOptions,
        multiple: true,
        dropdownParent: $(this.$el),
      };
    },
    Select2MeghallgatoOptions: function() {
      return {
        data: this.formData.meghallgatoOptions,
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
  },
  destroyed: function() {},
};
</script>
<style scoped>
.modal-scroll {
  height: 330px;
}
.vb-content {
  padding-right: 20px !important;
}
</style>
