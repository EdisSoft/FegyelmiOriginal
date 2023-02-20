<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-grey">
      <div class="modal-header bg-blue-grey-400">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title">
          36. Fegyelmi elkülönítés végrehajtása
          <p class="mt-10 mb-0 font-size-12">
            Adjuk meg az elkülönítés zárkáját, és a behelyezés idejét.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material static-select2"
                :validator="$v.formData.values.ElrendeloId"
              >
                <k-select2
                  :options="select2ElrendeloOptions"
                  v-model="$v.formData.values.ElrendeloId.$model"
                  class=""
                  placeholder="Elrendelő"
                >
                </k-select2>
                <span class="text-help float-right"
                  >Fő felügyelő / Szociális segégelőadó</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.ZarkabaHelyezes"
              >
                <treeselect
                  v-model="$v.formData.values.ZarkabaHelyezes.$model"
                  :multiple="false"
                  :disable-branch-nodes="true"
                  :clearable="false"
                  placeholder="Válasszon..."
                  v-bind="treeselectHun"
                  :options="formData.zarkabaHelyezesOptions"
                  @open="OpenTreeselect"
                  @close="DestroyTreeselect"
                />
                <span class="text-help float-right">Zárkába helyezés</span>
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
        @click="Elkulonites()"
        :disabled="elkulonitesLoading || buttonsDisabled"
      >
        <b-spinner small v-if="elkulonitesLoading"></b-spinner>
        Zárkába helyezés
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../../main';
import { eventBus } from '../../../main';
import { FegyelmiUgyStoreTypes } from '../../../store/modules/fegyelmiugy';
import { required, minValue, maxLength } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../../functions/notificationFunctions';

import { hidrateForm } from '../../../utils/vueUtils';
import { deselectDatatable } from '../../../utils/common';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';
import $ from 'jquery';
import { sortStr } from '../../../utils/sort';
import moment from 'moment';
import minLength from 'vuelidate/lib/validators/minLength';
import { treeselectHun } from '../../../plugins/vueTreeSelect';
import { useSimpleModalHandler } from '../../../utils/modal';

export default {
  name: 'fegyelmi-elkulonites-vegrehajtasa',
  data() {
    return {
      isFormLoading: false,
      title: 'Fegyelmi elkülönítés végrehajtása',
      fegyelmiUgyIds: [],
      treeselectHun,
      naplobejegyzesIds: [],
      elkulonitesLoading: false,
      isMounted: false,
      formData: {
        elrendeloOptions: [],
        zarkabaHelyezesOptions: [],
        values: {
          ElrendeloId: null,
          ZarkabaHelyezes: null,
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
        ElrendeloId: {
          required,
        },
        ZarkabaHelyezes: {
          required,
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

    OpenTreeselect: function() {
      // this.$nextTick(() => {
      //   $('.vue-treeselect__menu').slimScroll({
      //     minHeight: '65px',
      //     Height: '65px',
      //     maxHeight: '100px',
      //     wheelStep: 5,
      //     color: '#8349f5',
      //     position: 'right',
      //     distance: '5px',
      //   });
      // });
    },

    DestroyTreeselect: function() {
      //$('.vue-treeselect__menu').slimScroll({ destroy: true });
    },
    LoadFormData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFegyelmiElkulonitesVegrehajtasaModalData(
          {
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          }
        );
        console.log(result);

        hidrateForm(this, this.formData.values, result);
        this.formData.zarkabaHelyezesOptions = result.ZarkabaHelyezesOptions;
        this.formData.elrendeloOptions = result.ElrendeloOptions.sort(
          sortStr('text')
        );
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
        var result = await apiService.SaveFegyelmiElkulonitesVegrehajtasaModalData(
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
          text: err.responseText.title,
          errorObj: err,
        });
        console.log(errorMsg);
      }
    },
    async Elkulonites() {
      if (!this.IsFormValid()) {
        return;
      }
      this.elkulonitesLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.elkulonitesLoading = false;
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

    select2ElrendeloOptions: function() {
      return {
        data: this.formData.elrendeloOptions,
        dropdownParent: $(this.$el),
        readOnly: true,
        disabled: true,
      };
    },
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
