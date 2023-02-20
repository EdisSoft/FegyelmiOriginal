<template>
  <select>
    <slot></slot>
  </select>
</template>

<script>
import $ from 'jquery';
import select2 from 'select2';

export default {
  name: 'k-select2',
  props: ['options', 'value', 'fixValue', 'placeholder'],
  data: function() {
    return {
      select2: null,
      isMounted: false,
    };
  },
  mounted: function() {
    var vm = this;
    this.select2 = $(this.$el)
      .select2(this.GetOptions(this.options))
      .val(this.value)
      .trigger('change')
      .on('change', function(e) {
        var value = Array.from(e.target.selectedOptions).map(m => m.value);
        if (!vm.options.multiple) {
          value = value[0] || null;
        }
        vm.$emit('input', value);
      })
      .on('select2:unselecting', function(e) {
        let value = e.params.args.data.element.value;
        let text = e.params.args.data.element.text;
        if (Array.isArray(vm.fixValue)) {
          if (vm.fixValue.some(s => s + '' == value + '')) {
            e.preventDefault();
            vm.$emit('nemTorolheto', text);
          }
        } else if (vm.fixValue == value) {
          e.preventDefault();
          vm.$emit('nemTorolheto', text);
        }
      })
      .on('select2:open', function(e) {
        e.preventDefault();
        $('.select2-results > .select2-results__options').css(
          'max-height',
          '200px'
        );
        vm.$nextTick(() => {
          e.preventDefault();
          $('.select2-results > .select2-results__options').slimScroll({
            height: '100%',
            maxHeight: '200px',
            wheelStep: 5,
            color: '#8349F5',
            position: 'right',
            distance: '5px',
          });
        });
      });
    this.isMounted = true;
  },
  methods: {
    GetOptions(options) {
      if (!this.isMounted) {
        return null;
      }
      return {
        //theme: 'bootstrap4',
        templateSelection: state => {
          let removex = false;
          if (Array.isArray(this.fixValue)) {
            removex = this.fixValue.find(f => f == state.id) != null;
          } else {
            removex = this.fixValue == state.id;
          }
          return $(
            `<span class="${removex ? 'removex' : ''}">${state.text}</span>`
          );
        },
        placeholder: this.placeholder || 'Válasszon...',
        language: {
          locale: 'hu',
          inputTooShort: function(e) {
            var t = e.minimum - e.input.length;

            if (options.isSzakteruleti == true) {
              return 'Kezdje gépelni a személyi állományi tag nevét.';
            } else {
              return (
                'Kezdje gépelni a fogvatartott nevét vagy azonosítóját. Még ' +
                t +
                ' karakter hiányzik.'
              );
            }
          },
        },
        ...options,
      };
    },
  },
  watch: {
    value: function(value) {
      //this.$nextTick(() => {
      $(this.$el)
        .val(value)
        .trigger('change.select2');
      //})
    },
    options: function(options) {
      $(this.$el)
        .empty()
        .select2(this.GetOptions(this.options))
        .val(this.value)
        .trigger('change.select2');
    },
  },
  destroyed: function() {
    $(this.$el)
      .off()
      .select2('destroy');
  },
};
</script>

<style>
span.select2-container.select2-container--default.select2-container--open {
  z-index: 10000 !important;
}
.slimScrollBar {
  z-index: 1601 !important;
}
</style>
