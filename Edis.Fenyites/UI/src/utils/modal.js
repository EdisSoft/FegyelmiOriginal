import { ref, onMounted, onBeforeUnmount } from '@vue/composition-api';
import { eventBus, apiService, jfkApp } from '../main';
import { timeout } from './common';

export const useModalHandler = function(props, context) {
  let buttonsDisabled = ref(false);
  let isModalVisible = ref(false);

  let modalOpened = function(bvEvent, modalId) {
    if (context.parent.id == modalId) {
      buttonsDisabled.value = false;
      isModalVisible.value = true;
    }
  };
  let modalClosed = function(bvEvent, modalId) {
    if (context.parent.id == modalId) {
      buttonsDisabled.value = true;
      isModalVisible.value = false;
    }
  };
  onMounted(() => {
    context.root.$on('bv::modal::show', modalOpened);
    context.root.$on('bv::modal::hide', modalClosed);
  });
  onBeforeUnmount(() => {
    context.root.$off('bv::modal::show', modalOpened);
    context.root.$off('bv::modal::hide', modalClosed);
  });

  let Show = function() {
    context.root.$emit('bv::show::modal', context.parent.id);
  };
  let Hide = function() {
    context.root.$emit('bv::hide::modal', context.parent.id);
  };

  return { buttonsDisabled, isModalVisible, Show, Hide };
};

export const useBaseModalHandler = function(props, context) {
  let buttonsDisabled = ref(false);
  let isModalVisible = ref(false);
  let modalOpts = ref(null);

  let Show = async function(opts) {
    modalOpts.value = opts;
    await timeout(50);
    context.root.$emit('bv::show::modal', props.modalId);
  };
  let Hide = async function(opts) {
    await timeout(50);
    context.root.$emit('bv::hide::modal', props.modalId);
  };

  let modalOpened = function(bvEvent, modalId) {
    if (props.modalId == modalId) {
      buttonsDisabled.value = false;
      isModalVisible.value = true;
    }
  };
  let modalClosed = function(bvEvent, modalId) {
    if (props.modalId == modalId) {
      modalOpts.value = { state: false };
      buttonsDisabled.value = true;
      isModalVisible.value = false;
    }
  };
  onMounted(() => {
    context.root.$on('bv::modal::show', modalOpened);
    context.root.$on('bv::modal::hide', modalClosed);
    eventBus.$on('Modal:' + props.modalId, Show);
    eventBus.$on('Modal:' + props.modalId + ':hide', Hide);
    eventBus.$on('Modal:all:hide', Hide);
  });
  onBeforeUnmount(() => {
    context.root.$off('bv::modal::show', modalOpened);
    context.root.$off('bv::modal::hide', modalClosed);
    eventBus.$off('Modal:' + props.modalId, Show);
    eventBus.$off('Modal:' + props.modalId + ':hide', Hide);
    eventBus.$off('Modal:all:hide', Hide);
  });

  return { buttonsDisabled, isModalVisible, Show, Hide, modalOpts };
};
export const useSimpleModalHandler = function(props, context) {
  var parentModalId = context.parent.$parent.modalId;
  var parentModalOptions = context.parent.$parent.modalOpts;
  let Hide = async function() {
    await timeout(50);
    context.root.$emit('bv::hide::modal', parentModalId);
  };

  let buttonsDisabled = ref(false);
  let modalOpened = function(bvEvent, modalId) {
    if (parentModalId == modalId) {
      buttonsDisabled.value = false;
    }
  };
  let modalClosed = function(bvEvent, modalId) {
    if (parentModalId == modalId) {
      buttonsDisabled.value = true;
    }
  };
  onMounted(() => {
    context.root.$on('bv::modal::show', modalOpened);
    context.root.$on('bv::modal::hide', modalClosed);
  });
  onBeforeUnmount(() => {
    context.root.$off('bv::modal::show', modalOpened);
    context.root.$off('bv::modal::hide', modalClosed);
  });
  let modalOpts = parentModalOptions;
  return { Hide, buttonsDisabled, modalOpts };
};
