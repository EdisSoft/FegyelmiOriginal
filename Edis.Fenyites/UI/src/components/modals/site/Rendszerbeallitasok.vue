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
        <h4 class="modal-title ">
          4. Szakterületi vezetők megadása<br />
          <p class="mt-5  mb-0 font-size-12">
            Adja meg a magánál tartható tárgyak korlátozása és intézeti
            programokon való részvétel <br />tiltása és korlátozása esetén
            értesítendő személyeket.
          </p>
        </h4>
      </div>
      <div class="modal-body py-25 pl-25 pr-5">
        <div
          class="vuebar-element modal-scroll"
          v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
        >
          <div>
            <div class="row pr-10">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.LetetesekIds"
                >
                  <k-select2
                    :options="LetetesekSelect2Options"
                    v-model="$v.formData.values.LetetesekIds.$model"
                    class=""
                  >
                  </k-select2>
                  <!-- <k-select2-ajax
                    :options="LetetesekSelect2Options"
                    :defaultValue="formData.letetesekOptions"
                    v-model="$v.formData.values.LetetesekIds.$model"
                    class=""
                  >
                  </k-select2-ajax>-->
                  <span class="text-help float-right">Gazdaságisok</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.RendezvenySzervezokIds"
                >
                  <k-select2
                    :options="RendezvenySzervezokSelect2Options"
                    v-model="$v.formData.values.RendezvenySzervezokIds.$model"
                    class=""
                  >
                  </k-select2>
                  <!-- <k-select2-ajax
                    :options="RendezvenySzervezokSelect2Options"
                    :defaultValue="formData.rendezvenySzervezokOptions"
                    v-model="$v.formData.values.RendezvenySzervezokIds.$model"
                    class=""
                  >
                  </k-select2-ajax> -->
                  <span class="text-help float-right"
                    >Rendezvény szervezők</span
                  >
                </k-vuelidate-error-extractor>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer d-flex">
        <div class="text-right">
          <button type="button" class="btn btn-dark" @click="Hide()">
            Mégsem
          </button>
          <b-button
            type="button"
            class="btn btn-warning"
            @click="Mentes()"
            :disabled="isMentesLoading || buttonsDisabled"
          >
            <b-spinner small v-if="isMentesLoading"></b-spinner>
            Szakterületi vezetők mentése
          </b-button>
        </div>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import { useSimpleModalHandler } from '../../../utils/modal';
import { apiService } from '../../../main';
import { hidrateForm } from '../../../utils/vueUtils';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { sortStr } from '../../../utils/sort';
import { mapGetters } from 'vuex';
import $ from 'jquery';

export default {
  name: 'rendszerbeallitasok',
  data() {
    return {
      isMounted: false,
      isMentesLoading: false,
      title: 'Szakterületi vezetők megadása',
      isFormLoading: false,
      formData: {
        letetesekOptions: [],
        rendezvenySzervezokOptions: [],
        values: {
          LetetesekIds: [],
          RendezvenySzervezokIds: [],
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
  mounted() {
    this.isMounted = true;
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        LetetesekIds: {},
        RendezvenySzervezokIds: {},
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        this.LoadFormData();
      } else {
        this.Hide();
      }
    },
    async LoadFormData() {
      this.isFormLoading = true;
      try {
        let result = await apiService.GetRendszerbeallitasok();

        hidrateForm(this, this.formData.values, result);

        this.formData.letetesekOptions = result.LetetesekOptions;
        this.formData.letetesekOptions.sort(sortStr('text'));

        this.formData.rendezvenySzervezokOptions =
          result.RendezvenySzervezokOptions;
        this.formData.rendezvenySzervezokOptions.sort(sortStr('text'));

        this.$nextTick(() => {
          this.$v.$reset();
        });
        this.isFormLoading = false;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Szakterületi vezetők megadása',
          text: 'Adatok lekérdezése sikertelen',
          errorObj: err,
        });
        console.log(err);
        this.Hide();
      }
    },
    async Mentes() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isMentesLoading = true;
      let result = await this.SaveFormData();
      if (result) {
        this.Hide();
      }
      this.isMentesLoading = false;
    },
    async SaveFormData() {
      try {
        let result = await apiService.SaveRendszerbeallitasok({
          data: { ...this.formData.values },
        });
        NotificationFunctions.SuccessAlert(
          'Szakterületi vezetők megadása',
          'Sikeres mentés'
        );
        return result;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok mentése nem sikerült',
          errorObj: err,
        });
        console.log(err);
      }
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
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),
    // LetetesekSelect2Options: function() {
    //   if (!this.isMounted) {
    //     return;
    //   }
    //   return {
    //     data: this.formData.letetesekOptions,
    //     placeholder: 'Válasszon...',
    //     apiHandler: apiService.FindSzakteruletiVezetokForSelect.bind(
    //       apiService
    //     ),
    //     multiple: true,
    //     dropdownParent: $(this.$el),
    //     minimumInputLength: 5,
    //     readOnly: false,
    //     disabled: false,
    //   };
    // },
    LetetesekSelect2Options: function() {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.letetesekOptions,
        dropdownParent: $(this.$el),
        multiple: true,
      };
    },
    // RendezvenySzervezokSelect2Options: function() {
    //   if (!this.isMounted) {
    //     return;
    //   }
    //   return {
    //     data: this.formData.rendezvenySzervezokOptions,
    //     placeholder: 'Válasszon...',
    //     apiHandler: apiService.FindSzakteruletiVezetokForSelect.bind(
    //       apiService
    //     ),
    //     multiple: true,
    //     dropdownParent: $(this.$el),
    //     minimumInputLength: 5,
    //     readOnly: false,
    //     disabled: false,
    //   };
    // },
    RendezvenySzervezokSelect2Options: function() {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.rendezvenySzervezokOptions,
        dropdownParent: $(this.$el),
        multiple: true,
      };
    },
  },
  components: {},
  watch: {},
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
.modal-footer {
  height: 75px;
}
.modal-scroll {
  height: auto;
}
.vb-content {
  padding-right: 15px;
}
.error input {
  background-image: none !important;
}
</style>
