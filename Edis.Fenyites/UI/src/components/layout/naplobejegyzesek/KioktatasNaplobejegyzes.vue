<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        <span v-if="naplobejegyzes.AlairtaFl">
          <em>{{ naplobejegyzes.RogzitoSzemely | camelCaseString }}</em>
          kioktatta a fogvatartottat, ezzel az ügy lezárásra került.
        </span>
        <span v-else>
          A kioktatás rögzítése jelenleg folyamatban van. A nyomtatvány
          elkészült.
        </span>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12">
            <span
              v-if="naplobejegyzes.FegyelmiVetsegTipusaCimke"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Fegyelmi vétség típusa',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.FegyelmiVetsegTipusaCimke }}</span
            >
            <span
              v-if="naplobejegyzes.LetrehozasDatuma"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Kiszabás ideje',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.FenyitesKiszabasDatuma | toShortDate }}</span
            >
          </div>
          <div class="col col-12 mt-10">
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                :disabled="isModalOpen"
                v-if="
                  !naplobejegyzes.AlairtaFl &&
                    (vanJogkorGyakorloJoga || vanReintegraciosTisztJoga)
                "
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
            <div class="mt-5">
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
import { mapGetters } from 'vuex';
import { defaultToolTipOptions } from '../../../plugins/bootstrapVue';
import { AppStoreTypes } from '../../../store/modules/app';
import { UserStoreTypes } from '../../../store/modules/user';
import { eventBus } from '../../../main';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';

export default {
  name: 'kioktatas-naplobejegyzes',
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
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      };
      eventBus.$emit('Modal:reintegracios-tiszt-dontese-kioktatas', {
        state: true,
        data,
      });
    },
    Nyomtatas() {
      NyomtatvanyFunctions.KioktatasReintegraciosJogkorbenNyomtatas({
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
        naploId: this.naplobejegyzes.Id,
      });
    },
  },
  computed: {
    ...mapGetters({
      isModalOpen: AppStoreTypes.getters.isModalOpen,
      vanReintegraciosTisztJoga:
        UserStoreTypes.getters.vanReintegraciosTisztJoga,
      vanJogkorGyakorloJoga: UserStoreTypes.getters.vanJogkorGyakorloJoga,
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
