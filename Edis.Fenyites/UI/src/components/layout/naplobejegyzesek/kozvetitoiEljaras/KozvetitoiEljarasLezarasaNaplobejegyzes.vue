<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 disabled"
        v-b-toggle="panelId"
      >
        A fegyelmi jogkör gyakorlója a közvetítői eljárást lezárta.
        <em>{{ eljarasEredmenye }}</em>
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
import { mapGetters } from 'vuex';

export default {
  name: 'kozvetitoi-eljaras-lezarasa-naplobejegyzes',
  data() {
    return {};
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
    eljarasEredmenye() {
      if (
        this.naplobejegyzes.GaranciaTeljesultFl &&
        this.naplobejegyzes.KozvetitoiSikeresFL
      )
        return 'Garancia teljesült, eredményes lezárás';
      else if (
        this.naplobejegyzes.GaranciaTeljesultFl &&
        !this.naplobejegyzes.KozvetitoiSikeresFL
      )
        return 'Garancia teljesült, de nem számít eredményesnek';
      else if (
        !this.naplobejegyzes.GaranciaTeljesultFl &&
        this.naplobejegyzes.KozvetitoiSikeresFL
      )
        return 'Garancia nem teljesült, de eredményesnek számít';
      else if (
        !this.naplobejegyzes.GaranciaTeljesultFl &&
        !this.naplobejegyzes.KozvetitoiSikeresFL
      )
        return 'Garancia nem teljesült, eredménytelen lezárás';
      return '';
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
