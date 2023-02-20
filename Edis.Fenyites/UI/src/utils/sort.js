import moment from 'moment';

export function sortStr(key) {
  return function(a, b) {
    return (a[key] + '').localeCompare(b[key] + '');
  };
}

export function sortStrDesc(key) {
  return function(a, b) {
    return (b[key] + '').localeCompare(a[key] + '');
  };
}
export function sortNumber(key) {
  return function(a, b) {
    return parseInt(a[key]) - parseInt(b[key]);
  };
}

export function sortNumberDesc(key) {
  return function(a, b) {
    return parseInt(b[key]) - parseInt(a[key]);
  };
}

export function sortDate(key) {
  return function(a, b) {
    return (
      new Date(moment(a[key])).getTime() - new Date(moment(b[key])).getTime()
    );
  };
}

export function sortDateDesc(key) {
  return function(a, b) {
    return (
      new Date(moment(b[key])).getTime() - new Date(moment(a[key])).getTime()
    );
  };
}
