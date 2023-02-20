<template>
  <div class="panel" v-if="fegyelmiUgy && naplobejegyzes">
    <div class="panel-heading" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        :class="{ disabled: !vanTartalom }"
        v-b-toggle="panelId"
      >
        <!-- TODO: backend -->
        <div class="row mx-0">
          <div class="col col-12 px-0 d-flex justify-content-between">
            <div
              v-if="
                isElkulonitett &&
                  naplobejegyzes.ElkulonitesMegszuntetesenekIdeje == null
              "
            >
              A fegyelmi jogkör gyakorlója a fegyelmi elkülönítést módosította.
            </div>
            <div
              v-else-if="
                isElkulonitett &&
                  naplobejegyzes.ElkulonitesMegszuntetesenekIdeje != null
              "
            >
              A fegyelmi jogkör gyakorlója a fegyelmi elkülönítést megszüntette
              <em>{{
                naplobejegyzes.ElkulonitesMegszuntetesenekIdeje | toDateTime
              }}</em>
              -kor.
            </div>
            <div v-else-if="!isElkulonitett">
              A fegyelmi jogkör gyakorlója fegyelmi elkülönítést rendelt el
              <em>{{
                naplobejegyzes.ElkulonitesElrendelesIdeje | toDateTime
              }}</em>
              -kor.
            </div>
            <!-- <div>
              <b-button
                :disabled="isModalOpen"
                type="button"
                class="btn btn-pure btn-dark icon wb-print pt-5"
                @click="Nyomtatas"
              ></b-button>
            </div> -->
          </div>
        </div>
      </a>
    </div>
    <b-collapse :id="panelId" @show="NaploCollapseClick()">
      <div class="panel-body text-default pt-0" v-if="vanTartalom">
        <div class="row mb-10">
          <!-- <div class="col col-12 mt-10"> -->
          <div class="col col-xl-11 col-lg-10">
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-if="naplobejegyzes.JegyzokonyvTartalma"
              v-b-tooltip="{
                title: 'Elhelyezés',
                ...toolTipOptions,
              }"
              >{{ naplobejegyzes.JegyzokonyvTartalma }}</span
            >
            <span
              class="badge badge-outline badge-dark mr-5 bg-white font-weight-400 shadow-sm"
              v-if="naplobejegyzes.ElkulonitesFelulvizsgalvaFL"
              v-b-tooltip="{
                title: 'Felülvizsgálva',
                ...toolTipOptions,
              }"
              >Felülvizsgálva</span
            >
          </div>
          <div class="col col-xl-1 col-lg-2 float-right">
            <b-button
              :disabled="isModalOpen"
              type="button"
              @click="Nyomtatas"
              class="btn btn-pure btn-dark icon wb-print pt-5"
            ></b-button>
          </div>
          <!-- </div> -->
        </div>
        <div class="row">
          <div v-if="naplobejegyzes.ElkulonitesOka" class="col col-12 mt-10">
            <h6 class="mb-0">Elkülönítés oka:</h6>
            <div class="mb-10">
              <span
                v-html="naplobejegyzes.ElkulonitesOka"
                class="font-size-12"
              ></span>
            </div>
          </div>
          <!-- <div v-if="naplobejegyzes.JegyzokonyvTartalma" class="col col-12">
            <h6 class="mb-0">Elhelyezés:</h6>
            <div class="mb-10">
              <span
                v-html="naplobejegyzes.JegyzokonyvTartalma"
                class="font-size-12"
              ></span>
            </div>
          </div> -->
          <div
            v-if="naplobejegyzes.ElkulonitesRendelkezesek"
            class="col col-12"
          >
            <h6 class="mb-0">Vonatkozó rendelkezések:</h6>
            <div class="mb-10">
              <span
                v-html="naplobejegyzes.ElkulonitesRendelkezesek"
                class="font-size-12"
              ></span>
            </div>
          </div>
          <!-- <div
            v-if="naplobejegyzes.ElkulonitesFelulvizsgalvaFL"
            class="col col-12"
          >
            <h6 class="mb-0">Elkülönítés felülvizsgálva.</h6>
          </div> -->
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
import { NyomtatvanyFunctions } from '../../../../functions/nyomtatvanyFunctions';
import { defaultToolTipOptions } from '../../../../plugins/bootstrapVue';
import { AppStoreTypes } from '../../../../store/modules/app';
import { mapGetters } from 'vuex';
import moment from 'moment';

export default {
  name: 'fegyelmi-elkulonites-elrendelese-naplo-bejegyzes',
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
      NyomtatvanyFunctions.ElkulonitoLapNyomtatas({
        fegyelmiUgyId: this.naplobejegyzes.FegyelmiUgyId,
      });
    },
  },
  computed: {
    ...mapGetters({
      isModalOpen: AppStoreTypes.getters.isModalOpen,
    }),
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },
    vanTartalom() {
      if (!this.naplobejegyzes) {
        return false;
      }
      return (
        this.naplobejegyzes.ElkulonitesOka ||
        this.naplobejegyzes.ElkulonitesRendelkezesek
      );
    },
    isElkulonitett() {
      if (this.naplobejegyzes.IsFogvatartottElfogadta == true) {
        return true;
      }
      return false;
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
