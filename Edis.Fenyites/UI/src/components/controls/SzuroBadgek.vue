<template>
  <div :id="id" class="szuro-badgek">
    <transition-group name="list-complete">
      <span
        v-for="badge in badgekOrdered"
        :key="badge.Id + badge.szuro.propNev"
        class="list-complete-item filterbadge"
      >
        <a
          :class="{ active: badge.selected }"
          class="pointer"
          v-on:click="SelectBadge(badge)"
        >
          <div
            :class="[badge.szuro.class]"
            :id="badge.Id + badge.szuro.propNev"
          >
            <span>{{ badge.Nev }}</span>
            <span class="badge badge-pill up badge-default">{{
              badge.count
            }}</span>
            <span class="clear"></span>
          </div>
        </a>
        <b-tooltip
          :target="badge.Id + badge.szuro.propNev"
          triggers="hover"
          :delay="{ show: 500, hide: 100 }"
          :container="'#' + id"
        >
          {{ badge.szuro.tooltip }}
        </b-tooltip>
      </span>
    </transition-group>
  </div>
</template>

<script>
export default {
  name: 'szuro-badgek',
  data() {
    return {
      id: 'szuroBadgek-' + +new Date(),
    };
  },
  mounted() {},
  created() {},
  methods: {
    SelectBadge(badge) {
      let key = badge.szuro.propNev + badge.Id;
      let initialLength = this.selected.length;
      let badgek = this.selected.filter(s => s.key != key);
      if (badgek.length == initialLength) {
        badgek.push({ key: key, value: badge });
      }
      this.$emit('update:selected', badgek);
    },
    IsSelected(badge) {
      return this.selected.some(
        s =>
          s.value.szuro.propNev + s.value.Id == badge.szuro.propNev + badge.Id
      );
    },

    SortSzuroBadge(a, b) {
      let diffSelected = parseInt(b.selected) - parseInt(a.selected);
      if (diffSelected != 0) {
        return diffSelected;
      }
      let diffOrder = parseInt(a.szuro.order) - parseInt(b.szuro.order);
      if (diffOrder != 0) {
        return diffOrder;
      }
      return b.count - a.count;
    },
  },
  computed: {
    badgekOrdered() {
      let selected = this.selected;
      let badgek = this.badgek.map(m => {
        let badge = { ...m };
        badge.selected = this.IsSelected(badge) ? 1 : 0;
        return badge;
      });
      badgek = badgek.filter(f => f.selected || f.szuro.mapObj == this.mapObj);
      badgek.sort(this.SortSzuroBadge);
      return badgek;
    },
  },
  watch: {},
  props: {
    selected: {
      type: Array,
      required: true,
    },
    badgek: {
      type: Array,
      required: true,
    },
    mapObj: {
      type: String,
      required: true,
    },
  },
};
</script>
