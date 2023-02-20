<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-primary" id="elkulonitesModal">
      <div class="modal-header bg-blue-grey-400">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title">
          35. Fegyelmi elkülönítés elrendelése felülvizsgálata és megszüntetése
          <p class="mt-10 mb-0 font-size-12">
            Adjuk meg az elrendelés idejét, a felülvizsgálatot, és a
            megszüntetés idejét
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.ElrendeloId"
              >
                <k-select2
                  :options="select2ElrendeloOptions"
                  v-model="$v.formData.values.ElrendeloId.$model"
                  class=""
                  placeholder="Elrendelő"
                  :disabled="isMezoNemModosithato"
                >
                </k-select2>
                <span class="text-help float-right"
                  >Elkülönítés elrendelője</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.ElkulonitesOka"
              >
                <textarea-autosize
                  v-model="$v.formData.values.ElkulonitesOka.$model"
                  :min-height="30"
                  class="w-p100 mb-0"
                  name="ElkulonitesOka"
                  :rows="1"
                  :disabled="isMezoNemModosithato"
                />
                <!-- <k-summernote v-model="$v.formData.values.ElkulonitesOka.$model"
                              :settings="{
                    placeholder: 'Írja be az elkülönítés okát...',
                  }"
                              class="mb-0"
                              name="ElkulonitesOka"
                              :disabled="isMezoNemModosithato"></k-summernote> -->
                <span class="text-help float-right">Elkülönítés oka</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.ElrendelesIdeje"
              >
                <date-picker
                  v-model="$v.formData.values.ElrendelesIdeje.$model"
                  :config="elrendelesIdejeDatePickerOptions"
                  :disabled="isMezoNemModosithato"
                >
                </date-picker>
                <span class="text-help float-right"
                  >Elkülönítés elrendelés ideje</span
                >
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material summernote"
                :validator="$v.formData.values.Rendelkezesek"
              >
                <textarea-autosize
                  v-model="$v.formData.values.Rendelkezesek.$model"
                  :min-height="30"
                  class="w-p100 mb-0"
                  name="ElkulonitesOka"
                  :rows="1"
                  :disabled="isZarkabaHelyezesNemModosithato"
                />
                <span class="text-help float-right"
                  >Elkülönítésre vonatkozó rendelkezések</span
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
                  placeholder="Válasszon..."
                  v-bind="treeselectHun"
                  :options="formData.zarkabaHelyezesOptions"
                  :disabled="isZarkabaHelyezesNemModosithato"
                  class="treeselect-scroll"
                  @open="OpenTreeselect"
                  @close="DestroyTreeselect"
                />
                <span class="text-help float-right">Zárkába helyezés</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-12">
              <div class="checkbox-custom">
                <input
                  type="checkbox"
                  id="inputUnchecked"
                  v-model="formData.values.IsFelulvizsgalva"
                  :disabled="isZarkabaHelyezesNemModosithato"
                />
                <label
                  for="inputUnchecked"
                  class="font-weight-400 d-inline-flex align-items-end"
                >
                  <span class="text-help my-0 ml-10"
                    >Elkülönítést felülvizsgáltam</span
                  >
                </label>
              </div>
            </div>
            <div class="col-md-12" :key="key">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.MegszuntetesIdeje"
              >
                <date-picker
                  v-model="$v.formData.values.MegszuntetesIdeje.$model"
                  :config="megszuntetesIdejeDatePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right"
                  >Elkülönítés megszüntetésének ideje</span
                >
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
        id="mentesBtn"
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="IsZarkaValtozott()"
        :disabled="eljarasKezdemenyezeseLoading || buttonsDisabled"
      >
        <b-spinner small v-if="eljarasKezdemenyezeseLoading"></b-spinner>
        <span v-if="isMegszuntetes"> Megszüntetés rögzítése </span>
        <span v-else> Elkülönítés rögzítése </span>
        <b-popover
          target="mentesBtn"
          triggers="null"
          :show.sync="isShowPopup"
          placement="topleft"
          container="elkulonitesModal"
          ref="confirmPopover"
          custom-class="ujResztvevoPopover"
        >
          <template slot="title"> Megerősítés </template>
          <div class="pb-5">
            <div
              class="form-group form-material mb-10"
              data-plugin="formMaterial"
            >
              Biztosan nem kerül másik zárkába a fogvatartott?
            </div>
            <div class="text-right">
              <b-button
                size="sm"
                @click="ClosePopup()"
                variant="default"
                class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700 mr-5"
                >Nem</b-button
              >
              <b-button
                size="sm"
                @click="Megallapodas()"
                variant="warning"
                class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
                >Igen</b-button
              >
            </div>
          </div>
        </b-popover>
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { useSimpleModalHandler } from '../../../utils/modal';
import { eventBus, apiService } from '../../../main';
import { required, maxLength, minValue } from 'vuelidate/lib/validators';
import moment from 'moment';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { deselectDatatable } from '../../../utils/common';
import { mapGetters } from 'vuex';
import { hidrateForm } from '../../../utils/vueUtils';
import $ from 'jquery';
import { sortStr } from '../../../utils/sort';
import { treeselectHun } from '../../../plugins/vueTreeSelect';

