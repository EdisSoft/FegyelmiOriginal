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
          14. Fegyelmi ügyek összevonása
          <p class="mt-10 mb-0 font-size-12">
            Vonja össze a fogvatartott futó fegyelmi ügyeit, hogy közös
            tárgyalást lehessen kitűzni
          </p>
        </h4>
      </div>
      <div class="modal-body">
        <div>
          <div>
            <p>
              Jelölje ki
              <b
                >{{ formData.FogvatartottNytsz }}
                {{ formData.FogvatartottNev }}</b
              >
              összevonandó fegyelmi ügyeit. A kiválasztott ügyeket a
              <b>{{ formData.Ugyszam }}</b> ügyhöz rendeljük.
            </p>
          </div>
          <div class="row pr-10">
            <div class="col-md-12">
              <k-vuelidate-error-extractor
                class="form-group"
                :validator="$v.formData.values.selected"
              >
                <!--<textarea class="leiras summernote"></textarea>-->
                <k-datatable
                  :options="UgyListaDatatableOptions"
                  :dataList="formData.FegyelmiUgyList"
                  :dataKey="'FegyelmiUgyId'"
                  class="pointer table-hover iktatottDokumentumok-dt"
                  ref="datatable"
                >
                  <tfoot></tfoot>
                </k-datatable>
              </k-vuelidate-error-extractor>
            </div>
          </div>
        </div>
      </div>
      <div
        class="modal-footer bg-blue-grey-400 py-15 d-flex justify-content-between"
      >
        <div class="text-right" style="width: 100%;">
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
            Fegyelmi ügyeket összevonom
          </button>
        </div>
      </div>
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
import { required, minValue, minLength } from 'vuelidate/lib/validators';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import moment from 'moment';
import $ from 'jquery';
import { useSimpleModalHandler } from '../../utils/modal';
import { hidrateForm } from '../../utils/vueUtils';
import { deselectDatatable } from '../../utils/common';

