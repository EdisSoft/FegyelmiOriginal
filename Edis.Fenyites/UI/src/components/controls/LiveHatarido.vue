<template>
  <li class="px-0 mb-0 pointer" :class="{ lejart: diff <= 0 }">
    <div class="media d-flex align-items-center">
      <div class="pr-5">
        <a class="avatar" href="javascript:void(0)">
          <k-fogvatartott-kep
            :id="ugy.FogvatartottId"
          ></k-fogvatartott-kep> 
        </a>
      </div>
      <div class="media-body">
        <div class="pl-0 add-data" :class="{ 'nem-lejart': diff > 0 }">
          <p class="mb-0 text-right">
            <span class="badge badge-outline badge-dark font-weight-400">
              <span v-if="diff < 0" class="basic-span" :key="GetKey()">Lejárt {{ -diff }} napja</span>
              <span v-else-if="diff == 0" class="basic-span" :key="GetKey()">Ma jár le</span>
              <span v-else class="basic-span" :key="GetKey()">Még {{ diff }} nap</span>
            </span>
          </p>
        </div>
        <p class="blue-grey-700 mt-0 mb-0 font-size-12 text-break fogv-nev text-capitalize">
          {{ ugy.FogvatartottNev }}
        </p>
        <p class="mt-0 mb-5">
          <span v-if="ugy.AktNyilvantartasiSzam" class="badge badge-outline mt-1 mr-1 badge-default font-weight-400 text-break">
            {{ ugy.AktNyilvantartasiSzam }}
          </span>
          <span v-else class="badge badge-outline mt-1 mr-1 badge-default font-weight-400 text-break">
            {{ ugy.NyilvantartasiSzam }}
          </span>
          <span v-if="ugy.FegyelmiIntezet" class="badge badge-outline mt-1 mr-1 badge-default font-weight-400 text-break">
            {{ ugy.UgyIntezetAzon }}/{{ ugy.UgyEve }}/{{
              ugy.UgySzama
            }}
          </span>
          <span class="badge badge-outline mt-1 badge-default font-weight-400 text-break"
                v-if="isBvop && ugy.FegyelmiIntezet">
            {{ ugy.FegyelmiIntezet }}
          </span>
          <span class="badge badge-outline mt-1 badge-default font-weight-400 text-break"
                v-if="isBvop && ugy.IntezetRovidNev">
            {{ ugy.IntezetRovidNev }}
          </span>
        </p>
        <p v-if="ugy.UgyStatusz" class="blue-grey-700 mt-0 mb-0 font-size-11 text-break">
          {{ ugy.UgyStatusz }}
        </p>
        <p v-else class="blue-grey-700 mt-0 mb-0 font-size-11 text-break">
          {{ ugy.Statusz }}
        </p>
        <p v-if="ugy.Kivizsgalo"
           class="blue-grey-700 mt-0 mb-0 font-size-11">
          Kiv:
          <span class="text-capitalize">{{ ugy.Kivizsgalo }}</span>
          <span v-if="ugy.KivizsgaloRendfokozat != 'Nincs megadva'">
            {{ ugy.KivizsgaloRendfokozat }}
          </span>
        </p>
        <p v-else-if="ugy.Elrendelo"
           class="blue-grey-700 mt-0 mb-0 font-size-11">
          Elr: <span class="text-capitalize">{{ ugy.Elrendelo }}</span>
          <span v-if="ugy.ElrendeloRendfokozat != 'Nincs megadva'">
            {{ ugy.ElrendeloRendfokozat }}
          </span>
        </p>
        <p v-else-if="ugy.JavaslatTevo"
           class="blue-grey-700 mt-0 mb-0 font-size-11">
          Jav:
          <span class="text-capitalize">{{ ugy.JavaslatTevo }}</span>
          <!--<span v-if="ugy.KivizsgaloRendfokozat != 'Nincs megadva'">
            {{ ugy.KivizsgaloRendfokozat }}
          </span>-->
        </p>
      </div>
    </div>
  </li>
</template>
<script>
import moment from 'moment';
import { dateAdd, dateSubtract, dateDiffInDays } from '../../utils/date';
import { mapGetters } from 'vuex';
import { UserStoreTypes } from '../../store/modules/user';
import Intezetek from '../../data/enums/intezetek';
import { defaultToolTipOptions } from '../../plugins/bootstrapVue';

var livehatartidoCounter = 0;

export default {
  name: 'k-live-hatarido',
    props: ['ugy', 'isBvop'],
  data: function() {
    return {
      currentSettimeout: null,
      diff: 0,
      livehatartidoCounter: livehatartidoCounter++,
      toolTipOptions: {
        ...defaultToolTipOptions,
        container: '#HataridosFeladatok',
      },
    };
  },
  mounted: function() {},
  watch: {
    ugy: {
      handler: function() {
        this.CalculateTimeText();
        var vm = this;
      },
      immediate: true,
    },
  },
  computed: {
    ...mapGetters({
      userInfo: UserStoreTypes.getters.getUserInfo,
    }),
    isUgyJutalom: function () {
      if (this.ugy)
        if (this.ugy.JutalomId)
          return true;
      return false;
    }
  },
  methods: {
    GetKey() {
      return this.livehatartidoCounter + this.timeText;
    },
    CalculateTimeText: function() {
      if (!this.ugy) return;
      var vm = this;

      if (vm.currentSettimeout) clearTimeout(vm.currentSettimeout);

      vm.diff = dateDiffInDays(moment().format(), this.ugy.Hatarido);

      vm.currentSettimeout = setTimeout(function() {
        vm.CalculateTimeText();
      }, 60 * 60 * 1000);
    },
  },
  destroyed: function() {
    if (this.currentSettimeout) clearTimeout(this.currentSettimeout);
  },
};
</script>
<style scoped>
.lejart .media .add-data .badge {
  border-color: #f96868;
  color: #f96868;
}

.nem-lejart,
.nem-lejart p,
.nem-lejart .badge {
  color: #76838f !important;
}

.nem-lejart .badge {
  border-color: #ffc107;
}

.media .badge {
  font-size: 10px;
  padding: 2px 5px;
  color: #666;
  border-color: #ccc;
}

.media-body p {
  line-height: 14px;
}
.media-body .fogv-nev {
  /* max-width: 100px; */
  /* word-break: break-all; */
  margin-bottom: 3px !important;
}
.media-body {
  overflow: none !important;
}
.add-data {
  float: right;
  margin-left: 10px;
}
.media {
  position: relative;
}
.page-aside .list-group-item {
  overflow: unset;
  text-overflow: unset;
  white-space: unset;
}
</style>
