import Vue from 'vue';
import { ClientTable } from 'vue-tables-2';

Vue.use(
  ClientTable,
  {
    debounce: 10,
    sortIcon: {
      base: 'icon',
      up: 'wb-sort-asc',
      down: 'wb-sort-des',
      is: 'wb-sort-vertical',
    },
    texts: {
      count:
        'Találatok: {from} - {to} Összesen: {count} |Találatok: {count}|1 találat',
      first: 'Első',
      last: 'Utolsó',
      filter: 'Keresés:',
      filterPlaceholder: '',
      limit: 'Találatok:',
      page: 'Oldal:',
      noResults: 'Nincs találat',
      filterBy: 'Szűrve: {column}',
      loading: 'Betöltés...',
      defaultOption: '{column}',
      columns: 'Fejlécek',
    },
    datepickerOptions: {
      locale: {
        cancelLabel: 'Törlés',
      },
    },
    perPageValues: [10, 25, 50],
    filterable: true,
  },
  false,
  'bootstrap4',
  'footerPagination'
);
