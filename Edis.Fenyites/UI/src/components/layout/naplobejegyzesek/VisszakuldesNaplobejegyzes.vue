<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10"
        :class="{
          disabled: !naplobejegyzes.JegyzokonyvTartalma,
          pointer: naplobejegyzes.JegyzokonyvTartalma,
        }"
        v-b-toggle="panelId"
      >
        <span v-if="isVisszakuldes">
          Az ügy "felfüggesztés miatt" visszaküldésre került a fegyelmi jogkör
          gyakorlójának.
        </span>
        <span v-else>
          Az ügy visszaküldésre került a fegyelmi jogkör gyakorlójának.
          {{ naplobejegyzes.VisszakuldesOkaCimke }}.
        </span>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div
        class="panel-body text-default pt-0"
        v-if="naplobejegyzes.JegyzokonyvTartalma"
      >
        <div class="mb-10">
          <span
            v-html="naplobejegyzes.JegyzokonyvTartalma"
            class="font-size-12"
          ></span>
        </div>
      </div>
    </b-collapse>
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
import { mapGetters } from 'vuex';
import { defaultToolTipOptions } from '../../../plugins/bootstrapVue';
import Cimkek from '../../../data/enums/Cimkek';

export default {
  name: 'visszakuldes-naplobejegyzes',
  data() {
    return {
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#slidepanel-fegyelmi-ugy',
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
    ...mapGetters({}),
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },

    isVisszakuldes() {
      if (
        this.naplobejegyzes.VisszakuldesOkaCimkeId ==
        Cimkek.VisszakuldesOka.JogiKepviseletetKer
      )
        return true;
      else return false;
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
