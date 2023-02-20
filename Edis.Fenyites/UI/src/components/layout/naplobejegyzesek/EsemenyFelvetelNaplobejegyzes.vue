<template>
  <div class="panel" v-if="fegyelmiUgy && esemeny">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        <em>{{ esemeny.Jelleg }}</em> esemény történt
        <em>{{ esemeny.EsemenyDatuma | toDateTime }}</em
        >-kor.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row mb-10">
          <div class="col col-xl-8 col-lg-7">
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Esemény ideje',
                ...toolTipOptions,
              }"
              >{{ esemeny.EsemenyDatuma | toDateTime }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Napszak',
                ...toolTipOptions,
              }"
              >{{ esemeny.Napszak }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Hely',
                ...toolTipOptions,
              }"
              >{{ esemeny.Hely }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Esemény jellege',
                ...toolTipOptions,
              }"
              >{{ esemeny.Jelleg }}</span
            >
            <!--<span
              class="badge badge-outline badge-warning mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Elkövető',
                ...toolTipOptions,
              }"
              >{{ fegyelmiUgy.AktNyilvantartasiSzam }}
              {{ fegyelmiUgy.FogvatartottNev | camelCaseString }}
            </span>-->
            <span
              v-for="elkoveto in esemeny.TovabbiElkovetok"
              :key="elkoveto"
              v-b-tooltip="{
                title: 'Elkövető',
                ...toolTipOptions,
              }"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              >{{ elkoveto | camelCaseNameOnly }}</span
            >
            <span
              v-for="tanu in esemeny.Tanuk"
              :key="tanu"
              v-b-tooltip="{
                title: 'Tanú',
                ...toolTipOptions,
              }"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              >{{ tanu | camelCaseNameOnly }}</span
            >
            <span
              v-for="sertett in esemeny.Sertettek"
              :key="sertett"
              v-b-tooltip="{
                title: 'Sértett',
                ...toolTipOptions,
              }"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              >{{ sertett | camelCaseNameOnly }}</span
            >
          </div>
          <div class="col col-xl-4 col-lg-5">
            <div>
              <b-dropdown
                size="lg"
                variant="link"
                toggle-class="text-decoration-none pt-0"
                no-caret
              >
                <template v-slot:button-content>
                  <b-button class="btn btn-default dropdown-toggle">
                    Nyomtatványok
                  </b-button>
                </template>

                <!-- PDF MERGE GOMB -->
                <!-- <b-dropdown-item @click="pdfMerge()">PDFMerge</b-dropdown-item> -->

                <b-dropdown-item @click="FegyelmiLapNyomtatas()"
                  >Fegyelmi lap</b-dropdown-item
                >
                <b-dropdown-item @click="TajekoztatoNyomtatas(true)"
                  >Tájékoztató – befejezéskor betekintést kér</b-dropdown-item
                >
                <b-dropdown-item @click="TajekoztatoNyomtatas(false)">
                  Tájékoztató – befejezéskor betekintést nem kér
                </b-dropdown-item>
                <!-- <b-dropdown-item
    @click="EljarasAlaVontMeghallgatasiJegyzokonyvNyomtatas"
    >Eljárás alá vont meghallgatási jegyzőkönyv</b-dropdown-item
  >
  <b-dropdown-item @click="TanuMeghallgatasiJegyzokonyvNyomtatas">
    Tanú (Fogvatartott) meghallgatási jegyzőkönyv
  </b-dropdown-item>
  <b-dropdown-item @click="TanuAllomanyiTagJegyzokonyvNyomtatas">
    Tanú (Személyi állományi tag) meghallgatási jegyzőkönyv
  </b-dropdown-item> -->
                <!-- <b-dropdown-item @click="ElkulonitesiLapNyomtatas"
    >Fegyelmi elkülönítési lap</b-dropdown-item
  > -->
                <b-dropdown-item @click="AlairasMegtagadasaNyomtatas"
                  >Jegyzőkönyv aláírás megtagadásáról</b-dropdown-item
                >
                <b-dropdown-item @click="ErtesitoLapNyomtatas"
                  >Értesítő lap</b-dropdown-item
                >
                <!-- <b-dropdown-item @click="VedoKirendeleseNyilatkozatNyomtatas"
    >Nyilatkozat védő kirendeléséhez</b-dropdown-item
  >
  <b-dropdown-item @click="VedoKirendeleseNyomtatas"
    >Nyilatkozat védő kirendeléséhez V2</b-dropdown-item
  >
  <b-dropdown-item @click="VedoKirendeleseNyilatkozatV3Nyomtatas"
    >Nyilatkozat védő kirendeléséhez V3</b-dropdown-item
  >
  <b-dropdown-item @click="VedoKirendeleseNyilatkozatV4Nyomtatas"
    >Nyilatkozat védő kirendeléséhez V4</b-dropdown-item
  > -->
                <!-- <b-dropdown-item
    @click="KioktatasReintegraciosJogkorbenNyomtatas"
    >Kioktatás reintegrációs tiszti jogkörben</b-dropdown-item
  > -->
                <!-- <b-dropdown-item @click="VedoTelefonosTajekoztatasaNyomtatas"
    >Védő telefonon történő tájékoztatása</b-dropdown-item
  >-->
                <!-- <b-dropdown-item
    @click="FegyelmiTargyalasiJegyzokonyvMasodfokNyomtatas"
    >Másodfokú fegyelmi tárgyalási jegyzőkönyv</b-dropdown-item
  > -->
                <!-- <b-dropdown-item @click="SzembesitesiJegyzokonyvNyomtatas"
    >Szembesítési jegyzőkönyv</b-dropdown-item
  > -->
                <!-- <b-dropdown-item
    @click="MasodfokuFegyelmiHatarozatMegszunteteseNyomtatas"
    >Határozat megszűntetésről</b-dropdown-item
  >
  <b-dropdown-item @click="MasodfokuFegyelmiHatarozatNyomtatas"
    >Másodfokú fegyelmi határozat</b-dropdown-item
  > -->
                <b-dropdown-item @click="VegrehajtasiLapNyomtatas"
                  >Végrehajtási lap nyomtatás</b-dropdown-item
                >
                <b-dropdown-item
                  @click="FegyelmiEljarastKezdemenyezoLapNyomtatas"
                  >Fegyelmi eljárást kezdeményező lap</b-dropdown-item
                >
                <b-dropdown-item @click="IratosszesitoFegyelmiUgybenNyomtatas"
                  >Iratösszesítő fegyelmi ügyben</b-dropdown-item
                >
                <b-dropdown-item @click="LefoglalasiJegyzokonyvNyomtatas"
                  >Lefoglalási jegyzőkönyv</b-dropdown-item
                >
                <b-dropdown-item @click="KarjelentoLapNyomtatas"
                  >Kárjelentő lap nyomtatása</b-dropdown-item
                >
                <b-dropdown-item @click="OtosSzamuMellekletNyomtatas">
                  BvBank Kárjelentő lap nyomtatása
                </b-dropdown-item>
                <b-dropdown-item @click="FeljelentesNyomtatas">
                  BvBank feljelentés nyomtatása
                </b-dropdown-item>
              </b-dropdown>
            </div>
            <div>
              <!-- <b-dropdown
                size="lg"
                variant="link"
                toggle-class="text-decoration-none pt-0"
                no-caret
              >
                <template v-slot:button-content> -->
              <b-button
                class="btn btn-default"
                @click="EsemenyMegtekintes"
                v-b-tooltip="{
                  title: 'Résztvevő, tanú, csatolmány hozzáadása eseményhez',
                  ...toolTipOptions,
                }"
              >
                Esemény szerkesztése
              </b-button>
              <!-- </template>
              </b-dropdown> -->
            </div>
          </div>
        </div>

        <div class="row">
          <div class="col-12 mt-5">
            <span
              v-html="esemeny.Leiras"
              class="font-size-12 font-italic"
            ></span>
          </div>
          <br />
          <div class="col-12 mt-5" v-if="esemeny.Bizonyitek">
            <span class="font-size-12"
              >Bizonyíték: <em>{{ esemeny.Bizonyitek }}</em></span
            >
          </div>
        </div>
        <k-csatolmanyok :csatolmanyok="feltoltesek"></k-csatolmanyok>
      </div>
    </b-collapse>
    <div class="text-right pl-20 pb-0 mt-10">
      <small class="blue-grey-400">
        Esemény rögzítve · {{ esemeny.RogzitoSzemely | camelCaseString }}
        {{ esemeny.RogzitoSzemelyRendfokozat }} ·
        {{ esemeny.LetrehozasDatuma | toDateTime }}
      </small>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import { defaultToolTipOptions } from '../../../plugins/bootstrapVue';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';
