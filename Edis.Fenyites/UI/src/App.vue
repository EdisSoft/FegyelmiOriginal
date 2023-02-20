<template>
  <div id="app">
    <jfk-header></jfk-header>
    <component :is="layout">
      <router-view></router-view>
    </component>
    <div class="transparent-modal-bg">
      <div v-if="defer(1)">
        <base-modal modalId="jegyzokonyv-tanu-meghallgatas">
          <k-jegyzokonyv-tanumeghallgatas></k-jegyzokonyv-tanumeghallgatas>
        </base-modal>

        <base-modal modalId="jegyzokonyv-szemelyi-allomanyi-tanu-meghallgatas">
          <k-jegyzokonyv-szemelyiallomanyitanumeghallgatas></k-jegyzokonyv-szemelyiallomanyitanumeghallgatas>
        </base-modal>

        <base-modal modalId="fegyelmi-ugy-elrendelese">
          <fegyelmi-ugy-elrendelese></fegyelmi-ugy-elrendelese>
        </base-modal>

        <base-modal modalId="eljaras-megtagadasa">
          <k-eljaras-megtagadasa></k-eljaras-megtagadasa>
        </base-modal>

        <base-modal modalId="jutalom-eloterjesztes">
          <jutalom-eloterjesztes></jutalom-eloterjesztes>
        </base-modal>
      </div>
      <div v-if="deferT(2)">
        <base-modal modalId="helyszini-szemle">
          <helyszini-szemle></helyszini-szemle>
        </base-modal>
        <base-modal modalId="szakteruleti-velemeny">
          <szakteruleti-velemeny></szakteruleti-velemeny>
        </base-modal>
        <base-modal modalId="osszefoglalo-jelentes">
          <k-osszefoglalo-jelentes></k-osszefoglalo-jelentes>
        </base-modal>
        <base-modal modalId="ugy-osszevonas">
          <ugy-osszevonas></ugy-osszevonas>
        </base-modal>
        <base-modal modalId="I-foku-fegyelmi-targyalas-elokeszitese">
          <I-foku-fegyelmi-targyalas-elokeszitese></I-foku-fegyelmi-targyalas-elokeszitese>
        </base-modal>
        <base-modal modalId="reintegracios-tiszt-dontese-visszakuldes">
          <k-reintegracios-tiszt-dontese-visszakuldes></k-reintegracios-tiszt-dontese-visszakuldes>
        </base-modal>
        <base-modal modalId="esemeny-rogzites">
          <esemeny-rogzites></esemeny-rogzites>
        </base-modal>
      </div>
      <div v-if="deferT(2)">
        <base-modal modalId="jegyzokonyv-elso-foku-targyalasi">
          <k-jegyzokonyv-elsofokutargyalasi></k-jegyzokonyv-elsofokutargyalasi>
        </base-modal>
        <base-modal modalId="reintegracios-tiszt-dontese-feddes">
          <reintegracios-tiszt-dontese-feddes></reintegracios-tiszt-dontese-feddes>
        </base-modal>
        <base-modal modalId="reintegracios-tiszt-dontese-kioktatas">
          <reintegracios-tiszt-dontese-kioktatas></reintegracios-tiszt-dontese-kioktatas>
        </base-modal>
        <base-modal modalId="II-foku-fegyelmi-targyalas-elokeszitese">
          <k-II-foku-fegyelmi-targyalas-elokeszitese></k-II-foku-fegyelmi-targyalas-elokeszitese>
        </base-modal>
        <base-modal modalId="hatarozat-rogzitese">
          <k-hatarozat-rogzitese></k-hatarozat-rogzitese>
        </base-modal>
        <base-modal modalId="felfuggesztes-megszuntetes">
          <felfuggesztes-megszuntetes></felfuggesztes-megszuntetes>
        </base-modal>
      </div>
      <div v-if="deferT(2)">
        <base-modal modalId="kirendelt-vedo-kerese">
          <kirendelt-vedo-kerese></kirendelt-vedo-kerese>
        </base-modal>
        <base-modal modalId="meghatalmazott-vedo-kerese">
          <meghatalmazott-vedo-kerese></meghatalmazott-vedo-kerese>
        </base-modal>
        <base-modal modalId="hatarido-modositas">
          <hatarido-modositas></hatarido-modositas>
        </base-modal>
        <base-modal modalId="felfuggesztes">
          <felfuggesztes></felfuggesztes>
        </base-modal>
        <base-modal modalId="vedo-telefonos-tajekoztatasa">
          <vedo-telefonos-tajekoztatasa></vedo-telefonos-tajekoztatasa>
        </base-modal>
        <base-modal modalId="feljegyzes-es-megallapodas">
          <feljegyzes-es-megallapodas></feljegyzes-es-megallapodas>
        </base-modal>
      </div>
      <div v-if="deferT(2)">
        <base-modal modalId="szakteruleti-velemeny-kerese">
          <szakteruleti-velemeny-kerese></szakteruleti-velemeny-kerese>
        </base-modal>
        <base-modal modalId="jegyzokonyv-masodfokutargyalasi">
          <k-jegyzokonyv-masodfokutargyalasi></k-jegyzokonyv-masodfokutargyalasi>
        </base-modal>
        <base-modal modalId="hatarozat-rogzitese-masod-foku">
          <hatarozat-rogzitese-masod-foku></hatarozat-rogzitese-masod-foku>
        </base-modal>
        <base-modal modalId="szembesitesi-jegyzokonyv">
          <szembesitesi-jegyzokonyv></szembesitesi-jegyzokonyv>
        </base-modal>
        <base-modal modalId="maganelzaras-megkezdesenek-rogzitese">
          <maganelzaras-megkezdesenek-rogzitese></maganelzaras-megkezdesenek-rogzitese>
        </base-modal>
      </div>
      <div v-if="deferT(2)">
        <base-modal modalId="fenyites-vegrehajthatatlanna-tetele">
          <fenyites-vegrehajthatatlanna-tetele></fenyites-vegrehajthatatlanna-tetele>
        </base-modal>
        <base-modal modalId="maganelzaras-ideiglenesen-ellenjavallt">
          <maganelzaras-ideiglenesen-ellenjavallt></maganelzaras-ideiglenesen-ellenjavallt>
        </base-modal>
        <base-modal modalId="maganelzaras-megszakitasa">
          <maganelzaras-megszakitasa></maganelzaras-megszakitasa>
        </base-modal>
        <base-modal modalId="maganelzaras-vegrehajtva">
          <maganelzaras-vegrehajtva></maganelzaras-vegrehajtva>
        </base-modal>
        <base-modal modalId="kozvetitoi-eljaras-kezdemenyezese">
          <kozvetitoi-eljaras-kezdemenyezese></kozvetitoi-eljaras-kezdemenyezese>
        </base-modal>
        <base-modal modalId="dontes-kozvetitoi-eljarasrol">
          <dontes-kozvetitoi-eljarasrol></dontes-kozvetitoi-eljarasrol>
        </base-modal>
        <base-modal modalId="bunteto-feljelentes-rogzitese">
          <bunteto-feljelentes-rogzitese></bunteto-feljelentes-rogzitese>
        </base-modal>
      </div>
      <div v-if="deferT(2)">
        <base-modal modalId="garancia-teljesulesenek-rogzitese">
          <garancia-teljesulesenek-rogzitese></garancia-teljesulesenek-rogzitese>
        </base-modal>
        <base-modal modalId="kozvetitoi-eljaras-lezarasa">
          <kozvetitoi-eljaras-lezarasa></kozvetitoi-eljaras-lezarasa>
        </base-modal>
        <base-modal modalId="indoklassal-megszuntetes">
          <indoklassal-megszuntetes></indoklassal-megszuntetes>
        </base-modal>
        <base-modal modalId="fegyelmi-elkulonites-elrendelese">
          <fegyelmi-elkulonites-elrendelese></fegyelmi-elkulonites-elrendelese>
        </base-modal>
        <base-modal modalId="kozvetitoi-eljaras-hatarido-modositas-kerese">
          <kozvetitoi-eljaras-hatarido-modositas-kerese></kozvetitoi-eljaras-hatarido-modositas-kerese>
        </base-modal>
        <base-modal modalId="kozvetitoi-eljaras-hatarido-modositas">
          <kozvetitoi-eljaras-hatarido-modositas></kozvetitoi-eljaras-hatarido-modositas>
        </base-modal>
      </div>
      <div v-if="deferT(2)">
        <base-modal modalId="javaslat-uj-hataridore">
          <javaslat-uj-hataridore></javaslat-uj-hataridore>
        </base-modal>
        <base-modal modalId="javaslat-felfuggesztesre">
          <javaslat-felfuggesztesre></javaslat-felfuggesztesre>
        </base-modal>
        <base-modal modalId="rendszerbeallitasok" dialog-class="">
          <rendszerbeallitasok></rendszerbeallitasok>
        </base-modal>
        <base-modal modalId="maganelzaras-elrendelese">
          <maganelzaras-elrendelese></maganelzaras-elrendelese>
        </base-modal>
        <base-modal modalId="fegyelmi-ugy-kivizsgalo-modositasa">
          <fegyelmi-ugy-kivizsgalo-modositasa></fegyelmi-ugy-kivizsgalo-modositasa>
        </base-modal>
        <base-modal modalId="szabadszoveges-naplobejegyzes-rogzitese">
          <szabadszoveges-naplobejegyzes-rogzitese></szabadszoveges-naplobejegyzes-rogzitese>
        </base-modal>
        <base-modal modalId="ugyeszi-hatalyon-kivul-helyezes">
          <ugyeszi-hatalyon-kivul-helyezes></ugyeszi-hatalyon-kivul-helyezes>
        </base-modal>
      </div>
      <div v-if="deferT(3)">
        <base-modal modalId="jutalom-felkeres-velemenyezesre">
          <jutalom-felkeres-velemenyezesre></jutalom-felkeres-velemenyezesre>
        </base-modal>
        <base-modal modalId="jutalom-dontes-vagy-javaslat">
          <jutalom-dontes-vagy-javaslat></jutalom-dontes-vagy-javaslat>
        </base-modal>
        <base-modal modalId="jutalom-velemeny-megadasa">
          <jutalom-velemeny-megadasa></jutalom-velemeny-megadasa>
        </base-modal>
        <base-modal modalId="jutalom-ugy-osszevonas">
          <jutalom-ugy-osszevonas></jutalom-ugy-osszevonas>
        </base-modal>
      </div>
    </div>
  </div>
