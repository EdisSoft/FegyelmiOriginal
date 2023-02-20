<template>
  <transition name="slidePanel">
    <div
      v-show="visible"
      class="slidePanel slidePanel-right slidePanel-show w-p15 w-p50"
      style="overflow-y: auto;"
      id="slidepanel-esemeny"
    >
      <div class="slidePanel-content">
        <header class="slidePanel-header px-20 p-15 bg-blue-grey-400">
          <div
            class="slidePanel-actions"
            aria-label="actions"
            role="group"
            style="min-height:40px;"
          >
            <button
              type="button"
              class="btn btn-icon btn-pure slidePanel-close actions-top icon wb-close white py-0 px-0"
              @click="Hide()"
              aria-hidden="true"
            ></button>
          </div>
          <h1 v-if="formData.values.EsemenyId == 0" class="white">
            Új esemény felvétele
          </h1>
          <h1 v-else class="white">Rendkívüli esemény módosítása</h1>
          <p class="white mb-0">
            Írja le az esemény körülményeit, mentés után pedig adja meg a
            résztvevőket!
          </p>
        </header>
        <div class="slidePanel-inner position-relative w-p100 pr-5">
          <div
            class="vuebar-element slidePanelscroll"
            v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
          >
            <div>
              <div class="slidePanel-inner-section border-bottom-0 py-15 pr-20">
                <div class="row">
                  <div class="col-md-12">
                    <k-vuelidate-error-extractor
                      class="form-group form-material"
                      :validator="$v.formData.values.Megnevezes"
                    >
                      <b-form-input
                        v-model="$v.formData.values.Megnevezes.$model"
                      >
                      </b-form-input>
                      <span class="text-help float-right"
                        >Esemény rövid leírása</span
                      >
                    </k-vuelidate-error-extractor>
                  </div>
                  <div class="col-md-12">
                    <k-vuelidate-error-extractor
                      class="form-group form-material"
                      :validator="$v.formData.values.HelyCimId"
                    >
                      <k-select2
                        :options="Select2HelyOptions"
                        v-model="$v.formData.values.HelyCimId.$model"
                        class=""
                        placeholder="Esemény helye"
                      >
                      </k-select2>
                      <span class="text-help float-right">Esemény helye</span>
                    </k-vuelidate-error-extractor>
                  </div>
                  <div class="col-md-12">
                    <k-vuelidate-error-extractor
                      class="form-group form-material"
                      :validator="$v.formData.values.JellegCimId"
                    >
                      <k-select2
                        :options="Select2JellegOptions"
                        v-model="$v.formData.values.JellegCimId.$model"
                        class=""
                        placeholder="Esemény jellege"
                      >
                      </k-select2>
                      <span class="text-help float-right">Esemény jellege</span>
                    </k-vuelidate-error-extractor>
                  </div>
                  <div class="col-md-12 ">
                    <k-vuelidate-error-extractor
                      class="form-group form-material"
                      :validator="$v.formData.values.EsemenyDatuma"
                    >
                      <date-picker
                        v-model="$v.formData.values.EsemenyDatuma.$model"
                        :config="datePickerOptions"
                      >
                      </date-picker>
                      <span class="text-help float-right"
                        >Esemény dátuma és ideje</span
                      >
                    </k-vuelidate-error-extractor>
                  </div>
                  <div class="col-md-12">
                    <k-vuelidate-error-extractor
                      class="form-group form-material"
                      :validator="$v.formData.values.NapszakCimId"
                    >
                      <!-- <select></select> -->
                      <k-select2
                        :options="Select2NapszakOptions"
                        v-model="$v.formData.values.NapszakCimId.$model"
                      ></k-select2>
                      <span class="text-help float-right">Napszak</span>
                    </k-vuelidate-error-extractor>
                  </div>
                </div>
                <div class="row"></div>

                <div class="row">
                  <div class="col-md-12">
                    <k-vuelidate-error-extractor
                      class="form-group"
                      :validator="$v.formData.values.Leiras"
                    >
                      <!--<textarea class="leiras summernote"></textarea>-->
                      <k-summernote
                        v-model="$v.formData.values.Leiras.$model"
                        name="leiras"
                        class="mb-0"
                      ></k-summernote>
                      <span class="text-help float-right"
                        >Esemény részletes leírása</span
                      >
                    </k-vuelidate-error-extractor>
                  </div>
                </div>
              </div>
              <div
                v-if="isLoading"
                class="vertical-align text-center"
                style="position:absolute; left:0; right:0; bottom:0; top:0; background-color:white;"
              >
                <div class="loader vertical-align-middle loader-ellipsis"></div>
              </div>
            </div>
          </div>
          <!-- <slot></slot> -->
        </div>
        <footer
          class="slidePanel-header p-15 pr-30 bg-blue-grey-400 position-fixed text-right"
        >
          <b-button @click="Hide()" class="btn-raised">Mégsem</b-button>
          <b-button
            v-if="formData.values.EsemenyId == 0"
            @click="SaveEsemeny()"
            class="ml-3 btn btn-raised font-weight-700 savebtn"
            >Felvétel kész és adatlap megnyitása</b-button
          >
          <b-button
            v-else
            @click="SaveEsemeny()"
            class="ml-3 btn btn-raised font-weight-700 savebtn"
            >Fegyelmi esemény módosítása</b-button
          >
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import $ from 'jquery';
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { eventBus } from '../../main';
import settings from '../../data/settings';
import { async } from 'q';
import moment from 'moment';
import { required, minValue } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { debuglog } from 'util';
export default {
  name: 'k-slide-panel-esemeny',
  props: ['id'],
  data: function() {
    return {
      datePickerOptions: {
        format: 'YYYY.MM.DD HH:ss',
        useCurrent: false,
        locale: moment.locale('hu'),
        dayViewHeaderFormat: 'YYYY. MMMM',
      },
      visible: false,
      canClose: true,
      esemenyId: null,
      isActive: false,
      isLoading: false,

      formData: {
        jellegOptions: [],
        helyOptions: [],
        napszakOptions: [],
        values: {
          EsemenyId: 0,
          Megnevezes: '',
          Leiras: '',
          JellegCimId: 0,
          NapszakCimId: 0,
          HelyCimId: 0,
          EsemenyDatuma: null,
        },
      },
    };
  },
  mounted: function() {
    var vm = this;

    eventBus.$on('Sidebar:' + vm.id, ({ state, data }) => {
      if (state) {
        this.LoadEsemenyData(data.esemenyId);
      } else {
        this.Hide();
      }
    });
  },
  validations: {
    formData: {
      values: {
        Megnevezes: {
          required,
        },
        EsemenyDatuma: {
          required,
        },
        JellegCimId: {
          minValueSelect: minValue(1),
        },
        NapszakCimId: {
          minValueSelect: minValue(1),
        },
        HelyCimId: {
          minValueSelect: minValue(1),
        },
        Leiras: {
          required,
        },
      },
    },
  },
  methods: {
    /*FanyCategory: function(event) {
        this.isActive = true;
      },*/
    LoadEsemenyData: async function(esemenyId) {
      try {
        var result = await apiService.GetEsemeny({ id: esemenyId });

        this.formData.jellegOptions = result.JellegOptions.sort(function(a, b) {
          return ('' + a.text).localeCompare(b.text);
        });

        this.formData.napszakOptions = result.NapszakOptions.sort(function(
          a,
          b
        ) {
          return ('' + a.text).localeCompare(b.text);
        });

        this.formData.helyOptions = result.HelyOptions.sort(function(a, b) {
          return ('' + a.text).localeCompare(b.text);
        });

        for (const key in this.formData.values) {
          if (
            this.formData.values.hasOwnProperty(key) &&
            result.hasOwnProperty(key)
          ) {
            this.$set(this.formData.values, key, result[key]);
          }
        }

        this.formData.values.EsemenyDatuma = moment(
          this.formData.values.EsemenyDatuma
        ).format('YYYY.MM.DD HH:mm');

        this.Show(esemenyId);
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok lekérdezése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
    },
    SaveEsemeny: async function() {
      try {
        if (this.$v.$invalid) {
          this.$v.$touch();
          return;
        }
        var result = await apiService.SaveEsemeny({
          values: this.formData.values,
        });
        NotificationFunctions.SuccessAlert(
          'Rendkívüli esemény rögzítése',
          result.message
        );
        this.Hide();
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Esemény rögzítése sikertelen',
          errorObj: err,
        });
        console.log(err);
      }
    },
    Show: function(esemenyId) {
      //this.url = url;
      this.visible = true;
      this.canClose = false;
      this.esemenyId = esemenyId;
      setTimeout(() => {
        this.canClose = true;
      }, 350);
    },
    Hide: function() {
      //debugger;
      if (!this.canClose) {
        return;
      }
      if (this.visible) {
        $('.dataTable tr.active').removeClass('active');
      }
      this.visible = false;
      this.isActive = false;
      this.$v.$reset();
    },
    StartLoader: function() {
      this.isLoading = true;
    },
    EndLoader: function() {
      this.isLoading = false;
    },
  },
  computed: {
    Select2JellegOptions: function() {
      return {
        data: this.formData.jellegOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2NapszakOptions: function() {
      return {
        data: this.formData.napszakOptions,
        dropdownParent: $(this.$el),
      };
    },
    Select2HelyOptions: function() {
      return {
        data: this.formData.helyOptions,
        dropdownParent: $(this.$el),
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
    getData() {
      return JSON.stringify(this.formData.values, null, 2);
    },
  },
  watch: {
    esemenyId: {
      handler: function(value) {
        if (value) {
          //this.StartLoader();
        }
      },
      immediate: true,
    },
  },
  destroyed: function() {},
};
</script>
<style scoped>
.slide-overlay {
  background: #92929257;
  position: fixed;
  left: 0;
  z-index: 999;
  transition: background-color 0.5s ease;
}

.page-header {
  position: relative;
  padding: 30px 30px;
  margin-top: 0;
  margin-bottom: 0;
  background: transparent;
  border-bottom: 0;
}

.slidePanel {
  width: 561px !important;
}

.slidePanel-enter-active {
  transition: all 0.5s;
}

.slidePanel-leave-active {
  transition: all 0.5s;
}

.slidePanel-enter,
.slidePanel-leave-to {
  transform: translateX(100%);
}

.slidePanelOverlay-enter-active {
  transition: all 0.5s;
}

.slidePanelOverlay-leave-active {
  transition: all 0.5s;
}

.slidePanelOverlay-enter,
.slidePanelOverlay-leave-to {
  transform: translateX(100%);
  opacity: 0;
}

.slidePanel-content {
  height: 100%;
}

.slidePanel .actions-top {
  position: absolute;
  top: 30px;
  right: 20px;
}
.slidePanel header {
  z-index: 99;
  position: sticky;
  top: 0;
}
.slidePanel footer {
  bottom: 0;
  width: 561px;
}

.slidePanel .list-group li:last-child {
  margin-bottom: 25px;
}

.slidePanel .popover button.close {
  opacity: 1;
}
.ujResztvevoPopover textarea.form-control {
  color: #a3afb7 !important;
  font-weight: 400;
  font-size: 15px;
}
.resztvevotorles {
  display: none;
}
.slidePanel .list-group li:hover .resztvevotorles {
  display: inline-block;
}
.slidePanel .savebtn {
  background-color: #f4cf5d;
  border-color: #f4cf5d;
}
</style>
