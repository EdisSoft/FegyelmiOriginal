import $ from 'jquery';
import dataTable from 'datatables.net-bs4';
import jszip from 'jszip';
window.JSZip = jszip;
import 'datatables.net-bs4/css/dataTables.bootstrap4.css';
import 'datatables.net-buttons-bs4/css/buttons.bootstrap4.css';
import 'datatables.net-responsive-bs4/css/responsive.bootstrap4.css';
import 'datatables.net-select-bs4/css/select.bootstrap4.css';
import 'datatables.net-buttons/js/dataTables.buttons.js';
import 'datatables.net-buttons/js/buttons.colVis.js';
import 'datatables.net-select-bs4/js/select.bootstrap4.js';
import 'datatables.net-buttons/js/buttons.html5.js';
// import 'datatables.net-buttons/js/buttons.flash.js';
import 'datatables.net-buttons/js/buttons.print.js';
import 'datatables.net-buttons-bs4/js/buttons.bootstrap4.js';
import 'datatables.net-responsive/js/dataTables.responsive.js';
import 'datatables.net-responsive-bs4/js/responsive.bootstrap4.js';

import markjs from './markjs/mark';
import markjsDt from './markjs/datatables.mark';

var hunlang = {
  sEmptyTable: 'Nincs rendelkezésre álló adat',
  sInfo: 'Találatok: _START_ - _END_, Összesen: _TOTAL_ ',
  sInfoEmpty: 'Nincs találat',
  sInfoFiltered: '',
  sInfoPostFix: '',
  sInfoThousands: ' ',
  sLengthMenu: '_MENU_',
  sLoadingRecords: 'Betöltés...',
  sProcessing: 'Feldolgozás...',
  sSearch: '',
  sZeroRecords: 'Nincs a keresésnek megfelelő találat',
  oPaginate: {
    sFirst: 'Első',
    sPrevious: 'Előző',
    sNext: 'Következő',
    sLast: 'Utolsó',
  },
  oAria: {
    sSortAscending: ': aktiválja a növekvő rendezéshez',
    sSortDescending: ': aktiválja a csökkenő rendezéshez',
  },
  // select extension innentől
  select: {
    rows: {
      _: 'Kijelölve %d sor',
      0: '',
      1: 'Kijelölve %d sor',
    },
  },
};
$.fn.dataTable.ext.errMode = 'throw';
$.extend(true, $.fn.dataTable.defaults, {
  oLanguage: hunlang,
  iDisplayLength: 10,
  aLengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, 'Mind']],
  bDeferRender: true,
  dom: 'lBCfrtip',
  mark: true,
  //colReorder: true,
  language: {
    buttons: {
      copyTitle: 'Másolás',
      copySuccess: {
        _: 'A vágólapra másolva %d sor',
        0: 'Nincs másolható adat',
        1: 'Másolva 1 sor',
      },
    },
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
      // action: function(e, dt, button, config) {
      //   console.log('excel');
      //   config.filename =
      //     $(dt.header())
      //       .closest('table')
      //       .attr('data-export-filename') +
      //     '_' +
      //     $.format.date(new Date(), 'yyyyMMdd_hhmmss');
      //   $.fn.dataTable.ext.buttons.excelHtml5.action(e, dt, button, config);
      // },
      // exportOptions: {
      //   columns: ':visible :not(.noExport)',
      //   format: {
      //     body: function(data, row, column, node) {
      //       if (data.indexOf) {
      //         if (data.indexOf(' Ft') != -1) {
      //           return data.replace(/[Ft ]/g, '');
      //         } else {
      //           return data;
      //         }
      //       }
      //       return data;
      //     },
      //   },
      // },
    },
    // {
    //   extend: 'csvHtml5',
    //   // action: function(e, dt, button, config) {
    //   //   debugger;
    //   //   console.log('csv');
    //   //   config.filename = 'teszt';
    //   //   // $(dt.header())
    //   //   //   .closest('table')
    //   //   //   .attr('data-export-filename') +
    //   //   // '_' +
    //   //   // $.format.date(new Date(), 'yyyyMMdd_hhmmss');
    //   //   console.log('csv1');
    //   //   $.fn.dataTable.ext.buttons.csvHtml5.action(e, dt, button, config);
    //   //   console.log('csv2');
    //   // },
    //   exportOptions: {
    //     columns: ':visible :not(.noExport)',
    //   },
    //   fieldSeparator: '\t',
    //   bom: true,
    // },
    {
      extend: 'print',
      text: 'Nyomtatás',
    },
  ],
});

var translate_re = /[a,á,e,é,i,í,o,ó,ö,ő,ü,ű,ú]/g;

var translate = {
  a: 'aa',
  á: 'ab',
  e: 'ea',
  é: 'eb',
  i: 'ia',
  í: 'ib',
  o: 'oa',
  ó: 'ob',
  ö: 'oc',
  ő: 'od',
  u: 'ua',
  ú: 'ub',
  ü: 'uc',
  ű: 'ud',
};

var hun_accute = function(d) {
  return d.replace(translate_re, function(match) {
    return translate[match];
  });
};

var _orig_html_pre = $.fn.dataTable.ext.type.order['html-pre'];
$.fn.dataTable.ext.type.order['html-pre'] = function(d) {
  return hun_accute(_orig_html_pre(d));
};
var _orig_string_pre = $.fn.dataTable.ext.type.order['string-pre'];
$.fn.dataTable.ext.type.order['string-pre'] = function(d) {
  var r = hun_accute(_orig_string_pre(d));
  return r;
};
