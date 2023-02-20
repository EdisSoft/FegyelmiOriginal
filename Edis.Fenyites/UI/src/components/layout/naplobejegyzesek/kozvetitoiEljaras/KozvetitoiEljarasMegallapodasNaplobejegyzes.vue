<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        A közvetítői eljárás keretében
        <span v-if="mockNaplo.Megallapodas">megállapodás</span
        ><span
          v-if="mockNaplo.Megallapodas && mockNaplo.FeljegyzesMegbeszelesrol"
          >,
        </span>
        <span v-if="mockNaplo.FeljegyzesMegbeszelesrol">feljegyzés</span>
        született<span v-if="mockNaplo.Hatarido"
          >, melynek teljesítési határideje
          <em>{{ mockNaplo.Hatarido | toShortDate }}</em>
        </span>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12">
            <span
              v-if="mockNaplo.SertettKepviseloje"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Sértett képviselője',
                ...toolTipOptions,
              }"
              >{{ mockNaplo.SertettKepviseloje }}
            </span>
            <span
              v-if="mockNaplo.EljarasAlaVontKepviseloje"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Eljárás alá vont képviselője',
                ...toolTipOptions,
              }"
              >{{ mockNaplo.EljarasAlaVontKepviseloje }}
            </span>
            <span
              v-if="
                mockNaplo.EljarasAlaVontatErintoKoltsegek &&
                  mockNaplo.EljarasAlaVontatErintoKoltsegek > 0
              "
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Eljárás alá vontat érintő költségek',
                ...toolTipOptions,
              }"
              >{{ mockNaplo.EljarasAlaVontatErintoKoltsegek | formatNumber }} Ft
            </span>
            <span
              v-if="
                mockNaplo.SertettetErintoKoltsegek &&
                  mockNaplo.SertettetErintoKoltsegek > 0
              "
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Sértettet érintő költségek',
                ...toolTipOptions,
              }"
              >{{ mockNaplo.SertettetErintoKoltsegek | formatNumber }} Ft
            </span>
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

            <div class="mt-10" v-if="mockNaplo.FeljegyzesMegbeszelesrol">
              <div class="mb-10">
                Feljegyzés:
                <div
                  v-html="mockNaplo.FeljegyzesMegbeszelesrol"
                  class="font-size-12 ml-5"
                ></div>
              </div>
            </div>
            <div class="mt-10" v-if="mockNaplo.Megallapodas">
              <div class="mb-10">
                Megállapodás:
                <div
                  v-html="mockNaplo.Megallapodas"
                  class="font-size-12 ml-5"
                ></div>
              </div>
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
import { UserStoreTypes } from '../../../../store/modules/user';
import { eventBus } from '../../../../main';
import { NyomtatvanyFunctions } from '../../../../functions/nyomtatvanyFunctions';

export default {
  name: 'kozvetitoi-eljaras-megallapodas-naplobejegyzes',
  data() {
    return {
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#slidepanel-fegyelmi-ugy',
      },
      //TODO: backend, napló kitöltés, ennek átvezetése és törlése
      mockNaplo: {
        SertettKepviseloje: this.naplobejegyzes.SertettKepviseloje,
        EljarasAlaVontKepviseloje: this.naplobejegyzes
          .EljarasAlaVontKepviseloje,
        Hatarido:
          this.naplobejegyzes.Hatarido == null
            ? null
            : moment(this.naplobejegyzes.Hatarido).toISOString(),
        EljarasAlaVontatErintoKoltsegek: this.naplobejegyzes.VedoCime,
        SertettetErintoKoltsegek: this.naplobejegyzes.VedoNeve,
        FeljegyzesMegbeszelesrol: this.naplobejegyzes.VedoElerhetosege,
        Megallapodas: this.naplobejegyzes.JegyzokonyvTartalma,
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
      console.log('NaploBejegyzesSzerkesztes');
      eventBus.$emit('Modal:feljegyzes-es-megallapodas', {
        state: true,
        data,
      });
    },
    Nyomtatas() {
      NyomtatvanyFunctions.MegallapodasEsFeljegyzesNyomtatas({
        //fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
        naplobejegyzesIds: [this.naplobejegyzes.Id],
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
