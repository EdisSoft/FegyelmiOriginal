import $ from 'jquery';
import HttpStatusCode from '../data/enums/httpStatusCode';

import router from '../router';

class HttpFunctions {
  requestCache = [];

  /**
   *
   * @param {Object} payload
   * @param {String} payload.url
   * @param {Object} payload.data
   * @param {Object} payload.options
   * @param {Boolean} payload.options.fileUpload
   * @param {String}  payload.options.dataType
   */
  post({ url, data, options }) {
    // Cancel previous call
    let previousRequest = this.requestCache.find(f => f.url == url);
    if (previousRequest) {
      if (previousRequest.promise.abort) {
        previousRequest.promise.abort();
      }
    }
    let fileUpload = options && options.fileUpload;
    let dataType = options && options.dataType;
    let promise = $.ajax({
      url: url,
      type: 'POST',
      dataType: dataType || 'json',
      global: false,
      processData: !fileUpload,
      contentType: fileUpload
        ? false
        : 'application/x-www-form-urlencoded; charset=UTF-8',
      data: data,
      xhrFields: {
        withCredentials: true,
      },
    }).fail((jqXHR, textStatus, errorThrown) => {
      if (jqXHR.status == HttpStatusCode.FORBIDDEN) {
        router.push('/nopermission/');
      }
      if (jqXHR.status == HttpStatusCode.UNAUTHORIZED) {
      }
    });
    if (!previousRequest) {
      this.requestCache.push({ url, promise });
    } else {
      previousRequest.promise = promise;
    }
    return promise;
  }
}
export default new HttpFunctions();
