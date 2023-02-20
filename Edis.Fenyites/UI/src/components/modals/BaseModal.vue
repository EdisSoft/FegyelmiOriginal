<template>
  <div>
    <b-modal
      :id="modalId"
      :hide-header="true"
      :hide-footer="true"
      size="lg"
      centered
      lazy
      :no-close-on-backdrop="true"
      :dialog-class="dialogClass"
      :static="true"
      @hide="IsModalHidden"
      @show="IsModalShown"
    >
      <div>
        <slot></slot>
      </div>
    </b-modal>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import { AppStoreTypes } from '../../store/modules/app';
import store from '../../store';
import { useBaseModalHandler } from '../../utils/modal';

export default {
  name: 'base-modal',
  data() {
    return {
      key: 0,
    };
  },
  setup(props, context) {
    let { buttonsDisabled, Show, Hide, modalOpts } = useBaseModalHandler(
      props,
      context
    );
    return { buttonsDisabled, Show, Hide, modalOpts };
  },
  mounted() {},
  created() {},
  methods: {
    IsModalHidden() {
      //this.key++;
    },
    IsModalShown() {},
  },
  computed: {
    ...mapGetters({}),
  },
  watch: {},
  props: {
    modalId: {
      type: String,
      required: true,
    },
    dialogClass: {
      type: String,
      default: 'modal-left',
    },
  },
};
</script>
