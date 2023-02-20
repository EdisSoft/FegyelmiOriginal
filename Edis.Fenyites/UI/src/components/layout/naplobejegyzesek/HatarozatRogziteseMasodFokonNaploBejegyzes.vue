<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
        v-html="fejlec"
      >
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12">
            <span
              v-if="naplobejegyzes.FegyelmiVetsegTipusaCimke"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400"
              v-b-tooltip="{
                title: 'Fegyelmi vétség típusa',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.FegyelmiVetsegTipusaCimke }}</span
            >
            <span
              v-if="naplobejegyzes.VetsegRendeletSzerintCimke"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400"
              v-b-tooltip="{
                title: 'Vétség rendelet szerint',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.VetsegRendeletSzerintCimke }}</span
            >
          </div>
          <div class="col col-12 mt-10">
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                :disabled="isModalOpen"
                v-if="
                  fegyelmiUgy.UgyStatuszId ==
                    fegyelmiUgyStatuszok.IIFokuTargyalas &&
                    fegyelmiUgy.Felfuggesztve != true &&
                    (vanJogkorGyakorloJoga || vanReintegraciosTisztJoga)
                "
                class="btn btn-pure btn-dark icon btn-round icon wb-edit pt-5"
                @click="NaploBejegyzesSzerkesztes"
              >
              </b-button>

              <b-button
                :disabled="isModalOpen"
                type="button"
                @click="HatarozatRogzitesNyomtatas"
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
    <div class="text-right pl-20 pr-0 pb-0">
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
import ModalTipus from '../../../data/enums/modalTipus';

export default {
  name: 'hatarozat-rogzitese-masod-fokon-naplo-bejegyzes',
  data() {
    return {
      fegyelmiUgyStatuszok: Cimkek.FegyelmiUgyStatusza,
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
    HatarozatRogzitesNyomtatas() {
      if (this.naplobejegyzes.Vegleges) {
        NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatMegszunteteseNyomtatas({
          naplobejegyzesIds: [this.naplobejegyzes.Id],
        });
      } else {
        NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatNyomtatas({
          naplobejegyzesIds: [this.naplobejegyzes.Id],
        });
      }
    },
    NaploBejegyzesSzerkesztes() {
      var modalType = this.isHelybenHagya
        ? ModalTipus.HatarozatHelybenhagyasa
        : ModalTipus.HatarozatModositasa;

      var data = {
        fegyelmiUgyId: this.fegyelmiUgy.FegyelmiUgyId,
        naplobejegyzesId: this.naplobejegyzes.Id,
        modalType: modalType,
      };
      eventBus.$emit('Modal:hatarozat-rogzitese-masod-foku', {
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
    fejlec() {
      if (!this.naplobejegyzes || !this.fegyelmiUgy) {
        return '';
      }

      if (!this.naplobejegyzes.AlairtaFl) {
        return 'Határozat rögzítés folyamatban.';
      }

      if (this.naplobejegyzes.Vegleges) {
        var szemely = this.naplobejegyzes.DonteshozoSzemely
          ? `<em>${camelCaseString(
              this.naplobejegyzes.DonteshozoSzemely
            )} ${camelCaseString(
              this.naplobejegyzes.DonteshozoSzemelyRendfokozat
            )}</em>`
          : `<em>${camelCaseString(this.naplobejegyzes.Torvenyszek)}</em>`;
          if (this.naplobejegyzes.MegszuntetesOkaCimkeId == Cimkek.HatalyonKivulHelyezesTipusai.UjEljarasInditasa)
          {
            return (
              szemely +
              ' határozata alapján az eljárás hatályon kívül helyezésre került és új eljárás indult.'
            );
          }
          else if (this.naplobejegyzes.MegszuntetesOkaCimkeId == Cimkek.HatalyonKivulHelyezesTipusai.NemAkarokUjEljarast)
          {
            return (
              szemely +
              ' határozata alapján az eljárás hatályon kívül helyezésre került.'
            );
          }
          else 
          {
            return (
              szemely +
              ' határozata alapján az eljárás meg lett szüntetve a következő miatt: ' +
              this.naplobejegyzes.MegszuntetesOkaCimke.toLowerCase() +
              '.'
            );
          }
      }

      var tipus = `${this.naplobejegyzes.FenyitesTipusCimke.toLowerCase()}`;
      if (
        this.naplobejegyzes.FenyitesTipusCimkeId ==
        Cimkek.FenyitesTipusa.KietkezesCsokkentes
      ) {
        tipus = `${
          this.naplobejegyzes.KietkezesCsokkentes
        }%-os ${this.naplobejegyzes.FenyitesTipusCimke.toLowerCase()}`;
      }

      var hatarozatTipus = this.naplobejegyzes.IsHelybenhagyas
        ? 'helybenhagyó'
        : 'módosító';

      if (
        this.naplobejegyzes.TipusCimkeId ==
          Cimkek.NaploTipus.HatarozatRogziteseMasodFokon &&
        this.naplobejegyzes.DonteshozoSzemely
      ) {
        return `<em>${camelCaseString(
          this.naplobejegyzes.DonteshozoSzemely
        )} ${camelCaseString(
          this.naplobejegyzes.DonteshozoSzemelyRendfokozat
        )}</em> ${hatarozatTipus} határozata alapján <em>${
          this.naplobejegyzes.FenyitesHosszaEsTipusa
            ? this.naplobejegyzes.FenyitesHosszaEsTipusa
            : ''
        } ${tipus}</em> került kiszabásra.
        ${
          this.naplobejegyzes.Hatarido != null
            ? ' Határozat dátuma: ' +
              this.$options.filters.toShortDate(this.naplobejegyzes.Hatarido)
            : ''
        }`;
      } else if (
        this.naplobejegyzes.TipusCimkeId ==
          Cimkek.NaploTipus.HatarozatRogziteseMasodFokon &&
        !this.naplobejegyzes.DonteshozoSzemely
      ) {
        return `<em>${camelCaseString(
          this.naplobejegyzes.Torvenyszek
        )} </em> ${hatarozatTipus} határozata alapján <em>${
          this.naplobejegyzes.FenyitesHosszaEsTipusa
            ? this.naplobejegyzes.FenyitesHosszaEsTipusa
            : ''
        } ${tipus}</em> került kiszabásra.${
          this.naplobejegyzes.Hatarido != null
            ? ' Határozat dátuma: ' +
              this.$options.filters.toShortDate(this.naplobejegyzes.Hatarido)
            : ''
        }`;
      }
      return '';
    },
    isHelybenHagya() {
      return this.naplobejegyzes && this.naplobejegyzes.IsHelybenhagyas;
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
