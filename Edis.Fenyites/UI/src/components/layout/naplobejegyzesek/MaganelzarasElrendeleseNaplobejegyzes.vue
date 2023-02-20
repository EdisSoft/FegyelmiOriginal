<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        <div class="row mx-0">
          <div class="col col-12 px-0 d-flex justify-content-between">
            <div>
              A magánelzárást elrendeltem. Kezdés:
              <em>
                {{
                  naplobejegyzes.ElkulonitesElrendelesIdeje
                    | toShortDateKeltezes
                }}</em
              >.
            </div>
          </div>
        </div>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div v-if="felugyelok" class="panel-body text-default pt-0">
        <div>
          <span
            v-for="felugyelo in felugyelok"
            v-bind:key="felugyelo.Id"
            class="badge badge-outline badge-dark mr-5 bg-white font-weight-400"
            v-b-tooltip="{
              title: 'Főfelügyelő',
              ...toolTipOptions,
            }"
            >{{ felugyelo | camelCaseString }}</span
          >
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
import { defaultToolTipOptions } from '../../../plugins/bootstrapVue';

export default {
  name: 'maganelzaras-elrendelese-naplobejegyzes',
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
    felugyelok() {
      if (!this.naplobejegyzes.Fofelugyelok) {
        return [];
      }
      var result = this.naplobejegyzes.Fofelugyelok.split(',');
      return result;
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
