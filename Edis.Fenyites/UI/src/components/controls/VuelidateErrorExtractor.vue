<template>
  <div :class="{ error: hasErrors }">
    <slot />
    <div class="errors" v-if="hasErrors">
      <div class="error" v-for="error in activeErrorMessages" :key="error">
        {{ error }}
      </div>
    </div>
  </div>
</template>

<script>
import { singleErrorExtractorMixin } from 'vuelidate-error-extractor';

export default {
  mixins: [singleErrorExtractorMixin],
  name: 'k-vuelidate-error-extractor',
};
</script>

<style>
.errors .error {
  color: #ff4c52;
  font-size: 11px;
}
.error input,
.error .select2-selection.select2-selection--single,
.error .note-editor.note-frame.card {
  border-color: #ff4c52;
}
</style>
