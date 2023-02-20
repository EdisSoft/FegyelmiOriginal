<template>
  <div :id="id" class="szuro-badgek">
    <transition-group name="list-complete" class="list-complete-items">
      <div
        v-for="(badgeGroup, key, index) in badgeGrouped"
        :key="key + index"
        class="mb-0 px-0 py-0 list-complete-item badge-separator"
      >
        <transition-group
          name="list-complete"
          class="list-complete-items"
          :id="id + '-' + key"
        >
          <span
            v-for="badge in badgeGroup"
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
                v-b-tooltip="{
                  title: badge.szuro.tooltip,
                  html: true,
                  container: '#' + id,
                  delay: { show: 500, hide: 100 },
                  trigger: 'hover',
                }"
              >
                <span>{{ badge.Nev }}</span>
                <span class="badge badge-pill up badge-default">{{
                  badge.count
                }}</span>
                <span class="clear"></span>
              </div>
            </a>
          </span>
        </transition-group>
      </div>
    </transition-group>
  </div>
</template>

<script>
export default {
  name: 'szuro-badgek-grouped',
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
    badgeGrouped() {
      let badgek = this.badgekOrdered;
      let badgeCollection = {};
      badgek.forEach(element => {
        let key = element.szuro.propNev;
        //let key = 'nemKellGroupt';
        if (badgeCollection[key]) {
          badgeCollection[key].push(element);
        } else {
          badgeCollection[key] = [element];
        }
      });
      return badgeCollection;
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
