<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        A közvetítői eljárás keretében feljegyzés készült.
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
              }"
              >{{ mockNaplo.EljarasAlaVontKepviseloje }}
            </span>
            <span
              v-if="mockNaplo.Hatarido"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Határidő',
                ...toolTipOptions,
              }"
              >{{ mockNaplo.Hatarido | toShortDate }}
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
            <div class="mb-10">
              <span
                v-html="mockNaplo.FeljegyzesMegbeszelesrol"
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
  name: 'kozvetitoi-eljaras-feljegyzes-naplobejegyzes',
  data() {
    return {
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#slidepanel-fegyelmi-ugy',
      },
      //TODO: backend, napló kitöltés, ennek átvezetése és törlése
      mockNaplo: {
        SertettKepviseloje: 'Piros Péter',
        EljarasAlaVontKepviseloje: 'Kormos Károly',
        Hatarido: moment().toISOString(),
        EljarasAlaVontatErintoKoltsegek: 112123,
        SertettetErintoKoltsegek: 89237482,
        FeljegyzesMegbeszelesrol: 'Mock',
        Megallapodas: 'Mock',
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
