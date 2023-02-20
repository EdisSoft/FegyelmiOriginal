<template>
  <div>
    <k-datatable
      id="FegyemiUgyekDataTable"
      :options="fegyelmiUgyDatatableOptions"
      :dataList="fegyelmiUgyek"
      :dataKey="'FegyelmiUgyId'"
      class="pointer table-hover fegyelmiUgyek-dt"
      ref="datatable"
    >
    </k-datatable>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import $ from 'jquery';
import moment from 'moment';
import { getUgyszam } from '../../utils/fenyitesUtils';
import { FegyelmiUgyFunctions } from '../../functions/FegyelmiUgyFunctions';
import { eventBus } from '../../main';
import { FenyitesStoreTypes } from '../../store/modules/fenyites';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import Cimkek from '../../data/enums/Cimkek';
import { UserStoreTypes } from '../../store/modules/user';
import Intezetek from '../../data/enums/intezetek';
import IFrameUrls from '../../data/enums/iframeUrls';
import { excelExportCellBool } from '../../utils/common';

export default {
  name: 'fegyelmi-ugyek-table',
  data() {
    return {};
  },
  mounted() {},
  created() {},
  methods: {
    ...mapActions({
      AddFegyelmiUgySelected:
        FegyelmiUgyStoreTypes.actions.addFegyelmiUgySelected,
      RemoveFegyelmiUgySelected:
        FegyelmiUgyStoreTypes.actions.removeFegyelmiUgySelected,
    }),
    UgyReszletekMegtekintes(args) {
      eventBus.$emit('Sidebar:ugyReszletek', {
        state: true,
        data: args,
      });
    },
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
      ).map(m => m.FegyelmiUgyId);
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
    GetExcelExport() {
      let capitalize = this.$options.filters.camelCaseString;
      let fegyelmiUgyek = Array.from(
        $(this.$refs.datatable.$el)
          .DataTable()
          .rows({ search: 'applied' })
          .data()
      );
      let exportData = [
        // Elkövető
        {
          header: 'Fogvatarott azonosítószáma és neve',
          getCellValue(row) {
            return `${row.AktNyilvantartasiSzam} ${row.FogvatartottNev}`;
          },
        },
        {
          header: 'Fogvatartott nyilvántartási státusza',
          getCellValue(row) {
            return row.NyilvantartottStatusz || '';
          },
        },
        {
          header: 'Fogvatartott elhelyezése',
          getCellValue(row) {
            return `${row.Intezet}/${row.Elhelyezes}/${row.Korlet}/${row.Zarka}`;
          },
        },
        // Esemény
        {
          header: 'Esemény napja és ideje',
          getCellValue(row) {
            if (row.EsemenyDatuma) {
              return `${moment(row.EsemenyDatuma).format('YYYY.MM.DD. HH:mm')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Esemény jellege',
          getCellValue(row) {
            return row.Jelleg || '';
          },
        },

        // Ügy
        {
          header: 'Ügyszám',
          getCellValue(row) {
            return getUgyszam(row);
          },
        },
        {
          header: 'Ügy intézete',
          getCellValue(row) {
            return row.FegyelmiIntezet || '';
          },
        },
        {
          header: 'Elrendelés ideje',
          getCellValue(row) {
            if (row.DontesDatuma) {
              return `${moment(row.DontesDatuma).format('YYYY.MM.DD.')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Kivizsgáló személy',
          getCellValue(row) {
            return capitalize(row.Kivizsgalo) || '';
          },
        },
        {
          header: 'Elrendelő személy',
          getCellValue(row) {
            return capitalize(row.Elrendelo) || '';
          },
        },
        // Státusz
        {
          header: 'Jogszabályi határidő',
          getCellValue(row) {
            if (row.Hatarido) {
              return `${moment(row.Hatarido).format('YYYY.MM.DD.')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Kivizsgálási határidő',
          getCellValue(row) {
            if (row.KivizsgalasiHatarido) {
              return `${moment(row.KivizsgalasiHatarido).format(
                'YYYY.MM.DD.'
              )}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Fegyelmi ügy státusza',
          getCellValue(row) {
            return row.UgyStatusz || '';
          },
        },
        {
          header: 'Fenyítés típusa',
          getCellValue(row) {
            if (row.HatarozatJogerosFl && row.FenyitesTipus) {
              return row.FenyitesTipus;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Határidő lejárt',
          getCellValue(row) {
            if (row.Csuszas > 0) {
              return `Lejárt ${row.Csuszas} napja`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Jogi képviselet',
          getCellValue(row) {
            return excelExportCellBool(row.VanJogiKepviselet);
          },
        },
        {
          header: 'Felfüggesztési javaslat',
          getCellValue(row) {
            return excelExportCellBool(row.FelfuggesztesiJavaslat);
          },
        },
        {
          header: 'Felfüggesztve',
          getCellValue(row) {
            return excelExportCellBool(row.Felfuggesztve);
          },
        },
        {
          header: 'Határidő módosítási javaslat',
          getCellValue(row) {
            return excelExportCellBool(row.HataridoModositasJavaslat);
          },
        },
        // {
        //   header: 'Tárgyalás kitűzésre vár',
        //   getCellValue(row) {
        //     return excelExportCellBool(
        //       row.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IFokuTargyalas &&
        //         !row.ElsofokuTargyalasIdopontja
        //     );
        //   },
        // },
        // {
        //   header: 'Reintegrációból viszaküldve',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.Visszakuldve);
        //   },
        // },
        // {
        //   header: 'Szállításra előjegyezve',
        //   getCellValue(row) {
        //     if (row.SzallitasraElojegyezve) {
        //       return `${moment(row.SzallitasraElojegyezve).format(
        //         'YYYY.MM.DD.'
        //       )}`;
        //     } else {
        //       return '';
        //     }
        //   },
        // },
        // {
        //   header: 'Szakterületi véleményre vár',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.SzakteruletiVelemenyreVarFL);
        //   },
        // },
        // {
        //   header: 'Közvetítői eljárás kezdeményezve',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.KozvetitoiEljarasKezdemenyezve);
        //   },
        // },
        // {
        //   header: 'Közvetítői eljárásban',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.KozvetitoiEljarasban);
        //   },
        // },
        // {
        //   header: 'Elkülönítve',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.FegyelmiElkulonitesFL);
        //   },
        // },
        // {
        //   header: 'Jelölje ki a jogi képviselőt',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.JogiKepviseletetKer);
        //   },
        // },
      ];
      let body = [];
      let footer = null;
      fegyelmiUgyek.forEach(row => {
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
      selected: FegyelmiUgyStoreTypes.getters.getFegyelmiUgyekSelectedIds,
      userInfo: UserStoreTypes.getters.getUserInfo,
    }),
    isBvop() {
      if (!this.userInfo) {
        return false;
      }
      return this.userInfo.RogzitoIntezet.Id == Intezetek.BVOP;
    },
    fegyelmiUgyDatatableOptions() {
      var vm = this;
      let capitalize = vm.$options.filters.camelCaseString;
      var options = {
        initComplete: function(settings, json) {
          $(vm.$refs.datatable.$el)
            .DataTable()
            .on('select', function(e, dt, type, indexes) {
              var row = dt.rows(indexes).data()[0];
              vm.AddFegyelmiUgySelected({ value: row.FegyelmiUgyId });
            });
          $(vm.$refs.datatable.$el)
            .DataTable()
            .on('deselect', function(e, dt, type, indexes) {
              var row = dt.rows(indexes).data()[0];
              vm.RemoveFegyelmiUgySelected({ value: row.FegyelmiUgyId });
            });
        },
        select: {
          style: 'multiple',
          selector: 'td:first-child',
        },
        order: [[3, 'desc']],
        bSortClasses: false,
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
            sTitle: 'Elkövető',
            sClass: 'dt-td-center',
            mRender: function(data, type, row, meta) {
              var cimkek = ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott azonosítószáma és neve">
               ${row.FogvatartottNev} ${row.AktNyilvantartasiSzam} 
                </span></br>`;

              cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott nyilvántartási státusza">
              ${row.NyilvantartottStatusz}
              </span>`;

              if (row.Elhelyezes) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott elhelyezése">
                  ${row.Intezet}/${row.Elhelyezes}/${row.Korlet}/${row.Zarka}
                  </span>`;
              } else {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott elhelyezése">
                    ${row.Intezet}
                    </span>`;
              }
              if (row.Megorzeses) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott megőrzéses">
                  Megőrzéses
                  </span>`;
              }

              return cimkek;
            },
          },

          {
            mDataProp: null,
            sTitle: 'Esemény',
            sClass: 'dt-td-center',
            mRender: function(data, type, row, meta) {
              var cimkek = '';

              if (row.EsemenyDatuma) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400" data-toggle="m-tooltip" data-original-title="Esemény napja és ideje">${moment(
                  row.EsemenyDatuma
                ).format('YYYY.MM.DD. HH:mm')}</span>`;
              }

              if (row.Jelleg) {
                cimkek +=
                  ' <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fegyelmi vétség típusa">' +
                  row.Jelleg +
                  '</span>';
              }

              return cimkek;
            },
          },
          {
            mDataProp: null,
            sTitle: 'Ügy',
            sClass: 'dt-td-center',
            mRender: function(data, type, row, meta) {
              if (type == 'sort') {
                let azonStr =
                  row.UgyIntezetAzon +
                  '_' +
                  row.UgyEve +
                  '_' +
                  (row.UgySzama + '').padStart(6, 0);
                return azonStr;
              }
              var cimkek = '';

              cimkek += `<span class="blue-grey-500 font-weight-700 font-italic">${getUgyszam(
                row
              )}</span>`;

              if (row.FegyelmiIntezet && vm.isBvop) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Ügy intézete">
                ${row.FegyelmiIntezet}
                  </span>`;
              }
              if (row.DontesDatuma) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Elrendelés ideje">
                  ${moment(row.DontesDatuma).format('YYYY.MM.DD.')}
                  </span>`;
              }
              if (row.Kivizsgalo) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Kivizsgáló személy"> Kiv:
                  ${capitalize(row.Kivizsgalo)}
                  </span>`;
              }

              if (row.Elrendelo) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Elrendelő személy"> Elr:
                  ${capitalize(row.Elrendelo)}
                  </span>`;
              }
              if (row.KarteritesAzonosito) {
                cimkek +=
                  ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Kártérítési eljárás azonosító">
                  ` +
                  row.KarteritesAzonosito +
                  `
                  </span>`;
              }

              return cimkek;
            },
          },
          {
            mDataProp: null,
            sTitle: 'Státusz',
            sClass: 'dt-td-center',
            //width: '60%',
            mRender: function(data, type, row, meta) {
              var cimkek = '';

              if (
                row.Hatarido &&
                row.UgyStatuszId !=
                  Cimkek.FegyelmiUgyStatusza.KivizsgalasFolyamatban &&
                row.UgyStatuszId !=
                  Cimkek.FegyelmiUgyStatusza.ReintegraciosTisztiJogkorben
              ) {
                cimkek += `<span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Fegyelmi ügy státuszának határideje">Hat:
                  ${moment(row.Hatarido).format('YYYY.MM.DD.')}
                  </span>`;
              }

              if (
                row.KivizsgalasiHatarido &&
                (row.UgyStatuszId ==
                  Cimkek.FegyelmiUgyStatusza.KivizsgalasFolyamatban ||
                  row.UgyStatuszId ==
                    Cimkek.FegyelmiUgyStatusza.ReintegraciosTisztiJogkorben)
              ) {
                cimkek += `<span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Fegyelmi ügy státuszának határideje">Hat:
                  ${moment(row.KivizsgalasiHatarido).format('YYYY.MM.DD.')}
                  </span>`;
              }

              if (row.UgyStatusz) {
                cimkek += `<span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Fegyelmi ügy státusza">
                  ${row.UgyStatusz}
                  </span>`;
              }

              if (
                row.MaganelzarasEllenjavaltHatarido &&
                new Date(row.MaganelzarasEllenjavaltHatarido) >=
                  new Date(moment())
              ) {
                cimkek += `<span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Magánelzárás ideiglenesen ellenjavallt ${moment(
                  row.MaganelzarasEllenjavaltHatarido
                ).format('YYYY.MM.DD')}-ig">
                  Magánelzárás ellenjavallt
                  </span>`;
              }

              if (row.Csuszas > 0) {
                cimkek += ` <span class="badge badge-outline badge-danger shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Határidő lejárt"> Lejárt
                  ${row.Csuszas} napja
                  </span>`;
              }
              if (row.VanJogiKepviselet) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Jogi képviselet">
                  Jogi képviseletet kért
                  </span>`;
              }
              if (row.FelfuggesztesiJavaslat) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Felfüggesztési javaslat">
                  Felfüggesztési javaslat
                  </span>`;
              }
              if (row.Felfuggesztve) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Felfüggesztve">
                    Felfüggesztve
                  </span>`;
              }
              if (row.HataridoModositasJavaslat) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Határidő módosítási javaslat">
                  Határidő módosítási javaslat
                  </span>`;
              }
              if (
                row.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IFokuTargyalas &&
                !row.ElsofokuTargyalasIdopontja
              ) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Tárgyalás kitűzésre vár">
                  Tárgyalás kitűzésre vár
                  </span>`;
              }
              if (row.Visszakuldve) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Reintegrációból viszaküldve">
                  Visszaküldve
                  </span>`;
              }

              if (row.SzallitasraElojegyezve) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Szállításra előjegyezve">
                  Szállításra előjegyezve:
                  ${moment(row.SzallitasraElojegyezve).format('YYYY.MM.DD.')}
                  </span>`;
              }
              if (row.SzakteruletiVelemenyreVarFL) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Szakterületi véleményre vár">
                  Szakterületi véleményre vár
                  </span>`;
              }

              if (row.KozvetitoiEljarasKezdemenyezve) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Közvetítői eljárás kezdeményezve">
                  Közvetítői eljárás jóváhagyásra vár
                  </span>`;
              }
              if (row.KozvetitoiEljarasban) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Közvetítői eljárásban">
                  Közvetítői eljárásban
                  </span>`;
              }
              if (row.FegyelmiElkulonitesFL) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Elkülönítve">
                  Elkülönítve
                  </span>`;
              }
              if (row.Lezarva && row.HatarozatJogerosFl && row.FenyitesTipus) {
                cimkek += ` <span class="badge badge-outline badge-info shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Fenyítés típusa">
                  ${row.FenyitesTipus}
                  </span>`;
              }
              if (row.JogiKepviseletetKer) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Jelölje ki a jogi képviselőt">
                  Jelölje ki a jogi képviselőt
                  </span>`;
              }
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
              //if (row.FoFegyelmiUgyId) {
              //  return '';
              //}
              var dropdown = FegyelmiUgyFunctions.GetBootstrapFegyelmiUgyMuveletekMenu(
                [data]
              );
              if (!dropdown) {
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
        drawCallback: function(settings) {
          var ugyTable = '#' + settings.sTableId;
          vm.$nextTick(() => {
            vm.DrawSelectsOnDT(vm.selected);
          });
          $(ugyTable + ' tbody button:first').addClass('introJsDokumentumok');
          $(ugyTable + '_wrapper')
            .find('input')
            .attr(
              'placeholder',
              'Keresés ügyfélre, azonosítóra, ügyszámra, dátumokra'
            );
          $(ugyTable)
            .find('[data-toggle="m-tooltip"]')
            .each(function() {
              $(this).tooltip({
                container: $(this).closest('td'),
                delay: { show: 500, hide: 100 },
              });
            });
        },
        createdRow: function(row, data, rowIndex) {
          $(row).attr('data-id', data.FegyelmiUgyId);
          $(row).css('cursor', 'pointer');

          $(row)
            .find('.dt-fegyelmi-ugy-muvelet')
            .click(function(e) {
              let modalId = $(e.target).attr('data-modal-id');
              let modalTipus = $(e.target).attr('data-modal-tipus');
              let functionToRun = $(e.target).attr('data-function-to-run');
              vm.UgyReszletekMegtekintes({
                fegyelmiUgyId: data.FegyelmiUgyId,
                modalName: modalId,
                modalType: modalTipus,
                functionToRun: functionToRun,
                fegyelmiUgy: data,
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
              // if (data.Fany) {
              //   var Id = $(this).attr('data-id');
              //   var args = {};
              //   args.FegyelmiUgyId = data.FegyelmiUgyId;
              //   args.Urls = IFrameUrls.GetFanyArchiveUrl(
              //     vm.userInfo.RogzitoIntezet.Azonosito,
              //     data
              //   );
              //   if (args.Urls.length == 0) return;
              //   eventBus.$emit('Sidebar:ugyReszletek:close', {});
              //   eventBus.$emit('Sidebar:fanyFogvatartottAdatok', {
              //     state: true,
              //     data: args,
              //   });
              // } else {
              eventBus.$emit('Sidebar:fanyFogvatartottAdatok', {});
              vm.UgyReszletekMegtekintes({
                fegyelmiUgyId: data.FegyelmiUgyId,
                fegyelmiUgy: data,
              });
              // }
            });
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
      if (this.isFogvKereso) {
        options.select = null;
        options.aoColumns.shift();
        options.order = [[2, 'desc']];
        options.aoColumns.pop();
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
    fegyelmiUgyek: {
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
    isFogvKereso: {
      type: Boolean,
      default: false,
    },
  },
};
</script>
<style scoped>
.ujuenyitesbtn,
.ujuenyitesbtn + .dataTables_filter {
  display: inline-block;
}
.counter-number {
  font-size: 36px;
}
</style>
