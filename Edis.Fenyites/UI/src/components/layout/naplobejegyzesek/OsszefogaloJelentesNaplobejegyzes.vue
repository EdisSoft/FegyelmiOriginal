<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed aktiv font-weight-400 pl-5 pr-15 py-10 pointer"
        :class="{ inaktiv: naplobejegyzes.InaktivFl == true }"
        v-b-toggle="panelId"
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

        <em>{{ naplobejegyzes.RogzitoSzemely | camelCaseString }}</em>
        elkészítette az összefoglaló jelentést.
        <span v-if="!naplobejegyzes.AlairtaFl"
          >Jelenleg szerkesztés alatt.
        </span>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12 mt-10">
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                v-if="
                  isKivizsgalasFolyamatban &&
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
import { AppStoreTypes } from '../../../store/modules/app';
import { UserStoreTypes } from '../../../store/modules/user';
import { eventBus, apiService } from '../../../main';
import Cimkek from '../../../data/enums/Cimkek';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';
import { NotificationFunctions } from '../../../functions/notificationFunctions';
import { defaultToolTipOptions } from '../../../plugins/bootstrapVue';
//import ApiService from '../../../services/ApiService';

export default {
  name: 'osszefogalo-jelentes-naplobejegyzes',
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
      NyomtatvanyFunctions.OsszefoglaloJelentesDocxNyomtatas({
        naplobejegyzesId: this.naplobejegyzes.Id,
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
    NaploCollapseClick() {
    },
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      };
      eventBus.$emit('Modal:osszefoglalo-jelentes', {
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
      userInfo: UserStoreTypes.getters.getUserInfo,
    }),
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },
    isKivizsgalasFolyamatban() {
      if (!this.fegyelmiUgy) {
        return false;
      }
      return (
        this.fegyelmiUgy.UgyStatuszId ==
        Cimkek.FegyelmiUgyStatusza.KivizsgalasFolyamatban
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

<style scoped>
.aktiv {
  color: #76838f !important;
}

.inaktiv {
  color: #a3afb7 !important;
}
</style>