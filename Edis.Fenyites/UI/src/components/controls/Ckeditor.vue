<template>
  <div>
    <ckeditor
      ref="ckeditor"
      :editor="editor"
      v-model="editorData"
      :config="config"
      :name="name"
      :disabled="disabled"
      @focus="onEditorFocus"
      @ready="onEditorReady"
    ></ckeditor>
  </div>
</template>

<script>
import $ from 'jquery';
import ClassicEditor from '@ckeditor/ckeditor5-editor-classic/src/classiceditor';
import EssentialsPlugin from '@ckeditor/ckeditor5-essentials/src/essentials';
import BoldPlugin from '@ckeditor/ckeditor5-basic-styles/src/bold';
import ItalicPlugin from '@ckeditor/ckeditor5-basic-styles/src/italic';
import LinkPlugin from '@ckeditor/ckeditor5-link/src/link';
import ParagraphPlugin from '@ckeditor/ckeditor5-paragraph/src/paragraph';
import FontColorPlugin from '@ckeditor/ckeditor5-font/src/fontcolor';
import List from '@ckeditor/ckeditor5-list/src/list';
import Underline from '@ckeditor/ckeditor5-basic-styles/src/underline';
import RemoveFormat from '@ckeditor/ckeditor5-remove-format/src/removeformat';
import Plugin from '@ckeditor/ckeditor5-core/src/plugin';
import fullScreenIcon from '../../assets/images/ckeditor/fullscreen.svg';
import nofullScreenIcon from '../../assets/images/ckeditor/nofullscreen.svg';
import ButtonView from '@ckeditor/ckeditor5-ui/src/button/buttonview';

class MyCustomPlugin extends Plugin {
  init() {
    const editor = this.editor;

    editor.ui.componentFactory.add('myCustomPlugin', locale => {
      const view = new ButtonView(locale);
      view.set({
        label: 'Teljes képernyő',
        icon: fullScreenIcon,
        tooltip: true,
      });

      view.on('execute', () => {
        console.log('kattintottam');
        $('.ck.ck-editor').toggleClass('fullscreen');
      });

      return view;
    });
  }
}

export default {
  name: 'k-ckeditor',
  props: {
    value: { type: String },
    name: {
      type: String,
      required: true,
    },
    settings: {
      type: Object,
      default: function() {
        return {};
      },
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      isChanging: false,
      editor: ClassicEditor,
      editorDisabled: false,
      editorData: '',
      config: {
        plugins: [
          EssentialsPlugin,
          BoldPlugin,
          ItalicPlugin,
          LinkPlugin,
          ParagraphPlugin,
          List,
          Underline,
          RemoveFormat,
        ],
        extraPlugins: [MyCustomPlugin],
        toolbar: {
          items: [
            'bold',
            'italic',
            'underline',
            'removeformat',
            '|',
            'numberedlist',
            'bulletedlist',
            '|',
            'myCustomPlugin',
          ],
        },

        placeholder: '',
        language: 'hu',
        // disableResizeEditor: true,
        // dialogsInBody: true,
        // minHeight: 41, // set minimum height of editor
        // maxHeight: null,
        // tabsize: 2,
        // disableDragAndDrop: true,
      },
    };
  },
  mounted() {
    let vm = this;
    let config = { ...this.config, ...this.settings, disabled: this.disabled };
    vm.MyCustomPlugin;
    $('.modal-dialog').each(function(idx, element) {
      var $snote = $(element);
      $('.modal-dialog').click(function(e) {
        if (!$(e.target).closest('.ck.ck-editor').length) {
          if (
            $snote.find('.ck.ck-toolbar.ck-toolbar_grouping').hasClass('active')
          ) {
            //ya está desplegado
            $('.ck.ck-editor')
              .find('.ck.ck-toolbar.ck-toolbar_grouping')
              .removeClass('active')
              .slideUp();
          }
        }
      });
    });
  },
  methods: {
    onEditorReady() {
      var $ckeditor = $('.ck.ck-editor');
      $ckeditor.find('.ck-toolbar').hide();
    },
    onEditorFocus() {
      var $ckeditor = $('.ck.ck-editor');
      if (
        $ckeditor.find('.ck.ck-toolbar.ck-toolbar_grouping').hasClass('active')
      ) {
        //ya está desplegado
        // $ckeditor
        //   .find('.ck.ck-toolbar')
        //   .removeClass('active')
        //   .slideUp();
      } else {
        $ckeditor
          .find('.ck.ck-toolbar.ck-toolbar_grouping')
          .removeClass('active')
          .slideUp();
        $ckeditor
          .find('.ck.ck-toolbar.ck-toolbar_grouping')
          .addClass('active')
          .slideDown();
      }
      //   $ckeditor.each(function(idx, element) {
      //     var $snote = $(element);
      //     // $snote.on('focus', '.ck.ck-content.ck-editor__editable', function(e) {
      //     if ($snote.find('.ck.ck-toolbar').hasClass('active')) {
      //       //ya está desplegado
      //       // $ckeditor
      //       //   .find('.ck.ck-toolbar')
      //       //   .removeClass('active')
      //       //   .slideUp();
      //     } else {
      //       $ckeditor
      //         .find('.ck.ck-toolbar')
      //         .removeClass('active')
      //         .slideUp();
      //       $snote
      //         .find('.ck.ck-toolbar')
      //         .addClass('active')
      //         .slideDown();
      //     }
      //     // });
      //     $snote.on('focus', '.dropdown-toggle', function(e) {
      //       if (
      //         $('.modal.show .modal-body .note-editable').outerHeight() < 225 &&
      //         $('.modal.show .modal-body .modal-scroll').outerHeight() < 200
      //       ) {
      //         $(
      //           '.modal.show .modal-scroll, .modal.show .modal-scroll .vb-content'
      //         ).css('overflow', 'unset');
      //       }
      //     });
      //   });
    },
  },
  watch: {
    editorData: {
      handler: function(value, prev) {
        this.$emit('input', value);
      },
      immediate: true,
    },
  },
};
</script>
