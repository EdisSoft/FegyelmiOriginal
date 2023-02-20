<template>
  <div class="modal-primary">
    <div class="modal-header">
      <button
        type="button"
        class="close"
        data-dismiss="modal"
        aria-label="Close"
        @click="Cancel()"
      >
        <span aria-hidden="true">×</span>
      </button>
      <h4 class="modal-title">
        A {{ GetUgyszam(fenyites) }} ügy nyomtatványai
      </h4>
    </div>
    <div class="modal-body">
      <b-table
        id="nyomtatott-dokumentumok-table"
        hover
        :items="items"
        :busy="isBusy"
        :fields="fields"
        :per-page="perPage"
        :current-page="currentPage"
        :sort-by="'Datum'"
        :sort-desc="true"
      >
        <div slot="table-busy" class="text-center my-2">
          <b-spinner class="align-middle"></b-spinner>
          <!-- <strong>Adatok letöltése...</strong> -->
        </div>
        <template slot="Nyomtatvany" slot-scope="data">
          {{ data.value }} / {{ data.item.Alszam }}
        </template>
        <template slot="Datum" slot-scope="data">
          {{ data.value | toDateTime }}
        </template>
        <template slot="AktivSzoveg" slot-scope="data">
          <div style="display: inline-block;white-space: nowrap;">
            <button
              v-if="data.item.AktivFl == true"
              class="btn btn-outline-success"
              v-on:click="ChangeIktatottNyomtatvanyStatusz(data.item.Id)"
            >
              {{ data.value }}
            </button>
            <button
              v-if="data.item.AktivFl != true"
              class="btn btn-outline-warning"
              v-on:click="ChangeIktatottNyomtatvanyStatusz(data.item.Id)"
            >
              {{ data.value }}
            </button>
            <i
              style="margin-left:5px"
              class="icon wb-info-circle"
              data-toggle="tooltip"
              data-placement="top"
              v-if="data.item.Modosito != null"
              v-bind:title="
                GetModosito({
                  modosito: data.item.Modosito,
                  datum: data.item.ModositasDatuma,
                })
              "
            ></i>
          </div>
        </template>
      </b-table>
      <b-pagination
        v-if="perPage < items.length"
        v-model="currentPage"
        :total-rows="items.length"
        :per-page="perPage"
        aria-controls="nyomtatott-dokumentumok-table"
        :hide-goto-end-buttons="true"
      ></b-pagination>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { FenyitesStoreTypes } from '../../store/modules/fenyites';
import { getUgyszam } from '../../utils/fenyitesUtils';

export default {
  name: 'nyomtatott-dokumentumok',
  data() {
    return {
      fenyites: null,
      perPage: 10,
      currentPage: 1,
      isBusy: true,
      sortBy: 'LetrehozasDatuma',
      sortDesc: true,
      fields: [
        {
          key: 'Datum',
          label: 'Dátum',
          sortable: true,
        },
        {
          key: 'Nyomtatvany',
          label: 'Dokumentum neve',
          sortable: true,
        },
        {
          key: 'Rogzito',
          label: 'Nyomtatta',
          sortable: true,
        },
        {
          key: 'AktivSzoveg',
          label: 'Dokumentum helye',
          sortable: true,
        },
      ],
      items: [],
    };
  },

  mounted() {},
  created() {},
  methods: {
    Cancel() {
      this.$root.$emit('bv::hide::modal', this.$options.name);
    },
    GetUgyszam() {
      return getUgyszam(this.fenyites);
    },
    GetModosito({ modosito, datum }) {
      if (modosito == null) return null;
      return 'Módosította: ' + modosito + ', ' + datum;
    },
    ChangeIktatottNyomtatvanyStatusz(iktatottNyomtatvanyId) {
      apiService
        .ChangeIktatottNyomtatvanyStatusz({ id: iktatottNyomtatvanyId })
        .then(result => {
          this.ReloadDataTable({ fegyelmiUgyId: this.id });
        })
        .catch(e => {
          console.error(e);
        });
    },
    ReloadDataTable({ fegyelmiUgyId }) {
      console.log('fegyelmi ügy id: ' + fegyelmiUgyId);
      apiService
        .GetDokumentumokNyomtatva({ id: fegyelmiUgyId })
        .then(result => {
          this.items = result;
          this.isBusy = false;
        })
        .catch(e => {
          this.isBusy = false;
          console.error(e);
          this.items = [];
        });
    },
  },
  computed: {
    ...mapGetters({}),
  },
  watch: {
    id: {
      handler: function(newValue, oldValue) {
        if (newValue && oldValue != newValue) {
          this.isBusy = true;

          this.fenyites = this.$store.getters[
            FenyitesStoreTypes.getters.getFenyitesById
          ](newValue);
          this.ReloadDataTable({ fegyelmiUgyId: newValue });
        }
      },
      deep: true,
      immediate: true,
    },
  },
  components: {},
  props: {
    id: {
      type: String,
      required: true,
    },
  },
};
</script>
