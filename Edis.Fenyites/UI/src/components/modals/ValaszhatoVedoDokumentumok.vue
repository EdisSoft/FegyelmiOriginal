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
        Válasszon védői nyomtatványt a {{ GetUgyszam(fenyites) }} ügyhöz
      </h4>
    </div>
    <div class="modal-body">
      <ul class="list-unstyled row doc-list  p-4">
        <li
          v-for="(vedoDokumentum, idx) in vedoDokumentumok"
          :key="vedoDokumentum.Id"
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
              @click="DownloadFile(vedoDokumentum.Id)"
            >
              <span class="mt-0 mb-5">{{ vedoDokumentum.DisplayName }}</span>
            </div>
            <div class="pl-20"></div>
          </div>
        </li>
      </ul>
    </div>
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
  name: 'valaszthato-vedo-dokumentumok',
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
          // eslint-disable-next-line prettier/prettier
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
      vedoDokumentumok: AppStoreTypes.getters.getVedoDokumentumok,
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
