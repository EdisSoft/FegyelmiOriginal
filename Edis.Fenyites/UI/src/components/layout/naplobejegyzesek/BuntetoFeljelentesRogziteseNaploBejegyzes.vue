<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        Büntető feljelentés rögzítése
        <em>"{{ naplobejegyzes.VedoCime | lowerCaseString }}" </em>
        <em>{{ naplobejegyzes.Hatarido | toShortDatePontNelkul }}</em
        >-kor.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12">
            <span
              v-if="naplobejegyzes.VedoElerhetosege"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400"
              v-b-tooltip="{
                title: 'Elintézés módja',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.VedoElerhetosege }}</span
            >
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                :disabled="isModalOpen"
                v-if="vanJogkorGyakorloJoga || vanReintegraciosTisztJoga"
                class="btn btn-pure btn-dark icon btn-round icon wb-edit pt-5"
                @click="NaploBejegyzesSzerkesztes"
              >
              </b-button>
              <b-button
                v-if="naplobejegyzes.InaktivFl != true"
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
import { mapGetters } from 'vuex';
import { defaultToolTipOptions } from '../../../plugins/bootstrapVue';
import Cimkek from '../../../data/enums/Cimkek';
import { eventBus } from '../../../main';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';
import { AppStoreTypes } from '../../../store/modules/app';
import { UserStoreTypes } from '../../../store/modules/user';
import moment from 'moment';

export default {
  name: 'bunteto-feljelentes-rogzitese-naplo-bejegyzes',
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
    Nyomtatas() {
      if (!this.naplobejegyzes) {
        return;
      }
      NyomtatvanyFunctions.BuntetoFeljelentesDocxNyomtatas({
        naplobejegyzesId: this.naplobejegyzes.Id,
      });
    },
    NaploCollapseClick() {
    },
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      };
      eventBus.$emit('Modal:bunteto-feljelentes-rogzitese', {
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
