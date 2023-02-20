import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from '../fonts/timesnewroman/vfs_fonts';
pdfMake.vfs = pdfFonts.pdfMake.vfs;
import htmlToPdfMake from 'html-to-pdfmake';
import { apiService } from '../main';
import settings from '../data/settings';
import { FegyelmiUgyFunctions } from './FegyelmiUgyFunctions';
import { Modal } from 'bootstrap/dist/js/bootstrap.bundle';
import ApiService from '../services/ApiService';
import { isFulfilled } from 'q';
import Cimkek from '../data/enums/Cimkek';

class Nyomtatvany {
  async ErtesitoLapNyomtatas(fegyelmiUgyId, iktatasId) {
    var url =
      settings.baseUrl +
      `Api/Nyomtatvany/ErtesitoLapNyomtatvany?fegyelmiUgyId=${fegyelmiUgyId}`;
    if (iktatasId) {
      url += '&iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async FegyelmiEljarastKezdemenyezoLapNyomtatas(fegyelmiUgyId, iktatasId) {
    var url =
      settings.baseUrl +
      `Api/Nyomtatvany/FegyelmiEljarastKezdemenyezoNyomtatvany?fegyelmiUgyId=${fegyelmiUgyId}`;
    if (iktatasId) {
      url += '&iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async LefoglalasiJegyzokonyvNyomtatas(fegyelmiUgyId, iktatasId) {
    var url =
      settings.baseUrl +
      `Api/Nyomtatvany/LefoglalasiJegyzokonyvNyomtatvany?fegyelmiUgyId=${fegyelmiUgyId}`;
    if (iktatasId) {
      url += '&iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async IratosszesitoFegyelmiUgybenNyomtatas(fegyelmiUgyId, iktatasId) {
    var url =
      settings.baseUrl +
      `Api/Nyomtatvany/IratosszesitoFegyelmiUgybenNyomtatvany?fegyelmiUgyId=${fegyelmiUgyId}`;
    if (iktatasId) {
      url += '&iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async ElkulonitoLapNyomtatas({ fegyelmiUgyId, iktatasId }) {
    var url = settings.baseUrl + 'Api/Dokumentum/ElkulonitesiLapNyomtatvany';
    if (fegyelmiUgyId && iktatasId) {
      url += `?fegyelmiUgyId=${fegyelmiUgyId}` + '&iktatasId=' + iktatasId;
    } else if (fegyelmiUgyId) {
      url += `?fegyelmiUgyId=${fegyelmiUgyId}`;
    } else if (iktatasId) {
      url += '?iktatasId=' + iktatasId;
    }

    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async AlairasMegtagadasaNyomtatvany({ fegyelmiUgyId, iktatasId }) {
    var url =
      settings.baseUrl +
      `Api/Dokumentum/AlairasMegtagadasaNyomtatvany?fegyelmiUgyId=${fegyelmiUgyId}`;
    if (iktatasId) {
      url += '&iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async VedoKirendeleseNyomtatas({ naplobejegyzesId, iktatasId }) {
    var url = settings.baseUrl + 'Api/Nyomtatvany/VedoKirendeleseNyomtatvany?';
    if (naplobejegyzesId) url += `naplobejegyzesId=${naplobejegyzesId}`;
    if (iktatasId) {
      url += 'iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async VedoTelefonosTajekoztatasaNyomtatasDocX({
    naplobejegyzesId,
    iktatasId,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/FegyemiVedoTelefononTortenoTajekoztatasa?';
    if (naplobejegyzesId) url += `naplobejegyzesId=${naplobejegyzesId}`;
    if (iktatasId) {
      url += 'iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async MeghatalmazottVedoKirendeleseNyomtatas({
    naplobejegyzesId,
    iktatasId,
  }) {
    var url =
      settings.baseUrl +
      'Api/Nyomtatvany/MeghatalmazottVedoKirendeleseNyomtatvany?';
    if (naplobejegyzesId) url += `naplobejegyzesId=${naplobejegyzesId}`;
    if (iktatasId) {
      url += 'iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async OsszefoglaloJelentesDocxNyomtatas({ naplobejegyzesId, iktatasId }) {
    console.log('IDK');
    console.log(naplobejegyzesId);
    console.log(iktatasId);
    var url =
      settings.baseUrl + 'Api/Dokumentum/OsszefoglaloJelentesDocxNyomtatvany?';
    if (naplobejegyzesId) url += `naplobejegyzesId=${naplobejegyzesId}`;
    if (iktatasId) {
      url += 'iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async BuntetoFeljelentesDocxNyomtatas({ naplobejegyzesId, iktatasId }) {
    console.log('IDK');
    console.log(naplobejegyzesId);
    console.log(iktatasId);
    var url =
      settings.baseUrl + 'Api/Dokumentum/BuntetoFeljelentesDocxNyomtatvany?';
    if (naplobejegyzesId) url += `naplobejegyzesId=${naplobejegyzesId}`;
    if (iktatasId) {
      url += 'iktatasId=' + iktatasId;
    }
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async KarjelentoLapErtesitoLapDocxNyomtatas({ esemenyId }) {
    console.log('Kárjelentő lap értesítő lap id');
    console.log(esemenyId);
    var url =
      settings.baseUrl +
      'Api/Dokumentum/KarjelentoLapErtesitoLapDocxNyomtatvany?';
    if (esemenyId) url += `esemenyId=${esemenyId}`;
    console.log(url);
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async ErtesitoLapByEsemenyIdDocxNyomtatas({ esemenyId }) {
    console.log('Értesítő lap id');
    console.log(esemenyId);
    var url =
      settings.baseUrl +
      'Api/Dokumentum/ErtesitoLapByEsemenyIdDocxNyomtatvany?';
    if (esemenyId) url += `esemenyId=${esemenyId}`;
    console.log(url);
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async KarjelentoLapDocxNyomtatas({ esemenyId, iktatasId, fegyelmiUgyId }) {
    console.log('Kárjelentő lap ids');
    console.log(esemenyId);
    console.log(iktatasId);
    console.log(fegyelmiUgyId);
    var url = settings.baseUrl + 'Api/Dokumentum/KarjelentoLapDocxNyomtatvany?';
    if (esemenyId) url += `esemenyId=${esemenyId}`;
    if (iktatasId) {
      url += `iktatasId=${iktatasId}`;
    }
    if (fegyelmiUgyId) {
      url += `&fegyelmiUgyId=${fegyelmiUgyId}`;
    }
    console.log(url);
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async OtosSzamuMellekletDocxNyomtatas({ fegyelmiUgyId }) {
    console.log('IDK');
    console.log(fegyelmiUgyId);
    //console.log(iktatasId);
    var url =
      settings.baseUrl +
      `Api/Dokumentum/OtosSzamuMellekletDocxNyomtatvany?fegyelmiUgyId=${fegyelmiUgyId}`;
    //if (iktatasId) {
    //  url += 'iktatasId=' + iktatasId;
    //}
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }
  async FeljelentesNyomtatas({ fegyelmiUgyId }) {
    console.log('IDK');
    console.log(fegyelmiUgyId);
    //console.log(iktatasId);
    var url =
      settings.baseUrl +
      `Api/Dokumentum/FeljelentesNyomtatasDocxNyomtatvany?fegyelmiUgyId=${fegyelmiUgyId}`;
    //if (iktatasId) {
    //  url += 'iktatasId=' + iktatasId;
    //}
    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  async TeritesmentesTelefonalasJutalomNyomtatas({ jutalmiUgyId }) {
    var url =
      settings.baseUrl +
      `Api/Jutalom/TeritesmentesTelefonalasJutalomNyomtatas?jutalomUgyId=${jutalmiUgyId}`;

    FegyelmiUgyFunctions.NyomtatvanyLetoltes(url);
  }

  // async FegyelmiLapNyomtatas(fegyelmiUgyId, iktatasId) {
  //   if (pdfMake.vfs == undefined) {
  //     pdfMake.vfs = pdfFonts.pdfMake.vfs;
  //   }
  //   var model = await apiService.FegyelmiLapNyomtatas({
  //     fegyelmiUgyId: fegyelmiUgyId,
  //     iktatasId: iktatasId,
  //   });
  //   var esemenyLeiras = htmlToPdfMake(
  //     `
  //   <div style="margin-left: 20px; text-align: justify;">` +
  //       model.EsemenyLeirasa +
  //       '</div>'
  //   );
  //   var docDefinition = {
  //     header: function(currentPage, pageCount, pageSize) {
  //       // you can apply any logic and return any valid pdfmake element

  //       return [
  //         {
  //           margin: [40, 30, 40, 0],
  //           style: 'tableExample',
  //           table: {
  //             widths: ['*', '*'],
  //             body: [
  //               [
  //                 {
  //                   text: model.IntezetNev,
  //                   border: [false, false, false, true],
  //                   margin: [-5, 0, 0, 5],
  //                 },
  //                 {
  //                   text: model.Iktatoszam,
  //                   alignment: 'right',
  //                   border: [false, false, false, true],
  //                   margin: [0, 0, 0, 5],
  //                 },
  //               ],
  //             ],
  //           },
  //         },
  //       ];
  //     },
  //     content: [
  //       { text: model.UgySzama },
  //       {
  //         text: 'FEGYELMI LAP',
  //         style: 'header',
  //       },
  //       {
  //         style: 'tableExample',
  //         table: {
  //           widths: [150, 1, '*'],
  //           body: [
  //             [
  //               { text: 'Név', border: [false, false, false, false] },
  //               { text: ':', border: [false, false, false, false] },
  //               { text: model.Nev, border: [false, false, false, false] },
  //             ],
  //             [
  //               { text: 'Azonosító', border: [false, false, false, false] },
  //               { text: ':', border: [false, false, false, false] },
  //               { text: model.Azonosito, border: [false, false, false, false] },
  //             ],
  //             [
  //               {
  //                 text: 'Születési hely',
  //                 border: [false, false, false, false],
  //               },
  //               { text: ':', border: [false, false, false, false] },
  //               {
  //                 text: model.SzuletesiHely,
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //             [
  //               { text: 'Születési idő', border: [false, false, false, false] },
  //               { text: ':', border: [false, false, false, false] },
  //               {
  //                 text: model.SzuletesiIdo,
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //             [
  //               { text: 'Anyja neve', border: [false, false, false, false] },
  //               { text: ':', border: [false, false, false, false] },
  //               { text: model.AnyjaNeve, border: [false, false, false, false] },
  //             ],
  //             [
  //               { text: 'Elhelyezése', border: [false, false, false, false] },
  //               { text: ':', border: [false, false, false, false] },
  //               {
  //                 text: model.FogvElhelyezese,
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //             [
  //               {
  //                 text: 'Bv.I-ben tart. jogcím/fok',
  //                 border: [false, false, false, false],
  //               },
  //               { text: ':', border: [false, false, false, false] },
  //               {
  //                 text: model.TartozkodasFokaJogcime,
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //             [
  //               {
  //                 text: 'Aktuális szabadulási idő',
  //                 border: [false, false, false, false],
  //               },
  //               { text: ':', border: [false, false, false, false] },
  //               {
  //                 text: model.AktualisSzabadulasiIdo,
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //             // [
  //             //   {
  //             //     text: 'Esemény sorszáma',
  //             //     border: [false, false, false, false],
  //             //     margin: [0, 20, 0, 0],
  //             //   },
  //             //   {
  //             //     text: ':',
  //             //     border: [false, false, false, false],
  //             //     margin: [0, 20, 0, 0],
  //             //   },
  //             //   {
  //             //     text: model.EsemenySorszama,
  //             //     border: [false, false, false, false],
  //             //     margin: [0, 20, 0, 0],
  //             //   },
  //             // ],
  //             [
  //               {
  //                 text: 'Esemény észlelése',
  //                 border: [false, false, false, false],
  //                 margin: [0, 20, 0, 0],
  //               },
  //               {
  //                 text: ':',
  //                 border: [false, false, false, false],
  //                 margin: [0, 20, 0, 0],
  //               },
  //               {
  //                 text: model.EsemenyEszlelese,
  //                 border: [false, false, false, false],
  //                 margin: [0, 20, 0, 0],
  //               },
  //             ],
  //             [
  //               {
  //                 text: 'Eseményt észlelte',
  //                 border: [false, false, false, false],
  //               },
  //               { text: ':', border: [false, false, false, false] },
  //               {
  //                 text: model.EsemenytEszlelte,
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //           ],
  //         },
  //       },
  //       {
  //         margin: [0, 20, 0, 0],
  //         text: 'Esemény leírása:',
  //       },
  //       esemenyLeiras,
  //       {
  //         text: 'Kérem az eljárás lefolytatását',
  //         margin: [0, 30, 0, 0],
  //       },
  //       {
  //         margin: [-5, 20, 0, 0],
  //         table: {
  //           widths: ['auto', 50, 1, 20, 1, 20, 1],
  //           body: [
  //             [
  //               {
  //                 text: 'Elrendelve: ' + model.Telepules + ',',
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 canvas: [
  //                   {
  //                     type: 'line',
  //                     x1: 0,
  //                     y1: 12,
  //                     x2: 50,
  //                     y2: 12,
  //                     dash: { length: 2 },
  //                     lineWidth: 1,
  //                   },
  //                 ],
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 margin: [-5, 0, 0, 0],
  //                 text: '.',
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 canvas: [
  //                   {
  //                     type: 'line',
  //                     x1: 0,
  //                     y1: 12,
  //                     x2: 20,
  //                     y2: 12,
  //                     dash: { length: 2 },
  //                     lineWidth: 1,
  //                   },
  //                 ],
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 margin: [-5, 0, 0, 0],
  //                 text: '.',
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 canvas: [
  //                   {
  //                     type: 'line',
  //                     x1: 0,
  //                     y1: 12,
  //                     x2: 20,
  //                     y2: 12,
  //                     dash: { length: 2 },
  //                     lineWidth: 1,
  //                   },
  //                 ],
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 margin: [-5, 0, 0, 0],
  //                 text: '.',
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //             [
  //               {
  //                 text: 'Kivizsgáló: ',
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 canvas: [
  //                   {
  //                     type: 'line',
  //                     x1: 0,
  //                     y1: 12,
  //                     x2: 135,
  //                     y2: 12,
  //                     dash: { length: 2 },
  //                     lineWidth: 1,
  //                     colSpan: 5,
  //                   },
  //                 ],
  //                 border: [false, false, false, false],
  //               },
  //               { text: '', border: [false, false, false, false] },
  //               { text: '', border: [false, false, false, false] },
  //               { text: '', border: [false, false, false, false] },
  //               { text: '', border: [false, false, false, false] },
  //               { text: '', border: [false, false, false, false] },
  //             ],
  //             [
  //               {
  //                 text: 'Határidő: ',
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 canvas: [
  //                   {
  //                     type: 'line',
  //                     x1: 0,
  //                     y1: 12,
  //                     x2: 50,
  //                     y2: 12,
  //                     dash: { length: 2 },
  //                     lineWidth: 1,
  //                   },
  //                 ],
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 margin: [-5, 0, 0, 0],
  //                 text: '.',
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 canvas: [
  //                   {
  //                     type: 'line',
  //                     x1: 0,
  //                     y1: 12,
  //                     x2: 20,
  //                     y2: 12,
  //                     dash: { length: 2 },
  //                     lineWidth: 1,
  //                   },
  //                 ],
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 margin: [-5, 0, 0, 0],
  //                 text: '.',
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 canvas: [
  //                   {
  //                     type: 'line',
  //                     x1: 0,
  //                     y1: 12,
  //                     x2: 20,
  //                     y2: 12,
  //                     dash: { length: 2 },
  //                     lineWidth: 1,
  //                   },
  //                 ],
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 margin: [-5, 0, 0, 0],
  //                 text: '.',
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //           ],
  //         },
  //       },
  //       {
  //         margin: [-5, 30, 0, 15],
  //         table: {
  //           widths: [150, '*', '*'],
  //           body: [
  //             [
  //               {
  //                 text: '',
  //                 border: [false, false, false, false],
  //               },
  //               {
  //                 text: 'Elrendelő:',
  //                 border: [false, false, false, false],
  //                 alignment: 'right',
  //               },
  //               {
  //                 canvas: [
  //                   {
  //                     type: 'line',
  //                     x1: 0,
  //                     y1: 12,
  //                     x2: 120,
  //                     y2: 12,
  //                     dash: { length: 2 },
  //                     lineWidth: 1,
  //                   },
  //                 ],
  //                 border: [false, false, false, false],
  //               },
  //             ],
  //           ],
  //         },
  //       },
  //     ],
  //     pageSize: 'A4',
  //     pageMargins: [40, 60, 40, 60],
  //     styles: {
  //       header: {
  //         fontSize: 16,
  //         bold: true,
  //         alignment: 'center',
  //         margin: [0, 20, 0, 0],
  //       },
  //       subheader: {
  //         fontSize: 15,
  //         bold: true,
  //       },
  //       quote: {
  //         italics: true,
  //       },
  //       small: {
  //         fontSize: 8,
  //       },
  //       footersmall: {
  //         fontSize: 6,
  //       },
  //       tableExample: {
  //         margin: [-5, 30, 0, 15],
  //       },
  //       tableHeader: {
  //         bold: true,
  //         fontSize: 8,
  //         //color: 'black',
  //         margin: [0, 10, 5, 10],
  //       },
  //       tableHeader2: {
  //         bold: true,
  //         fontSize: 8,
  //         alignment: 'center',
  //         //color: 'black',
  //         margin: [0, 10, 0, 10],
  //       },
  //       tableCell: {
  //         fontSize: 8,
  //         alignment: 'right',
  //         margin: [0, 5, 5, 5],
  //       },
  //     },
  //     defaultStyle: {
  //       columnGap: 20,
  //       font: 'TimesNewRoman',
  //       fontSize: 12,
  //     },
  //   };

  //
  // pdfMake.fonts = {
  //   TimesNewRoman: {
  //     normal: 'TimesNewRoman.ttf',
  //     bold: 'TimesNewRoman.ttf',
  //     italics: 'TimesNewRoman.ttf',
  //     bolditalics: 'TimesNewRoman.ttf',
  //   },
  // };
  // pdfMake.createPdf(docDefinition).download();

  //   /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
  //   // pdfMake.createPdf(docDefinition).print();
  // }

  async FegyelmiLapokNyomtatasa({ fegyelmiUgyIds, iktatasId }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var model;

    if (iktatasId != null) {
      model = await apiService.FegyelmiLapNyomtatas({
        iktatasId: iktatasId,
      });
    } else if (fegyelmiUgyIds != null) {
      model = await apiService.FegyelmiLapokNyomtatasaEsemenyRogzitesnel({
        fegyelmiUgyIds: fegyelmiUgyIds,
      });
    } else return null;

    // model = await apiService.FegyelmiLapokNyomtatasaEsemenyRogzitesnel({
    //   fegyelmiUgyIds: fegyelmiUgyIds,
    // });

    // if (naplobejegyzesIds != null) {
    //   model = await apiService.VedoKirendeleseNyilatkozatNyomtatasByNaploIds({
    //     naplobejegyzesIds,
    //   });
    // } else if (iktatasIds != null) {
    //   model = await apiService.VedoKirendeleseNyilatkozatokNyomtatasByIktatasIds(
    //     {
    //       iktatasIds,
    //     }
    //   );
    // } else return null;

    //var fogvCount = model.length;
    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          var esemenyLeiras = htmlToPdfMake(
            '<div style="margin-left: 20px; text-align: justify;">' +
              item.EsemenyLeirasa +
              '</div>'
          );
          return {
            id:
              fegyelmiUgyIds && fegyelmiUgyIds.length > 1 && index >= 1
                ? 'NoBreak'
                : '',
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQICAgICAgICAgICAwMDAwMDAwMDAwEBAQEBAQECAQECAgIBAgIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD/8AAEQgAWAAwAwERAAIRAQMRAf/EAJQAAAICAwEAAAAAAAAAAAAAAAgJBgoABQcDAQACAwEAAAAAAAAAAAAAAAACAwABBAUQAAAHAQAABgIABgMBAAAAAAECAwQFBgcIABESExQJFRchIkMWGApCIyQyEQACAQIEAwQIBAUFAAAAAAABAhEhAwAxEgRBUWFxIjIT8IGRobFCIwXB0ZIU4fFSYjNyotJDNP/aAAwDAQACEQMRAD8Av8eJiYzxMTFZH7WfsC3XkXqydn8C19/bYumc/wB8T1rIZagalLZ7iK0dnp7LB6VLyNJqdwrj52lJXuuzL15JskyRzJJJBZ4Rm5WSJ0dtZsXduTuAVQOIcGskgRGZoGgSKmeuAYwYGcenpyw8fix1fpHm3OpjR9tpfQ8/PNZSdZ6xn0jGztUsVZlph89qqcdZoiErDC1mjIBZBsrJEi475aqZjC3THz8+exDMSBHMTMHiOlZpwywdeOCn8ViYzxMTGeJiYUt1v9l0nUdXJxpwplf+XvdEs1WcytOZyZ4bEucq6mqVo70XpvU0iKRtSiYx0qBEYFmdaelHIA2TSROomYxIqt3n1CzMFlANYNBNJFJBIzBwJPBY1YTl3Fg+2c8fC3Dv7s2/3E92xqZDYlub4NDOE84qxOjOYKRoCVBsaaDacukZO5/rs3DN4ywNCQEe1dicGaavrdeMu53Vy3eSxtEUJcuWwdUEkFtJJMgCBUkVGnxQIK2BAUtHeevsJz7QI9mWNLzVmR7nvjrBOKes+vsIGo6a2r8s60zTaDsbaBd2bCNH3S05+3Qxi8SGOzFJvsk3g5ly7iX/AOajX4GRKu3FM7bx0RcPkL5qhrbA6TQNQgTME0FADSCTGDAkQJU/D1e/DQ8x746L4x1CA5o+0aoIs6xaBdNcP7loQLWDJr2hGoGcr1zYTNYuKe0a6xbIhlFHi0c0SVZpGcuEgQQcyRwZRdP0VINKZnqeYHGtI4yMWshRrNcPCYP2MoxZycY8aSMbItG7+PkGDhF4xfsXiJHDR4zdtzqN3TR03UKdNQhjEOQwGKIgID4TlQ54LAE/Z122++vTkG79RMKBHaUrUbHQK6atSlp/tJp5X24RNOSkxfEjJd1ILRrqYTUTj26IOH5/JFM5DHAwGi62g0EGvYJ7a5U54mK8X1r6hpXMmOyv2XW+2Pn3PFsscpkXbdDctbAjrEx0/pWnsn9v2uAiXiRCxgY3tt+TyyBpCqR3ZYsjyQ+UL5woi5bubxa41guGFoxAA0rAkAaZB1qyszTyBkqcLVqTpPXn/HswZvCeAM9Zv+Kcpdtwkxomp8I4hotvv9A02ySWh162WTpTYoW75vZLd+cMuw1Cq1+k1RoWLSlE3TNnZ2b8CJ/IikjppKPqO4t6ls91Qad4wdRjMGa8M4ypi1LairVETPby4iBnPPhXBGdq4JzpybqfP++5BnpsitN8ltfwdek8+x8fnzbXL9qHPukQGPkRqldYtq+51xhcUkY6BmitSvWjeTc++qdqmAJEu4ZIsFPMU94A/LBBYjjBEyMjnngWYLcCjxMCfZA/HC5rHD9A6xylD89W+2yrGu/UXRm9h+wiJmL1bT3PctBjIy6zZIXMLa4EZGCi81zGBbaVTLBMHfg8UlIFoZEot3joloj3botoxRLhKgwJgmADpNJ8DxBA1EZjFtXxLJFfXEZcaE8cGj/r29z3jqTna4YJd6iCD7ihnn2St9RWeDFSOrwswW5O6LYnuaOomNkKCm9zKDhJBsHqUaP20iRVsRFECphd615UAZyQRAADDTIEEyK5zSIrEklNMqRjo339Z+w07jvOaZNXCKo8BLdK58pLWC1Q87ZaK3WhKlodnrLS21itgWYnTTVzgY2OhyJKJi3sD1i4AFDokSPVpEuEozBSVMTEkyPDObASYAYkAgCsiGZEc8V7Z3jm+s3+i5q81vSKtSRulK0a8V2159qshE4vc57Rue5sOgOgI+LXa1+4LT8/oEjYIl/F/EftpGrg6duCgi4VR51uwLtk7kaV3CwkMV1MmkkxC91EgI0jVWhymld3E6YOdZ8XaBn16Y6Qny7pMUwntiL1XrK12f1uz1JfoRqXQarS53MaR0hQM3gNlsb5/Jv9RPz1TWupTEs/agq4STeMXLlu8RZKquiaBs9nctpaYk7fUpYAq0NpIKrC1JKgKTJGoahIIxRu3LbKIAdgxOfCPbVuQy642dh4+0CXmSMnnZ927Tk8nvkLP5hrmEykxOU1zfpTDehrq2z2mqyt1vrSt9BQ/wCu2KXyYSYk/KFsiYKJNXCgJqMtfbdqLNxrf0ka3JWQREqJJKglIYiKE5kngZd9QZ4McePX4DHDbPzFoqFO1Ofb9aSejs9Pissba/p0Hne9Fzm3zFkxboN+4wbWoh5LvrJdr+3UyaAq0Yd04+O2b3Jq3dNSqC3br5bO22xv2VuMoUXNIMqAqg0aSImJMGkjxGcFLhSSAK9azxyzw2b6Q8AdYt2r1yopabN897lcFC3DOpOBnK1N1Z7UtTsVBz9ztJpUpGN0u7rPqGgrVX7BNFpHV106akAwHAS6bN5n2ih7RlrjHzCUPggaQFggMSWqDMCs5ptksNTDSQMu2pPtp6jjoH3B0uY3HXDUCz6jqUHnuZxeZ3es0WnTFWj6ktdXWH95XYbRY4edp9kTscqzmMjhRbfJMKDZJBT20yqnBUh27du9Zui5WGiOEHyxEcR3jiI7G444Kwj9M/HCzIfiKgO7oygVNG2csaGsS1OBJJ7kRFyQ6HS36nT9tf8ATQmSff2kYSGVKAet2b3RD/j4w7nY7K3dCrbt6SiE0GZVCficaldzBk8fx/LEEovFWdXGvQ9nnbnpzuZkaTmrp299jFxVUTvOPfXBeLDH+4ti6ywRLue6jtBithOKQIGYJmA3xCmUttrtUFpVtrpfUTSkqzgEDKYX44W9x1dQPmMf7Z+ONDGcKY9D59JOYacucMSr5LfdIgYyJgMEiYaIt8Dz303pbN+wiIzEG7NmU1my6LBUUipqqtRcpGP/AOgwl02tpZvbV9xfWb+hpNagXiigzUgKBQ0mowPmt5vljwxyHp+PPE+muELOwmv7mN+zUcNHqTaudGWjK6nhzyyvFM7tm8Fi1T0AnPib9kkvDUOGjlVhfmUUeNXL0UipOCELhOx2nlT5duShNFHi0kiZERIBOdMq4ZruawpNJ9v5YInhjKVOf+0ec7TnWqaui50Jg0pN9ipCRoRK9bqq9iuZLMav2CKr+d1757SPntTlXTI5lCrsjqEBBQpBXI41C1bsbO1dtIqPcvKDHEFLhMdpQdYHCpxnt3nuhGb5gZ/ST8RhnXTKfACv2H6wTu8cTCMDlvng2cfudZFFn801z6XJaRghdnIzM4/CCIO/PzUBmKn9EVvGj7efuA3t39j5kQs6edNPrzj+WED/ANL6v8fumFj3TjxK3/19RceZFeJxd/IHyEj+N+X8z8oUDeXpce/8384QPP8AqfNAP6oB46mr7wFzuaSY6TGXbHDOOmNH0+mPJu3/ANe8qZPiK8QFQ9tuCXxH8SCIoka1wzIEvYce2KJWCcMKHl/L7BY/0fyA08F5n3uAdV7TEipiK1HTOo69cSLfTCoK2tkJMvyyHuO9oczYhcc+rVIk9gZQMbY2lfzy5czdfwqrZg1tEPNRrBrJ1t0YiT9w3ODJMAVEwCX1eM2zS7etOCuu5DSKVjcsCKHnnHXI5Lot7l3R+Ppyw2mzVfkBfgKv2ST77/F5ot1RfNWa9UhXqUQJbZrldNNTuNPCmrVdSvIgk+tEzH/CLHlWafF9wRAyRjeMgsze8nQZFNMwfDGfrnDw3zE1n07cAPzY5qDjqzmQmf68beqLG6tcYKoa2aEja8NwrlffciQcQ6GMiI6IjQOyYsCNRcIt0SPPZ98AN7nma99aazt7NtlKEbhKTP8A1XjzMdnDphShVdFXwjVH6GwU/wBiv7GsXXOgUfL8E2zbZwmO43bJYcpr9OlI2vRMnlv2AZLFlnX9tvVNKg+kbZpDAEkUSuB+GRysYSgj6TrsX7a+bYM+YWBHZ9P/AIn3YG2R59xfmJB9QUD8RgZoir9QMrm2nl+DeyvgJavJXM4kpmNncfhXXS37XRMCP7zDzejU/wCYUvP+Dv8A6vP/AJeB3IF26GTwhVHrCqD8DjUpgV6/jiI57nnV1ZqsLCynBnYYPGFUy6GXBtUcaXRB7UMf+uKjTAJq/vEnrRLO8s2oET+Qe4gmxU8g+WBUxdQ3lQPAGn1s5EfqGFuCzIRkDJ/THxx64dk2xdJRM7X84u+C5JHq801FOYZ9AntLS7pq77mXVePnSQhqnNtI1spVY+2HcrkOu596Qbi39z2xFUdNp0tfbrdxqi6bq+LSRovsZEhuzp7hBb+qW4wOHMT05/xw1ixcd67K8lQtcS2LnlDTEut9R6beWtwS3Diqn7PsetOl6fHPiThbMZWJZ6UCSbpRcfecNDAKZSn8i4zGhajSKZ9CKHn6o6HDCCXniDOWAEpOSani3cvJFR0m/wCCX8rxNpKw73FXVrVcMfxE7zbnblKyJWSSkU003iVDRcNjImKPuLLpmAQTIJiukfs7KLEDcqM5P+O7HAcM+ZqIwhbYtNbStJFf9DYLXrTo7buWu49X0DKMzzDSo20894PT5trfdBs1EfxUjWYztDYWq0X+EpFvaybGRgc7kWyhlDIKIu1G/kUyZjmKj9vuXuvf2+gAEAlqiO5kAQZlhnQ+o4QFY33dCAwMVE5hTzHLEWYfab2k/sKNdJy/zaRZa4OqUDoehNBMiWUaa4GPqL+2GKAoLI0x5vAN/wDXxQ9Ih7n8PEvLv7LaddgsVU+F/mAOerqBhwG5ORX2dv8Ad0xpav8AbV2RaoljMs+WudGrSQiafNIlddB38Vis7nRuVb6wKoRLFjlBw1jetIVBUoCIe/GPfIRKKImojfrpl7HfmO6/AsDPe/t9/Q4FjfUgSskwKdJ/q5YT7o8fiqecRD/fMip1wtsnzJcoFGxEycmqxVSmD8x9bIxzD+5F6u+m4WEQ0ebYKMXCqKRUnQFeKAiKKiqe/b6b20Fll1XWD6Qc5/cMSRJoIAr2cwMGGK34kadM0y/ngxLpesbPytB4fIYBOMue0/s06XcV3QJDKax/iPJwraa6Ks0Cxg3iTleJQrJlZFqhEv1YlCBdTSIsmjpR4mVMU27Ra35YWCtuiwZJNvuwBlmKmAMzInDGuKG1/KDnn6dmOXcOR2LI9V81vsgymOzkhLVEws3INcmRyxeyKxkDx1/EiKkLCS0vHMZZZ8omdykUPddKLEAQces97i2E2tkMqi8NwgMRP+O8anjXiJB5zkm3rBtq86q+3Q2HBda8kWPqHt2bpUV0PaMMZv8Amii3pFvX83zu7KWl9ASnROMWFYr+8xr9RgnXqnu5iGRblEouJRBY4gZJMDNtFrSsRBRzUGeS9n9I44MKockGrQT6hFMR1t9Peps5hOcbfYHoxXyVqXuJDKc8c/qJhNOtEDUlze3+AIQWw2/zUKn5eRW4+z/EoeYg/wBQd+rwBPGAAAIypA4TTDQSMssauA+mLQ63GM4eM+wDSyMWEbXIhuVbn7AFlvx9TqXP1KhE1Vj17/sXSg+Y6kVVT0h7qiDs4gAuz+myVYKCBKAwa8SSePHUfdgSAxBOYMj2R8MVrNG0XRNTqON5yfE9LtUFCaxSIW82qB2mPy6L1ehZvVuiMh0h1KWDG5s+g0aD0e030zFlDwTeUk15dgpALsSPXLFi+fYazb2wtNcNu9cLBCqwA3nG6oliNNIGo1jvjUCcKZS9wgTkM+lDE+7lgoC7FXpnHIzDHvG0G2oMFr1m6FbJm+wHpFUyeh3bOZhedsRrNG5u5aS2bvG4uHiLZu2WhU4RY9rjW6pWE9+Iwnztrd/b3XuWrhJANJMQCJKiGAyWjalikqWrTKFNRkESe7lwOUR1joTiU/WJH61/ktzfTZ+k/go+I6S2JRo3t22y2p6Qem2dpHa3CfhZaXj5qauFaxSv4Wxrcy5npdtOgnOwz1NspGumzhfduPJezbsAtpTS4MRqYBxzqDrYmBQ+uDC6SC0yD8QRw7ezFgf7ZuWbxt2Owmv4yz02Y2rndpoU1A0jJNOt2S3DVqbcqW/grPQWVlpU7XJR9Kxc2hD2mGj1nXxH03XGzY5P/R6i1YYBtDsBbaJmqyMi3TgTwBJrlgyDmJnFfKsX/nCatUcYexdESgHOrS0GdpM977rXXbatN+ohpRWEzETe1R0zCOmVDTFqqm8SQdIIgY6gFOAnCbtLtq75SINICAkCe8VUkSOJknFppIBPXOnPh7MRjO9AwuSqcE6sPZ18PKuqrlT10Z79hOwNXKktM459as7ZvUiO5I+lVa26loZlk/SAJLupFLyL8ICNxc3VWzcVBoYMSdOcNc0+4DtgYB5DqFyJr2aZ+OOP44lW3OZtIyFWZPoL87pjeGfmmI2wxk3Et7nNxSSi87PpRbCQfmrCsc2cO5s/tSUYs1Zz71zCua9bYRjKLtg2t2FbbsYaAQUbUY1QTEmSrrk+qAGD2yAa3cdrQMXkryMcCOfKcuBx0Zs0Vbyftv13CRwUWdoSny7A1kouRbWf3XL0qi5465pPy3tgUz1BQWdkQsrUFvNhoLf3r2kIQr7L7iGvWyv07mcgDwmMrirVWBKlRIm2ZS0gNqalxTPStJHQ8VPHLhhsX0v4ZarxrNq69esxQxSDosxmOMvlGzJtDaVcZmdj1rdplEZw6TCBNSKPEQ7mvwkpHoGhJQZ2XXg0oiMcfhGTLq3LSiy76jOogioPymZJDEE6wanu6paSTiW1EyOH8PhPLFkzwjBYALWPr5yi06FZdsyNtUMb2e7O2j/RJpXKaFo+f668YolbtnmsZxaY4iM5NEakBH8zFP4SfMkAEO+UTKUgSWgLJ0g0E0ExMdsDnBrE5iVrMmcQM1auOaiVPXfrwxDVYdqUTu9A5bgs5mHRiHcGIRZfG9Tj6hbWZiFXFRVGMl7CqACf0esQ8jAGud43BlJBEmQKgZTqOXKRnXElsopivLhPB3cduiIWAo/KtsqcWeZnXzqd2yYb4NVq5FSlxtU1ENGdfnT3XSmMvWmMsugVCOg3UQgs6Iux9MZIzsG502boF1muFsiCI1aqCawqkNIma901Loj4y3duLtxbiSlxTmPTiMxkRQ1FG34R9J0E4eRtl7Y0qN28GpWbgcIziCkKLgBpBnDEgUjWz8jIyN91Bo1iwFoi1fu2EWeLTaRztk7ZxcWmzjXjoNpR9KCK1MTPKhHCMswZJnVpkkt6en5YevGxsdDRzCIiGDKKiYpk1jYuLjWqDGOjY5igRsyYMGTVNJszZM2yRU0kkylImQoFKAAAB4Tgsf/Z',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'FEGYELMI LAP',
                style: 'header',
              },
              {
                style: 'tableExample',
                table: {
                  widths: [150, 1, '*'],
                  body: [
                    [
                      { text: 'Név', border: [false, false, false, false] },
                      { text: ':', border: [false, false, false, false] },
                      { text: item.Nev, border: [false, false, false, false] },
                    ],
                    [
                      {
                        text: 'Azonosító',
                        border: [false, false, false, false],
                      },
                      { text: ':', border: [false, false, false, false] },
                      {
                        text: item.Azonosito,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Születési hely',
                        border: [false, false, false, false],
                      },
                      { text: ':', border: [false, false, false, false] },
                      {
                        text: item.SzuletesiHely,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Születési idő',
                        border: [false, false, false, false],
                      },
                      { text: ':', border: [false, false, false, false] },
                      {
                        text: item.SzuletesiIdo,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Anyja neve',
                        border: [false, false, false, false],
                      },
                      { text: ':', border: [false, false, false, false] },
                      {
                        text: item.AnyjaNeve,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Elhelyezése',
                        border: [false, false, false, false],
                      },
                      { text: ':', border: [false, false, false, false] },
                      {
                        text: item.FogvElhelyezese,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Végrehajtási fokozat',
                        border: [false, false, false, false],
                      },
                      { text: ':', border: [false, false, false, false] },
                      {
                        text: item.TartozkodasFokaJogcime,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Aktuális szabadulási idő',
                        border: [false, false, false, false],
                      },
                      { text: ':', border: [false, false, false, false] },
                      {
                        text: item.AktualisSzabadulasiIdo,
                        border: [false, false, false, false],
                      },
                    ],
                    // [
                    //   {
                    //     text: 'Esemény sorszáma',
                    //     border: [false, false, false, false],
                    //     margin: [0, 20, 0, 0],
                    //   },
                    //   {
                    //     text: ':',
                    //     border: [false, false, false, false],
                    //     margin: [0, 20, 0, 0],
                    //   },
                    //   {
                    //     text: item.EsemenySorszama,
                    //     border: [false, false, false, false],
                    //     margin: [0, 20, 0, 0],
                    //   },
                    // ],
                    [
                      {
                        text: 'Esemény észlelése',
                        border: [false, false, false, false],
                        margin: [0, 20, 0, 0],
                      },
                      {
                        text: ':',
                        border: [false, false, false, false],
                        margin: [0, 20, 0, 0],
                      },
                      {
                        text: item.EsemenyEszlelese,
                        border: [false, false, false, false],
                        margin: [0, 20, 0, 0],
                      },
                    ],
                    [
                      {
                        text: 'Eseményt észlelte',
                        border: [false, false, false, false],
                      },
                      { text: ':', border: [false, false, false, false] },
                      {
                        text: item.EsemenytEszlelte,
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 20, 0, 0],
                text: 'Esemény leírása:',
              },
              esemenyLeiras,
              {
                text: 'Kérem az eljárás lefolytatását',
                margin: [0, 30, 0, 0],
              },
              {
                margin: [-5, 20, 0, 0],
                table: {
                  widths: ['auto', 50, 1, 20, 1, 20, 1],
                  body: [
                    [
                      {
                        text: 'Elrendelve: ' + item.Telepules + ',',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 50,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        margin: [-5, 0, 0, 0],
                        text: '.',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 20,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        margin: [-5, 0, 0, 0],
                        text: '.',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 20,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        margin: [-5, 0, 0, 0],
                        text: '.',
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Kivizsgáló: ',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 135,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                            colSpan: 5,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      { text: '', border: [false, false, false, false] },
                      { text: '', border: [false, false, false, false] },
                      { text: '', border: [false, false, false, false] },
                      { text: '', border: [false, false, false, false] },
                      { text: '', border: [false, false, false, false] },
                    ],
                    [
                      {
                        text: 'Határidő: ',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 50,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        margin: [-5, 0, 0, 0],
                        text: '.',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 20,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        margin: [-5, 0, 0, 0],
                        text: '.',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 20,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        margin: [-5, 0, 0, 0],
                        text: '.',
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                margin: [-5, 30, 0, 15],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Elrendelő:',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 120,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
            ],
            //pageBreak: 'after',
          };
        }),
      ],
      // pageBreakBefore: function(currentNode) {
      //   if (currentNode.pageNumbers.length > 1) {
      //     return true;
      //   } else {
      //     return false;
      //   }
      // },
      pageBreakBefore: function(
        currentNode,
        followingNodesOnPage,
        nodesOnNextPage,
        previousNodesOnPage,
        pageNumber
      ) {
        if (currentNode.id === 'NoBreak') {
          return true;
        }
        return false;
      },
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          //color: 'black',
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          //color: 'black',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 12,
      },
    };
    // docDefinition['content'].push({
    //   pageBreak: isLastPage ? '' : 'after',
    //   columns: [left_columns, right_columns],
    // });

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    // pdfMake.createPdf(docDefinition).print();
  }

  async TajekoztatoNyomtatas(fegyelmiUgyId, kerem, iktatasId) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model = await apiService.TajekoztatoNyomtatas({
      fegyelmiUgyId: fegyelmiUgyId,
      kerem: kerem,
      iktatasId: iktatasId,
    });

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        {
          image:
            'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
          alignment: 'center',
          width: 30,
          height: 55,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.IntezetNev,
          alignment: 'center',
          fontSize: 12,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.Iktatoszam,
          alignment: 'right',
          fontSize: 10,
        },
        {
          text: [
            { text: 'TÁJÉKOZTATÓ\n' },
            {
              text:
                'A fegyelmi eljárással összefüggő jogokról, kötelezettségekről ',
              fontSize: 12,
            },
          ],
          style: 'header',
          decoration: 'underline',
        },

        {
          margin: [0, 10, 0, 0],
          alignment: 'center',
          text:
            'Melléklet a ' +
            model.UgySzama +
            ' sz. fegyelmi ügyhöz eljárás elrendelésekor',
        },
        {
          margin: [0, 10, 0, 0],
          text: [
            { text: 'Név: ' + model.Nev + ', ' },
            {
              text: 'azonosító jel: ' + model.Azonosito + ', ',
            },
            {
              text: 'szül. idő (év, hó, nap): ' + model.SzuletesiIdo,
            },
          ],
        },

        {
          text:
            'Nevezettet az alábbi jogokról és kötelezettségekről oktattam ki:',
          italics: true,
          margin: [0, 10, 0, 0],
        },
        {
          margin: [10, 10, 0, 0],
          italics: true,
          ul: [
            'Önmagára nem köteles terhelő vallomást tenni.',
            'Jogsérelem esetén ügyészhez, adatvédelmi biztoshoz fordulhat.',
            'Az eljárás során anyanyelvét vagy az általa ismert más nyelvet használhatja.',
            'Védekezését szóban vagy írásban előadhatja.',
            'A vallomástétel megtagadása nem akadálya az eljárás folytatásának, a fegyelmi felelősség megállapításnak.',
            'A vizsgálat során bizonyítási indítványt tehet, melynek teljesítéséről, vagy megtagadásáról a vizsgáló dönt, döntése ellen külön jogorvoslatnak nincs helye.',
            'Védőt bízhat meg, vagy kérheti kirendelését.',
            'Felhívtam figyelmét a hamis vád, illetve a hamis tanúzás törvényi következményeire.',
            'Fegyelmi kiszabása esetén panasszal élhet, ill. joga van felülvizsgálati kérelem előterjesztésére.',
            'Az eljárás során felmerült költségek megtérítésére kötelezhető.',
            'Az eljárás során keletkezett iratokat tanulmányozhatja, azokból másolatot kérhet.',
          ],
        },
        {
          text: 'A fenti jogokat velem ismertették, azokat tudomásul vettem.',
          italics: true,
          margin: [0, 10, 0, 0],
        },
        {
          italics: true,
          margin: [-5, 10, 0, 0],
          table: {
            body: [
              [
                {
                  text: 'Kelt: ' + model.Telepules + ',',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 50,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 20,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 20,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
              ],
            ],
          },
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*', 120],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 120,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  text: 'fogvatartott',
                  border: [false, false, false, false],
                  alignment: 'center',
                  italics: true,
                  margin: [0, -3, 0, 0],
                },
              ],
            ],
          },
        },
        {
          text: 'A terhemre rótt fegyelmi cselekményt velem ismertették.',
          italics: true,
        },
        {
          text:
            'A vizsgálat befejezésekor a fegyelmi iratok tanulmányozását ' +
            (model.Kerem == true ? 'kérem.' : 'nem kérem.'),
          italics: true,
        },
        {
          margin: [-5, 10, 0, 0],
          table: {
            widths: [150, '*', 120],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 120,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  text: 'fogvatartott',
                  border: [false, false, false, false],
                  alignment: 'center',
                  italics: true,
                  margin: [0, -3, 0, 0],
                },
              ],
            ],
          },
        },

        {
          italics: true,
          margin: [-5, 10, 0, 0],
          table: {
            body: [
              [
                {
                  text: 'Kelt: ' + model.Telepules + ',',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 50,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 20,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 20,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
              ],
            ],
          },
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*', 120],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 120,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  text: 'kivizsgáló',
                  border: [false, false, false, false],
                  alignment: 'center',
                  italics: true,
                  margin: [0, -3, 0, 0],
                },
              ],
            ],
          },
        },
        {
          text:
            'A rendelkezésre álló iratokba a vonatkozó adatvédelmi szabályok betartása mellett betekintettem.',
          italics: true,
        },
        {
          italics: true,
          margin: [-5, 10, 0, 0],
          table: {
            body: [
              [
                {
                  text: 'Kelt: ' + model.Telepules + ',',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 50,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 20,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 20,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
                {
                  margin: [-5, 0, 0, 0],
                  text: '.',
                  border: [false, false, false, false],
                },
              ],
            ],
          },
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*', 120],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 120,
                      y2: 12,
                      dash: { length: 2 },
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  text: 'fogvatartott',
                  border: [false, false, false, false],
                  alignment: 'center',
                  italics: true,
                  margin: [0, -3, 0, 0],
                },
              ],
            ],
          },
        },
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          //color: 'black',
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          //color: 'black',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 12,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    // pdfMake.createPdf(docDefinition).print();
  }

  async EljarasAlaVontMeghallgatasiJegyzokonyvNyomtatas({
    naplobejegyzesIds,
    iktatasIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var result = await apiService.EljarasAlaVontMeghallgatasDokumentumAdatok({
      naplobejegyzesIds: naplobejegyzesIds,
      iktatasIds: iktatasIds,
    });
    let model = result.data;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 0],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          var jegyzoKonyvText = htmlToPdfMake(
            `
          <div style="text-align: justify;">` +
              item.JegyzoKonyvText +
              `</div>
      `
          );
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQICAgICAgICAgICAwMDAwMDAwMDAwEBAQEBAQECAQECAgIBAgIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD/8AAEQgAWAAwAwERAAIRAQMRAf/EAJQAAAICAwEAAAAAAAAAAAAAAAgJBgoABQcDAQACAwEAAAAAAAAAAAAAAAACAwABBAUQAAAHAQAABgIABgMBAAAAAAECAwQFBgcIABESExQJFRchIkMWGApCIyQyEQACAQIEAwQIBAUFAAAAAAABAhEhAwAxEgRBUWFxIjIT8IGRobFCIwXB0ZIU4fFSYjNyotJDNP/aAAwDAQACEQMRAD8Av8eJiYzxMTFZH7WfsC3XkXqydn8C19/bYumc/wB8T1rIZagalLZ7iK0dnp7LB6VLyNJqdwrj52lJXuuzL15JskyRzJJJBZ4Rm5WSJ0dtZsXduTuAVQOIcGskgRGZoGgSKmeuAYwYGcenpyw8fix1fpHm3OpjR9tpfQ8/PNZSdZ6xn0jGztUsVZlph89qqcdZoiErDC1mjIBZBsrJEi475aqZjC3THz8+exDMSBHMTMHiOlZpwywdeOCn8ViYzxMTGeJiYUt1v9l0nUdXJxpwplf+XvdEs1WcytOZyZ4bEucq6mqVo70XpvU0iKRtSiYx0qBEYFmdaelHIA2TSROomYxIqt3n1CzMFlANYNBNJFJBIzBwJPBY1YTl3Fg+2c8fC3Dv7s2/3E92xqZDYlub4NDOE84qxOjOYKRoCVBsaaDacukZO5/rs3DN4ywNCQEe1dicGaavrdeMu53Vy3eSxtEUJcuWwdUEkFtJJMgCBUkVGnxQIK2BAUtHeevsJz7QI9mWNLzVmR7nvjrBOKes+vsIGo6a2r8s60zTaDsbaBd2bCNH3S05+3Qxi8SGOzFJvsk3g5ly7iX/AOajX4GRKu3FM7bx0RcPkL5qhrbA6TQNQgTME0FADSCTGDAkQJU/D1e/DQ8x746L4x1CA5o+0aoIs6xaBdNcP7loQLWDJr2hGoGcr1zYTNYuKe0a6xbIhlFHi0c0SVZpGcuEgQQcyRwZRdP0VINKZnqeYHGtI4yMWshRrNcPCYP2MoxZycY8aSMbItG7+PkGDhF4xfsXiJHDR4zdtzqN3TR03UKdNQhjEOQwGKIgID4TlQ54LAE/Z122++vTkG79RMKBHaUrUbHQK6atSlp/tJp5X24RNOSkxfEjJd1ILRrqYTUTj26IOH5/JFM5DHAwGi62g0EGvYJ7a5U54mK8X1r6hpXMmOyv2XW+2Pn3PFsscpkXbdDctbAjrEx0/pWnsn9v2uAiXiRCxgY3tt+TyyBpCqR3ZYsjyQ+UL5woi5bubxa41guGFoxAA0rAkAaZB1qyszTyBkqcLVqTpPXn/HswZvCeAM9Zv+Kcpdtwkxomp8I4hotvv9A02ySWh162WTpTYoW75vZLd+cMuw1Cq1+k1RoWLSlE3TNnZ2b8CJ/IikjppKPqO4t6ls91Qad4wdRjMGa8M4ypi1LairVETPby4iBnPPhXBGdq4JzpybqfP++5BnpsitN8ltfwdek8+x8fnzbXL9qHPukQGPkRqldYtq+51xhcUkY6BmitSvWjeTc++qdqmAJEu4ZIsFPMU94A/LBBYjjBEyMjnngWYLcCjxMCfZA/HC5rHD9A6xylD89W+2yrGu/UXRm9h+wiJmL1bT3PctBjIy6zZIXMLa4EZGCi81zGBbaVTLBMHfg8UlIFoZEot3joloj3botoxRLhKgwJgmADpNJ8DxBA1EZjFtXxLJFfXEZcaE8cGj/r29z3jqTna4YJd6iCD7ihnn2St9RWeDFSOrwswW5O6LYnuaOomNkKCm9zKDhJBsHqUaP20iRVsRFECphd615UAZyQRAADDTIEEyK5zSIrEklNMqRjo339Z+w07jvOaZNXCKo8BLdK58pLWC1Q87ZaK3WhKlodnrLS21itgWYnTTVzgY2OhyJKJi3sD1i4AFDokSPVpEuEozBSVMTEkyPDObASYAYkAgCsiGZEc8V7Z3jm+s3+i5q81vSKtSRulK0a8V2159qshE4vc57Rue5sOgOgI+LXa1+4LT8/oEjYIl/F/EftpGrg6duCgi4VR51uwLtk7kaV3CwkMV1MmkkxC91EgI0jVWhymld3E6YOdZ8XaBn16Y6Qny7pMUwntiL1XrK12f1uz1JfoRqXQarS53MaR0hQM3gNlsb5/Jv9RPz1TWupTEs/agq4STeMXLlu8RZKquiaBs9nctpaYk7fUpYAq0NpIKrC1JKgKTJGoahIIxRu3LbKIAdgxOfCPbVuQy642dh4+0CXmSMnnZ927Tk8nvkLP5hrmEykxOU1zfpTDehrq2z2mqyt1vrSt9BQ/wCu2KXyYSYk/KFsiYKJNXCgJqMtfbdqLNxrf0ka3JWQREqJJKglIYiKE5kngZd9QZ4McePX4DHDbPzFoqFO1Ofb9aSejs9Pissba/p0Hne9Fzm3zFkxboN+4wbWoh5LvrJdr+3UyaAq0Yd04+O2b3Jq3dNSqC3br5bO22xv2VuMoUXNIMqAqg0aSImJMGkjxGcFLhSSAK9azxyzw2b6Q8AdYt2r1yopabN897lcFC3DOpOBnK1N1Z7UtTsVBz9ztJpUpGN0u7rPqGgrVX7BNFpHV106akAwHAS6bN5n2ih7RlrjHzCUPggaQFggMSWqDMCs5ptksNTDSQMu2pPtp6jjoH3B0uY3HXDUCz6jqUHnuZxeZ3es0WnTFWj6ktdXWH95XYbRY4edp9kTscqzmMjhRbfJMKDZJBT20yqnBUh27du9Zui5WGiOEHyxEcR3jiI7G444Kwj9M/HCzIfiKgO7oygVNG2csaGsS1OBJJ7kRFyQ6HS36nT9tf8ATQmSff2kYSGVKAet2b3RD/j4w7nY7K3dCrbt6SiE0GZVCficaldzBk8fx/LEEovFWdXGvQ9nnbnpzuZkaTmrp299jFxVUTvOPfXBeLDH+4ti6ywRLue6jtBithOKQIGYJmA3xCmUttrtUFpVtrpfUTSkqzgEDKYX44W9x1dQPmMf7Z+ONDGcKY9D59JOYacucMSr5LfdIgYyJgMEiYaIt8Dz303pbN+wiIzEG7NmU1my6LBUUipqqtRcpGP/AOgwl02tpZvbV9xfWb+hpNagXiigzUgKBQ0mowPmt5vljwxyHp+PPE+muELOwmv7mN+zUcNHqTaudGWjK6nhzyyvFM7tm8Fi1T0AnPib9kkvDUOGjlVhfmUUeNXL0UipOCELhOx2nlT5duShNFHi0kiZERIBOdMq4ZruawpNJ9v5YInhjKVOf+0ec7TnWqaui50Jg0pN9ipCRoRK9bqq9iuZLMav2CKr+d1757SPntTlXTI5lCrsjqEBBQpBXI41C1bsbO1dtIqPcvKDHEFLhMdpQdYHCpxnt3nuhGb5gZ/ST8RhnXTKfACv2H6wTu8cTCMDlvng2cfudZFFn801z6XJaRghdnIzM4/CCIO/PzUBmKn9EVvGj7efuA3t39j5kQs6edNPrzj+WED/ANL6v8fumFj3TjxK3/19RceZFeJxd/IHyEj+N+X8z8oUDeXpce/8384QPP8AqfNAP6oB46mr7wFzuaSY6TGXbHDOOmNH0+mPJu3/ANe8qZPiK8QFQ9tuCXxH8SCIoka1wzIEvYce2KJWCcMKHl/L7BY/0fyA08F5n3uAdV7TEipiK1HTOo69cSLfTCoK2tkJMvyyHuO9oczYhcc+rVIk9gZQMbY2lfzy5czdfwqrZg1tEPNRrBrJ1t0YiT9w3ODJMAVEwCX1eM2zS7etOCuu5DSKVjcsCKHnnHXI5Lot7l3R+Ppyw2mzVfkBfgKv2ST77/F5ot1RfNWa9UhXqUQJbZrldNNTuNPCmrVdSvIgk+tEzH/CLHlWafF9wRAyRjeMgsze8nQZFNMwfDGfrnDw3zE1n07cAPzY5qDjqzmQmf68beqLG6tcYKoa2aEja8NwrlffciQcQ6GMiI6IjQOyYsCNRcIt0SPPZ98AN7nma99aazt7NtlKEbhKTP8A1XjzMdnDphShVdFXwjVH6GwU/wBiv7GsXXOgUfL8E2zbZwmO43bJYcpr9OlI2vRMnlv2AZLFlnX9tvVNKg+kbZpDAEkUSuB+GRysYSgj6TrsX7a+bYM+YWBHZ9P/AIn3YG2R59xfmJB9QUD8RgZoir9QMrm2nl+DeyvgJavJXM4kpmNncfhXXS37XRMCP7zDzejU/wCYUvP+Dv8A6vP/AJeB3IF26GTwhVHrCqD8DjUpgV6/jiI57nnV1ZqsLCynBnYYPGFUy6GXBtUcaXRB7UMf+uKjTAJq/vEnrRLO8s2oET+Qe4gmxU8g+WBUxdQ3lQPAGn1s5EfqGFuCzIRkDJ/THxx64dk2xdJRM7X84u+C5JHq801FOYZ9AntLS7pq77mXVePnSQhqnNtI1spVY+2HcrkOu596Qbi39z2xFUdNp0tfbrdxqi6bq+LSRovsZEhuzp7hBb+qW4wOHMT05/xw1ixcd67K8lQtcS2LnlDTEut9R6beWtwS3Diqn7PsetOl6fHPiThbMZWJZ6UCSbpRcfecNDAKZSn8i4zGhajSKZ9CKHn6o6HDCCXniDOWAEpOSani3cvJFR0m/wCCX8rxNpKw73FXVrVcMfxE7zbnblKyJWSSkU003iVDRcNjImKPuLLpmAQTIJiukfs7KLEDcqM5P+O7HAcM+ZqIwhbYtNbStJFf9DYLXrTo7buWu49X0DKMzzDSo20894PT5trfdBs1EfxUjWYztDYWq0X+EpFvaybGRgc7kWyhlDIKIu1G/kUyZjmKj9vuXuvf2+gAEAlqiO5kAQZlhnQ+o4QFY33dCAwMVE5hTzHLEWYfab2k/sKNdJy/zaRZa4OqUDoehNBMiWUaa4GPqL+2GKAoLI0x5vAN/wDXxQ9Ih7n8PEvLv7LaddgsVU+F/mAOerqBhwG5ORX2dv8Ad0xpav8AbV2RaoljMs+WudGrSQiafNIlddB38Vis7nRuVb6wKoRLFjlBw1jetIVBUoCIe/GPfIRKKImojfrpl7HfmO6/AsDPe/t9/Q4FjfUgSskwKdJ/q5YT7o8fiqecRD/fMip1wtsnzJcoFGxEycmqxVSmD8x9bIxzD+5F6u+m4WEQ0ebYKMXCqKRUnQFeKAiKKiqe/b6b20Fll1XWD6Qc5/cMSRJoIAr2cwMGGK34kadM0y/ngxLpesbPytB4fIYBOMue0/s06XcV3QJDKax/iPJwraa6Ks0Cxg3iTleJQrJlZFqhEv1YlCBdTSIsmjpR4mVMU27Ra35YWCtuiwZJNvuwBlmKmAMzInDGuKG1/KDnn6dmOXcOR2LI9V81vsgymOzkhLVEws3INcmRyxeyKxkDx1/EiKkLCS0vHMZZZ8omdykUPddKLEAQces97i2E2tkMqi8NwgMRP+O8anjXiJB5zkm3rBtq86q+3Q2HBda8kWPqHt2bpUV0PaMMZv8Amii3pFvX83zu7KWl9ASnROMWFYr+8xr9RgnXqnu5iGRblEouJRBY4gZJMDNtFrSsRBRzUGeS9n9I44MKockGrQT6hFMR1t9Peps5hOcbfYHoxXyVqXuJDKc8c/qJhNOtEDUlze3+AIQWw2/zUKn5eRW4+z/EoeYg/wBQd+rwBPGAAAIypA4TTDQSMssauA+mLQ63GM4eM+wDSyMWEbXIhuVbn7AFlvx9TqXP1KhE1Vj17/sXSg+Y6kVVT0h7qiDs4gAuz+myVYKCBKAwa8SSePHUfdgSAxBOYMj2R8MVrNG0XRNTqON5yfE9LtUFCaxSIW82qB2mPy6L1ehZvVuiMh0h1KWDG5s+g0aD0e030zFlDwTeUk15dgpALsSPXLFi+fYazb2wtNcNu9cLBCqwA3nG6oliNNIGo1jvjUCcKZS9wgTkM+lDE+7lgoC7FXpnHIzDHvG0G2oMFr1m6FbJm+wHpFUyeh3bOZhedsRrNG5u5aS2bvG4uHiLZu2WhU4RY9rjW6pWE9+Iwnztrd/b3XuWrhJANJMQCJKiGAyWjalikqWrTKFNRkESe7lwOUR1joTiU/WJH61/ktzfTZ+k/go+I6S2JRo3t22y2p6Qem2dpHa3CfhZaXj5qauFaxSv4Wxrcy5npdtOgnOwz1NspGumzhfduPJezbsAtpTS4MRqYBxzqDrYmBQ+uDC6SC0yD8QRw7ezFgf7ZuWbxt2Owmv4yz02Y2rndpoU1A0jJNOt2S3DVqbcqW/grPQWVlpU7XJR9Kxc2hD2mGj1nXxH03XGzY5P/R6i1YYBtDsBbaJmqyMi3TgTwBJrlgyDmJnFfKsX/nCatUcYexdESgHOrS0GdpM977rXXbatN+ohpRWEzETe1R0zCOmVDTFqqm8SQdIIgY6gFOAnCbtLtq75SINICAkCe8VUkSOJknFppIBPXOnPh7MRjO9AwuSqcE6sPZ18PKuqrlT10Z79hOwNXKktM459as7ZvUiO5I+lVa26loZlk/SAJLupFLyL8ICNxc3VWzcVBoYMSdOcNc0+4DtgYB5DqFyJr2aZ+OOP44lW3OZtIyFWZPoL87pjeGfmmI2wxk3Et7nNxSSi87PpRbCQfmrCsc2cO5s/tSUYs1Zz71zCua9bYRjKLtg2t2FbbsYaAQUbUY1QTEmSrrk+qAGD2yAa3cdrQMXkryMcCOfKcuBx0Zs0Vbyftv13CRwUWdoSny7A1kouRbWf3XL0qi5465pPy3tgUz1BQWdkQsrUFvNhoLf3r2kIQr7L7iGvWyv07mcgDwmMrirVWBKlRIm2ZS0gNqalxTPStJHQ8VPHLhhsX0v4ZarxrNq69esxQxSDosxmOMvlGzJtDaVcZmdj1rdplEZw6TCBNSKPEQ7mvwkpHoGhJQZ2XXg0oiMcfhGTLq3LSiy76jOogioPymZJDEE6wanu6paSTiW1EyOH8PhPLFkzwjBYALWPr5yi06FZdsyNtUMb2e7O2j/RJpXKaFo+f668YolbtnmsZxaY4iM5NEakBH8zFP4SfMkAEO+UTKUgSWgLJ0g0E0ExMdsDnBrE5iVrMmcQM1auOaiVPXfrwxDVYdqUTu9A5bgs5mHRiHcGIRZfG9Tj6hbWZiFXFRVGMl7CqACf0esQ8jAGud43BlJBEmQKgZTqOXKRnXElsopivLhPB3cduiIWAo/KtsqcWeZnXzqd2yYb4NVq5FSlxtU1ENGdfnT3XSmMvWmMsugVCOg3UQgs6Iux9MZIzsG502boF1muFsiCI1aqCawqkNIma901Loj4y3duLtxbiSlxTmPTiMxkRQ1FG34R9J0E4eRtl7Y0qN28GpWbgcIziCkKLgBpBnDEgUjWz8jIyN91Bo1iwFoi1fu2EWeLTaRztk7ZxcWmzjXjoNpR9KCK1MTPKhHCMswZJnVpkkt6en5YevGxsdDRzCIiGDKKiYpk1jYuLjWqDGOjY5igRsyYMGTVNJszZM2yRU0kkylImQoFKAAAB4Tgsf/Z',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
                margin: [0, 0, 0, 0],
              },
              {
                text:
                  'ELJÁRÁS ALÁ VONT MEGHALLGATÁSI JEGYZŐKÖNYV\n FEGYELMI ÜGY KIVIZSGÁLÁSÁHOZ',
                alignment: 'center',
                fontSize: 12,
                bold: true,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.UgySzam + ' számú fegyelmi ügyben',
                alignment: 'center',
                bold: true,
                fontSize: 11,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Készült a ' + item.IntezetNev + ' hivatalos helyiségében.',
                alignment: 'center',
                margin: [0, 0, 0, 0],
              },
              {
                text:
                  item.MeghallgatasEve +
                  '. év ' +
                  item.MeghallgatasHava +
                  '. hónap ' +
                  item.MeghallgatasNapja +
                  '. napon.',
                // +
                // (item.MeghallgatasOraja + '').padStart(2, 0) +
                // ':' +
                // (item.MeghallgatasPerce + '').padStart(2, 0) +
                // '-kor.',
                alignment: 'center',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'JELEN VANNAK:',
                alignment: 'center',
                margin: [0, 0, 0, 10],
              },
              {
                ul: [
                  'Eljárás alá vont: ' +
                    item.MeghallgatottNev +
                    ', ' +
                    item.MeghallgatottAzon,
                  'Meghallgató: ' +
                    item.MeghallgatoNev +
                    ' ' +
                    (item.MeghallgatoRang != '' ? item.MeghallgatoRang : ''),
                  'Jegyzőkönyvvezető: ' +
                    (item.JegyzoNev != null ? item.JegyzoNev : '') +
                    ' ' +
                    (item.JegyzoRang != null ? item.JegyzoRang : ''),
                  'Egyéb jelenlévő: ' +
                    (item.EgyebJelenlevo != null ? item.EgyebJelenlevo : ''),
                ],
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Eljárás alá vont adatai:\nNév: ' +
                  item.MeghallgatottNev +
                  '\nNyilvántartási szám: ' +
                  item.MeghallgatottAzon +
                  '\nSzületési helye, ideje: ' +
                  item.MeghallgatottSzulHelye +
                  ', ' +
                  item.MeghallgatottSzulIdeje +
                  '\nAnyja neve: ' +
                  item.MeghallgatottAnyjaNeve,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'A jegyzőkönyv felvétele előtt felhívtam a fogvatartott figyelmét a hamis vád törvényes következményeire, részletesen ismertettem a Büntető Törvénykönyvről szóló 2012. évi C. törvény alábbi vonatkozó részeit:\n268. § (1) Aki\n',
              },
              {
                text:
                  'a) mást hatóság előtt bűncselekmény elkövetésével hamisan vádol,\nb) más ellen bűncselekményre vonatkozó koholt bizonyítékot hoz a hatóság tudomására, bűntett miatt három évig terjedő szabadságvesztéssel büntetendő.',
                margin: [3, 0, 0, 10],
              },
              {
                text:
                  //'Tájékoztatom, hogy az olyan kérdésekben amelyben saját magát vagy közvetlen hozzátartozóját bűncselekmény, szabálysértés vagy fegyelem sértés elkövetésével vádolná a vallomástételt megtagadhatja, de a védekezés ezen módjáról ezzel lemond. Felhívom figyelmét arra, hogy a vallomástétel megtagadása nem akadálya az eljárás lefolytatásának és a fegyelmi felelősség megállapításának.\nTájékoztatom továbbá, hogy a fegyelmi eljárás során felmerülő költségek terhét viselnie kell, amennyiben a fegyelmi jogkör gyakorlója a fegyelmi cselekmény elkövetésében a felelősségét megállapítja.\nFigyelmeztettem, hogy a jegyzőkönyvbe foglaltak felhasználhatók Ön ellen.',
                  'Tájékoztatom, hogy a fegyelmi eljárás során felmerülő költségek terhét viselnie kell, amennyiben a fegyelmi jogkör gyakorlója a fegyelmi cselekmény elkövetésében a felelősségét megállapítja.\nFigyelmeztettem, hogy a jegyzőkönyvbe foglaltak felhasználhatók Ön ellen.',
                margin: [3, 0, 0, 10],
              },
              {
                text:
                  'A büntetés-végrehajtási intézetekben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi',
              },
              {
                text:
                  'felelősségéről szóló 14/2014. (XII.17.) IM rendelet 15. §. (1) bekezdése alapján tájékoztatom a fegyelmi\neljárás során érvényesíthető jogaira, azaz:',
                margin: [16, 0, 0, 0],
              },
              {
                ul: [
                  'védőt hatalmazhat meg, illetve kérheti kirendelését,',
                  'a 14/2014. (XII.17.) IM rendelet 36. §.-ban foglaltak fennállása esetén közvetítői eljárás lefolytatását kezdeményezheti,',
                  'anyanyelvét, vagy az Ön által ismert nyelvet használhatja,',
                  'védekezését szóban vagy írásban adhatja elő,',
                  'nem köteles vallomást tenni és a vallomástételt az eljárás során bármikor megtagadhatja, ez esetben figyelmezetem arra, hogy ez az eljárás folytatásának és a fegyelmi felelősség megállapításának nem akadálya',
                  'bizonyítási indítványt tehet,',
                  'a vizsgálat befejezése után az eljárási iratait tanulmányozhatja, azokról másolatot kérhet,',
                  'panasszal élhet, illetve bírósági felülvizsgálati kérelmet terjeszthet elő a fegyelmi határozattal szemben.',
                ],
              },
              {
                text:
                  'Megértettem a fegyelmi ügy kivizsgálójának figyelmeztetését a hamis vád törvényes következményeire.',
              },
              {
                text:
                  'Aláírásommal igazolom, hogy a fegyelmi eljárással kapcsolatos jogaimról és kötelezettségeimről kioktattak.',
                margin: [20, 0, 0, 10],
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [60, '*', 150],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 150,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: 'Eljárás alá vont aláírása',
                        border: [false, false, false, false],
                        margin: [0, -4, 0, 0],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              {
                pageBreak: 'before',
                text:
                  'Nevezett a fegyelmi üggyel kapcsolatosan történt meghallgatása során a következőket mondta el:',
                fontSize: 11,
                margin: [0, 0, 0, 10],
              },
              jegyzoKonyvText,
              {
                text:
                  'Az üggyel kapcsolatban egyebet elmondani nem tudok és nem is kívánok. A jegyzőkönyv az általam elmondottakat helyesen és jól tartalmazza, melyet elolvasás után helybenhagyólag aláírok.',
                margin: [0, 10, 0, 10],
              },
              {
                text:
                  'Jegyzőkönyv lezárva: ' +
                  item.JegyzoKonyvZarasOra +
                  ' óra ' +
                  item.JegyzoKonyvZarasPerc +
                  ' perc',
              },
              {
                text: 'Kelt, mint fent',
                alignment: 'center',
                margin: [0, 0, 0, 40],
              },
              {
                margin: [-5, 0, 0, 40],
                table: {
                  widths: [120, 180, '*'],
                  body: [
                    [
                      {
                        text: 'Eljárás alá vont aláírása: ',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          item.MeghallgatottNev + ', ' + item.MeghallgatottAzon,
                        border: [false, false, false, false],
                        margin: [0, -4, 0, 0],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                margin: [-5, 0, 0, 40],
                table: {
                  widths: [180, '*', 180],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Meghallgató',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -4, 0, 0],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Tanú',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -4, 0, 0],
                      },
                    ],
                  ],
                },
              },
              item.EgyebJelenlevo != null && item.EgyebJelenlevo != ''
                ? {
                    margin: [-5, 0, 0, 40],
                    table: {
                      widths: [120, '*', 180],
                      body: [
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 180,
                                y2: 12,
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                        ],
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: item.EgyebJelenlevo.split(', ')[0],
                            border: [false, false, false, false],
                            alignment: 'center',
                            margin: [0, -4, 0, 0],
                          },
                        ],
                      ],
                    },
                  }
                : null,
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }

  async TanuMeghallgatasiJegyzokonyvNyomtatas({
    naplobejegyzesIds,
    iktatasIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var result = await apiService.TanuMeghallgatasDokumentumAdatok({
      naplobejegyzesIds: naplobejegyzesIds,
      iktatasIds: iktatasIds,
    });
    let model = result.data;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          var jegyzoKonyvText = htmlToPdfMake(
            `
          <div style="text-align: justify;">` +
              item.JegyzoKonyvText +
              `</div>
      `
          );
          console.log(jegyzoKonyvText);
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text:
                  'TANÚ MEGHALLGATÁSI JEGYZŐKÖNYV\nFEGYELMI ÜGY KIVIZSGÁLÁSÁHOZ',
                alignment: 'center',
                fontSize: 12,
                bold: true,
                margin: [0, 15, 0, 10],
              },
              {
                text: item.UgySzam + ' számú fegyelmi ügyben',
                alignment: 'center',
                bold: true,
                fontSize: 11,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Készült a ' + item.IntezetNev + ' hivatalos helyiségében',
                alignment: 'center',
                margin: [0, 0, 0, 0],
              },
              {
                text:
                  item.MeghallgatasEve +
                  '. év ' +
                  item.MeghallgatasHava +
                  '. hónap ' +
                  item.MeghallgatasNapja +
                  '. napon. ',
                // +
                // (item.MeghallgatasOraja + '').padStart(2, 0) +
                // ':' +
                // (item.MeghallgatasPerce + '').padStart(2, 0) +
                // '-kor.',
                alignment: 'center',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'JELEN VANNAK:',
                alignment: 'center',
                margin: [0, 0, 0, 10],
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: ['*', '*'],
                  body: [
                    [
                      {
                        ul: [
                          'Tanú (fogvatartott): ' +
                            item.MeghallgatottNev +
                            ', ' +
                            item.MeghallgatottAzon,
                          'Meghallgató: ' +
                            item.MeghallgatoNev +
                            ' ' +
                            (item.MeghallgatoRang != ''
                              ? item.MeghallgatoRang
                              : ''),
                          'Jegyzőkönyvvezető: ' +
                            (item.JegyzoNev != null ? item.JegyzoNev : '') +
                            ' ' +
                            (item.JegyzoRang != null ? item.JegyzoRang : ''),
                          'Egyéb jelenlévő: ' +
                            (item.EgyebJelenlevo != null
                              ? item.EgyebJelenlevo
                              : ''),
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: 'nytsz.: ' + item.MeghallgatottAzon,
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                text: [
                  {
                    text: 'Tanú (fogvatartott) adatai:\n',
                    decoration: 'underline',
                  },
                  'Név: ' +
                    item.MeghallgatottNev +
                    '\nNyilvántartási szám: ' +
                    item.MeghallgatottAzon +
                    '\nSzületési helye, ideje: ' +
                    item.MeghallgatottSzulHelye +
                    ', ' +
                    item.MeghallgatottSzulIdeje +
                    '\nAnyja neve: ' +
                    item.MeghallgatottAnyjaNeve,
                ],
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'A hamis vád és hamis tanúzás törvényes következményeivel kapcsolatosan részletesen ismertettem a fogvatartottal a Büntető Törvénykönyvről szóló 2012. évi C. törvény alábbi vonatkozó részeit:\n268. § (1) Aki\n',
              },
              {
                text:
                  'a) mást hatóság előtt bűncselekmény elkövetésével hamisan vádol,\nb) más ellen bűncselekményre vonatkozó koholt bizonyítékot hoz a hatóság tudomására, bűntett miatt három évig terjedő szabadságvesztéssel büntetendő.',
                margin: [3, 0, 0, 10],
              },
              {
                text:
                  '272. § A tanú, aki hatóság előtt az ügy lényeges körülményére valótlan vallomást tesz, vagy a valót elhallgatja, hamis tanúzást követ el.',
              },
              {
                text:
                  'Tájékoztattam, hogy a tanúvallomást megtagadhatja a fegyelmi eljárás alá vont fogvatartott Bv. tv. szerinti hozzátartozója, az érintett kérdésben az, aki a vallomásával magát vagy a hozzátartozóját bűncselekmény, szabálysértés vagy fegyelemsértés elkövetésével vádolná, az egyházi személy és a vallási tevékenységet végző szervezet vallásos szertartást hivatásszerűen végző tagja arról, amire a hivatásánál fogva titoktartási kötelezettsége áll fenn, a védő arról, amiről mint védő szerzett tudomást.\nFelhívom a figyelmét arra, hogy a vallomástétel megtagadása nem akadálya az eljárás lefolytatásának és a fegyelmi felelősség megállapításának.\nA fegyelmi eljárás során anyanyelvét, vagy az Ön által ismert nyelvet használhatja, vallomását szóban vagy írásban adhatja elő.\nFigyelmeztetem, hogy a jegyzőkönyvbe foglaltak felhasználhatók Ön ellen.',
                margin: [3, 0, 0, 0],
              },
              // {
              //   ul: [
              //     'A büntetés-végrehajtási intézetekben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet 16. §. alapján tájékoztatom a fogvatartottat a fegyelmi eljárás során érvényesíthető jogaira, azaz:',
              //     'védőt bízhat meg,',
              //     //'a 14/2014. (XII.17.) IM rendelet 36. §.-ban foglaltak fennállása esetén közvetítői eljárás lefolytatását kezdeményezheti,',
              //     'anyanyelvét, vagy az Ön által ismert nyelvet használhatja,',
              //     'vallomását szóban vagy írásban adhatja elő.',
              //   ],
              //   margin: [40, 0, 0, 0],
              // },
              {
                text:
                  'Megértettem a fegyelmi ügy kivizsgálójának figyelmeztetését a hamis vád és hamis tanúzás törvényes következményeire. Kijelentem, hogy a fegyelmi eljárással kapcsolatos jogaimról és kötelezettségeimről kioktattak.',
                margin: [0, 0, 0, 15],
              },
              {
                margin: [-5, 0, 0, 30],
                table: {
                  widths: [60, '*', 150],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 150,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: 'Tanú (fogvatartott) aláírása',
                        border: [false, false, false, false],
                        margin: [0, -4, 0, 0],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              {
                pageBreak: 'before',
                text:
                  'Nevezett a fegyelmi üggyel kapcsolatosan történt meghallgatása során a következőket mondta el:',
                fontSize: 11,
                margin: [0, 0, 0, 15],
              },
              jegyzoKonyvText,
              {
                text:
                  'Az üggyel kapcsolatban egyebet elmondani nem tudok és nem is kívánok. A jegyzőkönyv az általam elmondottakat helyesen és jól tartalmazza, melyet elolvasás után helybenhagyólag aláírok.',
                margin: [0, 10, 0, 30],
              },
              {
                text:
                  'Jegyzőkönyv lezárva: ' +
                  item.JegyzoKonyvZarasOra +
                  ' óra ' +
                  item.JegyzoKonyvZarasPerc +
                  ' perc',
              },
              {
                text: 'Kelt, mint fent',
                alignment: 'center',
                margin: [0, 0, 0, 40],
              },
              {
                margin: [-5, 0, 0, 40],
                table: {
                  widths: [140, 180, '*'],
                  body: [
                    [
                      {
                        text: 'Tanú (fogvatartott) aláírása: ',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          item.MeghallgatottNev + ', ' + item.MeghallgatottAzon,
                        border: [false, false, false, false],
                        margin: [0, -4, 0, 0],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                margin: [-5, 0, 0, 40],
                table: {
                  widths: [180, '*', 180],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Meghallgató',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -4, 0, 0],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Tanú',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -4, 0, 0],
                      },
                    ],
                  ],
                },
              },
              item.EgyebJelenlevo != null && item.EgyebJelenlevo != ''
                ? {
                    margin: [-5, 0, 0, 40],
                    table: {
                      widths: [120, '*', 180],
                      body: [
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 180,
                                y2: 12,
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                        ],
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: item.EgyebJelenlevo.split(', ')[0],
                            border: [false, false, false, false],
                            alignment: 'center',
                            margin: [0, -4, 0, 0],
                          },
                        ],
                      ],
                    },
                  }
                : null,
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }

  async ElsoFokuTargyalsiJegyzokonyvNyomtatas({
    naplobejegyzesIds,
    iktatasIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var result = await apiService.ElsoFokuTargyalasiDokumentumAdatok({
      naplobejegyzesIds: naplobejegyzesIds,
      iktatasIds: iktatasIds,
    });
    let model = result.data;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          var esemenyLeiras = htmlToPdfMake(
            `
          <div style="margin-left: 20px; text-align: justify;">` +
              item.JegyzoKonyvText +
              `</div>
      `
          );
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'FEGYELMI TÁRGYALÁSI JEGYZŐKÖNYV',
                alignment: 'center',
                fontSize: 12,
                bold: true,
                margin: [0, 15, 0, 10],
              },
              {
                text: item.UgySzam + ' számú fegyelmi ügyben',
                alignment: 'center',
                bold: true,
                fontSize: 11,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Készült a ' + item.IntezetNev + ' hivatalos helyiségében',
                alignment: 'center',
                margin: [0, 0, 0, 0],
              },
              {
                text:
                  item.MeghallgatasEve +
                  '. év ' +
                  item.MeghallgatasHava +
                  '. hónap ' +
                  item.MeghallgatasNapja +
                  '. napon ',
                //  +
                // item.MeghallgatasOraja +
                // ':' +
                // item.MeghallgatasPerce +
                // ' órakor.',
                alignment: 'center',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'JELEN VANNAK:',
                alignment: 'center',
                margin: [0, 0, 0, 10],
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: ['*'],
                  body: [
                    [
                      {
                        ul: [
                          'Határozathozó: ' +
                            item.FegyelmiJogkorGyakorloNev +
                            ' ' +
                            (item.FegyelmiJogkorGyakorloRang != ''
                              ? item.FegyelmiJogkorGyakorloRang
                              : ''),
                          'Jegyzőkönyvvezető: ' +
                            item.JegyzoNev +
                            ' ' +
                            (item.JegyzoRang != '' ? item.JegyzoRang : ''),
                          'Egyéb: ' + item.EgyebJelenlevo,
                        ],
                        border: [false, false, false, false],
                      },
                      // {
                      //   text: 'nytsz.: ' + model.MeghallgatottAzon,
                      //   border: [false, false, false, false],
                      // },
                    ],
                  ],
                },
              },
              {
                text: [
                  {
                    text: 'Az eljárás alá vont adatai:\n',
                    decoration: 'underline',
                  },
                  'Név: ' +
                    item.EljarasAlaVontNev +
                    '\nNyilvántartási szám: ' +
                    item.EljarasAlaVontAzon +
                    '\nSzületési helye, ideje: ' +
                    item.EljarasAlaVontSzulHelye +
                    ', ' +
                    item.EljarasAlaVontSzulIdeje +
                    '\nAnyja neve: ' +
                    item.EljarasAlaVontAnyjaNeve,
                ],
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'A fegyelmi jogkör gyakorlója megállapítja, hogy a fegyelmi tárgyalás megtartásának nincs akadálya. A fegyelmi jogkör gyakorlója a tárgyalást megkezdi és ismerteti a fegyelmi eljárás alapjául szolgáló cselekményt.',
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'A jegyzőkönyv felvétele előtt felhívja a fogvatartott figyelmét a hamis vád törvényes következményeire, részletesen ismerteti a Büntető Törvénykönyvről szóló 2012. évi C. törvény alábbi vonatkozó részeit:\n268. § (1) Aki\n',
              },
              {
                text:
                  'a) mást hatóság előtt bűncselekmény elkövetésével hamisan vádol,\nb) más ellen bűncselekményre vonatkozó koholt bizonyítékot hoz a hatóság tudomására, bűntett miatt három évig terjedő szabadságvesztéssel büntetendő.\n',
              },
              {
                text:
                  'Tájékoztatom, hogy az olyan kérdésekben amelyben saját magát vagy közvetlen hozzátartozóját bűncselekmény, szabálysértés vagy fegyelem sértés elkövetésével vádolná a vallomástételt megtagadhatja, de a védekezés ezen módjáról ezzel lemond.\n',
              },
              {
                text:
                  'Tájékoztatom továbbá, hogy a fegyelmi eljárás során felmerülő költségek terhét viselnie kell, amennyiben a fegyelmi jogkör gyakorlója a fegyelmi cselekmény elkövetésében a felelősségét megállapítja.\n',
              },
              {
                text:
                  'Felhívja figyelmét arra, hogy a vallomástétel megtagadása nem akadálya az eljárás lefolytatásának és a fegyelmi felelősség megállapításának.\n',
              },
              {
                text:
                  'Figyelmeztetem, hogy a jegyzőkönyvbe foglaltak felhasználhatók Ön ellen.\n',
              },
              {
                text:
                  'A büntetés-végrehajtási intézetekben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet 15. §. (1) bekezdése alapján tájékoztatom a fogvatartottat a fegyelmi eljárás során érvényesíthető jogaira, azaz:\n',
              },
              {
                ul: [
                  'védőt bízhat meg, illetve kérheti kirendelését,',
                  'a 14/2014. (XII.17.) IM rendelet 36. §.-ban foglaltak fennállása esetén közvetítői eljárás lefolytatását kezdeményezheti,',
                  'anyanyelvét, vagy az Ön által ismert nyelvet használhatja,',
                  'védekezését szóban vagy írásban adhatja elő,',
                  'bizonyítási indítványt tehet,',
                  'a vizsgálat befejezése után az eljárási iratait tanulmányozhatja, azokról másolatot kérhet,',
                  'panasszal élhet, illetve bírósági felülvizsgálati kérelmet terjeszthet elő a fegyelmi határozattal szemben.',
                ],
                margin: [40, 0, 0, 0],
              },
              {
                text:
                  'Megértettem a fegyelmi ügy kivizsgálójának figyelmeztetését a hamis vád és hamis tanúzás törvényes következményeire. Kijelentem, hogy a fegyelmi eljárással kapcsolatos jogaimról és kötelezettségeimről kioktattak.',
                margin: [0, 0, 0, 15],
              },
              {
                margin: [-5, 0, 0, 40],
                table: {
                  widths: [180, '*', 180],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Fegyelmi jogkör gyakorlója',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -4, 0, 0],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Eljárás alá vont aláírása',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -4, 0, 0],
                      },
                    ],
                  ],
                },
              },
              {
                pageBreak: 'before',
                text:
                  'A fegyelmi jogkör gyakorlója megállapítja, hogy a fegyelmi tárgyalás megtartásának nincs akadálya. A fegyelmi jogkör gyakorlója a az eljárás alá vont fogvatartottat kioktatja a jogairól és kötelezettségeiről, felhívja a figyelmét a hamis vád törvényi következményeire. A fegyelmi jogkör gyakorlója ismerteti az elkövetett fegyelemsértést a jelenlévőkkel.',
                fontSize: 11,
                margin: [0, 0, 0, 15],
              },
              {
                text:
                  'Nevezett fogvatartott az I. fokú fegyelmi tárgyaláson történő meghallgatása során a következőket mondta el:',
                fontSize: 11,
                margin: [0, 0, 0, 10],
              },
              esemenyLeiras,
              {
                text:
                  'Az üggyel kapcsolatban egyebet elmondani nem tudok és nem is kívánok. A jegyzőkönyv az általam elmondottakat helyesen és jól tartalmazza, melyet elolvasás után helybenhagyólag aláírok.',
                margin: [0, 10, 0, 30],
              },
              {
                text:
                  'Jegyzőkönyv lezárva: ' +
                  item.JegyzoKonyvZarasOra +
                  ' óra ' +
                  item.JegyzoKonyvZarasPerc +
                  ' perc',
              },
              {
                text: 'Kelt, mint fent',
                alignment: 'center',
                margin: [0, 0, 0, 40],
              },
              {
                margin: [0, 0, 0, 40],
                table: {
                  widths: [130, 200, '*'],
                  body: [
                    [
                      {
                        text: 'Eljárás alá vont aláírása:',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                        margin: [10, 0, 0, 0],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          item.EljarasAlaVontNev +
                          ', ' +
                          item.EljarasAlaVontAzon,
                        border: [false, false, false, false],
                        margin: [10, 0, 0, 0],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                text:
                  'Tekintettel a fentiekre az elsőfokú fegyelmi határozat rendelkező részében leírtak szerint határoztam.',
                margin: [0, 0, 0, 30],
              },
              {
                margin: [0, 0, 0, 0],
                table: {
                  widths: [200, '*', 200],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Jegyzőkönyvvezető',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        text:
                          item.EljarasAlaVontNev +
                          ', ' +
                          item.EljarasAlaVontAzon,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              // saját saját
              {
                margin: [0, 30, 0, 0],
                table: {
                  widths: [200, '*', 200],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      item.EgyebJelenlevo != null && item.EgyebJelenlevo != ''
                        ? {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 200,
                                y2: 12,
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          }
                        : null,
                    ],
                    [
                      {
                        text: 'Fegyelmi jogkör gyakorlója',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      item.EgyebJelenlevo != null && item.EgyebJelenlevo != ''
                        ? {
                            text: item.EgyebJelenlevo,
                            border: [false, false, false, false],
                            alignment: 'center',
                          }
                        : null,
                    ],
                  ],
                },
              },
              // {
              //   margin: [0, 30, 0, 0],
              //   table: {
              //     widths: ['*', 200, '*'],
              //     body: [
              //       [
              //         {
              //           text: '',
              //           border: [false, false, false, false],
              //         },
              //         {
              //           canvas: [
              //             {
              //               type: 'line',
              //               x1: 0,
              //               y1: 12,
              //               x2: 200,
              //               y2: 12,
              //               lineWidth: 1,
              //             },
              //           ],
              //           border: [false, false, false, false],
              //         },
              //         {
              //           text: '',
              //           border: [false, false, false, false],
              //         },
              //       ],
              //       [
              //         {
              //           text: '',
              //           border: [false, false, false, false],
              //         },
              //         {
              //           text: 'Fegyelmi jogkör gyakorlója',
              //           border: [false, false, false, false],
              //           margin: [0, 0, 0, 0],
              //           alignment: 'center',
              //         },
              //         {
              //           text: '',
              //           border: [false, false, false, false],
              //         },
              //       ],
              //     ],
              //   },
              // },
              // item.EgyebJelenlevo != null && item.EgyebJelenlevo != ''
              //   ? {
              //       margin: [-5, 0, 0, 40],
              //       table: {
              //         widths: [120, '*', 180],
              //         body: [
              //           [
              //             {
              //               text: '',
              //               border: [false, false, false, false],
              //             },
              //             {
              //               text: '',
              //               border: [false, false, false, false],
              //             },
              //             {
              //               canvas: [
              //                 {
              //                   type: 'line',
              //                   x1: 0,
              //                   y1: 12,
              //                   x2: 180,
              //                   y2: 12,
              //                   lineWidth: 1,
              //                 },
              //               ],
              //               border: [false, false, false, false],
              //             },
              //           ],
              //           [
              //             {
              //               text: '',
              //               border: [false, false, false, false],
              //             },
              //             {
              //               text: '',
              //               border: [false, false, false, false],
              //             },
              //             {
              //               text: item.EgyebJelenlevo.split(', ')[0],
              //               border: [false, false, false, false],
              //               alignment: 'center',
              //               margin: [0, -4, 0, 0],
              //             },
              //           ],
              //         ],
              //       },
              //     }
              //   : null,
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }

  async SzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvNyomtatas({
    naplobejegyzesIds,
    iktatasIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var result = await apiService.SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok(
      {
        naplobejegyzesIds: naplobejegyzesIds,
        iktatasIds: iktatasIds,
      }
    );
    let model = result.data;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          var esemenyLeiras = htmlToPdfMake(
            `
          <div style="margin-left: 0px; text-align: justify;">` +
              item.JegyzoKonyvText +
              `</div>
      `
          );
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQICAgICAgICAgICAwMDAwMDAwMDAwEBAQEBAQECAQECAgIBAgIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD/8AAEQgAWAAwAwERAAIRAQMRAf/EAJQAAAICAwEAAAAAAAAAAAAAAAgJBgoABQcDAQACAwEAAAAAAAAAAAAAAAACAwABBAUQAAAHAQAABgIABgMBAAAAAAECAwQFBgcIABESExQJFRchIkMWGApCIyQyEQACAQIEAwQIBAUFAAAAAAABAhEhAwAxEgRBUWFxIjIT8IGRobFCIwXB0ZIU4fFSYjNyotJDNP/aAAwDAQACEQMRAD8Av8eJiYzxMTFZH7WfsC3XkXqydn8C19/bYumc/wB8T1rIZagalLZ7iK0dnp7LB6VLyNJqdwrj52lJXuuzL15JskyRzJJJBZ4Rm5WSJ0dtZsXduTuAVQOIcGskgRGZoGgSKmeuAYwYGcenpyw8fix1fpHm3OpjR9tpfQ8/PNZSdZ6xn0jGztUsVZlph89qqcdZoiErDC1mjIBZBsrJEi475aqZjC3THz8+exDMSBHMTMHiOlZpwywdeOCn8ViYzxMTGeJiYUt1v9l0nUdXJxpwplf+XvdEs1WcytOZyZ4bEucq6mqVo70XpvU0iKRtSiYx0qBEYFmdaelHIA2TSROomYxIqt3n1CzMFlANYNBNJFJBIzBwJPBY1YTl3Fg+2c8fC3Dv7s2/3E92xqZDYlub4NDOE84qxOjOYKRoCVBsaaDacukZO5/rs3DN4ywNCQEe1dicGaavrdeMu53Vy3eSxtEUJcuWwdUEkFtJJMgCBUkVGnxQIK2BAUtHeevsJz7QI9mWNLzVmR7nvjrBOKes+vsIGo6a2r8s60zTaDsbaBd2bCNH3S05+3Qxi8SGOzFJvsk3g5ly7iX/AOajX4GRKu3FM7bx0RcPkL5qhrbA6TQNQgTME0FADSCTGDAkQJU/D1e/DQ8x746L4x1CA5o+0aoIs6xaBdNcP7loQLWDJr2hGoGcr1zYTNYuKe0a6xbIhlFHi0c0SVZpGcuEgQQcyRwZRdP0VINKZnqeYHGtI4yMWshRrNcPCYP2MoxZycY8aSMbItG7+PkGDhF4xfsXiJHDR4zdtzqN3TR03UKdNQhjEOQwGKIgID4TlQ54LAE/Z122++vTkG79RMKBHaUrUbHQK6atSlp/tJp5X24RNOSkxfEjJd1ILRrqYTUTj26IOH5/JFM5DHAwGi62g0EGvYJ7a5U54mK8X1r6hpXMmOyv2XW+2Pn3PFsscpkXbdDctbAjrEx0/pWnsn9v2uAiXiRCxgY3tt+TyyBpCqR3ZYsjyQ+UL5woi5bubxa41guGFoxAA0rAkAaZB1qyszTyBkqcLVqTpPXn/HswZvCeAM9Zv+Kcpdtwkxomp8I4hotvv9A02ySWh162WTpTYoW75vZLd+cMuw1Cq1+k1RoWLSlE3TNnZ2b8CJ/IikjppKPqO4t6ls91Qad4wdRjMGa8M4ypi1LairVETPby4iBnPPhXBGdq4JzpybqfP++5BnpsitN8ltfwdek8+x8fnzbXL9qHPukQGPkRqldYtq+51xhcUkY6BmitSvWjeTc++qdqmAJEu4ZIsFPMU94A/LBBYjjBEyMjnngWYLcCjxMCfZA/HC5rHD9A6xylD89W+2yrGu/UXRm9h+wiJmL1bT3PctBjIy6zZIXMLa4EZGCi81zGBbaVTLBMHfg8UlIFoZEot3joloj3botoxRLhKgwJgmADpNJ8DxBA1EZjFtXxLJFfXEZcaE8cGj/r29z3jqTna4YJd6iCD7ihnn2St9RWeDFSOrwswW5O6LYnuaOomNkKCm9zKDhJBsHqUaP20iRVsRFECphd615UAZyQRAADDTIEEyK5zSIrEklNMqRjo339Z+w07jvOaZNXCKo8BLdK58pLWC1Q87ZaK3WhKlodnrLS21itgWYnTTVzgY2OhyJKJi3sD1i4AFDokSPVpEuEozBSVMTEkyPDObASYAYkAgCsiGZEc8V7Z3jm+s3+i5q81vSKtSRulK0a8V2159qshE4vc57Rue5sOgOgI+LXa1+4LT8/oEjYIl/F/EftpGrg6duCgi4VR51uwLtk7kaV3CwkMV1MmkkxC91EgI0jVWhymld3E6YOdZ8XaBn16Y6Qny7pMUwntiL1XrK12f1uz1JfoRqXQarS53MaR0hQM3gNlsb5/Jv9RPz1TWupTEs/agq4STeMXLlu8RZKquiaBs9nctpaYk7fUpYAq0NpIKrC1JKgKTJGoahIIxRu3LbKIAdgxOfCPbVuQy642dh4+0CXmSMnnZ927Tk8nvkLP5hrmEykxOU1zfpTDehrq2z2mqyt1vrSt9BQ/wCu2KXyYSYk/KFsiYKJNXCgJqMtfbdqLNxrf0ka3JWQREqJJKglIYiKE5kngZd9QZ4McePX4DHDbPzFoqFO1Ofb9aSejs9Pissba/p0Hne9Fzm3zFkxboN+4wbWoh5LvrJdr+3UyaAq0Yd04+O2b3Jq3dNSqC3br5bO22xv2VuMoUXNIMqAqg0aSImJMGkjxGcFLhSSAK9azxyzw2b6Q8AdYt2r1yopabN897lcFC3DOpOBnK1N1Z7UtTsVBz9ztJpUpGN0u7rPqGgrVX7BNFpHV106akAwHAS6bN5n2ih7RlrjHzCUPggaQFggMSWqDMCs5ptksNTDSQMu2pPtp6jjoH3B0uY3HXDUCz6jqUHnuZxeZ3es0WnTFWj6ktdXWH95XYbRY4edp9kTscqzmMjhRbfJMKDZJBT20yqnBUh27du9Zui5WGiOEHyxEcR3jiI7G444Kwj9M/HCzIfiKgO7oygVNG2csaGsS1OBJJ7kRFyQ6HS36nT9tf8ATQmSff2kYSGVKAet2b3RD/j4w7nY7K3dCrbt6SiE0GZVCficaldzBk8fx/LEEovFWdXGvQ9nnbnpzuZkaTmrp299jFxVUTvOPfXBeLDH+4ti6ywRLue6jtBithOKQIGYJmA3xCmUttrtUFpVtrpfUTSkqzgEDKYX44W9x1dQPmMf7Z+ONDGcKY9D59JOYacucMSr5LfdIgYyJgMEiYaIt8Dz303pbN+wiIzEG7NmU1my6LBUUipqqtRcpGP/AOgwl02tpZvbV9xfWb+hpNagXiigzUgKBQ0mowPmt5vljwxyHp+PPE+muELOwmv7mN+zUcNHqTaudGWjK6nhzyyvFM7tm8Fi1T0AnPib9kkvDUOGjlVhfmUUeNXL0UipOCELhOx2nlT5duShNFHi0kiZERIBOdMq4ZruawpNJ9v5YInhjKVOf+0ec7TnWqaui50Jg0pN9ipCRoRK9bqq9iuZLMav2CKr+d1757SPntTlXTI5lCrsjqEBBQpBXI41C1bsbO1dtIqPcvKDHEFLhMdpQdYHCpxnt3nuhGb5gZ/ST8RhnXTKfACv2H6wTu8cTCMDlvng2cfudZFFn801z6XJaRghdnIzM4/CCIO/PzUBmKn9EVvGj7efuA3t39j5kQs6edNPrzj+WED/ANL6v8fumFj3TjxK3/19RceZFeJxd/IHyEj+N+X8z8oUDeXpce/8384QPP8AqfNAP6oB46mr7wFzuaSY6TGXbHDOOmNH0+mPJu3/ANe8qZPiK8QFQ9tuCXxH8SCIoka1wzIEvYce2KJWCcMKHl/L7BY/0fyA08F5n3uAdV7TEipiK1HTOo69cSLfTCoK2tkJMvyyHuO9oczYhcc+rVIk9gZQMbY2lfzy5czdfwqrZg1tEPNRrBrJ1t0YiT9w3ODJMAVEwCX1eM2zS7etOCuu5DSKVjcsCKHnnHXI5Lot7l3R+Ppyw2mzVfkBfgKv2ST77/F5ot1RfNWa9UhXqUQJbZrldNNTuNPCmrVdSvIgk+tEzH/CLHlWafF9wRAyRjeMgsze8nQZFNMwfDGfrnDw3zE1n07cAPzY5qDjqzmQmf68beqLG6tcYKoa2aEja8NwrlffciQcQ6GMiI6IjQOyYsCNRcIt0SPPZ98AN7nma99aazt7NtlKEbhKTP8A1XjzMdnDphShVdFXwjVH6GwU/wBiv7GsXXOgUfL8E2zbZwmO43bJYcpr9OlI2vRMnlv2AZLFlnX9tvVNKg+kbZpDAEkUSuB+GRysYSgj6TrsX7a+bYM+YWBHZ9P/AIn3YG2R59xfmJB9QUD8RgZoir9QMrm2nl+DeyvgJavJXM4kpmNncfhXXS37XRMCP7zDzejU/wCYUvP+Dv8A6vP/AJeB3IF26GTwhVHrCqD8DjUpgV6/jiI57nnV1ZqsLCynBnYYPGFUy6GXBtUcaXRB7UMf+uKjTAJq/vEnrRLO8s2oET+Qe4gmxU8g+WBUxdQ3lQPAGn1s5EfqGFuCzIRkDJ/THxx64dk2xdJRM7X84u+C5JHq801FOYZ9AntLS7pq77mXVePnSQhqnNtI1spVY+2HcrkOu596Qbi39z2xFUdNp0tfbrdxqi6bq+LSRovsZEhuzp7hBb+qW4wOHMT05/xw1ixcd67K8lQtcS2LnlDTEut9R6beWtwS3Diqn7PsetOl6fHPiThbMZWJZ6UCSbpRcfecNDAKZSn8i4zGhajSKZ9CKHn6o6HDCCXniDOWAEpOSani3cvJFR0m/wCCX8rxNpKw73FXVrVcMfxE7zbnblKyJWSSkU003iVDRcNjImKPuLLpmAQTIJiukfs7KLEDcqM5P+O7HAcM+ZqIwhbYtNbStJFf9DYLXrTo7buWu49X0DKMzzDSo20894PT5trfdBs1EfxUjWYztDYWq0X+EpFvaybGRgc7kWyhlDIKIu1G/kUyZjmKj9vuXuvf2+gAEAlqiO5kAQZlhnQ+o4QFY33dCAwMVE5hTzHLEWYfab2k/sKNdJy/zaRZa4OqUDoehNBMiWUaa4GPqL+2GKAoLI0x5vAN/wDXxQ9Ih7n8PEvLv7LaddgsVU+F/mAOerqBhwG5ORX2dv8Ad0xpav8AbV2RaoljMs+WudGrSQiafNIlddB38Vis7nRuVb6wKoRLFjlBw1jetIVBUoCIe/GPfIRKKImojfrpl7HfmO6/AsDPe/t9/Q4FjfUgSskwKdJ/q5YT7o8fiqecRD/fMip1wtsnzJcoFGxEycmqxVSmD8x9bIxzD+5F6u+m4WEQ0ebYKMXCqKRUnQFeKAiKKiqe/b6b20Fll1XWD6Qc5/cMSRJoIAr2cwMGGK34kadM0y/ngxLpesbPytB4fIYBOMue0/s06XcV3QJDKax/iPJwraa6Ks0Cxg3iTleJQrJlZFqhEv1YlCBdTSIsmjpR4mVMU27Ra35YWCtuiwZJNvuwBlmKmAMzInDGuKG1/KDnn6dmOXcOR2LI9V81vsgymOzkhLVEws3INcmRyxeyKxkDx1/EiKkLCS0vHMZZZ8omdykUPddKLEAQces97i2E2tkMqi8NwgMRP+O8anjXiJB5zkm3rBtq86q+3Q2HBda8kWPqHt2bpUV0PaMMZv8Amii3pFvX83zu7KWl9ASnROMWFYr+8xr9RgnXqnu5iGRblEouJRBY4gZJMDNtFrSsRBRzUGeS9n9I44MKockGrQT6hFMR1t9Peps5hOcbfYHoxXyVqXuJDKc8c/qJhNOtEDUlze3+AIQWw2/zUKn5eRW4+z/EoeYg/wBQd+rwBPGAAAIypA4TTDQSMssauA+mLQ63GM4eM+wDSyMWEbXIhuVbn7AFlvx9TqXP1KhE1Vj17/sXSg+Y6kVVT0h7qiDs4gAuz+myVYKCBKAwa8SSePHUfdgSAxBOYMj2R8MVrNG0XRNTqON5yfE9LtUFCaxSIW82qB2mPy6L1ehZvVuiMh0h1KWDG5s+g0aD0e030zFlDwTeUk15dgpALsSPXLFi+fYazb2wtNcNu9cLBCqwA3nG6oliNNIGo1jvjUCcKZS9wgTkM+lDE+7lgoC7FXpnHIzDHvG0G2oMFr1m6FbJm+wHpFUyeh3bOZhedsRrNG5u5aS2bvG4uHiLZu2WhU4RY9rjW6pWE9+Iwnztrd/b3XuWrhJANJMQCJKiGAyWjalikqWrTKFNRkESe7lwOUR1joTiU/WJH61/ktzfTZ+k/go+I6S2JRo3t22y2p6Qem2dpHa3CfhZaXj5qauFaxSv4Wxrcy5npdtOgnOwz1NspGumzhfduPJezbsAtpTS4MRqYBxzqDrYmBQ+uDC6SC0yD8QRw7ezFgf7ZuWbxt2Owmv4yz02Y2rndpoU1A0jJNOt2S3DVqbcqW/grPQWVlpU7XJR9Kxc2hD2mGj1nXxH03XGzY5P/R6i1YYBtDsBbaJmqyMi3TgTwBJrlgyDmJnFfKsX/nCatUcYexdESgHOrS0GdpM977rXXbatN+ohpRWEzETe1R0zCOmVDTFqqm8SQdIIgY6gFOAnCbtLtq75SINICAkCe8VUkSOJknFppIBPXOnPh7MRjO9AwuSqcE6sPZ18PKuqrlT10Z79hOwNXKktM459as7ZvUiO5I+lVa26loZlk/SAJLupFLyL8ICNxc3VWzcVBoYMSdOcNc0+4DtgYB5DqFyJr2aZ+OOP44lW3OZtIyFWZPoL87pjeGfmmI2wxk3Et7nNxSSi87PpRbCQfmrCsc2cO5s/tSUYs1Zz71zCua9bYRjKLtg2t2FbbsYaAQUbUY1QTEmSrrk+qAGD2yAa3cdrQMXkryMcCOfKcuBx0Zs0Vbyftv13CRwUWdoSny7A1kouRbWf3XL0qi5465pPy3tgUz1BQWdkQsrUFvNhoLf3r2kIQr7L7iGvWyv07mcgDwmMrirVWBKlRIm2ZS0gNqalxTPStJHQ8VPHLhhsX0v4ZarxrNq69esxQxSDosxmOMvlGzJtDaVcZmdj1rdplEZw6TCBNSKPEQ7mvwkpHoGhJQZ2XXg0oiMcfhGTLq3LSiy76jOogioPymZJDEE6wanu6paSTiW1EyOH8PhPLFkzwjBYALWPr5yi06FZdsyNtUMb2e7O2j/RJpXKaFo+f668YolbtnmsZxaY4iM5NEakBH8zFP4SfMkAEO+UTKUgSWgLJ0g0E0ExMdsDnBrE5iVrMmcQM1auOaiVPXfrwxDVYdqUTu9A5bgs5mHRiHcGIRZfG9Tj6hbWZiFXFRVGMl7CqACf0esQ8jAGud43BlJBEmQKgZTqOXKRnXElsopivLhPB3cduiIWAo/KtsqcWeZnXzqd2yYb4NVq5FSlxtU1ENGdfnT3XSmMvWmMsugVCOg3UQgs6Iux9MZIzsG502boF1muFsiCI1aqCawqkNIma901Loj4y3duLtxbiSlxTmPTiMxkRQ1FG34R9J0E4eRtl7Y0qN28GpWbgcIziCkKLgBpBnDEgUjWz8jIyN91Bo1iwFoi1fu2EWeLTaRztk7ZxcWmzjXjoNpR9KCK1MTPKhHCMswZJnVpkkt6en5YevGxsdDRzCIiGDKKiYpk1jYuLjWqDGOjY5igRsyYMGTVNJszZM2yRU0kkylImQoFKAAAB4Tgsf/Z',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text:
                  'JEGYZŐKÖNYV TANÚ (SZEMÉLYI ÁLLOMÁNYI TAG)\nMEGHALLGATÁSÁRÓL',
                alignment: 'center',
                fontSize: 12,
                bold: true,
                margin: [0, 15, 0, 10],
              },
              {
                text: item.UgySzam + ' számú fegyelmi ügyben',
                alignment: 'center',
                bold: true,
                fontSize: 11,
                margin: [0, 0, 0, 30],
              },
              {
                text:
                  'Készült a ' + item.IntezetNev + ' hivatalos helyiségében.',
                alignment: 'center',
                margin: [0, 0, 0, 0],
              },
              {
                text:
                  item.MeghallgatasEve +
                  '. év ' +
                  item.MeghallgatasHava +
                  '. hónap ' +
                  item.MeghallgatasNapja +
                  '. napon. ',
                // +
                // item.MeghallgatasOraja +
                // ':' +
                // item.MeghallgatasPerce +
                // ' órakor.',
                alignment: 'center',
                margin: [0, 0, 0, 30],
              },
              {
                text: 'JELEN VANNAK:',
                alignment: 'center',
                margin: [0, 0, 0, 50],
              },
              {
                margin: [-5, 0, 0, 50],
                table: {
                  widths: ['*'],
                  body: [
                    [
                      {
                        ul: [
                          'Tanú (Személyi állomány tagja): ' +
                            item.MeghallgatottNev +
                            ' ' +
                            item.MeghallgatottRang +
                            ' \n',
                          // item.MeghallgatottRang,
                          'Meghallgató: ' +
                            item.MeghallgatoNev +
                            ' ' +
                            (item.MeghallgatoRang != ''
                              ? item.MeghallgatoRang
                              : ''),
                          // 'Jegyzőkönyvvezető: ' +
                          //   model.JegyzoNev +
                          //   ', ' +
                          //   model.JegyzoRang,
                          // 'Egyéb: ' + model.EgyebJelenlevo,
                        ],
                        border: [false, false, false, false],
                      },
                      // {
                      //   text: 'nytsz.: ' + model.MeghallgatottAzon,
                      //   border: [false, false, false, false],
                      // },
                    ],
                  ],
                },
              },
              // {
              //   text: [
              //     { text: 'Tanú adatai:\n', decoration: 'underline' },
              //     'Név: ' +
              //       item.MeghallgatottNev +
              //       ' ' +
              //       item.MeghallgatottRang,
              //     // '\nElfogultnak érzi-e magát?: ' + model.Elfogult,
              //   ],
              //   margin: [0, 0, 0, 10],
              // },
              {
                text:
                  'A hamis vád és hamis tanúzás törvényes következményeivel kapcsolatosan részletesen ismertettem a Büntető Törvénykönyvről szóló 2012. évi C. törvény alábbi vonatkozó részeit:\n268. § (1) Aki\n',
              },
              {
                text:
                  'a) mást hatóság előtt bűncselekmény elkövetésével hamisan vádol,\nb) más ellen bűncselekményre vonatkozó koholt bizonyítékot hoz a hatóság tudomására, bűntett miatt három évig terjedő szabadságvesztéssel büntetendő.',
                margin: [3, 0, 0, 10],
              },
              {
                text:
                  '272. § A tanú, aki hatóság előtt az ügy lényeges körülményére valótlan vallomást tesz, vagy a valót elhallgatja, hamis tanúzást követ el.',
              },
              {
                text:
                  'Tájékoztatom, hogy az olyan kérdésekben amelyben saját magát vagy közvetlen hozzátartozóját bűncselekmény, szabálysértés vagy fegyelem sértés elkövetésével vádolná a vallomástételt megtagadhatja, de a védekezés ezen módjáról ezzel lemond. Felhívom a figyelmét arra, hogy a vallomástétel megtagadása nem akadálya az eljárás lefolytatásának és a fegyelmi felelősség megállapításának.\nFigyelmeztetem, hogy a jegyzőkönyvbe foglaltak felhasználhatók Ön ellen.',
                margin: [3, 0, 0, 50],
              },
              // {
              //   ul: [
              //     'A büntetés-végrehajtási intézetekben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet 16. §. alapján tájékoztatom a fogvatartottat a fegyelmi eljárás során érvényesíthető jogaira, azaz:',
              //     'védőt bízhat meg,',
              //     'a 14/2014. (XII.17.) IM rendelet 36. §.-ban foglaltak fennállása esetén közvetítői eljárás lefolytatását kezdeményezheti,',
              //     'anyanyelvét, vagy az Ön által ismert nyelvet használhatja,',
              //     'vallomását szóban vagy írásban adhatja elő.',
              //   ],
              //   margin: [40, 0, 0, 0],
              // },
              // {
              //   text:
              //     'Megértettem a fegyelmi ügy kivizsgálójának figyelmeztetését a hamis vád és hamis tanúzás törvényes következményeire. Kijelentem, hogy a fegyelmi eljárással kapcsolatos jogaimról és kötelezettségeimről kioktattak.',
              //   margin: [0, 0, 0, 15],
              // },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [60, '*', 150],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 150,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: 'Tanú (személyi állomány tagja) aláírása',
                        border: [false, false, false, false],
                        margin: [0, -4, 0, 0],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              {
                pageBreak: 'before',
                text: 'Nevezett az üggyel kapcsolatban az alábbiakat közli:',
                fontSize: 11,
                margin: [0, 0, 0, 0],
              },
              esemenyLeiras,
              {
                text:
                  'Az üggyel kapcsolatban egyebet elmondani nem tudok és nem is kívánok. A jegyzőkönyv az általam elmondottakat helyesen és jól tartalmazza, melyet elolvasás után helybenhagyólag aláírok.',
                margin: [0, 30, 0, -5],
              },
              {
                canvas: [
                  {
                    type: 'line',
                    x1: 0,
                    y1: 12,
                    x2: 510,
                    y2: 12,
                    lineWidth: 1,
                  },
                ],
                border: [false, false, false, false],
                margin: [0, 0, 0, 5],
              },
              {
                text:
                  'Jegyzőkönyv lezárva: ' +
                  item.JegyzoKonyvZarasOra +
                  ' óra ' +
                  item.JegyzoKonyvZarasPerc +
                  ' perckor',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Kelt, mint fent',
                alignment: 'center',
                margin: [0, 0, 0, 40],
              },
              {
                margin: [-5, 0, 0, 40],
                table: {
                  widths: [180, 180, '*'],
                  body: [
                    [
                      {
                        text: 'Tanú (személyi állomány tagja) aláírása: ',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                        margin: [-15, 0, 0, 0],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          item.MeghallgatottNev + ' ' + item.MeghallgatottRang,
                        border: [false, false, false, false],
                        margin: [0, -4, 0, 0],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                margin: [-5, 0, 0, 40],
                table: {
                  widths: [180, '*', 180],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Meghallgató',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -4, 0, 0],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Tanú',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -4, 0, 0],
                      },
                    ],
                  ],
                },
              },
              item.EgyebJelenlevo != null && item.EgyebJelenlevo != ''
                ? {
                    margin: [-5, 0, 0, 40],
                    table: {
                      widths: [120, '*', 180],
                      body: [
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 180,
                                y2: 12,
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                        ],
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: item.EgyebJelenlevo.split(', ')[0],
                            border: [false, false, false, false],
                            alignment: 'center',
                            margin: [0, -4, 0, 0],
                          },
                        ],
                      ],
                    },
                  }
                : null,
            ],
          };
        }),
      ],
      pageBreakBefore: function(
        currentNode,
        followingNodesOnPage,
        nodesOnNextPage,
        previousNodesOnPage
      ) {
        if (
          currentNode.id === 'NoBreak' &&
          previousNodesOnPage.length != 1 &&
          currentNode.pageNumbers.length != 1
        ) {
          return true;
        }
        return false;
      },
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }

  //   async TanuAllomanyiTagJegyzokonyvNyomtatas(fegyelmiUgyId) {
  //     if (pdfMake.vfs == undefined) {
  //       pdfMake.vfs = pdfFonts.pdfMake.vfs;
  //     }
  //     var model = {
  //       fegyelmiUgyId: 101010,
  //       IntezetNev: 'Váci fegyház és börtön',
  //       Iktatoszam: 46843278,
  //       UgySzam: '18859/99214',
  //       MeghallgatasEve: '2019',
  //       MeghallgatasHava: '10',
  //       MeghallgatasNapja: '01',
  //       MeghallgatasOraja: '16:15',
  //       AllomanyiTanuNev: 'Teszt Elek',
  //       AllomanyiTanuSzulIdeje: '1996.07.25.',
  //       AllomanyiTanuSzulHelye: 'Budapest',
  //       AllomanyiTanuAnyjaNeve: 'Fekete Sára',
  //       Elfogult: 'nem',
  //       MeghallgatoNev: 'Fülelő Géza',
  //       MeghallgatoRang: 'őrnagy',
  //       JegyzoNev: 'Jegyzet Elek',
  //       JegyzoRang: 'főjegyző',
  //       EgyebJelenlevo: 'Jelen Lévő1, Jelen Lévő2',
  //       JegyzoKonyvText:
  //         'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.',
  //       JegyzoKonyvZarasOra: '18',
  //       JegyzoKonyvZarasPerc: '47',
  //     };

  //     var jegyzokonyvText = htmlToPdfMake(
  //       `
  //     <div style="margin-left: 20px; text-align: justify;">` +
  //         model.JegyzokonyvText +
  //         `</div>
  // `
  //     );

  //     var docDefinition = {
  //       footer: function(currentPage, pageCount, pageSize) {
  //         // you can apply any logic and return any valid pdfmake element

  //         return [
  //           {
  //             text: pageCount >= 2 ? pageCount + '. oldal' : '',
  //             alignment: 'center',
  //             opacity: 0.5,
  //           },
  //         ];
  //       },
  //       content: [
  //         {
  //           image:
  //             'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
  //           alignment: 'center',
  //           width: 30,
  //           height: 55,
  //           opacity: 0.5,
  //         },
  //         {
  //           text: model.IntezetNev,
  //           alignment: 'center',
  //           fontSize: 12,
  //           opacity: 0.5,
  //           margin: [0, 0, 0, 10],
  //         },
  //         {
  //           text: model.Iktatoszam,
  //           alignment: 'right',
  //           fontSize: 10,
  //         },
  //         {
  //           text: 'JEGYZŐKÖNYV TANÚ (SZEMÉLYI ÁLLOMÁNYI TAG)\nMEGHALLGATÁSÁRÓL',
  //           alignment: 'center',
  //           fontSize: 12,
  //           bold: true,
  //           margin: [0, 15, 0, 15],
  //         },
  //         {
  //           text: model.UgySzam + ' számú fegyelmi ügyben',
  //           alignment: 'center',
  //           bold: true,
  //           fontSize: 11,
  //           margin: [0, 0, 0, 30],
  //         },
  //         {
  //           text: 'Készült a ' + model.IntezetNev + ' hivatalos helyiségében.',
  //           alignment: 'center',
  //           margin: [0, 0, 0, 0],
  //         },
  //         {
  //           text:
  //             model.MeghallgatasEve +
  //             '. év ' +
  //             model.MeghallgatasHava +
  //             '. hónap ' +
  //             model.MeghallgatasNapja +
  //             '. napon ' +
  //             model.MeghallgatasOraja +
  //             ' órakor.',
  //           alignment: 'center',
  //           margin: [0, 0, 0, 50],
  //         },
  //         {
  //           text: 'JELEN VANNAK:',
  //           alignment: 'center',
  //           margin: [0, 0, 0, 50],
  //         },
  //         {
  //           ul: [
  //             'Tanú (Személyi állomány tagja): ' + model.AllomanyiTanuNev,
  //             'Meghallgató: ' + model.MeghallgatoNev,
  //           ],
  //           margin: [0, 0, 0, 50],
  //         },
  //         {
  //           text:
  //             'A hamis vád és hamis tanúzás törvényes következményeivel kapcsolatosan részletesen ismertettem a Büntető Törvénykönyvről szóló 2012. évi C. törvény alábbi vonatkozó részeit:\n268. § (1) Aki\n',
  //         },
  //         {
  //           text:
  //             'a) mást hatóság előtt bűncselekmény elkövetésével hamisan vádol,\nb) más ellen bűncselekményre vonatkozó koholt bizonyítékot hoz a hatóság tudomására, bűntett miatt három évig terjedő szabadságvesztéssel büntetendő.',
  //           margin: [3, 0, 0, 10],
  //         },
  //         {
  //           text:
  //             '272. § A tanú, aki hatóság előtt az ügy lényeges körülményére valótlan vallomást tesz, vagy a valót elhallgatja, hamis tanúzást követ el.',
  //           margin: [0, 0, 0, 10],
  //         },
  //         {
  //           text:
  //             'Tájékoztatom, hogy az olyan kérdésekben amelyben saját magát vagy közvetlen hozzátartozóját bűncselekmény, szabálysértés vagy fegyelem sértés elkövetésével vádolná a vallomástételt megtagadhatja, de a védekezés ezen módjáról ezzel lemond. Felhívom a figyelmét arra, hogy a vallomástétel megtagadása nem akadálya az eljárás lefolytatásának és a fegyelmi felelősség megállapításának.\nFigyelmeztetem, hogy a jegyzőkönyvbe foglaltak felhasználhatók Ön ellen.',
  //           margin: [0, 0, 0, 40],
  //         },
  //         {
  //           pageBreak: 'after',
  //           margin: [-5, 0, 0, 10],
  //           table: {
  //             widths: [60, '*', '*'],
  //             body: [
  //               [
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                 },
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                   alignment: 'right',
  //                 },
  //                 {
  //                   canvas: [
  //                     {
  //                       type: 'line',
  //                       x1: 0,
  //                       y1: 12,
  //                       x2: 200,
  //                       y2: 12,
  //                       lineWidth: 1,
  //                     },
  //                   ],
  //                   border: [false, false, false, false],
  //                 },
  //               ],
  //               [
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                 },
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                   alignment: 'right',
  //                 },
  //                 {
  //                   text: 'Tanú (személyi állomány tagja) aláírása',
  //                   border: [false, false, false, false],
  //                   margin: [10, -4, 0, 0],
  //                 },
  //               ],
  //             ],
  //           },
  //         },
  //         {
  //           text: 'Nevezett az üggyel kapcsolatban az alábbiakat közli:',
  //           fontSize: 11,
  //           margin: [0, 0, 0, 10],
  //         },
  //         // {
  //         //   text: model.JegyzoKonyvText,
  //         //   margin: [0, 0, 0, 10],
  //         // },
  //         jegyzoKonyvText,
  //         {
  //           text:
  //             'Az üggyel kapcsolatban egyebet elmondani nem tudok és nem is kívánok. A jegyzőkönyv az általam elmondottakat helyesen és jól tartalmazza, melyet elolvasás után helybenhagyólag aláírok.',
  //           margin: [0, 0, 0, -8],
  //         },
  //         {
  //           canvas: [
  //             {
  //               type: 'line',
  //               x1: 0,
  //               y1: 12,
  //               x2: 470,
  //               y2: 12,
  //               lineWidth: 1,
  //             },
  //           ],
  //         },
  //         {
  //           text:
  //             'Jegyzőkönyv lezárva: ' +
  //             model.JegyzoKonyvZarasOra +
  //             ' óra ' +
  //             model.JegyzoKonyvZarasPerc +
  //             ' perckor',
  //           margin: [0, 0, 0, 15],
  //         },
  //         {
  //           text: 'Kelt, mint fent',
  //           alignment: 'center',
  //           margin: [0, 0, 0, 40],
  //         },
  //         {
  //           margin: [-5, 0, 0, 40],
  //           table: {
  //             widths: [200, 180, '*'],
  //             body: [
  //               [
  //                 {
  //                   text: 'Tanú (személyi állomány tagja) aláírása: ',
  //                   border: [false, false, false, false],
  //                 },
  //                 {
  //                   canvas: [
  //                     {
  //                       type: 'line',
  //                       x1: 0,
  //                       y1: 12,
  //                       x2: 180,
  //                       y2: 12,
  //                       lineWidth: 1,
  //                     },
  //                   ],
  //                   border: [false, false, false, false],
  //                   margin: [-15, 0, 0, 0],
  //                 },
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                 },
  //               ],
  //               [
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                 },
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                   margin: [18, -4, 0, 0],
  //                 },
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                 },
  //               ],
  //             ],
  //           },
  //         },
  //         {
  //           margin: [-5, 0, 0, 40],
  //           table: {
  //             widths: [180, '*', 180],
  //             body: [
  //               [
  //                 {
  //                   canvas: [
  //                     {
  //                       type: 'line',
  //                       x1: 0,
  //                       y1: 12,
  //                       x2: 180,
  //                       y2: 12,
  //                       lineWidth: 1,
  //                     },
  //                   ],
  //                   border: [false, false, false, false],
  //                 },
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                 },
  //                 {
  //                   canvas: [
  //                     {
  //                       type: 'line',
  //                       x1: 0,
  //                       y1: 12,
  //                       x2: 180,
  //                       y2: 12,
  //                       lineWidth: 1,
  //                     },
  //                   ],
  //                   border: [false, false, false, false],
  //                 },
  //               ],
  //               [
  //                 {
  //                   text: 'Meghallgató',
  //                   border: [false, false, false, false],
  //                   alignment: 'center',
  //                   margin: [0, -4, 0, 0],
  //                 },
  //                 {
  //                   text: '',
  //                   border: [false, false, false, false],
  //                 },
  //                 {
  //                   text: 'Tanú',
  //                   border: [false, false, false, false],
  //                   alignment: 'center',
  //                   margin: [0, -4, 0, 0],
  //                 },
  //               ],
  //             ],
  //           },
  //         },
  //       ],
  //       pageSize: 'A4',
  //       pageMargins: [40, 150, 40, 70],
  //       styles: {
  //         header: {
  //           fontSize: 16,
  //           bold: true,
  //           alignment: 'center',
  //           margin: [0, 20, 0, 0],
  //         },
  //         subheader: {
  //           fontSize: 15,
  //           bold: true,
  //         },
  //         quote: {
  //           italics: true,
  //         },
  //         small: {
  //           fontSize: 8,
  //         },
  //         footersmall: {
  //           fontSize: 6,
  //         },
  //         tableExample: {
  //           margin: [-5, 30, 0, 15],
  //         },
  //         tableHeader: {
  //           bold: true,
  //           fontSize: 8,
  //           margin: [0, 10, 5, 10],
  //         },
  //         tableHeader2: {
  //           bold: true,
  //           fontSize: 8,
  //           alignment: 'center',
  //           margin: [0, 10, 0, 10],
  //         },
  //         tableCell: {
  //           fontSize: 8,
  //           alignment: 'right',
  //           margin: [0, 5, 5, 5],
  //         },
  //       },
  //       defaultStyle: {
  //         columnGap: 20,
  //         fontSize: 10.5,
  //       },
  //     };

  //
  // pdfMake.fonts = {
  //   TimesNewRoman: {
  //     normal: 'TimesNewRoman.ttf',
  //     bold: 'TimesNewRoman.ttf',
  //     italics: 'TimesNewRoman.ttf',
  //     bolditalics: 'TimesNewRoman.ttf',
  //   },
  // };
  // pdfMake.createPdf(docDefinition).download();

  //     /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
  //     //pdfMake.createPdf(docDefinition).print();
  //   }

  async FegyelmiHatarozatNyomtatas(fegyelmiUgyId, iktatasId, naploId) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var model = await apiService.FegyelmiHatarozatNyomtatas(
      fegyelmiUgyId,
      iktatasId,
      naploId
    );

    var indoklas = htmlToPdfMake(
      `
    <div style="margin-left: 20px; text-align: justify;">` +
        model.IndoklasText +
        `</div>
`
    );

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        {
          image:
            'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
          alignment: 'center',
          width: 30,
          height: 55,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.IntezetNev,
          alignment: 'center',
          fontSize: 12,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.Iktatoszam,
          alignment: 'right',
          fontSize: 10,
        },
        {
          text: 'I. FOKÚ\n FEGYELMI HATÁROZAT',
          alignment: 'center',
          fontSize: 14,
          bold: true,
          margin: [0, 15, 0, 10],
        },
        {
          text:
            'A büntetés-végrehajtási intézetben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet rendelkezéseire figyelemmel',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*'],
            body: [
              [
                {
                  text: 'Név',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottNev,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Azonosító',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvAzon,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Születési hely',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottSzulHelye,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Születési idő',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottSzulIdeje,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Anyja neve',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottAnyjaNeve,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Elhelyezése',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvElhelyezese,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Bv.i-ben tart. jogcím/fok',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.BvFok,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Végrehajtási fokozat',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.VegrehajtasiFok,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Szabadulás aktuális dátum',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvSzabadul,
                  border: [false, false, false, false],
                },
              ],
            ],
          },
        },
        {
          text:
            'fogvatartott ellen kezdeményezett fegyelmi ügyben a fegyelmi felelősség megállapítva.',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*'],
            body: [
              [
                {
                  text: 'Fegyelmi vétség',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FegyVetseg,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Fegyelmi fenyítés',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FenyTipus,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Fenyítés tartama',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FenyIdotart + model.KietkezesCsokkentes,
                  border: [false, false, false, false],
                },
              ],
            ],
          },
        },
        {
          text:
            model.FegyelmiUgyekOsszevont != ''
              ? 'Összevont fegyelmi ügyek:'
              : null,
          fontSize: 12,
          margin: [20, 0, 0, 0],
        },
        {
          text: model.FegyelmiUgyekOsszevont,
          fontSize: 12,
          margin: [30, 0, 0, 10],
        },
        {
          text: 'Indoklás:',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        // {
        //   text: indoklas,
        //   fontSize: 12,
        //   margin: [20, 0, 0, 10],
        // },
        indoklas,
        // {
        //   text: model.FegyelmiPaymentText,
        //   fontSize: 12,
        //   margin: [20, 20, 0, 30],
        // },
        // {
        //   text: model.FegyelmiResultText,
        //   fontSize: 12,
        //   margin: [20, 0, 0, 30],
        // },
        {
          text:
            'E fegyelmi ügyben keletkezett határozatot a fogvatartott előtt szóban kihirdettem, indokoltam. A határozat egy példányát a fogvatartottnak átadtam.',
          fontSize: 12,
          margin: [0, 0, 0, 20],
        },
        {
          text:
            model.PanasszalElt == true
              ? model.FenyTipus == 'Magánelzárás'
                ? 'Az eljárás alá vont a fegyelmi határozottal szemben bírósági felülvizsgálati kérelemmel él.'
                : 'Az eljárás alá vont a fegyelmi határozattal szemben panasszal él.'
              : model.FenyTipus == 'Magánelzárás'
              ? 'Az eljárás alá vont a fegyelmi határozottal szemben bírósági felülvizsgálati kérelemmel nem él.'
              : 'Az eljárás alá vont a fegyelmi határozattal szemben panasszal nem él.',
          fontSize: 12,
          margin: [0, 0, 0, 20],
        },
        {
          text: 'Határozat: ' + model.HatarozatHelyDatum,
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          margin: [-5, 0, 0, 15],
          table: {
            widths: ['*', 90, 180],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: 'Határozathozó: ',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 180,
                      y2: 12,
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                  margin: [0, 0, 0, 0],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  margin: [0, 0, 0, 0],
                },
                {
                  text:
                    (model.HatarozathozoNev && model.HatarozathozoNev != ''
                      ? model.HatarozathozoNev + '\n'
                      : '') +
                    (model.HatarozathozoRang && model.HatarozathozoRang != ''
                      ? model.HatarozathozoRang + '\n'
                      : '') +
                    (model.HatarozathozoTitulus &&
                    model.HatarozathozoTitulus != ''
                      ? model.HatarozathozoTitulus + '\n'
                      : ''),
                  border: [false, false, false, false],
                  alignment: 'center',
                  margin: [0, 0, 0, 0],
                  bold: 'true',
                },
              ],
            ],
          },
        },
        {
          margin: [-5, 0, 0, 40],
          table: {
            widths: ['*', 170, 180],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '1 pld-t a határozatból átvettem: ',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 180,
                      y2: 12,
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                  margin: [0, 0, 0, 0],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  margin: [0, 0, 0, 0],
                },
                {
                  text: 'fogvatartott',
                  border: [false, false, false, false],
                  alignment: 'center',
                  margin: [0, -5, 0, 0],
                },
              ],
            ],
          },
        },
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }

  async FegyelmiHatarozatMegszuntetesNyomtatas(
    fegyelmiUgyId,
    iktatasId,
    naploId
  ) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model = await apiService.FegyelmiHatarozatMegszuntetesNyomtatas(
      fegyelmiUgyId,
      iktatasId,
      naploId
    );

    var indoklas = htmlToPdfMake(
      `
    <div style="margin-left: 20px; text-align: justify;">` +
        model.IndoklasText +
        `</div>
`
    );

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        {
          image:
            'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
          alignment: 'center',
          width: 30,
          height: 55,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.IntezetNev,
          alignment: 'center',
          fontSize: 12,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.Iktatoszam,
          alignment: 'right',
          fontSize: 10,
        },
        {
          text: 'FEGYELMI HATÁROZAT',
          alignment: 'center',
          fontSize: 14,
          bold: true,
          margin: [0, 15, 0, 10],
        },
        {
          text:
            'A büntetés-végrehajtási intézetben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet rendelkezéseire figyelemmel',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*'],
            body: [
              [
                {
                  text: 'Név',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottNev,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Azonosító',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvAzon,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Születési hely',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottSzulHelye,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Születési idő',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottSzulIdeje,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Anyja neve',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottAnyjaNeve,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Elhelyezése',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvElhelyezese,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Bv.i-ben tart. jogcím/fok',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.BvFok,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Szabadulás aktuális dátum',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvSzabadul,
                  border: [false, false, false, false],
                },
              ],
            ],
          },
        },
        {
          text:
            'fogvatartott ellen kezdeményezett fegyelmi ügyben a fegyelmi eljárást megszüntettem.',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          text: 'Indoklás:',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.MegszuntetesOka,
          margin: [20, 0, 0, 10],
        },
        indoklas,
        // {
        //   text: model.DDLText,
        //   fontSize: 12,
        //   margin: [30, 0, 0, 10],
        // },
        {
          text: 'E fegyelmi határozat jogerős és végrehajtható.',
          fontSize: 12,
          margin: [0, 0, 0, 20],
        },
        {
          text:
            'E fegyelmi ügyben keletkezett határozatot a fogvatartott előtt szóban kihirdettem, indokoltam.\n A határozat egy példányát a fogvatartottnak átadtam.',
          fontSize: 12,
          margin: [0, 0, 0, 20],
        },
        {
          text: 'Határozat: ' + model.HatarozatHelyDatum,
          fontSize: 12,
          margin: [0, 0, 0, 30],
        },
        {
          margin: [-5, 0, 0, 15],
          table: {
            widths: ['*', 90, 180],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: 'Határozathozó: ',
                  border: [false, false, false, false],
                  alignment: 'right',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 180,
                      y2: 12,
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                  margin: [0, 0, 0, 0],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  margin: [0, 0, 0, 0],
                },
                {
                  text:
                    (model.HatarozathozoNev && model.HatarozathozoNev != ''
                      ? model.HatarozathozoNev + '\n'
                      : '') +
                    (model.HatarozathozoRang && model.HatarozathozoRang != ''
                      ? model.HatarozathozoRang + '\n'
                      : '') +
                    (model.HatarozathozoTitulus &&
                    model.HatarozathozoTitulus != ''
                      ? model.HatarozathozoTitulus + '\n'
                      : '') +
                    (model.HatarozathozoSzam && model.HatarozathozoSzam != ''
                      ? model.HatarozathozoSzam + '\n'
                      : ''),
                  border: [false, false, false, false],
                  alignment: 'center',
                  margin: [0, -5, 0, 0],
                  bold: 'true',
                },
              ],
            ],
          },
        },
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };
    console.log(docDefinition);

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async HatarozatReintegraciosNyomtatas(fegyelmiUgyId, naploId, iktatasId) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var model = await apiService.HatarozatReintegraciosNyomtatas(
      fegyelmiUgyId,
      naploId,
      iktatasId
    );
    var indoklas = htmlToPdfMake(
      `
    <div style="margin-left: 20px; text-align: justify;">` +
        model.IndoklasText +
        `</div>
`
    );
    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        {
          image:
            'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
          alignment: 'center',
          width: 30,
          height: 55,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.IntezetNev,
          alignment: 'center',
          fontSize: 12,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.Iktatoszam,
          alignment: 'right',
          fontSize: 10,
        },
        {
          text: 'REINTEGRÁCIÓS TISZTI JOGKÖRBEN HOZOTT\nFEGYELMI HATÁROZAT',
          alignment: 'center',
          fontSize: 14,
          bold: true,
          margin: [0, 15, 0, 10],
        },
        {
          text:
            'A büntetés-végrehajtási intézetben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet rendelkezéseire figyelemmel',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*'],
            body: [
              [
                {
                  text: 'Név',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottNev,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Azonosító',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvAzon,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Születési hely',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottSzulHelye,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Születési idő',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottSzulIdeje,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Anyja neve',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvatartottAnyjaNeve,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Elhelyezése',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvElhelyezese,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Bv.i-ben tart. jogcím/fok',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.BvFok,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Szabadulás aktuális dátum',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvSzabadul,
                  border: [false, false, false, false],
                },
              ],
            ],
          },
        },
        {
          text:
            'fogvatartott ellen kezdeményezett fegyelmi ügyben a fegyelmi felelősség megállapítva.',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*'],
            body: [
              [
                {
                  text: 'Fegyelmi vétség',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FegyVetseg,
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: 'Fegyelmi fenyítés',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FenyTipus,
                  border: [false, false, false, false],
                },
              ],
            ],
          },
        },
        {
          text: 'Indoklás:',
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        // {
        //   text: indoklas,
        //   fontSize: 12,
        //   margin: [20, 0, 0, 10],
        // },
        indoklas,
        // {
        //   text: model.DDLText,
        //   fontSize: 12,
        //   margin: [30, 0, 0, 10],
        // },
        {
          text:
            'Ezen fegyelmi határozat ellen panasszal élhet, mely a fegyelmi eljárás lefolytatását vonja maga után.',
          fontSize: 12,
          margin: [0, 0, 0, 20],
        },
        {
          text:
            'E fegyelmi ügyben keletkezett határozatot a fogvatartott előtt szóban kihirdettem, indokoltam.\n A határozat egy példányát a fogvatartottnak átadtam.',
          fontSize: 12,
          margin: [0, 0, 0, 20],
        },
        {
          text: 'Határozat: ' + model.HatarozatHelyDatum,
          fontSize: 12,
          margin: [0, 0, 0, 10],
        },
        {
          margin: [-5, 0, 0, 15],
          table: {
            widths: ['*', 90, 180],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: 'Határozathozó: ',
                  border: [false, false, false, false],
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 180,
                      y2: 12,
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                  margin: [0, 0, 0, 0],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  margin: [0, 0, 0, 0],
                },
                {
                  text:
                    (model.HatarozathozoNev && model.HatarozathozoNev != ''
                      ? model.HatarozathozoNev + '\n'
                      : '') +
                    (model.HatarozathozoRang && model.HatarozathozoRang != ''
                      ? model.HatarozathozoRang + '\n'
                      : '') +
                    (model.HatarozathozoTitulus &&
                    model.HatarozathozoTitulus != ''
                      ? model.HatarozathozoTitulus + '\n'
                      : '') +
                    (model.HatarozathozoSzam && model.HatarozathozoSzam != ''
                      ? model.HatarozathozoSzam + '\n'
                      : ''),
                  border: [false, false, false, false],
                  alignment: 'center',
                  margin: [0, -5, 0, 0],
                  bold: 'true',
                },
              ],
            ],
          },
        },
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };
    console.log(docDefinition);

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }

  async VedoKirendeleseNyilatkozatNyomtatas({ iktatasIds, naplobejegyzesIds }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model;

    if (naplobejegyzesIds != null) {
      model = await apiService.VedoKirendeleseNyilatkozatNyomtatasByNaploIds({
        naplobejegyzesIds,
      });
    } else if (iktatasIds != null) {
      model = await apiService.VedoKirendeleseNyilatkozatokNyomtatasByIktatasIds(
        {
          iktatasIds,
        }
      );
    } else return null;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          return {
            id:
              (iktatasIds && iktatasIds.length > 1 && index >= 1) ||
              (naplobejegyzesIds && naplobejegyzesIds.length > 1 && index >= 1)
                ? 'NoBreak'
                : '',
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'NYILATKOZAT',
                alignment: 'center',
                fontSize: 20,
                margin: [0, 15, 0, 10],
              },
              {
                text: 'Védő kirendeléséhez',
                alignment: 'center',
                fontSize: 16,
                margin: [0, 5, 0, 100],
              },
              {
                text: 'Alulírott ' + item.Nev + ' nytsz.: ' + item.FogvAzon,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'a 14/2014. (XII.17.) IM. Rendelet 23.§. (1) bekezdése alapján kérem a Bv. Bíró Urat, hogy a ' +
                  item.UgySzam +
                  ' számon indított fegyelmi ügyben részemre védőt kirendelni szíveskedjen.',
                margin: [0, 0, 0, 50],
              },
              {
                text:
                  item.Varos +
                  ', ' +
                  item.Ev +
                  '. ' +
                  item.Honap +
                  '. ' +
                  item.Nap +
                  '. ',
                margin: [0, 0, 0, 30],
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [60, '*', 150],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 150,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: 'Eljárás alá vont aláírása',
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
            ],
          };
        }),
      ],
      pageBreakBefore: function(
        currentNode,
        followingNodesOnPage,
        nodesOnNextPage,
        previousNodesOnPage
      ) {
        if (
          currentNode.id === 'NoBreak' &&
          previousNodesOnPage.length != 1 &&
          currentNode.pageNumbers.length != 1
        ) {
          return true;
        }
        return false;
      },
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };
    console.log(docDefinition);

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }

  // async VedoKirendeleseNyomtatas({ iktatasIds, naplobejegyzesIds }) {
  //   if (pdfMake.vfs == undefined) {
  //     pdfMake.vfs = pdfFonts.pdfMake.vfs;
  //   }

  //   var model;

  //   if (naplobejegyzesIds != null) {
  //     model = await apiService.VedoKirendeleseNyomtatasByNaploIds({
  //       naplobejegyzesIds,
  //     });
  //   } else if (iktatasIds != null) {
  //     model = await apiService.VedoKirendeleseNyomtatasByIktatasIds({
  //       iktatasIds,
  //     });
  //   } else return null;

  //   var docDefinition = {
  //     header: function(currentPage, pageCount, pageSize) {
  //       return [
  //         [
  //           model.map(function(item, index) {
  //             return {
  //               margin: [40, 20, 40, 20],
  //               stack: [
  //                 {
  //                   image:
  //                     'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
  //                   alignment: 'center',
  //                   width: 30,
  //                   height: 55,
  //                   opacity: 0.5,
  //                   margin: [0, 0, 0, 10],
  //                 },
  //                 {
  //                   text: item.IntezetNev,
  //                   alignment: 'center',
  //                   fontSize: 12,
  //                   opacity: 0.5,
  //                   margin: [0, 0, 0, 10],
  //                 },
  //                 {
  //                   text: item.Iktatoszam,
  //                   alignment: 'right',
  //                   fontSize: 10,
  //                 },
  //               ],
  //             };
  //           }),
  //         ],
  //       ];
  //     },

  //     footer: function(currentPage, pageCount) {
  //       return {
  //         margin: [40, 20, 40, 20],
  //         text: pageCount >= 2 ? currentPage + '. oldal' : '',
  //         opacity: 0.5,
  //         margin: [0, 10, 0, 10],
  //         alignment: 'center',
  //         fontSize: 10,
  //       };
  //     },
  //     content: [
  //       model.map(function(item, index) {
  //         return {
  //           id:
  //             (iktatasIds && iktatasIds.length > 1 && index >= 1) ||
  //             (naplobejegyzesIds && naplobejegyzesIds.length > 1 && index >= 1)
  //               ? 'NoBreak'
  //               : '',
  //           stack: [
  //             {
  //               margin: [-5, 0, 0, 20],
  //               table: {
  //                 widths: ['*', '200'],
  //                 body: [
  //                   [
  //                     {
  //                       text: '',
  //                       border: [false, false, false, false],
  //                     },
  //                     {
  //                       text: 'Tárgy:',
  //                       border: [false, false, false, false],
  //                       bold: true,
  //                     },
  //                   ],
  //                   [
  //                     {
  //                       text: '',
  //                       border: [false, false, false, false],
  //                     },
  //                     {
  //                       text: 'Ü.i.:',
  //                       border: [false, false, false, false],
  //                       bold: true,
  //                     },
  //                   ],
  //                   [
  //                     {
  //                       text: '',
  //                       border: [false, false, false, false],
  //                     },
  //                     {
  //                       text: 'Tel.:',
  //                       border: [false, false, false, false],
  //                       bold: true,
  //                     },
  //                   ],
  //                   [
  //                     {
  //                       text: '',
  //                       border: [false, false, false, false],
  //                     },
  //                     {
  //                       text: 'E-mail:',
  //                       border: [false, false, false, false],
  //                       bold: true,
  //                     },
  //                   ],
  //                 ],
  //               },
  //             },
  //             {
  //               text:
  //                 '.........................................................',
  //             },
  //             {
  //               text: 'Bv. bíró',
  //               margin: [0, 0, 0, 20],
  //               bold: true,
  //             },
  //             {
  //               text: item.Torvenyszek,
  //               bold: true,
  //             },
  //             {
  //               text: 'Bv. csoport',
  //               margin: [0, 0, 0, 20],
  //               bold: true,
  //             },
  //             {
  //               text: 'Címzés',
  //               margin: [0, 0, 0, 20],
  //               bold: true,
  //               decoration: 'underline',
  //             },
  //             {
  //               text: 'Tisztelt...........................................!',
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 'Tájékoztatom, hogy ' +
  //                 item.Nev +
  //                 ' (Szül.: ' +
  //                 item.SzulDatum +
  //                 ', an.: ' +
  //                 item.AnyjaNeve +
  //                 ') ' +
  //                 item.FogvAzon +
  //                 ' fogvatartott ellen intézetünkben ' +
  //                 item.UgySzam +
  //                 ' számon fegyelmi eljárás indult, mivel megsértette a fogvatartás rendjére vonatkozó szabályokat.',
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 'A 14/2014. (XII.17.) IM. Rendelet 23.§. (1) bekezdése alapján a fogvatartott védő kirendelése iránti kérelmet terjeszthet elő a bv. bíróhoz. Nevezett úgy nyilatkozott, hogy a fegyelmi eljárása során védő kirendelését kéri, erről írásban is nyilatkozott, melyet mellékelten csatolok.',
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 'Helye: .........................................................................................................................................',
  //               margin: [0, 0, 0, 10],
  //               bold: true,
  //             },
  //             {
  //               text:
  //                 'Kérem a tisztelt Bíró Urat, hogy a védő kirendelését követően intézetünket tájékoztatni szíveskedjen.',
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 '...................................... az elektronikus dátumbélyegző alapján.',
  //               margin: [0, 0, 0, 30],
  //               bold: true,
  //             },
  //             {
  //               text: 'Tisztelettel:',
  //               margin: [0, 0, 100, 20],
  //               bold: true,
  //               alignment: 'right',
  //             },
  //             {
  //               text: item.NyomtatoSzemely,
  //               margin: [0, 0, 0, 0],
  //               bold: true,
  //               alignment: 'right',
  //             },
  //           ],
  //         };
  //       }),
  //     ],
  //     pageBreakBefore: function(
  //       currentNode,
  //       followingNodesOnPage,
  //       nodesOnNextPage,
  //       previousNodesOnPage
  //     ) {
  //       if (
  //         currentNode.id === 'NoBreak' &&
  //         previousNodesOnPage.length != 1 &&
  //         currentNode.pageNumbers.length != 1
  //       ) {
  //         return true;
  //       }
  //       return false;
  //     },
  //     pageSize: 'A4',
  //     pageMargins: [40, 150, 40, 70],
  //     styles: {
  //       header: {
  //         fontSize: 16,
  //         bold: true,
  //         alignment: 'center',
  //         margin: [0, 20, 0, 0],
  //       },
  //       subheader: {
  //         fontSize: 15,
  //         bold: true,
  //       },
  //       quote: {
  //         italics: true,
  //       },
  //       small: {
  //         fontSize: 8,
  //       },
  //       footersmall: {
  //         fontSize: 6,
  //       },
  //       tableExample: {
  //         margin: [-5, 30, 0, 15],
  //       },
  //       tableHeader: {
  //         bold: true,
  //         fontSize: 8,
  //         margin: [0, 10, 5, 10],
  //       },
  //       tableHeader2: {
  //         bold: true,
  //         fontSize: 8,
  //         alignment: 'center',
  //         margin: [0, 10, 0, 10],
  //       },
  //       tableCell: {
  //         fontSize: 8,
  //         alignment: 'right',
  //         margin: [0, 5, 5, 5],
  //       },
  //     },
  //     defaultStyle: {
  //       columnGap: 20,
  //       fontSize: 10.5,
  //     },
  //   };
  //   console.log(docDefinition);
  //
  // pdfMake.fonts = {
  //   TimesNewRoman: {
  //     normal: 'TimesNewRoman.ttf',
  //     bold: 'TimesNewRoman.ttf',
  //     italics: 'TimesNewRoman.ttf',
  //     bolditalics: 'TimesNewRoman.ttf',
  //   },
  // };
  // pdfMake.createPdf(docDefinition).download();

  //   /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
  //   //pdfMake.createPdf(docDefinition).print();
  // }
  async MeghatalmazottVedoKirendeleseNyilatkozatNyomtatas({
    naplobejegyzesIds,
    iktatasIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model;

    if (naplobejegyzesIds != null) {
      model = await apiService.MeghatalmazottVedoKirendeleseNyilatkozatNyomtatasByNaploIds(
        {
          naplobejegyzesIds,
        }
      );
    } else if (iktatasIds != null) {
      model = await apiService.MeghatalmazottVedoKirendeleseNyilatkozatokNyomtatasByIktatasIds(
        {
          iktatasIds,
        }
      );
    } else return null;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          return {
            id:
              (iktatasIds && iktatasIds.length > 1 && index >= 1) ||
              (naplobejegyzesIds && naplobejegyzesIds.length > 1 && index >= 1)
                ? 'NoBreak'
                : '',
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'NYILATKOZAT',
                alignment: 'center',
                fontSize: 20,
                margin: [0, 15, 0, 10],
              },
              {
                text: 'Védő meghatalmazásához',
                alignment: 'center',
                fontSize: 16,
                margin: [0, 5, 0, 100],
              },
              {
                text: 'Alulírott ' + item.Nev + ' nytsz.: ' + item.FogvAzon,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'a 14/2014. (XII.17.) IM. Rendelet 23.§. (1) bekezdése alapján nyilatkozom, hogy a ' +
                  item.UgySzam +
                  ' számú fegyelmi ügy, értesítő lapjának V. pontjában megjelölt ügyvédeim közül:',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Védő neve: ' + item.VedoNeve,
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Védő címe: ' + item.VedoCime,
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Védő elérhetősége (tel.:/FAX): ' + item.VedoElerhetosege,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  item.UrAsszony +
                  ' jelölöm meg vezető védőként, hogy a fegyelmi eljárás során ügyemben képviseljen.',
                margin: [0, 0, 0, 50],
              },
              {
                text:
                  item.Varos +
                  ', ' +
                  item.Ev +
                  '. ' +
                  item.Honap +
                  '. ' +
                  item.Nap +
                  '. ',
                margin: [0, 0, 0, 50],
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [60, '*', 150],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 150,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: 'Eljárás alá vont aláírása',
                        border: [false, false, false, false],
                        margin: [15, 0, 0, 0],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
            ],
          };
        }),
      ],
      pageBreakBefore: function(
        currentNode,
        followingNodesOnPage,
        nodesOnNextPage,
        previousNodesOnPage
      ) {
        if (
          currentNode.id === 'NoBreak' &&
          previousNodesOnPage.length != 1 &&
          currentNode.pageNumbers.length != 1
        ) {
          return true;
        }
        return false;
      },
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };
    console.log(docDefinition);

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  // async MeghatalmazottVedoKirendeleseNyomtatas({
  //   naplobejegyzesIds,
  //   iktatasIds,
  // }) {
  //   if (pdfMake.vfs == undefined) {
  //     pdfMake.vfs = pdfFonts.pdfMake.vfs;
  //   }

  //   var model;

  //   if (naplobejegyzesIds != null) {
  //     model = await apiService.MeghatalmazottVedoKirendeleseNyomtatasByNaploIds(
  //       {
  //         naplobejegyzesIds,
  //       }
  //     );
  //   } else if (iktatasIds != null) {
  //     model = await apiService.MeghatalmazottVedoKirendeleseNyomtatasByIktatasIds(
  //       {
  //         iktatasIds,
  //       }
  //     );
  //   } else return null;

  //   var docDefinition = {
  //     header: function(currentPage, pageCount, pageSize) {
  //       return [
  //         [
  //           model.map(function(item, index) {
  //             return {
  //               margin: [40, 20, 40, 20],
  //               stack: [
  //                 {
  //                   image:
  //                     'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
  //                   alignment: 'center',
  //                   width: 30,
  //                   height: 55,
  //                   opacity: 0.5,
  //                   margin: [0, 0, 0, 10],
  //                 },
  //                 {
  //                   text: item.IntezetNev,
  //                   alignment: 'center',
  //                   fontSize: 12,
  //                   opacity: 0.5,
  //                   margin: [0, 0, 0, 10],
  //                 },
  //                 {
  //                   text: item.Iktatoszam,
  //                   alignment: 'right',
  //                   fontSize: 10,
  //                 },
  //               ],
  //             };
  //           }),
  //         ],
  //       ];
  //     },

  //     footer: function(currentPage, pageCount) {
  //       return {
  //         margin: [40, 20, 40, 20],
  //         text: pageCount >= 2 ? currentPage + '. oldal' : '',
  //         opacity: 0.5,
  //         margin: [0, 10, 0, 10],
  //         alignment: 'center',
  //         fontSize: 10,
  //       };
  //     },
  //     content: [
  //       model.map(function(item, index) {
  //         return {
  //           id:
  //             (iktatasIds && iktatasIds.length > 1 && index >= 1) ||
  //             (naplobejegyzesIds && naplobejegyzesIds.length > 1 && index >= 1)
  //               ? 'NoBreak'
  //               : '',
  //           stack: [
  //             {
  //               margin: [-5, 0, 0, 20],
  //               table: {
  //                 widths: ['*', '200'],
  //                 body: [
  //                   [
  //                     {
  //                       text: '',
  //                       border: [false, false, false, false],
  //                     },
  //                     {
  //                       text: 'Tárgy:',
  //                       border: [false, false, false, false],
  //                       bold: true,
  //                     },
  //                   ],
  //                   [
  //                     {
  //                       text: '',
  //                       border: [false, false, false, false],
  //                     },
  //                     {
  //                       text: 'Ü.i.:',
  //                       border: [false, false, false, false],
  //                       bold: true,
  //                     },
  //                   ],
  //                   [
  //                     {
  //                       text: '',
  //                       border: [false, false, false, false],
  //                     },
  //                     {
  //                       text: 'Tel.:',
  //                       border: [false, false, false, false],
  //                       bold: true,
  //                     },
  //                   ],
  //                   [
  //                     {
  //                       text: '',
  //                       border: [false, false, false, false],
  //                     },
  //                     {
  //                       text: 'E-mail:',
  //                       border: [false, false, false, false],
  //                       bold: true,
  //                     },
  //                   ],
  //                 ],
  //               },
  //             },
  //             {
  //               text: item.VedoNeve,
  //               bold: true,
  //             },
  //             {
  //               text: 'ügyvéd',
  //               margin: [0, 0, 0, 20],
  //               bold: true,
  //             },
  //             {
  //               text: item.VedoCime,
  //               margin: [0, 0, 0, 20],
  //               bold: true,
  //               decoration: 'underline',
  //             },
  //             {
  //               text: 'Tisztelt ' + item.UrAsszony + '!',
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 'Tájékoztatom, hogy ' +
  //                 item.Nev +
  //                 ' (szül.: ' +
  //                 item.SzulDatum +
  //                 ', an.: ' +
  //                 item.AnyjaNeve +
  //                 ') ' +
  //                 item.FogvAzon +
  //                 ' fogvatartott ellen intézetünkben ' +
  //                 item.UgySzam +
  //                 ' számon fegyelmi eljárás indult, mivel megsértette a fogvatartás rendjére vonatkozó szabályokat.\nA fenti számú ügyben a fegyelmi tárgyalás várható időpontja: ' +
  //                 item.ElsofokuTargyalasIdopont +
  //                 '\nHelye: ' +
  //                 item.IntezetCime,
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 'A 14/2014. (XII.17.) IM. Rendelet 23.§. (2) bekezdése szerint a fogvatartott által megbízott jogi képviselő a fegyelmi vizsgálat során végzett eljárási cselekményeknél, illetve a fegyelmi tárgyaláson jelen lehet.',
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 'Nevezett védőjeként Önt jelölte meg, erről írásban is nyilatkozott, melyet mellékelten csatolok.',
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 'Felhívom figyelmét, hogy védő távolmaradása nem akadálya a fegyelmi eljárás lefolytatásának.',
  //               margin: [0, 0, 0, 10],
  //             },
  //             {
  //               text:
  //                 '...................................... az elektronikus dátumbélyegző alapján.',
  //               margin: [0, 0, 0, 30],
  //               bold: true,
  //             },
  //             {
  //               text: 'Tisztelettel:',
  //               margin: [0, 0, 100, 20],
  //               bold: true,
  //               alignment: 'right',
  //             },
  //             {
  //               text: item.NyomtatoSzemely,
  //               margin: [0, 0, 0, 0],
  //               bold: true,
  //               alignment: 'right',
  //             },
  //           ],
  //         };
  //       }),
  //     ],
  //     pageBreakBefore: function(
  //       currentNode,
  //       followingNodesOnPage,
  //       nodesOnNextPage,
  //       previousNodesOnPage
  //     ) {
  //       if (
  //         currentNode.id === 'NoBreak' &&
  //         previousNodesOnPage.length != 1 &&
  //         currentNode.pageNumbers.length != 1
  //       ) {
  //         return true;
  //       }
  //       return false;
  //     },
  //     pageSize: 'A4',
  //     pageMargins: [40, 150, 40, 70],
  //     styles: {
  //       header: {
  //         fontSize: 16,
  //         bold: true,
  //         alignment: 'center',
  //         margin: [0, 20, 0, 0],
  //       },
  //       subheader: {
  //         fontSize: 15,
  //         bold: true,
  //       },
  //       quote: {
  //         italics: true,
  //       },
  //       small: {
  //         fontSize: 8,
  //       },
  //       footersmall: {
  //         fontSize: 6,
  //       },
  //       tableExample: {
  //         margin: [-5, 30, 0, 15],
  //       },
  //       tableHeader: {
  //         bold: true,
  //         fontSize: 8,
  //         margin: [0, 10, 5, 10],
  //       },
  //       tableHeader2: {
  //         bold: true,
  //         fontSize: 8,
  //         alignment: 'center',
  //         margin: [0, 10, 0, 10],
  //       },
  //       tableCell: {
  //         fontSize: 8,
  //         alignment: 'right',
  //         margin: [0, 5, 5, 5],
  //       },
  //     },
  //     defaultStyle: {
  //       columnGap: 20,
  //       fontSize: 10.5,
  //     },
  //   };
  //   console.log(docDefinition);
  //
  // pdfMake.fonts = {
  //   TimesNewRoman: {
  //     normal: 'TimesNewRoman.ttf',
  //     bold: 'TimesNewRoman.ttf',
  //     italics: 'TimesNewRoman.ttf',
  //     bolditalics: 'TimesNewRoman.ttf',
  //   },
  // };
  // pdfMake.createPdf(docDefinition).download();

  //   /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
  //   //pdfMake.createPdf(docDefinition).print();
  // }
  async KioktatasReintegraciosJogkorbenNyomtatas({
    fegyelmiUgyId,
    iktatasId,
    naploId,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model = await apiService.KioktatasReintegraciosJogkorbenNyomtatas({
      fegyelmiUgyId: fegyelmiUgyId,
      iktatasId: iktatasId,
      naploId: naploId,
      Telefonszam: '12345',
      Fax: '678910',
      Iranyitoszam: '0000',
    });

    var foglalkozas = htmlToPdfMake(
      `
    <div style="margin-left: 20px; text-align: justify;">` +
        model.FoglalkozasText +
        `</div>
`
    );

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        {
          image:
            'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQICAgICAgICAgICAwMDAwMDAwMDAwEBAQEBAQECAQECAgIBAgIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD/8AAEQgAWAAwAwERAAIRAQMRAf/EAJQAAAICAwEAAAAAAAAAAAAAAAgJBgoABQcDAQACAwEAAAAAAAAAAAAAAAACAwABBAUQAAAHAQAABgIABgMBAAAAAAECAwQFBgcIABESExQJFRchIkMWGApCIyQyEQACAQIEAwQIBAUFAAAAAAABAhEhAwAxEgRBUWFxIjIT8IGRobFCIwXB0ZIU4fFSYjNyotJDNP/aAAwDAQACEQMRAD8Av8eJiYzxMTFZH7WfsC3XkXqydn8C19/bYumc/wB8T1rIZagalLZ7iK0dnp7LB6VLyNJqdwrj52lJXuuzL15JskyRzJJJBZ4Rm5WSJ0dtZsXduTuAVQOIcGskgRGZoGgSKmeuAYwYGcenpyw8fix1fpHm3OpjR9tpfQ8/PNZSdZ6xn0jGztUsVZlph89qqcdZoiErDC1mjIBZBsrJEi475aqZjC3THz8+exDMSBHMTMHiOlZpwywdeOCn8ViYzxMTGeJiYUt1v9l0nUdXJxpwplf+XvdEs1WcytOZyZ4bEucq6mqVo70XpvU0iKRtSiYx0qBEYFmdaelHIA2TSROomYxIqt3n1CzMFlANYNBNJFJBIzBwJPBY1YTl3Fg+2c8fC3Dv7s2/3E92xqZDYlub4NDOE84qxOjOYKRoCVBsaaDacukZO5/rs3DN4ywNCQEe1dicGaavrdeMu53Vy3eSxtEUJcuWwdUEkFtJJMgCBUkVGnxQIK2BAUtHeevsJz7QI9mWNLzVmR7nvjrBOKes+vsIGo6a2r8s60zTaDsbaBd2bCNH3S05+3Qxi8SGOzFJvsk3g5ly7iX/AOajX4GRKu3FM7bx0RcPkL5qhrbA6TQNQgTME0FADSCTGDAkQJU/D1e/DQ8x746L4x1CA5o+0aoIs6xaBdNcP7loQLWDJr2hGoGcr1zYTNYuKe0a6xbIhlFHi0c0SVZpGcuEgQQcyRwZRdP0VINKZnqeYHGtI4yMWshRrNcPCYP2MoxZycY8aSMbItG7+PkGDhF4xfsXiJHDR4zdtzqN3TR03UKdNQhjEOQwGKIgID4TlQ54LAE/Z122++vTkG79RMKBHaUrUbHQK6atSlp/tJp5X24RNOSkxfEjJd1ILRrqYTUTj26IOH5/JFM5DHAwGi62g0EGvYJ7a5U54mK8X1r6hpXMmOyv2XW+2Pn3PFsscpkXbdDctbAjrEx0/pWnsn9v2uAiXiRCxgY3tt+TyyBpCqR3ZYsjyQ+UL5woi5bubxa41guGFoxAA0rAkAaZB1qyszTyBkqcLVqTpPXn/HswZvCeAM9Zv+Kcpdtwkxomp8I4hotvv9A02ySWh162WTpTYoW75vZLd+cMuw1Cq1+k1RoWLSlE3TNnZ2b8CJ/IikjppKPqO4t6ls91Qad4wdRjMGa8M4ypi1LairVETPby4iBnPPhXBGdq4JzpybqfP++5BnpsitN8ltfwdek8+x8fnzbXL9qHPukQGPkRqldYtq+51xhcUkY6BmitSvWjeTc++qdqmAJEu4ZIsFPMU94A/LBBYjjBEyMjnngWYLcCjxMCfZA/HC5rHD9A6xylD89W+2yrGu/UXRm9h+wiJmL1bT3PctBjIy6zZIXMLa4EZGCi81zGBbaVTLBMHfg8UlIFoZEot3joloj3botoxRLhKgwJgmADpNJ8DxBA1EZjFtXxLJFfXEZcaE8cGj/r29z3jqTna4YJd6iCD7ihnn2St9RWeDFSOrwswW5O6LYnuaOomNkKCm9zKDhJBsHqUaP20iRVsRFECphd615UAZyQRAADDTIEEyK5zSIrEklNMqRjo339Z+w07jvOaZNXCKo8BLdK58pLWC1Q87ZaK3WhKlodnrLS21itgWYnTTVzgY2OhyJKJi3sD1i4AFDokSPVpEuEozBSVMTEkyPDObASYAYkAgCsiGZEc8V7Z3jm+s3+i5q81vSKtSRulK0a8V2159qshE4vc57Rue5sOgOgI+LXa1+4LT8/oEjYIl/F/EftpGrg6duCgi4VR51uwLtk7kaV3CwkMV1MmkkxC91EgI0jVWhymld3E6YOdZ8XaBn16Y6Qny7pMUwntiL1XrK12f1uz1JfoRqXQarS53MaR0hQM3gNlsb5/Jv9RPz1TWupTEs/agq4STeMXLlu8RZKquiaBs9nctpaYk7fUpYAq0NpIKrC1JKgKTJGoahIIxRu3LbKIAdgxOfCPbVuQy642dh4+0CXmSMnnZ927Tk8nvkLP5hrmEykxOU1zfpTDehrq2z2mqyt1vrSt9BQ/wCu2KXyYSYk/KFsiYKJNXCgJqMtfbdqLNxrf0ka3JWQREqJJKglIYiKE5kngZd9QZ4McePX4DHDbPzFoqFO1Ofb9aSejs9Pissba/p0Hne9Fzm3zFkxboN+4wbWoh5LvrJdr+3UyaAq0Yd04+O2b3Jq3dNSqC3br5bO22xv2VuMoUXNIMqAqg0aSImJMGkjxGcFLhSSAK9azxyzw2b6Q8AdYt2r1yopabN897lcFC3DOpOBnK1N1Z7UtTsVBz9ztJpUpGN0u7rPqGgrVX7BNFpHV106akAwHAS6bN5n2ih7RlrjHzCUPggaQFggMSWqDMCs5ptksNTDSQMu2pPtp6jjoH3B0uY3HXDUCz6jqUHnuZxeZ3es0WnTFWj6ktdXWH95XYbRY4edp9kTscqzmMjhRbfJMKDZJBT20yqnBUh27du9Zui5WGiOEHyxEcR3jiI7G444Kwj9M/HCzIfiKgO7oygVNG2csaGsS1OBJJ7kRFyQ6HS36nT9tf8ATQmSff2kYSGVKAet2b3RD/j4w7nY7K3dCrbt6SiE0GZVCficaldzBk8fx/LEEovFWdXGvQ9nnbnpzuZkaTmrp299jFxVUTvOPfXBeLDH+4ti6ywRLue6jtBithOKQIGYJmA3xCmUttrtUFpVtrpfUTSkqzgEDKYX44W9x1dQPmMf7Z+ONDGcKY9D59JOYacucMSr5LfdIgYyJgMEiYaIt8Dz303pbN+wiIzEG7NmU1my6LBUUipqqtRcpGP/AOgwl02tpZvbV9xfWb+hpNagXiigzUgKBQ0mowPmt5vljwxyHp+PPE+muELOwmv7mN+zUcNHqTaudGWjK6nhzyyvFM7tm8Fi1T0AnPib9kkvDUOGjlVhfmUUeNXL0UipOCELhOx2nlT5duShNFHi0kiZERIBOdMq4ZruawpNJ9v5YInhjKVOf+0ec7TnWqaui50Jg0pN9ipCRoRK9bqq9iuZLMav2CKr+d1757SPntTlXTI5lCrsjqEBBQpBXI41C1bsbO1dtIqPcvKDHEFLhMdpQdYHCpxnt3nuhGb5gZ/ST8RhnXTKfACv2H6wTu8cTCMDlvng2cfudZFFn801z6XJaRghdnIzM4/CCIO/PzUBmKn9EVvGj7efuA3t39j5kQs6edNPrzj+WED/ANL6v8fumFj3TjxK3/19RceZFeJxd/IHyEj+N+X8z8oUDeXpce/8384QPP8AqfNAP6oB46mr7wFzuaSY6TGXbHDOOmNH0+mPJu3/ANe8qZPiK8QFQ9tuCXxH8SCIoka1wzIEvYce2KJWCcMKHl/L7BY/0fyA08F5n3uAdV7TEipiK1HTOo69cSLfTCoK2tkJMvyyHuO9oczYhcc+rVIk9gZQMbY2lfzy5czdfwqrZg1tEPNRrBrJ1t0YiT9w3ODJMAVEwCX1eM2zS7etOCuu5DSKVjcsCKHnnHXI5Lot7l3R+Ppyw2mzVfkBfgKv2ST77/F5ot1RfNWa9UhXqUQJbZrldNNTuNPCmrVdSvIgk+tEzH/CLHlWafF9wRAyRjeMgsze8nQZFNMwfDGfrnDw3zE1n07cAPzY5qDjqzmQmf68beqLG6tcYKoa2aEja8NwrlffciQcQ6GMiI6IjQOyYsCNRcIt0SPPZ98AN7nma99aazt7NtlKEbhKTP8A1XjzMdnDphShVdFXwjVH6GwU/wBiv7GsXXOgUfL8E2zbZwmO43bJYcpr9OlI2vRMnlv2AZLFlnX9tvVNKg+kbZpDAEkUSuB+GRysYSgj6TrsX7a+bYM+YWBHZ9P/AIn3YG2R59xfmJB9QUD8RgZoir9QMrm2nl+DeyvgJavJXM4kpmNncfhXXS37XRMCP7zDzejU/wCYUvP+Dv8A6vP/AJeB3IF26GTwhVHrCqD8DjUpgV6/jiI57nnV1ZqsLCynBnYYPGFUy6GXBtUcaXRB7UMf+uKjTAJq/vEnrRLO8s2oET+Qe4gmxU8g+WBUxdQ3lQPAGn1s5EfqGFuCzIRkDJ/THxx64dk2xdJRM7X84u+C5JHq801FOYZ9AntLS7pq77mXVePnSQhqnNtI1spVY+2HcrkOu596Qbi39z2xFUdNp0tfbrdxqi6bq+LSRovsZEhuzp7hBb+qW4wOHMT05/xw1ixcd67K8lQtcS2LnlDTEut9R6beWtwS3Diqn7PsetOl6fHPiThbMZWJZ6UCSbpRcfecNDAKZSn8i4zGhajSKZ9CKHn6o6HDCCXniDOWAEpOSani3cvJFR0m/wCCX8rxNpKw73FXVrVcMfxE7zbnblKyJWSSkU003iVDRcNjImKPuLLpmAQTIJiukfs7KLEDcqM5P+O7HAcM+ZqIwhbYtNbStJFf9DYLXrTo7buWu49X0DKMzzDSo20894PT5trfdBs1EfxUjWYztDYWq0X+EpFvaybGRgc7kWyhlDIKIu1G/kUyZjmKj9vuXuvf2+gAEAlqiO5kAQZlhnQ+o4QFY33dCAwMVE5hTzHLEWYfab2k/sKNdJy/zaRZa4OqUDoehNBMiWUaa4GPqL+2GKAoLI0x5vAN/wDXxQ9Ih7n8PEvLv7LaddgsVU+F/mAOerqBhwG5ORX2dv8Ad0xpav8AbV2RaoljMs+WudGrSQiafNIlddB38Vis7nRuVb6wKoRLFjlBw1jetIVBUoCIe/GPfIRKKImojfrpl7HfmO6/AsDPe/t9/Q4FjfUgSskwKdJ/q5YT7o8fiqecRD/fMip1wtsnzJcoFGxEycmqxVSmD8x9bIxzD+5F6u+m4WEQ0ebYKMXCqKRUnQFeKAiKKiqe/b6b20Fll1XWD6Qc5/cMSRJoIAr2cwMGGK34kadM0y/ngxLpesbPytB4fIYBOMue0/s06XcV3QJDKax/iPJwraa6Ks0Cxg3iTleJQrJlZFqhEv1YlCBdTSIsmjpR4mVMU27Ra35YWCtuiwZJNvuwBlmKmAMzInDGuKG1/KDnn6dmOXcOR2LI9V81vsgymOzkhLVEws3INcmRyxeyKxkDx1/EiKkLCS0vHMZZZ8omdykUPddKLEAQces97i2E2tkMqi8NwgMRP+O8anjXiJB5zkm3rBtq86q+3Q2HBda8kWPqHt2bpUV0PaMMZv8Amii3pFvX83zu7KWl9ASnROMWFYr+8xr9RgnXqnu5iGRblEouJRBY4gZJMDNtFrSsRBRzUGeS9n9I44MKockGrQT6hFMR1t9Peps5hOcbfYHoxXyVqXuJDKc8c/qJhNOtEDUlze3+AIQWw2/zUKn5eRW4+z/EoeYg/wBQd+rwBPGAAAIypA4TTDQSMssauA+mLQ63GM4eM+wDSyMWEbXIhuVbn7AFlvx9TqXP1KhE1Vj17/sXSg+Y6kVVT0h7qiDs4gAuz+myVYKCBKAwa8SSePHUfdgSAxBOYMj2R8MVrNG0XRNTqON5yfE9LtUFCaxSIW82qB2mPy6L1ehZvVuiMh0h1KWDG5s+g0aD0e030zFlDwTeUk15dgpALsSPXLFi+fYazb2wtNcNu9cLBCqwA3nG6oliNNIGo1jvjUCcKZS9wgTkM+lDE+7lgoC7FXpnHIzDHvG0G2oMFr1m6FbJm+wHpFUyeh3bOZhedsRrNG5u5aS2bvG4uHiLZu2WhU4RY9rjW6pWE9+Iwnztrd/b3XuWrhJANJMQCJKiGAyWjalikqWrTKFNRkESe7lwOUR1joTiU/WJH61/ktzfTZ+k/go+I6S2JRo3t22y2p6Qem2dpHa3CfhZaXj5qauFaxSv4Wxrcy5npdtOgnOwz1NspGumzhfduPJezbsAtpTS4MRqYBxzqDrYmBQ+uDC6SC0yD8QRw7ezFgf7ZuWbxt2Owmv4yz02Y2rndpoU1A0jJNOt2S3DVqbcqW/grPQWVlpU7XJR9Kxc2hD2mGj1nXxH03XGzY5P/R6i1YYBtDsBbaJmqyMi3TgTwBJrlgyDmJnFfKsX/nCatUcYexdESgHOrS0GdpM977rXXbatN+ohpRWEzETe1R0zCOmVDTFqqm8SQdIIgY6gFOAnCbtLtq75SINICAkCe8VUkSOJknFppIBPXOnPh7MRjO9AwuSqcE6sPZ18PKuqrlT10Z79hOwNXKktM459as7ZvUiO5I+lVa26loZlk/SAJLupFLyL8ICNxc3VWzcVBoYMSdOcNc0+4DtgYB5DqFyJr2aZ+OOP44lW3OZtIyFWZPoL87pjeGfmmI2wxk3Et7nNxSSi87PpRbCQfmrCsc2cO5s/tSUYs1Zz71zCua9bYRjKLtg2t2FbbsYaAQUbUY1QTEmSrrk+qAGD2yAa3cdrQMXkryMcCOfKcuBx0Zs0Vbyftv13CRwUWdoSny7A1kouRbWf3XL0qi5465pPy3tgUz1BQWdkQsrUFvNhoLf3r2kIQr7L7iGvWyv07mcgDwmMrirVWBKlRIm2ZS0gNqalxTPStJHQ8VPHLhhsX0v4ZarxrNq69esxQxSDosxmOMvlGzJtDaVcZmdj1rdplEZw6TCBNSKPEQ7mvwkpHoGhJQZ2XXg0oiMcfhGTLq3LSiy76jOogioPymZJDEE6wanu6paSTiW1EyOH8PhPLFkzwjBYALWPr5yi06FZdsyNtUMb2e7O2j/RJpXKaFo+f668YolbtnmsZxaY4iM5NEakBH8zFP4SfMkAEO+UTKUgSWgLJ0g0E0ExMdsDnBrE5iVrMmcQM1auOaiVPXfrwxDVYdqUTu9A5bgs5mHRiHcGIRZfG9Tj6hbWZiFXFRVGMl7CqACf0esQ8jAGud43BlJBEmQKgZTqOXKRnXElsopivLhPB3cduiIWAo/KtsqcWeZnXzqd2yYb4NVq5FSlxtU1ENGdfnT3XSmMvWmMsugVCOg3UQgs6Iux9MZIzsG502boF1muFsiCI1aqCawqkNIma901Loj4y3duLtxbiSlxTmPTiMxkRQ1FG34R9J0E4eRtl7Y0qN28GpWbgcIziCkKLgBpBnDEgUjWz8jIyN91Bo1iwFoi1fu2EWeLTaRztk7ZxcWmzjXjoNpR9KCK1MTPKhHCMswZJnVpkkt6en5YevGxsdDRzCIiGDKKiYpk1jYuLjWqDGOjY5igRsyYMGTVNJszZM2yRU0kkylImQoFKAAAB4Tgsf/Z',
          alignment: 'center',
          width: 30,
          height: 55,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.IntezetNev,
          alignment: 'center',
          fontSize: 12,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.Iktatoszam,
          alignment: 'right',
          fontSize: 10,
        },
        {
          text: 'Kioktatás reintegrációs tiszti jogkörben',
          alignment: 'center',
          fontSize: 14,
          margin: [0, 30, 0, 30],
        },
        {
          text: 'A foglalkozáson részt vett fogvatartott adatai:',
          margin: [0, 0, 0, 10],
        },
        {
          margin: [-5, 0, 0, 10],
          table: {
            widths: [150, '*'],
            body: [
              [
                {
                  text: 'Név',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.Nev,
                  border: [false, false, false, false],
                  alignment: 'left',
                },
              ],
              [
                {
                  text: 'Azonosító',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.FogvAzon,
                  border: [false, false, false, false],
                  alignment: 'left',
                },
              ],
              [
                {
                  text: 'Születési idő',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.SzulDatum,
                  border: [false, false, false, false],
                  alignment: 'left',
                },
              ],
              [
                {
                  text: 'Anyja neve',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.AnyjaNeve,
                  border: [false, false, false, false],
                  alignment: 'left',
                },
              ],
              [
                {
                  text: 'Elhelyezése',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.Elhelyezes,
                  border: [false, false, false, false],
                  alignment: 'left',
                },
              ],
              [
                {
                  text: 'Végrehajtási fokozata',
                  border: [false, false, false, false],
                },
                {
                  text: ': ' + model.VegrehajtasFoka,
                  border: [false, false, false, false],
                  alignment: 'left',
                },
              ],
            ],
          },
        },
        {
          margin: [-5, 0, 0, 30],
          table: {
            widths: [150, '*'],
            body: [
              [
                {
                  text: 'A foglalkozás témája',
                  border: [false, false, false, false],
                },
                {
                  text: ': Kioktatás (fegyelmi)',
                  border: [false, false, false, false],
                  alignment: 'left',
                },
              ],
            ],
          },
        },
        {
          text: 'Foglalkozás leírása:',
          margin: [0, 0, 0, 10],
        },
        foglalkozas,
        {
          text:
            model.Hely +
            ', ' +
            model.Ev +
            '. ' +
            model.Honap +
            '. ' +
            model.Nap +
            '.',
          margin: [0, 0, 0, 40],
        },
        {
          margin: [0, 0, 0, 40],
          table: {
            widths: ['*', '*', '150'],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: '',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 150,
                      y2: 12,
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: '',
                },
                {
                  text: model.FoglalkozastTartotta,
                  border: [false, false, false, false],
                  alignment: 'center',
                  uppercase: 'true',
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: '',
                },
                {
                  text: 'Foglalkozást tartotta',
                  border: [false, false, false, false],
                  alignment: 'center',
                },
              ],
            ],
          },
        },
        {
          margin: [0, 0, 0, 40],
          table: {
            widths: ['*', '*', '150'],
            body: [
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: '',
                },
                {
                  canvas: [
                    {
                      type: 'line',
                      x1: 0,
                      y1: 12,
                      x2: 150,
                      y2: 12,
                      lineWidth: 1,
                    },
                  ],
                  border: [false, false, false, false],
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: '',
                },
                {
                  text: model.Nev,
                  border: [false, false, false, false],
                  alignment: 'center',
                  uppercase: 'true',
                },
              ],
              [
                {
                  text: '',
                  border: [false, false, false, false],
                },
                {
                  text: '',
                  border: [false, false, false, false],
                  alignment: '',
                },
                {
                  text: 'Résztvevő',
                  border: [false, false, false, false],
                  alignment: 'center',
                },
              ],
            ],
          },
        },
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };
    console.log(docDefinition);

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async VedoTelefonosTajekoztatasaNyomtatas({ naplobejegyzesIds, iktatasIds }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var model;

    if (naplobejegyzesIds != null) {
      model = await apiService.VedoTelefonosTajekoztatasaNyomtatasByNaploIds({
        naplobejegyzesIds,
      });
    } else if (iktatasIds != null) {
      model = await apiService.VedoTelefonosTajekoztatasaNyomtatasByIktatasIds({
        iktatasIds,
      });
    } else return null;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          var tajekoztatoTartalma = htmlToPdfMake(
            '<div style="margin-left: 20px;">' +
              item.TajekoztatoTartalma +
              '</div>'
          );
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'Feljegyzés',
                alignment: 'center',
                margin: [0, 30, 0, 0],
                bold: 'true',
              },
              {
                text:
                  'védő meghatalmazása, kirendelése esetén történő telefonos tájékoztatásról,',
                margin: [0, 0, 0, 0],
                alignment: 'center',
              },
              {
                text: item.UgySzam + ' számú fegyelmi ügyben.',
                margin: [0, 0, 0, 10],
                alignment: 'center',
                bold: 'true',
              },
              {
                text:
                  'Készült a ' + item.IntezetNev + ' hivatalos helyiségében.',
                margin: [0, 0, 0, 30],
                alignment: 'center',
              },
              {
                text:
                  'Tájékoztatást nyújtó neve: ' + item.TajekoztatoSzemelyNev,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Tájékoztatott neve, telefonszáma: ' +
                  item.TajekoztatottSzemelyNev +
                  ', ' +
                  item.TajekoztatottSzemelyTel,
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Tájékoztatás időpontja: ' + item.TajekoztatoIdopontja,
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Tájékoztatás tartalma: ',
                margin: [0, 0, 0, 10],
              },
              tajekoztatoTartalma,
              {
                text: item.SikertelenText,
                margin: [0, 0, 10, 50],
                alignment: 'justify',
              },
              {
                margin: [0, 0, 0, 40],
                table: {
                  widths: ['*', '*', '200'],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                            dash: { length: 3, space: 1 },
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        text: item.NyomtatoSzemely,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };
    console.log(docDefinition);

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async FegyelmiTargyalasiJegyzokonyvMasodfokNyomtatas({
    naplobejegyzesIds,
    iktatasIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }
    var model;

    if (naplobejegyzesIds != null) {
      model = await apiService.MasodfokuTargyalasiJegyzokonyvNyomtatatasByNaploIds(
        {
          naplobejegyzesIds,
        }
      );
    } else if (iktatasIds != null) {
      model = await apiService.MasodfokuTargyalasiJegyzokonyvNyomtatatasByIktatasIds(
        {
          iktatasIds,
        }
      );
    } else return null;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          var jegyzokonyvText = htmlToPdfMake(
            '<div style="margin-left: 20px; text-align: justify;">' +
              item.JegyzokonyvText +
              '</div>'
          );
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'II. FOKÚ FEGYELMI TÁRGYALÁSI JEGYZŐKÖNYV',
                alignment: 'center',
                bold: true,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.UgySzam + ' számú fegyelmi ügyben',
                alignment: 'center',
                bold: true,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Készült a ' +
                  item.IntezetNev +
                  ' hivatalos helyiségében.\n ' +
                  item.Ev +
                  ' év ' +
                  item.Honap +
                  '. hónap ' +
                  item.Nap +
                  '. napon ' +
                  item.Ora +
                  ':' +
                  item.Perc +
                  ' órakor.',
                alignment: 'center',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Jelen vannak:',
                alignment: 'center',
                bold: true,
                margin: [0, 0, 0, 10],
              },
              {
                ul: [
                  'Intézet parancsnok: ' + item.IntezetParancsnok,
                  'Jegyzőkönyvvezető: ' + item.JegyzokonyvVezeto,
                  'Egyéb jelenlévő: ' + item.Egyeb,
                ],
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Az eljárás alá vont adatai:',
                decoration: 'underline',
              },
              {
                text: 'Név: ' + item.Fogvatartott + ', ' + item.FogvAzon,
              },
              {
                text:
                  'Születési helye, ideje: ' +
                  item.FogvatartottSzulHelye +
                  ', ' +
                  item.FogvatartottSzulIdeje,
              },
              {
                text: 'Anyja neve: ' + item.FogvatartottAnyja,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'A fegyelmi jogkör gyakorlója megállapítja, hogy a fegyelmi tárgyalás megtartásának nincs akadálya. A fegyelmi jogkör gyakorlója a tárgyalást megkezdi és ismerteti a fegyelmi eljárás alapjául szolgáló cselekményt.',
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'A jegyzőkönyv felvétele előtt felhívja a fogvatartott figyelmét a hamis vád törvényes következményeire, részletesen ismerteti a Büntető Törvénykönyvről szóló 2012. évi C. törvény alábbi vonatkozó részeit:',
              },
              {
                text: '268. § (1) Aki',
              },
              {
                text:
                  'a) mást hatóság előtt bűncselekmény elkövetésével hamisan vádol,',
                margin: [10, 0, 0, 0],
              },
              {
                text:
                  'b) más ellen bűncselekményre vonatkozó koholt bizonyítékot hoz a hatóság tudomására, bűntett miatt három évig terjedő szabadságvesztéssel büntetendő.',
                margin: [10, 0, 0, 0],
              },
              {
                text:
                  'Tájékoztatom, hogy az olyan kérdésekben, amelyben saját magát vagy közvetlen hozzátartozóját bűncselekmény, szabálysértés vagy fegyelemsértés elkövetésével vádolná a vallomástételt megtagadhatja, de a védekezés ezen módjáról ezzel lemond.',
              },
              {
                text:
                  'Tájékoztatom továbbá, hogy a fegyelmi eljárás során felmerülő költségek terhét viselnie kell, amennyiben a fegyelmi jogkör gyakorlója a fegyelmi cselekmény elkövetésében a felelősségét megállapítja.',
              },
              {
                text:
                  'Felhívja figyelmét arra, hogy a vallomástétel megtagadása nem akadálya az eljárás lefolytatásának és a fegyelmi felelősség megállapításának.',
              },
              {
                text:
                  'Figyelmeztetem, hogy a jegyzőkönyvbe foglaltak felhasználhatók Ön ellen.',
              },
              {
                text:
                  'A büntetés-végrehajtási intézetekben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet 15. §. (1) bekezdése alapján tájékoztatom a fogvatartottat a fegyelmi eljárás során érvényesíthető jogaira, azaz:',
              },
              {
                ul: [
                  'védőt bízhat meg, illetve kérheti kirendelését,',
                  'a 14/2014. (XII.17.) IM rendelet 36. §.-ban foglaltak fennállása esetén közvetítői eljárás lefolytatását kezdeményezheti,',
                  'anyanyelvét, vagy az Ön által ismert nyelvet használhatja,',
                  'védekezését szóban vagy írásban adhatja elő,',
                  'bizonyítási indítványt tehet,',
                  'a vizsgálat befejezése után az eljárási iratait tanulmányozhatja, azokról másolatot kérhet,',
                  'panasszal élhet, illetve bírósági felülvizsgálati kérelmet terjeszthet elő a fegyelmi határozattal szemben.',
                ],
                margin: [30, 0, 0, 0],
              },
              {
                text:
                  'Megértettem a fegyelmi ügy kivizsgálójának figyelmeztetését a hamis vád törvényes következményeire.',
              },
              {
                text:
                  'Kijelentem, hogy a fegyelmi eljárással kapcsolatos jogaimról és kötelezettségeimről kioktattak.',
                margin: [0, 0, 0, 30],
              },
              {
                pageBreak: 'after',
                margin: [0, 0, 0, 40],
                table: {
                  widths: [200, '*', 200],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Fegyelmi jogkör gyakorlója',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        text: item.Fogvatartott + ', ' + item.FogvAzon,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              {
                text:
                  'A fegyelmi jogkör gyakorlója megállapítja, hogy a fegyelmi tárgyalás megtartásának nincs akadálya. A fegyelmi jogkör gyakorlója a az eljárás alá vont fogvatartottat kioktatja a jogairól és kötelezettségeiről, felhívja a figyelmét a hamis vád törvényi következményeire. A fegyelmi jogkör gyakorlója ismerteti az elkövetett fegyelemsértést a jelenlévőkkel.',
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Nevezett fogvatartott az II. fokú fegyelmi tárgyaláson történő meghallgatása során a következőket mondta el:',
                margin: [0, 0, 0, 10],
              },
              jegyzokonyvText,
              // {
              //   text: item.JegyzokonyvText,
              //   margin: [20, 0, 0, 50],
              // },
              {
                text:
                  'Az üggyel kapcsolatban egyebet elmondani nem tudok és nem is kívánok. A jegyzőkönyv az általam elmondottakat helyesen és jól tartalmazza, melyet elolvasás után helybenhagyólag aláírok.',
                margin: [0, 0, 20, 30],
              },
              {
                text: 'Kelt, mint fent',
                margin: [0, 0, 0, 30],
                alignment: 'center',
              },
              {
                margin: [0, 0, 0, 40],
                table: {
                  widths: [130, 200],
                  body: [
                    [
                      {
                        text: 'Eljárás alá vont aláírása:',
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                        alignment: 'left',
                        margin: [10, 0, 0, 0],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: item.Fogvatartott + ', ' + item.FogvAzon,
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [10, 0, 0, 0],
                      },
                    ],
                  ],
                },
              },
              {
                text:
                  'Tekintettel a fentiekre a másodfokú fegyelmi határozat rendelkező részében leírtak szerint határoztam.',
                margin: [0, 0, 0, 30],
              },
              {
                margin: [0, 0, 0, 0],
                table: {
                  widths: [200, '*', 200],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Jegyzőkönyvvezető',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        text: item.Fogvatartott + ', ' + item.FogvAzon,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 30, 0, 0],
                table: {
                  widths: ['*', 200, '*'],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Fegyelmi jogkör gyakorlója',
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              item.EgyebJelenlevo != null && item.EgyebJelenlevo != ''
                ? {
                    margin: [-5, 0, 0, 40],
                    table: {
                      widths: [120, '*', 180],
                      body: [
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 180,
                                y2: 12,
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                        ],
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: item.Egyeb.split(', ')[0],
                            border: [false, false, false, false],
                            alignment: 'center',
                            margin: [0, -4, 0, 0],
                          },
                        ],
                      ],
                    },
                  }
                : null,
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };
    console.log(docDefinition);

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async SzembesitesiJegyzokonyvNyomtatas({ naplobejegyzesIds, iktatasIds }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model;

    if (naplobejegyzesIds != null) {
      model = await apiService.SzembesitesiJegyzokonyvNyomtatasByNaploIds({
        naplobejegyzesIds,
      });
    } else if (iktatasIds != null) {
      model = await apiService.SzembesitesiJegyzokonyvNyomtatasByIktatasIds({
        iktatasIds,
      });
    } else return null;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index) {
          var jegyzokonyvText = htmlToPdfMake(
            `
          <div style="text-align: justify;">` +
              item.JegyzokonyvText +
              `</div>
              `
          );
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'JEGYZŐKÖNYV',
                alignment: 'center',
                bold: true,
                margin: [0, 0, 0, 0],
              },
              {
                text: 'fogvatartottak szembesítéséről',
                alignment: 'center',
                bold: true,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.UgySzam + ' számú fegyelmi ügyben',
                alignment: 'center',
                bold: true,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Készült a ' +
                  item.IntezetNev +
                  ' hivatalos helyiségében.\n' +
                  item.Ev +
                  '. év ' +
                  item.Honap +
                  '. hónap ' +
                  item.Nap +
                  '. napon ' +
                  item.Ora +
                  ':' +
                  item.Perc +
                  ' órakor.',
                alignment: 'center',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Jelen vannak:',
                alignment: 'center',
                bold: true,
                margin: [0, 0, 0, 20],
              },
              {
                text: 'Meghallgató: ' + item.Meghallgato,
              },
              {
                text: 'Jegyzőkönyvvezető: ' + item.JegyzokonyvVezeto,
              },
              {
                text:
                  item.ElsoSzembesitettTipus +
                  ' – ' +
                  item.ElsoSzembesitett +
                  ', ' +
                  item.ElsoSzembesitettAzon,
                margin: [20, 0, 0, 0],
              },
              {
                text:
                  item.MasodikSzembesitettTipus +
                  ' – ' +
                  item.MasodikSzembesitett +
                  ', ' +
                  item.MasodikSzembesitettAzon,
                margin: [20, 0, 0, 0],
              },
              {
                text: 'Egyéb jelenlévő: ' + item.EgyebJelenlevo,
                margin: [0, 0, 0, 30],
              },
              {
                margin: [0, 0, 0, 20],
                table: {
                  widths: ['*', '*'],
                  body: [
                    [
                      {
                        text: item.ElsoSzembesitettTipus,
                        border: [false, false, false, false],
                        decoration: 'underline',
                      },
                      {
                        text: item.MasodikSzembesitettTipus,
                        border: [false, false, false, false],
                        decoration: 'underline',
                      },
                    ],
                    [
                      {
                        text: 'Név: ' + item.ElsoSzembesitett,
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Név: ' + item.MasodikSzembesitett,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text:
                          'Nyilvántartási szám: ' + item.ElsoSzembesitettAzon,
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          'Nyilvántartási szám: ' +
                          item.MasodikSzembesitettAzon,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text:
                          'Születési helye, ideje: ' +
                          item.ElsoSzembesitettSzulHelye +
                          ', ' +
                          item.ElsoSzembesitettSzulIdeje,
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          'Születési helye, ideje: ' +
                          item.MasodikSzembesitettSzulHelye +
                          ', ' +
                          item.MasodikSzembesitettSzulIdeje,
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                text:
                  'A hamis vád és hamis tanúzás törvényes következményeivel kapcsolatosan részletesen ismertettem a Büntető Törvénykönyvről szóló 2012. évi C. törvény alábbi vonatkozó részeit:',
              },
              {
                text: '268. § (1) Aki',
              },
              {
                text:
                  'a) mást hatóság előtt bűncselekmény elkövetésével hamisan vádol,',
                margin: [10, 0, 0, 0],
              },
              {
                text:
                  'b) más ellen bűncselekményre vonatkozó koholt bizonyítékot hoz a hatóság tudomására, bűntett miatt három évig terjedő szabadságvesztéssel büntetendő.',
                margin: [10, 0, 0, 0],
              },
              {
                text:
                  '272. § A tanú, aki hatóság előtt az ügy lényeges körülményére valótlan vallomást tesz, vagy a valót elhallgatja, hamis tanúzást követ el.',
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'A fegyelmi eljárás során érvényesíthető jogaimról és kötelességeimről tájékoztatást kaptam, azokat tudomásul vettem.',
                margin: [0, 0, 0, 20],
              },
              {
                margin: [0, 0, 0, 20],
                table: {
                  widths: [200, '*'],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                    ],
                    [
                      {
                        text: item.ElsoSzembesitettTipus + ' aláírása, neve',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                    ],
                  ],
                },
              },
              {
                text:
                  'A fegyelmi eljárás során érvényesíthető jogaimról és kötelességeimről tájékoztatást kaptam, azokat tudomásul vettem.',
                margin: [0, 0, 0, 20],
              },
              {
                pageBreak: 'after',
                margin: [0, 0, 0, 0],
                table: {
                  widths: [200, '*'],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                    ],
                    [
                      {
                        text: item.MasodikSzembesitettTipus + ' aláírása, neve',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                    ],
                  ],
                },
              },
              {
                text:
                  'A szembesített felek a meghallgatáson az alábbi megállapításokat tették.',
                bold: true,
                margin: [0, 0, 0, 10],
              },
              jegyzokonyvText,
              {
                text:
                  'Az üggyel kapcsolatban egyebet elmondani nem tudok és nem is kívánok. A jegyzőkönyv az általam elmondottakat helyesen és jól tartalmazza, melyet elolvasás után helybenhagyólag aláírok.',
                margin: [0, 0, 30, 30],
              },
              {
                text: 'Jegyzőkönyv lezárva: ' + item.Ora + ':' + item.Perc,
                margin: [0, 0, 0, 30],
              },
              {
                margin: [0, 0, 0, 30],
                table: {
                  widths: [130, 200],
                  body: [
                    [
                      {
                        text: item.ElsoSzembesitettTipus + ' aláírása: ',
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          item.ElsoSzembesitett +
                          ', ' +
                          item.ElsoSzembesitettAzon,
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, 0, 0, 0],
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 80],
                table: {
                  widths: [130, 200],
                  body: [
                    [
                      {
                        text: item.MasodikSzembesitettTipus + ' aláírása: ',
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          item.MasodikSzembesitett +
                          ', ' +
                          item.MasodikSzembesitettAzon,
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, 0, 0, 0],
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 40],
                table: {
                  widths: [200, '*', 200],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 200,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Meghallgató',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: '',
                      },
                      {
                        text: 'Jegyzőkönyvvezető',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              item.EgyebJelenlevo != null && item.EgyebJelenlevo != ''
                ? {
                    margin: [-5, 0, 0, 40],
                    table: {
                      widths: [120, '*', 180],
                      body: [
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 180,
                                y2: 12,
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                        ],
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: item.EgyebJelenlevo.split(', ')[0],
                            border: [false, false, false, false],
                            alignment: 'center',
                            margin: [0, -4, 0, 0],
                          },
                        ],
                      ],
                    },
                  }
                : null,
              // {
              //   margin: [0, 0, 0, 0],
              //   table: {
              //     widths: [200, '*'],
              //     body: [
              //       [
              //         {
              //           canvas: [
              //             {
              //               type: 'line',
              //               x1: 0,
              //               y1: 12,
              //               x2: 200,
              //               y2: 12,
              //               lineWidth: 1,
              //             },
              //           ],
              //           border: [false, false, false, false],
              //         },
              //         {
              //           text: '',
              //           border: [false, false, false, false],
              //           alignment: '',
              //         },
              //       ],
              //       [
              //         {
              //           text: 'Egyéb jelenlévő',
              //           border: [false, false, false, false],
              //           alignment: 'center',
              //         },
              //         {
              //           text: '',
              //           border: [false, false, false, false],
              //           alignment: '',
              //         },
              //       ],
              //     ],
              //   },
              // },
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };
    console.log(docDefinition);

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async MasodfokuFegyelmiHatarozatMegszunteteseNyomtatas({
    iktatasIds,
    naplobejegyzesIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model;

    if (naplobejegyzesIds != null) {
      model = await apiService.MasodfokuFegyelmiHatarozatMegszunteteseNyomtatasByNaploIds(
        {
          naplobejegyzesIds,
        }
      );
    } else if (iktatasIds != null) {
      model = await apiService.MasodfokuFegyelmiHatarozatMegszunteteseNyomtatasByIktatasIds(
        {
          iktatasIds,
        }
      );
    } else return null;

    /*var model = await apiService.FegyelmiHatarozatNyomtatas(
      fegyelmiUgyId,
      iktatasId,
      naploId
    );

    var indoklas = htmlToPdfMake(
      `
    <div style="margin-left: 20px;">` +
        model.IndoklasText +
        `</div>
`
    );*/

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index, pageCount) {
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'FEGYELMI HATÁROZAT',
                alignment: 'center',
                fontSize: 14,
                bold: true,
                margin: [0, 15, 0, 10],
              },
              {
                text:
                  'A büntetés-végrehajtási intézetben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet rendelkezéseire figyelemmel',
                margin: [0, 0, 0, 10],
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [150, '*'],
                  body: [
                    [
                      {
                        text: 'Név',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.Fogvatartott,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Azonosító',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvAzon,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Születési hely',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvatartottSzulHelye,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Születési idő',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvatartottSzulIdeje,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Anyja neve',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvatartottAnyjaNeve,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Elhelyezése',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.Elhelyezes,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Végrehajtási fokozat',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.VegrehajtasiFok,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Szabadulás aktuális dátuma',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvSzabadul,
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                text:
                  'fogvatartott ellen kezdeményezett fegyelmi ügyben a fegyelmi eljárást hatályon kívül helyezem.',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Indoklás:',
                margin: [0, 0, 0, 10],
              },
              // {
              //   text: item.MegszuntetesOka,
              //   margin: [20, 0, 0, 10],
              // },
              {
                text: item.IndoklasText,
                margin: [20, 0, 0, 30],
                alignment: 'justify',
              },
              {
                text: 'E fegyelmi határozat jogerős és végrehajtható.',
                margin: [0, 0, 0, 20],
              },
              {
                text:
                  'E fegyelmi ügyben keletkezett határozatot a fogvatartott előtt szóban kihirdettem, indokoltam.\n A határozat egy példányát a fogvatartottnak átadtam.',
                margin: [0, 0, 0, 30],
              },
              {
                text:
                  'Kelt: ' + item.HatarozatHely + ', ' + item.HatarozatDatum,
                margin: [0, 0, 0, 30],
              },
              {
                margin: [-5, 0, 0, 15],
                table: {
                  widths: ['*', 90, 180],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Határozathozó: ',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                      {
                        text:
                          item.Torvenyszek && item.Torvenyszek != ''
                            ? item.Torvenyszek + '\n'
                            : (item.HatarozathozoNev &&
                              item.HatarozathozoNev != ''
                                ? item.HatarozathozoNev + '\n'
                                : '') +
                              (item.HatarozathozoRang &&
                              item.HatarozathozoRang != ''
                                ? item.HatarozathozoRang + '\n'
                                : '') +
                              (item.HatarozathozoTitulus &&
                              item.HatarozathozoTitulus != ''
                                ? item.HatarozathozoTitulus + '\n'
                                : item.HatarozathozoSzam &&
                                  item.HatarozathozoSzam != ''
                                ? item.HatarozathozoSzam + '\n'
                                : ''),
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -5, 0, 0],
                        bold: 'true',
                      },
                    ],
                  ],
                },
              },
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async MasodfokuFegyelmiHatarozatNyomtatas({
    iktatasIds,
    naplobejegyzesIds,
    fegyelmiUgyIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model;
    if (naplobejegyzesIds != null) {
      model = await apiService.MasodfokuFegyelmiHatarozatNyomtatasByNaploIds({
        naplobejegyzesIds,
      });
    } else if (iktatasIds != null) {
      model = await apiService.MasodfokuFegyelmiHatarozatNyomtatasByIktatasIds({
        iktatasIds,
      });
    } else if (fegyelmiUgyIds != null) {
      model = await apiService.MasodfokuFegyelmiHatarozatNyomtatasByFegyelmiUgyIds(
        {
          fegyelmiUgyIds,
        }
      );
    } else return null;
    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index, currentPage) {
          let indoklasText = htmlToPdfMake(
            '<div style="margin-left: 20px; text-align: justify;">' +
              item.IndoklasText +
              '</div>'
          );
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'II. FOKÚ \n FEGYELMI HATÁROZAT',
                alignment: 'center',
                fontSize: 14,
                bold: true,
                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'A büntetés-végrehajtási intézetben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet rendelkezéseire figyelemmel',
                margin: [0, 0, 0, 10],
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [150, '*'],
                  body: [
                    [
                      {
                        text: 'Név',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.Fogvatartott,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Azonosító',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvAzon,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Születési hely',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvatartottSzulHelye,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Születési idő',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvatartottSzulIdeje,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Anyja neve',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvatartottAnyjaNeve,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Elhelyezése',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.Elhelyezes,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Végrehajtási fokozat',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.TartozkodasFokaJogcime,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Szabadulás aktuális dátuma',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FogvSzabadul,
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                text:
                  'fogvatartott ellen kezdeményezett fegyelmi ügyben: ' +
                  item.FegyelmiUgy,
                margin: [0, 0, 0, 10],
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [150, '*'],
                  body: [
                    [
                      {
                        text: 'Fegyelmi vétség',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FegyVetseg,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Fegyelmi fenyítés',
                        border: [false, false, false, false],
                      },
                      {
                        text: ': ' + item.FenyTipus,
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: 'Fenyítés tartama',
                        border: [false, false, false, false],
                      },
                      {
                        text:
                          ': ' + item.FenyIdotart + item.KietkezesCsokkentes,
                        border: [false, false, false, false],
                      },
                    ],
                  ],
                },
              },
              {
                text: 'Indoklás:',
                margin: [0, 0, 0, 10],
              },
              indoklasText,
              {
                text: 'E fegyelmi határozat jogerős és végrehajtható.',
                margin: [0, 0, 0, 20],
              },
              {
                text:
                  'E fegyelmi ügyben keletkezett határozatot a fogvatartott előtt szóban kihirdettem, indokoltam.\n A határozat egy példányát a fogvatartottnak átadtam.',
                margin: [0, 0, 0, 20],
              },
              {
                text:
                  'Kelt: ' + item.HatarozatHely + ', ' + item.HatarozatDatum,
                fontSize: 12,
                margin: [0, 0, 0, 20],
              },
              {
                margin: [-5, 0, 0, 15],
                table: {
                  widths: ['*', 90, 180],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: 'Határozathozó: ',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                      {
                        text:
                          item.Torvenyszek && item.Torvenyszek != ''
                            ? item.Torvenyszek + '\n'
                            : (item.HatarozathozoNev &&
                              item.HatarozathozoNev != ''
                                ? item.HatarozathozoNev + '\n'
                                : '') +
                              (item.HatarozathozoRang &&
                              item.HatarozathozoRang != ''
                                ? item.HatarozathozoRang + '\n'
                                : '') +
                              (item.HatarozathozoTitulus &&
                              item.HatarozathozoTitulus != ''
                                ? item.HatarozathozoTitulus + '\n'
                                : ''),
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -5, 0, 0],
                        bold: 'true',
                      },
                    ],
                  ],
                },
              },
              {
                margin: [-5, 0, 0, 0],
                table: {
                  widths: ['*', 150, 180],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '1 pld-t a határozatból átvettem: ',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 180,
                            y2: 12,
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        margin: [0, 0, 0, 0],
                      },
                      {
                        text: 'fogvatartott',
                        border: [false, false, false, false],
                        alignment: 'center',
                        margin: [0, -5, 0, 0],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async VegrehajtasiLapNyomtatas({
    iktatasIds,
    naplobejegyzesIds,
    fegyelmiUgyIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model;

    if (naplobejegyzesIds != null) {
      model = await apiService.MaganelzarasMegkezdesenekRogziteseNyomtatasByNaploIds(
        {
          naplobejegyzesIds,
        }
      );
    } else if (iktatasIds != null) {
      model = await apiService.MaganelzarasMegkezdesenekRogziteseNyomtatasByIktatasIds(
        {
          iktatasIds,
        }
      );
    } else if (fegyelmiUgyIds != null) {
      model = await apiService.MaganelzarasMegkezdesenekRogziteseNyomtatasByFegyelmiUgyIds(
        {
          fegyelmiUgyIds,
        }
      );
    } else return null;
    console.log('VegrehajtasiLapNyomtatas model', model);
    // var model = [
    //   {
    //     IntezetNev: 'Váci fegyház és börtön',
    //     Iktatoszam: 46843278,
    //     UgySzam: '18859/99214',
    //     Fogvatartott: 'Teszt Elek',
    //     FogvAzon: 'ZW-7961',
    //     SzulDatum: '1996.07.25.',
    //   },
    // ];

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index, currentPage) {
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'VÉGREHAJTÁSI LAP',
                alignment: 'center',
                fontSize: 12,
                bold: true,
                decoration: 'underline',
                margin: [0, 0, 0, 10],
              },
              {
                text: 'Melléklet a ' + item.UgySzam + ' sz. fegyelmi ügyhöz',
                margin: [0, 0, 0, 10],
                alignment: 'center',
              },
              {
                text:
                  'Név: ' +
                  item.Fogvatartott +
                  ', Nytsz.: ' +
                  item.FogvAzon +
                  ', szül. idő(év, hó, nap): ' +
                  item.SzulDatum,
                margin: [0, 0, 0, 10],
                alignment: 'center',
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [335, 150],
                          body: [
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'I/a. A magánelzárás fenyítés végrehajtása egészségügyi szempontból',
                              },
                              {},
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'a) nem ellenjavallt.',
                                margin: [40, 0, 0, 0],
                              },
                              {},
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'b) ideiglenesen ellenjavallt, előreláthatólag …….. év ………………. hó …….. napjáig.',
                                margin: [40, 0, 0, 0],
                              },
                              {},
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'c) véglegesen ellenjavallt.',
                                margin: [40, 0, 0, 10],
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },
                              {
                                text: 'orvos aláírása',
                                border: [false, false, false, false],
                                fontSize: 10,
                                alignment: 'center',
                                italics: true,
                                margin: [0, -5, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [335, 150],
                          body: [
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'I/b. A magánelzárás fenyítés végrehajtása pszichológiai szempontból',
                              },
                              {},
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'a) nem ellenjavallt.',
                                margin: [40, 0, 0, 0],
                              },
                              {},
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'b) ideiglenesen ellenjavallt, előreláthatólag …….. év ………………. hó …….. napjáig.',
                                margin: [40, 0, 0, 0],
                              },
                              {},
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'c) véglegesen ellenjavallt.',
                                margin: [40, 0, 0, 10],
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },
                              {
                                text: 'pszichológus aláírása',
                                border: [false, false, false, false],
                                fontSize: 10,
                                alignment: 'center',
                                italics: true,
                                margin: [0, -5, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [335, 150],
                          body: [
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'II/a. A magánelzárás fenyítés végrehajtása az orvos/pszichológus (megfelelő rész aláhúzandó) nyilatkozata alapján nem hajtható végre.',
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },

                              {
                                italics: true,
                                fontSize: 10,
                                alignment: 'center',
                                margin: [0, -5, 0, 10],
                                text: 'fegyelmi jogkör gyakorlója',
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'II/b. A magánelzárás fenyítés végrehajtását ……… év ……………….. hó …… nap ……….. órakor kell megkezdeni.',
                                margin: [0, 0, 0, 10],
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },
                              {
                                text: 'fegyelmi jogkör gyakorlója',
                                border: [false, false, false, false],
                                fontSize: 10,
                                alignment: 'center',
                                italics: true,
                                margin: [0, -5, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                pageBreak: 'after',
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [335, 150],
                          body: [
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'III. A magánelzárás fenyítés végrehajtása',
                              },
                              {},
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  '……… év ………………….hó ……….. nap …………….órakor megkezdődött.',
                                margin: [0, 0, 0, 10],
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },
                              {
                                text: 'főfelügyelő aláírása',
                                border: [false, false, false, false],
                                fontSize: 10,
                                alignment: 'center',
                                italics: true,
                                margin: [0, -5, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [50, 100, 235, 82],
                          body: [
                            [
                              {
                                text: 'IV/a. Egészségügyi vizsgálatok',
                                italics: true,
                                colSpan: 4,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                              },
                              {},
                              {},
                              {},
                            ],
                            [
                              {
                                text: '',
                                italics: true,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                              },
                              {
                                text: 'időpont',
                                italics: true,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                                alignment: 'center',
                              },
                              {
                                text: 'feljegyzés',
                                italics: true,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                                alignment: 'center',
                              },
                              {
                                text: 'orvos aláírása',
                                italics: true,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                                alignment: 'center',
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, 0, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                // pageBreak: 'after',
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [50, 100, 235, 82],
                          body: [
                            [
                              {
                                text: 'IV/b. Pszichológiai állapot felmérés',
                                italics: true,
                                colSpan: 4,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                              },
                              {},
                              {},
                              {},
                            ],
                            [
                              {
                                text: '',
                                italics: true,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                              },
                              {
                                text: 'időpont',
                                italics: true,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                                alignment: 'center',
                              },
                              {
                                text: 'feljegyzés',
                                italics: true,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                                alignment: 'center',
                              },
                              {
                                text: 'pszichológus aláírása',
                                italics: true,
                                fontSize: 10,
                                border: [0, 0, 0, 0],
                                alignment: 'center',
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 50,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 100,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 235,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                              },
                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 82,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, 0, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [335, 150],
                          body: [
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'V. A magánelzárás félbeszakítását ………………………………………………………………………….............................................................…………………………………………………………………………………………………………….. miatt javaslom.',
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },
                              {
                                text: 'orvos/pszichológus aláírása',
                                border: [false, false, false, false],
                                fontSize: 10,
                                alignment: 'center',
                                italics: true,
                                margin: [0, -5, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [334.5, 150],
                          body: [
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'VI. A magánelzárás fenyítés végrehajtását orvosi /pszichológusi (megfelelő rész aláhúzandó) javaslat alapján félbeszakítom.',
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },
                              {
                                text: 'fegyelmi jogkör gyakorlója',
                                border: [false, false, false, false],
                                fontSize: 10,
                                alignment: 'center',
                                italics: true,
                                margin: [0, -5, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [116.5, 116.5, 116.5, 116.5],
                          body: [
                            [
                              {
                                colSpan: 4,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                margin: [0, 0, 0, 5],
                                text:
                                  'VII. A magánelzárás fenyítés végrehajtása',
                              },
                              {},
                              {},
                              {},
                            ],
                            [
                              {
                                margin: [0, 0, 0, 5],
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'kezdete',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'vége',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'kezdete',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'vége',
                                alignment: 'center',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '...... év ... hó ... nap ... óra ',
                                alignment: 'center',
                                margin: [0, 0, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                // pageBreak: 'after',
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [30, 203.5, 30, 203.5],
                          body: [
                            [
                              {
                                colSpan: 4,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                margin: [0, 0, 0, 5],
                                text:
                                  'VIII. A magánelzárás végrehajtása alatt reintegrációs tiszt által történő ellenőrzések',
                              },
                              {},
                              {},
                              {},
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                            ],
                            [
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '20 …….',
                              },
                              {
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: '…………………………………………………………………………………….',
                                margin: [0, 0, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [335, 150],
                          body: [
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'IX. A magánelzárás végrehajtása megtörtént.',
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },

                              {
                                italics: true,
                                fontSize: 10,
                                alignment: 'center',
                                margin: [0, -5, 0, 10],
                                text: 'főfelügyelő aláírása',
                                border: [false, false, false, false],
                              },
                            ],
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text: 'Ellenőrizte:',
                                margin: [0, 0, 0, 30],
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },
                              {
                                text: 'fegyelmi jogkör gyakorlója',
                                border: [false, false, false, false],
                                fontSize: 10,
                                alignment: 'center',
                                italics: true,
                                margin: [0, -5, 0, 10],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                width: ['*'],
                table: {
                  body: [
                    [
                      {
                        table: {
                          widths: [335, 150],
                          body: [
                            [
                              {
                                colSpan: 2,
                                border: [false, false, false, false],
                                italics: true,
                                fontSize: 10,
                                text:
                                  'X. A magánelzárás nem hajtható végre, mert a fenyítés kiszabása óta eltelt 6 hónap.',
                              },
                              {},
                            ],
                            [
                              {
                                text:
                                  '..................................,  ..................................',
                                border: [false, false, false, false],
                              },

                              {
                                canvas: [
                                  {
                                    type: 'line',
                                    x1: 0,
                                    y1: 12,
                                    x2: 150,
                                    y2: 12,
                                    lineWidth: 1,
                                    dash: { length: 1.5, space: 2 },
                                  },
                                ],
                                border: [false, false, false, false],
                                margin: [0, -2, 0, 0],
                              },
                            ],
                            [
                              {
                                text: '',
                                border: [false, false, false, false],
                              },

                              {
                                italics: true,
                                fontSize: 10,
                                alignment: 'center',
                                margin: [0, -5, 0, 10],
                                text: 'bv. intézet parancsnoka',
                                border: [false, false, false, false],
                              },
                            ],
                          ],
                        },
                      },
                    ],
                  ],
                },
              },
            ],
          };
        }),
      ],
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async MegallapodasEsFeljegyzesNyomtatas({ iktatasIds, naplobejegyzesIds }) {
    console.log('MegallapodasEsFeljegyzesNyomtatas kezd');
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model;

    var content = [];

    if (naplobejegyzesIds != null) {
      model = await apiService.MegallapodasEsFeljegyzesNyomtatasByNaploIds({
        naplobejegyzesIds,
      });
    } else if (iktatasIds != null) {
      model = await apiService.MegallapodasEsFeljegyzesNyomtatasByIktatasIds({
        iktatasIds,
      });
    } else return null;
    console.log('MegallapodasEsFeljegyzesNyomtatas model', model);

    // var model = [
    //   {
    //     IntezetNev: 'Váci fegyház és börtön',
    //     Iktatoszam: 46843278,
    //     UgySzam: '18859/99214',
    //     EljarasAlaVontFogvatartott: 'Teszt Elek',
    //     EljarasAlaVontFogvAzon: 'ZW-7961',
    //     SertettFogvatartott: 'Teszt Elek',
    //     SertettFogvAzon: 'ZW-7961',
    //     EljarasAlaVontkepviselo:'Nagy János',
    //     Sertettkepviselo:'Nagy János',
    //     Sertettkepviselo:'Nagy János',
    //     FegyelmiUgyId: 348,
    //     EljarasAlaVontatErintoKoltsegek:'1 000 Ft',
    //     SertettetErintoKoltsegek:'1 000 Ft',
    //     ReintegraciosTiszt:'Nagy János',
    //     FeljegyzesMegbeszelesrol:'Lorem ipsum hablatyikum',
    //     //Megallapodas:'Lorem ipsum hablatyikum',
    //     Hatarido:'2019. 12. 24.',
    //     Kozvetito:'Nyomi Béla',
    //   },
    // ];

    var Megallapodas = model[0].Megallapodas ? true : false;
    var Feljegyzes = model[0].FeljegyzesMegbeszelesrol ? true : false;

    if (Megallapodas) {
      model[0].Megallapodas = htmlToPdfMake(
        '<div style="margin-left: 20px; text-align: justify;">' +
          model[0].Megallapodas +
          '</div>'
      );
    }
    if (Feljegyzes) {
      model[0].FeljegyzesMegbeszelesrol = htmlToPdfMake(
        '<div style="margin-left: 20px; text-align: justify;">' +
          model[0].FeljegyzesMegbeszelesrol +
          '</div>'
      );
    }

    var Osszefuzni = Megallapodas && Feljegyzes;

    function megallapodasContent() {
      var megallapodascontent = content.push(
        model.map(function(item, index, currentPage) {
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'MEGÁLLAPODÁS',
                alignment: 'center',
                fontSize: 12,
                bold: true,
                decoration: 'underline',
                margin: [0, 0, 0, 5],
              },
              {
                text: 'Közvetítői eljárás eredményéről',
                bold: true,
                margin: [0, 0, 0, 15],
                alignment: 'center',
              },
              {
                text:
                  'A megállapodás a büntetés-végrehajtási intézetekben fogvatartott elítéltek és egyéb jogcímen fogvatartottak fegyelmi felelősségéről szóló 14/2014. (XII.17.) IM rendelet, közvetítő eljárást tartalmazó részében szabályozott feltételek alapján jött létre az alábbi elítéltek között.',

                margin: [0, 0, 0, 10],
              },
              {
                text:
                  'Létre jön ' +
                  item.SertettFogvatartott +
                  ' név ' +
                  item.SertettFogvAzon +
                  ' nytsz., mint sértett\n és ' +
                  item.EljarasAlaVontFogvatartott +
                  ' név ' +
                  item.EljarasAlaVontFogvAzon +
                  ' nytsz., mint eljárás alá vont, a közvetítői eljárásban részt vett elítéltek között.',
                margin: [0, 0, 0, 10],
                lineHeight: 2,
              },
              {
                margin: [0, 0, 0, 10],
                text:
                  'A közvetítői eljárásban résztvevő felek megállapodnak abban, hogy:',
              },
              // {
              //   margin: [0, 0, 0, 10],
              //   text: item.Megallapodas,
              // },
              item.Megallapodas,
              {
                margin: [0, 0, 0, 10],
                text: 'A teljesítés határideje: ' + item.Hatarido,
              },
              {
                margin: [0, 0, 0, 10],
                text: 'A közvetítés során felmerült költségek:',
              },
              {
                margin: [0, 0, 0, 10],
                layout: 'noBorders',
                table: {
                  body: [
                    [
                      { text: 'Eljárás alá vont:' },
                      {
                        alignment: 'right',
                        text: item.EljarasAlaVontatErintoKoltsegek,
                      },
                    ],
                    [
                      { text: 'Sértett:' },
                      {
                        alignment: 'right',
                        text: item.SertettetErintoKoltsegek,
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 10],
                text:
                  'Az eljárásban részt vett fogvatartottak tudomásul veszik, hogy',
              },
              {
                margin: [10, 0, 0, 10],
                separator: ')',
                type: 'lower-alpha',
                ol: [
                  'A vállalt kötelezettség nem lehet ellentétes a jogszabályokkal, a büntetés-végrehajtás rendjét, biztonságát nem sértheti vagy veszélyeztetheti.',
                  'A megállapodás a közvetítői eljárás céljain túli joghatás kiváltására nem vehető igénybe, a közvetítői eljárás során tett nyilatkozatok, keletkezett iratok más eljárásban bizonyítékként nem használhatók fel.',
                  'A közvetítői eljárás eredményesen fejeződik be, ha az eljárás alá vont fogvatartott a megállapodásban foglalt kötelezettségét teljesítette. Ha a sértett magatartása miatt hiúsul meg a teljesítés, a közvetítői eljárást eredményesen befejezettnek kell tekinteni.',
                  'A megállapodásban foglalt kötelezettségek teljesítését az eljárás alá vont fogvatartott reintegrációs tisztje ellenőrzi.',
                  'A közvetítői eljárás során felmerült költségeket az eljárás alá vont fogvatartott viseli, kivéve azokat, amelyek a sértett fogvatartott érdekkörében merültek fel.',
                ],
              },
              {
                margin: [0, 0, 0, 10],
                text:
                  'Megállapodás helye: ' +
                  item.IntezetNev +
                  ', kelte: ' +
                  item.MegallapodasKelte,
              },

              {
                margin: [0, 0, 0, 10],
                text:
                  'A megállapodásban leírtakat megismertem, megértettem és elfogadom:',
              },
              {
                margin: [-5, 0, 0, 0],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: item.EljarasAlaVontFogvatartott,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: item.SertettFogvatartott,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },

              {
                pageBreak: 'after',
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 190,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: item.Kozvetito,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                  ],
                },
              },

              {
                margin: [0, 0, 0, 15],
                text:
                  'A közvetítői eljárás során kötött megállapodásban szereplő vállalások határidőn belül érvényesültek.',
              },
              {
                columns: [
                  { width: 150, alignment: 'center', text: 'igen' },
                  { width: '*', alignment: 'center', text: '-' },
                  { width: 150, alignment: 'center', text: 'nem' },
                ],
              },
              {
                margin: [-5, 0, 0, 15],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 190,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: item.ReintegraciosTiszt,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                  ],
                },
              },

              {
                margin: [-5, 0, 0, 15],
                id:
                  (iktatasIds && iktatasIds.length > 1 && index >= 1) ||
                  (naplobejegyzesIds &&
                    naplobejegyzesIds.length > 1 &&
                    index >= 1)
                    ? 'NoBreak'
                    : '',
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        colSpan: 3,
                        margin: [0, 0, 0, 10],
                        text:
                          'A közvetítői eljárást: eredményesnek / eredménytelennek  minősítem.',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 190,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: item.FegyelmiJogkorGyakorloja,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                  ],
                },
              },
            ],
          };
        })
      );
      return megallapodascontent;
    }

    function feljegyzesContent(osszefuzni) {
      var feljegyzescontent = content.push(
        model.map(function(item, index, currentPage) {
          return {
            id: osszefuzni ? 'NoBreak' : '',
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'FELJEGYZÉS',
                alignment: 'center',
                fontSize: 12,
                bold: true,
                decoration: 'underline',
                margin: [0, 0, 0, 5],
              },
              {
                text: 'Közvetítői megbeszélésről',
                bold: true,
                decoration: 'underline',
                margin: [0, 0, 0, 5],
                alignment: 'center',
              },
              {
                text:
                  'Készült a ' + item.IntezetNev + ' hivatalos helyiségében.',
                alignment: 'center',
              },
              {
                text:
                  item.MegbeszelesEv +
                  ' év ' +
                  item.MegbeszelesHonap +
                  ' hónap ' +
                  item.MegbeszelesNap +
                  ' napon ' +
                  item.MegbeszelesOra +
                  +':' +
                  item.MegbeszelesPerc +
                  ' órakor.',
                margin: [0, 0, 0, 20],
                alignment: 'center',
              },
              {
                margin: [0, 0, 0, 10],
                text: 'Fegyelmi eljárás ügyszáma: ' + item.UgySzam,
              },
              {
                margin: [0, 0, 0, 10],
                text: 'Közvetítői megbeszélésen részt vevők:',
              },
              {
                type: 'none',
                margin: [0, 0, 0, 10],
                ol: [
                  {
                    text:
                      'Név: ' +
                      item.EljarasAlaVontFogvatartott +
                      ', nytsz: ' +
                      item.EljarasAlaVontFogvAzon +
                      ', mint eljárás alá vont.',
                    margin: [0, 0, 0, 5],
                  },
                  {
                    text:
                      'Név: ' +
                      item.SertettFogvatartott +
                      ', nytsz: ' +
                      item.SertettFogvAzon +
                      ', mint eljárás alá vont.',
                    margin: [0, 0, 0, 5],
                  },
                  {
                    text:
                      'Védő: ' +
                      (item.EljarasAlaVontkepviselo
                        ? item.EljarasAlaVontkepviselo +
                          ', mint az eljárás alá vont meghatalmazottja.'
                        : ''),
                    margin: [0, 0, 0, 5],
                  },
                  {
                    text:
                      'Képviselő: ' +
                      (item.Sertettkepviselo
                        ? item.Sertettkepviselo +
                          ', mint a sértett meghatalmazott képviselője.'
                        : ''),
                    margin: [0, 0, 0, 5],
                  },
                  {
                    stack: [
                      {
                        text: 'Egyéb:',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 35,
                            y1: 0,
                            x2: 500,
                            y2: 0,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                      },
                    ],
                    margin: [0, 0, 0, 5],
                  },
                  {
                    text:
                      'Közvetítő: ' +
                      item.Kozvetito +
                      ', mint az eljárás lefolytatására kijelölt személy.',
                    margin: [0, 0, 0, 5],
                  },
                ],
              },
              {
                text:
                  'A közvetítő tájékoztatja a megbeszélésen résztvevőket a közvetítői eljárás lényegéről, jogkövetkezményeiről, valamint jogaikról és kötelezettségeikről.',
              },
              {
                text:
                  'A közvetítői eljárás más fogvatartott sérelmére elkövetett fegyelmi cselekmény által kiváltott konfliktust kezelő eljárás, amelynek célja, hogy egy közvetítő, harmadik személy bevonásával a sértett és a fegyelmi eljárás alá vont fogvatartott közötti konfliktus rendezésének megoldását tartalmazó, a fegyelemsértés következményeinek jóvátételét és az eljárás alá vont fogvatartott jövőbeni szabálykövető magatartását elősegítő írásbeli megállapodás jöjjön létre.',
              },
              {
                text:
                  'A közvetítőnek biztosítania kell, hogy a résztvevők egymással szemben tisztelettel járjanak el.',
              },
              {
                text:
                  'A közvetítői eljárás a fegyelmi jogkör gyakorlójának közvetítői eljárásra utaló döntése napján kezdődik. A fegyelmi jogkör gyakorlója közvetítői eljárásra utaló döntésében kijelöli a közvetítőt és szükség szerint rendelkezik a kiszabott fenyítés végrehajtásának felfüggesztéséről.',
              },
              {
                text:
                  'Az eljárás során az eljárás alá vont jogosult védő, a sértett, képviselő meghatalmazására.',
              },
              {
                text:
                  'Amennyiben a 14/2014. (XII.17) IM rendeletben foglalt kizárási okok fennállnak, a fogvatartottak bármelyike azt bejelentheti.',
              },

              {
                text:
                  'A sértett és az eljárás alá vont fogvatartott indítványozhatja, hogy érdekében legfeljebb egy-egy, általa megjelölt fogvatartott a közvetítői megbeszélésen jelen legyen és felszólalhasson. A közvetítő az indítványt csak abban az esetben utasíthatja el, ha a fogvatartott jelenléte a közvetítői eljárás céljával ellentétes. A döntés ellen jogorvoslatnak nincs helye.',
              },
              {
                text:
                  'A közvetítői megbeszélést a közvetítő vezeti, aki meghatározza a közvetítői megbeszélés menetét, valamint a közvetítői megbeszélésen résztvevők felszólalásának rendjét.',
              },

              {
                margin: [0, 0, 0, 10],
                text:
                  'A sértett és az eljárás alá vont fogvatartott az üggyel kapcsolatos álláspontját szóban kifejtheti, és a rendelkezésére álló iratokat is bemutathatja.',
              },
              {
                margin: [0, 0, 0, 10],
                text:
                  'A közvetítő által, a közvetítői eljárás lényegéről, jogkövetkezményeiről, jogaimról, kötelezettségeimről szóló tájékoztatást megértettem.',
              },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: item.EljarasAlaVontFogvatartott,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: item.SertettFogvatartott,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
                pageBreak: 'after',
              },

              {
                margin: [0, 0, 0, 15],
                text:
                  'A közvetítői megbeszélés során az alábbi a jelenlévők részéről az alábbiak hangzottak el:',
              },
              // {
              //   margin: [0, 0, 0, 10],
              //   text: item.FeljegyzesMegbeszelesrol,
              // },
              item.FeljegyzesMegbeszelesrol,
              {
                margin: [0, 0, 0, 10],
                text:
                  'Az üggyel kapcsolatban egyebet elmondani nem tudok és nem is kívánok. A feljegyzés az általam elmondottakat helyesen és jól tartalmazza, melyet elolvasás után helybenhagyólag aláírok.',
              },
              { alignment: 'center', text: 'Kelt, mint fent' },
              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: item.EljarasAlaVontFogvatartott,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: item.SertettFogvatartott,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },

              {
                margin: [-5, 0, 0, 15],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: item.Kozvetito,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                    ],
                  ],
                },
              },
            ],
          };
        })
      );
      return feljegyzescontent;
    }
    if (Megallapodas && !Osszefuzni) {
      megallapodasContent();
    }
    if (Feljegyzes == true && !Osszefuzni) {
      feljegyzesContent(Osszefuzni);
    }

    if (Osszefuzni) {
      megallapodasContent();
      feljegyzesContent(Osszefuzni);
    }
    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: content,
      pageBreakBefore: function(
        currentNode,
        followingNodesOnPage,
        nodesOnNextPage,
        previousNodesOnPage
      ) {
        if (
          currentNode.id === 'NoBreak' &&
          currentNode.pageNumbers.length != 1
        ) {
          return true;
        }
        return false;
      },
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download('Megállapodás_Feljegyzés');

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }
  async KozvetitoiEljarasonReszvetelNyomtatas({
    iktatasIds,
    naplobejegyzesIds,
  }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    var model;

    if (naplobejegyzesIds != null) {
      model = await apiService.KozvetitoiEljarasonReszvetelNyomtatasByNaploIds({
        naplobejegyzesIds,
      });
    } else if (iktatasIds != null) {
      model = await apiService.KozvetitoiEljarasonReszvetelNyomtatasByIktatasIds(
        {
          iktatasIds,
        }
      );
    } else return null;
    console.log('KozvetitoiEljarasonReszvetelNyomtatas model', model);
    // var model = [
    //   {
    //     IntezetNev: 'Váci fegyház és börtön',
    //     Iktatoszam: 46843278,
    //     UgySzam: '18859/99214',
    //     Fogvatartott: 'Teszt Elek',
    //     FogvAzon: 'ZW-7961',
    //     SzulDatum: '1996.07.25.',
    //     IsSertett: true,
    //     Javaslat:
    //       'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?',
    //     ReintegraciosTiszt: 'Teszt Béla',
    //   },
    // ];

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        model.map(function(item, index, currentPage) {
          var javaslat = htmlToPdfMake(
            `
          <div style="text-align: justify;">` +
              item.Javaslat +
              `</div>
              `
          );
          return {
            stack: [
              {
                image:
                  'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQICAgICAgICAgICAwMDAwMDAwMDAwEBAQEBAQECAQECAgIBAgIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD/8AAEQgAWAAwAwERAAIRAQMRAf/EAJQAAAICAwEAAAAAAAAAAAAAAAgJBgoABQcDAQACAwEAAAAAAAAAAAAAAAACAwABBAUQAAAHAQAABgIABgMBAAAAAAECAwQFBgcIABESExQJFRchIkMWGApCIyQyEQACAQIEAwQIBAUFAAAAAAABAhEhAwAxEgRBUWFxIjIT8IGRobFCIwXB0ZIU4fFSYjNyotJDNP/aAAwDAQACEQMRAD8Av8eJiYzxMTFZH7WfsC3XkXqydn8C19/bYumc/wB8T1rIZagalLZ7iK0dnp7LB6VLyNJqdwrj52lJXuuzL15JskyRzJJJBZ4Rm5WSJ0dtZsXduTuAVQOIcGskgRGZoGgSKmeuAYwYGcenpyw8fix1fpHm3OpjR9tpfQ8/PNZSdZ6xn0jGztUsVZlph89qqcdZoiErDC1mjIBZBsrJEi475aqZjC3THz8+exDMSBHMTMHiOlZpwywdeOCn8ViYzxMTGeJiYUt1v9l0nUdXJxpwplf+XvdEs1WcytOZyZ4bEucq6mqVo70XpvU0iKRtSiYx0qBEYFmdaelHIA2TSROomYxIqt3n1CzMFlANYNBNJFJBIzBwJPBY1YTl3Fg+2c8fC3Dv7s2/3E92xqZDYlub4NDOE84qxOjOYKRoCVBsaaDacukZO5/rs3DN4ywNCQEe1dicGaavrdeMu53Vy3eSxtEUJcuWwdUEkFtJJMgCBUkVGnxQIK2BAUtHeevsJz7QI9mWNLzVmR7nvjrBOKes+vsIGo6a2r8s60zTaDsbaBd2bCNH3S05+3Qxi8SGOzFJvsk3g5ly7iX/AOajX4GRKu3FM7bx0RcPkL5qhrbA6TQNQgTME0FADSCTGDAkQJU/D1e/DQ8x746L4x1CA5o+0aoIs6xaBdNcP7loQLWDJr2hGoGcr1zYTNYuKe0a6xbIhlFHi0c0SVZpGcuEgQQcyRwZRdP0VINKZnqeYHGtI4yMWshRrNcPCYP2MoxZycY8aSMbItG7+PkGDhF4xfsXiJHDR4zdtzqN3TR03UKdNQhjEOQwGKIgID4TlQ54LAE/Z122++vTkG79RMKBHaUrUbHQK6atSlp/tJp5X24RNOSkxfEjJd1ILRrqYTUTj26IOH5/JFM5DHAwGi62g0EGvYJ7a5U54mK8X1r6hpXMmOyv2XW+2Pn3PFsscpkXbdDctbAjrEx0/pWnsn9v2uAiXiRCxgY3tt+TyyBpCqR3ZYsjyQ+UL5woi5bubxa41guGFoxAA0rAkAaZB1qyszTyBkqcLVqTpPXn/HswZvCeAM9Zv+Kcpdtwkxomp8I4hotvv9A02ySWh162WTpTYoW75vZLd+cMuw1Cq1+k1RoWLSlE3TNnZ2b8CJ/IikjppKPqO4t6ls91Qad4wdRjMGa8M4ypi1LairVETPby4iBnPPhXBGdq4JzpybqfP++5BnpsitN8ltfwdek8+x8fnzbXL9qHPukQGPkRqldYtq+51xhcUkY6BmitSvWjeTc++qdqmAJEu4ZIsFPMU94A/LBBYjjBEyMjnngWYLcCjxMCfZA/HC5rHD9A6xylD89W+2yrGu/UXRm9h+wiJmL1bT3PctBjIy6zZIXMLa4EZGCi81zGBbaVTLBMHfg8UlIFoZEot3joloj3botoxRLhKgwJgmADpNJ8DxBA1EZjFtXxLJFfXEZcaE8cGj/r29z3jqTna4YJd6iCD7ihnn2St9RWeDFSOrwswW5O6LYnuaOomNkKCm9zKDhJBsHqUaP20iRVsRFECphd615UAZyQRAADDTIEEyK5zSIrEklNMqRjo339Z+w07jvOaZNXCKo8BLdK58pLWC1Q87ZaK3WhKlodnrLS21itgWYnTTVzgY2OhyJKJi3sD1i4AFDokSPVpEuEozBSVMTEkyPDObASYAYkAgCsiGZEc8V7Z3jm+s3+i5q81vSKtSRulK0a8V2159qshE4vc57Rue5sOgOgI+LXa1+4LT8/oEjYIl/F/EftpGrg6duCgi4VR51uwLtk7kaV3CwkMV1MmkkxC91EgI0jVWhymld3E6YOdZ8XaBn16Y6Qny7pMUwntiL1XrK12f1uz1JfoRqXQarS53MaR0hQM3gNlsb5/Jv9RPz1TWupTEs/agq4STeMXLlu8RZKquiaBs9nctpaYk7fUpYAq0NpIKrC1JKgKTJGoahIIxRu3LbKIAdgxOfCPbVuQy642dh4+0CXmSMnnZ927Tk8nvkLP5hrmEykxOU1zfpTDehrq2z2mqyt1vrSt9BQ/wCu2KXyYSYk/KFsiYKJNXCgJqMtfbdqLNxrf0ka3JWQREqJJKglIYiKE5kngZd9QZ4McePX4DHDbPzFoqFO1Ofb9aSejs9Pissba/p0Hne9Fzm3zFkxboN+4wbWoh5LvrJdr+3UyaAq0Yd04+O2b3Jq3dNSqC3br5bO22xv2VuMoUXNIMqAqg0aSImJMGkjxGcFLhSSAK9azxyzw2b6Q8AdYt2r1yopabN897lcFC3DOpOBnK1N1Z7UtTsVBz9ztJpUpGN0u7rPqGgrVX7BNFpHV106akAwHAS6bN5n2ih7RlrjHzCUPggaQFggMSWqDMCs5ptksNTDSQMu2pPtp6jjoH3B0uY3HXDUCz6jqUHnuZxeZ3es0WnTFWj6ktdXWH95XYbRY4edp9kTscqzmMjhRbfJMKDZJBT20yqnBUh27du9Zui5WGiOEHyxEcR3jiI7G444Kwj9M/HCzIfiKgO7oygVNG2csaGsS1OBJJ7kRFyQ6HS36nT9tf8ATQmSff2kYSGVKAet2b3RD/j4w7nY7K3dCrbt6SiE0GZVCficaldzBk8fx/LEEovFWdXGvQ9nnbnpzuZkaTmrp299jFxVUTvOPfXBeLDH+4ti6ywRLue6jtBithOKQIGYJmA3xCmUttrtUFpVtrpfUTSkqzgEDKYX44W9x1dQPmMf7Z+ONDGcKY9D59JOYacucMSr5LfdIgYyJgMEiYaIt8Dz303pbN+wiIzEG7NmU1my6LBUUipqqtRcpGP/AOgwl02tpZvbV9xfWb+hpNagXiigzUgKBQ0mowPmt5vljwxyHp+PPE+muELOwmv7mN+zUcNHqTaudGWjK6nhzyyvFM7tm8Fi1T0AnPib9kkvDUOGjlVhfmUUeNXL0UipOCELhOx2nlT5duShNFHi0kiZERIBOdMq4ZruawpNJ9v5YInhjKVOf+0ec7TnWqaui50Jg0pN9ipCRoRK9bqq9iuZLMav2CKr+d1757SPntTlXTI5lCrsjqEBBQpBXI41C1bsbO1dtIqPcvKDHEFLhMdpQdYHCpxnt3nuhGb5gZ/ST8RhnXTKfACv2H6wTu8cTCMDlvng2cfudZFFn801z6XJaRghdnIzM4/CCIO/PzUBmKn9EVvGj7efuA3t39j5kQs6edNPrzj+WED/ANL6v8fumFj3TjxK3/19RceZFeJxd/IHyEj+N+X8z8oUDeXpce/8384QPP8AqfNAP6oB46mr7wFzuaSY6TGXbHDOOmNH0+mPJu3/ANe8qZPiK8QFQ9tuCXxH8SCIoka1wzIEvYce2KJWCcMKHl/L7BY/0fyA08F5n3uAdV7TEipiK1HTOo69cSLfTCoK2tkJMvyyHuO9oczYhcc+rVIk9gZQMbY2lfzy5czdfwqrZg1tEPNRrBrJ1t0YiT9w3ODJMAVEwCX1eM2zS7etOCuu5DSKVjcsCKHnnHXI5Lot7l3R+Ppyw2mzVfkBfgKv2ST77/F5ot1RfNWa9UhXqUQJbZrldNNTuNPCmrVdSvIgk+tEzH/CLHlWafF9wRAyRjeMgsze8nQZFNMwfDGfrnDw3zE1n07cAPzY5qDjqzmQmf68beqLG6tcYKoa2aEja8NwrlffciQcQ6GMiI6IjQOyYsCNRcIt0SPPZ98AN7nma99aazt7NtlKEbhKTP8A1XjzMdnDphShVdFXwjVH6GwU/wBiv7GsXXOgUfL8E2zbZwmO43bJYcpr9OlI2vRMnlv2AZLFlnX9tvVNKg+kbZpDAEkUSuB+GRysYSgj6TrsX7a+bYM+YWBHZ9P/AIn3YG2R59xfmJB9QUD8RgZoir9QMrm2nl+DeyvgJavJXM4kpmNncfhXXS37XRMCP7zDzejU/wCYUvP+Dv8A6vP/AJeB3IF26GTwhVHrCqD8DjUpgV6/jiI57nnV1ZqsLCynBnYYPGFUy6GXBtUcaXRB7UMf+uKjTAJq/vEnrRLO8s2oET+Qe4gmxU8g+WBUxdQ3lQPAGn1s5EfqGFuCzIRkDJ/THxx64dk2xdJRM7X84u+C5JHq801FOYZ9AntLS7pq77mXVePnSQhqnNtI1spVY+2HcrkOu596Qbi39z2xFUdNp0tfbrdxqi6bq+LSRovsZEhuzp7hBb+qW4wOHMT05/xw1ixcd67K8lQtcS2LnlDTEut9R6beWtwS3Diqn7PsetOl6fHPiThbMZWJZ6UCSbpRcfecNDAKZSn8i4zGhajSKZ9CKHn6o6HDCCXniDOWAEpOSani3cvJFR0m/wCCX8rxNpKw73FXVrVcMfxE7zbnblKyJWSSkU003iVDRcNjImKPuLLpmAQTIJiukfs7KLEDcqM5P+O7HAcM+ZqIwhbYtNbStJFf9DYLXrTo7buWu49X0DKMzzDSo20894PT5trfdBs1EfxUjWYztDYWq0X+EpFvaybGRgc7kWyhlDIKIu1G/kUyZjmKj9vuXuvf2+gAEAlqiO5kAQZlhnQ+o4QFY33dCAwMVE5hTzHLEWYfab2k/sKNdJy/zaRZa4OqUDoehNBMiWUaa4GPqL+2GKAoLI0x5vAN/wDXxQ9Ih7n8PEvLv7LaddgsVU+F/mAOerqBhwG5ORX2dv8Ad0xpav8AbV2RaoljMs+WudGrSQiafNIlddB38Vis7nRuVb6wKoRLFjlBw1jetIVBUoCIe/GPfIRKKImojfrpl7HfmO6/AsDPe/t9/Q4FjfUgSskwKdJ/q5YT7o8fiqecRD/fMip1wtsnzJcoFGxEycmqxVSmD8x9bIxzD+5F6u+m4WEQ0ebYKMXCqKRUnQFeKAiKKiqe/b6b20Fll1XWD6Qc5/cMSRJoIAr2cwMGGK34kadM0y/ngxLpesbPytB4fIYBOMue0/s06XcV3QJDKax/iPJwraa6Ks0Cxg3iTleJQrJlZFqhEv1YlCBdTSIsmjpR4mVMU27Ra35YWCtuiwZJNvuwBlmKmAMzInDGuKG1/KDnn6dmOXcOR2LI9V81vsgymOzkhLVEws3INcmRyxeyKxkDx1/EiKkLCS0vHMZZZ8omdykUPddKLEAQces97i2E2tkMqi8NwgMRP+O8anjXiJB5zkm3rBtq86q+3Q2HBda8kWPqHt2bpUV0PaMMZv8Amii3pFvX83zu7KWl9ASnROMWFYr+8xr9RgnXqnu5iGRblEouJRBY4gZJMDNtFrSsRBRzUGeS9n9I44MKockGrQT6hFMR1t9Peps5hOcbfYHoxXyVqXuJDKc8c/qJhNOtEDUlze3+AIQWw2/zUKn5eRW4+z/EoeYg/wBQd+rwBPGAAAIypA4TTDQSMssauA+mLQ63GM4eM+wDSyMWEbXIhuVbn7AFlvx9TqXP1KhE1Vj17/sXSg+Y6kVVT0h7qiDs4gAuz+myVYKCBKAwa8SSePHUfdgSAxBOYMj2R8MVrNG0XRNTqON5yfE9LtUFCaxSIW82qB2mPy6L1ehZvVuiMh0h1KWDG5s+g0aD0e030zFlDwTeUk15dgpALsSPXLFi+fYazb2wtNcNu9cLBCqwA3nG6oliNNIGo1jvjUCcKZS9wgTkM+lDE+7lgoC7FXpnHIzDHvG0G2oMFr1m6FbJm+wHpFUyeh3bOZhedsRrNG5u5aS2bvG4uHiLZu2WhU4RY9rjW6pWE9+Iwnztrd/b3XuWrhJANJMQCJKiGAyWjalikqWrTKFNRkESe7lwOUR1joTiU/WJH61/ktzfTZ+k/go+I6S2JRo3t22y2p6Qem2dpHa3CfhZaXj5qauFaxSv4Wxrcy5npdtOgnOwz1NspGumzhfduPJezbsAtpTS4MRqYBxzqDrYmBQ+uDC6SC0yD8QRw7ezFgf7ZuWbxt2Owmv4yz02Y2rndpoU1A0jJNOt2S3DVqbcqW/grPQWVlpU7XJR9Kxc2hD2mGj1nXxH03XGzY5P/R6i1YYBtDsBbaJmqyMi3TgTwBJrlgyDmJnFfKsX/nCatUcYexdESgHOrS0GdpM977rXXbatN+ohpRWEzETe1R0zCOmVDTFqqm8SQdIIgY6gFOAnCbtLtq75SINICAkCe8VUkSOJknFppIBPXOnPh7MRjO9AwuSqcE6sPZ18PKuqrlT10Z79hOwNXKktM459as7ZvUiO5I+lVa26loZlk/SAJLupFLyL8ICNxc3VWzcVBoYMSdOcNc0+4DtgYB5DqFyJr2aZ+OOP44lW3OZtIyFWZPoL87pjeGfmmI2wxk3Et7nNxSSi87PpRbCQfmrCsc2cO5s/tSUYs1Zz71zCua9bYRjKLtg2t2FbbsYaAQUbUY1QTEmSrrk+qAGD2yAa3cdrQMXkryMcCOfKcuBx0Zs0Vbyftv13CRwUWdoSny7A1kouRbWf3XL0qi5465pPy3tgUz1BQWdkQsrUFvNhoLf3r2kIQr7L7iGvWyv07mcgDwmMrirVWBKlRIm2ZS0gNqalxTPStJHQ8VPHLhhsX0v4ZarxrNq69esxQxSDosxmOMvlGzJtDaVcZmdj1rdplEZw6TCBNSKPEQ7mvwkpHoGhJQZ2XXg0oiMcfhGTLq3LSiy76jOogioPymZJDEE6wanu6paSTiW1EyOH8PhPLFkzwjBYALWPr5yi06FZdsyNtUMb2e7O2j/RJpXKaFo+f668YolbtnmsZxaY4iM5NEakBH8zFP4SfMkAEO+UTKUgSWgLJ0g0E0ExMdsDnBrE5iVrMmcQM1auOaiVPXfrwxDVYdqUTu9A5bgs5mHRiHcGIRZfG9Tj6hbWZiFXFRVGMl7CqACf0esQ8jAGud43BlJBEmQKgZTqOXKRnXElsopivLhPB3cduiIWAo/KtsqcWeZnXzqd2yYb4NVq5FSlxtU1ENGdfnT3XSmMvWmMsugVCOg3UQgs6Iux9MZIzsG502boF1muFsiCI1aqCawqkNIma901Loj4y3duLtxbiSlxTmPTiMxkRQ1FG34R9J0E4eRtl7Y0qN28GpWbgcIziCkKLgBpBnDEgUjWz8jIyN91Bo1iwFoi1fu2EWeLTaRztk7ZxcWmzjXjoNpR9KCK1MTPKhHCMswZJnVpkkt6en5YevGxsdDRzCIiGDKKiYpk1jYuLjWqDGOjY5igRsyYMGTVNJszZM2yRU0kkylImQoFKAAAB4Tgsf/Z',
                alignment: 'center',
                width: 30,
                height: 55,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.IntezetNev,
                alignment: 'center',
                fontSize: 12,
                opacity: 0.5,
                margin: [0, 0, 0, 10],
              },
              {
                text: item.Iktatoszam,
                alignment: 'right',
                fontSize: 10,
              },
              {
                text: 'NYILATKOZAT',
                alignment: 'center',
                fontSize: 12,
                bold: true,
                decoration: 'underline',
                margin: [0, 0, 0, 5],
              },
              {
                text: 'Közvetítői eljáráson való részvételről',
                bold: true,
                decoration: 'underline',
                margin: [0, 0, 0, 5],
                alignment: 'center',
              },
              {
                text: item.ErintettsegFoka,
                margin: [0, 0, 0, 20],
                alignment: 'center',
              },
              {
                margin: [0, 0, 0, 15],
                text:
                  'Alulírott ' +
                  item.Fogvatartott +
                  ' (név), ' +
                  item.FogvAzon +
                  ' nytsz. elítélt, mint ' +
                  item.ErintettsegFoka +
                  ' kezdeményezem, hogy a ' +
                  item.UgySzam +
                  ' számú fegyelmi eljárásban közvetítői eljárás kerüljön lefolytatásra.',
              },
              {
                margin: [0, 0, 0, 15],
                text:
                  'Alulírott ' +
                  item.Fogvatartott +
                  ' (név), ' +
                  item.FogvAzon +
                  ' nytsz. elítélt, mint ' +
                  item.ErintettsegFoka +
                  ' hozzájárulok, hogy a ' +
                  item.UgySzam +
                  ' számú fegyelmi eljárásban közvetítői eljárás kerüljön lefolytatásra.',
              },
              {
                margin: [0, 0, 0, 5],
                text:
                  'Jelen nyilatkozatommal tudomásul veszem, hogy közvetítői eljárás lefolytatása engedélyezhető, ha',
              },
              {
                margin: [15, 0, 0, 15],
                type: 'lower-alpha',
                separator: ')',
                italics: true,
                ol: [
                  'a fegyelmi eljárás alá vont fogvatartott a fegyelemsértés elkövetését beismerte,',
                  'a fegyelmi eljárás alá vont fogvatartott és a sértett a közvetítői eljáráshoz hozzájárult, és',
                  'a fegyelemsértés jellegére, a fegyelmi eljárás alá vont fogvatartott személyére tekintettel a fegyelmi eljárás lefolytatása vagy a fenyítés végrehajtása mellőzhető.',
                ],
              },
              {
                margin: [0, 0, 0, 15],
                text:
                  'Büntetőeljárás gyanúját megalapozó, továbbá olyan fegyelemsértés esetén, amely miatt a sértett magánindítványt tett, közvetítői eljárás nem alkalmazható. Nem alkalmazható a közvetítő eljárás akkor sem, ha a fegyelmi eljárás alá vont fogvatartott vagy a sértett előzetes letartóztatásban lévő terhelt.',
              },
              {
                margin: [0, 0, 0, 15],
                text:
                  'A közvetítői eljárás során felmerült költségeket az eljárás alá vont fogvatartott viseli, kivéve azokat, amelyek a sértett fogvatartott érdekkörében merültek fel.',
              },
              {
                margin: [0, 0, 0, 15],
                text:
                  'A jelen közvetítői eljárás lefolytatására irányuló szándéknyilatkozatot elolvasás és értelmezést követően, mint akaratommal mindenben megegyezőt, jóváhagyólag aláírok.',
              },
              {
                margin: [0, 0, 0, 15],
                text:
                  'A jelen közvetítői eljárás lefolytatására irányuló szándéknyilatkozatot elolvasás és értelmezést követően, mint akaratommal mindenben megegyezőt, jóváhagyólag aláírok.',
              },
              {
                margin: [-5, 0, 0, 0],
                table: {
                  body: [
                    [
                      {
                        text: item.Telepules + ', ' + item.IktatasDatum,
                        border: [false, false, false, false],
                      },
                      // {
                      //   canvas: [
                      //     {
                      //       type: 'line',
                      //       x1: 0,
                      //       y1: 12,
                      //       x2: 50,
                      //       y2: 12,
                      //       dash: { length: 2 },
                      //       lineWidth: 1,
                      //     },
                      //   ],
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   margin: [-5, 0, 0, 0],
                      //   text: '.',
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   canvas: [
                      //     {
                      //       type: 'line',
                      //       x1: 0,
                      //       y1: 12,
                      //       x2: 20,
                      //       y2: 12,
                      //       dash: { length: 2 },
                      //       lineWidth: 1,
                      //     },
                      //   ],
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   margin: [-5, 0, 0, 0],
                      //   text: '.',
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   canvas: [
                      //     {
                      //       type: 'line',
                      //       x1: 0,
                      //       y1: 12,
                      //       x2: 20,
                      //       y2: 12,
                      //       dash: { length: 2 },
                      //       lineWidth: 1,
                      //     },
                      //   ],
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   margin: [-5, 0, 0, 0],
                      //   text: '.',
                      //   border: [false, false, false, false],
                      // },
                    ],
                  ],
                },
              },

              {
                margin: [-5, 0, 0, 10],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: item.Fogvatartott + ', ' + item.FogvAzon,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              {
                margin: [0, 0, 0, 15],
                text: 'Reintegrációs tiszt javaslata:',
              },
              javaslat,
              {
                margin: [-5, 0, 0, 0],
                table: {
                  body: [
                    [
                      {
                        text: item.Telepules + ', ' + item.IktatasDatum,
                        border: [false, false, false, false],
                      },
                      // {
                      //   canvas: [
                      //     {
                      //       type: 'line',
                      //       x1: 0,
                      //       y1: 12,
                      //       x2: 50,
                      //       y2: 12,
                      //       dash: { length: 2 },
                      //       lineWidth: 1,
                      //     },
                      //   ],
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   margin: [-5, 0, 0, 0],
                      //   text: '.',
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   canvas: [
                      //     {
                      //       type: 'line',
                      //       x1: 0,
                      //       y1: 12,
                      //       x2: 20,
                      //       y2: 12,
                      //       dash: { length: 2 },
                      //       lineWidth: 1,
                      //     },
                      //   ],
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   margin: [-5, 0, 0, 0],
                      //   text: '.',
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   canvas: [
                      //     {
                      //       type: 'line',
                      //       x1: 0,
                      //       y1: 12,
                      //       x2: 20,
                      //       y2: 12,
                      //       dash: { length: 2 },
                      //       lineWidth: 1,
                      //     },
                      //   ],
                      //   border: [false, false, false, false],
                      // },
                      // {
                      //   margin: [-5, 0, 0, 0],
                      //   text: '.',
                      //   border: [false, false, false, false],
                      // },
                    ],
                  ],
                },
              },

              {
                margin: [-5, 0, 0, 15],
                table: {
                  widths: [150, '*', '*'],
                  body: [
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        canvas: [
                          {
                            type: 'line',
                            x1: 0,
                            y1: 12,
                            x2: 170,
                            y2: 12,
                            dash: { length: 2 },
                            lineWidth: 1,
                          },
                        ],
                        border: [false, false, false, false],
                      },
                    ],
                    [
                      {
                        text: '',
                        border: [false, false, false, false],
                      },
                      {
                        text: '',
                        border: [false, false, false, false],
                        alignment: 'right',
                      },
                      {
                        text: item.ReintegraciosTiszt,
                        border: [false, false, false, false],
                        alignment: 'center',
                      },
                    ],
                  ],
                },
              },
              {
                id:
                  (iktatasIds && iktatasIds.length > 1 && index >= 1) ||
                  (naplobejegyzesIds &&
                    naplobejegyzesIds.length > 1 &&
                    index >= 1)
                    ? 'NoBreak'
                    : '',
                margin: [0, 0, 0, 0],
                text: [
                  {
                    text:
                      'Alulírott kijelentem, tudomásomra hozták, hogy a fegyelmi eljárást közvetítői eljárásra utalták, közvetítőnek név: ………………………...... rendfokozat: ……………………. beosztás: ………………………. bízták meg.',
                  },
                  {
                    text:
                      'A közvetítő személyét elfogadom – kizárását indítványozom.',
                  },
                ],
              },
              {
                id:
                  (iktatasIds && iktatasIds.length > 1 && index >= 1) ||
                  (naplobejegyzesIds &&
                    naplobejegyzesIds.length > 1 &&
                    index >= 1)
                    ? 'NoBreak'
                    : '',
                stack: [
                  {
                    margin: [-5, 15, 0, 0],
                    table: {
                      body: [
                        [
                          {
                            text: item.Telepules + ',',
                            border: [false, false, false, false],
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 50,
                                y2: 12,
                                dash: { length: 2 },
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                          {
                            margin: [-5, 0, 0, 0],
                            text: '.',
                            border: [false, false, false, false],
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 20,
                                y2: 12,
                                dash: { length: 2 },
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                          {
                            margin: [-5, 0, 0, 0],
                            text: '.',
                            border: [false, false, false, false],
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 20,
                                y2: 12,
                                dash: { length: 2 },
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                          {
                            margin: [-5, 0, 0, 0],
                            text: '.',
                            border: [false, false, false, false],
                          },
                        ],
                      ],
                    },
                  },

                  {
                    margin: [-5, 0, 0, 15],
                    table: {
                      widths: [150, '*', '*'],
                      body: [
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                            alignment: 'right',
                          },
                          {
                            canvas: [
                              {
                                type: 'line',
                                x1: 0,
                                y1: 12,
                                x2: 170,
                                y2: 12,
                                dash: { length: 2 },
                                lineWidth: 1,
                              },
                            ],
                            border: [false, false, false, false],
                          },
                        ],
                        [
                          {
                            text: '',
                            border: [false, false, false, false],
                          },
                          {
                            text: '',
                            border: [false, false, false, false],
                            alignment: 'right',
                          },
                          {
                            text: item.Fogvatartott + ', ' + item.FogvAzon,
                            border: [false, false, false, false],
                            alignment: 'center',
                          },
                        ],
                      ],
                    },
                  },
                ],
              },
            ],
          };
        }),
      ],
      pageBreakBefore: function(
        currentNode,
        followingNodesOnPage,
        nodesOnNextPage,
        previousNodesOnPage
      ) {
        if (
          currentNode.id === 'NoBreak' &&
          previousNodesOnPage.length != 1 &&
          currentNode.pageNumbers.length != 1
        ) {
          return true;
        }
        return false;
      },
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();

    /********* Ezzel tudjuk egyből nyomtatóra küldeni ********/
    //pdfMake.createPdf(docDefinition).print();
  }

  async OsszefoglaloJelentesNyomtatas({ naplobejegyzesId, iktatasId }) {
    if (pdfMake.vfs == undefined) {
      pdfMake.vfs = pdfFonts.pdfMake.vfs;
    }

    // var model = await apiService.OsszefoglaloJelentesNyomtatasByFegyelmiUgyId({
    //   fegyelmiUgyId: fegyelmiUgyId,
    // });

    var model;

    if (naplobejegyzesId != null) {
      model = await apiService.OsszefoglaloJelentesNyomtatasByNaplobejegyzesId({
        naplobejegyzesId,
      });
    } else if (iktatasId != null) {
      model = await apiService.OsszefoglaloJelentesNyomtatasByIktatasId({
        iktatasId,
      });
    } else return null;

    var docDefinition = {
      footer: function(currentPage, pageCount) {
        return {
          margin: [40, 20, 40, 20],
          text: pageCount >= 2 ? currentPage + '. oldal' : '',
          opacity: 0.5,
          margin: [0, 10, 0, 10],
          alignment: 'center',
          fontSize: 10,
        };
      },
      content: [
        {
          image:
            'data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDODQ1MkJGRkUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDODQ1MkMwMEUzN0UxMUU5QTNFOUUzMkYyMjNGNUMyMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkM4NDUyQkZERTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkM4NDUyQkZFRTM3RTExRTlBM0U5RTMyRjIyM0Y1QzIxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQAAQEBAQEBAQEBAQEBAQEB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',
          alignment: 'center',
          width: 30,
          height: 55,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.IntezetNev,
          alignment: 'center',
          fontSize: 12,
          opacity: 0.5,
          margin: [0, 0, 0, 10],
        },
        {
          text: model.Iktatoszam,
          alignment: 'right',
          fontSize: 10,
        },
        model.Naplok.map(function(item, index, currentPage) {
          var fejlec = htmlToPdfMake(
            `
          <div style="text-align: justify;">` +
              item.Fejlec +
              `</div>
              `
          );
          var jegyzokonyvTartalma = htmlToPdfMake(
            `
          <div style="text-align: justify;">` +
              item.JegyzokonyvTartalma +
              `</div>
              `
          );
          return {
            stack: [
              // {
              //   widths: ['auto', 'auto'],
              //   style: 'tableExample',
              //   table: {
              //     body: [
              //       [
              //         {
              //           margin: [0, 0, 0, 0],
              //           text: model.IntezetNev,
              //           opacity: 0.5,
              //           border: [false, false, false, false],
              //         },
              //         {
              //           margin: [0, 0, 0, 0],
              //           text: model.Iktatoszam,
              //           opacity: 0.5,
              //           border: [false, false, false, false],
              //           alignment: 'right',
              //         },
              //       ],
              //       [
              //         {
              //           border: [false, false, false, false],
              //           colSpan: 2,
              //           margin: [0, 0, 0, 0],
              //           canvas: [
              //             {
              //               type: 'line',
              //               x1: 0,
              //               y1: 0,
              //               x2: 505,
              //               y2: 0,
              //               lineWidth: 0.5,
              //               opacity: 0.5,
              //             },
              //           ],
              //         },
              //       ],
              //     ],
              //   },
              // },
              {
                text:
                  index == 0
                    ? model.Ugyszam + ' számú ügy összefoglaló jelentése'
                    : '',
                alignment: 'center',
                fontSize: 14,
                bold: true,
                decoration: 'underline',
                margin: [0, 15, 0, 15],
              },
              // {
              //   text: '',
              //   fontSize: 12,
              //   bold: true,
              //   margin: [0, 10, 0, 4],
              // },
              fejlec,
              // {
              //   text: '',
              //   fontSize: 12,
              //   bold: true,
              //   margin: [0, 10, 0, 4],
              // },
              {
                border: [false, false, false, false],
                colSpan: 2,
                margin: [0, 0, 0, 0],
                canvas: [
                  {
                    type: 'line',
                    x1: 0,
                    y1: 0,
                    x2: 509,
                    y2: 0,
                    lineWidth: 0.5,
                    strokeOpacity: 0.5,
                  },
                ],
              },

              //  {
              //    text: item.JegyzokonyvTartalma,
              //    fontSize: 12,
              //    margin: [10, 5, 0, 4],
              //  },
              jegyzokonyvTartalma,
              // {
              //   border: [false, false, false, false],
              //   colSpan: 2,
              //   margin: [0, 0, 0, 0],
              //   canvas: [
              //     {
              //       type: 'line',
              //       x1: 0,
              //       y1: 0,
              //       x2: 505,
              //       y2: 0,
              //       lineWidth: 0.5,
              //       opacity: 0.5,
              //     },
              //   ],
              // },
            ],
          };
        }),
      ],
      pageBreakBefore: function(
        currentNode,
        followingNodesOnPage,
        nodesOnNextPage,
        previousNodesOnPage
      ) {
        if (
          currentNode.id === 'NoBreak' &&
          currentNode.pageNumbers.length != 1
        ) {
          return true;
        }
        return false;
      },
      pageSize: 'A4',
      pageMargins: [40, 20, 40, 40],
      styles: {
        header: {
          fontSize: 16,
          bold: true,
          alignment: 'center',
          margin: [0, 20, 0, 0],
        },
        subheader: {
          fontSize: 15,
          bold: true,
        },
        quote: {
          italics: true,
        },
        small: {
          fontSize: 8,
        },
        footersmall: {
          fontSize: 6,
        },
        tableExample: {
          margin: [-5, 30, 0, 15],
        },
        tableHeader: {
          bold: true,
          fontSize: 8,
          margin: [0, 10, 5, 10],
        },
        tableHeader2: {
          bold: true,
          fontSize: 8,
          alignment: 'center',
          margin: [0, 10, 0, 10],
        },
        tableCell: {
          fontSize: 8,
          alignment: 'right',
          margin: [0, 5, 5, 5],
        },
      },
      defaultStyle: {
        columnGap: 20,
        font: 'TimesNewRoman',
        fontSize: 10.5,
      },
    };

    pdfMake.fonts = {
      TimesNewRoman: {
        normal: 'TimesNewRoman.ttf',
        bold: 'TimesNewRoman.ttf',
        italics: 'TimesNewRoman.ttf',
        bolditalics: 'TimesNewRoman.ttf',
      },
    };
    pdfMake.createPdf(docDefinition).download();
  }
}
export const NyomtatvanyFunctions = new Nyomtatvany();
