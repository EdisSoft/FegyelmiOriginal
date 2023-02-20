import { FenyitesStoreTypes } from './store/modules/fenyites';
import { apiService, eventBus } from './main';
import { initSocket } from './utils/socketConnection';
import store from './store';
import { FegyelmiUgyStoreTypes } from './store/modules/fegyelmiugy';
import { UserStoreTypes } from './store/modules/user';
import { JutalomUgyStoreTypes } from './store/modules/jutalomugy';

initSocket([
  // Fenyítések - Kifutó ügyek
   {
     socketName: 'fegyelmiUgyValtozasFany',
     callback: function(ids) {
       console.log(' ids', ids);
       apiService.GetFenyitesekByIds({ ids });
     },
   },
   {
     socketName: 'modifyFegyelmiUgyOnRecconectFany',
     callback: function(obj) {
       console.log('modifyFegyelmiUgyOnRecconectFany obj', obj);
       store.dispatch(FenyitesStoreTypes.actions.addFenyitesek, {
         value: Object.freeze(obj),
       });
     },
   },
   {
     socketName: 'fegyelmiUgyMegszunesFany',
     callback: function(ids) {
       console.log('fegyelmiUgyMegszunesFany ids', ids);
       store.dispatch(FenyitesStoreTypes.actions.removeFenyitesek, {
         value: ids,
       });
     },
   },
   {
     socketName: 'refreshFenyitesekListAfterServerRRFany',
     callback: function() {
       console.log('refreshFenyitesekListAfterServerRRFany');
       apiService.GetFenyitesek();
     },
   },

  // Fegyelmi ügyek

  {
    socketName: 'fegyelmiUgyValtozas',
    callback: function(obj) {
      console.log('fegyelmiUgyValtozas objs', obj);
      store.dispatch(FegyelmiUgyStoreTypes.actions.addFegyelmiUgyek, {
        value: Object.freeze(obj),
      });
    },
  },
  {
    socketName: 'modifyFegyelmiUgyOnRecconect',
    callback: function(obj) {
      console.log('modifyFegyelmiUgyOnRecconect obj', obj);
      store.dispatch(FegyelmiUgyStoreTypes.actions.modifyFegyelmiUgyek, {
        value: Object.freeze(obj),
      });
    },
  },
  {
    socketName: 'fegyelmiUgyMegszunes',
    callback: function(ids) {
      console.log('fegyelmiUgyMegszunes ids', ids);
      store.dispatch(FegyelmiUgyStoreTypes.actions.removeFegyelmiUgyek, {
        value: ids,
      });
    },
  },
  {
    socketName: 'refreshFenyitesekListAfterServerRR',
    callback: function() {
      console.log('refreshFenyitesekListAfterServerRR');
      eventBus.$emit('IntezetValasztas');
    },
  },
  {
    socketName: 'refreshAfterInetezetValtas',
    callback: async function(intezetId) {
      if (
        store.getters[UserStoreTypes.getters.getUserInfo] &&
        store.getters[UserStoreTypes.getters.getUserInfo].RogzitoIntezet.Id !=
          intezetId
      ) {
        console.log(
          'IntezetValtas ahol voltunk:',
          store.getters[UserStoreTypes.getters.getUserInfo].RogzitoIntezet.Id
        );
        console.log('IntezetValtas ahova megyünk: ', intezetId);
        let result = await apiService.IntezetValtas({
          intezetId: intezetId,
          mock: true,
        });
      }
      console.log('refreshAfterInetezetValtas');
      eventBus.$emit('IntezetValasztas');
    },
  },
  //Aktivitás hírfolyam

  {
    socketName: 'aktivitasPanelFrissites',
    callback: function(obj) {
      console.log('aktivitasPanelFrissites objs', obj);
      store.dispatch(FegyelmiUgyStoreTypes.actions.addAktivitasFolyam, {
        value: Object.freeze(obj),
      });
    },
  },

  // Jutalom ügyek

  {
    socketName: 'jutalomUgyValtozas',
    callback: function(obj) {
      console.log('jutalomUgyValtozas objs', obj);
      store.dispatch(JutalomUgyStoreTypes.actions.addJutalomUgyek, {
        value: Object.freeze(obj),
      });
    },
  },
  {
    socketName: 'modifyJutalomUgyOnRecconect',
    callback: function(obj) {
      console.log('modifyJutalomUgyOnRecconect obj', obj);
      store.dispatch(JutalomUgyStoreTypes.actions.modifyJutalomUgyek, {
        value: Object.freeze(obj),
      });
    },
  },
  {
    socketName: 'jutalomUgyMegszunes',
    callback: function(ids) {
      console.log('fjutalomUgyMegszunes ids', ids);
      store.dispatch(JutalomUgyStoreTypes.actions.removeJutalomUgyek, {
        value: ids,
      });
    },
  },

  // Debug
  {
    socketName: 'cheers',
    callback: function(message) {
      console.log(message);
    },
  },
]);
