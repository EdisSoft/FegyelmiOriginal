import toastr from 'toastr';
import 'toastr/build/toastr.css';
import { jfkApp } from '../main';
import store from '../store';
import { AppStoreTypes } from '../store/modules/app';
import WarningLevel from '../data/enums/waningLevel';

/**
 * @type {ToastrOptions}
 */
toastr.options = {
  closeButton: false,
  debug: false,
  newestOnTop: false,
  progressBar: true,
  positionClass: 'toast-top-right',
  preventDuplicates: false,
  onclick: null,
  toastClass: 'toast-just-text',
  showDuration: 300,
  hideDuration: 1000,
  timeOut: 5000,
  extendedTimeOut: 1000,
  showEasing: 'swing',
  hideEasing: 'linear',
  showMethod: 'fadeIn',
  hideMethod: 'fadeOut',
};

class Notifications {
  InfoAlert(title, text) {
    toastr.info(text, title, { progressBar: true });
  }

  SuccessAlert(title, text) {
    toastr.success(text, title);
  }

  WarningAlert(title, text) {
    toastr.warning(text, title);
  }

  ErrorAlert(title, text) {
    toastr.error(text, title);
  }
  AjaxError({ title = 'Hiba', text = 'Hiba történt', errorObj }) {
    let warningLevel = WarningLevel.Error;

    if (errorObj.statusText == 'abort') {
      return true;
    }
    if (errorObj) {
      let responseJSON = errorObj.responseJSON;
      if (errorObj.jqXHR && errorObj.jqXHR.responseJSON) {
        responseJSON = errorObj.jqXHR.responseJSON;
      }

      if (
        responseJSON &&
        (responseJSON.serverWarning || responseJSON.serverError)
      ) {
        title = responseJSON.title || title;
        text = responseJSON.message || text;
        warningLevel = responseJSON.warningExceptionLevel;
      }
      if (responseJSON && responseJSON.validationError) {
        let hibak = responseJSON.errors.map(m => m.Text).join(', ');
        title = 'Hibás adatok';
        text = hibak || text;
      }
    }

    switch (warningLevel) {
      case WarningLevel.Warning:
        toastr.warning(text, title);
        break;
      case WarningLevel.Error:
        toastr.error(text, title);
        break;
      default:
        toastr.error(text, title);
        break;
    }
    return false;
  }
  async ConfirmModal(message, options = {}) {
    let ismSidePanelOpen =
      store.getters[AppStoreTypes.getters.isSlidePanelFegyelmiUgyReszletekOpen];
    let ismJutalmiSidePanelOpen =
      store.getters[AppStoreTypes.getters.isSlidePanelJutalmiUgyReszletekOpen];
    let modalClass = 'confirm-modal';
    if (ismSidePanelOpen || ismJutalmiSidePanelOpen) {
      modalClass += ' modal-left';
    }
    const messageHtml = jfkApp.$createElement('div', {
      domProps: { innerHTML: message },
    });
    return await jfkApp.$bvModal.msgBoxConfirm(messageHtml, {
      title: 'options.title',
      modalClass,
      size: 'lg',
      buttonSize: 'sm',
      okVariant: 'warning',
      okTitle: 'Igen',
      cancelTitle: 'Nem',
      headerClass: 'text-white',
      bodyClass: 'p-4',
      footerClass: 'py-2 px-25',
      hideHeaderClose: true,
      centered: true,
      ...options,
    });
  }
}
export const NotificationFunctions = new Notifications();
