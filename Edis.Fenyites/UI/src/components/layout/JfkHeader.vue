<template>
  <div>
    <nav
      class="site-navbar navbar navbar-default navbar-fixed-top navbar-mega navbar-inverse"
      :class="
        $route.meta.layout == 'jutalom' ? 'bg-green-600' : 'bg-blue-grey-600'
      "
      role="navigation"
    >
      <div class="navbar-header">
        <button
          type="button"
          class="navbar-toggler hamburger hamburger-close navbar-toggler-left hided"
          data-toggle="menubar"
        >
          <span class="sr-only">Toggle navigation</span>
          <span class="hamburger-bar"></span>
        </button>
        <button
          type="button"
          class="navbar-toggler collapsed"
          data-target="#site-navbar-collapse"
          data-toggle="collapse"
        >
          <i class="icon wb-more-horizontal" aria-hidden="true"></i>
        </button>
        <a
          id="JFKLogo"
          class="navbar-brand navbar-brand-center"
          href="javascript:;"
        >
          <img
            class="navbar-brand-logo navbar-brand-logo-normal jfk-logo-margin-top-0 mr-20"
            src="@/assets/images/topbar/logo.png"
            title="Noemi"
          />
          <img
            class="navbar-brand-logo navbar-brand-logo-special mr-20"
            src="@/assets/images/topbar/logo-colored.png"
            title="Noemi"
          />
          <transition-group name="list-complete">
            <span
              role="button"
              v-for="item in fejlecMenupontok"
              :key="item.Id"
              @click="item.ClickEvent"
              class="list-complete-item jfk-header-item"
              :class="{ active: item.Active }"
            >
              {{ item.Nev }}
            </span>
          </transition-group>
        </a>
        <button
          type="button"
          class="navbar-toggler collapsed"
          data-target="#site-navbar-search"
          data-toggle="collapse"
        >
          <span class="sr-only">Toggle Search</span>
          <i class="icon wb-search" aria-hidden="true"></i>
        </button>
      </div>
      <div class="navbar-container container-fluid">
        <div
          class="collapse navbar-collapse navbar-collapse-toolbar"
          id="site-navbar-collapse"
        >
          <ul
            id="jobbfelsosarok"
            class="nav navbar-toolbar navbar-right navbar-toolbar-right"
          >
            <li
              class="nav-item dropdown"
              v-if="VirKimutatasFegyelmiUrl || VirKimutatasJutalomUrl"
            >
              <a
                class="nav-link"
                v-bind:href="
                  $route.meta.layout == 'fenyites'
                    ? VirKimutatasFegyelmiUrl
                    : VirKimutatasJutalomUrl
                "
                role="button"
                target="_blank"
              >
                <i class="icon wb-stats-bars mr-10"></i>&nbsp;Statisztika
              </a>
            </li>
            <li
              class="nav-item dropdown"
              v-if="$route.meta.layout == 'fenyites'"
            >
              <a
                class="nav-link"
                data-toggle="dropdown"
                href="javascript:void(0)"
                data-animation="scale-up"
                aria-expanded="false"
                role="button"
              >
                <a @click="startIntroJs()">Bemutató</a>
              </a>
            </li>
            <li
              class="nav-item dropdown"
              v-if="
                userInfo &&
                userInfo.Jogkor &&
                userInfo.Jogkor.Nev &&
                $route.meta.layout == 'jutalom'
              "
            >
              <a
                class="nav-link"
                data-toggle="dropdown"
                href="javascript:void(0)"
                data-animation="scale-up"
                aria-expanded="false"
                role="button"
              >
                <a>{{ userInfo.Jogkor.Nev }}</a>
              </a>
            </li>
            <li class="dropdown nav-item" role="presentation">
              <b-dropdown
                size="lg"
                variant="link"
                :disabled="intezetValtasLoading"
                toggle-class="text-decoration-none white font-size-14 nav-link py-0 pl-0 pr-10 border-radius-0"
                class="h-p100"
              >
                <template v-slot:button-content>
                  <b-button variant="link" class="white text-decoration-none">
                    <b-spinner class="mb-3" v-if="false" small></b-spinner>
                    {{ userInfo.RogzitoIntezet.RovidNev }}
                  </b-button>
                </template>
                <div
                  :class="{
                    'vuebar-element header-ertesitesek':
                      valaszthatoIntezetek.length > 7,
                  }"
                  v-bar="{
                    preventParentScroll: true,
                    scrollThrottle: 30,
                    resizeRefresh: true,
                  }"
                >
                  <div>
                    <b-dropdown-item
                      href="#"
                      v-for="intezet in valaszthatoIntezetek"
                      :key="intezet.Id"
                      @click.native="IntezetValasztas(intezet.Id)"
                      :class="{
                        active: intezet.Id == userInfo.RogzitoIntezet.Id,
                      }"
                    >
                      {{ intezet.Nev }}
                    </b-dropdown-item>
                  </div>
                </div>
              </b-dropdown>
            </li>

            <li
              id="bejelentkezettFelhasznalo"
              class="nav-item"
              role="presentation"
            >
              <a class="nav-link" href="javascript:void(0)"
                >Üdv, {{ userInfo.SzemelyzetNev }}</a
              >
            </li>
            <li>
              <k-n2020toolbar></k-n2020toolbar>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <div
      class="site-menubar site-menubar-light"
      v-if="$route.meta.layout == 'fenyites'"
    >
      <div class="site-menubar-body d-flex align-items-center">
        <div id="fomenu">
          <ul class="site-menu d-inline-block px-0" data-plugin="menu">
            <!--<li class="site-menu-category">General</li>-->

            <router-link
              :to="
                link.isArchive && ugyEve
                  ? `/ArchivUgyek/?ev=${ugyEve}`
                  : link.url
              "
              tag="li"
              class="dropdown site-menu-item"
              active-class="active"
              :exact="link.isExact != false"
              v-for="link in fegyelmiLinks"
              :key="link.url"
              :class="{
                active: setArchiveLink && link.isArchive,
                disabled: link.isDisabled,
              }"
            >
              <a href="javascript:;" class="site-menu-item-link">
                <i
                  class="site-menu-icon mr-2"
                  :class="link.icon"
                  aria-hidden="true"
                ></i>
                <span class="site-menu-title">
                  {{ link.name
                  }}{{ link.isArchive && ugyEve ? ' - ' + ugyEve : '' }}
                </span>
              </a>
            </router-link>
          </ul>

          <ul class="d-inline-block px-0" v-if="vanJogosultsaga">
            <b-dropdown
              split
              split-variant="transparent"
              variant="outline-warning"
              :disabled="archivEvek.length == 0"
              class="archiv-dropdown"
            >
              <div
                class="vuebar-element header-archiv-dropdown"
                v-bar="{
                  preventParentScroll: true,
                  scrollThrottle: 30,
                  resizeRefresh: true,
                }"
              >
                <div>
                  <b-dropdown-item
                    class="font-size-12"
                    href="#"
                    v-for="ev in archivEvek"
                    :key="ev"
                    :to="`/ArchivUgyek/?ev=${ev}`"
                    >Archív ügyek – {{ ev }}</b-dropdown-item
                  >
                </div>
              </div>
            </b-dropdown>
          </ul>
          <ul class="site-menu d-inline-block px-0" data-plugin="menu">
            <!--<li class="site-menu-category">General</li>-->
            <router-link
              :to="
                link.isArchive && ugyEve
                  ? `/ArchivUgyek/?ev=${ugyEve}`
                  : link.url
              "
              tag="li"
              class="dropdown site-menu-item"
              active-class="active"
              :exact="link.isExact != false"
              v-for="link in fegyelmiLinksArchivUtan"
              :key="link.url"
              :class="{
                active: setArchiveLink && link.isArchive,
                disabled: link.isDisabled,
              }"
            >
              <a href="javascript:;" class="site-menu-item-link">
                <i
                  class="site-menu-icon mr-2"
                  :class="link.icon"
                  aria-hidden="true"
                ></i>
                <span class="site-menu-title">
                  {{ link.name
                  }}{{ link.isArchive && ugyEve ? ' - ' + ugyEve : '' }}
                </span>
              </a>
            </router-link>
          </ul>
          <div
            class="site-menu d-inline-block px-0 fogvatartottkereso"
            :class="{ aktiv: isFogvKeresesAktiv }"
            v-if="vanJogosultsaga"
          >
            <div class="site-menu-title site-menu-select2">
              <k-select2-ajax
                :options="fogvatartottSelect"
                v-model="selectedFogvatartott"
                placeholder="Kezdje gépelni a fogvatartott nevét vagy azonosítóját"
                id="selectedFogvatartott"
              ></k-select2-ajax>
            </div>
          </div>
        </div>
        <ul class="site-menu d-inline-block ml-auto pr-0">
          <li class="dropdown site-menu-item" v-if="vanJogkorGyakorloJoga">
            <a class="site-menu-item-link" @click="OpenSettings">
              <i
                aria-hidden="true"
                class="site-menu-icon wb-settings mr-0"
                v-b-tooltip="{
                  title: 'Szakterületi vezetők megadása',
                }"
              >
              </i>
            </a>
          </li>
        </ul>
      </div>
    </div>
    <div
      class="site-menubar site-menubar-light"
      v-if="$route.meta.layout == 'jutalom'"
    >
      <div class="site-menubar-body d-flex align-items-center">
        <div id="fomenu">
          <ul class="site-menu d-inline-block px-0" data-plugin="menu">
            <!--<li class="site-menu-category">General</li>-->

            <router-link
              :to="
                link.isArchive && ugyEve
                  ? `/JutalomArchivUgyek/?ev=${ugyEve}`
                  : link.url
              "
              tag="li"
              class="dropdown site-menu-item"
              active-class="active"
              :exact="link.isExact != false"
              v-for="link in jutalomLinks"
              :key="link.url"
              :class="{
                active: setArchiveLink && link.isArchive,
                disabled: link.isDisabled,
              }"
            >
              <a href="javascript:;" class="site-menu-item-link">
                <i
                  class="site-menu-icon mr-2"
                  :class="link.icon"
                  aria-hidden="true"
                ></i>
                <span class="site-menu-title">
                  {{ link.name
                  }}{{ link.isArchive && ugyEve ? ' - ' + ugyEve : '' }}
                </span>
              </a>
            </router-link>
          </ul>

          <ul class="d-inline-block px-0" v-if="vanJogosultsaga">
            <b-dropdown
              split
              split-variant="transparent"
              variant="outline-warning"
              :disabled="archivEvek.length == 0"
              class="archiv-dropdown"
            >
              <div
                class="vuebar-element header-archiv-dropdown"
                v-bar="{
                  preventParentScroll: true,
                  scrollThrottle: 30,
                  resizeRefresh: true,
                }"
              >
                <div>
                  <b-dropdown-item
                    class="font-size-12"
                    href="#"
                    v-for="ev in archivEvek"
                    :key="ev"
                    :to="`/JutalomArchivUgyek/?ev=${ev}`"
                    >Archív ügyek – {{ ev }}</b-dropdown-item
                  >
                </div>
              </div>
            </b-dropdown>
          </ul>
          <div
            class="site-menu d-inline-block px-0 fogvatartottkereso"
            :class="{ aktiv: isFogvKeresesAktiv }"
            v-if="vanJogosultsaga"
          >
            <div class="site-menu-title site-menu-select2">
              <k-select2-ajax
                :options="fogvatartottSelect"
                v-model="selectedFogvatartott"
                placeholder="Kezdje gépelni a fogvatartott nevét vagy azonosítóját"
                id="selectedFogvatartott"
              ></k-select2-ajax>
            </div>
          </div>
        </div>
        <ul class="site-menu d-inline-block ml-auto pr-0">
          <li class="dropdown site-menu-item" v-if="vanJogkorGyakorloJoga">
            <a class="site-menu-item-link" @click="OpenSettings">
              <i
                aria-hidden="true"
                class="site-menu-icon wb-settings mr-0"
                v-b-tooltip="{
                  title: 'Szakterületi vezetők megadása',
                }"
              >
              </i>
            </a>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>