export default {
  name: 'ugy-osszevonas',
  data() {
    return {
      isFormLoading: false,
      fegyelmiUgyId: 0,
      mentesFolyamatban: false,
      title: 'Fegyelmi ügyek összevonása',
      formData: {
        FogvatartottNytsz: '',
        FogvatartottNev: '',
        Ugyszam: '',
        FegyelmiUgyList: [],
        values: {
          FegyelmiUgyId: 0,
          selected: [],
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
        selected: {
          required,
        },
      },
    },
  },
  methods: {
    InitEvents: function({ state, data }) {
      if (state) {
        console.log(data);
        this.fegyelmiUgyId = data.fegyelmiUgyId;
        if (data.fegyelmiUgyIds) {
          this.formData.values.selected = data.fegyelmiUgyIds.filter(
            x => x != data.fegyelmiUgyId
          );
        }
        this.LoadFegyelmiUgyData(data.fegyelmiUgyId);
      } else {
        this.Hide();
      }
    },
    LoadFegyelmiUgyData: async function(fegyelmiUgyId) {
      this.isFormLoading = true;
      try {
        var result = await apiService.GetFegyelmiUgyDataOsszevonashoz({
          data: { fegyelmiUgyId },
        });
        if (result.isSuccess == false) {
          NotificationFunctions.WarningAlert(this.title, result.message);
          this.Hide();
        }
        this.formData.FogvatartottNytsz = result.FogvatartottNytsz;
        this.formData.FogvatartottNev = result.FogvatartottNev;
        this.formData.Ugyszam = result.Ugyszam;
        //this.formData.values.FegyelmiUgyList = result.ValaszthatoFegyelmiUgyek;
        this.$set(
          this.formData,
          'FegyelmiUgyList',
          result.ValaszthatoFegyelmiUgyek
        );
        hidrateForm(this, this.formData.values, result);

        //this.formData.values.Datuma = moment(
        //  this.formData.values.Datuma
        //).format('YYYY.MM.DD');

        this.$v.$reset();
        this.isFormLoading = false;
      } catch (err) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Adatok letöltése sikertelen',
          errorObj: err,
        });
        this.Hide();
      }
    },
    Save: async function() {
      this.mentesFolyamatban = true;
      try {
        if (this.$v.$invalid) {
          this.$v.$touch();
          this.mentesFolyamatban = false;
          return;
        }
        // this.mentesFolyamatban = false;
        // return;
        var result = await apiService.FegyelmiUgyOsszevonasMentes({
          data: {
            fegyelmiUgyId: this.formData.values.FegyelmiUgyId,
            alUgyList: this.formData.values.selected,
          },
        });
        if (result.success == true) {
          NotificationFunctions.SuccessAlert(
            'Ügy összevonás mentés',
            result.message
          );
          eventBus.$emit('Sidebar:ugyReszletek:refresh');
          deselectDatatable();
          this.Hide();
          this.$v.$reset();
        } else {
          NotificationFunctions.WarningAlert(
            'Ügy összevonás mentés',
            result.message
          );
        }
        this.mentesFolyamatban = false;
      } catch (err) {
        var errorMsg = 'Ügy összevonás során: ' + err;
        NotificationFunctions.AjaxError({
          title: 'Ügy összevonás mentés',
          text: '',
          errorObj: err,
        });
        console.log(errorMsg);
        this.mentesFolyamatban = false;
      }
    },
  },
  computed: {
    ...mapGetters({}),
    UgyListaDatatableOptions: function() {
      var vm = this;
      var options = {
        initComplete: function(settings, json) {
          $(vm.$refs.datatable.$el)
            .DataTable()
            .on('select', function(e, dt, type, indexes) {
              var row = dt.rows(indexes).data()[0];
              if (
                !vm.formData.values.selected.some(s => s == row.FegyelmiUgyId)
              ) {
                //vm.formData.values.selected.push(row.FegyelmiUgyId);
                vm.$v.formData.values.selected.$model.push(row.FegyelmiUgyId);
                vm.$v.$touch();
              }
            });
          $(vm.$refs.datatable.$el)
            .DataTable()
            .on('deselect', function(e, dt, type, indexes) {
              var row = dt.rows(indexes).data()[0];
              //vm.formData.values.selected = vm.formData.values.selected.filter(x => x != row.FegyelmiUgyId);
              vm.$v.formData.values.selected.$model = vm.$v.formData.values.selected.$model.filter(
                x => x != row.FegyelmiUgyId
              );
              vm.$v.$touch();
            });
        },
        drawCallback: function(settings) {
          vm.$nextTick(() => {
            if (vm.formData.values.selected) {
              var selected = vm.formData.values.selected
                .map(m => {
                  return `[data-id="${m}"]`;
                })
                .join(',');
              if (selected) {
                $(vm.$refs.datatable.$el)
                  .DataTable()
                  .rows(selected, { selected: false })
                  .select();
              }
            }
          });

          //$('#DataTables_Table_0_filter')
          //  .find('input')
          //  .attr(
          //    'placeholder',
          //    'Keresés ügyfélre, azonosítóra, ügyszámra, dátumokra'
          //  );
          var ugyTable = '#' + settings.sTableId;
          $(ugyTable)
            .find('[data-toggle="m-tooltip"]')
            .each(function() {
              $(this).tooltip({
                container: $(this).closest('td'),
                delay: { show: 500, hide: 100 },
              });
            });
        },
        order: [[2, 'desc']],
        select: {
          style: 'multiple',
          selector: 'td:first-child',
        },
        aoColumns: [
          {
            mDataProp: null,
            sTitle: '',
            sWidth: 50,
            bSortable: false,
            sClass: ' select-checkbox remarkcheckbox',
            mRender: function(data, type, row, meta) {
              return '';
            },
          },
          {
            mDataProp: null,
            sTitle: 'Esemény',
            sClass: 'dt-td-center',
            mRender: function(data, type, row, meta) {
              var html =
                '<span class="fegyossz-jelleg text-center">' +
                row.EsemenyJellege +
                '</span>';
              return html;
            },
          },
          {
            mDataProp: null,
            sTitle: 'Fegyelmi ügy adatai',
            sClass: 'dt-td-center',
            mRender: function(data, type, row, meta) {
              var html =
                '<span>' +
                vm.$options.filters.toShortDate(
                  row.FegyelmiUgyElrendelesenekDatuma
                ) +
                '</span><br>' +
                '<span class="fegyossz-ugyszam">' +
                row.Ugyszam +
                '</span>';
              return html;
            },
          },
          {
            mDataProp: null,
            sTitle: 'Cimkék',
            sClass: 'dt-td-center',
            mRender: function(data, type, row, meta) {
              var html =
                '<span class="fegyossz-cimke">Elr: ' +
                row.ElrendeloSzemely +
                '</span><br>' +
                '<span class="fegyossz-cimke">Kiv: ' +
                row.KivizsgaloSzemely +
                '</span>';
              return html;
            },
          },
        ],
        paging: false,
        responsive: false,
        deferRender: true,
        sDom: `<'row dt-panelmenu'<'col-sm-12 col-md-5  col-xl-5 'i><'col-sm-12 col-md-7 col-xl-7  text-right'<'ujuenyitesbtn'>f>>
          <'row'<'col-sm-12'tr>>`,
        //<'row dt-panelfooter'<'col-sm-12 col-md-6 'lB><'col-sm-12 col-md-6 'p>>`,

        createdRow: function(row, data, rowIndex) {
          $(row).attr('data-id', data.FegyelmiUgyId);
          $(row).css('cursor', 'pointer');
        },
      };
      return options;
    },
  },
  watch: {
    id: {
      handler: function(newValue, oldValue) {
        if (newValue && oldValue != newValue) {
          this.fegyelmiUgy = this.$store.getters[
            FegyelmiUgyStoreTypes.getters.getFegyelmiUgyById
          ](newValue);
        }
      },
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
.iktatottDokumentumok-dt {
  width: 100% !important;
}
</style>
