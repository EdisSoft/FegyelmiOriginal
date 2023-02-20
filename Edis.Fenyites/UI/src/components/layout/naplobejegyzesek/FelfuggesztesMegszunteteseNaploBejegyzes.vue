<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 disabled"
        v-b-toggle="panelId"
      >
        <span v-if="naplobejegyzes.AutomatikusFl == true">
          A rendszer
          <em>{{ naplobejegyzes.LetrehozasDatuma | toShortDateKeltezes }}</em>
          automatikusan megszüntette a felfüggesztést.
        </span>
        <span v-else>
          <em>{{ naplobejegyzes.RogzitoSzemely | camelCaseString }}</em> a
          felfüggesztést megszüntette.
        </span>
        Új határidő:
        <em>{{ naplobejegyzes.Hatarido | toShortDate }}</em>
        <!-- <em>{{ naplobejegyzes.LetrehozasDatuma | toShortDateKeltezes }}</em> -->
        <span v-if="naplobejegyzes.KivizsgaloSzemely">
          Kivizsgaló:
          <em>{{ naplobejegyzes.KivizsgaloSzemely | camelCaseString }}</em
          >.
        </span>
        <span v-else></span>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()"> </b-collapse>
    <div class="text-right pl-20 pb-0">
      <small v-if="naplobejegyzes.AutomatikusFl == true" class="blue-grey-400">
        {{ naplobejegyzes.TipusCimke }} ·
        {{ naplobejegyzes.RogzitoSzemely | camelCaseString }} ·
        {{ naplobejegyzes.LetrehozasDatuma | toDateTime }}
      </small>
      <small v-else class="blue-grey-400">
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
  name: 'felfuggesztes-megszuntetese-naplobejegyzes',
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
