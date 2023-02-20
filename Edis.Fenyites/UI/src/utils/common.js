import moment from 'moment';
import $ from 'jquery';
import { extensionRegEx } from './regexPatterns';
import store from '../store';
import { FegyelmiUgyStoreTypes } from '../store/modules/fegyelmiugy';

export function camelCaseString(str) {
  if (!str) return '';
  return str
    .toLowerCase()
    .split(' ')
    .map(word => word.charAt(0).toUpperCase() + word.slice(1))
    .join(' ');
}

export function camelCaseNameOnly(str) {
  if (!str) return '';
  var strSplit = str.split(' - ');
  var azon = strSplit[0];
  console.log(azon);
  var nev = strSplit[1];
  console.log(nev);
  return (
    azon +
    ' - ' +
    nev
      .toLowerCase()
      .split(' ')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ')
  );
}

export function groupBy(array, key) {
  const result = {};
  array.forEach(item => {
    if (!result[item[key]]) {
      result[item[key]] = [];
    }
    result[item[key]].push(item);
  });

  return result;
}

export function removeHtmlFromString(str) {
  if (!str) {
    return '';
  }
  var pernCsere = /<br\s*[/]?>\n?/gi;
  var brCsere = /\n/gm;
  //var htmlStyleRegex = /<style([\s\S]*?)<\/style>/gi;
  var htmlStyleRegex = /<(?:.)*?>/gm;

  return str
    .replace(/&nbsp;/g, ' ')
    .replace(pernCsere, '\n')
    .replace(htmlStyleRegex, '')
    .replace(/\n+/g, '\n')
    .replace(brCsere, '<br>');
}
export function removeAllHtmlFromString(str) {
  if (!str) {
    return '';
  }
  var pernCsere = /<br\s*[/]?>\n?/gi;
  var brCsere = /\n/gm;
  //var htmlStyleRegex = /<style([\s\S]*?)<\/style>/gi;
  var htmlStyleRegex = /<(?:.)*?>/gm;

  return str
    .replace(/&nbsp;/g, ' ')
    .replace(pernCsere, '\n')
    .replace(htmlStyleRegex, '')
    .replace(/\n+/g, '\n');
}

export function removeHtmlNewLinesFromString(str) {
  if (!str) {
    return '';
  }
  var pernCsere = /<br\s*[/]?>\n?/gi;
  var brCsere = /\n/gm;

  return str.replace(pernCsere, ' ').replace(brCsere, ' ');
}

export function safeGetProp(object, path) {
  const pathArray = path.split('.');
  return pathArray.reduce(function(xs, x) {
    return xs && xs[x] ? xs[x] : null;
  }, object);
}

export function getKeyFromLastCommentsGroup(datumStr) {
  var now = new Date();
  var datum = new Date(moment(datumStr));
  var yesterday = new Date(now.setTime(now.getTime() - 1 * 86400000));
  var beforeYesterday = new Date(now.setTime(now.getTime() - 1 * 86400000));
  if (datum.getTime() > yesterday.getTime()) {
    return 'Ma';
  } else if (datum.getTime() > beforeYesterday.getTime()) {
    return 'Tegnap';
  } else {
    return moment(datum).format('YYYY.MM.DD.');
  }
}

export function freezeArray(arr) {
  let tmp = arr.slice();
  let result = tmp.map(m => {
    return Object.freeze({ ...m });
  });
  return result;
}

export function timeout(ms) {
  return new Promise(res => setTimeout(res, ms));
}

export function formatNumber(numStr, separator = ' ') {
  return (numStr + '').replace(/(.)(?=(\d{3})+$)/g, '$1' + separator);
}

export function enumDocumentation(data) {
  var r = '';
  for (var s in data) {
    r += '/** ' + data[s] + '*/\n';
    r += s + ':' + data[s] + ',\n';
  }
  console.log(r);
}

export function fileDownload(url) {
  return $.get(url).then(response => {
    var bytes = [];
    var raw = window.atob(response.File);
    for (var i = 0; i < raw.length; i++) {
      bytes.push(raw.charCodeAt(i));
    }

    var data = new Uint8Array(bytes);
    const blob = new Blob([data], { type: response.MimeType });
    let link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = response.FileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    return response;
  });
}

export function getAttachmentIcon(filename) {
  var ext = extensionRegEx.exec(filename)[1].toLowerCase();
  var classname = '';
  switch (ext) {
    case 'doc':
    case 'docx':
      classname += 'doc';
      break;
    case 'xls':
    case 'xlsx':
      classname += 'xls';
      break;
    case 'ppt':
      classname += 'ppt';
      break;
    case 'pdf':
      classname += 'pdf';
      break;
    case 'jpg':
      classname += 'jpg';
      break;
    case 'eps':
      classname += 'eps';
      break;
    case 'psd':
      classname += 'psd';
      break;
    case 'png':
      classname += 'png';
      break;
    case 'zip':
      classname += 'zip';
      break;
    case 'avi':
      classname += 'avi';
      break;
    case 'htm':
    case 'html':
      classname += 'html';
      break;
    case 'css':
      classname += 'css';
      break;
    case 'mp3':
      classname += 'mp3';
      break;
    case 'txt':
      classname += 'txt';
      break;
    case 'mov':
      classname += 'mov';
      break;
    case 'wav':
      classname += 'wav';
      break;
    case 'dll':
      classname += 'dll';
      break;
    default:
      classname += 'unknown';
      break;
  }
  return require('../assets/images/alkalmazasIkonok/alkalmazasok_' +
    classname +
    '.png');
}

export function deselectDatatable(id) {
  store.dispatch(FegyelmiUgyStoreTypes.actions.setFegyelmiUgyekSelected, {
    value: [],
  });
}

export function selectDatatable(ids) {
  console.log(ids);
  store.dispatch(FegyelmiUgyStoreTypes.actions.setFegyelmiUgyekSelected, {
    value: ids,
  });
}

export function stringToBoolean(string) {
  if (!string) return false;
  switch (string.toLowerCase().trim()) {
    case 'true':
    case '1':
      return true;
    case 'false':
    case '0':
      return false;
    default:
      return Boolean(string);
  }
}

export function CorrectSummerAndWinterTimePeriod(activityDate) {
  var d = activityDate;
  d.setTime(d.getTime() - new Date().getTimezoneOffset() * 60 * 1000);
  return d;
}

// We need to get the longitude/latitude for every activity
export async function GetGeoLocation() {
  return new Promise((resolve, reject) => {
    navigator.geolocation.getCurrentPosition(
      position => {
        resolve(position.coords);
      },
      error => {
        reject(error);
      }
    );
  }).catch(error => console.log(error));
}
export function excelExportCellBool(value) {
  if (value) {
    return 'Igen';
  } else {
    return 'Nem';
  }
}
global.KonaDevTools = {};
global.KonaDevTools.enumDocumentation = enumDocumentation;
