/*
 *  jquery-maskmoney - v3.0.2
 *  jQuery plugin to mask data entry in the input text in the form of money (currency)
 *  https://github.com/plentz/jquery-maskmoney
 *
 *  Made by Diego Plentz
 *  Under MIT License (https://raw.github.com/plentz/jquery-maskmoney/master/LICENSE)
 */
(function ($) {
    "use strict";
    if (!$.browser) {
        $.browser = {};
        $.browser.mozilla = /mozilla/.test(navigator.userAgent.toLowerCase()) && !/webkit/.test(navigator.userAgent.toLowerCase());
        $.browser.webkit = /webkit/.test(navigator.userAgent.toLowerCase());
        $.browser.opera = /opera/.test(navigator.userAgent.toLowerCase());
        $.browser.msie = /msie/.test(navigator.userAgent.toLowerCase());
    }
    var methods = {
        destroy: function () {
            $(this).unbind(".maskMoney");

            if ($.browser.msie) {
                this.onpaste = null;
            }
            return this;
        },

        mask: function (value) {

            return this.each(function () {
                var $this = $(this),
                    decimalSize;
                if (typeof value === "number") {
                    $this.trigger("mask");
                    decimalSize = $($this.val().split(/\D/)).last()[0].length;
                    value = value.toFixed(decimalSize);
                    $this.val(value);
                }
                return $this.trigger("mask");
            });
        },

        unmasked: function () {
            return this.map(function () {
                var value = ($(this).val() || "0"),
                    isNegative = value.indexOf("-") !== -1,
                    decimalPart;

                // get the last position of the array that is a number(coercion makes "" to be evaluated as false)
                $(value.split(/\D/).reverse()).each(function (index, element) {
                    if (element) {
                        decimalPart = element;
                        return false;
                    }
                });
                value = value.replace(/\D/g, "");
                value = value.replace(new RegExp(decimalPart + "$"), "." + decimalPart);
                if (isNegative) {
                    value = "-" + value;
                }
                return parseFloat(value);
            });
        },

        init: function (settings) {
            settings = $.extend({
                prefix: "",
                suffix: "",
                affixesStay: true,
                length: "",
                customMask: "",
                precision: 2,
                thousands: ",",
                decimal: ".",
                allowZero: false,
                allowNegative: false
            }, settings);


            return this.each(function () {
                var $input = $(this),
                    onFocusValue;

                // data-* api
                settings = $.extend(settings, $input.data());

                function getInputSelection() {
                    var el = $input.get(0),
                        start = 0,
                        end = 0,
                        normalizedValue,
                        range,
                        textInputRange,
                        len,
                        endRange;

                    if (typeof el.selectionStart === "number" && typeof el.selectionEnd === "number") {
                        start = el.selectionStart;
                        end = el.selectionEnd;
                    } else {
                        range = document.selection.createRange();

                        if (range && range.parentElement() === el) {
                            len = el.value.length;
                            normalizedValue = el.value.replace(/\r\n/g, "\n");

                            // Create a working TextRange that lives only in the input
                            textInputRange = el.createTextRange();
                            textInputRange.moveToBookmark(range.getBookmark());

                            // Check if the start and end of the selection are at the very end
                            // of the input, since moveStart/moveEnd doesn't return what we want
                            // in those cases
                            endRange = el.createTextRange();
                            endRange.collapse(false);

                            if (textInputRange.compareEndPoints("StartToEnd", endRange) > -1) {
                                start = end = len;
                            } else {
                                start = -textInputRange.moveStart("character", -len);
                                start += normalizedValue.slice(0, start).split("\n").length - 1;

                                if (textInputRange.compareEndPoints("EndToEnd", endRange) > -1) {
                                    end = len;
                                } else {
                                    end = -textInputRange.moveEnd("character", -len);
                                    end += normalizedValue.slice(0, end).split("\n").length - 1;
                                }
                            }
                        }
                    }

                    return {
                        start: start,
                        end: end
                    };
                } // getInputSelection

                function canInputMoreNumbers() {
                    var haventReachedMaxLength = !($input.val().length >= $input.attr("maxlength") && $input.attr("maxlength") >= 0),
                        selection = getInputSelection(),
                        start = selection.start,
                        end = selection.end,
                        haveNumberSelected = (selection.start !== selection.end && $input.val().substring(start, end).match(/\d/)) ? true : false,
                        startWithZero = ($input.val().substring(0, 1) === "0");
                    return haventReachedMaxLength || haveNumberSelected || startWithZero;
                }

                function setCursorPosition(pos) {
                    $input.each(function (index, elem) {
                        if (elem.setSelectionRange) {
                            elem.focus();
                            elem.setSelectionRange(pos, pos);
                        } else if (elem.createTextRange) {
                            var range = elem.createTextRange();
                            range.collapse(true);
                            range.moveEnd("character", pos);
                            range.moveStart("character", pos);
                            range.select();
                        }
                    });
                }

                function setSymbol(value) {
                    var operator = "";
                    if (value.indexOf("-") > -1) {
                        value = value.replace("-", "");
                        operator = "-";

                    }
                    var temp = operator + settings.prefix + value + settings.suffix;
                    temp = trimSpaces(temp);
                    return temp;

                }

                function trimSpaces(temp) {
                    return temp.replace(/ /g, '');
                }

                function maskValue(value) {
                    var negative = (value.indexOf("-") > -1 && settings.allowNegative) ? "-" : "",
                        onlyNumbers = value.replace(/[^0-9]/g, ""),
                        integerPart = onlyNumbers.slice(0, onlyNumbers.length - settings.precision),
                        newValue,
                        decimalPart,
                        leadingZeros;

                    integerPart = setIntegerPart(integerPart, value);
                    newValue = negative + settings.prefix + integerPart;

                    if (settings.precision > 0) {
                        decimalPart = onlyNumbers.slice(onlyNumbers.length - settings.precision);
                        leadingZeros = new Array((settings.precision + 1) - decimalPart.length).join("_");
                        newValue += settings.decimal + leadingZeros + decimalPart;
                    }

                    return newValue;
                }

                function setIntegerPart(integerPart, value) {

                    //put settings.thousands every 3 chars
                    var integerPart = integerPart.replace(/\B(?=(\d{3})+(?!\d))/g, settings.thousands);
                    var intPart = createCustomCurrencyFormat();
                    if (intPart.indexOf("-") == 0) {
                        intPart = intPart.replace(intPart[intPart.indexOf("-")], "");
                    }
                    if (integerPart != "") {
                        intPart = intPart.substring(0, (intPart.length) - integerPart.length)
                        intPart = intPart + integerPart;
                    }
                    return intPart;
                }

                function createCustomCurrencyFormat() {
                    var currFormat = settings.customMask.replace(/\$/g, "");
                    currFormat = currFormat.replace(/9/g, "_");
                    if (currFormat.indexOf('.') >= 0) {
                        currFormat = currFormat.substring(0, currFormat.indexOf('.'));
                    }
                    return currFormat;
                }

                function maskAndPosition(startPos) {

                    var originalLen = $input.val().length,
                        newLen;
                    $input.val(maskValue($input.val()));
                    newLen = $input.val().length;
                    startPos = startPos - (originalLen - newLen);
                    setCursorPosition(startPos);
                }

                function mask() {
                    var value = $input.val();
                    $input.val(maskValue(value));
                }

                function changeSign() {
                    var inputValue = $input.val();
                    if (settings.allowNegative) {
                        if (inputValue !== "" && inputValue.charAt(0) === "-") {
                            return inputValue.replace("-", "");
                        } else {
                            return "-" + inputValue;
                        }
                    } else {
                        return inputValue;
                    }
                }

                function preventDefault(e) {
                    if (e.preventDefault) { //standard browsers
                        e.preventDefault();
                    } else { // old internet explorer
                        e.returnValue = false;
                    }
                }

                //function keypressEvent(e) {

                //    if ($input.val().indexOf("_") < 0)
                //    {
                //        return false;
                //    }

                //    e = e || window.event;
                //    var key = e.which || e.charCode || e.keyCode,
                //        keyPressedChar,
                //        selection,
                //        startPos,
                //        endPos,
                //        value;
                //    //added to handle an IE "special" event
                //    if (key === undefined) {
                //        return false;
                //    }

                //    // any key except the numbers 0-9
                //    if (key < 48 || key > 57) {
                //        // -(minus) key
                //        if (key === 45) {
                //            $input.val(changeSign());
                //            return false;
                //            // +(plus) key
                //        } else if (key === 43) {
                //            $input.val($input.val().replace("-", ""));
                //            return false;
                //            // enter key or tab key
                //        } else if (key === 13 || key === 9) {
                //            return true;
                //        } else if ($.browser.mozilla && (key === 37 || key === 39) && e.charCode === 0) {
                //            // needed for left arrow key or right arrow key with firefox
                //            // the charCode part is to avoid allowing "%"(e.charCode 0, e.keyCode 37)
                //            return true;
                //        } else { // any other key with keycode less than 48 and greater than 57
                //            preventDefault(e);
                //            return true;
                //        }
                //    } else if (!canInputMoreNumbers()) {
                //        return false;
                //    } else {
                //        preventDefault(e);

                //        keyPressedChar = String.fromCharCode(key);
                //        selection = getInputSelection();
                //        startPos = selection.start;
                //        endPos = selection.end;
                //        value = $input.val();
                //        var temp = value.substring(0, startPos) + keyPressedChar + value.substring(endPos, value.length);
                //        //$input.val(value.substring(0, startPos) + keyPressedChar + value.substring(endPos, value.length));
                //        $input.val(temp);
                //        maskAndPosition(startPos + 1);
                //        return false;
                //    }
                //}

                function keypressEvent(e) {

                    if ($input.val().indexOf("_") < 0) {
                        return false;
                    }

                    e = e || window.event;
                    var key = e.which || e.charCode || e.keyCode,
                        keyPressedChar,
                        selection,
                        startPos,
                        endPos,
                        value;
                    //added to handle an IE "special" event
                    if (key === undefined) {
                        return false;
                    }

                    // any key except the numbers 0-9
                    if (key < 48 || key > 57) {
                        // -(minus) key
                        if (key === 45) {
                            $input.val(changeSign());
                            return false;
                            // +(plus) key
                        } else if (key === 43) {
                            $input.val($input.val().replace("-", ""));
                            return false;
                            // enter key or tab key
                        } else if (key === 13 || key === 9) {
                            return true;
                        } else if ($.browser.mozilla && (key === 37 || key === 39) && e.charCode === 0) {
                            // needed for left arrow key or right arrow key with firefox
                            // the charCode part is to avoid allowing "%"(e.charCode 0, e.keyCode 37)
                            return true;
                        } else { // any other key with keycode less than 48 and greater than 57
                            preventDefault(e);
                            return true;
                        }
                    } else if (!canInputMoreNumbers()) {
                        return false;
                    } else {
                        preventDefault(e);

                        keyPressedChar = String.fromCharCode(key);
                        selection = getInputSelection();
                        startPos = selection.start;
                        endPos = selection.end;
                        value = $input.val();

                        var temp = applymask(settings.customMask, value, keyPressedChar);
                        $input.val(temp);
                        return false;
                    }
                }


                function keydownEvent(e) {
                    e = e || window.event;
                    var key = e.which || e.charCode || e.keyCode,
                        selection,
                        startPos,
                        endPos,
                        value,
                        lastNumber;
                    //needed to handle an IE "special" event
                    if (key === undefined) {
                        return false;
                    }

                    selection = getInputSelection();
                    startPos = selection.start;
                    endPos = selection.end;

                    if (key === 8 || key === 46 || key === 63272) { // backspace or delete key (with special case for safari)
                        preventDefault(e);

                        value = $input.val();
                        // not a selection
                        if (startPos === endPos) {
                            // backspace
                            if (key === 8) {

                                if (settings.suffix === "") {
                                    startPos -= 1;
                                } else {
                                    // needed to find the position of the last number to be erased
                                    lastNumber = value.split("").reverse().join("").search(/\d/);
                                    startPos = value.length - lastNumber - 1;
                                    endPos = startPos + 1;
                                }
                                //delete
                            } else {
                                endPos += 1;
                            }
                        }

                        $input.val(value.substring(0, startPos) + value.substring(endPos, value.length));
                        value = $input.val();
                        var temp = applymask(settings.customMask, value, "");
                        $input.val(temp);

                        return false;
                    } else if (key === 9) { // tab key
                        return true;
                    } else { // any other key
                        return true;
                    }
                }

                function focusEvent() {
                    onFocusValue = $input.val();

                    mask();
                    var input = $input.get(0),
                        textRange;
                    if (input.createTextRange) {
                        textRange = input.createTextRange();
                        textRange.collapse(false); // set the cursor at the end of the input
                        textRange.select();
                    }

                    if (onFocusValue != "") {
                        setTimeout(function () {
                            $input.val(onFocusValue);
                        }, 0);
                    }
                }

                function cutPasteEvent() {
                    setTimeout(function () {
                        mask();
                    }, 0);
                }

                function getDefaultMask() {
                    var n = createCustomCurrencyFormat();
                    var onlyNumbers = $input.val().replace(/[0-9]/g, "_");
                    if (settings.precision > 0) {
                        var decimalPart = onlyNumbers.slice(onlyNumbers.length - settings.precision);
                        var leadingZeros = new Array((settings.precision + 1) - decimalPart.length).join("_");
                        n += settings.decimal + leadingZeros + decimalPart;
                    }

                    return n;
                }

                function blurEvent(e) {

                    if ($.browser.msie) {
                        keypressEvent(e);
                    }

                    if ($input.val() === "" || $input.val() === setSymbol(getDefaultMask())) {
                        if (!settings.allowZero) {
                            $input.val("");
                        } else if (!settings.affixesStay) {
                            $input.val(getDefaultMask());
                        } else {
                            $input.val(setSymbol(getDefaultMask()));
                        }
                    } else {
                        if (!settings.affixesStay) {
                            var newValue = $input.val().replace(settings.prefix, "").replace(settings.suffix, "");
                            $input.val(newValue);
                        }
                    }
                    if ($input.val() !== onFocusValue) {
                        $input.change();
                    }
                }

                function clickEvent() {
                    var input = $input.get(0),
                        length;
                    if (input.setSelectionRange) {
                        length = $input.val().length;
                        input.setSelectionRange(length, length);
                    } else {
                        $input.val($input.val());
                    }
                }

                $input.unbind(".maskMoney");
                $input.bind("keypress.maskMoney", keypressEvent);
                $input.bind("keydown.maskMoney", keydownEvent);
                $input.bind("blur.maskMoney", blurEvent);
                $input.bind("focus.maskMoney", focusEvent);
                $input.bind("click.maskMoney", clickEvent);
                $input.bind("cut.maskMoney", cutPasteEvent);
                $input.bind("paste.maskMoney", cutPasteEvent);
                $input.bind("mask.maskMoney", mask);

            });
        }
    };

    $.fn.maskMoney = function (method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === "object" || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error("Method " + method + " does not exist on jQuery.maskMoney");
        }
    };
})(window.jQuery || window.Zepto);


