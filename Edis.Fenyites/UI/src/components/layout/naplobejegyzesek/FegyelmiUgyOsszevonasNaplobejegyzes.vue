<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer "
        v-b-toggle="panelId"
        v-html="fejlec"
        :class="{
          disabled:
            egyebAdatok &&
            fegyelmiUgy &&
            egyebAdatok.FoFegyelmiUgyId != fegyelmiUgy.FegyelmiUgyId,
        }"
      >
      </a>
    </div>
    <b-collapse
      :id="panelId"
      @show="NaploCollapseClick()"
      v-if="
        egyebAdatok &&
          fegyelmiUgy &&
          egyebAdatok.FoFegyelmiUgyId == fegyelmiUgy.FegyelmiUgyId
      "
    >
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div
            v-for="ugy in egyebAdatok.Ugyek.filter(
              x => x.FegyelmiUgyId != egyebAdatok.FoFegyelmiUgyId
            )"
            :key="ugy.FegyelmiUgyId"
            class="col-lg-4"
          >
            <span
              :key="ugy.FegyelmiUgyId"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Esemény',
                ...toolTipOptions,
              }"
            >
              {{ ugy.EsemenyJellege }}
            </span>
            <br />
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Ügyszám',
                ...toolTipOptions,
              }"
            >
              {{ ugy.UgyszamIntezetAzon }}/{{ ugy.UgyszamEv }}/{{ ugy.Ugyszam }}
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Elrendelés dátuma',
                ...toolTipOptions,
              }"
            >
              {{ ugy.ElrendelesDatuma | toShortDate }}
            </span>
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Kivizsgáló személy',
                ...toolTipOptions,
              }"
            >
              Kiv: {{ ugy.KivizsgaloSzemely.Nev | camelCaseString }}
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
import { defaultToolTipOptions } from '../../../plugins/bootstrapVue';
import Cimkek from '../../../data/enums/Cimkek';
import { eventBus } from '../../../main';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';
import { AppStoreTypes } from '../../../store/modules/app';
import { UserStoreTypes } from '../../../store/modules/user';
import { camelCaseString } from '../../../utils/common';

export default {
  name: 'ugy-osszevonas-naplobejegyzes',
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
      if (!this.naplobejegyzes || !this.fegyelmiUgy || !this.egyebAdatok) {
        return '';
      }

      if (this.egyebAdatok.FoFegyelmiUgyId == this.fegyelmiUgy.FegyelmiUgyId) {
        return `<em>${camelCaseString(this.naplobejegyzes.RogzitoSzemely)} ${
          this.naplobejegyzes.RogzitoSzemelyRendfokozat
            ? this.naplobejegyzes.RogzitoSzemelyRendfokozat
            : ''
        }</em> összevonta a fogvatartott <em>${
          this.egyebAdatok.Ugyek.length
        }</em> folyamatban lévő fegyelmi ügyét, így ebben az ügyben közösen kerülnek tárgyalásra.`;
      } else {
        var foUgy = this.egyebAdatok.Ugyek.find(
          x => x.FegyelmiUgyId == this.egyebAdatok.FoFegyelmiUgyId
        );

        return `<em>${camelCaseString(this.naplobejegyzes.RogzitoSzemely)} ${
          this.naplobejegyzes.RogzitoSzemelyRendfokozat
            ? this.naplobejegyzes.RogzitoSzemelyRendfokozat
            : ''
        }</em> a fegyelmi ügyet összevonta a <em>${foUgy.UgyszamIntezetAzon}/${
          foUgy.UgyszamEv
        }/${
          foUgy.Ugyszam
        }</em> számú üggyel. Az ügy a másik az eljárásban kerül tárgyalásra.`;
      }
      return '';
    },
    egyebAdatok() {
      if (!this.fegyelmiUgy) {
        return '';
      }
      return JSON.parse(this.naplobejegyzes.EgyebAdatokJson);
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
