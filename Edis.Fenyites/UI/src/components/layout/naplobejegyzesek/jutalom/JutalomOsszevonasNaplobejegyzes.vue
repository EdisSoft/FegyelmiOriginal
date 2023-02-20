<template>
  <div class="panel" v-if="jutalomUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10"
        v-b-toggle="panelId"
        v-html="fejlec"
        :class="{
          disabled: !isFougy,
          pointer: isFougy,
        }"
      >
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()" v-if="isFougy">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div v-for="ugy in tovabbiUgyek" :key="ugy.Id" class="col-lg-4">
            <span
              v-if="ugy.Datum"
              v-b-tooltip="{
                title: 'Elrendelés dátuma',
                ...toolTipOptions,
              }"
            >
              {{ ugy.Datum | toDateTime }}
            </span>
            <br />
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Oka',
                ...toolTipOptions,
              }"
            >
              {{ ugy.Oka.Nev }}
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Előterjesztő',
                ...toolTipOptions,
              }"
            >
              Elo: {{ ugy.Eloterjeszto.Nev | camelCaseString }}
            </span>
          </div>
        </div>

        <div class="mt-5">
          <span v-html="naplobejegyzes.JegyzokonyvTartalma"></span>
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
import { defaultToolTipOptions } from '../../../../plugins/bootstrapVue';

import { AppStoreTypes } from '../../../../store/modules/app';
import { UserStoreTypes } from '../../../../store/modules/user';
import { camelCaseString, safeGetProp } from '../../../../utils/common';

export default {
  name: 'jutalom-osszevonas-naplobejegyzes',
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
    NaploCollapseClick() {},
  },
  computed: {
    ...mapGetters({
      isModalOpen: AppStoreTypes.getters.isModalOpen,
      vanReintegraciosTisztJoga:
        UserStoreTypes.getters.vanReintegraciosTisztJoga,
    }),
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },
    tovabbiJelenlevok() {
      if (!this.naplobejegyzes.TovabbiJelenlevok) {
        return [];
      }
      return this.naplobejegyzes.TovabbiJelenlevok.split(',');
    },
    fejlec() {
      let nev = this.naplobejegyzes.RogzitoSzemely || 'A rögzítő személy';
      let rendf = this.naplobejegyzes.RogzitoSzemelyRendfokozat || '';
      let ugyDb = this.tovabbiUgyek.length;
      if (this.isFougy) {
        return `${nev} ${rendf} összevonta a fogvatartott ${ugyDb} további ügyével, így ebben az ügyben közösen kerülnek mérlegelésre.`;
      }
      let datum = this.$options.filters.toShortDate(
        this.jutalomUgy.RogzitesIdeje
      );
      let jutalmazasOka = safeGetProp(this.fougy, 'Oka.Nev') || '';
      return `${nev} ${rendf} az előterjesztést összevonta a ${datum} ${jutalmazasOka} üggyel. Az ügy a másik jutalomban kerül mérlegelésre.`;
    },
    fougyId() {
      return this.naplobejegyzes.NaploJSon.FoUgyId;
    },
    fougy() {
      let fougyId = this.fougyId;
      return this.naplobejegyzes.NaploJSon.OsszevontUgyek.find(
        f => f.Id == fougyId
      );
    },
    isFougy() {
      let fougyId = this.fougyId;
      return this.jutalomUgy.Id == fougyId;
    },
    tovabbiUgyek() {
      let fougyId = this.fougyId;
      return this.naplobejegyzes.NaploJSon.OsszevontUgyek.filter(
        f => f.Id != fougyId
      );
    },
    osszevontUgyek() {
      return this.naplobejegyzes.NaploJSon.OsszevontUgyek || [];
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
