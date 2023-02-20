<template>
  <div>
    <div class="page-header pt-10  pb-10">
      <ol class="breadcrumb">
        <li class="breadcrumb-item">
          <a href="../index.html">JFK</a>
        </li>
        <li class="breadcrumb-item active">
          Fegyelmi ügyek és fenyítések
        </li>
      </ol>
    </div>
    <div class="page-content">
      <div class="row">
        <div class="col-xxl-9 col-xl-9 col-lg-12" v-if="defer(2)">
          <div
            class="vuebar-element withheader mb-sm-25 mb-xl-0"
            ref="vuebarscroll"
            v-bar="{
              preventParentScroll: true,
              scrollThrottle: 30,
              resizeRefresh: true,
            }"
          >
            <div class="panel">
              <div class="panel-body">
                <div class="szurok">
                  <transition-group name="list-complete">
                    <span
                      v-for="tag in OsszesTagSzurt"
                      :key="tag.key"
                      class="list-complete-item filterbadge"
                    >
                      <a
                        href="#"
                        v-if="tag.tagType == 'intezet'"
                        v-on:click="ChangeIntezetekSelectedTag(tag.id)"
                        :class="{ active: Contains(selectedIntezetek, tag.id) }"
                      >
                        <div>
                          <span>{{ tag.name }}</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>

                      <a
                        href="#"
                        v-if="tag.tagType == 'ugystatusz'"
                        v-on:click="ChangeUgyStatuszokSelectedTag(tag.id)"
                        :class="{
                          active: Contains(selectedUgyStatuszok, tag.id),
                        }"
                      >
                        <div class="ugystatusz-badge">
                          <span>{{ tag.name }}</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>
                      <a
                        href="#"
                        v-if="tag.tagType == 'fenyitesstatusz'"
                        v-on:click="ChangeFenyitesStatuszokSelectedTag(tag.id)"
                        :class="{
                          active: Contains(selectedFenyitesStatuszok, tag.id),
                        }"
                      >
                        <div class="fenyitesstatusz-badge">
                          <span>{{ tag.name }}</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>

                      <a
                        href="#"
                        v-if="tag.tagType == 'fenyitestipus'"
                        v-on:click="ChangeFenyitesTipusokSelectedTag(tag.id)"
                        :class="{
                          active: Contains(selectedFenyitesTipusok, tag.id),
                        }"
                      >
                        <div class="fenyitestipus-badge">
                          <span>{{ tag.name }}</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>

                      <a
                        href="#"
                        v-if="tag.tagType == 'project'"
                        v-on:click="ChangeProjektekSelectedTag(tag.id)"
                        :class="{ active: Contains(selectedProjektek, tag.id) }"
                      >
                        <div>
                          <span>{{ tag.name }}</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>
                      <a
                        href="#"
                        v-if="tag.tagType == 'bejelento'"
                        v-on:click="ChangeBejelentokSelectedTag(tag.id)"
                        :class="{
                          active: Contains(selectedBejelentok, tag.id),
                        }"
                      >
                        <div>
                          <span>{{ tag.name }}</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>
                      <a
                        href="#"
                        v-if="tag.tagType == 'felelos'"
                        v-on:click="ChangeFelelosokSelectedTag(tag.id)"
                        :class="{ active: Contains(selectedFelelosok, tag.id) }"
                      >
                        <div>
                          <span>{{ tag.name }}</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>
                      <a
                        href="#"
                        v-if="tag.tagType == 'kategoria'"
                        v-on:click="ChangeKategoriakSelectedTag(tag.id)"
                        :class="{
                          active: Contains(selectedKategoriak, tag.id),
                        }"
                      >
                        <div>
                          <span>{{ tag.name }}</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>
                      <a
                        href="#"
                        v-if="tag.tagType == 'datum'"
                        v-on:click="ChangeDatumokSelectedTag(tag.id)"
                        :class="{ active: Contains(selectedDatumok, tag.id) }"
                      >
                        <div>
                          <span> {{ Moment(tag.date, 'YYYY. W.') }} hét</span>
                          <span class="badge badge-pill badge-default up">
                            {{ tag.count }}
                          </span>
                          <span class="clear"></span>
                        </div>
                      </a>
                    </span>
                  </transition-group>
                </div>
                <div class="pt-10">
                  <!-- <div class="page-content-actions pr-0 pb-15">
                    <div class="float-right">
                      <button
                        type="button"
                        class="btn btn-outline btn-warning"
                        @click="UjFenyites"
                      >
                        Új fegyelmi ügy indítása
                      </button>
                    </div>
                  </div> -->
                  <k-datatable
                    id="FenyitesDataTable"
                    :options="BejelentesDatatableOptions"
                    :dataList="SzurtFegyelmiUgyek"
                    :dataKey="'FegyelmiUgyId'"
                    class="pointer table-hover fenyitesek-dt"
                    ref="datatable"
                  >
                    <tfoot></tfoot>
                  </k-datatable>
                  <!-- <fenyitesek-table
                    :rows="SzurtFegyelmiUgyek"
                  ></fenyitesek-table> -->
                </div>
              </div>
            </div>
          </div>
          <!-- <button
            type="button"
            class="btn btn-floating btn-warning uj-fenyites btn-sm"
            @click="UjFenyites"
          >
            Új fegyelmi ügy indítása
          </button> -->
        </div>
        <div class="col-xxl-3 col-xl-3 col-lg-12">
          <div
            id="fenyitesJobbPanel"
            class="vuebar-element withheader vb vb-visible"
            ref="vuebarscroll"
            v-bar="{
              preventParentScroll: true,
              scrollThrottle: 30,
              resizeRefresh: true,
            }"
          >
            <div class="panel vb-content">
              <div class="card" style="margin-bottom: 0" v-if="defer(3)">
                <div class="card-block">
                  <h4 class="project-option-title mb-0">
                    {{ AktualisEv }}. {{ AktualisHet }}. hét
                  </h4>
                </div>
                <div id="statisztika" class="row">
                  <div class="col-md-4 col-xl-6">
                    <div class="card-block p-10 sameheight">
                      <div class="counter counter-md">
                        <span class="counter-number">
                          <k-animated-number
                            :value="SzurtFegyelmiUgyek.length"
                          ></k-animated-number>
                        </span>
                        <div class="counter-label text-uppercase">
                          Nyitott ügyek
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-4 col-xl-6">
                    <div
                      class="card card-block p-10 sameheight tagItem"
                      @click="ToggleCustomFilter('sajat')"
                      :class="{
                        'border border-primary': customFilters.sajat,
                      }"
                    >
                      <div class="counter counter-md">
                        <div class="counter-number-group">
                          <span class="counter-number">
                            <k-animated-number
                              :value="SzurtAdatokSajat.length"
                            ></k-animated-number>
                          </span>
                        </div>
                        <div class="counter-label text-uppercase">
                          Saját ügyeim
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-4 col-xl-6">
                    <div
                      class="card card-block p-10 sameheight tagItem"
                      @click="ToggleCustomFilter('kesesben')"
                      :class="{
                        'border border-primary': customFilters.kesesben,
                      }"
                    >
                      <div class="counter counter-md">
                        <div class="counter-number-group">
                          <span class="counter-number">
                            <k-animated-number
                              :value="SzurtAdatokKesesben.length"
                            ></k-animated-number>
                          </span>
                        </div>
                        <div class="counter-label text-uppercase">
                          Késésben
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-4 col-xl-6">
                    <div
                      class="card card-block p-10 sameheight tagItem"
                      @click="ToggleCustomFilter('hetenEsedekes')"
                      :class="{
                        'border border-primary': customFilters.hetenEsedekes,
                      }"
                    >
                      <div class="counter counter-md">
                        <div class="counter-number-group">
                          <span class="counter-number">
                            <k-animated-number
                              :value="SzurtAdatokAHetenLejar.length"
                            ></k-animated-number>
                          </span>
                        </div>
                        <div class="counter-label text-uppercase">
                          Héten esedékes
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-4 col-xl-6">
                    <div
                      class="card card-block p-10 sameheight tagItem"
                      @click="ToggleCustomFilter('vegrehajtasraVaro')"
                      :class="{
                        'border border-primary': isVegrehajtasravaroFilterEnabled,
                      }"
                    >
                      <div class="counter counter-md">
                        <div class="counter-number-group">
                          <span class="counter-number">
                            <k-animated-number
                              :value="SzurtAdatokVegrehajtasraVar.length"
                            ></k-animated-number>
                          </span>
                        </div>
                        <div class="counter-label text-uppercase">
                          Végrehajtásra váró
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-4 col-xl-6">
                    <div
                      class="card card-block p-10 sameheight tagItem"
                      @click="ToggleCustomFilter('vegrehajtasAlatt')"
                      :class="{
                        'border border-primary': isVegrehajtasAlattFilterEnabled,
                      }"
                    >
                      <div class="counter counter-md">
                        <div class="counter-number-group">
                          <span class="counter-number">
                            <k-animated-number
                              :value="SzurtAdatokVegrehajtasAlatt.length"
                            ></k-animated-number>
                          </span>
                        </div>
                        <div class="counter-label text-uppercase">
                          Végrehajtás <br />alatt
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-4 col-xl-6">
                    <div
                      class="card card-block p-10 sameheight tagItem"
                      @click="ToggleCustomFilter('szallitasraVar')"
                      :class="{
                        'border border-primary': customFilters.szallitasraVar,
                      }"
                    >
                      <div class="counter counter-md">
                        <div class="counter-number-group">
                          <span class="counter-number">
                            <k-animated-number
                              :value="SzurtAdatokSzallitasraVar.length"
                            ></k-animated-number>
                          </span>
                        </div>
                        <div class="counter-label text-uppercase">
                          Szállításra előjegyezve
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <b-modal
      id="nyomtatott-dokumentumok"
      ref="nyomtatottDokumentumok"
      :hide-header="true"
      :hide-footer="true"
      @hidden="nyomtatvaModalId = null"
      size="lg"
      centered
      :no-close-on-backdrop="true"
    >
      <nyomtatott-dokumentumok :id="nyomtatvaModalId"></nyomtatott-dokumentumok>
    </b-modal>
    <b-modal
      id="valaszthato-dokumentumok"
      ref="valaszthatoDokumentumok"
      :hide-header="true"
      :hide-footer="true"
      @hidden="valaszthatoDokumentumokId = null"
      size="lg"
      centered
      :no-close-on-backdrop="true"
    >
      <valaszthato-dokumentumok
        :id="valaszthatoDokumentumokId"
      ></valaszthato-dokumentumok>
    </b-modal>
    <b-modal
      id="valaszthato-vedo-dokumentumok"
      ref="valaszthatoVedoDokumentumok"
      :hide-header="true"
      :hide-footer="true"
      @hidden="valaszthatoDokumentumokId = null"
      size="lg"
      centered
      :no-close-on-backdrop="true"
    >
      <valaszthato-vedo-dokumentumok
        :id="valaszthatoVedoDokumentumokId"
      ></valaszthato-vedo-dokumentumok>
    </b-modal>
    <k-slide-panel-with-fogvatartott-adatok
      ref="fanySlidePanelWithFogvatartottAdatokRef"
      id="fanyFogvatartottAdatok"
    ></k-slide-panel-with-fogvatartott-adatok>
    <k-slide-panel-with-fejlec
      ref="fanySlidePanelWithFejlecRef"
      id="fanyFejlec"
    ></k-slide-panel-with-fejlec>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import $ from 'jquery';
