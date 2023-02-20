<template>
  <div class="panel" v-if="jutalomUgy && naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10"
        v-b-toggle="panelId"
      >
        <span
          v-html="
            naplobejegyzes.NaploJSon.JutalomTipusa.Id == 12
              ? 'Javaslom az előterjesztés elutasítását.'
              : `Javaslom <em>${naplobejegyzes.NaploJSon.JutalomTipusa.Nev}</em> jutalomban részesíteni.`
          "
        ></span>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div
        class="panel-body text-default pt-0"
        v-if="
          naplobejegyzes.NaploJSon.JutalomHossz != null ||
            naplobejegyzes.NaploJSon.SzemSzuksFordOsszegNoveles != null
        "
      >
        <div
          class="row mb-10"
          v-if="naplobejegyzes.NaploJSon.JutalomHossz != null"
        >
          <p>
            Jutalom tartama: {{ naplobejegyzes.NaploJSon.JutalomHossz.Nev }}
          </p>
        </div>
        <div
          class="row mb-10"
          v-if="naplobejegyzes.NaploJSon.SzemSzuksFordOsszegNoveles != null"
        >
          <p>
            Növelés mértéke:
            {{ naplobejegyzes.NaploJSon.SzemSzuksFordOsszegNoveles }}%
          </p>
        </div>
      </div>
      <div
        class="panel-body text-default pt-0"
        v-if="naplobejegyzes.NaploJSon.ToroltFegyelmiUgy"
      >
        <div class="row">
          A fegyelmi ügy azonosítója:
          {{ naplobejegyzes.NaploJSon.ToroltFegyelmiUgy.Nev }}
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
  name: 'jutalom-javaslat-naplobejegyzes',
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
      return 'Javaslat';
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
