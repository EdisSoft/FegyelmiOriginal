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
          <div class="col col-12 mt-10">
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                v-if="szerkesztheto"
                :disabled="isModalOpen"
                class="btn btn-pure btn-dark icon btn-round icon wb-edit pt-5"
                @click="NaploBejegyzesSzerkesztes"
              >
              </b-button>
            </div>

            <div class="mb-10">
              <span
                v-html="naplobejegyzes.JegyzokonyvTartalma"
                class="font-size-12"
              ></span>
            </div>
          </div>
        </div>
        <div class="row">
          <k-csatolmanyok :csatolmanyok="feltoltesek"></k-csatolmanyok>
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

export default {
  name: 'szakteruleti-velemeny-naplobejegyzes',
  data() {
    return {
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#slidepanel-fegyelmi-ugy',
      },
      fegyelmiUgyStatuszok: Cimkek.FegyelmiUgyStatusza,
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
      eventBus.$emit('Modal:szakteruleti-velemeny', {
        state: true,
        data,
      });
    },
  },
  computed: {
    ...mapGetters({ isModalOpen: AppStoreTypes.getters.isModalOpen }),
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },
    szerkesztheto() {
      return (
        !this.naplobejegyzes.AlairtaFl &&
        this.fegyelmiUgy.UgyStatuszId ==
          this.fegyelmiUgyStatuszok.KivizsgalasFolyamatban &&
        !this.fegyelmiUgy.Lezarva
      );
    },

    fejlec() {
      if (!this.naplobejegyzes || !this.fegyelmiUgy) {
        return '';
      }

      if (
        this.naplobejegyzes.TipusCimkeId ==
        Cimkek.NaploTipus.SzakteruletiVelemeny
      ) {
        return `<em>${camelCaseString(
          this.naplobejegyzes.JegyzokonyvVezetoSzemely
        )} ${
          this.naplobejegyzes.JegyzokonyvVezetoSzemelyRendfokozat
        }</em> szakterületi véleményt készített <em>${this.$options.filters.toShortDateKeltezes(
          this.naplobejegyzes.JegyzokonyvLezarasDatuma
        )}</em>.`;
      }
      return 'Szakterületi vélemény';
    },
    feltoltesek() {
      if (!this.esemeny || !this.naplobejegyzes) {
        return [];
      }
      return this.esemeny.Feltoltesek.filter(
        x => x.NaploId == this.naplobejegyzes.Id
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