import moment from 'moment';
import { FenyitesStoreTypes } from '../store/modules/fenyites';
import Defer from '../mixins/Defer';
import { AppStoreTypes } from '../store/modules/app';
import settings from '../data/settings';
import { UserStoreTypes } from '../store/modules/user';
import { getUgyszam } from '../utils/fenyitesUtils';
import { eventBus } from '../main';
import IFrameUrls from '../data/enums/iframeUrls';
import Intezetek from '../data/enums/intezetek';
import { removeAllHtmlFromString } from '../utils/common';

export default {
  name: 'fenyitesek',
  data: function() {
    return {
      selectedVevok: [],
      selectedProjektek: [],
      selectedBejelentok: [],
      selectedFelelosok: [],
      selectedDatumok: [],
      selectedKategoriak: [],
      selectedIntezetek: [],
      selectedFenyitesStatuszok: [],
      selectedFenyitesTipusok: [],
      selectedUgyStatuszok: [],
      nyomtatvaModalId: null,
      valaszthatoDokumentumokId: null,
      valaszthatoVedoDokumentumokId: null,
      customFilters: {
        sajat: false,
        kesesben: false,
        hetenEsedekes: false,
        szallitasraVar: false,
      },
    };
  },
  mounted: function() {
    console.log('user');
    console.log(this.userInfo);
    if (this.userInfo.RogzitoIntezet.Id != Intezetek.BVOP)
      this.selectedIntezetek.push(this.userInfo.RogzitoIntezet.RovidNev);
  },
  methods: {
    UjFenyitesBtn() {
      var vm = this;
      var table = $(this.$refs.datatable.$el).DataTable();

      new $.fn.dataTable.Buttons(table, {
        buttons: [
          {
            text: 'Új fegyelmi esemény',
            className: 'mx-0 btn btn-outline btn-warning',
            action: function(e, dt, node, config) {
              vm.UjFenyites();
            },
          },
        ],
      });

      table
        .buttons(1, null)
        .container()
        .appendTo($('.dt-panelmenu .ujuenyitesbtn'));
    },
    UjFenyites() {
      var args = {};
      args.FegyelmiUgyId = null;
      args.Url =
        IFrameUrls.UjFenyites() + this.userInfo.RogzitoIntezet.Azonosito;
      args.FejlecCim = 'Fegyelmi eljárás indítása';
      eventBus.$emit('Sidebar:fanyFejlec', {
        state: true,
        data: args,
      });
    },
    ToggleCustomFilter(name) {
      var value = this.customFilters[name];
      var newValue = !value;
      for (const key in this.customFilters) {
        if (this.customFilters.hasOwnProperty(key)) {
          this.customFilters[key] = false;
        }
      }

      if (this.customFilters.hasOwnProperty(name)) {
        this.customFilters[name] = !value;
        this.selectedFenyitesStatuszok = this.selectedFenyitesStatuszok.filter(
          f => f != 'Fenyítés kiszabva' && f != 'Végrehajtás alatt'
        );
      } else {
        if (name == 'vegrehajtasraVaro') {
          if (this.selectedFenyitesStatuszok.includes('Fenyítés kiszabva')) {
            this.selectedFenyitesStatuszok = this.selectedFenyitesStatuszok.filter(
              f => f != 'Fenyítés kiszabva'
            );
          } else {
            this.selectedFenyitesStatuszok.push('Fenyítés kiszabva');
          }
          this.selectedFenyitesStatuszok = this.selectedFenyitesStatuszok.filter(
            f => f != 'Végrehajtás alatt'
          );
        }
        if (name == 'vegrehajtasAlatt') {
          if (this.selectedFenyitesStatuszok.includes('Végrehajtás alatt')) {
            this.selectedFenyitesStatuszok = this.selectedFenyitesStatuszok.filter(
              f => f != 'Végrehajtás alatt'
            );
          } else {
            this.selectedFenyitesStatuszok.push('Végrehajtás alatt');
          }
          this.selectedFenyitesStatuszok = this.selectedFenyitesStatuszok.filter(
            f => f != 'Fenyítés kiszabva'
          );
        }
        // if (name == 'szallitasraVaro') {
        //   if (this.selectedFenyitesStatuszok.includes('Szállításra vár')) {
        //     this.selectedFenyitesStatuszok = this.selectedFenyitesStatuszok.filter(
        //       f => f != 'Szállításra vár'
        //     );
        //   } else {
        //     this.selectedFenyitesStatuszok.push('Szállításra vár');
        //   }
        //   // this.selectedFenyitesStatuszok = this.selectedFenyitesStatuszok.filter(
        //   //   f => f != 'Végrehajtás alatt'
        //   // );
        // }
      }

      $(this.$refs.datatable.$el)
        .DataTable()
        .search('');
    },
    IntezetKigyujtes: function(datalist) {
      var tagsObj = {};

      $(datalist).each(function(i, v) {
        if (tagsObj.hasOwnProperty(v.RelevansIntezet)) {
          tagsObj[v.RelevansIntezet].count++;
        } else {
          tagsObj[v.RelevansIntezet] = {
            id: v.RelevansIntezet,
            name: v.RelevansIntezet,
            count: 1,
          };
        }
      });

      var list = [];
      for (var index in tagsObj) {
        list.push(tagsObj[index]);
      }

      return list;
    },
    UgyStatuszKigyujtes: function(datalist) {
      var tagsObj = {};
      $(datalist).each(function(i, v) {
        if (tagsObj.hasOwnProperty(v.UgyStatusz)) {
          tagsObj[v.UgyStatusz].count++;
        } else {
          tagsObj[v.UgyStatusz] = {
            id: v.UgyStatusz,
            name: v.UgyStatusz,
            count: 1,
          };
        }
      });
      var list = [];
      for (var index in tagsObj) {
        list.push(tagsObj[index]);
      }
      return list;
    },
    FenyitesStatuszKigyujtes: function(datalist) {
      var tagsObj = {};
      $(datalist).each(function(i, v) {
        if (v.FenyitesStatusz) {
          if (tagsObj.hasOwnProperty(v.FenyitesStatusz)) {
            tagsObj[v.FenyitesStatusz].count++;
          } else {
            tagsObj[v.FenyitesStatusz] = {
              id: v.FenyitesStatusz,
              name: v.FenyitesStatusz,
              count: 1,
            };
          }
        }
      });
      var list = [];
      for (var index in tagsObj) {
        list.push(tagsObj[index]);
      }
      return list;
    },
    FenyitesTipusKigyujtes: function(datalist) {
      var tagsObj = {};
      $(datalist).each(function(i, v) {
        if (v.FenyitesTipus && v.FenyitesTipus != '-') {
          if (tagsObj.hasOwnProperty(v.FenyitesTipus)) {
            tagsObj[v.FenyitesTipus].count++;
          } else {
            tagsObj[v.FenyitesTipus] = {
              id: v.FenyitesTipus,
              name: v.FenyitesTipus,
              count: 1,
            };
          }
        }
      });
      var list = [];
      for (var index in tagsObj) {
        list.push(tagsObj[index]);
      }
      return list;
    },
    Contains: function(list, value) {
      return list.find(x => x == value) != null;
    },
    ChangeUgyStatuszokSelectedTag: function(id) {
      var index = this.selectedUgyStatuszok.findIndex(x => x == id);
      if (index == -1) {
        this.selectedUgyStatuszok.push(id);
      } else {
        this.selectedUgyStatuszok.splice(index, 1);
      }
      $(this.$refs.datatable.$el)
        .DataTable()
        .search('');
    },
    ChangeFenyitesStatuszokSelectedTag: function(id) {
      var index = this.selectedFenyitesStatuszok.findIndex(x => x == id);
      if (index == -1) {
        this.selectedFenyitesStatuszok.push(id);
      } else {
        this.selectedFenyitesStatuszok.splice(index, 1);
      }
      $(this.$refs.datatable.$el)
        .DataTable()
        .search('');
    },
    ChangeFenyitesTipusokSelectedTag: function(id) {
      var index = this.selectedFenyitesTipusok.findIndex(x => x == id);
      if (index == -1) {
        this.selectedFenyitesTipusok.push(id);
      } else {
        this.selectedFenyitesTipusok.splice(index, 1);
      }
      $(this.$refs.datatable.$el)
        .DataTable()
        .search('');
    },
    ChangeIntezetekSelectedTag: function(id) {
      var index = this.selectedIntezetek.findIndex(x => x == id);
      if (index == -1) {
        this.selectedIntezetek.push(id);
      } else {
        this.selectedIntezetek.splice(index, 1);
      }
      $(this.$refs.datatable.$el)
        .DataTable()
        .search('');
    },
    OpenNyomtatvaModal(id) {
      console.log('OpenNyomtatvaModal', id);
      this.nyomtatvaModalId = id;
      this.$root.$emit('bv::show::modal', 'nyomtatott-dokumentumok');
    },
    OpenValaszthatoDokumentumokModal(id) {
      console.log('valaszthatoDokumentumok', id);
      this.valaszthatoDokumentumokId = id;
      this.$root.$emit('bv::show::modal', 'valaszthato-dokumentumok');
    },
    OpenValaszthatoVedoDokumentumokModal(id) {
      console.log('valaszthatoVedoDokumentumok', id);
      this.valaszthatoVedoDokumentumokId = id;
      this.$root.$emit('bv::show::modal', 'valaszthato-vedo-dokumentumok');
    },
    GetVegrehajtasAlatt(list) {
      return list.filter(f => f.FenyitesStatusz == 'Végrehajtás alatt');
    },
    GetExcelExport() {
      let capitalize = this.$options.filters.camelCaseString;
      let SzurtFegyelmiUgyek = Array.from(
        $(this.$refs.datatable.$el)
          .DataTable()
          .rows({ search: 'applied' })
          .data()
      );
      let exportData = [
        {
          header: 'Születési név',
          getCellValue(row) {
            return row.SzuletesiNev || '';
          },
        },
        {
          header: 'Nyilvántartási szám',
          getCellValue(row) {
            return row.AktNyilvantartasiSzam || '';
          },
        },
        {
          header: 'Nyilvántartási státusz',
          getCellValue(row) {
            return row.NyilvantartottStatusz || '';
          },
        },
        {
          header: 'Elhelyezés',
          getCellValue(row) {
            return row.Elhelyezes || '';
          },
        },
        {
          header: 'Intézet',
          getCellValue(row) {
            return row.RelevansIntezet || '';
          },
        },
        {
          header: 'Legközelebbi szállítás dátuma',
          getCellValue(row) {
            if (row.LegkozelebbiSzallitasDatuma) {
              return `${moment(row.LegkozelebbiSzallitasDatuma).format(
                'YYYY.MM.DD.'
              )}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Vétség',
          getCellValue(row) {
            return row.VetsegTipusNev || '';
          },
        },
        {
          header: 'Kezdeményezés',
          getCellValue(row) {
            if (row.LegkozelebbiSzallitasDatuma) {
              return `${moment(row.KezdemenyezesDatum).format('YYYY.MM.DD.')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Határidő',
          getCellValue(row) {
            if (row.HataridoDatum) {
              return `${moment(row.HataridoDatum).format('YYYY.MM.DD.')}`;
            } else {
              return '';
            }
          },
        },
        {
          header: 'Fenyítés státusza',
          getCellValue(row) {
            return row.FenyitesStatusz || '';
          },
        },
        {
          header: 'Ügyszám',
          getCellValue(row) {
            return getUgyszam(row);
          },
        },
        {
          header: 'Ügy státusza',
          getCellValue(row) {
            return row.UgyStatusz || '';
          },
        },
        {
          header: 'Fenyítés típusa',
          getCellValue(row) {
            return (row.FenyitesTipus == '-' ? '' : row.FenyitesTipus) || '';
          },
        },
        {
          header: 'Kivizsgáló',
          getCellValue(row) {
            return (
              (row.Kivizsgalo == '-' ? '' : capitalize(row.Kivizsgalo)) || ''
            );
          },
        },
        {
          header: 'Elrendelő',
          getCellValue(row) {
            return (
              (row.Elrendelo == '-' ? '' : capitalize(row.Elrendelo)) || ''
            );
          },
        },
        {
          header: 'Fegyelmi leírás',
          getCellValue(row) {
            return removeAllHtmlFromString(row.UgyIndoklas) || '';
          },
        },
      ];
      let body = [];
      let footer = null;
      SzurtFegyelmiUgyek.forEach(row => {
        let excelRow = [];

        exportData.forEach(data => {
          excelRow.push(data.getCellValue(row));
        });
        body.push(excelRow);
      });
      return { header: exportData.map(m => m.header), body, footer };
    },
  },
  computed: {
    ...mapGetters({
      fenyitesek: FenyitesStoreTypes.getters.getFenyitesek,
      dokumentumok: AppStoreTypes.getters.getDokumentumok,
      vedoDokumentumok: AppStoreTypes.getters.getVedoDokumentumok,
      userInfo: UserStoreTypes.getters.getUserInfo,
    }),
    BejelentesDatatableOptions: function() {
      var vm = this;
      var url = settings.baseUrl + 'Api/Nyomtatvany/NyomtatvanyGeneralas?';
      var dokumentumok = vm.dokumentumok;
      var vedoDokumentumok = vm.vedoDokumentumok;
      // dokumentumok = dokumentumok
      //   .map(m => {
      //     return `<div class="col-6"><a class="dropdown-item" onclick="window.open('${url}${
      //       m.Id
      //     }', '_system');" href="javascript:void(0)" role="menuitem" tabindex="-1">${
      //       m.DisplayName
      //     }</a></div>`;
      //   })
      //   .join('');
      var options = {
        aoColumns: [
          {
            mDataProp: null, // címkék
            sTitle: 'Fogvatartott Adatok',
            mRender: function(data, type, row, meta) {
              var cimkek =
                ' <span >' +
                row.SzuletesiNev +
                '</span></br>' +
                ' <span class="badge badge-outline badge-default">' +
                row.AktNyilvantartasiSzam +
                '</span>' +
                ' <span class="badge badge-outline badge-warning">' +
                row.NyilvantartottStatusz +
                '</span>';
              if (row.Elhelyezes) {
                cimkek +=
                  ' <span class="badge badge-outline badge-info"> ' +
                  row.Elhelyezes +
                  ' </span>';
              }
              if (row.RelevansIntezet) {
                cimkek +=
                  ' <span class="badge badge-outline badge-success">' +
                  row.RelevansIntezet +
                  '</span>';
              }
              if (row.LegkozelebbiSzallitasDatuma) {
                cimkek +=
                  ' <span class="badge badge-outline badge-info"> ' +
                  'Szállítás: ' +
                  moment(row.LegkozelebbiSzallitasDatuma).format(
                    'YYYY.MM.DD.'
                  ) +
                  ' </span>';
              }
              if (row.VetsegTipusNev) {
                cimkek +=
                  ' <span class="badge badge-outline badge-default"> ' +
                  row.VetsegTipusNev +
                  ' </span>';
              }
              // } else {
              //   cimkek +=
              //     ' <span class="badge badge-outline badge-info"> ' +
              //     'Nincs szállítás' +
              //     ' </span>';
              // }
              return cimkek;
            },
          },
          {
            mDataProp: null, // címkék
            sTitle: 'Ügyadatok',
            mRender: function(data, type, row, meta) {
              var cimkek =
                ' <span class="badge badge-outline badge-info">' +
                moment(row.KezdemenyezesDatum).format('YYYY.MM.DD.') +
                '</span>' +
                ' <span class="badge badge-outline badge-primary">' +
                getUgyszam(row) +
                '</span>';
              if (row.HataridoDatum) {
                cimkek +=
                  '</br><span class="badge badge-outline badge-warning">Hat: ' +
                  moment(row.HataridoDatum).format('YYYY.MM.DD.') +
                  ' </span>';
              }

              if (row.FenyitesStatusz) {
                cimkek +=
                  ' <span class="badge badge-outline badge-default">' +
                  row.FenyitesStatusz +
                  '</span>';
              } else {
                if (row.UgyStatusz) {
                  cimkek +=
                    ' <span class="badge badge-outline badge-default">' +
                    row.UgyStatusz +
                    '</span>';
                }
              }
              if (row.FenyitesTipus && row.FenyitesTipus != '-') {
                cimkek +=
                  ' <span class="badge badge-outline badge-default">' +
                  row.FenyitesTipus +
                  '</span>';
              }
              if (row.Kivizsgalo && row.Kivizsgalo != '-') {
                cimkek +=
                  ' <span class="badge badge-outline badge-default">' +
                  'Kiv: ' +
                  row.Kivizsgalo +
                  '</span>';
              }
              if (row.Elrendelo && row.Elrendelo != '-') {
                cimkek +=
                  ' <span class="badge badge-outline badge-default">' +
                  'Elr: ' +
                  row.Elrendelo +
                  '</span>';
              }

              return cimkek;
            },
          },

          {
            mDataProp: null,
            sTitle: 'Fegyelmi leírás',
            width: '60%',
            mRender: function(data, type, row, meta) {
              return '<span>' + row.UgyIndoklas + '</span>';
            },
          },
          {
            mDataProp: null,
            sTitle: '',
            bSortable: false,
            sClass: 'dt-td-center dt-td-noClick',
            sWidth: '55px',
            mRender: function(data, type, row, meta) {
              var tempDokumentumok = dokumentumok
                .map(m => {
                  return `<div class="col-6"><a class="dropdown-item" onclick="window.open('${url}nyomtatvanyId=${
                    m.Id
                  }&fegyelmiUgyId=${
                    data.FegyelmiUgyId
                  }', '_system');" href="javascript:void(0)" role="menuitem" tabindex="-1">${
                    m.DisplayName
                  }</a></div>`;
                })
                .join('');

              var tempVedoDokumentumok = vedoDokumentumok
                .map(m => {
                  /*eslint template-curly-spacing: 2*/
                  return `<div class="col-6"><a class="dropdown-item" onclick="window.open('${url}nyomtatvanyId=${
                    m.Id
                  }&fegyelmiUgyId=${
                    data.FegyelmiUgyId
                  }', '_system');" href="javascript:void(0)" role="menuitem" tabindex="-1">${
                    m.DisplayName
                  }</a></div>`;
                })
                .join('');

              return `<div class="dropdown">
                      <button type="button" class="btn btn-icon btn-dark btn-outline btn-round" id="dropdownMenu2${
                        data.FegyelmiUgyId
                      }"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >
                        <i class="icon wb-more-horizontal" aria-hidden="true"></i>
                      </button>
                      <div class="dropdown-menu" aria-labelledby="dropdownMenu2${
                        data.FegyelmiUgyId
                      }">
                        <button class="dropdown-item dokumentumokModal" data-id="${
                          data.FegyelmiUgyId
                        }" type="button">Nyomtatványok</button>
                        <button class="dropdown-item vedoDokumentumokModal" data-id="${
                          data.FegyelmiUgyId
                        }" type="button">Védői nyomtatványok</button>
                        <button class="dropdown-item nyomtatvaModal" data-id="${
                          data.FegyelmiUgyId
                        }" type="button">Iktatott nyomtatványok</button>
                      </div>
                    </div>`;
            },
          },
        ],
        responsive: false,
        deferRender: true,
        sDom: `<'row dt-panelmenu'<'col-sm-12 col-md-5  col-xl-5 'i><'col-sm-12 col-md-7 col-xl-7  text-right'<'ujuenyitesbtn'>f>>
          <'row'<'col-sm-12'tr>>
          <'row dt-panelfooter'<'col-sm-12 col-md-6 'lB><'col-sm-12 col-md-6 'p>>`,
        //buttons: ['copy', 'excel', 'pdf', 'print'],
        initComplete: function initComplete() {
          // vm.UjFenyitesBtn();
        },
        drawCallback: function(settings) {
          $('#DataTables_Table_0 tbody button:first').addClass(
            'introJsDokumentumok'
          );
          $('#DataTables_Table_0_filter')
            .find('input')
            .attr(
              'placeholder',
              'Keresés ügyfélre, azonosítóra, ügyszámra, dátumokra'
            );
        },
        createdRow: function(row, data, rowIndex) {
          $(row).attr('data-id', data.FegyelmiUgyId);
          $(row).css('cursor', 'pointer');
          $(row)
            .find('.dokumentumokModal')
            .click(function(e) {
              var id = $(e.target).attr('data-id');
              vm.OpenValaszthatoDokumentumokModal(id);
            });
          $(row)
            .find('.vedoDokumentumokModal')
            .click(function(e) {
              var id = $(e.target).attr('data-id');
              vm.OpenValaszthatoVedoDokumentumokModal(id);
            });
          $(row)
            .find('.nyomtatvaModal')
            .click(function(e) {
              var id = $(e.target).attr('data-id');
              vm.OpenNyomtatvaModal(id);
            });

          $(row)
            .find('td:not(.dt-td-noClick)')
            .on('click', this, function(e) {
              var Id = $(this).attr('data-id');
              var args = {};
              args.FegyelmiUgyId = data.FegyelmiUgyId;
              args.Urls = IFrameUrls.GetFanyUrl(
                vm.userInfo.RogzitoIntezet.Azonosito,
                data
              );
              if (args.Urls.length == 0) return;
              eventBus.$emit('Sidebar:fanyFogvatartottAdatok', {
                state: true,
                data: args,
              });
            });
        },
        buttons: [
          {
            text: 'Másolás',
            extend: 'copyHtml5',
            exportOptions: {
              columns: ':visible :not(.noExport)',
            },
          },
          {
            extend: 'excelHtml5',
            text: 'Excel',

            exportOptions: {
              columns: ':visible :not(.noExport)',
              customizeData: function(data) {
                let newData = vm.GetExcelExport();

                data.header = newData.header;
                data.body = newData.body;
                data.footer = newData.footer;
              },
            },
          },
          {
            extend: 'print',
            text: 'Nyomtatás',
          },
        ],
      };
      return options;
    },
    SzurtFegyelmiUgyek: function() {
      var intezetek = new Set();
      var ugyStatuszok = new Set();
      var fenyitesStatuszok = new Set();
      var fenyitesTipusok = new Set();

      intezetek = new Set(this.selectedIntezetek);
      ugyStatuszok = new Set(this.selectedUgyStatuszok);
      fenyitesStatuszok = new Set(this.selectedFenyitesStatuszok);
      fenyitesTipusok = new Set(this.selectedFenyitesTipusok);

      var filteredList = [];
      var fegyelmiUgyekTmp = this.fenyitesek.slice();

      var customFilter = this.customFilters;
      let userId = this.$get(this.userInfo, 'SzemelyzetSid');

      for (var i in fegyelmiUgyekTmp) {
        var v = fegyelmiUgyekTmp[i];
        if (intezetek.size > 0) {
          if (!intezetek.has(v.RelevansIntezet) && !intezetek.has(v.UgyIntezet))
            continue;
        }

        if (ugyStatuszok.size > 0) {
          if (!ugyStatuszok.has(v.UgyStatusz)) continue;
        }

        if (fenyitesStatuszok.size > 0) {
          if (!v.FenyitesStatusz || !fenyitesStatuszok.has(v.FenyitesStatusz))
            continue;
        }
        if (fenyitesTipusok.size > 0) {
          if (!v.FenyitesTipus || !fenyitesTipusok.has(v.FenyitesTipus))
            continue;
        }
        if (customFilter.kesesben && v.Lejart == false) {
          continue;
        }
        if (customFilter.hetenEsedekes && v.AHetenJarLe != true) {
          continue;
        }
        if (
          customFilter.szallitasraVar &&
          v.LegkozelebbiSzallitasDatuma == null
        ) {
          continue;
        }
        if (
          customFilter.sajat &&
          (!v.KivizsgaloSid || v.KivizsgaloSid != userId)
        ) {
          continue;
        }
        filteredList.push(v);
      }

      return filteredList;
    },
    SzurtAdatokSajat: function() {
      let userId = this.$get(this.userInfo, 'SzemelyzetSid');
      return this.SzurtFegyelmiUgyek.filter(
        f => f.KivizsgaloSid && f.KivizsgaloSid == userId
      );
    },
    SzurtAdatokKesesben: function() {
      return this.SzurtFegyelmiUgyek.filter(f => f.Lejart == true);
    },
    SzurtAdatokAHetenLejar: function() {
      return this.SzurtFegyelmiUgyek.filter(f => f.AHetenJarLe == true);
    },
    SzurtAdatokVegrehajtasraVar: function() {
      return this.SzurtFegyelmiUgyek.filter(
        f => f.FenyitesStatusz == 'Fenyítés kiszabva'
      );
    },
    SzurtAdatokVegrehajtasAlatt: function() {
      return this.GetVegrehajtasAlatt(this.SzurtFegyelmiUgyek);
    },
    SzurtAdatokSzallitasraVar: function() {
      return this.SzurtFegyelmiUgyek.filter(
        f => f.LegkozelebbiSzallitasDatuma != null
      );
    },
    AktualisEv: function() {
      return moment().format('YYYY');
    },
    AktualisHet: function() {
      return moment().format('W');
    },
    IntezetekSzurt: function() {
      return this.IntezetKigyujtes(this.SzurtFegyelmiUgyek);
    },
    UgyStatuszokSzurt: function() {
      return this.UgyStatuszKigyujtes(this.SzurtFegyelmiUgyek);
    },
    FenyitesStatuszok: function() {
      return this.FenyitesStatuszKigyujtes(this.FegyelmiUgyek);
    },
    FenyitesStatuszokSzurt: function() {
      return this.FenyitesStatuszKigyujtes(this.SzurtFegyelmiUgyek);
    },
    FenyitesStatuszokDarabSzerintCsokkenoSzurt: function() {
      return this.FenyitesStatuszokSzurt.slice().sort(function(a, b) {
        return b.count - a.count;
      });
    },
    FenyitesTipusok: function() {
      return this.FenyitesTipusKigyujtes(this.FegyelmiUgyek);
    },
    FenyitesTipusokSzurt: function() {
      return this.FenyitesTipusKigyujtes(this.SzurtFegyelmiUgyek);
    },
    FenyitesTipusokDarabSzerintCsokkenoSzurt: function() {
      return this.FenyitesTipusokSzurt.slice().sort(function(a, b) {
        return b.count - a.count;
      });
    },
    OsszesTagSzurt: function() {
      var list = [];

      var intezetek = this.IntezetekSzurt.slice();

      var intezetekArr = intezetek.map(elem => {
        elem.selectedContainer = this.selectedIntezetek;
        elem.typeOrder = 1;
        elem.tagType = 'intezet';
        elem.key = elem.tagType + elem.id;
        return elem;
      });

      list = list.concat(intezetekArr);
      var ugyStatuszok = this.UgyStatuszokSzurt.slice();

      var ugyStatuszokArr = ugyStatuszok.map(elem => {
        elem.selectedContainer = this.selectedUgyStatuszok;
        elem.typeOrder = 2;
        elem.tagType = 'ugystatusz';
        elem.key = elem.tagType + elem.id;
        return elem;
      });

      list = list.concat(ugyStatuszokArr);

      var fenyitesStatuszok = this.FenyitesStatuszokSzurt.slice();

      var fenyitesStatuszokArr = fenyitesStatuszok.map(elem => {
        elem.selectedContainer = this.selectedFenyitesStatuszok;
        elem.typeOrder = 3;
        elem.tagType = 'fenyitesstatusz';
        elem.key = elem.tagType + elem.id;
        return elem;
      });

      list = list.concat(fenyitesStatuszokArr);

      var fenyitesTipusok = this.FenyitesTipusokSzurt.slice();

      var fenyitesTipusokArr = fenyitesTipusok.map(elem => {
        elem.selectedContainer = this.selectedFenyitesTipusok;
        elem.typeOrder = 4;
        elem.tagType = 'fenyitestipus';
        elem.key = elem.tagType + elem.id;
        return elem;
      });

      list = list.concat(fenyitesTipusokArr);

      var vm = this;
      list.sort(function(a, b) {
        var bIndex =
          b.selectedContainer.findIndex(x => x == b.id) == -1 ? 0 : 1;
        var aIndex =
          a.selectedContainer.findIndex(x => x == a.id) == -1 ? 0 : 1;
        if (aIndex != bIndex) {
          return bIndex - aIndex;
        } else {
          if (b.typeOrder != a.typeOrder) {
            return a.typeOrder - b.typeOrder;
          } else {
            return b.count - a.count;
          }
        }
      });
      return list;
    },
    isVegrehajtasravaroFilterEnabled() {
      return this.selectedFenyitesStatuszok.includes('Fenyítés kiszabva');
    },
    isVegrehajtasAlattFilterEnabled() {
      return this.selectedFenyitesStatuszok.includes('Végrehajtás alatt');
    },
    // isSzallitasraVarFilterEnabled() {
    //   return this.selectedFenyitesStatuszok.includes('Szállításra vár');
    // },
  },

  mixins: [Defer()],
};
</script>
<style scoped>
.dropdown-menu {
  width: 200px;
}

.ujuenyitesbtn,
.ujuenyitesbtn + .dataTables_filter {
  display: inline-block;
}

.vuebar-element.withheader {
  height: calc(100vh - 181px);
  max-width: 100%;
}
/* .uj-fenyites i {
    -webkit-transition: -webkit-transform 0.3s ease-out;
    -moz-transition: -moz-transform 0.3s ease-out;
    transition: transform 0.3s ease-out;
  }

  .uj-fenyites:hover i {
    -webkit-transform: rotate(90deg);
    transform: rotate(90deg);
    line-height: 1;
    width: 22.5px;
    height: 15px;
    margin-left: 1px;
    margin-top: 3px;
    margin-bottom: 3px;
    margin-right: 0;
  }

  .uj-fenyites:hover {
    line-height: 0.5;
  } */
</style>
