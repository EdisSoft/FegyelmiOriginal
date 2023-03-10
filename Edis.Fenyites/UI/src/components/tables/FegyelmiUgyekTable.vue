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
        // Elk??vet??
        {
          header: 'Fogvatarott azonos??t??sz??ma ??s neve',
          getCellValue(row) {
            return `${row.AktNyilvantartasiSzam} ${row.FogvatartottNev}`;
          },
        },
        {
          header: 'Fogvatartott nyilv??ntart??si st??tusza',
          getCellValue(row) {
            return row.NyilvantartottStatusz || '';
          },
        },
        {
          header: 'Fogvatartott elhelyez??se',
          getCellValue(row) {
            return `${row.Intezet}/${row.Elhelyezes}/${row.Korlet}/${row.Zarka}`;
          },
        },
        // Esem??ny
        {
          header: 'Esem??ny napja ??s ideje',
          getCellValue(row) {
            if (row.EsemenyDatuma) {
              return `${moment(row.EsemenyDatuma).format('YYYY.MM.DD. HH:mm')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Esem??ny jellege',
          getCellValue(row) {
            return row.Jelleg || '';
          },
        },

        // ??gy
        {
          header: '??gysz??m',
          getCellValue(row) {
            return getUgyszam(row);
          },
        },
        {
          header: '??gy int??zete',
          getCellValue(row) {
            return row.FegyelmiIntezet || '';
          },
        },
        {
          header: 'Elrendel??s ideje',
          getCellValue(row) {
            if (row.DontesDatuma) {
              return `${moment(row.DontesDatuma).format('YYYY.MM.DD.')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Kivizsg??l?? szem??ly',
          getCellValue(row) {
            return capitalize(row.Kivizsgalo) || '';
          },
        },
        {
          header: 'Elrendel?? szem??ly',
          getCellValue(row) {
            return capitalize(row.Elrendelo) || '';
          },
        },
        // St??tusz
        {
          header: 'Jogszab??lyi hat??rid??',
          getCellValue(row) {
            if (row.Hatarido) {
              return `${moment(row.Hatarido).format('YYYY.MM.DD.')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Kivizsg??l??si hat??rid??',
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
          header: 'Fegyelmi ??gy st??tusza',
          getCellValue(row) {
            return row.UgyStatusz || '';
          },
        },
        {
          header: 'Feny??t??s t??pusa',
          getCellValue(row) {
            if (row.HatarozatJogerosFl && row.FenyitesTipus) {
              return row.FenyitesTipus;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Hat??rid?? lej??rt',
          getCellValue(row) {
            if (row.Csuszas > 0) {
              return `Lej??rt ${row.Csuszas} napja`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Jogi k??pviselet',
          getCellValue(row) {
            return excelExportCellBool(row.VanJogiKepviselet);
          },
        },
        {
          header: 'Felf??ggeszt??si javaslat',
          getCellValue(row) {
            return excelExportCellBool(row.FelfuggesztesiJavaslat);
          },
        },
        {
          header: 'Felf??ggesztve',
          getCellValue(row) {
            return excelExportCellBool(row.Felfuggesztve);
          },
        },
        {
          header: 'Hat??rid?? m??dos??t??si javaslat',
          getCellValue(row) {
            return excelExportCellBool(row.HataridoModositasJavaslat);
          },
        },
        // {
        //   header: 'T??rgyal??s kit??z??sre v??r',
        //   getCellValue(row) {
        //     return excelExportCellBool(
        //       row.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IFokuTargyalas &&
        //         !row.ElsofokuTargyalasIdopontja
        //     );
        //   },
        // },
        // {
        //   header: 'Reintegr??ci??b??l viszak??ldve',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.Visszakuldve);
        //   },
        // },
        // {
        //   header: 'Sz??ll??t??sra el??jegyezve',
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
        //   header: 'Szakter??leti v??lem??nyre v??r',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.SzakteruletiVelemenyreVarFL);
        //   },
        // },
        // {
        //   header: 'K??zvet??t??i elj??r??s kezdem??nyezve',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.KozvetitoiEljarasKezdemenyezve);
        //   },
        // },
        // {
        //   header: 'K??zvet??t??i elj??r??sban',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.KozvetitoiEljarasban);
        //   },
        // },
        // {
        //   header: 'Elk??l??n??tve',
        //   getCellValue(row) {
        //     return excelExportCellBool(row.FegyelmiElkulonitesFL);
        //   },
        // },
        // {
        //   header: 'Jel??lje ki a jogi k??pvisel??t',
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
            sTitle: 'Elk??vet??',
            sClass: 'dt-td-center',
            mRender: function(data, type, row, meta) {
              var cimkek = ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott azonos??t??sz??ma ??s neve">
               ${row.FogvatartottNev} ${row.AktNyilvantartasiSzam} 
                </span></br>`;

              cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott nyilv??ntart??si st??tusza">
              ${row.NyilvantartottStatusz}
              </span>`;

              if (row.Elhelyezes) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott elhelyez??se">
                  ${row.Intezet}/${row.Elhelyezes}/${row.Korlet}/${row.Zarka}
                  </span>`;
              } else {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott elhelyez??se">
                    ${row.Intezet}
                    </span>`;
              }
              if (row.Megorzeses) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fogvatartott meg??rz??ses">
                  Meg??rz??ses
                  </span>`;
              }

              return cimkek;
            },
          },

          {
            mDataProp: null,
            sTitle: 'Esem??ny',
            sClass: 'dt-td-center',
            mRender: function(data, type, row, meta) {
              var cimkek = '';

              if (row.EsemenyDatuma) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400" data-toggle="m-tooltip" data-original-title="Esem??ny napja ??s ideje">${moment(
                  row.EsemenyDatuma
                ).format('YYYY.MM.DD. HH:mm')}</span>`;
              }

              if (row.Jelleg) {
                cimkek +=
                  ' <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Fegyelmi v??ts??g t??pusa">' +
                  row.Jelleg +
                  '</span>';
              }

              return cimkek;
            },
          },
          {
            mDataProp: null,
            sTitle: '??gy',
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
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="??gy int??zete">
                ${row.FegyelmiIntezet}
                  </span>`;
              }
              if (row.DontesDatuma) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Elrendel??s ideje">
                  ${moment(row.DontesDatuma).format('YYYY.MM.DD.')}
                  </span>`;
              }
              if (row.Kivizsgalo) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Kivizsg??l?? szem??ly"> Kiv:
                  ${capitalize(row.Kivizsgalo)}
                  </span>`;
              }

              if (row.Elrendelo) {
                cimkek += ` <span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="Elrendel?? szem??ly"> Elr:
                  ${capitalize(row.Elrendelo)}
                  </span>`;
              }
              if (row.KarteritesAzonosito) {
                cimkek +=
                  ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break" data-toggle="m-tooltip" data-original-title="K??rt??r??t??si elj??r??s azonos??t??">
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
            sTitle: 'St??tusz',
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
                cimkek += `<span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Fegyelmi ??gy st??tusz??nak hat??rideje">Hat:
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
                cimkek += `<span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Fegyelmi ??gy st??tusz??nak hat??rideje">Hat:
                  ${moment(row.KivizsgalasiHatarido).format('YYYY.MM.DD.')}
                  </span>`;
              }

              if (row.UgyStatusz) {
                cimkek += `<span class="badge badge-outline badge-default shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Fegyelmi ??gy st??tusza">
                  ${row.UgyStatusz}
                  </span>`;
              }

              if (
                row.MaganelzarasEllenjavaltHatarido &&
                new Date(row.MaganelzarasEllenjavaltHatarido) >=
                  new Date(moment())
              ) {
                cimkek += `<span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Mag??nelz??r??s ideiglenesen ellenjavallt ${moment(
                  row.MaganelzarasEllenjavaltHatarido
                ).format('YYYY.MM.DD')}-ig">
                  Mag??nelz??r??s ellenjavallt
                  </span>`;
              }

              if (row.Csuszas > 0) {
                cimkek += ` <span class="badge badge-outline badge-danger shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Hat??rid?? lej??rt"> Lej??rt
                  ${row.Csuszas} napja
                  </span>`;
              }
              if (row.VanJogiKepviselet) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Jogi k??pviselet">
                  Jogi k??pviseletet k??rt
                  </span>`;
              }
              if (row.FelfuggesztesiJavaslat) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Felf??ggeszt??si javaslat">
                  Felf??ggeszt??si javaslat
                  </span>`;
              }
              if (row.Felfuggesztve) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Felf??ggesztve">
                    Felf??ggesztve
                  </span>`;
              }
              if (row.HataridoModositasJavaslat) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Hat??rid?? m??dos??t??si javaslat">
                  Hat??rid?? m??dos??t??si javaslat
                  </span>`;
              }
              if (
                row.UgyStatuszId == Cimkek.FegyelmiUgyStatusza.IFokuTargyalas &&
                !row.ElsofokuTargyalasIdopontja
              ) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="T??rgyal??s kit??z??sre v??r">
                  T??rgyal??s kit??z??sre v??r
                  </span>`;
              }
              if (row.Visszakuldve) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Reintegr??ci??b??l viszak??ldve">
                  Visszak??ldve
                  </span>`;
              }

              if (row.SzallitasraElojegyezve) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Sz??ll??t??sra el??jegyezve">
                  Sz??ll??t??sra el??jegyezve:
                  ${moment(row.SzallitasraElojegyezve).format('YYYY.MM.DD.')}
                  </span>`;
              }
              if (row.SzakteruletiVelemenyreVarFL) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Szakter??leti v??lem??nyre v??r">
                  Szakter??leti v??lem??nyre v??r
                  </span>`;
              }

              if (row.KozvetitoiEljarasKezdemenyezve) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="K??zvet??t??i elj??r??s kezdem??nyezve">
                  K??zvet??t??i elj??r??s j??v??hagy??sra v??r
                  </span>`;
              }
              if (row.KozvetitoiEljarasban) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="K??zvet??t??i elj??r??sban">
                  K??zvet??t??i elj??r??sban
                  </span>`;
              }
              if (row.FegyelmiElkulonitesFL) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Elk??l??n??tve">
                  Elk??l??n??tve
                  </span>`;
              }
              if (row.Lezarva && row.HatarozatJogerosFl && row.FenyitesTipus) {
                cimkek += ` <span class="badge badge-outline badge-info shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Feny??t??s t??pusa">
                  ${row.FenyitesTipus}
                  </span>`;
              }
              if (row.JogiKepviseletetKer) {
                cimkek += ` <span class="badge badge-outline badge-warning shadow-sm font-weight-400 text-break mr-5" data-toggle="m-tooltip" data-original-title="Jel??lje ki a jogi k??pvisel??t">
                  Jel??lje ki a jogi k??pvisel??t
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
              'Keres??s ??gyf??lre, azonos??t??ra, ??gysz??mra, d??tumokra'
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
            text: 'M??sol??s',
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
            text: 'Nyomtat??s',
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
