<template>
  <div class="page-aside-right">
    <div class="page-header py-10 px-15 d-flex align-items-center">
      <h1 class="page-title text-dark mr-15 font-weight-normal">
        &nbsp;
      </h1>
    </div>
    <div class="card">
      <div
        id="statisztika"
        class="vuebar-element withheader right-numbers py-2"
        ref="vuebarscroll"
        v-bar="{
          preventParentScroll: true,
          scrollThrottle: 30,
          resizeRefresh: true,
        }"
      >
        <div>
          <div
            class="card-block p-10 border-0"
            :class="{ 'card-selected': data.isSelected }"
            @click="SelectStatisztika(data.szuro)"
            v-for="data in statisztika"
            :key="data.title"
          >
            <div class="card-block-inner">
              <div class="counter counter-lg">
                <span class="counter-number text-warning">
                  <k-animated-number
                    :value="data.array.length"
                  ></k-animated-number
                ></span>
                <div class="counter-label">{{ data.title }}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import Cimkek from '../../data/enums/Cimkek';
import { UserStoreTypes, jogosultsagok } from '../../store/modules/user';
import { dateSameDay } from '../../utils/date';
import { FegyelmiUgyFunctions } from '../../functions/FegyelmiUgyFunctions';
import StatisztikaSzurok from '../../data/enums/statisztikaSzurok';
import { JutalomUgyStoreTypes } from '../../store/modules/jutalomugy';
import { JutalmiUgyFunctions } from '../../functions/JutalmiUgyFunctions';

export default {
  name: 'jutalmi-statisztika',
  data() {
    return {};
  },
  mounted() {},
  created() {},
  methods: {
    ...mapActions({
      SetjutalomUgyekSzuro: JutalomUgyStoreTypes.actions.setJutalomUgyekSzuro,
    }),
    SelectStatisztika(value) {
      if (this.$route.name != 'JutalomUgyek') {
        setTimeout(() => {
          this.$router.push('/JutalomUgyek/');
        }, 150);
      }
      this.SetjutalomUgyekSzuro({ value });
    },
  },
  computed: {
    ...mapGetters({
      jutalomUgyek: JutalomUgyStoreTypes.getters.getJutalomUgyek,
      user: UserStoreTypes.getters.getUserInfo,
      jutalomUgyekSzuro: JutalomUgyStoreTypes.getters.getJutalomUgyekSzuro,
    }),
    statisztika() {
      let jutalomUgyek = this.jutalomUgyek;
      if (this.jutalomUgyekSzuro >= 0) {
        jutalomUgyek = JutalmiUgyFunctions.GetJutalomUgyekByStatisztikaSzuro(
          jutalomUgyek,
          this.user,
          this.jutalomUgyekSzuro
        );
      }
      let ugyek = {
        isSelected: this.jutalomUgyekSzuro == StatisztikaSzurok.Nyitott,
        szuro: StatisztikaSzurok.Nyitott,
        title: 'Nyitott ügyek',
        array: jutalomUgyek,
      };

      let sajatUgyekArr = [];
      if (this.user) {
        sajatUgyekArr = FegyelmiUgyFunctions.GetSajatUgyeim(
          jutalomUgyek,
          this.user
        );
      }
      let sajatUgyek = {
        isSelected: this.jutalomUgyekSzuro == StatisztikaSzurok.Sajat,
        szuro: StatisztikaSzurok.Sajat,
        title: 'Feladataim',
        array: sajatUgyekArr,
      };
      // let vegrehajtasAlattArr = FegyelmiUgyFunctions.GetVegrehajtasAlattiUgyek(
      //   jutalomUgyek
      // );
      // let vegrehajtasAlatt = {
      //   isSelected:
      //     this.jutalomUgyekSzuro == StatisztikaSzurok.VegrehajtasAlatt,
      //   szuro: StatisztikaSzurok.VegrehajtasAlatt,
      //   title: 'Végrehajtás alatt',
      //   array: vegrehajtasAlattArr,
      // };
      let kesesbenArr = FegyelmiUgyFunctions.GetKesesbenlevoUgyek(jutalomUgyek);
      let kesesben = {
        isSelected: this.jutalomUgyekSzuro == StatisztikaSzurok.Kesesben,
        szuro: StatisztikaSzurok.Kesesben,
        title: 'Késésben',
        array: kesesbenArr,
      };
      let hetenEsedekesArr = FegyelmiUgyFunctions.GetHetenEsedekesUgyek(
        jutalomUgyek
      );
      let hetenEsedekes = {
        isSelected: this.jutalomUgyekSzuro == StatisztikaSzurok.HetenEsedekes,
        szuro: StatisztikaSzurok.HetenEsedekes,
        title: 'Héten esedékes',
        array: hetenEsedekesArr,
      };
      let szallitasraElojegyezveArr = FegyelmiUgyFunctions.GetSzallitasraElojegyezveUgyek(
        jutalomUgyek
      );
      let szallitasraElojegyezve = {
        isSelected:
          this.jutalomUgyekSzuro == StatisztikaSzurok.SzallitasraElojegyezve,
        szuro: StatisztikaSzurok.SzallitasraElojegyezve,
        title: 'Szállításra előjegyezve',
        array: szallitasraElojegyezveArr,
      };
      // let vegrehajtasraVaroArr = FegyelmiUgyFunctions.GetVegrehajtasraVaroUgyek(
      //   jutalomUgyek
      // );
      // let vegrehajtasraVaro = {
      //   isSelected:
      //     this.jutalomUgyekSzuro == StatisztikaSzurok.VegrehajtasraVaro,
      //   szuro: StatisztikaSzurok.VegrehajtasraVaro,
      //   title: 'Végrehajtásra váró',
      //   array: vegrehajtasraVaroArr,
      // };
      return [
        ugyek,
        sajatUgyek,
        kesesben,
        hetenEsedekes,
        // vegrehajtasraVaro,
        // vegrehajtasAlatt,
        szallitasraElojegyezve,
      ];
    },
  },
  watch: {},
  components: {},
};
</script>
<style scoped>
.right-numbers .card-block {
  border: 2px solid rgba(131, 73, 245, 0) !important;
  cursor: pointer;
  transition: 0.5s;
}

.right-numbers .card-block:hover {
  border: 2px solid rgba(131, 73, 245, 0.5) !important;
}

.right-numbers .card-selected {
  border: 2px solid rgba(131, 73, 245, 1) !important;
  transition: 0.5s;
}
</style>
