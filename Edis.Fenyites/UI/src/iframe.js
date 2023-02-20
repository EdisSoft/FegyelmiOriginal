import { createIframeListener } from './utils/iframe';
import store from './store';
import { AppStoreTypes } from './store/modules/app';
import $ from 'jquery';

createIframeListener(
  ['www.konasoft.hu'],
  [
    {
      eventName: 'n2020m-setN2020NotificationCounter',
      handler: e => {
        let number = e.data.value;
        if (isNaN(number)) {
          number = 0;
        } else {
          if (number > 99) {
            number = '+99';
          }
        }
        store.dispatch(AppStoreTypes.actions.setN2020NotificationCounter, {
          value: number,
        });
      },
    },
    {
      eventName: 'n2020m-imageLightbox',
      handler: e => {
        let number = e.data.value;
        if (isNaN(number)) {
          number = 0;
        } else {
          if (number > 99) {
            number = '+99';
          }
        }
        if (
          (number == 0 && $('#iframeInitContainer').is('.bigscreen')) ||
          (number > 0 && $('#iframeInitContainer').is('.bigscreen'))
        ) {
          return 0;
        } else if (number == 0) {
          $('#iframeInitContainer').toggleClass('bigscreen');
          // $('#iframeBody').attr(
          //   'style',
          //   'width: 100% !important; max-width: 100% !important'
          // );
          // $('#iframeInitContainer').attr(
          //   'style',
          //   'width: 50% !important; max-width: 50% !important'
          // );
          $('.panel-fullscreen').toggleClass('wb-expand wb-contract');
        } else {
          $('#iframeInitContainer').removeClass('bigscreen');
          $('.panel-fullscreen').toggleClass('wb-expand wb-contract');
        }
        // store.dispatch(AppStoreTypes.actions.setN2020NotificationCounter, {
        //   value: number,
        // });
      },
    },
    {
      eventName: 'teszt',
      handler: e => {
        console.log('teszt', e);
      },
    },
  ]
);
