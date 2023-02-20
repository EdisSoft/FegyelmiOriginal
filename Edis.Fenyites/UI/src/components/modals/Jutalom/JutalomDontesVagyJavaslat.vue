<template>
  <basic-loader :isLoading="isFormLoading">
    <div class="modal-primary" id="dontesVagyJavaslat">
      <div class="modal-header bg-blue-grey-400">
        <button
          type="button"
          class="close icon wb-close text-white"
          data-dismiss="modal"
          aria-label="Close"
          @click="Hide()"
        ></button>
        <h4 class="modal-title">
          2. {{ title }}
          <p class="mt-10 mb-0 font-size-12">
            Döntés saját jögkörben, illetve javaslat tétel más jögkörben
            elérhető jutalomra.
          </p>
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.JutalomTipusId"
              >
                <k-select2
                  :options="jutalomTipusokOptions"
                  :defaultValue="formData.jutalomTipusokDefault"
                  v-model="$v.formData.values.JutalomTipusId.$model"
                  class=""
                >
                </k-select2>
                <span class="text-help float-right"
                  >Döntés vagy javaslat a jutalom típusára</span
                >
              </k-vuelidate-error-extractor>
            </div>

            <div class="col-md-12" v-show="isJutalomTipusaFenyitesTorles">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.FegyelmiUgyId"
              >
                <k-select2
                  :options="fegyelmiUgyOptions"
                  :defaultValue="formData.fegyelmiUgyDefault"
                  v-model="$v.formData.values.FegyelmiUgyId.$model"
                >
                </k-select2>

                <span class="text-help float-right">
                  Fogvatartott fegyelmi ügyei
                </span>
              </k-vuelidate-error-extractor>
            </div>

            <div
              :class="['col-md-' + isJutalomHosszaMezoSzelesseg]"
              v-show="isJutalomTipusaSpejzNoveles || isKondiDijmHaszn"
            >
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.JutalomHossz"
              >
                <k-select2
                  :defaultValue="formData.jutalomHosszDefault"
                  :options="Select2JutalomHosszOptions"
                  v-model="formData.values.JutalomHossz"
                ></k-select2>
                <span class="text-help float-right">Jutalom tartama</span>
              </k-vuelidate-error-extractor>
            </div>
            <div class="col-md-6" v-show="isJutalomTipusaSpejzNoveles">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.SzemSzuksFordOsszegNoveles"
              >
                <input
                  type="number"
                  class="form-control"
                  v-model="$v.formData.values.SzemSzuksFordOsszegNoveles.$model"
                  style="height: 2.1rem !important;"
                  min="1"
                  max="100"
                />
                <span class="text-help float-right">
                  Növelés mértéke (1 - 100%)
                </span>
              </k-vuelidate-error-extractor>
            </div>

            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group summernote"
                :validator="$v.formData.values.Leiras"
              >
                <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  name="leiras"
                  class="mb-0"
                ></k-summernote>
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
        id="elutasitasBtn"
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="RogzitesBefore()"
        :disabled="mentesLoading || buttonsDisabled"
        v-if="isParancsnok"
      >
        <b-spinner small v-if="mentesLoading"></b-spinner>
        Elutasítás
      </button>
      <button
        id="csakJavaslatBtn"
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="Rogzites(JutalomTipusId, (CsakJavaslatFl = true))"
        :disabled="mentesLoading || buttonsDisabled"
        v-if="isReintegraciosUser || isOsztalyvezeto"
      >
        <b-spinner small v-if="mentesLoading"></b-spinner>
        Csak javaslat
      </button>
      <button
        id="mentesBtn"
        type="button"
        class="btn savebtn white mb-lg-5"
        @click="Rogzites()"
        :disabled="mentesLoading || buttonsDisabled || isRogzitesDisabled"
      >
        <b-spinner small v-if="mentesLoading"></b-spinner>
        Jutalom kiosztása
      </button>
      <b-popover
        target="elutasitasBtn"
        triggers="null"
        :show.sync="isShowPopup"
        placement="topleft"
        container="dontesVagyJavaslat"
        ref="confirmPopover"
        custom-class="ujResztvevoPopover"
      >
        <template slot="title">
          Megerősítés
        </template>
        <div class="pb-5">
          <div
            class="form-group form-material  mb-10"
            data-plugin="formMaterial"
          >
            Biztosan elutasítja az előterjesztést?
          </div>
          <div class="text-right">
            <b-button
              size="sm"
              @click="isShowPopup = false"
              variant="default"
              class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700 mr-5"
              >Nem
            </b-button>
            <b-button
              size="sm"
              @click="Rogzites((JutalomTipusId = 12))"
              variant="warning"
              class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
            >
              Igen
            </b-button>
          </div>
        </div>
      </b-popover>
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
import { useSimpleModalHandler } from '../../../utils/modal';
import { hidrateForm } from '../../../utils/vueUtils';
import { deselectDatatable, timeout } from '../../../utils/common';
import $ from 'jquery';
import moment from 'moment';
import { jogosultsagok, UserStoreTypes } from '../../../store/modules/user';
import Cimkek from '../../../data/enums/Cimkek';
import { sortStr } from '../../../utils/sort';
import requiredIf from 'vuelidate/lib/validators/requiredIf';
import maxValue from 'vuelidate/lib/validators/maxValue';

