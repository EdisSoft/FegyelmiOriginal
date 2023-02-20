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
          <p v-if="id" class="mt-10 mb-0 font-size-12">
            Az űrlapon új résztvevőket vehet fel és új bizonyítékokat csatolhat.
          </p>
          <p v-else class="mt-10 mb-0 font-size-12">
            Írja le az esemény körülményeit és adja meg a résztvevőket.
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
                  :validator="$v.formData.values.EszleloId"
                >
                  <k-select2
                    :options="eszlelokSelectOptions"
                    v-model="$v.formData.values.EszleloId.$model"
                    class=""
                  >
                  </k-select2>
                  <span class="text-help float-right">Észlelő</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-6 longtext-fix">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.EsemenyHelyCimkeId"
                >
                  <k-select2
                    :options="esemenyHelySelectOptions"
                    v-model="$v.formData.values.EsemenyHelyCimkeId.$model"
                    class=""
                  >
                  </k-select2>
                  <span class="text-help float-right">Esemény helye</span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-6 longtext-fix">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.EsemenyJellegCimkeId"
                >
                  <k-select2
                    :options="esemenyJellegSelectOptions"
                    v-model="$v.formData.values.EsemenyJellegCimkeId.$model"
                    class=""
                  >
                  </k-select2>
                  <span class="text-help float-right"
                    >Fegyelmi vétség típusa</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="row align-items-end mx-0" style="width: 100%;">
                <div class="col-md-6">
                  <k-vuelidate-error-extractor
                    class="form-group form-material"
                    :validator="$v.formData.values.EsemenyDatuma"
                  >
                    <date-picker
                      :readonly="!!id"
                      class="text-left"
                      v-model="$v.formData.values.EsemenyDatuma.$model"
                      :config="datePickerOptions"
                      ref="EsemenyDatuma"
                    >
                    </date-picker>
                    <span class="text-help float-right"
                      >Esemény dátuma és ideje</span
                    >
                  </k-vuelidate-error-extractor>
                </div>
                <div class="col-md-6 longtext-fix">
                  <k-vuelidate-error-extractor
                    class="form-group form-material"
                    :validator="$v.formData.values.NapszakCimkeId"
                  >
                    <k-select2
                      :options="napszakSelectOptions"
                      v-model="$v.formData.values.NapszakCimkeId.$model"
                      class=""
                    >
                    </k-select2>
                    <span class="text-help float-right">Napszak</span>
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
                    ><span class="text-help my-0 ml-10"
                      >Elkövetők, sértettek és tanúk kiválasztása ebből az
                      intézetből</span
                    ></label
                  >
                </div>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.Elkovetok"
                >
                  <k-select2-ajax
                    :options="elkovetokSelectOptions"
                    :defaultValue="formData.elkovetokDefault"
                    :fixValue="elkovetokFix"
                    :additionalParams="additionalParams"
                    v-model="$v.formData.values.Elkovetok.$model"
                    @nemTorolheto="ElkovetoNemTorolheto"
                  >
                  </k-select2-ajax>
                  <span class="text-help float-right">
                    Elkövetők
                    <span v-if="kellIntezet"> az intézetemben</span> (több
                    személy kiválasztása: Ctrl + klikk a listában)
                  </span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.Sertettek"
                >
                  <k-select2-ajax
                    :options="sertettekSelectOptions"
                    :defaultValue="formData.sertettekDefault"
                    :additionalParams="additionalParams"
                    v-model="$v.formData.values.Sertettek.$model"
                    class=""
                  >
                  </k-select2-ajax>
                  <span class="text-help float-right">
                    Sértett
                    <span v-if="kellIntezet"> az intézetemben</span> (több
                    személy kiválasztása: Ctrl + klikk a listában)
                  </span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.Tanuk"
                >
                  <k-select2-ajax
                    :options="tanukSelectOptions"
                    :defaultValue="formData.tanukDefault"
                    :additionalParams="additionalParams"
                    v-model="$v.formData.values.Tanuk.$model"
                    class=""
                  >
                  </k-select2-ajax>
                  <span class="text-help float-right">
                    Tanúk
                    <span v-if="kellIntezet"> az intézetemben</span> (több
                    személy kiválasztása: Ctrl + klikk a listában)
                  </span>
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.AllomanyiTanuk"
                >
                  <k-select2
                    :options="allomanyiTanukSelectOptions"
                    v-model="$v.formData.values.AllomanyiTanuk.$model"
                    class=""
                  >
                  </k-select2>
                  <span class="text-help float-right"
                    >Személyi állományi tanú</span
                  >
                </k-vuelidate-error-extractor>
              </div>
              <div class="col-md-12">
                <k-vuelidate-error-extractor
                  class="form-group form-material"
                  :validator="$v.formData.values.Bizonyitek"
                >
                  <b-form-input
                    :readonly="!!id"
                    v-model="$v.formData.values.Bizonyitek.$model"
                    placeholder=""
                  ></b-form-input>
                  <span class="text-help float-right">Bizonyíték</span>
                </k-vuelidate-error-extractor>
              </div>
              <div
                class="col-md-12 mb-4"
                v-if="formData.values.PrevUploadedFiles.length > 0"
              >
                <div>
                  <span
                    class="badge text-break badge-outline badge-default mr-2 pointer"
                    v-for="file in formData.values.PrevUploadedFiles"
                    :key="file.Id"
                    @click="DownloadFile(file.DownloadUrl)"
                  >
                    {{ file.FileName }}
                  </span>
                </div>
                <span class="text-help float-right"
                  >Korábban feltöltött fájlok</span
                >
              </div>
              <div class="col-md-12">
                <k-file-upload
                  id="esemeny"
                  v-model="formData.values.UploadedFiles"
                  ref="fileUpload"
                  projectName="Fenyites"
                  @OnUploadError="OnUploadError"
                ></k-file-upload>
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
            <span class="ml-3">{{ saveBtn }}</span>

            <!--Nyomtatás popover a gombhoz-->
            <b-popover
              target="mentesBtn"
              triggers="null"
              :show.sync="fegyelmiLapNyomtatasaConfirm.isShow"
              placement="topleft"
              container="esemenyRogziteseModal"
              ref="confirmPopover"
              custom-class="ujResztvevoPopover"
            >
              <template slot="title">
                <b-button
                  @click="CloseFegyelmiLapNyomtatasaConfirm()"
                  class="close"
                  aria-label="Close"
                >
                  <span class="d-inline-block white" aria-hidden="true"
                    >&times;</span
                  >
                </b-button>
                Megerősítés
              </template>
              <div class="pb-5">
                <div
                  class="form-group form-material mb-10"
                  data-plugin="formMaterial"
                >
                  {{ fegyelmiLapNyomtatasaConfirm.confirmText }}
                </div>
                <div class="text-right">
                  <b-button
                    size="sm"
                    @click="CloseFegyelmiLapNyomtatasaConfirm()"
                    variant="default"
                    class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700 mr-5"
                    >Nem</b-button
                  >
                  <b-button
                    size="sm"
                    @click="FegyelmiLapNyomtatasa()"
                    variant="warning"
                    class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
                    >Igen</b-button
                  >
                </div>
              </div>
            </b-popover>
          </b-button>
        </div>
      </div>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService, eventBus } from '../../main';
