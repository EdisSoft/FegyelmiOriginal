<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        <!-- TODO: frontend, nincs XD -->
        <!-- <em>{{ naplobejegyzes.RogzitoSzemely | camelCaseString }}</em> -->
        <em
          >{{ naplobejegyzes.RogzitoSzemely | camelCaseString }}
          <span v-if="naplobejegyzes.RogzitoSzemelyRendfokozat">
            {{ naplobejegyzes.RogzitoSzemelyRendfokozat }}</span
          ></em
        >
        a magánelzárás ellen pszichológiai és egészségügyi okokból
        ellenjavallattal élt. Végrehajthatóság, vagy felülvizsgálat dátuma:
        <em>{{ naplobejegyzes.Hatarido | toShortDate }}</em>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
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

export default {
  name: 'maganelzaras-ideiglenesen-ellenjavallt-naplobejegyzes',
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
