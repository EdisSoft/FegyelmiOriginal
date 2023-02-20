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
        Válasszon nyomtatványt a {{ GetUgyszam(fenyites) }} ügyhöz
      </h4>
    </div>
    <div class="modal-body">
      <ul class="list-unstyled row doc-list  p-4">
        <li
          v-for="(dokumentum, idx) in dokumentumok"
          :key="dokumentum.Id"
          class="list-item col-6 py-2 "
        >
          <div class="media ">
            <div class="el-center list-item-border-botw">
              <span class="avatar" href="javascript:void(0)">
                {{ idx + 1 }}.
              </span>
            </div>
            <div
              class="media-body text-left el-center py-1 list-item-border-bot pointer"
              @click="DownloadFile(dokumentum.Id)"
            >
              <span class="mt-0 mb-5">{{ dokumentum.DisplayName }}</span>
            </div>
            <div class="pl-20"></div>
          </div>
        </li>
      </ul>
    </div>
    <!-- <div class="modal-footer">
      <button type="button" class="btn btn-default" data-dismiss="modal">
        Close
      </button>
      <button type="button" class="btn btn-primary">Save changes</button>
    </div> -->
    <!-- <b-table
      id="valaszthato-dokumentumok-table"
      hover
      :items="dokumentumok"
      :fields="fields"
      :per-page="perPage"
      :current-page="currentPage"
    >

    </b-table> -->
    <!-- <b-pagination
      v-model="currentPage"
      :total-rows="dokumentumok.length"
      :per-page="perPage"
      aria-controls="valaszthato-dokumentumok-table"
      :hide-goto-end-buttons="true"
    ></b-pagination> -->
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService } from '../../main';
import { AppStoreTypes } from '../../store/modules/app';
import settings from '../../data/settings';
import { FenyitesStoreTypes } from '../../store/modules/fenyites';
import { getUgyszam } from '../../utils/fenyitesUtils';

export default {
  name: 'valaszthato-dokumentumok',
  data() {
    return {
      perPage: 5,
      currentPage: 1,
      fields: [
        {
          key: 'DisplayName',
          label: 'Dokumentum neve',
          sortable: true,
        },
      ],
      fenyites: null,
    };
  },
  mounted() {},
  created() {},
  methods: {
    Cancel() {
      this.$root.$emit('bv::hide::modal', this.$options.name);
    },
    DownloadFile(nyomtatvanyId) {
      window.open(
        settings.baseUrl +
          `Api/Nyomtatvany/NyomtatvanyGeneralas?nyomtatvanyId=${nyomtatvanyId}&fegyelmiUgyId=${this.fenyites.FegyelmiUgyId}`,
        '_system'
      );
    },

    GetUgyszam() {
      return getUgyszam(this.fenyites);
    },
  },
  computed: {
    ...mapGetters({
      dokumentumok: AppStoreTypes.getters.getDokumentumok,
    }),
  },
  watch: {
    id: {
      handler: function(newValue, oldValue) {
        if (newValue && oldValue != newValue) {
          this.fenyites = this.$store.getters[
            FenyitesStoreTypes.getters.getFenyitesById
          ](newValue);
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
<style scoped>
.doc-list {
  text-align: center;
}

.el-center {
  transform: translateY(-50%);
  top: 50%;
  position: relative;
}
.media {
  height: 100%;
}
.list-item-border-bot {
  border-bottom: 1px solid #bcbdbe;
}
.list-item-border-botw {
  border-bottom: 1px solid white;
}
.list-item-border-bot:hover {
  background-color: #dee2e6;
}
</style>