<script>
import introJs from 'intro.js';
import $ from 'jquery';
import { mapGetters } from 'vuex';
import { UserStoreTypes } from '../../store/modules/user';
import { strCompare, boolCompare, sortStr } from '../../utils/sort';
import { apiService, eventBus } from '../../main';
import {
  sendToSocket,
  GetSocketConnectionId,
} from '../../utils/socketConnection';
import { NotificationFunctions } from '../../functions/notificationFunctions';
import { BeallitasokStoreTypes } from '../../store/modules/beallitasok';
import { timeout } from '../../utils/common';
import { FegyelmiUgyStoreTypes } from '../../store/modules/fegyelmiugy';
import { isJelenlevo } from '../../data/nyilvantartasiStatuszok';
import { FogvatartottakStoreTypes } from '../../store/modules/fogvatartottak';
import Intezetek from '../../data/enums/intezetek';
import { JutalomUgyStoreTypes } from '../../store/modules/jutalomugy';

export default {
  name: 'jfk-header',
  components: {},
  data: function (params) {
    return {
      dropDownkey: 0,
      intezetValtasLoading: false,
      ugyEve: null,
      selectedFogvatartott: null,
      isMounted: false,
    };
  },
  mounted() {
    this.isMounted = true;
  },
  computed: {
    ...mapGetters({
      userInfo: UserStoreTypes.getters.getUserInfo,
      vanJogosultsaga: UserStoreTypes.getters.vanJogosultsaga,
      vanJogkorGyakorloJoga: UserStoreTypes.getters.vanJogkorGyakorloJoga,
      archivEvek: UserStoreTypes.getters.archivEvek,
      VirKimutatasFegyelmiUrl:
        BeallitasokStoreTypes.getters.getVirKimutatasFegyelmiUrl,
      VirKimutatasJutalomUrl:
        BeallitasokStoreTypes.getters.getVirKimutatasJutalomUrl,
      fegyelmiUgyek: FegyelmiUgyStoreTypes.getters.getFegyelmiUgyek,
      intezetiFogvatartottak:
        FogvatartottakStoreTypes.intezetiFogvatartottak.getAll,
      jutalomUgyek: JutalomUgyStoreTypes.getters.getJutalomUgyek,
    }),
    setArchiveLink() {
      return this.$route.name == 'ArchivUgyek';
    },
    fegyelmiLinks() {
      let fegyelmiLinks = [
        {
          url: '/',
          name: 'Események',
          icon: 'wb-users',
        },
        {
          url: '/FegyelmiUgyek/',
          name: 'Ügyek',
          icon: 'fa-calendar',
          isExact: false,
          isDisabled: !this.vanJogosultsaga,
        },
      ];

      //if (this.userInfo && this.userInfo.VanJfkFegyjutmegtekintoJoga) {
      fegyelmiLinks.push({
        url: '/ArchivUgyek/',
        name: 'Archív ügyek',
        icon: 'wb-lock',
        isDisabled: this.archivEvek.length == 0 || !this.vanJogosultsaga,
        isArchive: true,
      });

      //}
      return fegyelmiLinks;
    },
    fegyelmiLinksArchivUtan() {
      let fegyelmiLinks = [
        {
          url: '/Fenyites/',
          name: 'Kifutó illetve régi ügyek a Fany-ból',
          icon: 'wb-bell',
          isDisabled: !this.vanJogosultsaga,
        },
      ];

      return null; // fegyelmiLinks;
    },
    jutalomLinks() {
      let jutalomLinks = [
        {
          url: '/JutalomUgyek/',
          name: 'Jutalmi ügyek',
          icon: 'wb-users',
          isExact: false,
          isDisabled: !this.vanJogosultsaga,
        },
      ];
      jutalomLinks.push({
        url: '/JutalomArchivUgyek/',
        name: 'Archív ügyek',
        icon: 'wb-lock',
        isDisabled: this.archivEvek.length == 0 || !this.vanJogosultsaga,
        isArchive: true,
      });
      return jutalomLinks;
    },
    valaszthatoIntezetek() {
      if (!this.userInfo) return [];
      let intezet = this.userInfo.RogzitoIntezet.Id;
      /** @type {Array} */
      let valaszthatoIntezetek = this.userInfo.ValaszthatoIntezetek.slice();
      valaszthatoIntezetek = valaszthatoIntezetek.map((f) => {
        let intezetTmp = { selected: f.Id == intezet.Id, ...f };
        return intezetTmp;
      });
      // valaszthatoIntezetek.sort((a, b) => {
      //   let isSelected = boolCompare(a, b, 'selected');
      //   if (isSelected != 0) {
      //     return isSelected;
      //   }
      //   return strCompare(a, b, 'Nev');
      // });

      valaszthatoIntezetek.sort(sortStr('Nev'));
      return valaszthatoIntezetek;
    },
    fogvatartottSelect() {
      if (!this.isMounted) {
        return;
      }
      // var fegyelmiUgyek = this.fegyelmiUgyek;
      // let list = new Map();
      // console.log(fegyelmiUgyek);
      // fegyelmiUgyek.forEach(element => {
      //   if (
      //     !list.has(element.FogvatartottId) &&
      //     isJelenlevo(element.NyilvantartottStatuszId)
      //   ) {
      //     list.set(element.FogvatartottId, {
      //       id: element.FogvatartottId,
      //       text: `${element.FogvatartottNev} - ${element.AktNyilvantartasiSzam}`,
      //     });
      //   }
      // });
      // let result = Array.from(list.values());
      let result = this.intezetiFogvatartottak;
      result.sort(sortStr('text'));
      let minimumInputLength = 3;
      if (this.$get(this.userInfo, 'RogzitoIntezet.Id') == Intezetek.BVOP) {
        minimumInputLength = 5;
      }
      return {
        placeholder: 'Név vagy nyilvántartási szám...',
        apiHandler: apiService.GetIntezetiFogvatartottak.bind(apiService),
        multiple: false,
        dropdownParent: $(this.$el),
        minimumInputLength: minimumInputLength,
        allowClear: true,
      };
    },
    fejlecMenupontok() {
      let fejlecMenupontok = [];
      fejlecMenupontok.push({
        Id: 1,
        Nev: 'Fegyelmi modul',
        Active: this.$route.meta.layout == 'fenyites',
        ClickEvent: () => {
          this.FegyelmiFejlecvaltas();
        },
      });
      let jutalomMenupont = {
        Id: 2,
        Nev: 'Jutalom modul',
        Active: this.$route.meta.layout == 'jutalom',
        ClickEvent: () => {
          this.JutalomFejlecvaltas();
        },
      };
      switch (this.$route.meta.layout) {
        case 'fenyites':
          fejlecMenupontok.push(jutalomMenupont);
          break;

        default:
          fejlecMenupontok.unshift(jutalomMenupont);
          break;
      }
      return fejlecMenupontok;
    },
    isFogvKeresesAktiv() {
      return (
        this.$route.name == 'FogvatartottUgyei' ||
        this.$route.name == 'FogvatartottJutalomai'
      );
    },
  },
  methods: {
    startIntroJs: function () {
      var intro = introJs();
      var dropdownButtonId = $('#DataTables_Table_0');

      var introSteps = [];
      switch (this.$route.name) {
        case 'FegyelmiUgyek':
          introSteps = [
            {
              element: '#firstslide',
              position: 'auto',
              intro:
                'Üdvözöljük a Fegyelmi modul bemutatójában!  <p class="content">A bemutató oldalak között az „Előző” és „Következő” gombokkal léptethet, vagy bármikor kiléphet a jobb alsó sarokban található „X”-el. Kérjük kattintson a „Következő” gombra, és már kezdhetjük is!</p>',
            },
            {
              element: '#JFKLogo',
              position: 'left',
              intro:
                'JFK <p class="content">A JFK modulban a fogvatartottak fegyelmi eljárásainak teljes körű ügyintézésére van lehetőség. A bal felső sarokban található ikonra kattintva program bármely részéből egyszerűen visszaugorhatunk a kezdőoldalra.</p>',
            },
            {
              element: '#jobbfelsosarok',
              position: 'left',
              intro:
                'Fejléc adatok <p class="content">•	A „Statisztika” gomb egy könnyen áttekinthető Excel kimutatást készít a fegyelmi ügyekről. Önnek egy kattintáson kívül mást nem kell tennie, az adatok az internetböngészőjében, új lapon nyílnak meg. </br> </br>•	Bemutató gombra kattintva bármikor újraindíthatja ezt az isemrtetőt.  </br> </br>•	Ha több bv. intézetben is rendelkezik felhasználói jogosultságokkal, ezek között az intézetnévre kattintva tud váltani. </br> </br>•	A jobb felső sarokban az éppen bejelentkezett felhasználó neve látszik. </p>',
            },
            {
              element: '#fomenu',
              hintPosition: 'right',
              intro:
                'A JFK négy menüpontja<p class="content">•	Események: Rendkívüli események rögzítésére,fegyelmi eljárás kezdeményezésére szolgáló felület. </br>•Ügyek: Itt lehet a fegyelmi folyamatát adminisztrálni.</br>•Archív ügyek: Lezárt, vagy elévült fegyelmi ügyek, évenkénti bontásban.</br>•Kifutó illetve régi ügyek a Fany-ból: A FANY-ban indított korábbi fegyelmi ügyeket tudja intézni.</p>',
            },
            {
              element: '#aktivitasfolyam',
              position: 'right',
              intro:
                'Információs folyamok <p class="content">•	Határidős feladatok: Itt jelennek meg azok az ügyek, amelyek határideje már elmúlt vagy 3 napon belül esedékes. A program a fegyelmi ügy előrehaladása során mindig a jogszabályok alapján számítja a határidőket. Egy ügyre kattintva elérheti annak adatlapját.</br> </br>• Aktivitás folyam: Ezen a hasábon jelennek meg a legutóbb megváltozott fegyelmi ügyek. A legfrissebb változás a lista tetejére kerül, és minden ügyben látható az is, hogy mi történt benne (pl. kezdeményezés, meghallgatás). Egy ügyre kattintva elérheti annak adatlapját.</p>',
            },
            {
              element: '#statisztika',
              position: 'right',
              intro:
                'Gyors statisztika <p class="content">Hét fontos számadatot követhet nyomon. Egy számra kattintva a képernyő közepén az annak megfelelő ügyek kerülnek kilistázásra, a számadat körül pedig egy narancssárga keret jelzi az aktív szűrést. Ha meg szeretné szüntetni a szűrést egyszerűen kattintson ismét a kijelölt számra.</p>',
            },
            {
              element: '.szurok .list-complete-item',
              position: 'auto',
              intro:
                'Címkék <p class="content">Intézetek, ügytípusok és státuszok szerinti szűrést teszi lehetővé – választhat egyet vagy többet is, a találatok száma a címke jobb felső sarkában látható.' +
                '</br></br>Ha rákattint egy címkére, három dolog történik:' +
                '</br>• Csak azok az adatsorok látszanak, amikre a címke állítása igaz,' +
                '</br>• Megváltozik a címke háttérszíne, ez jelzi az aktív szűrőt,' +
                '</br>• A többi címke közül csak azok látszanak, amelyek a már szűrt adatok között is előfordulnak.' +
                '</br></br>Egy aktív címkére kattintva megszűntetheti annak kijelölését, ekkor annak háttérszíne visszaváltozik.</p> ',
            },
            {
              element: '#FegyemiUgyekDataTable_filter input',
              position: 'right',
              intro:
                'Táblázat kereső <p class="content">Keresőmező mindig az alatta levő adattáblában keres. Ahogy elkezd gépelni, a program azonnal szűri a táblázatot, és csak azokat a sorokat mutatja, amelyekben a keresett szó, szótöredék, számadat megtalálható. Egyszerre akár több kereső szó is megadható, ezeket elég csak szóközzel elválasztani (például: „Szeged jelenlevő”). Ha meg akarja szüntetni a keresést, azt a szöveg kitörlésével, vagy a kereső mezőben megjelenő „X”-re kattintva teheti meg.</p>',
            },
            {
              element: '#FegyemiUgyekDataTable_info',
              position: 'right',
              intro:
                'Találatok <p class="content">Az egy oldalon megjelenő találatok számát a táblázat alatt állíthatja (5-10-25-50-mind).</p>',
            },
            {
              element: '#FegyemiUgyekDataTable tr',
              position: 'right',
              intro:
                'Táblázat <p class="content">A képernyő közepén a fegyelmi események/ügyek alapadatait összefoglaló adattábla található. A táblázat oszlopaiban szövegesen és címkékkel jelennek meg az adatok, ha egy címke fölé mozgatja az egérmutatót, megjelenik az adat típusa is. Egy sorra kattintva események nézetben a felviteli űrlap, ügyek nézetben a fegyelmi ügy adatlapja töltődik be. A fegyelmi ügyek táblájában a sorok bal szélén egy jelölőnégyzet látható. Ezekkel tömeges kijelölést hajthat végre, és egy kattintással intézkedhet több ügyben is (pl. megtagadásról vagy kivizsgálás elrendeléséről).</p>',
            },
            {
              element: '#FegyemiUgyekDataTable .introJsDokumentumok',
              position: 'left',
              intro:
                'Egy fegyelmi ügynél elérhető funkciók <p class="content">A fegyelmi ügy sorának jobb szélén található menüben mindig az adott ügy státuszának megfelelő tevékenységek jelennek meg. A megjelenő lehetőségek mindig igazodnak az ügy státuszához és a bejelentkező felhasználó jogosultságához.</p>',
            },
            {
              element: '#n2020',
              position: 'left',
              intro:
                'N2020 <p class="content">Ha észrevétele van, vagy hibát tapasztal itt van lehetősége üzenni a fejlesztőknek. A felugró ablakban láthatók azok a beszélgetések, melyekben érintett, ezekre kattintva átnézheti a korábbi hozzászólásokat, vagy újat írhat.</p>',
            },
            {
              element: '#bemutato',
              position: 'left',
              intro:
                'Felhasználói kézikönyv <p class="content">Ebben a letölthető PDF-ben olvashat bővebben a program használatával kapcsolatban</p>',
            },
          ];
          break;
        case 'FegyelmiEsemenyek':
          introSteps = [
            {
              element: '#firstslide',
              position: 'auto',
              intro:
                'Üdvözöljük a Fegyelmi modul bemutatójában!  <p class="content">A bemutató oldalak között az „Előző” és „Következő” gombokkal léptethet, vagy bármikor kiléphet a jobb alsó sarokban található „X”-el. Kérjük kattintson a „Következő” gombra, és már kezdhetjük is!</p>',
            },
            {
              element: '#JFKLogo',
              position: 'left',
              intro:
                'JFK <p class="content">A JFK modulban a fogvatartottak fegyelmi eljárásainak teljes körű ügyintézésére van lehetőség. A bal felső sarokban található ikonra kattintva program bármely részéből egyszerűen visszaugorhatunk a kezdőoldalra.</p>',
            },
            {
              element: '#jobbfelsosarok',
              position: 'left',
              intro:
                'Fejléc adatok <p class="content">•	A „Statisztika” gomb egy könnyen áttekinthető Excel kimutatást készít a fegyelmi ügyekről. Önnek egy kattintáson kívül mást nem kell tennie, az adatok az internetböngészőjében, új lapon nyílnak meg. </br> </br>•	Bemutató gombra kattintva bármikor újraindíthatja ezt az isemrtetőt.  </br> </br>•	Ha több bv. intézetben is rendelkezik felhasználói jogosultságokkal, ezek között az intézetnévre kattintva tud váltani. </br> </br>•	A jobb felső sarokban az éppen bejelentkezett felhasználó neve látszik. </p>',
            },
            {
              element: '#fomenu',
              position: 'left',
              intro:
                'A JFK négy menüpontja<p class="content">•	Események: Rendkívüli események rögzítésére,fegyelmi eljárás kezdeményezésére szolgáló felület. </br>•Ügyek: Itt lehet a fegyelmi folyamatát adminisztrálni.</br>•Archív ügyek: Lezárt, vagy elévült fegyelmi ügyek, évenkénti bontásban.</br>•Kifutó illetve régi ügyek a Fany-ból: A FANY-ban indított korábbi fegyelmi ügyeket tudja intézni.</p>',
            },
            {
              element: '#aktivitasfolyam',
              position: 'right',
              intro:
                'Információs folyamok <p class="content">•	Határidős feladatok: Itt jelennek meg azok az ügyek, amelyek határideje már elmúlt vagy 3 napon belül esedékes. A program a fegyelmi ügy előrehaladása során mindig a jogszabályok alapján számítja a határidőket. Egy ügyre kattintva elérheti annak adatlapját.</br> </br>• Aktivitás folyam: Ezen a hasábon jelennek meg a legutóbb megváltozott fegyelmi ügyek. A legfrissebb változás a lista tetejére kerül, és minden ügyben látható az is, hogy mi történt benne (pl. kezdeményezés, meghallgatás). Egy ügyre kattintva elérheti annak adatlapját.</p>',
            },
            {
              element: '#statisztika',
              position: 'right',
              intro:
                'Gyors statisztika <p class="content">Fegyelmi jogosultság függvényében látható. Hét fontos számadatot követhet nyomon. Egy számra kattintva a képernyő közepén az annak megfelelő ügyek kerülnek kilistázásra, a számadat körül pedig egy narancssárga keret jelzi az aktív szűrést. Ha meg szeretné szüntetni a szűrést egyszerűen kattintson ismét a kijelölt számra.</p>',
            },
            {
              element: '.szurok  .list-complete-item',
              position: 'left',
              intro:
                'Címkék <p class="content">Fegyelmi vétségek szerinti szűrést teszi lehetővé – választhat egyet vagy többet is, a találatok száma a címke jobb felső sarkában látható.</p> ',
            },
            {
              element: '#FegyemiEsemenyekDataTable_filter input',
              position: 'right',
              intro:
                'Táblázat kereső <p class="content">Keresőmező mindig az alatta levő adattáblában keres. Ahogy elkezd gépelni, a program azonnal szűri a táblázatot, és csak azokat a sorokat mutatja, amelyekben a keresett szó, szótöredék, számadat megtalálható. Egyszerre akár több kereső szó is megadható, ezeket elég csak szóközzel elválasztani (például: „Szeged jelenlevő”). Ha meg akarja szüntetni a keresést, azt a szöveg kitörlésével, vagy a kereső mezőben megjelenő „X”-re kattintva teheti meg.</p>',
            },
            {
              element: '#FegyemiEsemenyekDataTable_info',
              position: 'right',
              intro:
                'Találatok <p class="content">Az egy oldalon megjelenő találatok számát a táblázat alatt állíthatja (5-10-25-50-mind).</p>',
            },
            {
              element: '#FegyemiEsemenyekDataTable tr',
              position: 'right',
              intro:
                'Táblázat <p class="content">A képernyő közepén a fegyelmi események/ügyek alapadatait összefoglaló adattábla található. A táblázat oszlopaiban szövegesen és címkékkel jelennek meg az adatok, ha egy címke fölé mozgatja az egérmutatót, megjelenik az adat típusa is. Egy sorra kattintva események nézetben a felviteli űrlap, ügyek nézetben a fegyelmi ügy adatlapja töltődik be.</p>',
            },
            {
              element: '#uj-esemeny-btn',
              position: 'right',
              intro:
                'Esemény rögzítése <p class="content">Itt lehet rögzíteni az észlelt rendkívüli esemény körülményeit.</p>',
            },
            {
              element: '#n2020',
              position: 'left',
              intro:
                'N2020 <p class="content">Ha észrevétele van, vagy hibát tapasztal itt van lehetősége üzenni a fejlesztőknek. A felugró ablakban láthatók azok a beszélgetések, melyekben érintett, ezekre kattintva átnézheti a korábbi hozzászólásokat, vagy újat írhat.</p>',
            },
            {
              element: '#bemutato',
              position: 'left',
              intro:
                'Felhasználói kézikönyv <p class="content">Ebben a letölthető PDF-ben olvashat bővebben a program használatával kapcsolatban</p>',
            },
          ];
          break;
        case 'Fenyites':
          introSteps = [
            {
              element: '#firstslide',
              position: 'auto',
              intro:
                'Üdvözöljük a Fegyelmi modul bemutatójában!  <p class="content">A bemutató oldalak között az „Előző” és „Következő” gombokkal léptethet, vagy bármikor kiléphet a jobb alsó sarokban található „X”-el. Kérjük kattintson a „Következő” gombra, és már kezdhetjük is!</p>',
            },
            {
              element: '#JFKLogo',
              position: 'left',
              intro:
                'JFK <p class="content">A JFK modulban a fogvatartottak fegyelmi eljárásainak teljes körű ügyintézésére van lehetőség. A bal felső sarokban található ikonra kattintva program bármely részéből egyszerűen visszaugorhatunk a kezdőoldalra.</p>',
            },
            {
              element: '#jobbfelsosarok',
              position: 'left',
              intro:
                'Fejléc adatok <p class="content">•	A „Statisztika” gomb egy könnyen áttekinthető Excel kimutatást készít a fegyelmi ügyekről. Önnek egy kattintáson kívül mást nem kell tennie, az adatok az internetböngészőjében, új lapon nyílnak meg. </br> </br>•	Bemutató gombra kattintva bármikor újraindíthatja ezt az isemrtetőt.  </br> </br>•	Ha több bv. intézetben is rendelkezik felhasználói jogosultságokkal, ezek között az intézetnévre kattintva tud váltani. </br> </br>•	A jobb felső sarokban az éppen bejelentkezett felhasználó neve látszik. </p>',
            },
            {
              element: '#fomenu',
              position: 'left',
              intro:
                'A JFK négy menüpontja<p class="content">•	Események: Rendkívüli események rögzítésére,fegyelmi eljárás kezdeményezésére szolgáló felület. </br>•Ügyek: Itt lehet a fegyelmi folyamatát adminisztrálni.</br>•Archív ügyek: Lezárt, vagy elévült fegyelmi ügyek, évenkénti bontásban.</br>•Kifutó illetve régi ügyek a Fany-ból: A FANY-ban indított korábbi fegyelmi ügyeket tudja intézni.</p>',
            },
            {
              element: '#felelosLista',
              position: 'left',
              intro:
                'Aktivitás folyam <p class="content">Ezen a hasábon jelennek meg a Fany-ban indított fegyelmi ügyek változásai. A legfrissebb változás a lista tetejére kerül, és minden ügyben látható az is, hogy mi történt benne (pl. kezdeményezés, meghallgatás). Egy ügyre kattintva elérheti a Fany kapcsolódó funkcióit.</p>',
            },
            {
              element: '#fenyitesJobbPanel',
              position: 'left',
              intro:
                'Gyors statisztika <p class="content">Hét fontos számadatot követhet nyomon. Egy számra kattintva a képernyő közepén az annak megfelelő ügyek kerülnek kilistázásra, a számadat körül pedig egy kék keret jelzi az aktív szűrést. Ha meg szeretné szüntetni a szűrést egyszerűen kattintson ismét a kijelölt számra.</p>',
            },
            {
              element: '#FenyitesDataTable_filter input',
              position: 'right',
              intro:
                'Táblázat kereső <p class="content">Keresőmező mindig az alatta levő adattáblában keres. Ahogy elkezd gépelni, a program azonnal szűri a táblázatot, és csak azokat a sorokat mutatja, amelyekben a keresett szó, szótöredék, számadat megtalálható. Egyszerre akár több kereső szó is megadható, ezeket elég csak szóközzel elválasztani (például: „Szeged jelenlevő”). Ha meg akarja szüntetni a keresést, azt a szöveg kitörlésével, vagy a kereső mezőben megjelenő „X”-re kattintva teheti meg.</p>',
            },
            {
              element: '#FenyitesDataTable_info',
              position: 'right',
              intro:
                'Találatok <p class="content">Az egy oldalon megjelenő találatok számát a táblázat alatt állíthatja (5-10-25-50-mind).</p>',
            },
            {
              element: '#FenyitesDataTable tr',
              position: 'right',
              intro:
                'Táblázat <p class="content">A képernyő közepén a fegyelmi események/ügyek alapadatait összefoglaló adattábla található. A táblázat oszlopaiban szövegesen és címkékkel jelennek meg az adatok, ha egy címke fölé mozgatja az egérmutatót, megjelenik az adat típusa is. Egy sorra kattintva események nézetben a felviteli űrlap, ügyek nézetben a fegyelmi ügy adatlapja töltődik be.</p>',
            },
          ];
          break;

        case 'ArchivUgyek':
          introSteps = [
            {
              element: '#firstslide',
              position: 'auto',
              intro:
                'Üdvözöljük a Fegyelmi modul bemutatójában!  <p class="content">A bemutató oldalak között az „Előző” és „Következő” gombokkal léptethet, vagy bármikor kiléphet a jobb alsó sarokban található „X”-el. Kérjük kattintson a „Következő” gombra, és már kezdhetjük is!</p>',
            },
            {
              element: '#JFKLogo',
              position: 'left',
              intro:
                'JFK <p class="content">A JFK modulban a fogvatartottak fegyelmi eljárásainak teljes körű ügyintézésére van lehetőség. A bal felső sarokban található ikonra kattintva program bármely részéből egyszerűen visszaugorhatunk a kezdőoldalra.</p>',
            },
            {
              element: '#jobbfelsosarok',
              position: 'left',
              intro:
                'Fejléc adatok <p class="content">•	A „Statisztika” gomb egy könnyen áttekinthető Excel kimutatást készít a fegyelmi ügyekről. Önnek egy kattintáson kívül mást nem kell tennie, az adatok az internetböngészőjében, új lapon nyílnak meg. </br> </br>•	Bemutató gombra kattintva bármikor újraindíthatja ezt az isemrtetőt.  </br> </br>•	Ha több bv. intézetben is rendelkezik felhasználói jogosultságokkal, ezek között az intézetnévre kattintva tud váltani. </br> </br>•	A jobb felső sarokban az éppen bejelentkezett felhasználó neve látszik. </p>',
            },
            {
              element: '#fomenu',
              position: 'auto',
              intro:
                'A JFK négy menüpontja<p class="content">•	Események: Rendkívüli események rögzítésére,fegyelmi eljárás kezdeményezésére szolgáló felület. </br>•Ügyek: Itt lehet a fegyelmi folyamatát adminisztrálni.</br>•Archív ügyek: Lezárt, vagy elévült fegyelmi ügyek, évenkénti bontásban.</br>•Kifutó illetve régi ügyek a Fany-ból: A FANY-ban indított korábbi fegyelmi ügyeket tudja intézni.</p>',
            },
            {
              element: '#aktivitasfolyam',
              position: 'right',
              intro:
                'Információs folyamok <p class="content">•	Határidős feladatok: Itt jelennek meg azok az ügyek, amelyek határideje már elmúlt vagy 3 napon belül esedékes. A program a fegyelmi ügy előrehaladása során mindig a jogszabályok alapján számítja a határidőket. Egy ügyre kattintva elérheti annak adatlapját.</br> </br>• Aktivitás folyam: Ezen a hasábon jelennek meg a legutóbb megváltozott fegyelmi ügyek. A legfrissebb változás a lista tetejére kerül, és minden ügyben látható az is, hogy mi történt benne (pl. kezdeményezés, meghallgatás). Egy ügyre kattintva elérheti annak adatlapját.</p>',
            },
            {
              element: '#statisztika',
              position: 'right',
              intro:
                'Gyors statisztika <p class="content">Hét fontos számadatot követhet nyomon. Egy számra kattintva a képernyő közepén az annak megfelelő ügyek kerülnek kilistázásra, a számadat körül pedig egy narancssárga keret jelzi az aktív szűrést. Ha meg szeretné szüntetni a szűrést egyszerűen kattintson ismét a kijelölt számra.</p>',
            },
            {
              element: '.szurok  .list-complete-item',
              position: 'auto',
              intro:
                'Címkék <p class="content">Fegyelmi ügy státusz és fenyítési nemek szerinti szűrést teszi lehetővé – választhat egyet vagy többet is, a találatok száma a címke jobb felső sarkában látható.</p> ',
            },
            {
              element: '#FegyemiUgyekDataTable_filter input',
              position: 'right',
              intro:
                'Táblázat kereső <p class="content">Keresőmező mindig az alatta levő adattáblában keres. Ahogy elkezd gépelni, a program azonnal szűri a táblázatot, és csak azokat a sorokat mutatja, amelyekben a keresett szó, szótöredék, számadat megtalálható. Egyszerre akár több kereső szó is megadható, ezeket elég csak szóközzel elválasztani (például: „Szeged jelenlevő”). Ha meg akarja szüntetni a keresést, azt a szöveg kitörlésével, vagy a kereső mezőben megjelenő „X”-re kattintva teheti meg.</p>',
            },
            {
              element: '#FegyemiUgyekDataTable_info',
              position: 'right',
              intro:
                'Találatok <p class="content">Az egy oldalon megjelenő találatok számát a táblázat alatt állíthatja (5-10-25-50-mind).</p>',
            },
            {
              element: '#FegyemiUgyekDataTable tr',
              position: 'right',
              intro:
                'Táblázat <p class="content">A képernyő közepén a fegyelmi események/ügyek alapadatait összefoglaló adattábla található. A táblázat oszlopaiban szövegesen és címkékkel jelennek meg az adatok, ha egy címke fölé mozgatja az egérmutatót, megjelenik az adat típusa is. Egy sorra kattintva események nézetben a felviteli űrlap, ügyek nézetben a fegyelmi ügy adatlapja töltődik be.</p>',
            },

            {
              element: '#n2020',
              position: 'left',
              intro:
                'N2020 <p class="content">Ha észrevétele van, vagy hibát tapasztal itt van lehetősége üzenni a fejlesztőknek. A felugró ablakban láthatók azok a beszélgetések, melyekben érintett, ezekre kattintva átnézheti a korábbi hozzászólásokat, vagy újat írhat.</p>',
            },
            {
              element: '#bemutato',
              position: 'left',
              intro:
                'Felhasználói kézikönyv <p class="content">Ebben a letölthető PDF-ben olvashat bővebben a program használatával kapcsolatban</p>',
            },
          ];
          break;
      }

      intro.setOptions({
        steps: introSteps,
        skipLabel: '<i class="wb-close"></i>',
        doneLabel: '<i class="wb-close"></i>',
        nextLabel: 'Következő <i class="wb-chevron-right"></i>',
        prevLabel: '<i class="wb-chevron-left"></i>Előző',
        showBullets: false,
        showStepNumbers: false,
        //  tooltipClass:'mt-30'
      });

      //intro.onchange(function (targetElement) {
      //  console.log(targetElement);
      //  if (targetElement.id.indexOf('dropdownMenu') >= 0) {
      //    // document.getElementById('DataTables_Table_0').scrollIntoView();
      //    // window.scrollBy(0, -200);
      //    return;
      //  }
      //  switch (targetElement.id) {
      //    case 'JFKLogo':
      //      $('body').addClass('introjscustom');
      //      //document.getElementById('DataTables_Table_0_info').scrollIntoView();
      //      //window.scrollBy(0, -200);
      //      break;
      //    case 'jobbfelsosarok':
      //      $('body').addClass('introjscustom');
      //      //  alert("Do whatever you want on this callback.");
      //      break;
      //    case 'fomenu':
      //      $('.introjs-tooltip').addClass('mt-30');
      //      $('body').removeClass('introjscustom');
      //      break;
      //    default:
      //      $('body').removeClass('introjscustom');

      //  }
      // });

      intro.onafterchange(function (targetElement) {
        switch (targetElement.id) {
          case 'JFKLogo':
            $('body').addClass('introjscustom');
            //document.getElementById('DataTables_Table_0_info').scrollIntoView();
            //window.scrollBy(0, -200);
            break;
          case 'jobbfelsosarok':
            $('body').addClass('introjscustom');
            //  alert("Do whatever you want on this callback.");
            break;
          case 'fomenu':
            $('.introjs-tooltip').addClass('mt-30');
            $('body').removeClass('introjscustom');
            break;
          default:
            $('body').removeClass('introjscustom');
        }
      });

      intro.onexit(function () {
        // returning false means don't exit the intro
        return false;
      });
      intro.start();
    },
    async IntezetValasztas(intezetId) {
      var socketConnectionId = GetSocketConnectionId();

      let data = [];
      if (socketConnectionId && intezetId) {
        data = [socketConnectionId, intezetId];
      }
      if (this.userInfo.RogzitoIntezet.Id == intezetId) {
        console.log('IntezetValasztas() - Nem kell adatokat letölteni');
        return;
      }
      await timeout(150);
      this.intezetValtasLoading = true;

      try {
        let result = await apiService.IntezetValtas({
          intezetId: intezetId,
          mock: true,
        });
        sendToSocket('SetIntezetIdToUser', data);
      } catch (error) {
        NotificationFunctions.AjaxError({
          title: 'Hiba',
          text: 'Sikertelen intézetváltás',
          errorObj: error,
        });
      }
      this.intezetValtasLoading = false;
      return 'ok';
    },
    OpenSettings() {
      eventBus.$emit('Modal:rendszerbeallitasok', {
        state: true,
        data: {},
      });
    },
    FegyelmiFejlecvaltas() {
      this.vanJogosultsaga
        ? this.$router.push('/FegyelmiUgyek/')
        : this.$router.push('/');
    },
    JutalomFejlecvaltas() {
      this.$router.push('/JutalomUgyek/');
    },
  },
  watch: {
    '$route.query.ev': {
      handler: function (ev) {
        if (ev) {
          this.ugyEve = ev;
        }
      },
      deep: true,
      immediate: true,
    },
    selectedFogvatartott: {
      handler: function (fogvId) {
        if (fogvId) {
          console.log(fogvId);
          this.$nextTick(() => {
            let selectedFogvatartottNev = 'Fogvatartott';
            try {
              let nev = $('#selectedFogvatartott option:selected')[0].text;
              if (nev) {
                selectedFogvatartottNev = nev;
              }
            } catch (error) {
              console.log(error);
            }

            this.$store.dispatch(
              FogvatartottakStoreTypes.fogvatartottKereses.set,
              { value: { id: fogvId, text: selectedFogvatartottNev } }
            );
            this.selectedFogvatartott = null;
            if (this.$route.meta.layout == 'fenyites') {
              this.$router.push({
                name: 'FogvatartottUgyei',
                query: { fogvatartottId: fogvId },
              });
            } else {
              this.$router.push({
                name: 'FogvatartottJutalomai',
                query: { fogvatartottId: fogvId },
              });
            }
          });
        }
      },
      deep: true,
      immediate: true,
    },
  },
};
</script>
<style scoped>
.int-dropdown {
  box-shadow: 3px 3px 6px rgba(0, 0, 0, 0.3);
}

.site-menubar-body .badge-info {
  font-size: 13px;
  box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
}

.site-menu-title {
  max-width: 230px;
}
</style>
