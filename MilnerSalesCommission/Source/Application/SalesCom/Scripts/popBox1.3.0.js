
/*
* jQuery popBox
* Copyright (c) 2011 Simon Hibbard
* 
* Permission is hereby granted, free of charge, to any person
* obtaining a copy of this software and associated documentation
* files (the "Software"), to deal in the Software without
* restriction, including without limitation the rights to use,
* copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following
* conditions:

* The above copyright notice and this permission notice shall be
* included in all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
* OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
* NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
* HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
* OTHER DEALINGS IN THE SOFTWARE. 
*/

/*
* Version: V1.3.0
* Release: 26-01-2011
* Based on jQuery 1.5.0
* Additional features provided with thanks to Alex Lareau
*/

(function ($) {
    var objMain;
    var objValue;

    $.fn.popBox = function (options) {
        var mode = options;
        if (mode == "AddItems") {
            var defaults = {
                height: 140,
                width: 160,
                newlineString: "<br/>"
            };
        } else if (mode == "DocView") {
            var defaults = {
                height: 140,
                width: 180,
                newlineString: "<br/>"
            };
        }

        var options = $.extend(defaults, options);
        return this.each(function () {

            obj = $(this);
            var inputName = 'popBoxInput' + obj.attr("Id");

            var labelValue = $("label[for=" + obj.attr('id') + "]").text();
            objValue = labelValue;
            var addItemDiv;
            
            //obj.after('<div class="popBox-holder" ></div><div class="popBox-container" id="popupMain"><label style="display: none;" for="' + inputName + '">' + labelValue + '</label><textarea id="' + inputName + '" name="' + inputName + '" class="popBox-input" /><div class="done-button"><input type="button" id="save" class="submit-popBox save"/><input type="button" class="refresh Reload"/></div></div>');
            if (mode == "AddItems") {

                
                addItemDiv = "<div></div>";
                addItemDiv = addItemDiv + "<div class='popBox-container' id='popupMain'>"
                addItemDiv = addItemDiv + "<label style='display: none;' for='" + inputName + "'>'" + labelValue + "'</label>"
                addItemDiv = addItemDiv + "<textarea id='" + inputName + "' name='" + inputName + "' class='popBox-input' style='width:160px !important;' />"
                addItemDiv = addItemDiv + "<div class='button' style='margin-top:4px;'>"
                addItemDiv = addItemDiv + "<ul class='import-page' style=' list-style:none;'>"
                addItemDiv = addItemDiv + "<li style='float:left; width:47px; height:21px;'>"
                addItemDiv = addItemDiv + "<input type='button' id='save' class='submit-popbox save'/>"
                addItemDiv = addItemDiv + "</li>"
                addItemDiv = addItemDiv + "<li style='float:left; width:49px; height:21px;margin-left:3px;'>"
                addItemDiv = addItemDiv + "<input type='button' class='refresh-popbox Reload'/>"
                addItemDiv = addItemDiv + "</li>"
                addItemDiv = addItemDiv + "<li style='margin-left:3px;margin-left:6px\9;float:left; width:44px; height:21px;'>"
                addItemDiv = addItemDiv + "<input type='button' class='close-popbox Cancel'/>"
                addItemDiv = addItemDiv + "</li>"
                addItemDiv = addItemDiv + "</ul>"
                addItemDiv = addItemDiv + "</div>"
                addItemDiv = addItemDiv + "</div>"
                
                obj.after(addItemDiv);

            } else if (mode == "DocView") {
                addItemDiv = "<div class='popBox-container' id='popupMain' style='width:auto;'>"
                addItemDiv = addItemDiv + "<label style='display: none;' for='" + inputName + "'>'" + labelValue + "'</label>"
                addItemDiv = addItemDiv + "<textarea id='" + inputName + "' name='" + inputName + "' class='popBox-input' style='width:177px !important;' />"
                addItemDiv = addItemDiv + "<div class='button' style='margin-top:4px;'>"
                addItemDiv = addItemDiv + "<ul class='import-page' style=' list-style:none;'>"
                addItemDiv = addItemDiv + "<li style='float:left; width:47px; height:21px;'>"
                addItemDiv = addItemDiv + "<input type='button' id='save' class='submit-popbox save'/>"
                addItemDiv = addItemDiv + "</li>"
                addItemDiv = addItemDiv + "<li style='float:left; width:49px; height:21px;margin-left:3px;'>"
                addItemDiv = addItemDiv + "<input type='button' class='refresh-popbox Reload'/>"
                addItemDiv = addItemDiv + "</li>"
                addItemDiv = addItemDiv + "<li style='margin-left:3px;margin-left:6px\9;float:left; width:44px; height:21px;'>"
                addItemDiv = addItemDiv + "<input type='button' class='close-popbox Cancel'/>"
                addItemDiv = addItemDiv + "</li>"
                addItemDiv = addItemDiv + "</ul>"
                addItemDiv = addItemDiv + "</div>"
                addItemDiv = addItemDiv + "</div>"

                obj.after(addItemDiv);
            }

           // obj.after('<div class="popBox-holder" ></div><div class="popBox-container" id="popupMain"><label style="display: none;" for="' + inputName + '">' + labelValue + '</label><textarea id="' + inputName + '" name="' + inputName + '" class="popBox-input" /><div class="done-button"><input type="button" id="save" class="submit-popBox save"/><input type="button" class="refresh Reload"/></div></div>');

            obj.focus(function () {
                $(".popBox-container").hide();
                $(this).next(".popBox-holder").show();

                if (mode == "DocView") {
                    $(this).next().next(".popBox-container").css({
                        width: 205
                    });
                }

                var popBoxContainer = $(this).next().next(".popBox-container");
                var change = true;
                popBoxContainer.children('.popBox-input').css({
                    height: options.height,
                    width: options.width
                });
                popBoxContainer.show();

                var winH = $(window).height();
                var winW = $(window).width();
                var objH = popBoxContainer.height();
                var objW = popBoxContainer.width();
                var left = (winW / 2) - (objW / 2);
                var top = (winH / 2) - (objH / 2);
                popBoxContainer.css({
                    position: 'absolute',
                    margin: 0
                    //top: 100,
                    //left:100
                    //top: (top > 0 ? top : 0) + 'px',
                    //left: (left > 0 ? left : 0) + 'px'
                });
                popBoxContainer.children('.popBox-input').val($(this).val().replace(RegExp(options.newlineString, "g"), "\n"));
                popBoxContainer.children('.popBox-input').focus();
                popBoxContainer.children().keydown(function (e) {

                    if (e == null) {
                        keycode = event.keyCode
                    } else {
                        keycode = e.which
                    } if (keycode == 27) {
                        $(this).parent().hide();
                        $(this).parent().prev().hide();
                        change = false
                    }
                });

                popBoxContainer.children().children().children().children('input').click(function (msg) {
                    if ($(this).hasClass('Reload')) {

                        var prevValue = $(this).parent().parent().parent().parent().prev().prev().val();
                        $(this).parent().parent().parent().prev().val(prevValue.replace(/\n/g, options.newlineString));
                    }
                    if ($(this).hasClass('save')) {                        
                        var submitValue = $(this).parent().parent().parent().prev().val();
                        var inputField = $(this).parent().parent().parent().parent().prev().prev();
                        inputField.val(submitValue.replace(/\n/g, options.newlineString))
                        $(this).parent().parent().parent().parent().hide();
                        $(this).parent().parent().parent().parent().prev().hide();                        
                        focusNexElement(inputField);
                    }
                    if ($(this).hasClass('Cancel')) {
                        $(this).parent().parent().parent().parent().hide();
                        $(this).parent().parent().parent().parent().prev().hide();
                        var inputField = $(this).parent().parent().parent().parent().prev().prev();
                        focusNexElement(inputField);
                    }
                });
            })
            function focusNexElement(inputField)
            {
                var inputFieldLabel = inputField.parent("td").prev().children(".Name");
                var inputFieldIndex = $("#dtDocumentInfo .Name").index(inputFieldLabel);
                var totalVisibleInputs = $("#dtDocumentInfo .Name").length;
                if (totalVisibleInputs - 1 == inputFieldIndex) {
                    if ($("#txtViewNotes").length > 0) {
                        document.getElementById("txtViewNotes").focus();
                    }
                }
                else {
                    $("#dtDocumentInfo .Name:eq(" + inputFieldIndex + 1 + ")").trigger("focus");
                }
            }
        })
    }
})(jQuery);

function hidepopup() {
    $(".popBox-container").hide();
}
