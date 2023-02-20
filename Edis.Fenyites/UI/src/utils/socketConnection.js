import settings from '../data/settings';
import $ from 'jquery';
import 'signalr/jquery.signalR.min';
import store from '../store';
import { UserStoreTypes } from '../store/modules/user';

export const initSocket = function(actions, onInitCompleted) {
  if (global.hub && global.connection) {
    console.log('initSocket már inicializálva van');
    return;
  }
  try {
    var urlReplace = settings.baseUrl.substring(0, settings.baseUrl.length - 1);
    global.connection = $.hubConnection(urlReplace);

    global.hub = global.connection.createHubProxy('SystemEventsHub');
    actions.forEach(element => {
      global.hub.on(element.socketName, element.callback);
    });

    global.connection
      .start()
      .done(function(d) {
        console.log('SignalR connected', d);
        var userInfo = store.getters[UserStoreTypes.getters.getUserInfo];
        if (userInfo) {
          var intezetId =
            store.getters[UserStoreTypes.getters.getUserInfo].RogzitoIntezet.Id;
          let data = [global.connection.id, intezetId];
          sendToSocket('SetIntezetIdToUser', data);
        }
      })
      .fail(function(params) {
        console.log('SignalR failed connected, ', params);
      });
    global.connection.disconnected(function() {
      var disconnetedDate = new Date();
      console.log('SignalR disconnected');
      setTimeout(function() {
        global.connection.start().done(e => {
          sendToSocket('GetLegfrissebbFenyitesekByDatum', [disconnetedDate]);
          console.log('SignalR reconnected');
        });
      }, 1000);
    });
  } catch (error) {
    console.error('Nem lehetett elindítani a socket-et!', error);
  }
};

export function sendToSocket(action, data) {
  if (
    global.hub &&
    global.hub.connection.state == $.signalR.connectionState.connected
  ) {
    if (data) {
      console.log('sendToSocket()', action, ...data);
      global.hub.invoke(action, ...data);
    } else {
      global.hub.invoke(action);
    }
  }
}

export function GetSocketConnectionId() {
  if (!global.connection) {
    return null;
  }
  return global.connection.id;
}
global.InitSocket = initSocket;
