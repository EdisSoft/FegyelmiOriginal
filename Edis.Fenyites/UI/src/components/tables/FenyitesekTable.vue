<template>
  <div id="fenyitesek-table-container">
    <v-client-table :data="rows" :columns="columns" :options="options">
      <div slot="fogvAdatok" slot-scope="props">
        <span> {{ props.row.SzuletesiNev }}</span
        ><br />
        <span class="badge badge-outline badge-default">
          {{ props.row.AktNyilvantartasiSzam }}</span
        >
        <span class="badge badge-outline badge-warning"
          >{{ props.row.NyilvantartottStatusz }}
        </span>
        <span class="badge badge-outline badge-info">
          {{ props.row.Elhelyezes }}
        </span>
        <span class="badge badge-outline badge-success">
          {{ props.row.UgyIntezet }}
        </span>
      </div>
      <div slot="ugyadatok" slot-scope="props">
        {{ props.row.UtolsoModositasDatum | toShortDate }}
        <span class="badge badge-outline badge-primary"
          >{{ props.row.UgyIntezetAzon }} / {{ props.row.UgySzama }} /
          {{ props.row.UgyEve }}</span
        ><span class="badge badge-outline badge-info">
          {{ props.row.KezdemenyezesDatum | toShortDate }}</span
        ><br /><span class="badge badge-outline badge-warning"
          >Hat: ' +
          {{ props.row.HataridoDatum | toShortDate }}
        </span>
        <span class="badge badge-outline badge-default">
          {{ props.row.FenyitesStatusz }}
        </span>
      </div>
      <div slot="leiras" slot-scope="props">
        {{ props.row.FegyelmiUgyId }}
        {{ props.row.UgyIndoklas }}
      </div>
    </v-client-table>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  name: 'fenyitesek-table',
  data() {
    return {
      columns: ['fogvAdatok', 'ugyadatok', 'leiras'],
      options: {
        headings: {
          fogvAdatok: 'Fogvatartott Adatok',
          ugyadatok: 'Ügyadatok',
          leiras: 'Fegyelmi leírás',
        },
        sortable: ['fogvAdatok', 'ugyadatok', 'leiras'],
        filterable: [
          'AktNyilvantartasiSzam',
          'NyilvantartottStatusz',
          'FenyitesStatusz',
          'SzuletesiNev',
        ],
        uniqueKey: 'FegyelmiUgyId',
      },
    };
  },
  mounted() {},
  created() {},
  methods: {},
  computed: {
    ...mapGetters({}),
  },
  watch: {},
  components: {},
  props: {
    rows: {
      type: Array,
      required: true,
    },
  },
};
</script>
