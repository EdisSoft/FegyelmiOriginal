<template>
  <div class="file-upload-container">
    <vue-dropzone
      :id="id"
      ref="dropzone"
      :options="dropzoneOptions"
      @vdropzone-removed-file="OnRemovedfile"
      @vdropzone-error="OnError"
      @vdropzone-success="OnSuccess"
      :destroyDropzone="false"
    >
    </vue-dropzone>
  </div>
</template>

<script>
import settings from '../../data/settings';
import { vue2DropzoneHun } from '../../plugins/vueDropzone';
import $ from 'jquery';

export default {
  name: 'k-file-upload',
  data() {
    let vm = this;
    return {
      dropzoneOptions: {
        ...vue2DropzoneHun,
        url: settings.baseUrlCsatolmanyKezelo + 'Csatolmany/Add',
        thumbnailWidth: 200,
        method: 'POST',
        timeout: 0,
        addRemoveLinks: true,
        paramName: 'File',
        params: {
          ProjectName: vm.projectName,
        },
      },
    };
  },
  mounted() {},
  created() {},
  methods: {
    async OnRemovedfile(file) {
      // Nem lett feltöltve
      if (!file.xhr || !file.xhr.responseText) {
        return;
      }

      let response = JSON.parse(file.xhr.responseText);
      if (response.serverWarning) {
        return;
      }
      let fileData = response.Uploaded[0];
      let fileUrl = fileData.FileUrl;
      this.$emit('input', this.value.filter(f => f != fileUrl));
      try {
        let result = await $.post(
          settings.baseUrlCsatolmanyKezelo + 'Csatolmany/Delete',
          { uploadUrl: fileUrl },

          'application/json'
        );
      } catch (error) {
        console.log(error);
      }
    },
    OnSuccess(file, response) {
      this.$emit('input', [
        ...this.value,
        ...response.Uploaded.map(m => m.FileUrl),
      ]);
    },
    OnError(file, response) {
      this.$emit('OnUploadError', { file, response });
    },
  },
  computed: {},
  watch: {},
  props: {
    id: {
      type: String,
      required: true,
    },
    value: {
      type: Array,
      default: function() {
        return [];
      },
    },
    projectName: {
      type: String,
      required: true,
    },
  },
  components: {},
};
</script>
<style lang="css"></style>
