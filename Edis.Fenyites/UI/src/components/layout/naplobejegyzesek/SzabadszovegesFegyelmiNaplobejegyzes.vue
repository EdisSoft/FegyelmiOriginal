<template>
  <div class="panel" v-if="naplobejegyzes">
    <div class="panel-heading" :id="panelId" role="tab">
      <a
        class="panel-title collapsed blue-grey-500 font-weight-400 pl-5 pr-15 py-10 pointer"
        v-b-toggle="panelId"
      >
        <span
          v-html="
            naplobejegyzes.JegyzokonyvTartalma.replace(
              /<br\s*\/?>/gi,
              ' '
            ).substring(0, 100) +
              (naplobejegyzes.JegyzokonyvTartalma.length > 100 ? '...' : '')
          "
          class="font-size-12"
        ></span>
      </a>
    </div>
    <b-collapse
      :id="panelId"
      @show="NaploCollapseClick()"
      :visible="velemenyVisible"
    >
      <div class="panel-body text-default pt-0">
        <div class="row">
          <div class="col col-12 mt-10">
            <div class="float-right ml-10 mb-10 d-flex">
              <b-button
                :disabled="isModalOpen"
                class="btn btn-pure btn-dark icon btn-round icon wb-edit pt-5"
                @click="NaploBejegyzesSzerkesztes"
                v-if="szerkesztheto"
              >
              </b-button>

              <b-button
                v-if="
                  naplobejegyzes.FenyitesTipusCimke != null && velemenyVisible
                "
                :disabled="isModalOpen"
                type="button"
                @click="NyomtatasElsoFok"
                class="btn btn-pure btn-dark icon wb-print pt-5 elsofokprint"
                v-b-tooltip="{
                  title: 'Elsőfokú határozat nyomtatása',
                  ...toolTipOptions,
                }"
              ></b-button>
              <b-button
                v-if="
                  naplobejegyzes.FenyitesTipusCimke != null && velemenyVisible
                "
                :disabled="isModalOpen"
                type="button"
                @click="NyomtatasMasodFok"
                class="btn btn-pure btn-dark icon wb-print pt-5 masodfokprint"
                v-b-tooltip="{
                  title: 'Másodfokú határozat nyomtatása',
                  ...toolTipOptions,
                }"
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
import { eventBus } from '../../../main';
import Cimkek from '../../../data/enums/Cimkek';
import moment from 'moment';
import { defaultToolTipOptions } from '../../../plugins/bootstrapVue';
import { NyomtatvanyFunctions } from '../../../functions/nyomtatvanyFunctions';
import { NotificationFunctions } from '../../../functions/notificationFunctions';

export default {
  name: 'szabadszavas-fegyelmi-naplobejegyzes',
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
    async NyomtatasElsoFok() {
      if (!this.naplobejegyzes) {
        return;
      }
      try {
        await NyomtatvanyFunctions.FegyelmiHatarozatNyomtatas({
          fegyelmiUgyId: this.naplobejegyzes.FegyelmiUgyId,
          archivFok: 1,
        });
      } catch (error) {
        NotificationFunctions.AjaxError({
          errorObj: error,
        });
      }
    },
    async NyomtatasMasodFok() {
      if (!this.naplobejegyzes) {
        return;
      }
      try {
        await NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatNyomtatas({
          fegyelmiUgyIds: [this.naplobejegyzes.FegyelmiUgyId],
        });
      } catch (error) {
        NotificationFunctions.AjaxError({
          errorObj: error,
        });
      }
    },
    NaploCollapseClick() {},
    NaploBejegyzesSzerkesztes() {
      var data = {
        fegyelmiUgyIds: [this.fegyelmiUgy.FegyelmiUgyId],
        naplobejegyzesIds: [this.naplobejegyzes.Id],
        //naploId: this.naplobejegyzes.Id,
        leiras: this.naplobejegyzes.Leiras,
      };
      eventBus.$emit('Modal:szabadszoveges-naplobejegyzes-rogzitese', {
        state: true,
        data,
      });
    },
  },
  computed: {
    ...mapGetters({
      userInfo: UserStoreTypes.getters.getUserInfo,
      isModalOpen: AppStoreTypes.getters.isModalOpen,
      vanReintegraciosTisztJoga:
        UserStoreTypes.getters.vanReintegraciosTisztJoga,
    }),
    panelId() {
      return this.$options.name + '_' + this.naplobejegyzes.Id;
    },
    szerkesztheto() {
      console.log('userInfo', this.userInfo);
      console.log('naplobejegyzes', this.naplobejegyzes);
      if (
        !moment(this.naplobejegyzes.LetrehozasDatuma).isSame(moment(), 'day') //nem a mai napon
      ) {
        return false;
      }
      if (this.naplobejegyzes.EgyebAdatokJson != this.userInfo.SzemelyzetSid) {
        //nem a rögzítő az
        return false;
      }
      return true;
    },
    velemenyVisible() {
      if (this.fegyelmiUgy.FegyelmiUgyId < 0) {
        return true;
      } else {
        return false;
      }
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