import settings from '../../../data/settings';
import { FegyelmiUgyFunctions } from '../../../functions/FegyelmiUgyFunctions';
import {
  timeout,
  getAttachmentIcon,
  fileDownload,
} from '../../../utils/common';
import { eventBus } from '../../../main';

export default {
  name: 'esemeny-felvetel-naplobejegyzes',
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
    NaploCollapseClick() {},
    pdfMerge() {
      NyomtatvanyFunctions.pdfMerge(this.fegyelmiUgy.FegyelmiUgyId);
    },
    FegyelmiLapNyomtatas() {
      NyomtatvanyFunctions.FegyelmiLapokNyomtatasa({
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
      });
    },
    TajekoztatoNyomtatas(kerem) {
      NyomtatvanyFunctions.TajekoztatoNyomtatas(
        this.fegyelmiUgy.FegyelmiUgyId,
        kerem
      );
    },
    // EljarasAlaVontMeghallgatasiJegyzokonyvNyomtatas() {
    //   NyomtatvanyFunctions.EljarasAlaVontMeghallgatasiJegyzokonyvNyomtatas();
    // },
    // TanuMeghallgatasiJegyzokonyvNyomtatas() {
    //   NyomtatvanyFunctions.TanuMeghallgatasiJegyzokonyvNyomtatas();
    // },
    // TanuAllomanyiTagJegyzokonyvNyomtatas() {
    //   NyomtatvanyFunctions.TanuAllomanyiTagJegyzokonyvNyomtatas();
    // },
    FegyelmiHatarozatNyomtatas() {
      NyomtatvanyFunctions.FegyelmiHatarozatNyomtatas({
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
      });
    },
    FegyelmiHatarozatMegszuntetesNyomtatas() {
      NyomtatvanyFunctions.FegyelmiHatarozatMegszuntetesNyomtatas({
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
      });
    },
    HatarozatReintegraciosNyomtatas() {
      NyomtatvanyFunctions.HatarozatReintegraciosNyomtatas({
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
      });
    },
    ErtesitoLapNyomtatas() {
      NyomtatvanyFunctions.ErtesitoLapNyomtatas(this.fegyelmiUgy.FegyelmiUgyId);
    },
    FegyelmiEljarastKezdemenyezoLapNyomtatas() {
      NyomtatvanyFunctions.FegyelmiEljarastKezdemenyezoLapNyomtatas(
        this.fegyelmiUgy.FegyelmiUgyId
      );
    },

    IratosszesitoFegyelmiUgybenNyomtatas() {
      NyomtatvanyFunctions.IratosszesitoFegyelmiUgybenNyomtatas(
        this.fegyelmiUgy.FegyelmiUgyId
      );
    },

    LefoglalasiJegyzokonyvNyomtatas() {
      NyomtatvanyFunctions.LefoglalasiJegyzokonyvNyomtatas(
        this.fegyelmiUgy.FegyelmiUgyId
      );
    },

    KarjelentoLapNyomtatas() {
      NyomtatvanyFunctions.KarjelentoLapDocxNyomtatas({
        esemenyId: this.esemeny.Id,
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
      });
    },

    OtosSzamuMellekletNyomtatas() {
      NyomtatvanyFunctions.OtosSzamuMellekletDocxNyomtatas({
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
      });
    },
    FeljelentesNyomtatas() {
      NyomtatvanyFunctions.FeljelentesNyomtatas({
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
      });
    },
    // VedoKirendeleseNyilatkozatNyomtatas() {
    //   NyomtatvanyFunctions.VedoKirendeleseNyilatkozatNyomtatas(
    //     this.fegyelmiUgy.FegyelmiUgyId
    //   );
    // },
    // VedoKirendeleseNyomtatas() {
    //   NyomtatvanyFunctions.VedoKirendeleseNyomtatas(
    //     this.fegyelmiUgy.FegyelmiUgyId
    //   );
    // },
    // VedoKirendeleseNyilatkozatV3Nyomtatas() {
    //   NyomtatvanyFunctions.VedoKirendeleseNyilatkozatV3Nyomtatas(
    //     this.fegyelmiUgy.FegyelmiUgyId
    //   );
    // },
    // VedoKirendeleseNyilatkozatV4Nyomtatas() {
    //   NyomtatvanyFunctions.VedoKirendeleseNyilatkozatV4Nyomtatas(
    //     this.fegyelmiUgy.FegyelmiUgyId
    //   );
    // },
    // KioktatasReintegraciosJogkorbenNyomtatas() {
    //   NyomtatvanyFunctions.KioktatasReintegraciosJogkorbenNyomtatas({
    //     fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
    //   });
    // },
    VedoTelefonosTajekoztatasaNyomtatas() {
      NyomtatvanyFunctions.VedoTelefonosTajekoztatasaNyomtatas(
        this.fegyelmiUgy.FegyelmiUgyId
      );
    },
    FegyelmiTargyalasiJegyzokonyvMasodfokNyomtatas() {
      NyomtatvanyFunctions.FegyelmiTargyalasiJegyzokonyvMasodfokNyomtatas(
        this.fegyelmiUgy.FegyelmiUgyId
      );
    },
    VegrehajtasiLapNyomtatas() {
      NyomtatvanyFunctions.VegrehajtasiLapNyomtatas({
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
      });
    },
    // SzembesitesiJegyzokonyvNyomtatas() {
    //   NyomtatvanyFunctions.SzembesitesiJegyzokonyvNyomtatas(
    //     this.fegyelmiUgy.FegyelmiUgyId
    //   );
    // },
    // MasodfokuFegyelmiHatarozatMegszunteteseNyomtatas() {
    //   NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatMegszunteteseNyomtatas(
    //     this.fegyelmiUgy.FegyelmiUgyId
    //   );
    // },
    // MasodfokuFegyelmiHatarozatNyomtatas() {
    //   NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatNyomtatas(
    //     this.fegyelmiUgy.FegyelmiUgyId
    //   );
    // },
    ElkulonitesiLapNyomtatas() {
      NyomtatvanyFunctions.ElkulonitoLapNyomtatas({
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
      });
    },
    AlairasMegtagadasaNyomtatas() {
      NyomtatvanyFunctions.AlairasMegtagadasaNyomtatvany({
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
      });
    },
    async EsemenyMegtekintes() {
      var args = {
        esemenyId: this.esemeny.Id,
      };
      eventBus.$emit('Modal:esemeny-rogzites', {
        state: true,
        data: args,
      });
    },
    DownloadFile(url) {
      window.open(url);
    },
  },
  computed: {
    ...mapGetters({}),
    panelId() {
      return this.$options.name + '_' + this.fegyelmiUgy.FegyelmiUgyId;
    },
    feltoltesek() {
      if (!this.esemeny) {
        return [];
      }
      return this.esemeny.Feltoltesek.filter(x => x.EsemenyId);
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
