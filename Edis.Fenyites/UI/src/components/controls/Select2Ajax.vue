<template>
  <k-select2
    ref="select2"
    v-model="selectValue"
    :options="getOptions"
    :fixValue="fixValue"
    class=""
    @nemTorolheto="$emit('nemTorolheto', $event)"
  ></k-select2>
</template>

<script>
export default {
  name: 'k-select2-ajax',
  data: function() {
    return {
      selectValue: null,
    };
  },
  mounted: function() {},
  methods: {
    AddOptions(val) {
      if (!val) {
        return;
      }
      let select2Comp = this.$refs.select2;
      let ids = this.GetIdsAsArray();
      if (select2Comp) {
        let select2 = select2Comp.select2;
        let value = val;
        if (!Array.isArray(val)) {
          value = [val];
        }
        value.forEach(element => {
          if (!ids.includes(element.id)) {
            var option = new Option(element.text, element.id, true, true);
            select2.append(option).trigger('change');
            ids.push(element.id);
          }
        });

        select2.trigger({
          type: 'select2:select',
          params: {
            data: val,
          },
        });
      } else {
        setTimeout(() => {
          this.AddOptions(val);
        }, 100);
      }
    },
    GetIdsAsArray() {
      let currentValues = [];
      if (!this.selectValue) {
        return [];
      }
      if (!Array.isArray(this.selectValue)) {
        currentValues = [this.selectValue];
      } else {
        currentValues = [...this.selectValue];
      }
      return currentValues;
    },
  },
  computed: {
    getOptions() {
      let vm = this;
      return {
        minimumInputLength: 2,
        ajax: {
          delay: 500,
          transport: function(params, success, failure) {
            var queryParameters = {
              term: params.data.term,
            };
            queryParameters[vm.queryProp] = vm.GetIdsAsArray();
            queryParameters = { ...vm.additionalParams, ...queryParameters };
            return vm.options
              .apiHandler({
                data: queryParameters,
                mock: true,
              })
              .then(function(response) {
                success(response);
              })
              .catch(function(error) {
                failure();
              });
          },
          processResults: function(data) {
            return {
              results: data,
            };
          },
        },
        ...this.options,
      };
    },
  },
  watch: {
    value: {
      handler: function(val) {
        this.selectValue = val;
      },
      immediate: true,
    },
    defaultValue: {
      handler: function(val) {
        this.AddOptions(val);
      },
      immediate: true,
    },
    selectValue: {
      handler: function(val) {
        this.$emit('input', val);
      },
    },
  },
  destroyed: function() {},

  props: {
    value: {
      type: [Array, String, Number],
      default: null,
    },
    fixValue: {
      type: [Array, String, Number],
      default: null,
    },
    defaultValue: {
      type: [Array, Object],
      default: null,
    },
    options: {
      type: Object,
      default: function() {
        return {};
      },
    },
    additionalParams: {
      type: Object,
      default: function() {
        return {};
      },
    },
    queryProp: {
      type: String,
      default: 'fogvatartottIds',
    },
  },
};
</script>