</template>

<script>
import { apiService, eventBus } from './main';
import JfkHeader from './components/layout/JfkHeader.vue';
import DeferMixin from './mixins/Defer';
import store from './store';
import { AppStoreTypes } from './store/modules/app';
import { timeout } from './utils/common';
import { FegyelmiUgyFunctions } from './functions/FegyelmiUgyFunctions';
const default_layout = 'default';

export default {
  name: 'app',
  computed: {
    layout() {
      return (this.$route.meta.layout || default_layout) + '-layout';
    },
  },
  created() {
    this.$root.$on('bv::modal::show', this.SetModalOpenTrue);
    this.$root.$on('bv::modal::hide', this.SetModalOpenFalse);
  },
  beforeDestroy() {
    this.$root.$off('bv::modal::show', this.SetModalOpenTrue);
    this.$root.$off('bv::modal::hide', this.SetModalOpenFalse);
  },
  mounted() {
    this.GetAdatok();
    eventBus.$on('IntezetValasztas', () => {
      this.AdatokFrissitese();
    });
  },
  methods: {
    async GetAdatok() {
      //await apiService.GetFenyitesek();
      await timeout(1000);
      await apiService.RefreshAktivitasFolyam();
    },
    async AdatokFrissitese() {
      //await timeout(500);
      await Promise.all([
        apiService.GetFegyelmiUgyek(),
        apiService.GetJutalomUgyek(),
      ]);
      await timeout(1000);
      await apiService.GetEsemenyek();
      await timeout(1000);
      //await apiService.GetFenyitesek();
      //await timeout(1000);
      await apiService.RefreshAktivitasFolyam();
    },
    SetModalOpenTrue() {
      store.dispatch(AppStoreTypes.actions.setModalOpen, { value: true });
    },
    SetModalOpenFalse() {
      store.dispatch(AppStoreTypes.actions.setModalOpen, { value: false });
    },
  },
  components: {
    JfkHeader,
  },
  mixins: [DeferMixin()],
};
</script>

<style lang="scss">
#app {
  height: 100%;
}
</style>
