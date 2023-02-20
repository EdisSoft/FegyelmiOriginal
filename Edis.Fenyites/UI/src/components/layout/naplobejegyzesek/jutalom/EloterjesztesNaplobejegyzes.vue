<template>
  <div class="panel" v-if="jutalomUgy && naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10"
        v-b-toggle="panelId"
      >
        Előterjesztés&nbsp;<em>{{
          naplobejegyzes.LetrehozasDatuma | toDateTime
        }}</em
        >-kor <em>{{ naplobejegyzes.NaploJSon.JutalmazasOka }}</em
        >&nbsp;okból.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div
        class="panel-body text-default pt-0"
        v-if="naplobejegyzes.NaploJSon.Indoklas"
      >
        <div class="row mb-10">
          <em>{{ naplobejegyzes.NaploJSon.JavaslatTevo.Nev }}</em
          >&nbsp;javaslatára.
        </div>
        <div class="row">
          <div class="col-12 mt-5">
            <span
              v-html="naplobejegyzes.NaploJSon.Indoklas"
              class="font-size-12 font-italic"
            ></span>
          </div>
        </div>
      </div>
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

export default {
  name: 'eloterjesztes-naplobejegyzes',
  data() {
    return {
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#slidepanel-jutalmi-ugy',
      },
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
      return 'Előterjesztés';
    },
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
    jutalomUgy: {
      type: Object,
    },
    naplobejegyzes: {
      type: Object,
    },
  },
};
</script>