function applymask(customMask, maskedvalue, value) {

    customMask = customMask.replace(/9/g, "_");
    var delimiter = finddelimiter(customMask);

    var outputVal = "";
    var replaced = false;

    if (value != "") {
        maskedvalue = RelocationCharacter(maskedvalue, delimiter, customMask);

        for (var i = customMask.length - 1; i >= 0; i--) {
            var subStringChar = maskedvalue[i];
            if (!replaced) {
                if (subStringChar == "_") {
                    outputVal = value + outputVal;
                    replaced = true;
                }
                else {
                    outputVal = subStringChar + outputVal;
                }
            }
            else {
                outputVal = subStringChar + outputVal;
            }
        }
    }
    else {

        var actualChar = maskedvalue;

        for (var i = 0; i <= delimiter.length - 1; i++) {
            for (var j = 0; j <= maskedvalue.length - 1; j++) {
                actualChar = actualChar.replace(delimiter[i], "");
            }
        }
        actualChar = actualChar.replace(/_/g, "");

        maskedvalue = customMask;
        for (var i = 0; i <= actualChar.length - 1; i++) {
            value = actualChar[i];
            replaced = false;
            outputVal = ""
            for (var j = customMask.length - 1; j >= 0; j--) {
                var subStringChar = maskedvalue[j];
                if (!replaced) {
                    if (subStringChar == "_") {
                        outputVal = value + outputVal;
                        replaced = true;
                    }
                    else {
                        outputVal = subStringChar + outputVal;
                    }
                }
                else {
                    outputVal = subStringChar + outputVal;
                }
            }
            maskedvalue = outputVal;
            maskedvalue = RelocationCharacter(maskedvalue, delimiter, customMask);
        }
        if (outputVal == "") {
            outputVal = customMask;
        }
    }

    return outputVal;
}

