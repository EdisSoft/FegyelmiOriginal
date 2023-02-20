<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        <em>
          {{ naplobejegyzes.TajekoztatastNyujto }}
          {{ naplobejegyzes.TajekoztatastNyujtoRendFokozat }}
          {{ naplobejegyzes.TajekoztatasIdeje | toShortDateKeltezes }}
        </em>
        a védőt telefonon tájékoztatta.
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12">
            <div>
              <span
                class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
                v-b-tooltip="{
                  title: 'Tájékoztatott',
                  ...toolTipOptions,
                }"
                >{{ naplobejegyzes.Tajekoztatott }}:
                {{ naplobejegyzes.Telefonszam }}</span
              >
            </div>
            <div class="col col-12 mt-10">
              <div class="float-right ml-10 mb-10 d-flex">
                <b-button
                  :disabled="isModalOpen"
                  v-if="
                    naplobejegyzes.AlairtaFl != true &&
                      (vanJogkorGyakorloJoga || vanReintegraciosTisztJoga)
                  "
                  class="btn btn-pure btn-dark icon btn-round icon wb-edit pt-5"
                  @click="NaploBejegyzesSzerkesztes"
                >
                </b-button>

                <b-button
                  :disabled="isModalOpen"
                  type="button"
                  @click="VedoTelefonosTajekoztatasaNyomtatas"
                  class="btn btn-pure btn-dark icon wb-print pt-5"
                ></b-button>
              </div>
              <div>
                <div>
                  <span
                    v-html="naplobejegyzes.JegyzokonyvTartalma"
                    class="font-size-12"
                  ></span>
                </div>
              </div>
              <div v-if="naplobejegyzes.TajekoztatasSikertelensegenekOka">
                <p class="font-size-12 text-black">
                  Sikertelenség oka: "{{
                    naplobejegyzes.TajekoztatasSikertelensegenekOka
                  }}"
                </p>
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
  name: 'vedo-telefonos-tajekoztatasa-naplobejegyzes',
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
    VedoTelefonosTajekoztatasaNyomtatas() {
      NyomtatvanyFunctions.VedoTelefonosTajekoztatasaNyomtatasDocX({
        naplobejegyzesId: this.naplobejegyzes.Id,
      });
    },
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
      };
      eventBus.$emit('Modal:vedo-telefonos-tajekoztatasa', {
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
