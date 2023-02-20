<template>
  <div class="panel" v-if="jutalomUgy && naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 "
        v-b-toggle="panelId"
      >
        {{ title }}
      </a>
    </div>
    <b-collapse
      :id="panelId"
      :visible="velemenyVisible"
      @show="NaploCollapseClick()"
    >
      <div
        class="panel-body text-default pt-0"
        v-html="naplobejegyzesTartalma"
      ></div>
    </b-collapse>
    <div class="text-right pl-20 pb-0">
      <small class="blue-grey-400">
        {{ TipusCimke }} ·
        {{ naplobejegyzes.RogzitoSzemely | camelCaseString }}
        {{ naplobejegyzes.RogzitoSzemelyRendfokozat }} ·
        {{ naplobejegyzes.LetrehozasDatuma | toDateTime }}
      </small>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import { defaultToolTipOptions } from '../../../../plugins/bootstrapVue';
import Cimkek from '../../../../data/enums/Cimkek';
import { removeAllHtmlFromString } from '../../../../utils/common';

export default {
  name: 'jutalom-velemeny-naplobejegyzes',
  data() {
    return {
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#slidepanel-jutalmi-ugy',
      },
      visible: false,
    };
  },
  mounted() {},
  created() {},
  methods: {
    NaploCollapseClick() {
      // korábbi analitika helye
    },
  },
  computed: {
    ...mapGetters({}),
    TipusCimke() {
      return 'Vélemény';
    },
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },
    naplobejegyzesTartalma() {
      if (!this.naplobejegyzes) {
        return '';
      }
      return this.naplobejegyzes.JegyzokonyvTartalma;
    },
    title() {
      let title = removeAllHtmlFromString(this.naplobejegyzesTartalma).slice(
        0,
        97
      );
      if (title.length == 97) {
        title += '...';
      }
      return title;
    },
    velemenyVisible() {
      if (this.jutalomUgy.Id < 0) {
        return true;
      } else {
        return false;
      }
    },
    // isVisszakuldes() {
    //   if (
    //     this.naplobejegyzes.VisszakuldesOkaCimkeId ==
    //     Cimkek.VisszakuldesOka.JogiKepviseletetKer
    //   )
    //     return true;
    //   else return false;
    // },
  },
  watch: {},
  components: {},
  props: {
    jutalomUgy: {
      type: Object,
    },
    naplobejegyzes: {
      type: Object,
    },
  },
};
</script>
