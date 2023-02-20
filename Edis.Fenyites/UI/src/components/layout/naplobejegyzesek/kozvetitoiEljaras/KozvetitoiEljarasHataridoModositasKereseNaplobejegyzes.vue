<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        A közvetítői eljárás felelőse határidő módosítási kéréssel élt.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12 mt-10">
            <div class="mb-10">
              <span
                v-html="naplobejegyzes.JegyzokonyvTartalma"
                class="font-size-12"
              ></span>
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
import { defaultToolTipOptions } from '../../../../plugins/bootstrapVue';
import { AppStoreTypes } from '../../../../store/modules/app';
import { mapGetters } from 'vuex';
import moment from 'moment';

export default {
  name: 'kozvetitoi-eljaras-hatarido-modositas-kerese-naplobejegyzes',
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
