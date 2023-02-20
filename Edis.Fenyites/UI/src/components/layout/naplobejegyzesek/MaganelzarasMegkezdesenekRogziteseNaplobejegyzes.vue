<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10  disabled"
        v-b-toggle="panelId"
        v-if="naplobejegyzes.MaganelzarasVegeDatum"
      >
        Magánelzárás megkezdése:
        <em>{{ naplobejegyzes.Hatarido | toDateTime }}</em
        >. Magánelzárás tervezett vége:
        <em>{{ naplobejegyzes.MaganelzarasVegeDatum | toDateTime }}</em
        >. Elhelyezés:
        <em>{{ naplobejegyzes.JegyzokonyvTartalma }}</em>
      </a>
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10  disabled"
        v-b-toggle="panelId"
        v-else
      >
        Magánelzárás megkezdése:
        <em>{{ naplobejegyzes.Hatarido | toDateTime }}</em
        >. Elhelyezés:
        <em>{{ naplobejegyzes.JegyzokonyvTartalma }}</em>
      </a>
    </div>
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
import { UserStoreTypes } from '../../../store/modules/user';
import { AppStoreTypes } from '../../../store/modules/app';
import Cimkek from '../../../data/enums/Cimkek';
import { eventBus } from '../../../main';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';

export default {
  name: 'maganelzaras-megkezdesenek-rogzitese-naplobejegyzes',
  data() {
    return {
      fegyelmiUgyStatuszok: Cimkek.FegyelmiUgyStatusza,
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
      vanReintegraciosTisztJoga:
        UserStoreTypes.getters.vanReintegraciosTisztJoga,
      vanJogkorGyakorloJoga: UserStoreTypes.getters.vanJogkorGyakorloJoga,
      vanFofelugyeloJoga: UserStoreTypes.getters.vanFofelugyeloJoga,
    }),
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },
  },
  methods: {
    Nyomtatas() {
      NyomtatvanyFunctions.VegrehajtasiLapNyomtatas({
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      });
    },
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      };
      eventBus.$emit('Modal:maganelzaras-megkezdesenek-rogzitese', {
        state: true,
        data,
      });
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
