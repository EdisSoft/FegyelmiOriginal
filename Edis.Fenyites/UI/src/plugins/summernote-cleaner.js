/* eslint-disable */
// prettier-ignore
(function(factory){
    if(typeof define==='function'&&define.amd){
        define(['jquery'],factory);
    }else if(typeof module==='object'&&module.exports){
        module.exports=factory(require('jquery'));
    }else{
        factory(window.jQuery);
    }
}
(function($){
    $.extend(true,$.summernote.lang,{
        'en-US':{
            cleaner:{
                tooltip:'Cleaner',
                //not:'Text has been Cleaned!!!'
            }
        }
    });
    $.extend($.summernote.options,{
        cleaner:{
            //notTime:2400, // Time to display Notifications.
            action:'both', // both|button|paste 'button' only cleans via toolbar button, 'paste' only clean when pasting content, both does both options.
            newline:'<br>', // Summernote's default is to use '<p><br></p>'
            //notStyle:'position:absolute;bottom:0;left:2px',
            icon:'<i class="note-icon"><svg xmlns="http://www.w3.org/2000/svg" id="libre-paintbrush" viewBox="0 0 14 14"><path d="m 11.821425,1 q 0.46875,0 0.82031,0.311384 0.35157,0.311384 0.35157,0.780134 0,0.421875 -0.30134,1.01116 -2.22322,4.212054 -3.11384,5.035715 -0.64956,0.609375 -1.45982,0.609375 -0.84375,0 -1.44978,-0.61942 -0.60603,-0.61942 -0.60603,-1.469866 0,-0.857143 0.61608,-1.419643 l 4.27232,-3.877232 Q 11.345985,1 11.821425,1 z m -6.08705,6.924107 q 0.26116,0.508928 0.71317,0.870536 0.45201,0.361607 1.00781,0.508928 l 0.007,0.475447 q 0.0268,1.426339 -0.86719,2.32366 Q 5.700895,13 4.261155,13 q -0.82366,0 -1.45982,-0.311384 -0.63616,-0.311384 -1.0212,-0.853795 -0.38505,-0.54241 -0.57924,-1.225446 -0.1942,-0.683036 -0.1942,-1.473214 0.0469,0.03348 0.27455,0.200893 0.22768,0.16741 0.41518,0.29799 0.1875,0.130581 0.39509,0.24442 0.20759,0.113839 0.30804,0.113839 0.27455,0 0.3683,-0.247767 0.16741,-0.441965 0.38505,-0.753349 0.21763,-0.311383 0.4654,-0.508928 0.24776,-0.197545 0.58928,-0.31808 0.34152,-0.120536 0.68974,-0.170759 0.34821,-0.05022 0.83705,-0.07031 z"/></svg></i>'
      }
    });
    $.extend($.summernote.plugins,{
        'cleaner':function(context){
            var self=this;
            var ui=$.summernote.ui;
            var $note=context.layoutInfo.note;
            var $editor=context.layoutInfo.editor;
            var options=context.options;
            var lang=options.langInfo;
            var cleanHTML=function(input){
                    // 1. remove line breaks / Mso classes
                    var stringStripper = /(\n|\r| class=(")?Mso[a-zA-Z]+(")?)/g; 
                    var output = input.replace(stringStripper, ' ');
                    // 2. strip Word generated HTML comments
                    var commentSripper = new RegExp('<!--(.*?)-->','g');
                    var output = output.replace(commentSripper, '');
                    var tagStripper = new RegExp('<(/)*(meta|link|span|\\?xml:|st1:|o:|font)(.*?)>','gi');
                    // 3. remove tags leave content if any
                    output = output.replace(tagStripper, '');
                    // 4. Remove everything in between and including tags '<style(.)style(.)>'
                    var badTags = ['style','script','applet','embed','noframes','noscript']; // style marad
            
                    for (var i=0; i< badTags.length; i++) {
                    tagStripper = new RegExp('<'+badTags[i]+'.*?'+badTags[i]+'(.*?)>', 'gi');
                    output = output.replace(tagStripper, '');
                    }
                    // 5. remove attributes ' style="..."'
                    var badAttributes = ['style','start']; //style marad
                    for (var i=0; i< badAttributes.length; i++) {
                        var attributeStripper = new RegExp(' ' + badAttributes[i] + '="(.*?)"','gi');
                        output = output.replace(attributeStripper, '');
                        attributeStripper = new RegExp(' ' + badAttributes[i] + '=\'(.*?)\'','gi');
                        output = output.replace(attributeStripper, '');
                    }
                    if (output.includes('<body'))
                    {
                        output = output.split(/<body[^>]*>/gi)[1].split(/<\/body>/gi)[0];//.replace(/<!\[[^>]*>/gi,"").trim();
                    }
                    output = output.replace(/<!\[[^>]*>/gi,"").trim();
                    output = output.replace(/<p >??<\/p>/g,'');
                    output = output.replace('nbsp;',' ');
                    return output;
            };
            var cleanText=function(txt,nlO){
                var sS=/(\r| class=(")?Mso[a-zA-Z]+(")?)/g;
                var out=txt.replace(sS,' ');
                var nL=/(\n)+/g;
                out=out.replace(nL,nlO);
                var cS=new RegExp('<!--(.*?)-->','gi');
                out=out.replace(cS,'');
                var tS=new RegExp('<(/)*(meta|link|span|\\?xml:|st1:|o:|font)(.*?)>','gi');
                out=out.replace(tS,'');
                var bT=['style','script','applet','embed','noframes','noscript'];
                for(var i=0;i<bT.length;i++){
                    tS=new RegExp('<'+bT[i]+'.*?'+bT[i]+'(.*?)>','gi');
                    out=out.replace(tS,'');
                }
                var bA=['style','start'];
                for(var ii=0;ii<bA.length;ii++){
                    var aS=new RegExp(' '+bA[i]+'="(.*?)"','gi');
                    out=out.replace(aS,'');
                }
                return out;
            };
            if(options.cleaner.action=='both'||options.cleaner.action=='button'){
                context.memo('button.cleaner',function(){
                    var button=ui.button({
                        contents:options.cleaner.icon,
                        tooltip:lang.cleaner.tooltip,
                        click:function(){
                            if($note.summernote('createRange').toString()){
                                var text=cleanText($note.summernote('createRange').toString(),options.cleaner.newline);
                                $note.summernote('pasteHTML',text);
                            }else{
                                var text=cleanText($note.summernote().text(),options.cleaner.newline);
                                var old=$note.summernote('code');
                            }
                            //$editor.find('.note-resizebar').append('<div class="summernote-cleanerAlert alert alert-success" style="'+options.cleaner.notStyle+'">'+lang.cleaner.not+'</div>');
                            //setTimeout(function(){$editor.find('.summernote-cleanerAlert').remove();},options.cleaner.notTime);
                        }
                    });
                    return button.render();
                });
            }
            this.events={
                // 'summernote.paste':function(we,e){
                //     if(options.cleaner.action=='both'||options.cleaner.action=='paste'){
                //         e.preventDefault();
                //         var ua=window.navigator.userAgent;
                //         var msie=ua.indexOf("MSIE ");
                //         if (msie>0||!!navigator.userAgent.match(/Trident.*rv\:11\./)){
                //             var text=window.clipboardData.getData("Text");
                //         }else{
                //             var text = e.originalEvent.clipboardData.getData('text/html');
                //             text = cleanHTML(text);
                //         }
                //         var text=cleanText(text,options.cleaner.newline);
                //         if($note.summernote('createRange').toString()){
                //             $note.summernote('pasteHTML',text);
                //         }else{
                //             var old=$note.summernote('code');
                //             $note.summernote('pasteHTML',text);
                //         }
                //         $editor.find('.note-resizebar').append('<div class="summernote-cleanerAlert alert alert-success" style="'+options.cleaner.notStyle+'">'+lang.cleaner.not+'</div>');
                //         setTimeout(function(){$editor.find('.summernote-cleanerAlert').remove();},options.cleaner.notTime);
                //     }
                // }
                'summernote.paste': function (we, e) {
                    if (options.cleaner.action == 'both' || options.cleaner.action == 'paste') {
                      e.preventDefault();
                      var ua   = window.navigator.userAgent;
                      var msie = ua.indexOf("MSIE ");
                          msie = msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./);
                      var ffox = navigator.userAgent.toLowerCase().indexOf('firefox') > -1;
                      if (msie)
                      {
                        var text = window.clipboardData.getData("Text");
                      }
                      else if (ffox)
                      {
                        var text =  e.originalEvent.clipboardData.getData('text/plain');
                      }
                      else
                      {
                        var text = e.originalEvent.clipboardData.getData(options.cleaner.keepHtml ? 'text/html' : 'text/plain');
                        text = cleanHTML(text);
                      }
                      var text = cleanText(text,options.cleaner.newline);
                      if (text) {
                        if (msie || ffox)
                            $note.summernote('pasteHTML', text);
                        //   setTimeout(function () {
                        //     $note.summernote('pasteHTML', text);
                        //   }, 1);
                        else
                          $note.summernote('pasteHTML', text);
                        // if ($editor.find('.note-status-output').length > 0)
                        //   $editor.find('.note-status-output').html('<div class="summernote-cleanerAlert alert alert-success">' + lang.cleaner.not + '</div>');
                      }
                    }
                  }
            }
        }
    });
}));
