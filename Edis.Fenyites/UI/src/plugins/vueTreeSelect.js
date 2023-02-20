import Vue from 'vue';
import Treeselect from '@riophae/vue-treeselect';
import '@riophae/vue-treeselect/dist/vue-treeselect.css';
Vue.component('treeselect', Treeselect);
export const treeselectHun = {
  clearAllText: 'Törlés',
  clearValueText: 'Törlés',
  limitText: count => 'és ${count} további',
  loadingText: 'Betöltés...',
  noChildrenText: 'Nincsenek választható elemek',
  noOptionsText: 'Nincsenek választható elemek',
  noResultsText: 'Nincs találat',
  retryText: 'Újratöltés',
  searchPromptText: 'Kezdjen el gépelni',
};