export default {
  name: 'jutalom-dontes-vagy-javaslat',
  data() {
    return {
      isFormLoading: false,
      isShowPopup: false,
      title: 'Döntés a jutalomról vagy javaslat',
      jutalomIds: [],
      fenyitesek: [],

      mentesLoading: false,
      isMounted: false,
      formData: {
        jutalomTipusokOptions: [],
        jutalomTipusokDefault: [],
        jutalomHosszOptions: [],
        fegyelmiUgyOptions: [],
        fegyelmiUgyDefault: [],
        fogvatartasId: null,
        jutalomHosszDefault: [],
        values: {
          JutalomTipusId: null,
          JutalomHossz: null,
          FegyelmiUgyId: null,
          FenyitesId: null,
          Leiras: '',
          SzemSzuksFordOsszegNoveles: null,
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
        JutalomTipusId: {
          minValueSelect: required,
        },
        JutalomHossz: {
          required: requiredIf(function() {
            if (
              !this.CsakJavaslatFl &&
              (this.formData.values.JutalomTipusId ==
                Cimkek.JutalomTipusok.SzemSzuksFordOsszegNoveles ||
                this.formData.values.JutalomTipusId ==
                  Cimkek.JutalomTipusok.KondiDijmHaszn)
            ) {
              return true;
            } else {
              return false;
            }
          }),
        },
        SzemSzuksFordOsszegNoveles: {
          required: requiredIf(function() {
            if (
              !this.CsakJavaslatFl &&
              this.formData.values.JutalomTipusId ==
                Cimkek.JutalomTipusok.SzemSzuksFordOsszegNoveles
            ) {
              return true;
            } else {
              return false;
            }
          }),
          minValue: minValue(1),
          maxValue: maxValue(100),
        },
        FenyitesId: {},
        Leiras: {
          required,
        },
        FegyelmiUgyId: {},
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        console.log(data);
        this.jutalomIds = data.jutalomIds || [];
        this.LoadFormData(data.jutalomIds);
      } else {
        this.Hide();
      }
    },
    LoadFormData: async function(jutalomIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetJutalomDontesVagyJavaslatModalData({
          jutalomId: jutalomIds[0],
        });
        console.log(result);
        this.formData.jutalmazandoFogvatartottakDefault =
          result.jutalmazandoFogvatartottakDefault;
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        hidrateForm(this, this.formData.values, result);

        this.formData.jutalomTipusokOptions = result.JutalomTipusokOptions;

        this.formData.fegyelmiUgyOptions = result.FegyelmiUgyOptions;
        this.formData.fegyelmiUgyDefault = result.FegyelmiUgyDefault;
        this.formData.jutalomHosszDefault = result.JutalomHosszDefault;
        this.formData.jutalomTipusokDefault = result.JutalomTipusokDefault;

        this.formData.jutalomHosszOptions = result.JutalomHosszOptions.sort(
          sortStr('text')
        );
        console.log(
          this.formData.fegyelmiUgyOptions,
          'this.formData.fegyelmiUgyOptions2 '
        );

        this.formData.fogvatartasId = result.FogvatartasId;
        this.isFormLoading = false;
        await this.$nextTick();
        this.$v.$reset();
      } catch (err) {
        console.log(err);
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        this.Hide();
      }
    },
    SaveFormData: async function(CsakJavaslatFl) {
      try {
        console.log('data', this.formData.values);
        var result = await apiService.SaveJutalomDontesVagyJavaslatModalData({
          data: {
            ...this.formData.values,
            JutalomId: this.jutalomIds[0],
            CsakJavaslatFl: CsakJavaslatFl ? CsakJavaslatFl : false,
          },
        });
        this.naplobejegyzesIds = result.naplobejegyzesIds;
        NotificationFunctions.SuccessAlert(this.title, 'Sikeres mentés');
        eventBus.$emit('Sidebar:jutalmiUgyReszletek:refresh');
        await this.$nextTick();
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
    async RogzitesBefore() {
      if (!this.IsFormValid()) {
        return;
      }
      if (this.jutalomTipusId != Cimkek.JutalomTipusok.Elutasitas) {
        // Animáció megvárása
        if ($('#dontesVagyJavaslat').find('.note-toolbar:visible').length > 0) {
          await timeout(500);
        }
        this.isShowPopup = true;
      } else {
        this.Rogzites();
      }
    },
    async Rogzites(JutalomTipusId, CsakJavaslatFl) {
      this.isShowPopup = false;
      this.mentesLoading = true;
      if (JutalomTipusId) {
        this.formData.values.JutalomTipusId = Cimkek.JutalomTipusok.Elutasitas;
      }
      var success = await this.SaveFormData(CsakJavaslatFl);
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
    ...mapGetters({
      userInfo: UserStoreTypes.getters.getUserInfo,
    }),
    jutalomTipusokOptions() {
      return {
        data: this.formData.jutalomTipusokOptions,
        dropdownParent: $(this.$el),
      };
    },
    datePickerOptions() {
      return {
        format: 'YYYY.MM.DD',
        useCurrent: false,
        defaultDate: this.formData.values.Hatarido || moment(),
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        minDate: moment().startOf('d'),
        maxDate: moment(
          this.formData.maxHatarido || new Date().toISOString()
        ).endOf('d'),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
    isParancsnok() {
      let isParancsnok =
        this.userInfo.Jogkor.Id == Cimkek.JutalmazasJogkor.Parancsnok;
      return isParancsnok;
    },
    isReintegraciosUser() {
      let isReintegracios =
        this.userInfo.Jogkor.Id == Cimkek.JutalmazasJogkor.ReintegraciosTiszt;

      return isReintegracios;
    },
    isOsztalyvezeto() {
      let isOsztalyvezeto =
        this.userInfo.Jogkor.Id == Cimkek.JutalmazasJogkor.Osztalyvezeto;

      return isOsztalyvezeto;
    },
    isRogzitesDisabled() {
      let jutalomTipusokOptions = this.formData.jutalomTipusokOptions || [];
      let JutalomTipusId = this.formData.values.JutalomTipusId;
      let kivalasztottCsoport = jutalomTipusokOptions.find(f =>
        f.children.some(s => s.id == JutalomTipusId)
      );
      if (!kivalasztottCsoport) {
        return false;
      }
      let isReintVanKivalasztva =
        kivalasztottCsoport.id == Cimkek.JutalmazasJogkor.ReintegraciosTiszt;

      if (!isReintVanKivalasztva && this.isReintegraciosUser) {
        return true;
      }
      let isOsztalyvezVanKivalasztva =
        kivalasztottCsoport.id == Cimkek.JutalmazasJogkor.Osztalyvezeto;

      let isParancsnokVanKivalasztva =
        kivalasztottCsoport.id == Cimkek.JutalmazasJogkor.Parancsnok;

      if (isParancsnokVanKivalasztva && this.isOsztalyvezeto) {
        return true;
      }

      let isElutasitvaVanKivalasztva =
        this.jutalomTipusId == Cimkek.JutalomTipusok.Elutasitas;

      if (isElutasitvaVanKivalasztva && this.isParancsnok) {
        return true;
      }

      return false;
    },
    rogzitesTitle() {
      if (!this.userInfo) {
        return 'Mentés';
      }

      let jutalomTipusokOptions = this.formData.jutalomTipusokOptions || [];
      let JutalomTipusId = this.formData.values.JutalomTipusId;
      let kivalasztottCsoport = jutalomTipusokOptions.find(f =>
        f.children.some(s => s.id == JutalomTipusId)
      );

      if (!kivalasztottCsoport) {
        return 'Mentés';
      }

      let userJogosultsagok = this.userInfo.Jogosultsagok || [];

      let isJogkorGyakorloUser = userJogosultsagok.some(
        s => s == jogosultsagok.fegyelmiJogkorGyakorloja
      );

      let isParancsnok = false;
      if (this.userInfo.Jogkor) {
        isParancsnok =
          this.userInfo.Jogkor.Id == Cimkek.JutalmazasJogkor.Parancsnok;
      }

      let isReintegraciosUser = false;
      if (this.userInfo.Jogkor) {
        isReintegraciosUser =
          this.userInfo.Jogkor.Id == Cimkek.JutalmazasJogkor.ReintegraciosTiszt;
      }

      let isOsztalyvezeto = false;
      if (this.userInfo.Jogkor) {
        isOsztalyvezeto =
          this.userInfo.Jogkor.Id == Cimkek.JutalmazasJogkor.Osztalyvezeto;
      }

      if (isReintegraciosUser || isParancsnok || isOsztalyvezeto) {
        return 'Jutalom kiosztása';
      }

      return 'Mentés';
    },
    fegyelmiUgyOptions() {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.fegyelmiUgyOptions,
        dropdownParent: $(this.$el),
        readOnly: false,
        disabled:
          this.formData.values.JutalomTipusId !=
          Cimkek.JutalomTipusok.FenyitesTorles,
      };
    },
    jutalomTipusId() {
      return this.formData.values.JutalomTipusId;
    },
    Select2JutalomHosszOptions: function() {
      return {
        data: this.formData.jutalomHosszOptions,
        dropdownParent: $(this.$el),
      };
    },
    isJutalomTipusaSpejzNoveles() {
      return (
        this.formData.values.JutalomTipusId ==
        Cimkek.JutalomTipusok.SzemSzuksFordOsszegNoveles
      );
    },
    isKondiDijmHaszn() {
      return (
        this.formData.values.JutalomTipusId ==
        Cimkek.JutalomTipusok.KondiDijmHaszn
      );
    },
    isJutalomTipusaFenyitesTorles() {
      return (
        this.formData.values.JutalomTipusId ==
        Cimkek.JutalomTipusok.FenyitesTorles
      );
    },

    isJutalomHosszaMezoSzelesseg() {
      if (
        this.formData.values.JutalomTipusId ==
        Cimkek.JutalomTipusok.SzemSzuksFordOsszegNoveles
      ) {
        return 6;
      } else {
        return 12;
      }
    },
  },
  watch: {},
  components: {},
};
</script>
<style scoped>
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
