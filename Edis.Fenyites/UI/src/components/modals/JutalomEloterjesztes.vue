<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-grey" id="esemenyRogziteseModal">
      <div class="modal-header">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title">
          1. {{ title }}<br />
          <p class="mt-10 mb-0 font-size-12">
            Írja le az előterjesztés okát, és adja meg a résztvevőket.
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
                  :validator="$v.formData.values.JavaslatTevoId"
                >
                  <k-select2
                    :options="javaslatTevoOptions"
                    v-model="$v.formData.values.JavaslatTevoId.$model"
                    class=""
                  >
                  </k-select2>
                  <span class="text-help float-right">Javaslat tevő</span>
                </k-vuelidate-error-extractor>
              </div>

              <div class="row align-items-end mx-0" style="width: 100%;">
                <div class="col-md-12 longtext-fix">
                  <k-vuelidate-error-extractor
                    class="form-group form-material"
                    :validator="$v.formData.values.JutalmazasOkaId"
                  >
                    <k-select2
                      :options="jutalmazasOkaOptions"
                      v-model="$v.formData.values.JutalmazasOkaId.$model"
                      class=""
                    >
                    </k-select2>
                    <span class="text-help float-right">Jutalmazás oka</span>
                  </k-vuelidate-error-extractor>
                </div>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material summernote"
                  :validator="$v.formData.values.Leiras"
                >
                  <textarea-autosize
                    v-if="!id"
                    ref="myTextarea"
                    v-model="$v.formData.values.Leiras.$model"
                    :min-height="30"
                    placeholder="Írja le mivel indokolja az előterjesztést..."
                    class="w-p100 mb-0 form-control"
                    :rows="1"
                    name="leiras"
                  />
                  <!-- <textarea
                    v-if="!id"
                    class="form-control mb-0"
                    v-model="$v.formData.values.Leiras.$model"
                    name="leiras"
                    rows="3"
                  ></textarea> -->
                  <!-- <k-summernote
                    v-if="!id"
                    v-model="$v.formData.values.Leiras.$model"
                    name="leiras"
                    class="mb-0"
                  ></k-summernote> -->
                  <div
                    class="unique-desc"
                    v-else
                    v-html="$v.formData.values.Leiras.$model"
                  ></div>
                  <span class="text-help float-right">Leírás</span>
                </k-vuelidate-error-extractor>
                <!-- <k-vuelidate-error-extractor
                  class="form-group form-material ckeditor"
                  :validator="$v.formData.values.Leiras"
                >
                  <k-ckeditor
                    v-if="!id"
                    v-model="$v.formData.values.Leiras.$model"
                    name="leiras"
                    class="mb-0"
                  ></k-ckeditor>
                  <div
                    class="unique-desc"
                    v-else
                    v-html="$v.formData.values.Leiras.$model"
                  ></div>
                  <span class="text-help float-right">Leírás</span>
                </k-vuelidate-error-extractor> -->
              </div>
              <div class="col-md-12">
                <div class="checkbox-custom">
                  <input
                    type="checkbox"
                    id="esemeny-kellIntezet"
                    v-model="kellIntezet"
                  />
                  <label
                    for="esemeny-kellIntezet"
                    class="font-weight-400 d-inline-flex align-items-end"
                    ><span class="text-help my-0 ml-10">
                      Fogvatartott kiválasztása ebből az intézetből
                    </span>
                  </label>
                </div>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.JutalmazandoFogvatartottIds"
                >
                  <k-select2-ajax
                    :options="jutalmazandoFogvatartottOptions"
                    :defaultValue="formData.JutalmazandoFogvatartottakDefault"
                    :additionalParams="additionalParams"
                    v-model="
                      $v.formData.values.JutalmazandoFogvatartottIds.$model
                    "
                  >
                  </k-select2-ajax>

                  <span class="text-help float-right">
                    Jutalmazandó fogvatartottak
                    <span v-if="kellIntezet"> az intézetemben</span> (több
                    személy kiválasztása: Ctrl + klikk a listában)
                  </span>
                </k-vuelidate-error-extractor>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer d-flex justify-content-between">
        <div class="modalorszam">
          <span v-if="id">{{ id }}</span>
        </div>
        <div class="text-right">
          <button type="button" class="btn btn-dark" @click="Hide()">
            Mégsem
          </button>
          <b-button
            id="mentesBtn"
            type="button"
            class="btn btn-warning"
            @click="Mentes()"
            :disabled="isMentesLoading || buttonsDisabled"
          >
            <b-spinner small v-if="isMentesLoading"></b-spinner>
            <span class="ml-3">Mentés és jutalmak előterjesztése</span>
          </b-button>
        </div>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { required, minValue, minLength } from 'vuelidate/lib/validators';
