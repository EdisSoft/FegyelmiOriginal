<template>
  <k-datatable
    id="JutalomUgyekDataTable"
    :options="jutalomUgyekDatatableOptions"
    :dataList="jutalomUgyek"
    :dataKey="'JutalomId'"
    class="pointer table-hover fenyitesek-dt"
    ref="datatable"
  >
    <tfoot></tfoot>
  </k-datatable>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import moment from 'moment';
import $ from 'jquery';
import { eventBus } from '../../main';
import store from '../../store';
import { JutalmiUgyFunctions } from '../../functions/JutalmiUgyFunctions';
import { JutalomUgyStoreTypes } from '../../store/modules/jutalomugy';
import { UserStoreTypes } from '../../store/modules/user';
import {
  timeout,
  excelExportCellBool,
  removeAllHtmlFromString,
} from '../../utils/common';

export default {
  name: 'jutalom-ugyek-table',
  data() {
    return {};
  },
  mounted() {},
  created() {},
  methods: {
    ...mapActions({
      AddJutalomUgySelected: JutalomUgyStoreTypes.actions.addJutalomUgySelected,
      RemoveJutalomUgySelected:
        JutalomUgyStoreTypes.actions.removeJutalomUgySelected,
    }),
    DrawSelectsOnDT(ids) {
      var selected = ids
        .map(m => {
          return `[data-id="${m}"]`;
        })
        .join(',');

      var selectedIdsInDt = Array.from(
        $(this.$refs.datatable.$el)
          .DataTable()
          .rows({ selected: true })
          .data()
      ).map(m => m.JutalomId);
      let removedFromDt = selectedIdsInDt.filter(f => !ids.some(s => s == f));
      let notSelected = removedFromDt
        .map(m => {
          return `[data-id="${m}"]`;
        })
        .join(',');
      if (selected) {
        $(this.$refs.datatable.$el)
          .DataTable()
          .rows([selected], { selected: false })
          .select();
      }
      if (notSelected) {
        $(this.$refs.datatable.$el)
          .DataTable()
          .rows(notSelected, { selected: true })
          .deselect();
      }
    },
    // async JutalomMegtekintes(jutalomId) {
    //   var args = {
    //     jutalomId: jutalomId,
    //     jutalomUgy: this.jutalomUgyek.find(x => x.JutalomId == jutalomId),
    //   };
    //   eventBus.$emit('Sidebar:ugyReszletek', {
    //     state: true,
    //     data: args,
    //   });
    //   // await timeout(50);
    //   // eventBus.$emit('Modal:jutalom-rogzites', {
    //   //   state: true,
    //   //   data: args,
    //   // });
    // },
    UgyReszletekMegtekintes(args) {
      eventBus.$emit('Sidebar:jutalmiUgyReszletek', {
        state: true,
        data: args,
      });
    },
    GetExcelExport() {
      let capitalize = this.$options.filters.camelCaseString;
      let jutalomUgyek = Array.from(
        $(this.$refs.datatable.$el)
          .DataTable()
          .rows({ search: 'applied' })
          .data()
      );
      let exportData = [
        {
          header: 'Rögzítés ideje',
          getCellValue(row) {
            if (row.RogzitesIdeje) {
              return `${moment(row.RogzitesIdeje).format('YYYY.MM.DD. HH:mm')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Fogvatartott név',
          getCellValue(row) {
            return row.FogvatartottNev || '';
          },
        },
        {
          header: 'Nyilvántartási szám',
          getCellValue(row) {
            return row.NyilvantartasiSzam || '';
          },
        },
        {
          header: 'Fogvatartott elhelyezése',
          getCellValue(row) {
            return row.Elhelyezes || '';
          },
        },
        {
          header: 'Intézet',
          getCellValue(row) {
            return row.Intezet || '';
          },
        },
        {
          header: 'Javaslat tevő',
          getCellValue(row) {
            return row.JavaslatTevo || '';
          },
        },
        {
          header: 'Jutalmazás oka',
          getCellValue(row) {
            return removeAllHtmlFromString(row.JutalmazasOka) || '';
          },
        },
        {
          header: 'Jutalom típusa',
          getCellValue(row) {
            return row.JutalomTipusa || '';
          },
        },
      ];
      let body = [];
      let footer = null;
      jutalomUgyek.forEach(row => {
        let excelRow = [];

        exportData.forEach(data => {
          excelRow.push(data.getCellValue(row));
        });
        body.push(excelRow);
      });
      return { header: exportData.map(m => m.header), body, footer };
    },
  },
  computed: {
    ...mapGetters({
      selected: JutalomUgyStoreTypes.getters.getJutalomUgyekSelectedIds,
      userInfo: UserStoreTypes.getters.getUserInfo,
      vanJogosultsaga: UserStoreTypes.getters.vanJogosultsaga,
      // vanReintegraciosTisztJoga:
      //   UserStoreTypes.getters.vanReintegraciosTisztJoga,
      // vanEgyebSzakteruletJoga: UserStoreTypes.getters.vanEgyebSzakteruletJoga,
    }),

    jutalomUgyekDatatableOptions() {
      var vm = this;
      let capitalize = vm.$options.filters.camelCaseString;
      var options = {
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
            mDataProp: null, // esemény
            sTitle: 'Előterjesztés',
            width: '20%',
            mRender: function(data, type, row, meta) {
              var cimkek =
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Rögzítés ideje">' +
                moment(row.RogzitesIdeje).format('YYYY.MM.DD. HH:mm') +
                '</span>' +
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Jutalmazás oka">' +
                row.JutalmazasOka +
                '</span>' +
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Intézet">' +
                row.Intezet +
                '</span>' +
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Javaslat tevő">' +
                row.JavaslatTevo +
                '</span>';
              return cimkek;
            },
          },
          {
            mDataProp: null, // résztvevők
            sTitle: 'Fogvatartott',
            mRender: function(data, type, row, meta) {
              var cimkek =
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Fogvatartott">' +
                row.NyilvantartasiSzam +
                ' - ' +
                row.FogvatartottNev +
                '</span>' +
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Nyilvántartási státusz">' +
                row.NyilvantartasiStatusz +
                '</span>' +
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Elhelyezés">' +
                row.Elhelyezes +
                '</span>';

              if (row.FegyelmiUgyeiSzama > 0) {
                cimkek +=
                  ' <span class="badge text-break badge-outline badge-warning" data-toggle="m-tooltip" data-original-title="Folyamatban lévő fegyelmik">' +
                  row.FegyelmiUgyeiSzama +
                  ' nyitott fegyelmi' +
                  '</span>';
              }
              return cimkek;
            },
          },
          {
            mDataProp: null, // címkék
            sTitle: 'Státusz',
            mRender: function(data, type, row, meta) {
              var cimkek = '';

              if (row.DentesDatuma != null) {
                cimkek +=
                  ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Döntés dátuma">' +
                  moment(row.DentesDatuma).format('YYYY.MM.DD.') +
                  '</span>';
              } else {
                if (!vm.isArchive && row.Hatarido != null) {
                  cimkek +=
                    ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Határidő">' +
                    moment(row.Hatarido).format('YYYY.MM.DD.') +
                    '</span>';
                }
              }
              cimkek +=
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Ügy státusza">' +
                row.Statusz +
                '</span>' +
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Utolsó módosító">' +
                row.UtolsoDonteshozo +
                '</span>' +
                ' <span class="badge text-break badge-outline badge-default" data-toggle="m-tooltip" data-original-title="Jutalom típusa">' +
                row.JutalomTipusa +
                '</span>';
              return cimkek;
            },
          },
          {
            mDataProp: null,
            sTitle: '',
            bSearchable: false,
            bSortable: false,
            sClass: 'dt-td-center dt-td-noClick',
            sWidth: '55px',
            mRender: function(data, type, row, meta) {
              //   var dropdown = `<div class="dropdown">
              //     <button type="button" class="btn btn-icon btn-dark btn-outline btn-round dataTable-dropdown" id="dropdownMenu2${
              //       row.JutalomId
              //     }"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >
              //       <i class="icon wb-more-horizontal" aria-hidden="true"></i>
              //     </button>
              //     <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenu2${
              //       row.JutalomId
              //     }">`;
              //   if (!dropdown) {
              //     return '';
              //   }
              //   return dropdown;
              // },

              var dropdown = JutalmiUgyFunctions.GetBootstrapJutalomUgyMuveletekMenu(
                [data]
              );
              if (vm.isArchive || !dropdown || !vm.vanJogosultsaga) {
                return '';
              }
              return dropdown;
            },
          },
        ],
        responsive: false,
        deferRender: true,
        sDom: `<'row dt-panelmenu'<'col-sm-12 col-md-5  col-xl-5 'i><'col-sm-12 col-md-7 col-xl-7  text-right'<'ujuenyitesbtn'>f>>
          <'row'<'col-sm-12'tr>>
          <'row dt-panelfooter'<'col-sm-12 col-md-6 'lB><'col-sm-12 col-md-6 'p>>`,
        //buttons: ['copy', 'excel', 'pdf', 'print'],
        initComplete: function(settings, json) {
          $(vm.$refs.datatable.$el)
            .DataTable()
            .on('select', function(e, dt, type, indexes) {
              var row = dt.rows(indexes).data()[0];
              console.log(row);
              vm.AddJutalomUgySelected({ value: row.JutalomId });
            });
          $(vm.$refs.datatable.$el)
            .DataTable()
            .on('deselect', function(e, dt, type, indexes) {
              var row = dt.rows(indexes).data()[0];
              vm.RemoveJutalomUgySelected({ value: row.JutalomId });
            });
        },
        drawCallback: function(settings) {
          // $('#DataTables_Table_0 tbody button:first').addClass(
          //   'introJsDokumentumok'
          // );
          vm.$nextTick(() => {
            vm.DrawSelectsOnDT(vm.selected);
          });
          var jutalomTable = '#' + settings.sTableId;
          $(jutalomTable)
            .find('[data-toggle="m-tooltip"]')
            .each(function() {
              $(this).tooltip({
                container: $(this).closest('td'),
                delay: { show: 500, hide: 100 },
              });
            });
          $(jutalomTable + '_filter')
            .find('input')
            .attr('placeholder', 'Keresés ügyre, ügyfélre, címkékre');
        },
        createdRow: function(row, data, rowIndex) {
          $(row).attr('data-id', data.JutalomId);
          if (vm.vanJogosultsaga) {
            $(row).css('cursor', 'pointer');
            $(row)
              .find('.dt-jutalom-ugy-muvelet')
              .click(function(e) {
                let modalId = $(e.target).attr('data-modal-id');
                let modalTipus = $(e.target).attr('data-modal-tipus');
                let functionToRun = $(e.target).attr('data-function-to-run');
                vm.UgyReszletekMegtekintes({
                  jutalomId: data.JutalomId,
                  modalName: modalId,
                  modalType: modalTipus,
                  functionToRun: functionToRun,
                  jutalomUgy: data,
                });
              });

            var selector = 'td:not(:last-child):not(:first-child)';
            if (vm.isArchive) {
              selector = 'td:not(:last-child)';
            }
            if (vm.isFogvKereso) {
              selector = 'td';
            }

            $(row)
              .find(selector)
              .on('click', this, function(e) {
                //eventBus.$emit('Sidebar:fanyFogvatartottAdatok', {});
                vm.UgyReszletekMegtekintes({
                  jutalomId: data.JutalomId,
                  jutalomUgy: data,
                });
              });
          }

          // $(row)
          //   //.find('td:not(.dt-td-noClick)')
          //   .on('click', this, function(e) {
          //     var jutalomId = $(this).attr('data-id');
          //     console.log('JutalomUgyId: ' + jutalomId);
          //     // var args = {};
          //     // args.JutalomId = data.JutalomId;
          //     // args.Urls = IFrameUrls.GetFanyUrl(vm.userInfo.IntezetAzon, data);
          //     // if (args.Urls.length == 0) return;
          //     // eventBus.$emit('Sidebar:fanyFogvatartottAdatok', {
          //     //   state: true,
          //     //   data: args,
          //     // });

          //     vm.$emit('input', jutalomId);
          //     vm.JutalomMegtekintes(jutalomId);
          //   });
        },
        buttons: [
          {
            text: 'Másolás',
            extend: 'copyHtml5',
            exportOptions: {
              columns: ':visible :not(.noExport)',
            },
          },
          {
            extend: 'excelHtml5',
            text: 'Excel',

            exportOptions: {
              columns: ':visible :not(.noExport)',
              customizeData: function(data) {
                let newData = vm.GetExcelExport();

                data.header = newData.header;
                data.body = newData.body;
                data.footer = newData.footer;
              },
            },
          },
          {
            extend: 'print',
            text: 'Nyomtatás',
          },
        ],
      };
      if (this.isArchive) {
        options.select = null;
        options.aoColumns.shift();
        options.order = [[2, 'desc']];
      }

      if (!this.vanJogosultsaga) {
        options.select = null;
        options.aoColumns.shift();
        options.order = [[2, 'desc']];
      }
      return options;
    },
  },
  watch: {
    selected: {
      handler: async function(curr) {
        await this.$nextTick();
        this.DrawSelectsOnDT(curr);
      },
      deep: true,
    },
  },
  components: {},
  props: {
    jutalomUgyek: {
      type: Array,
      required: true,
    },
    value: {
      type: [String, Number],
      default: null,
    },
    isArchive: {
      type: Boolean,
      default: false,
    },
  },
};
</script>
