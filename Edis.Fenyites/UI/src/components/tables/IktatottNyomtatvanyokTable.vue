<template>
  <div>
    <div v-if="isLoading" class="d-flex justify-content-center mt-4">
      <b-spinner variant="secondary"></b-spinner>
    </div>
    <div v-show="!isLoading">
      <k-datatable
        :options="iktatottDokumentumokDatatableOptions"
        :dataList="dokumentumok"
        :dataKey="'Id'"
        class="pointer table-hover iktatottDokumentumok-dt"
        ref="datatable"
      >
        <tfoot></tfoot>
      </k-datatable>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import $ from 'jquery';
import { apiService } from '../../main';

import { NyomtatvanyFunctions } from '../../functions/nyomtatvanyFunctions';
import Cimkek from '../../data/enums/Cimkek';
export default {
  name: 'iktatott-nyomtatvanyok-table',
  data() {
    return {
      dokumentumok: [],
      prevStr: '',
      isLoading: false,
    };
  },
  mounted() {},
  created() {
    this.GetDokumentumok(this.fegyelmiUgyId);
  },
  methods: {
    async GetDokumentumok(fegyelmiUgyId) {
      var filteredList = [];

      this.isLoading = true;
      if (fegyelmiUgyId) {
        try {
          let list = await apiService.GetDokumentumokNyomtatva({
            id: fegyelmiUgyId,
          });
          list.forEach((dokumentum) => {
            filteredList.push({
              id: dokumentum.Id,
              iratszam:
                this.fegyelmiUgy.UgyIntezetAzon +
                '/' +
                this.fegyelmiUgy.UgyEve +
                '/' +
                this.fegyelmiUgy.UgySzama +
                '/' +
                dokumentum.Alszam,
              nev: dokumentum.DokumentumTipus,
              keszitesdatum: this.$options.filters.toDateTime(dokumentum.Datum),
              tipusCimkeId: dokumentum.DokumentumTipusCimkeId,
              fegyelmiUgyId: dokumentum.FegyelmiUgyId,
              naploId: dokumentum.NaploId,
              inaktivFl: dokumentum.InaktivFl,
            });
          });
          let newStr = JSON.stringify(filteredList);
          if (this.prevStr != newStr) {
            this.dokumentumok = filteredList;
          }
          this.prevStr = newStr;
        } catch (error) {
          this.isLoading = error.statusText == 'abort';
          console.log(error);
          this.dokumentumok = [];
        }
      }
      this.isLoading = false;
    },
  },
  computed: {
    ...mapGetters({}),
    iktatottDokumentumokDatatableOptions: function () {
      var vm = this;
      var options = {
        order: [[2, 'desc']],
        aoColumns: [
          {
            mDataProp: 'iratszam',
            sTitle: 'Irat száma',
            sClass: 'dt-td-center',
            mRender: function (data, type, row, meta) {
              var icon =
                '<span ' +
                (row.inaktivFl == true ? 'class="blue-grey-400"' : '') +
                '>' +
                data +
                '</span>';
              return icon;
            },
          },
          {
            mDataProp: 'nev',
            sTitle: 'Dokumentum neve',
            sClass: 'dt-td-center',
            mRender: function (data, type, row, meta) {
              var icon =
                '<span ' +
                (row.inaktivFl == true ? 'class="blue-grey-400"' : '') +
                '>' +
                data +
                '</span>';
              return icon;
            },
          },
          {
            mDataProp: 'keszitesdatum',
            sTitle: 'Készítés ideje',
            sClass: 'dt-td-center',
            mRender: function (data, type, row, meta) {
              var icon =
                '<span ' +
                (row.inaktivFl == true ? 'class="blue-grey-400"' : '') +
                '>' +
                data +
                '</span>';
              return icon;
            },
          },
          {
            mDataProp: null,
            sTitle: '&nbsp;-',
            bSortable: false,
            sClass: 'dt-td-center dt-td-noClick',
            sWidth: '55px',
            mRender: function (data, type, row, meta) {
              if (row.inaktivFl == true) {
                var icon =
                  '<span class="blue-grey-400"><i class="icon wb-trash" /></span>';
              } else {
                var icon =
                  '<span class="dt-dokumentumnyomtatas ' +
                  (row.inaktivFl == true ? 'blue-grey-400' : '') +
                  '"><i class="icon wb-print" /></span>';
              }
              return icon;
            },
          },
        ],
        paging: false,
        responsive: false,
        deferRender: true,
        sDom: `<'row dt-panelmenu'<'col-sm-12 col-md-5  col-xl-5 'i><'col-sm-12 col-md-7 col-xl-7  text-right'<'ujuenyitesbtn'>f>>
          <'row'<'col-sm-12'tr>>
          <'row dt-panelfooter'<'col-sm-12 col-md-6 'lB><'col-sm-12 col-md-6 'p>>`,
        initComplete: function initComplete() {
          //vm.UjFegyelmiUgyBtn();
        },
        createdRow: function (row, data, rowIndex) {
          $(row).attr('data-id', data.id);
          $(row).css('cursor', 'pointer');

          $(row)
            .find('.dt-dokumentumnyomtatas')
            .click(function (e) {
              let cimke = Cimkek.IktatottDokumentumTipus;
              switch (data.tipusCimkeId) {
                // Értesítő lap
                case cimke.ErtesitoLap: {
                  NyomtatvanyFunctions.ErtesitoLapNyomtatas(
                    data.fegyelmiUgyId,
                    data.id
                  );
                  break;
                }
                case cimke.ElkulonitesiLap: {
                  NyomtatvanyFunctions.ElkulonitoLapNyomtatas({
                    //fegyelmiUgyId: data.fegyelmiUgyId,
                    iktatasId: data.id,
                  });
                  break;
                }
                case cimke.AlairasMegtagadasa: {
                  NyomtatvanyFunctions.AlairasMegtagadasaNyomtatvany({
                    //fegyelmiUgyId: data.fegyelmiUgyId,
                    iktatasId: data.id,
                  });
                  break;
                }
                case cimke.FegyelmiLap: {
                  NyomtatvanyFunctions.FegyelmiLapokNyomtatasa(
                    //data.fegyelmiUgyId,
                    { iktatasId: data.id }
                  );
                  break;
                }
                case cimke.TajekoztatasNemKerNyomtatvanyt: {
                  NyomtatvanyFunctions.TajekoztatoNyomtatas(
                    data.fegyelmiUgyId,
                    false,
                    data.id
                  );
                  break;
                }
                case cimke.TajekoztatasKerNyomtatvanyt: {
                  NyomtatvanyFunctions.TajekoztatoNyomtatas(
                    data.fegyelmiUgyId,
                    true,
                    data.id
                  );
                  break;
                }
                case cimke.TanuMeghallgatasa: {
                  NyomtatvanyFunctions.TanuMeghallgatasiJegyzokonyvNyomtatas({
                    iktatasIds: [data.id],
                  });
                  break;
                }
                case cimke.SzemelyiAllomanyiTanuMeghallgatasa: {
                  NyomtatvanyFunctions.SzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvNyomtatas(
                    {
                      iktatasIds: [data.id],
                    }
                  );
                  break;
                }
                case cimke.TargyalasiJegyzokonyv: {
                  NyomtatvanyFunctions.ElsoFokuTargyalsiJegyzokonyvNyomtatas({
                    iktatasIds: [data.id],
                  });
                  break;
                }
                case cimke.FegyelmiHatarozat: {
                  NyomtatvanyFunctions.FegyelmiHatarozatNyomtatas({
                    fegyelmiUgyId: data.fegyelmiUgyId,
                    iktatasId: data.id,
                  });
                  break;
                }
                case cimke.FegyelmiHatarozatMegszuntetese: {
                  NyomtatvanyFunctions.FegyelmiHatarozatMegszuntetesNyomtatas({
                    fegyelmiUgyId: data.fegyelmiUgyId,
                    iktatasId: data.id,
                  });
                  break;
                }
                case cimke.ReintegraciosTisztiHatarozat: {
                  NyomtatvanyFunctions.HatarozatReintegraciosNyomtatas({
                    fegyelmiUgyId: data.fegyelmiUgyId,
                    iktatasId: data.id,
                  });
                  break;
                }
                case cimke.ReintegraciosTisztiKioktatas: {
                  NyomtatvanyFunctions.KioktatasReintegraciosJogkorbenNyomtatas(
                    {
                      fegyelmiUgyId: data.fegyelmiUgyId,
                      iktatasId: data.id,
                    }
                  );
                  break;
                }
                case cimke.EljarasAlaVontMeghallgatasa: {
                  NyomtatvanyFunctions.EljarasAlaVontMeghallgatasiJegyzokonyvNyomtatas(
                    { iktatasIds: [data.id] }
                  );
                  break;
                }
                case cimke.KirendeltVedoKereseNyilatkozat: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.VedoKirendeleseNyilatkozatNyomtatas({
                    iktatasIds: iktatasIds,
                  });
                  break;
                }
                case cimke.KirendeltVedoKerese: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.VedoKirendeleseNyomtatas({
                    iktatasId: iktatasIds[0],
                  });
                  break;
                }
                case cimke.MeghatalmazottVedoKereseNyilatkozat: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.MeghatalmazottVedoKirendeleseNyilatkozatNyomtatas(
                    {
                      iktatasIds: iktatasIds,
                    }
                  );
                  break;
                }
                case cimke.MeghatalmazottVedoKerese: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.MeghatalmazottVedoKirendeleseNyomtatas({
                    iktatasId: iktatasIds[0],
                  });
                  break;
                }
                case cimke.VedoTelefonosTajekoztatasa: {
                  var iktatasId = data.id;
                  NyomtatvanyFunctions.VedoTelefonosTajekoztatasaNyomtatasDocX({
                    iktatasId: iktatasId,
                  });
                  break;
                }
                case cimke.MasodfokuTargyalasiJegyzokonyv: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.FegyelmiTargyalasiJegyzokonyvMasodfokNyomtatas(
                    {
                      iktatasIds: iktatasIds,
                    }
                  );
                  break;
                }
                case cimke.MasodfokuHatarozat: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatNyomtatas({
                    iktatasIds: iktatasIds,
                  });
                  break;
                }
                case cimke.MasodfokuHatarozatMegszuntetese: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.MasodfokuFegyelmiHatarozatMegszunteteseNyomtatas(
                    {
                      iktatasIds: iktatasIds,
                    }
                  );
                  break;
                }
                case cimke.SzembesitesiJegyzokonyvNyomtatvany: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.SzembesitesiJegyzokonyvNyomtatas({
                    iktatasIds: iktatasIds,
                  });
                  break;
                }
                case cimke.MaganelzarasMegkezdesenekRogziteseNyomtatvany: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.VegrehajtasiLapNyomtatas({
                    iktatasIds: iktatasIds,
                  });
                  break;
                }
                case cimke.KozvetitoiEljarasKezdemenyezese: {
                  var iktatasIds = [data.id];
                  NyomtatvanyFunctions.KozvetitoiEljarasonReszvetelNyomtatas({
                    iktatasIds: iktatasIds,
                  });
                  break;
                }
                case cimke.OsszefoglaloJelentes: {
                  NyomtatvanyFunctions.OsszefoglaloJelentesNyomtatas({
                    iktatasId: data.id,
                  });
                  break;
                }
                case cimke.OsszefoglaloJelentesDocx: {
                  NyomtatvanyFunctions.OsszefoglaloJelentesDocxNyomtatas({
                    iktatasId: data.id,
                  });
                  break;
                }
                case cimke.KarjelentoLap: {
                  NyomtatvanyFunctions.KarjelentoLapDocxNyomtatas({
                    iktatasId: data.id,
                  });
                  break;
                }
                case cimke.BvBankKarjelentoLap: {
                  NyomtatvanyFunctions.OtosSzamuMellekletDocxNyomtatas({
                    fegyelmiUgyId: data.fegyelmiUgyId,
                  });
                  break;
                }
                case cimke.BuntetoFeljelentes: {
                  NyomtatvanyFunctions.BuntetoFeljelentesDocxNyomtatas({
                    iktatasId: data.id,
                  });
                  break;
                }
                default:
                  throw 'Nincs felvéve a naplótípus id: ' + data.tipusCimkeId;
                  break;
              }
            });
        },
      };
      return options;
    },
    fegyelmiUgyId() {
      if (!this.fegyelmiUgy) {
        return null;
      }
      return this.fegyelmiUgy.FegyelmiUgyId;
    },
  },
  watch: {
    isActive: {
      handler: function (value) {
        if (value) {
          //this.StartLoader();
        }
      },
      immediate: true,
    },
    fegyelmiUgyId: {
      handler: function (value) {},
      immediate: true,
    },
  },
  props: {
    fegyelmiUgy: {
      type: [Object, String],
      default: null,
    },
  },
  components: {},
};
</script>