import moment from 'moment';
import $ from 'jquery';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { hidrateForm } from '../../utils/vueUtils';
import { useModalHandler, useSimpleModalHandler } from '../../utils/modal';
import { ConfirmModalFunctions } from '../../functions/ConfirmModalFunctions';

export default {
  name: 'jutalom-eloterjesztes',
  data() {
    return {
      id: 0,
      isMounted: false,
      isMentesLoading: false,
      isFormLoading: false,
      kellIntezet: true,
      formData: {
        JavaslatTevoOptions: [],
        JutalmazasOkaOptions: [],
        values: {
          JavaslatTevoId: null,
          JutalmazasOkaId: null,
          Leiras: '',
          JutalmazandoFogvatartottIds: [],
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
        JavaslatTevoId: {
          required,
        },
        JutalmazasOkaId: {
          required,
        },
        Leiras: {
          required,
        },
        JutalmazandoFogvatartottIds: {
          required,
        },
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
        let result = await apiService.GetJutalomEloterjesztesModalData({
          mock: true,
        });

        hidrateForm(this, this.formData.values, result);
        this.formData.JavaslatTevoOptions = result.JavaslatTevoOptions;
        this.formData.JutalmazasOkaOptions = result.JutalmazasOkaOptions;
        await this.$nextTick();
        this.$v.$reset();
        this.isFormLoading = false;
      } catch (err) {
        console.log(err);
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok lekérdezése sikertelen',
          errorObj: err,
        });

        this.Hide();
      }
    },
    IsFormValid() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        this.$nextTick(() => {
          var element = this.$el.querySelector('.form-group.error:first-child');
          if (element) {
            element.scrollIntoView();
          }
        });
        return false;
      }
      return true;
    },
    async Mentes() {
      if (!this.IsFormValid()) {
        return;
      }
      let fogvatartasIds = this.formData.values.JutalmazandoFogvatartottIds;
      let success = await ConfirmModalFunctions.OpenNyitottFegyelmiVagyJutalomConfirmModal(
        fogvatartasIds
      );
      if (!success) {
        return;
      }
      this.isMentesLoading = true;
      try {
        let data = { ...this.formData.values };
        console.log('Mentés...', data);
        let result = await apiService.SaveJutalomEloterjesztesModalData({
          data: data,
          mock: true,
        });
        NotificationFunctions.SuccessAlert(this.title, result.message);
        this.Hide();
      } catch (err) {
        console.log(err);
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
      }
      this.isMentesLoading = false;
    },
  },
  computed: {
    ...mapGetters({}),

    title() {
      return 'Előterjesztés';
    },

    javaslatTevoOptions() {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.JavaslatTevoOptions,
        dropdownParent: $(this.$el),
      };
    },
    jutalmazasOkaOptions: function() {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.JutalmazasOkaOptions,
        dropdownParent: $(this.$el),
      };
    },
    jutalmazandoFogvatartottOptions() {
      if (!this.isMounted) {
        return;
      }

      return {
        placeholder: 'Előterjesztett neve',
        apiHandler: apiService.FindFogvatartottakForSelectJutalom.bind(
          apiService
        ),
        multiple: true,
        dropdownParent: $(this.$el),
        minimumInputLength: 4,
      };
    },

    additionalParams() {
      return {
        kellIntezet: this.kellIntezet,
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
  height: calc(100vh - 350px);
}
.vb-content {
  padding-right: 15px;
}
.error input {
  background-image: none !important;
}
</style>
