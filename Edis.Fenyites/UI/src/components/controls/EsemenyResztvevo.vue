<template>
  <div :id="'row_' + resztvevo.FogvatartottId">
    <div class="media align-items-center">
      <div class="pr-20">
        <a class="avatar" href="javascript:void(0)">
          <k-fogvatartott-kep
            :id="resztvevo.FogvatartottId"
          ></k-fogvatartott-kep>
          <!-- <img class="img-fluid" src="../../global/portraits/1.jpg" alt="..."> -->
        </a>
      </div>
      <div class="media-body">
        <h5 class="mt-0 mb-5 blue-grey-700 font-weight-300">
          {{ resztvevo.NyilvantartasiAzonosito }} -
          {{ resztvevo.FogvatartottNev }}
          <span class="font-size-14 blue-grey-400">
            {{ resztvevo.ErintettsegFokaNev }}
          </span>
        </h5>
        <span class="blue-grey-500  font-weight-300">
          <span v-if="resztvevo.Bizonyitek">{{ resztvevo.Bizonyitek }}</span>
          <span v-else>Bizonyíték megadása</span>
        </span>
      </div>
      <div class="pl-20">
        <span
          v-if="resztvevo.FegyelmiUgyszam"
          class="badge badge-outline badge-warning"
          >{{ resztvevo.FegyelmiUgyszam }}</span
        >
        <button
          v-else
          type="button"
          :id="getfegyelmiInditasaId"
          class="btn btn-outline btn-info btn-xs"
          @click="FegyelmiUgyInditasaConfirm()"
        >
          Fegyelmi ügy kezdeményezése
        </button>
        <button
          @click="DeleteResztvevoConfirm()"
          type="button"
          :id="getResztvevokTorlesId"
          class="btn btn-pure btn-default icon wb-trash resztvevotorles"
        ></button>
      </div>
    </div>
    <b-popover
      :target="getfegyelmiInditasaId"
      triggers="null"
      :show.sync="fegyelmiInditasaConfirm.isShow"
      placement="topleft"
      :container="'row_' + resztvevo.FogvatartottId"
      ref="confirmPopover"
      custom-class="ujResztvevoPopover"
    >
      <template slot="title">
        <b-button
          @click="CloseFegyelmiInditasConfirm()"
          class="close"
          aria-label="Close"
        >
          <span class="d-inline-block white" aria-hidden="true">&times;</span>
        </b-button>
        Megerősítés
      </template>
      <div class="pb-5">
        <div class="form-group form-material  mb-10" data-plugin="formMaterial">
          {{ fegyelmiInditasaConfirm.confirmText }}
        </div>
        <div class="text-right">
          <b-button
            size="sm"
            @click="CloseFegyelmiInditasConfirm()"
            variant="default"
            class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
            >Mégse</b-button
          >
          <b-button
            size="sm"
            @click="FegyelmiUgyInditasa()"
            variant="info"
            class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
            >Kezdeményezem</b-button
          >
        </div>
      </div>
    </b-popover>
    <b-popover
      :target="getResztvevokTorlesId"
      triggers="null"
      :show.sync="deleteResztvevoConfirm.isShow"
      placement="topleft"
      :container="'row_' + resztvevo.FogvatartottId"
      ref="confirmPopover"
      custom-class="ujResztvevoPopover"
    >
      <template slot="title">
        <b-button
          @click="CloseDeleteResztvevoConfirm()"
          class="close"
          aria-label="Close"
        >
          <span class="d-inline-block white" aria-hidden="true">&times;</span>
        </b-button>
        Megerősítés
      </template>
      <div class="pb-5">
        <div class="form-group form-material  mb-10" data-plugin="formMaterial">
          {{ deleteResztvevoConfirm.confirmText }}
        </div>
        <div class="text-right">
          <b-button
            size="sm"
            @click="CloseDeleteResztvevoConfirm()"
            variant="default"
            class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
            >Mégse</b-button
          >
          <b-button
            size="sm"
            @click="DeleteResztvevo()"
            variant="info"
            class="font-size-14 nyugtaz-ok-button btn-raised font-weight-700"
            >Törlöm</b-button
          >
        </div>
      </div>
    </b-popover>
  </div>
</template>

<script>
import $ from 'jquery';
import { apiService } from '../../main';

export default {
  name: 'k-esemeny-resztvevo',
  props: {
    resztvevo: {
      type: Object,
      default: function() {
        return {};
      },
    },
  },
  data: function() {
    return {
      fegyelmiInditasaConfirm: {
        isShow: false,
        confirmText: '',
      },
      deleteResztvevoConfirm: {
        isShow: false,
        confirmText: '',
      },
    };
  },
  mounted() {
    let vm = this;
  },
  methods: {
    GetResztvevoKey: function(fogvatartottId) {
      return 'resztvevo_' + fogvatartottId;
    },
    FegyelmiUgyInditasaConfirm: function() {
      this.$root.$emit('bv::hide::popover');
      this.fegyelmiInditasaConfirm.isShow = true;

      this.fegyelmiInditasaConfirm.confirmText = `Biztos fegyelmit kezdeményez ${this.resztvevo.FogvatartottNev} ${this.resztvevo.NyilvantartasiAzonosito} fogvatartott ellen?`;
    },
    FegyelmiUgyInditasa: async function() {
      var vm = this;
      this.fegyelmiInditasaConfirm.isShow = false;

      var result = await apiService.FegyelmiUgyInditasa({
        fogvatartottId: this.resztvevo.FogvatartottId,
        esemenyId: this.resztvevo.EsemenyId,
      });

      vm.$emit('fegyelmiinditas', {
        fogvatartottId: this.resztvevo.FogvatartottId,
        ugyszam: result.ugyszam,
      });
    },
    CloseFegyelmiInditasConfirm: function() {
      this.fegyelmiInditasaConfirm.isShow = false;
    },
    CloseDeleteResztvevoConfirm: function() {
      this.deleteResztvevoConfirm.isShow = false;
    },
    DeleteResztvevoConfirm: function() {
      this.$root.$emit('bv::hide::popover');
      this.deleteResztvevoConfirm.isShow = true;
      this.deleteResztvevoConfirm.confirmText = `Biztos törli ${this.resztvevo.FogvatartottNev} ${this.resztvevo.NyilvantartasiAzonosito} fogvatartottat?`;
    },
    DeleteResztvevo: async function() {
      var vm = this;
      this.deleteResztvevoConfirm.isShow = false;
      console.log(
        'DeleteResztvevo() - this.resztvevo.FogvatartottId:' +
          this.resztvevo.FogvatartottId
      );

      var result = await apiService.DeleteResztvevo({
        fogvatartottId: this.resztvevo.FogvatartottId,
        esemenyId: this.resztvevo.EsemenyId,
      });

      vm.$emit('resztvevotorles', this.resztvevo.FogvatartottId);
    },
  },
  computed: {
    getfegyelmiInditasaId: function() {
      return 'fegyelmi_' + this.GetResztvevoKey(this.resztvevo.FogvatartottId);
    },
    getResztvevokTorlesId: function() {
      return 'torles_' + this.GetResztvevoKey(this.resztvevo.FogvatartottId);
    },
  },
};
</script>
