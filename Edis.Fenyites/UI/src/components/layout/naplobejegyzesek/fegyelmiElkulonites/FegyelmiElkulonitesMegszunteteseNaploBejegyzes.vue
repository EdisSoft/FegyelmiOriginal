<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 disabled"
        v-b-toggle="panelId"
      >
        Elkülönítés megszüntetésének elrendelése
        <em>{{ mockNaplo.ElkMegszuntetese | toDateTime }}</em
        >-kor.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()"> </b-collapse>
    <div class="text-right pl-20 pb-0">
      <small class="blue-grey-400">
        {{ naplobejegyzes.TipusCimke }} ·
        {{ naplobejegyzes.RogzitoSzemely | camelCaseString }}
        {{ naplobejegyzes.RogzitoSzemelyRendfokozat }} ·
        {{ naplobejegyzes.LetrehozasDatuma | toDateTime }}
      </small>
    </div>
  </div>
</template>
<script>
import { defaultToolTipOptions } from '../../../../plugins/bootstrapVue';
import { AppStoreTypes } from '../../../../store/modules/app';
import { mapGetters } from 'vuex';
import moment from 'moment';

export default {
  name: 'fegyelmi-elkulonites-megszuntetese-naplo-bejegyzes',
  data() {
    return {
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#slidepanel-fegyelmi-ugy',
      },
      //TODO: backend, napló kitöltés, ennek átvezetése és törlése
      mockNaplo: {
        ElkMegszuntetese: moment().toISOString(),
      },
    };
  },
  mounted() {},
  created() {},
  methods: {
    NaploCollapseClick() {
    },
  },
  computed: {
    ...mapGetters({
      isModalOpen: AppStoreTypes.getters.isModalOpen,
    }),
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },
  },
  watch: {},
  components: {},
  props: {
    fegyelmiUgy: {
      type: Object,
    },
    naplobejegyzes: {
      type: Object,
    },
    esemeny: {
      type: Object,
    },
  },
};
</script>
