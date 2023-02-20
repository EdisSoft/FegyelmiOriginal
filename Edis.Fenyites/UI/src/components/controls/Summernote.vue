<template>
  <div :class="{ 'summernote-disabled': disabled }">
    <textarea ref="summernote" :name="name"></textarea>
  </div>
</template>

<script>
import $ from 'jquery';

export default {
  name: 'k-summernote',
  props: {
    value: {},
    name: {
      type: String,
      required: true,
    },
    config: {
      type: Object,
      default: function() {
        return {
          toolbar: [
            // [groupName, [list of button]]
            ['style', ['bold', 'italic', 'underline', 'clear']],
            //['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            // ['forecolor', ['forecolor']],
            ['para', ['ul', 'ol']],
            ['fullscreen', ['fullscreen']],
          ],
          placeholder: '',
          disableResizeEditor: true,
          dialogsInBody: true,
          minHeight: 41, // set minimum height of editor
          maxHeight: null,
          tabsize: 2,
          disableDragAndDrop: true,
          cleaner: {
            action: 'both', // both|button|paste 'button' only cleans via toolbar button, 'paste' only clean when pasting content, both does both options.
            newline: '<br>', // Summernote's default is to use '<p><br></p>'
            notStyle: 'position:absolute;top:0;left:0;right:0', // Position of Notification
            icon: '<i class="note-icon">[Your Button]</i>',
            keepHtml: true, // Remove all Html formats
            keepOnlyTags: [
              '<p>',
              '<br>',
              '<ul>',
              '<li>',
              '<b>',
              '<strong>',
              '<i>',
              '<a>',
              '<u>',
              '<b>',
            ], // If keepHtml is true, remove all tags except these
            keepClasses: false, // Remove Classes
            badTags: [
              //'style',
              'script',
              'applet',
              'embed',
              'noframes',
              'noscript',
              'html',
              //'font',
            ], // Remove full tags with contents
            // badAttributes: ['style', 'start', 'color'], // Remove attributes from remaining tags
            badAttributes: ['start'],
            limitChars: false, // 0/false|# 0/false disables option
            limitDisplay: 'both', // text|html|both
            limitStop: false, // true/false
          },
        };
      },
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
  data: function() {
    return {
      isChanging: false,
    };
  },

  mounted() {
    let vm = this;
    let config = { ...this.config, ...this.settings, disabled: this.disabled };

    $('.modal-body .modal-body').on('click', function(e) {
      if (
        $(
          '.modal.show .modal-body .note-btn-group.show .dropdown-menu'
        ).hasClass('show')
      ) {
        $('.modal.show .modal-scroll').css('overflow', 'hidden');
        $('.modal.show .modal-scroll .vb-content').css(
          'overflow',
          'hidden scroll'
        );
      }
    });

    config.callbacks = {
      // onInit: function() {
      //   $(vm.$refs.summernote).summernote('code', vm.model);
      // },
      // onChange: function(content) {
      //   var html = content.trim();
      //   // fix for problem with ENTER and new paragraphs
      //   if (html.substring(0, 5) !== '<div>') {
      //     $(vm.$refs.summernote).summernote('code', '<div><br></div>' + html);
      //   }
      // },
      onPaste: function(e) {
        // var bufferText = (
        //   (e.originalEvent || e).clipboardData || window.clipboardData
        // ).getData('Text');
        // e.preventDefault();
        // //document.execCommand('insertText', false, bufferText);
        // $(vm.$refs.summernote).summernote('pasteHTML', bufferText);
      },
    };

    if (!config.lang) {
      config.lang = 'hu-HU';
    }

    var $instance = $(vm.$refs.summernote).summernote(config);

    $instance.on('summernote.change', function() {
      if (!vm.isChanging) {
        vm.isChanging = true;
        let val = vm.getData();
        vm.$emit('input', val);
        vm.$nextTick(function() {
          vm.isChanging = false;
        });
      }
    });

    var $summernotes = $('.note-editor');
    $summernotes.find('.note-toolbar').hide();
    $summernotes.each(function(idx, element) {
      var $snote = $(element);
      $snote.on('focus', '.note-editable', function(e) {
        if ($snote.find('.note-toolbar').hasClass('active')) {
          //ya está desplegado
        } else {
          $summernotes
            .find('.note-toolbar')
            .removeClass('active')
            .slideUp();
          $snote
            .find('.note-toolbar')
            .addClass('active')
            .slideDown();
        }
      });
      $snote.on('focus', '.dropdown-toggle', function(e) {
        if (
          $('.modal.show .modal-body .note-editable').outerHeight() < 225 &&
          $('.modal.show .modal-body .modal-scroll').outerHeight() < 200
        ) {
          $(
            '.modal.show .modal-scroll, .modal.show .modal-scroll .vb-content'
          ).css('overflow', 'unset');
        }
      });
    });

    $('.modal-dialog').each(function(idx, element) {
      var $snote = $(element);
      $('.modal-dialog').click(function(e) {
        if (!$(e.target).closest('.note-editor').length) {
          if ($snote.find('.note-toolbar').hasClass('active')) {
            //ya está desplegado
            $summernotes
              .find('.note-toolbar')
              .removeClass('active')
              .slideUp();
          }
        }
      });
    });

    $('.note-editor .note-editable').css('line-height', '100%');
  },
  methods: {
    getData() {
      var vm = this;

      var data = $(vm.$refs.summernote).summernote('code');

      if (data.replace(/&nbsp;|<\/?[^>]+(>|$)/g, '').trim().length == 0) {
        return '';
      }

      return data;
    },
  },
  watch: {
    value: {
      handler: function(value, prev) {
        var vm = this;
        if (vm.isChanging) {
          return;
        }
        vm.isChanging = true;
        var code = value === null ? '' : value;
        $(vm.$refs.summernote).summernote('code', code);
        vm.$nextTick(function() {
          vm.isChanging = false;
        });
      },
      immediate: true,
    },
    disabled: {
      handler: function(value, prev) {
        if (!this.$refs.summernote) {
          return;
        }
        if (value) {
          $(this.$refs.summernote).summernote('disable');
        } else {
          $(this.$refs.summernote).summernote('enable');
        }
      },
      immediate: true,
    },
  },
  destroyed: function() {
    $(this.$refs.summernote).summernote('destroy');
  },
};
</script>
