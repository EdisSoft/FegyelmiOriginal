<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        <!-- TODO: backend -->
        Az eljárás alá vont közvetítői eljárás kezdeményezését kérte
        <em
          >{{ naplobejegyzes.SertettAktNyilvantartasiSzam }}
          {{ naplobejegyzes.SertettNeve }}</em
        >
        sértett egyetértésével.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12">
            <span
              v-if="naplobejegyzes.SertettKepviseloje"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Sértett képviselője',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.SertettKepviseloje }}</span
            >
            <span
              v-if="naplobejegyzes.EljarasAlaVontKepviseloje"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Eljárás alá vont képviselője',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.EljarasAlaVontKepviseloje }}</span
            >
          </div>
          <div class="col col-12 mt-10">
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                v-if="isSzerkesztheto"
                :disabled="isModalOpen"
                class="btn btn-pure btn-dark icon btn-round icon wb-edit pt-5"
                @click="NaploBejegyzesSzerkesztes"
              >
              </b-button>

              <b-button
                :disabled="isModalOpen"
                type="button"
                @click="Nyomtatas"
                class="btn btn-pure btn-dark icon wb-print pt-5"
              ></b-button>
            </div>

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
import { UserStoreTypes } from '../../../../store/modules/user';
import Cimkek from '../../../../data/enums/Cimkek';
import { eventBus } from '../../../../main';
import { NyomtatvanyFunctions } from '../../../../functions/nyomtatvanyFunctions';

export default {
  name: 'kozvetitoi-eljaras-kezdemenyezese-naplobejegyzes',
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
    Nyomtatas() {
      NyomtatvanyFunctions.KozvetitoiEljarasonReszvetelNyomtatas({
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      });
    },
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      };
      eventBus.$emit('Modal:kozvetitoi-eljaras-kezdemenyezese', {
        state: true,
        data,
      });
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
    isSzerkesztheto() {
      let fegyStatusz = Cimkek.FegyelmiUgyStatusza;
      if (!this.fegyelmiUgy || !this.naplobejegyzes) {
        return false;
      }
      let ugySatuszId = this.fegyelmiUgy.UgyStatuszId;
      return (
        (ugySatuszId == fegyStatusz.ReintegraciosTisztiJogkorben ||
          ugySatuszId == fegyStatusz.KivizsgalasFolyamatban ||
          ugySatuszId == fegyStatusz.FenyitesKiszabva ||
          ugySatuszId == fegyStatusz.FenyitesVegrehajtasAlatt) &&
        !this.naplobejegyzes.AlairtaFl &&
        this.vanReintegraciosTisztJoga
      );
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