export default {
  name: 'fegyelmi-elkulonites-elrendelese',
  data() {
    return {
      isFormLoading: false,
      title: 'Fegyelmi elkülönítés',
      fegyelmiUgyIds: [],
      naplobejegyzesIds: [],
      treeselectHun,
      eljarasKezdemenyezeseLoading: false,
      isMounted: false,
      isMezoNemModosithato: false,
      isZarkabaHelyezesNemModosithato: false,
      isMegszuntetes: false,
      isShowPopup: false,
      key: 1,
      formData: {
        maxDatum: null,
        elrendeloOptions: [],
        zarkabaHelyezesOptions: [],
        ElrendelesIdeje: null,
        zarkaString: '',
        values: {
          ZarkabaHelyezes: null,
          ElrendeloId: null,
          ElkulonitesOka: '',
          ElkulonitesOkaSzerkesztheto: true,
          ElrendelesIdeje: null,
          ElrendelesIdejeSzerkesztheto: true,
          Rendelkezesek: '',
          IsFelulvizsgalva: true,
          MegszuntetesIdeje: null,
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
    this.isMounted = true;
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations() {
    let form = {
      formData: {
        values: {
          ElrendeloId: {
            required,
          },
          ZarkabaHelyezes: {},
          ElkulonitesOka: {
            required,
            maxLength: maxLength(2000),
          },
          ElrendelesIdeje: {
            required,
          },
          ElrendelesIdejeSzerkesztheto: true,
          Rendelkezesek: {
            maxLength: maxLength(2000),
          },
          IsFelulvizsgalva: true,
          MegszuntetesIdeje: {},
        },
      },
    };
    // if (
    //   moment(this.formData.values.ElrendelesIdeje).isSameOrAfter(
    //     this.formData.values.MegszuntetesIdeje
    //   )
    // ) {
    form.formData.values.MegszuntetesIdeje = {
      nagyobbMintElrendeles: (v) =>
        !this.formData.values.ElrendelesIdeje ||
        moment(
          this.formData.values.ElrendelesIdeje,
          'YYYY.MM.DD HH:mm'
        ).isSameOrBefore(
          moment(this.formData.values.MegszuntetesIdeje, 'YYYY.MM.DD HH:mm')
        ) ||
        !this.formData.values.MegszuntetesIdeje,
    };
    // }
    return form;
  },
  methods: {
    onInput(id) {
      this.$nextTick(() => {
        $('.vue-treeselect__menu').slimScroll();
      });
      console.log($(this));
    },
    InitEvents: function ({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds || [];
        this.naplobejegyzesIds = data.naplobejegyzesIds || [];
        this.LoadFormData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    ClosePopup() {
      this.isShowPopup = false;
    },
    OpenTreeselect: function () {
      // this.$nextTick(() => {
      //   $('.vue-treeselect__menu').slimScroll({
      //     auto: 'auto',
      //     wheelStep: 5,
      //     color: '#8349f5',
      //     position: 'right',
      //     distance: '5px',
      //   });
      // });
    },

    DestroyTreeselect: function () {
      // $('.vue-treeselect__menu').slimScroll({ destroy: true });
    },

    LoadFormData: async function (fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result =
          await apiService.GetFegyelmiElkulonitesElrendeleseModalData({
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          });
        this.formData.zarkaString = result.ZarkabaHelyezes;
        console.log(result);
        hidrateForm(this, this.formData.values, result);

        this.formData.elrendeloOptions = result.ElrendeloOptions.sort(
          sortStr('text')
        );

        if (result.ZarkabaHelyezes) {
          var elhelyezes = result.ZarkabaHelyezes.split('_');
          var id;
          var elhelyezesNev = [
            result.ZarkabaHelyezesObjektumNev,
            result.ZarkabaHelyezesReszlegNev,
            result.ZarkabaHelyezesZarkaNev,
            result.ZarkabaHelyezesObjektumNev +
              '/' +
              result.ZarkabaHelyezesReszlegNev +
              '/' +
              result.ZarkabaHelyezesZarkaNev +
              '/' +
              elhelyezes[3],
          ];
          var list = result.ZarkabaHelyezesOptions;

          for (var i = 0; i <= 3; i++) {
            id = i == 0 ? elhelyezes[0] : id + '_' + elhelyezes[i];

            var data = list.find((x) => x.id == id);
            if (!data) {
              data = {
                id: id,
                label: elhelyezesNev[i],
              };
              if (i < 3) data.children = [];
              list.push(data);
              list = list.sort(sortStr('label'));
            }
            list = data.children;
          }
        }

        this.formData.zarkabaHelyezesOptions = result.ZarkabaHelyezesOptions;
        this.formData.values.ElrendelesIdeje = this.formData.values
          .ElrendelesIdeje
          ? moment(this.formData.values.ElrendelesIdeje).format(
              'YYYY.MM.DD HH:mm'
            )
          : null;
        this.formData.ElrendelesIdeje = result.ElrendelesIdeje
          ? moment(result.ElrendelesIdeje).format('YYYY.MM.DD HH:mm')
          : null;
        this.formData.values.MegszuntetesIdeje = this.formData.values
          .MegszuntetesIdeje
          ? moment(this.formData.values.MegszuntetesIdeje).format(
              'YYYY.MM.DD HH:mm'
            )
          : null;
        this.isFormLoading = false;
        if (result.ElkulonitesOka != null && result.ElkulonitesOka != '') {
          this.isMezoNemModosithato = true;
        }
        if (result.MegszuntetesIdeje != null) {
          this.isZarkabaHelyezesNemModosithato = true;
        }
      } catch (err) {
        console.log(err);

        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });

        this.Hide();
      }
      this.$v.$reset();
    },
    SaveFormData: async function (nocheckVegrehajtasFok = false) {
      try {
        var result =
          await apiService.SaveFegyelmiElkulonitesElrendeleseModalData({
            data: {
              ...this.formData.values,
              FegyelmiUgyIds: this.fegyelmiUgyIds,
              NaplobejegyzesIds: this.naplobejegyzesIds,
              NocheckVegrehajtasiFok: nocheckVegrehajtasFok,
            },
          });

        if (
          result &&
          result.Status == 3 &&
          result.Details &&
          result.Details.Kerdes
        ) {
          var behelyez = await NotificationFunctions.ConfirmModal(
            result.Message
          );
          if (behelyez) await this.SaveFormData(true);
          return;
        }

        this.naplobejegyzesIds = result.naplobejegyzesIds;

        NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        eventBus.$emit('Sidebar:ugyReszletek:refresh');

        this.$v.$reset();
        return true;
      } catch (err) {
        console.log(err);
        NotificationFunctions.AjaxError({
          title: this.title,
          text: 'Hiba a mentés során',
          errorObj: err,
        });
      }
    },
    IsZarkaValtozott: function () {
      if (
        this.formData.values.ZarkabaHelyezes == this.formData.zarkaString &&
        this.formData.values.MegszuntetesIdeje != null
      ) {
        this.isShowPopup = true;
      } else {
        this.Megallapodas();
      }
    },
    async Megallapodas() {
      if (!this.IsFormValid()) {
        return;
      }
      this.isShowPopup = false;
      this.eljarasKezdemenyezeseLoading = true;
      var success = await this.SaveFormData();
      if (success) {
        deselectDatatable();
        this.Hide();
      }
      this.eljarasKezdemenyezeseLoading = false;
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
    select2ElrendeloOptions: function () {
      return {
        data: this.formData.elrendeloOptions,
        dropdownParent: $(this.$el),
      };
    },
    elrendelesIdejeDatePickerOptions() {
      var datumOptions = {
        format: 'YYYY.MM.DD HH:mm',
        useCurrent: false,
        //defaultDate: this.formData.values.ElrendelesIdeje || moment(),
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        minDate: moment().add({ d: -3 }).startOf('d'),
        maxDate: moment().add({ d: 1 }).endOf('d'),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };

      if (this.isMezoNemModosithato && this.formData.ElrendelesIdeje != null) {
        datumOptions.minDate = moment(
          this.formData.ElrendelesIdeje,
          'YYYY.MM.DD HH:mm'
        ).startOf('d');
        datumOptions.defaultDate = this.formData.ElrendelesIdeje;
      }

      return datumOptions;
    },
    //asd
    megszuntetesIdejeDatePickerOptions() {
      return {
        format: 'YYYY.MM.DD HH:mm',
        useCurrent: false,
        //defaultDate: megszunDate ? moment(megszunDate) : moment(),
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        minDate: moment().startOf('d'),
        maxDate: moment().add({ d: 1 }).endOf('d'),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
  },
  watch: {
    id: {
      handler: function (newValue, oldValue) {},
      deep: true,
      immediate: true,
    },
    'formData.values.MegszuntetesIdeje': function (value) {
      if (value != null || value != undefined) {
        this.isMegszuntetes = true;
      } else {
        this.isMegszuntetes = false;
      }
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
