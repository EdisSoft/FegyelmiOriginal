<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        A magánelzárás meg lett szakítva
        <em>{{ naplobejegyzes.Hatarido | toDateTime }}-kor</em>.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12">
            <span
              v-if="naplobejegyzes.JegyzokonyvTartalma"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              >Áthelyezve: {{ naplobejegyzes.JegyzokonyvTartalma }}</span
            >
            <div>
              <em>
                <span
                  v-html="naplobejegyzes.Indoklas"
                  class="font-size-12"
                ></span
              ></em>
            </div>
          </div>
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
import { AppStoreTypes } from '../../../store/modules/app';
export default {
  name: 'maganelzaras-megszakitasa-naplobejegyzes',
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
