<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 disabled"
        v-b-toggle="panelId"
      >
        <div class="row mx-0">
          <div class="col col-12 px-0 d-flex justify-content-between">
            <div>
              A magánelzárást végrehajtottuk, a fogvatartott visszakerült
              zárkára
              <em>{{ naplobejegyzes.Hatarido | toDateTime }}</em
              >-kor.
              <span v-if="naplobejegyzes.JegyzokonyvTartalma.length > 0"
                >Elhelyezés:
                <em>{{ naplobejegyzes.JegyzokonyvTartalma }}</em></span
              >
            </div>
          </div>
        </div>
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
import { AppStoreTypes } from '../../../store/modules/app';

export default {
  name: 'maganelzaras-vegrehajtva-naplobejegyzes',
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
