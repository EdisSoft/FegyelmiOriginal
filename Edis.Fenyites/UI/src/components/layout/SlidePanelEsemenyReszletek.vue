<template>
  <transition name="slidePanel">
    <div
      v-show="visible"
      class="slidePanel slidePanel-right slidePanel-show w-p15 w-p50"
      style="overflow-y: auto;"
      id="slidepanel-esemeny-reszletek"
    >
      <div class="slidePanel-content">
        <header class="slidePanel-header p-15 bg-blue-grey-400">
          <div
            class="slidePanel-actions"
            aria-label="actions"
            role="group"
            style="min-height:40px;"
          >
            <button
              type="button"
              class="btn btn-icon btn-pure btn-inverse slidePanel-close actions-top icon wb-close white py-0 px-0"
              @click="Hide()"
              aria-hidden="true"
            ></button>
          </div>
          <h1 class="white">Rendkívüli esemény felvétele</h1>
        </header>
        <div class="slidePanel-inner position-relative w-p100 pr-5">
          <div
            class="vuebar-element slidePanelscroll"
            v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
          >
            <div>
              <div class="slidePanel-inner-section border-bottom-0 py-15 pr-20">
                <div class="row">
                  <div class="col-md-6">
                    <label class="form-control-label blue-grey-500"
                      >Megnevezése</label
                    >
                    <br />
                    <label>
                      {{ formData.Megnevezes }}
                    </label>
                  </div>
                  <div class="col-md-6 ">
                    <label class="form-control-label blue-grey-500"
                      >Ideje</label
                    >
                    <br />
                    <label>
                      {{ formData.EsemenyDatuma }}
                    </label>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-6">
                    <label class="form-control-label blue-grey-500"
                      >Helye</label
                    >
                    <br />
                    <label>{{ formData.HelyCimke.Nev }}</label>
                  </div>
                  <div class="col-md-6 ">
                    <label class="form-control-label blue-grey-500"
                      >Napszaka</label
                    >
                    <br />
                    <label>{{ formData.NapszakCimke.Nev }}</label>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-6">
                    <label class="form-control-label blue-grey-500"
                      >Jellege</label
                    >
                    <br />
                    <label>{{ formData.JellegCimke.Nev }}</label>
                  </div>
                </div>

                <div class="row">
                  <div class="col-md-12">
                    <label class="form-control-label blue-grey-500"
                      >Leírása</label
                    ><span>{{ formData.ErvenyessegKezd }}</span>
                    <br />
                    <label v-html="formData.Leiras"></label>
                  </div>
                </div>

                <div class="row">
                  <div class="col-md-12">
                    <div class="font-size-16  blue-grey-700 block">
                      <span v-if="formData.Resztvevok.length == 0">
                        Nincs felvéve résztvevő
                      </span>
                      <span v-else>
                        Résztvevők
                        <span
                          class="badge badge-pill badge-dark bg-blue-grey-200 blue-grey-500 shadow-sm"
                          >{{ formData.Resztvevok.length }}</span
                        >
                      </span>
                      <span class="float-right">
                        Új résztvevő hozzáadása
                        <button
                          type="button"
                          class="btn btn-pure btn-default icon wb-plus"
                          id="ujresztvevo"
                          @click="UjResztvevo()"
                        ></button>
                      </span>
                    </div>
                  </div>
                </div>
                <ul
                  class="list-group list-group-full w-p100 list-group-dividered"
                >
                  <li
                    class="list-group-item"
                    style="display:block;"
                    v-for="resztvevo in ResztvevoList"
                    :key="resztvevo.FogvatartottId"
                  >
                    <k-esemeny-resztvevo
                      :resztvevo="resztvevo"
                      @resztvevotorles="DeleteResztvevo"
                      @fegyelmiinditas="FegyelmiInditas"
                    ></k-esemeny-resztvevo>
                  </li>
                </ul>

                <b-popover
                  target="#ujresztvevo"
                  triggers="click"
                  :show.sync="ujResztvevoPopoverShow"
                  placement="left"
                  container="slidepanel-esemeny-reszletek"
                  ref="ujResztvevoPopover"
                  custom-class="ujResztvevoPopover"
                >
                  <template slot="title">
                    <b-button
                      @click="CloseUjResztvevoPopover"
                      class="close"
                      aria-label="Close"
                    >
                      <span class="d-inline-block white" aria-hidden="true"
                        >&times;</span
                      >
                    </b-button>
                    Új érintett kiválasztása
                  </template>
                  <div class="pb-5">
                    <div
                      class="form-group form-material  mb-10"
                      data-plugin="formMaterial"
                    >
                      <k-select2
                        :options="Select2FogvatartottKeresoOptions"
                        v-model="ujResztvevoForm.FogvatartottId"
                        placeholder="Kezdje gépelni a fogvatartott nevét vagy azonosítóját"
                      ></k-select2>
                    </div>

                    <div class="row align-items-end">
                      <div class="col-md-7">
                        <div
                          class="form-group form-material  mb-10"
                          data-plugin="formMaterial"
                        >
                          <textarea
                            class="form-control"
                            v-model="ujResztvevoForm.Bizonyitek"
                            placeholder="Bizonyíték megadása"
                          ></textarea>
                          <span class="text-help blue-grey-500"
                            >Bizonyíték a fogvatartottal szemben</span
                          >
                        </div>
                      </div>
                      <div class="col-md-5">
                        <div
                          class="form-group form-material  mb-10"
                          data-plugin="formMaterial"
                        >
                          <k-select2
                            :options="Select2ErintettsegFokaOptions"
                            v-model="ujResztvevoForm.ErintettsegFokaCimId"
                            placeholder="Érintettség foka"
                          ></k-select2>
                        </div>
                      </div>
                    </div>

                    <div class="text-right">
                      <b-button
                        size="sm"
                        @click="AddResztvevo()"
                        variant="info"
                        class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
                        >Hozzáadás</b-button
                      >
                    </div>
                  </div>
                </b-popover>
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
          <b-button @click="Hide()" class="btn-raised">Bezárás</b-button>
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
  name: 'k-slide-panel-esemeny-reszletek',
  props: ['id'],
  data: function() {
    return {
      datePickerOptions: {
        format: 'YYYY.MM.DD HH:mm',
        useCurrent: false,
        locale: moment.locale('hu'),
      },
      visible: false,
      canClose: true,
      esemenyId: null,
      isActive: false,
      isLoading: false,
      fegyelmiInditasaConfirm: {
        isShow: false,
        target: 'slidepanel-esemeny-reszletek',
        confirmText: '',
        fogvatartottId: 0,
      },
      ujResztvevoPopoverShow: false,
      ujResztvevoForm: {
        ErintettsegFokaCimId: 0,
        FogvatartottId: 0,
        Bizonyitek: '',
      },
      formData: {
        ErvenyessegKezd: '',
        EsemenyDatuma: '',
        EsemenyId: 0,
        HelyCimke: {},
        JellegCimke: {},
        Leiras: '',
        Megnevezes: '',
        NapszakCimke: {},
        Resztvevok: [],
        RogzitoSzemely: '',
        ErintettsegFokaOptions: [],
      },
    };
  },
  mounted: function() {
    var vm = this;
    eventBus.$on('Sidebar:' + vm.id, this.InitEvents);
  },
  beforeDestroy() {
    var vm = this;
    eventBus.$off('Sidebar:' + vm.id, this.InitEvents);
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
    InitEvents({ state, data }) {
      if (state) {
        this.LoadEsemenyData(data.esemenyId);
      } else {
        this.Hide();
      }
    },
    LoadEsemenyData: async function(esemenyId) {
      console.log('LoadEsemenyData()', esemenyId);
      try {
        var result = await apiService.GetEsemenyReszletek({ id: esemenyId });

        //this.formData.jellegOptions = result.JellegOptions.sort(function(a, b) {
        //  return ('' + a.text).localeCompare(b.text);
        //});

        //this.formData.napszakOptions = result.NapszakOptions.sort(function(
        //  a,
        //  b
        //) {
        //  return ('' + a.text).localeCompare(b.text);
        //});

        //this.formData.helyOptions = result.HelyOptions.sort(function(a, b) {
        //  return ('' + a.text).localeCompare(b.text);
        //});

        this.formData.ErintettsegFokaOptions = result.ErintettsegFokaOptions.sort(
          function(a, b) {
            return ('' + a.text).localeCompare(b.text);
          }
        );

        for (const key in this.formData) {
          if (this.formData.hasOwnProperty(key) && result.hasOwnProperty(key)) {
            this.$set(this.formData, key, result[key]);
          }
        }

        this.formData.EsemenyDatuma = moment(
          this.formData.EsemenyDatuma
        ).format('YYYY.MM.DD HH:mm');

        this.formData.ErvenyessegKezd = moment(
          this.formData.ErvenyessegKezd
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
          text: 'Hiba esemény mentése során',
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
    UjResztvevo: function() {
      this.ujResztvevoPopoverShow = true;
      this.ujResztvevoForm.ErintettsegFokaCimId = '65';
      this.ujResztvevoForm.Bizonyitek = '';
      this.ujResztvevoForm.FogvatartottId = 0;
    },

    CloseUjResztvevoPopover: function() {
      this.ujResztvevoPopoverShow = false;
    },

    AddResztvevo: async function() {
      if (this.ujResztvevoForm.FogvatartottId != 0) {
        var fogv = await apiService.AddResztvevo({
          fogvatartottId: this.ujResztvevoForm.FogvatartottId,
          esemenyId: this.formData.EsemenyId,
          bizonyitek: this.ujResztvevoForm.Bizonyitek,
          ErintettsegFokaCimId: this.ujResztvevoForm.ErintettsegFokaCimId,
        });

        this.formData.Resztvevok.push({
          ErintettsegFokaCimId: this.ujResztvevoForm.ErintettsegFokaCimId,
          ErintettsegFokaNev: this.formData.ErintettsegFokaOptions.find(
            x => x.id == this.ujResztvevoForm.ErintettsegFokaCimId
          ).text,
          FogvatartottId: this.ujResztvevoForm.FogvatartottId,
          Bizonyitek: this.ujResztvevoForm.Bizonyitek,
          FogvatartottNev: fogv.Nev,
          NyilvantartasiAzonosito: fogv.NyilvantartasiAzonosito,
        });

        this.CloseUjResztvevoPopover();
      }
    },
    DeleteResztvevo(fogvatartottId) {
      console.log('DeleteResztvevo: ' + fogvatartottId);
      var vm = this;
      vm.$set(
        vm.formData,
        'Resztvevok',
        vm.formData.Resztvevok.filter(x => x.FogvatartottId != fogvatartottId)
      );
    },
    FegyelmiInditas(adatok) {
      console.log('FegyelmiInditas: ');
      console.log(adatok);
      var vm = this;
      var resztvevo = vm.formData.Resztvevok.find(
        x => x.FogvatartottId == adatok.fogvatartottId
      );
      resztvevo.FegyelmiUgyszam = adatok.ugyszam;
    },
  },
  computed: {
    ResztvevoList: function() {
      var vm = this;

      var list = this.formData.Resztvevok.slice();

      var tipus = {
        65: 1,
        66: 2,
        67: 3,
        68: 4,
      };

      list = list.sort(function(a, b) {
        return tipus[a.ErintettsegFokaCimId] - tipus[b.ErintettsegFokaCimId];
      });

      return list;
    },
    //Select2JellegOptions: function() {
    //  return {
    //    data: this.formData.jellegOptions,
    //    dropdownParent: $(this.$el),
    //  };
    //},
    //Select2NapszakOptions: function() {
    //  return {
    //    data: this.formData.napszakOptions,
    //    dropdownParent: $(this.$el),
    //  };
    //},
    //Select2HelyOptions: function() {
    //  return {
    //    data: this.formData.helyOptions,
    //    dropdownParent: $(this.$el),
    //  };
    //},
    Select2ErintettsegFokaOptions: function() {
      if (this.formData.ErintettsegFokaOptions.length > 0) {
        var list = this.formData.ErintettsegFokaOptions;
        return {
          data: list,
          dropdownParent: $(this.$el),
          placeholder: 'Érintettség foka',
        };
      }
      return [];
    },
    Select2FogvatartottKeresoOptions: function() {
      this.formData.ErintettsegFokaOptions;
      var fogvatartottIds = this.formData.Resztvevok.map(x => x.FogvatartottId);

      return {
        placeholder: 'Fogvatartott neve vagy azonosítója',
        minimumInputLength: 5,
        dropdownParent: $(this.$el)
          .children()
          .first(),
        ajax: {
          url: settings.baseUrl + 'Api/Esemeny/FindFogvatartottakForSelect',
          dataType: 'json',
          type: 'POST',
          delay: 500,
          data: function(params) {
            var queryParameters = {
              term: params.term,
              fogvatartottIds: fogvatartottIds,
            };
            return queryParameters;
          },
          processResults: function(data) {
            return {
              results: data,
            };
          },
        },
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
</style>