import { AppStoreTypes } from '../../store/modules/app';
import settings from '../../data/settings';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { getUgyszam } from '../../utils/fenyitesUtils';
import { required, minValue } from 'vuelidate/lib/validators';
import moment from 'moment';
import $ from 'jquery';
import ReintegraciosTisztDontesTipus from '../../data/enums/reintegraciosTisztDontesTipus';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { hidrateForm } from '../../utils/vueUtils';
import { sortStr } from '../../utils/sort';
import { useModalHandler, useSimpleModalHandler } from '../../utils/modal';
import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import { deselectDatatable } from '../../utils/common';
import { FegyelmiUgyFunctions } from '../../functions/FegyelmiUgyFunctions';
import esemeny from '../../store/modules/esemeny';

export default {
  name: 'esemeny-rogzites',
  data() {
    return {
      id: 0,
      isMounted: false,
      fegyelmiUgy: null,
      isMentesLoading: false,
      isNyomtatas: false,
      isFormLoading: false,
      kellIntezet: true,
      //ujFegyelmiUgyek: [],
      fegyelmiLapNyomtatasaConfirm: {
        isShow: false,
        confirmText: '',
      },
      formData: {
        esemenyJellegOptions: [],
        helyOptions: [],
        napszakOptions: [],
        sertettekOptions: [],
        elkovetokOptions: [],
        esemenyRogzitesIdeje: [],
        eszleloDefault: null,
        tanukDefault: [],
        allomanyiTanukDefault: [],
        sertettekDefault: [],
        elkovetokDefault: [],
        eszleloOptions: [],
        values: {
          EsemenyJellegCimkeId: null,
          Leiras: '',
          Bizonyitek: '',
          NapszakCimkeId: null,
          EsemenyHelyCimkeId: null,
          EsemenyDatuma: null,
          Tanuk: [],
          Sertettek: [],
          Elkovetok: [],
          EszleloId: null,
          AllomanyiTanuk: [],
          UploadedFiles: [],
          PrevUploadedFiles: [],
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
        EszleloId: {
          required,
        },
        EsemenyJellegCimkeId: {
          minValueSelect: minValue(1),
        },
        EsemenyHelyCimkeId: {
          minValueSelect: minValue(1),
        },
        EsemenyDatuma: {
          required,
        },
        NapszakCimkeId: {
          minValueSelect: minValue(1),
        },
        Leiras: {
          required,
        },
        Tanuk: {},
        AllomanyiTanuk: {},
        Sertettek: {},
        Elkovetok: { required },
        Bizonyitek: {},
      },
    },
  },
  methods: {
    InitEvents: function ({ state, data }) {
      if (state) {
        this.id = data.esemenyId;
        if (data.esemenyId) {
          this.fegyelmiUgy = this.$store.getters[
            FegyelmiUgyStoreTypes.getters.getFegyelmiUgyById
          ](data.esemenyId);
        } else {
          this.fegyelmiUgy = null;
        }
        this.LoadFormData();
      } else {
        this.Hide();
      }
    },
    async LoadFormData() {
      // Hotfix amíg okosabb megoldás nincs
      this.formData.values.Elkovetok = [];
      this.formData.values.Sertettek = [];
      this.formData.values.Tanuk = [];
      this.formData.values.AllomanyiTanuk = [];
      this.$v.$reset();
      this.isFormLoading = true;
      try {
        let result = await apiService.GetEsemenyRogzitesModalData({
          esemenyId: this.id,
        });
        console.log(result);

        hidrateForm(this, this.formData.values, result);
        this.formData.values.PrevUploadedFiles = FegyelmiUgyFunctions.GetCsatolmanyInfo(
          this.formData.values.PrevUploadedFiles
        );
        this.formData.values.EsemenyDatuma = moment(
          this.formData.values.EsemenyDatuma
        ).format('YYYY.MM.DD HH:mm');

        this.formData.allomanyiTanukDefault = result.AllomanyiTanukDefault;
        this.formData.values.AllomanyiTanuk = result.AllomanyiTanukDefault.map(
          (x) => x.id
        );
        this.formData.tanukDefault = result.TanukDefault;
        this.formData.esemenyRogzitesIdeje = result.EsemenyRogzitesIdeje;
        //this.formData.eszleloDefault = result.EszleloDefault;

        this.formData.sertettekDefault = result.SertettekDefault;
        this.formData.elkovetokDefault = result.ElkovetokDefault;

        this.formData.eszleloOptions = result.EszleloOptions;
        this.formData.eszleloOptions.sort(sortStr('text'));

        this.formData.esemenyJellegOptions = result.EsemenyJellegOptions;
        this.formData.esemenyJellegOptions.sort(sortStr('text'));

        this.formData.helyOptions = result.HelyOptions;
        this.formData.helyOptions.sort(sortStr('text'));

        this.formData.napszakOptions = result.NapszakOptions;
        this.formData.napszakOptions.sort(sortStr('text'));
        this.$nextTick(() => {
          this.$v.$reset();
        });
        this.isFormLoading = false;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok lekérdezése sikertelen',
          errorObj: err,
        });
        console.log(err);
        this.Hide();
      }
    },
    async Mentes() {
      this.isMentesLoading = true;
      if (this.$v.$invalid) {
        this.$v.$touch();
        this.$nextTick(() => {
          var element = this.$el.querySelector('.form-group.error:first-child');
          element.scrollIntoView();
        });
        this.isMentesLoading = false;
        return;
      }
      let dropZone = this.$get(this, '$refs.fileUpload.$refs.dropzone');
      if (dropZone && dropZone.getActiveFiles().length > 0) {
        NotificationFunctions.WarningAlert(
          'Figyelmeztetés',
          'Várja meg a fájlfeltöltés végét!'
        );
        return;
        this.isMentesLoading = false;
      }
      try {
        var fixArray = this.formData.elkovetokDefault.map((m) => m.id);
        var elkovetok = this.formData.values.Elkovetok;
        var ujElkovetok = elkovetok.filter((x) => !fixArray.includes(x));

        if (ujElkovetok.length > 0) {
          this.FegyelmiLapNyomtatasaConfirm();
        } else {
          this.MentesFolytatas();
        }
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: `hiba az esemény ${
            this.id ? 'módosítása' : 'létrehozás'
          } során`,
          errorObj: err,
        });
        console.log(err);
        this.isMentesLoading = false;
      }
      if (this.isSlidePanelOpen)eventBus.$emit('Sidebar:ugyReszletek:refresh');
    },

    async MentesFolytatas() {
      this.isMentesLoading = true;
      let data = { ...this.formData.values, EsemenyId: this.id };
      let result = null;
      try {
        result = await apiService.EsemenyRogzitesModalMentes({
          data,
          mock: true,
        });

        apiService.GetEsemenyek();
        NotificationFunctions.SuccessAlert(
          `Esemény ${this.id ? 'módosítás' : 'létrehozás'}`,
          result.message
        );

        if (this.isNyomtatas == true) {
          console.log('Fegyelmi lap nyomtatás: ' + result.ujFegyelmiUgyek);
          try {
            await NyomtatvanyFunctions.FegyelmiLapokNyomtatasa({
              fegyelmiUgyIds: result.ujFegyelmiUgyek,
            });
            console.log(this.formData.values.EsemenyJellegCimkeId);
            if (
              this.formData.values.EsemenyJellegCimkeId == 98 ||
              this.formData.values.EsemenyJellegCimkeId == 99
            ) {
              console.log('Kárjelentő lap értesítő lap nyomtatása');
              await NyomtatvanyFunctions.KarjelentoLapErtesitoLapDocxNyomtatas({
                esemenyId: result.esemenyId,
              });
            } else {
              console.log('Értesítő lap nyomtatása');
              await NyomtatvanyFunctions.ErtesitoLapByEsemenyIdDocxNyomtatas({
                esemenyId: result.esemenyId,
              });
            }
          } catch (error) {
            NotificationFunctions.WarningAlert(
              `Esemény ${this.id ? 'módosítás' : 'létrehozás'}`,
              'A nyomtatás megszakadt. Kérem ellenőrizze a böngésző nem blokkolja az ablak megnyitását. ' +
                'A nyomtatvány elérhető a fegyelmi ügy listából az ügy iktatott nyomtványai között.'
            );
            this.Hide();
            //return false;
          }
        }

        // if (this.isNyomtatas == true) {
        //   let i;
        //   for (i = 0; i < result.ujFegyelmiUgyek.length; i++) {
        //     console.log('Fegyelmi lap nyomtatás: ' + result.ujFegyelmiUgyek[i]);
        //     try {
        //       await NyomtatvanyFunctions.FegyelmiLapNyomtatas(
        //         result.ujFegyelmiUgyek[i]
        //       );
        //     } catch (error) {
        //       NotificationFunctions.WarningAlert(
        //         `Esemény ${this.id ? 'módosítás' : 'létrehozás'}`,
        //         'A nyomtatás megszakadt. Kérem ellenőrizze a böngésző nem blokkolja az ablak megnyitását. ' +
        //           'A nyomtatvány elérhető a fegyelmi ügy listából az ügy iktatott nyomtványai között.'
        //       );
        //       this.Hide();
        //     }
        //   }
        // }
        deselectDatatable();
        this.Hide();
      } catch (error) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Az esemény rögzítése nem sikerült',
          errorObj: error,
        });
        this.isMentesLoading = false;
      }
    },

    ElkovetoNemTorolheto(text) {
      NotificationFunctions.WarningAlert(
        'Figyelem',
        text +
          ' ellen korábban fegyelmit kezdeményeztünk, ezért a résztvevők közül nem törölhető.'
      );
    },

    FegyelmiLapNyomtatasaConfirm: function () {
      this.$root.$emit('bv::hide::popover');
      this.fegyelmiLapNyomtatasaConfirm.isShow = true;

      this.fegyelmiLapNyomtatasaConfirm.confirmText =
        'Kinyomtatja az új fegyelmi ügyekhez tartozó fegyelmi lapokat?';
    },
    FegyelmiLapNyomtatasa: async function () {
      var vm = this;
      this.isNyomtatas = true;
      this.fegyelmiLapNyomtatasaConfirm.isShow = false;
      // var result = await apiService.FegyelmiUgyInditasa({
      //   fogvatartottId: this.resztvevo.FogvatartottId,
      //   esemenyId: this.resztvevo.EsemenyId,
      // });

      // vm.$emit('fegyelmiinditas', {
      //   fogvatartottId: this.resztvevo.FogvatartottId,
      //   ugyszam: result.ugyszam,
      // });
      //this.Hide();
      this.MentesFolytatas();
    },
    CloseFegyelmiLapNyomtatasaConfirm: function () {
      this.fegyelmiLapNyomtatasaConfirm.isShow = false;
      this.MentesFolytatas();
      //this.Hide();
    },
    OnUploadError({ file, response }) {
      if (response == 'Megszakítva') {
        return;
      }
      NotificationFunctions.ErrorAlert(
        'Hiba',
        response.message || `${file.name} feltöltése sikertelen.`
      );
    },
    DownloadFile(url) {
      window.open(url);
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
      isSlidePanelOpen: AppStoreTypes.getters.isSlidePanelFegyelmiUgyReszletekOpen
    }),

    title() {
      if (this.id) {
        return 'Esemény módosítása';
        //return `${this.id}. számú esemény módosítása`;
      } else {
        return 'Új esemény felvétele';
      }
    },
    saveBtn() {
      return 'Mentés és fegyelmi kezdeményezése';
    },
    datePickerOptions() {
      let maxDate = this.formData.esemenyRogzitesIdeje;
      if (!maxDate) {
        maxDate = new Date().toISOString();
      }
      return {
        format: 'YYYY.MM.DD HH:mm',
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment(maxDate),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
      };
    },
    elkovetokFix() {
      return this.formData.elkovetokDefault.map((m) => m.id);
    },
    eszlelokSelectOptions() {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.eszleloOptions,
        dropdownParent: $(this.$el),
        readOnly: !!this.id,
        disabled: !!this.id,
      };
    },
    elkovetokSelectOptions() {
      if (!this.isMounted) {
        return;
      }

      return {
        placeholder: 'Elkövető neve',
        apiHandler: apiService.FindFogvatartottakForSelect.bind(apiService),
        multiple: true,
        dropdownParent: $(this.$el),
        minimumInputLength: 4,
      };
    },
    sertettekSelectOptions() {
      if (!this.isMounted) {
        return;
      }
      var szerkesztheto = true;
      if (this.formData && this.formData.sertettekDefault.length > 0) {
        szerkesztheto = false;
      }

      //return {
      //  placeholder: 'Sértett neve',
      //  apiHandler: apiService.FindFogvatartottakForSelect.bind(apiService),
      //  multiple: false,
      //  dropdownParent: $(this.$el),
      //  minimumInputLength: this.kellIntezet ? 3 : 5,
      //  readOnly: !!this.id,
      //  disabled: !!this.id,
      //};

      if (szerkesztheto) {
        return {
          placeholder: 'Sértett neve',
          apiHandler: apiService.FindFogvatartottakForSelect.bind(apiService),
          multiple: false,
          dropdownParent: $(this.$el),
          minimumInputLength: this.kellIntezet ? 3 : 5,
          readOnly: false,
          disabled: false,
        };
      } else {
        return {
          placeholder: 'Sértett neve',
          apiHandler: apiService.FindFogvatartottakForSelect.bind(apiService),
          multiple: false,
          dropdownParent: $(this.$el),
          minimumInputLength: this.kellIntezet ? 3 : 5,
          readOnly: true,
          disabled: true,
          data: this.formData.sertettekDefault,
        };
      }
    },

    tanukSelectOptions() {
      if (!this.isMounted) {
        return;
      }
      var szerkesztheto = true;
      if (this.formData && this.formData.tanukDefault.length > 0) {
        szerkesztheto = false;
      }

      if (szerkesztheto) {
        return {
          placeholder: 'Tanú neve',
          apiHandler: apiService.FindFogvatartottakForSelect.bind(apiService),
          multiple: true,
          dropdownParent: $(this.$el),
          minimumInputLength: this.kellIntezet ? 3 : 5,
          readOnly: false,
          disabled: false,
        };
      } else {
        return {
          placeholder: 'Tanú neve',
          apiHandler: apiService.FindFogvatartottakForSelect.bind(apiService),
          multiple: true,
          dropdownParent: $(this.$el),
          minimumInputLength: this.kellIntezet ? 3 : 5,
          data: this.formData.tanukDefault,
          readOnly: true,
          disabled: true,
        };
      }
    },

    allomanyiTanukSelectOptions() {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.eszleloOptions,
        dropdownParent: $(this.$el),
        multiple: true,
        readOnly: !!this.id && this.formData.allomanyiTanukDefault.length > 0,
        disabled: !!this.id && this.formData.allomanyiTanukDefault.length > 0,
      };
    },

    esemenyHelySelectOptions: function () {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.helyOptions,
        dropdownParent: $(this.$el),
        readOnly: !!this.id,
        disabled: !!this.id,
      };
    },
    esemenyJellegSelectOptions: function () {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.esemenyJellegOptions,
        dropdownParent: $(this.$el),
        readOnly: !!this.id,
        disabled: !!this.id,
      };
    },
    napszakSelectOptions: function () {
      if (!this.isMounted) {
        return;
      }
      return {
        data: this.formData.napszakOptions,
        dropdownParent: $(this.$el),
        readOnly: !!this.id,
        disabled: !!this.id,
      };
    },
    additionalParams() {
      return {
        kellIntezet: this.kellIntezet,
      };
    },
  },
  components: {},
  watch: {
    id: {
      handler: function (newValue, oldValue) {},
      deep: true,
      immediate: true,
    },
    'formData.values.EsemenyDatuma': function (newDatum, oldDatum) {
      //$('.picker-switch')[0].click();
      if ($('[data-action="togglePicker"] > .fa-clock-o').length > 0) {
        $('[data-action="togglePicker"]')[0].click();
      }
      //$('.timepicker').click();
    },
  },
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
