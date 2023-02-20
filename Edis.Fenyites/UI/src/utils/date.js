import moment from 'moment';

export function addDays(date, days) {
  let momentDate = moment(date);
  if (!date || !momentDate.isValid()) return '';

  return momentDate.add(days, 'days');
}

export function dateAdd(date, typeStr, days) {
  let momentDate = moment(date);
  if (!date || !momentDate.isValid()) return '';

  return momentDate.add(days, typeStr).toDate();
}

export function dateSubtract(date, typeStr, days) {
  let momentDate = moment(date);
  if (!date || !momentDate.isValid()) return '';

  return momentDate.subtract(days, typeStr).toDate();
}

export function getWeekDates(date) {
  var weekDates = [];
  for (var i = 1; i <= 7; i++) {
    weekDates.push(moment(date).isoWeekday(i));
  }
  return weekDates;
}

/**
 *
 * @param {Date} d1
 * @param {Date} d2
 */
export function dateSameDay(d1, d2) {
  if (!d1 || !d2) {
    return false;
  }
  return (
    d1.getFullYear() === d2.getFullYear() &&
    d1.getMonth() === d2.getMonth() &&
    d1.getDate() === d2.getDate()
  );
}
/**
 *
 * @param {Date} from
 * @param {Date} end
 * @param {Date} d
 */
export function dateBetween(from, end, d) {
  if (!d) {
    return false;
  }
  if (!from) {
    from = d;
  }
  if (!end) {
    end = d;
  }
  return from.getTime() <= d.getTime() && end.getTime() >= d.getTime();
}

export function isSameWeek(d1, d2) {
  return getWeekNumber(d1) == getWeekNumber(d2);
}
export function getWeekNumber(d) {
  // Copy date so don't modify original
  d = new Date(Date.UTC(d.getFullYear(), d.getMonth(), d.getDate()));
  // Set to nearest Thursday: current date + 4 - current day number
  // Make Sunday's day number 7
  d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay() || 7));
  // Get first day of year
  var yearStart = new Date(Date.UTC(d.getUTCFullYear(), 0, 1));
  // Calculate full weeks to nearest Thursday
  var weekNo = Math.ceil(((d - yearStart) / 86400000 + 1) / 7);
  // Return array of year and week number
  return weekNo;
}

export function dateDiffInDays(date1, date2) {
  var a = moment(date1).startOf('day');
  var b = moment(date2).startOf('day');
  var diff = b.diff(a, 'days');
  return diff;
}
