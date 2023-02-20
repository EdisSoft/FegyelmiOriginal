<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-primary">
      <div class="modal-header bg-blue-grey-400">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title">
          21. Fenyítés nem végrehajtható
          <p class="mt-10 mb-0 font-size-12">
            Egészségügyi ok miatt a magánelzárás végrehajthatatlan, vagy
            közvetítőin a felek megegyeztek.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
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
      <button
        type="button"
        class="btn bg-blue-grey-600 white mb-lg-5"
        data-dismiss="modal"
        @click="Hide()"
      >
        Mégsem
      </button>
      <button
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="FenyitesVegrehajthatatlannaTetele()"
        :disabled="mentesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="mentesLoading"></b-spinner>
        Fenyítés nem hajtható végre
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { required, minValue, maxLength } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';
// import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import $ from 'jquery';
import { sortStr } from '../../utils/sort';
import moment from 'moment';
import minLength from 'vuelidate/lib/validators/minLength';

export default {
  name: 'fenyites-vegrehajthatatlanna-tetele',
  data() {
    return {
      isFormLoading: false,
      title: 'Fenyítés nem végrehajtható',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      mentesLoading: false,
      isMounted: false,
      formData: {
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
    this.isMounted = true;
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        Leiras: {
          required,
          maxLength: maxLength(4000),
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        this.naplobejegyzesIds = data.naplobejegyzesIds || [];
        this.LoadFormData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFenyitesVegrehajthatatlannaTeteleModalData(
          {
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          }
        );
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        console.log(result);

        hidrateForm(this, this.formData.values, result);
        this.isFormLoading = false;
      } catch (err) {
        console.log(err);
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

      this.$v.$reset();
    },
    SaveFormData: async function() {
      try {
        var result = await apiService.SaveFenyitesVegrehajthatatlannaTeteleModalData(
          {
            data: {
              ...this.formData.values,
              FegyelmiUgyIds: this.fegyelmiUgyIds,
              NaplobejegyzesIds: this.naplobejegyzesIds,
            },
          }
        );
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        eventBus.$emit('Sidebar:ugyReszletek:refresh');

        this.$v.$reset();
        return true;
      } catch (err) {
        var errorMsg = 'Hiba a mentés során: ' + err;
        NotificationFunctions.AjaxError({
          title: this.title,
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
        console.log(errorMsg);
      }
    },
    async FenyitesVegrehajthatatlannaTetele() {
      if (!this.IsFormValid()) {
        return;
      }
      this.mentesLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.mentesLoading = false;
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
    ...mapGetters({}),
  },
  watch: {
    id: {
      handler: function(newValue, oldValue) {},
      deep: true,
      immediate: true,
    },
  },
  components: {},
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

.close {
  opacity: 1 !important;
}

.checkbox-custom input[type='checkbox'],
.checkbox-custom input[type='radio'],
.checkbox-custom label::before,
.checkbox-custom label::after {
  width: 32px;
  height: 32px;
  top: 0;
}

.checkbox-custom label::after {
  line-height: 32px;
  font-size: 16px;
}
.checkbox-custom label {
  min-height: 32px;
}
</style>