function RelocationCharacter(value, delimiter, customMask) {
    var returnVal = "";
    var temp = "";
    for (var i = 0; i <= value.length - 1; i++) {
        var subStringChar = value[i];
        var result = IsLetterOrDigit(subStringChar);
        if (result == true) {
            temp += value[i];
        }
    }
    temp = temp.trim();
    var outputVal = "";

    var custMaskLength = customMask.length - 2;
    var tempOutput = customMask;
    if (temp != "") {
        var position = 0;
        var tempCount = 0;
        //Find the potions
        for (var i = custMaskLength; i >= 0; i--) {
            var subStringChar = customMask[i];
            if (subStringChar == "_" && tempCount < temp.length) {
                position = i;
                tempCount += 1;
            }

        }

        var existingChar = 0;

        for (var replChar = 0; replChar <= customMask.length - 1; replChar++) {
            var subStringChar = customMask[replChar];
            if (replChar >= position) {
                if (subStringChar == "_" && existingChar < temp.length) {
                    outputVal = outputVal + temp[existingChar];
                    existingChar += 1;
                }
                else {
                    outputVal = outputVal + subStringChar;
                }
            }
            else {
                outputVal = outputVal + subStringChar;
            }
        }

        returnVal = outputVal;
    }
    else {
        returnVal = value;
    }
    return returnVal;
}

function IsLetterOrDigit(str) {
    var letterNumber = new RegExp(/[a-zA-Z0-9]/);
    if (letterNumber.test(str)) {
        return true;
    }
    else {
        return false;
    }
}
function finddelimiter(customMask) {
    var delimiter = "";

    for (var i = 0; i < customMask.length; i++) {
        if (customMask[i] != "_") {
            delimiter = delimiter + customMask[i];
        }
    }
    return delimiter;
}