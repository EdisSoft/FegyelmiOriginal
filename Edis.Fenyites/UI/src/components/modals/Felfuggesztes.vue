<template>
  <basic-loader :isLoading="isFormLoading">
    <div>
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
            31. Felfüggesztés
            <p class="mt-10 mb-0 font-size-12">
              A felfüggesztett ügy határideje az ügy indításától számított 6
              hónap
            </p>
          </h4>
        </div>
        <div class="modal-body px-25 pt-20 pb-40">
          <div>
            <div class="row">
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.FelfuggesztesOkaCimkeId"
                >
                  <k-select2
                    :options="felfuggesztesOkaOptions"
                    v-model="$v.formData.values.FelfuggesztesOkaCimkeId.$model"
                    class=""
                  >
                  </k-select2>
                  <span class="text-help float-right">Felfüggesztés oka</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12 text-secondary">
                <h2 class="font-size-14 my-0">
                  A 14/2014 IM rendelet szerint a fegyelmi eljárást fel kell
                  függeszteni,
                </h2>
                <div class="mx-4 mt-10">
                  <p class="mb-10 font-size-12">
                    a) ha a fegyelmi eljárás alá vont fogvatartott egészségi
                    állapota miatt az eljárás nem folytatható, vagy a személyes
                    meghallgatása nem lehetséges,
                  </p>
                  <p class="mb-10 font-size-12">
                    b) ha a beszámítási képesség megítélése tárgyában szakértői
                    vizsgálat szükséges, a szakvélemény elkészítéséig,
                  </p>
                  <p class="mb-10 font-size-12">
                    c) ha a cselekmény elbírálása olyan előzetes kérdéstől függ,
                    amelynek eldöntése bíróság vagy más hatóság hatáskörébe
                    tartozik, annak a döntéséig,
                  </p>
                  <p class="mb-10 font-size-12">
                    d) közvetítői eljárásra utalás esetén,
                  </p>
                  <p class="mb-10 font-size-12">
                    e) a 8. § (4) bekezdésében írt megkeresés esetén,
                  </p>
                  <p class="mb-10 font-size-12">
                    f) a fogvatartott jogellenes távollétének idejére.
                  </p>
                </div>
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
          @click="Rogzites()"
          :disabled="rogzitesLoading || buttonsDisabled"
        >
          <b-spinner small v-if="rogzitesLoading"></b-spinner>
          Felfüggesztés rögzítése
        </button>
      </div>
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
import $ from 'jquery';
import moment from 'moment';
import { sortStr } from '../../utils/sort';
import { deselectDatatable } from '../../utils/common';

export default {
  name: 'felfuggesztes',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      title: 'Felfüggesztés',
      rogzitesLoading: false,
      isMounted: false,
      formData: {
        felfuggesztesOkaOptions: [],
        values: {
          FelfuggesztesOkaCimkeId: null,
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
        FelfuggesztesOkaCimkeId: {
          required,
          minValueSelect: minValue(1),
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
        console.log(fegyelmiUgyIds);
        var result = await apiService.GetFelfuggesztesModalData({
          data: { fegyelmiUgyIds: fegyelmiUgyIds },
        });
        this.formData.felfuggesztesOkaOptions = result.FelfuggesztesOkaOptions.sort(
          sortStr('text')
        );
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
        var result = await apiService.SaveFelfuggesztesModalData({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
          },
        });
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert('Felfüggesztés', 'Sikeres mentés');
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        this.Hide();
        this.$v.$reset();
        deselectDatatable();
        return true;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Felfüggesztés',
          text: 'Hiba történt a mentés során',
          errorObj: err,
        });
        console.log(err);
      }
    },
    async Rogzites() {
      if (!this.IsFormValid()) {
        return;
      }
      this.rogzitesLoading = true;
      await this.SaveFormData();
      this.rogzitesLoading = false;
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
    felfuggesztesOkaOptions: function() {
      return {
        data: this.formData.felfuggesztesOkaOptions,
        dropdownParent: $(this.$el),
        placeholder: 'Kérem válassza ki a felfüggesztés okát...',
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
