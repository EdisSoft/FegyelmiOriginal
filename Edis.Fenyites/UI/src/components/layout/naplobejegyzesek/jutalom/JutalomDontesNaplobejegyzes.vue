<template>
  <div class="panel" v-if="jutalomUgy && naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10"
        v-b-toggle="panelId"
        v-html="
          naplobejegyzes.NaploJSon.JutalomTipusa.Id == 12
            ? 'Döntöttem az előterjesztés elutasításáról.'
            : `Döntöttem <em>${naplobejegyzes.NaploJSon.JutalomTipusa.Nev}</em> jutalmazásról.`
        "
      >
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div
        class="panel-body text-default pt-0"
        v-if="
          naplobejegyzes.NaploJSon.JutalomHossz != null ||
          naplobejegyzes.NaploJSon.SzemSzuksFordOsszegNoveles != null
        "
      >
        <div
          class="row mb-10"
          v-if="naplobejegyzes.NaploJSon.JutalomHossz != null"
        >
          <p>
            Jutalom tartama: {{ naplobejegyzes.NaploJSon.JutalomHossz.Nev }}
          </p>
        </div>
        <div
          class="row mb-10"
          v-if="naplobejegyzes.NaploJSon.SzemSzuksFordOsszegNoveles != null"
        >
          <p>
            Növelés mértéke:
            {{ naplobejegyzes.NaploJSon.SzemSzuksFordOsszegNoveles }}%
          </p>
        </div>
      </div>
      <div
        class="panel-body text-default pt-0"
        v-if="naplobejegyzes.NaploJSon.ToroltFegyelmiUgy"
      >
        <div class="row">
          A fegyelmi ügy azonosítója:
          {{ naplobejegyzes.NaploJSon.ToroltFegyelmiUgy.Nev }}
        </div>
      </div>
      <div v-if="naplobejegyzes.NaploJSon.JutalomTipusa.Id == 15">
        <div class="pt-5 px-4">
          Dokumentum nyomtatásához kattintson a nyomtató ikonra.
          <b-button
            v-if="naplobejegyzes.InaktivFl != true"
            :disabled="isModalOpen"
            type="button"
            @click="Nyomtatas()"
            class="btn btn-pure btn-dark icon wb-print pt-5"
          ></b-button>
        </div>
      </div>
      <!-- <div
        class="panel-body text-default pt-0"
        v-if="naplobejegyzes.NaploJSon.Indoklas"
      >
        <div class="row mb-10">
          <em>{{ naplobejegyzes.NaploJSon.JavaslatTevo.Nev }}</em>&nbsp;javaslatára a fogvatartottat előterjesztették jutalmazásra&nbsp;
          <em>{{ naplobejegyzes.NaploJSon.JutalmazasOka }}</em>&nbsp;okból.
        </div>
        <div class="row">
          <div class="col-12 mt-5">
            <span
              v-html="naplobejegyzes.NaploJSon.Indoklas"
              class="font-size-12 font-italic"
            ></span>
          </div>
        </div>
      </div> -->
    </b-collapse>
    <div class="text-right pl-20 pb-0">
      <small class="blue-grey-400">
        {{ TipusCimke }} ·
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
import Cimkek from '../../../../data/enums/Cimkek';
import { AppStoreTypes } from '../../../../store/modules/app';
import { NyomtatvanyFunctions } from '../../../../functions/nyomtatvanyFunctions';

export default {
  name: 'jutalom-dontes-naplobejegyzes',
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
    NaploCollapseClick() {
      // korábbi analitika helye
    },
    async Nyomtatas() {
      this.nyomtatasLoading = true;

      var vm = this;
      var jutalmiUgyId = this.jutalomUgy.Id;
      await NyomtatvanyFunctions.TeritesmentesTelefonalasJutalomNyomtatas({
        jutalmiUgyId: jutalmiUgyId,
      });
      this.nyomtatasLoading = false;
    },
  },
  computed: {
    ...mapGetters({
      isModalOpen: AppStoreTypes.getters.isModalOpen,
    }),
    TipusCimke() {
      return 'Döntés';
    },
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },

    isVisszakuldes() {
      if (
        this.naplobejegyzes.VisszakuldesOkaCimkeId ==
        Cimkek.VisszakuldesOka.JogiKepviseletetKer
      )
        return true;
      else return false;
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
