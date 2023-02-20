<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        :class="{ inaktiv: naplobejegyzes.InaktivFl == true }"
        v-b-toggle="panelId"
        v-if="!naplobejegyzes.AlairtaFl"
      >
        <span
          v-if="naplobejegyzes.InaktivFl == true"
          class="blue-grey-400"
          v-b-tooltip="{
            title: 'Inaktív ',
            ...toolTipOptions,
          }"
        >
          <i class="icon wb-trash" />&nbsp;
        </span>
        <!-- TODO: backend -->
        {{ naplobejegyzes.Szembesitett1AzonKod }}
        {{ naplobejegyzes.Szembesitett1Neve | camelCaseString }}
        {{ szembesitett1Tipus }} és
        {{ naplobejegyzes.Szembesitett2AzonKod }}
        {{ naplobejegyzes.Szembesitett2Neve | camelCaseString }}
        {{ szembesitett2Tipus }} szembesítése megtörtént, de a jegyzőkönyvet nem
        írták alá.
      </a>
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
        v-else
      >
        <span
          v-if="naplobejegyzes.InaktivFl == true"
          class="blue-grey-400"
          v-b-tooltip="{
            title: 'Inaktív ',
            ...toolTipOptions,
          }"
        >
          <i class="icon wb-trash" />&nbsp;
        </span>
        <!-- TODO: backend -->
        {{ naplobejegyzes.Szembesitett1AzonKod }}
        {{ naplobejegyzes.Szembesitett1Neve | camelCaseString }}
        {{ szembesitett1Tipus }} és
        {{ naplobejegyzes.Szembesitett2AzonKod }}
        {{ naplobejegyzes.Szembesitett2Neve | camelCaseString }}
        {{ szembesitett2Tipus }} szembesítése megtörtént.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12">
            <span
              v-if="naplobejegyzes.MeghallgatoSzemely"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Meghallgató',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.MeghallgatoSzemely | camelCaseString }}
              {{ naplobejegyzes.MeghallgatoSzemelyRendfokozat }}</span
            >
            <span
              v-if="naplobejegyzes.JegyzokonyvVezetoSzemely"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'Jegyzőkönyv vezető',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.JegyzokonyvVezetoSzemely | camelCaseString }}
              {{ naplobejegyzes.JegyzokonyvVezetoSzemelyRendfokozat }}</span
            >
            <span
              v-for="tovabbiJelenlevo in tovabbiJelenlevok"
              :key="tovabbiJelenlevo"
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-b-tooltip="{
                title: 'További jelenlévő',
                ...toolTipOptions,
              }"
              >{{ tovabbiJelenlevo | camelCaseString }}</span
            >
          </div>
          <div class="col col-12 mt-10">
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                v-if="
                  isStatuszSzerkesztheto &&
                  !naplobejegyzes.AlairtaFl &&
                  vanReintegraciosTisztJoga
                "
                :disabled="isModalOpen"
                class="btn btn-pure btn-dark icon btn-round icon wb-edit pt-5"
                @click="NaploBejegyzesSzerkesztes"
              >
              </b-button>
              <!--Naplóbejegyzés inaktiválás-->
              <b-button
                v-if="
                  naplobejegyzes.InaktivFl != true &&
                  naplobejegyzes.RogzitoSzemelyId == userInfo.Id
                "
                :disabled="isModalOpen"
                v-b-tooltip="{
                  title: 'Naplóbejegyzés inaktiválása',
                  ...toolTipOptions,
                }"
                type="button"
                @click="NaploBejegyzesInaktivalasa"
                class="btn btn-pure btn-dark icon wb-trash pt-5"
              ></b-button>
              <!--Naplóbejegyzés aktiválás-->
              <b-button
                v-if="
                  naplobejegyzes.InaktivFl == true &&
                  naplobejegyzes.RogzitoSzemelyId == userInfo.Id
                "
                :disabled="isModalOpen"
                v-b-tooltip="{
                  title: 'Naplóbejegyzés visszanyitása',
                  ...toolTipOptions,
                }"
                type="button"
                @click="NaploBejegyzesAktivalasa"
                class="btn btn-pure btn-dark icon wb-refresh pt-5"
              ></b-button>
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
import { eventBus, apiService } from '../../../main';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';
import { AppStoreTypes } from '../../../store/modules/app';
import { UserStoreTypes } from '../../../store/modules/user';
import { camelCaseString } from '../../../utils/common';
import { NotificationFunctions } from '../../../functions/notificationFunctions';

