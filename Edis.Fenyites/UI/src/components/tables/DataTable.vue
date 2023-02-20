<template>
  <table class="table" data-role="content">
    <slot />
  </table>
</template>

<script>
import $ from 'jquery';
import dataTable from 'datatables.net-bs4';
import { close } from 'fs';

export default {
  name: 'k-datatable',
  props: ['options', 'dataList', 'dataKey'],
  data: function() {
    return {
      eventTrackingTimeOut: null,
      defaultOptions: {
        mark: true,
      },
    };
  },
  mounted: function() {
    var vm = this;
    var table = $(this.$el).dataTable(this.options);

    table.on('search.dt', function() {
      if (vm.eventTrackingTimeOut) {
        clearTimeout(vm.eventTrackingTimeOut);
        vm.eventTrackingTimeOut = null;
      }

      var searchTerm = table.DataTable().search();

      if (searchTerm) {
        vm.eventTrackingTimeOut = setTimeout(function() {}, 2000);
      }
    });

    setTimeout(function() {
      table.DataTable().rows.add(vm.dataList);
      table.DataTable().draw();
    }, 200);

    $(this.$el).on('click', 'tr', function() {
      var data = table
        .api()
        .row(this)
        .data();
      vm.$emit('rowClick', data);
    });
  },
  watch: {
    dataList: function(dataList, prev) {
      var table = $(this.$el);
      var key = this.dataKey;

      setTimeout(() => {
        table.DataTable().clear();
        table.DataTable().rows.add(dataList);
        table.DataTable().draw();
        // --------------------------------------------------------------------
        // Spike - ne töröld ki !!!!!
        // --------------------------------------------------------------------
        // --------------------------------------------------------------------
        // var array = this.chunkArray(dataList, 100);
        // console.log(array);
        // console.log(array.length);
        // var i = 0;
        // array.forEach(function(entry) {
        //   i++;
        //   setTimeout(function() {
        //     console.log('addCucc - ' + entry.length);
        //     table.DataTable().rows.add(entry);
        //     table.DataTable().draw();
        //     console.log('endCucc');
        //   }, i * 200);
        // });
        // for (var i = 0; i < array.length; i++) {
        //   setTimeout(function() {
        //     debugger;
        //     console.log('index - ' + i);
        //     console.log('addCucc - ' + array[i].length);
        //     table.DataTable().rows.add(array[i]);
        //     table.DataTable().draw();
        //     console.log('endCucc');
        //   }, i * 50);
        // }
        // --------------------------------------------------------------------
        // --------------------------------------------------------------------
      }, 250);
    },
  },
  methods: {
    chunkArray: function(myArray, chunk_size) {
      var index = 0;
      var arrayLength = myArray.length;
      var tempArray = [];

      for (index = 0; index < arrayLength; index += chunk_size) {
        var myChunk = myArray.slice(index, index + chunk_size);
        // Do something if you want with the group
        tempArray.push(myChunk);
      }

      return tempArray;
    },
  },
  destroyed: function() {
    $(this.$el)
      .DataTable()
      .destroy();
  },
};
</script>
