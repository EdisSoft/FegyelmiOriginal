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
          45. Szakterületi vélemény
        </h4>
      </div>
      <div class="modal-body px-25 pt-20 pb-40">
        <div>
          <div class="row">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group form-material"
                :validator="$v.formData.values.Datuma"
              >
                <date-picker
                  v-model="$v.formData.values.Datuma.$model"
                  :config="datePickerOptions"
                >
                </date-picker>
                <span class="text-help float-right">Szakvélemény dátuma</span>
              </k-vuelidate-error-extractor>
            </div>
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
                />
                <!-- <k-summernote
                  v-model="$v.formData.values.Leiras.$model"
                  name="leiras"
                  class="mb-0"
                ></k-summernote> -->
                <span class="text-help float-right">Szakvélemény</span>
              </k-vuelidate-error-extractor>
            </div>
            <div
              class="col-md-12 mb-2"
              v-if="formData.values.PrevUploadedFiles.length > 0"
            >
              <div>
                <span
                  class="badge text-break badge-outline badge-default mr-2 mb-2 pointer pl-5 pr-10 py-0 rounded-right-0"
                  v-for="file in formData.values.PrevUploadedFiles"
                  :key="file.Id"
                  @click.stop="DownloadFile(file.DownloadUrl)"
                >
                  {{ file.FileName }}
                  <span
                    class="delete-file icon wb-close pointer py-5 px-5"
                    @click.stop="DeleteFile(file.Id)"
                  >
                  </span>
                </span>
              </div>
            </div>
            <div class="col-md-12">
              <k-file-upload
                id="esemeny"
                v-model="formData.values.UploadedFiles"
                ref="fileUpload"
                projectName="Fenyites"
                @OnUploadError="OnUploadError"
              ></k-file-upload>
              <span class="text-help float-right">Csatolt fájlok</span>
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
        v-on:click="Save"
        :disabled="mentesFolyamatban || buttonsDisabled"
      >
        <b-spinner small v-if="mentesFolyamatban"></b-spinner>
        Kész
      </button>
    </div>
  </basic-loader>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { AppStoreTypes } from '../../store/modules/app';
import { eventBus } from '../../main';
import settings from '../../data/settings';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { getUgyszam } from '../../utils/fenyitesUtils';
import { required, minValue } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import moment from 'moment';
import $ from 'jquery';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';
import { FegyelmiUgyFunctions } from '../../functions/FegyelmiUgyFunctions';

export default {
  name: 'szakteruleti-velemeny',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyIds: [],
      title: 'Szakterületi vélemény',
      mentesFolyamatban: false,
      formData: {
        values: {
          NaplobejegyzesIds: [],
          Datuma: null,
          Leiras: '',
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
  mounted: function() {
    this.InitEvents(this.modalOpts);
  },
  created() {},
  validations: {
    formData: {
      values: {
        Datuma: {
          required,
        },
        Leiras: {
          required,
        },
      },
    },
  },
  methods: {
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
    async DeleteFile(fileId) {
      try {
        var result = await apiService.SzakteruletiVelemenyFileDelete({
          fileId,
        });
        this.formData.values.PrevUploadedFiles = this.formData.values.PrevUploadedFiles.filter(
          f => f.Id != fileId
        );
        NotificationFunctions.SuccessAlert('A fájl törlése sikerült');
      } catch (error) {
        NotificationFunctions.ErrorAlert(
          'Szakterületi vélemény',
          'A fájl törlése nem sikerült'
        );
      }
    },
    InitEvents: function({ state, data }) {
      if (state) {
        this.fegyelmiUgyIds = data.fegyelmiUgyIds;
        this.formData.values.NaplobejegyzesIds = data.naplobejegyzesIds;
        this.LoadFegyelmiUgyData(data.fegyelmiUgyIds, data.naplobejegyzesIds);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyIds, naplobejegyzesIds) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFegyelmiUgyDataSzakteruletiVelemenyhez(
          {
            fegyelmiUgyIds: fegyelmiUgyIds,
            naplobejegyzesIds: naplobejegyzesIds,
          }
        );
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        this.isFormLoading = false;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        this.Hide();
      }

      hidrateForm(this, this.formData.values, result);
      this.formData.values.PrevUploadedFiles = FegyelmiUgyFunctions.GetCsatolmanyInfo(
        this.formData.values.PrevUploadedFiles
      );

      this.formData.values.Datuma = moment(this.formData.values.Datuma).format(
        'YYYY.MM.DD'
      );

      this.$v.$reset();
    },
    Save: async function() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        return;
      }
      let dropZone = this.$get(this, '$refs.fileUpload.$refs.dropzone');
      if (dropZone && dropZone.getActiveFiles().length > 0) {
        NotificationFunctions.WarningAlert(
          'Figyelmeztetés',
          'Várja meg a fájlfeltöltés végét!'
        );
        return;
      }
      this.mentesFolyamatban = true;
      try {
        var result = await apiService.FegyelmiUgySzakteruletiVelemenyMentes({
          data: {
            ...this.formData.values,
            FegyelmiUgyIds: this.fegyelmiUgyIds,
          },
        });
        this.formData.values.NaplobejegyzesIds = result.naplobejegyzesIds;

        NotificationFunctions.SuccessAlert(
          'Szakterületi vélemény mentés',
          result.message
        );
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        deselectDatatable();
        this.Hide();
        this.$v.$reset();

        this.mentesFolyamatban = false;
      } catch (err) {
        var errorMsg = 'Hiba a mentés során: ' + err;
        NotificationFunctions.AjaxError({
          title: 'Szakterületi vélemény mentés',
          text: '',
          errorObj: err,
        });
        console.log(errorMsg);
        this.mentesFolyamatban = false;
      }
    },
  },
  computed: {
    ...mapGetters({
      //dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),

    datePickerOptions() {
      //let minDate = this.formData.ElrendelesDatuma;
      //if (!minDate) {
      //  minDate = moment()
      //    .subtract({ days: 2 })
      //    .toISOString();
      //}

      return {
        format: 'YYYY.MM.DD',
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
        maxDate: moment().endOf('d'),
        //minDate: moment(minDate),
        widgetPositioning: {
          horizontal: 'left',
          vertical: 'bottom',
        },
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
.deleteBtn {
}
</style>