export default {
  name: 'szembesitesi-jegyzokonyv-naplobejegyzes',
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
      if (!this.naplobejegyzes) {
        return;
      }
      NyomtatvanyFunctions.SzembesitesiJegyzokonyvNyomtatas({
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      });
    },
    NaploBejegyzesInaktivalasa() {
      if (!this.naplobejegyzes) {
        return;
      }
      apiService.NaploBejegyzesInaktivalasa({
        naploBejegyzesId: this.naplobejegyzes.Id,
      });
      NotificationFunctions.SuccessAlert(
        'Naplóbejegyzés',
        'A naplóbejegyzés inaktiválásra került'
      );
      eventBus.$emit('Sidebar:ugyReszletek:refresh');
    },
    NaploBejegyzesAktivalasa() {
      if (!this.naplobejegyzes) {
        return;
      }
      apiService.NaploBejegyzesAktivalasa({
        naploBejegyzesId: this.naplobejegyzes.Id,
      });
      NotificationFunctions.SuccessAlert(
        'Naplóbejegyzés',
        'A naplóbejegyzés aktiválásra került'
      );
      eventBus.$emit('Sidebar:ugyReszletek:refresh');
      //deselectDatatable();
    },
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      };
      eventBus.$emit('Modal:szembesitesi-jegyzokonyv', {
        state: true,
        data,
      });
    },
    GetNaploBejegyzesIds: function () {
      var result = apiService.GetKozosNaploBejegyzesIds({
        data: this.fegyelmiUgy.FegyelmiUgyId.Id,
      });
      return result;
    },
  },
  computed: {
    ...mapGetters({
      isModalOpen: AppStoreTypes.getters.isModalOpen,
      vanReintegraciosTisztJoga:
        UserStoreTypes.getters.vanReintegraciosTisztJoga,
      userInfo: UserStoreTypes.getters.getUserInfo,
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
    isStatuszSzerkesztheto() {
      if (!this.fegyelmiUgy) {
        return false;
      }
      return (
        this.fegyelmiUgy.UgyStatuszId ==
          Cimkek.FegyelmiUgyStatusza.KivizsgalasFolyamatban ||
        this.fegyelmiUgy.UgyStatuszId ==
          Cimkek.FegyelmiUgyStatusza.IFokuTargyalas
      );
    },
    szembesitett1Tipus() {
      let szembesitettTipus;
      if (
        this.fegyelmiUgy.FogvatartottId ==
        this.naplobejegyzes.Szembesitett1FogvatartottId
      ) {
        szembesitettTipus = 'eljárás alá vont';
      } else {
        szembesitettTipus = 'tanú';
      }
      console.log(szembesitettTipus, 'szembesitett1tipus');
      return szembesitettTipus;
    },
    szembesitett2Tipus() {
      let szembesitettTipus;
      if (
        this.fegyelmiUgy.FogvatartottId ==
        this.naplobejegyzes.Szembesitett2FogvatartottId
      ) {
        szembesitettTipus = 'eljárás alá vont';
      } else {
        szembesitettTipus = 'tanú';
      }
      console.log(szembesitettTipus, 'szembesitett2tipus');
      return szembesitettTipus;
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

<style scoped>
.aktiv {
  color: #76838f !important;
}

.inaktiv {
  color: #a3afb7 !important;
}
</style>