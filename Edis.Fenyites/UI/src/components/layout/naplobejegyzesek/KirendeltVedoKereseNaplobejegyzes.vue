<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        A fogvatartott kirendelt védőt kért a
        <em>{{ naplobejegyzes.Torvenyszek }} törvényszék</em>ről.
        <span v-if="!naplobejegyzes.Vegleges">Nincs véglegesítve.</span>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12 mt-10">
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                :disabled="isModalOpen"
                v-if="vanJogkorGyakorloJoga || vanReintegraciosTisztJoga"
                class="btn btn-pure btn-dark icon btn-round icon wb-edit pt-5"
                @click="NaploBejegyzesSzerkesztes"
              >
              </b-button>
              <div class="short-drop">
                <b-dropdown
                  size="lg"
                  variant="link"
                  toggle-class="text-decoration-none"
                  no-caret
                >
                  <template v-slot:button-content>
                    <b-button class="btn btn-default dropdown-toggle">
                      Nyomtatványok
                    </b-button>
                  </template>

                  <b-dropdown-item @click="NyilatkozatNyomtatas()"
                    >Nyilatkozat nyomtatás</b-dropdown-item
                  >
                  <b-dropdown-item @click="LevelSablonNyomtatas(true)"
                    >Levél sablon nyomtatás</b-dropdown-item
                  >
                </b-dropdown>
              </div>
            </div>

            <div>
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
import moment from 'moment';
import { camelCaseString } from '../../../utils/common';
import { UserStoreTypes } from '../../../store/modules/user';

export default {
  name: 'kirendelt-vedo-kerese-naplobejegyzes',
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
    NyilatkozatNyomtatas() {
      NyomtatvanyFunctions.VedoKirendeleseNyilatkozatNyomtatas({
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      });
    },
    LevelSablonNyomtatas() {
      NyomtatvanyFunctions.VedoKirendeleseNyomtatas({
        naplobejegyzesId: this.naplobejegyzes.Id,
      });
    },
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      };
      eventBus.$emit('Modal:kirendelt-vedo-kerese', {
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
