// Copyright 2010-2011, Comsquared Systems, Inc.

// This document contains data and information proprietary to
// Comsquared Systems, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Comsquared Systems, Inc., without the express
// written consent of an officer of the corporation.

var isLoaded = false;
var isDirty = false;
var pageLoading = true;
var metadataWidth = 0;
var metadataHeight = 0;


function pageLoad(sender, eventArgs)
{
    pageLoading = false;
    isLoaded = true;
}

/// The document view has been resized.  Update its dimension
/// stored in the user configuration in the database.
function OnClientResize_DocView(sender, eventArgs)
{    
    var rcp = $find('ResizableControlBehavior1');
    var rxptd3 = document.getElementById('m_Column3');
    var main = document.getElementById('maincontainer');
    if (isLoaded)
    {
        var width = rcp.get_Size().width;
        var height = rcp.get_Size().height;
        var rcpTop = main.offsetTop;
        var rcpLeft = rxptd3.offsetLeft;
        var winwidth = 0;
        var winheight = 0;
        if (self.innerWidth) {
            winwidth = self.innerWidth;
            winheight = self.innerHeight;
        } else {
            winwidth = document.body.clientWidth;
            winheight = document.body.clientHeight;
        }
        var resizewidth = parseInt(rcpLeft) + parseInt(width)  + 200;
        var resizeheight = parseInt(height) + parseInt(rcpTop) + 200;
        var movebool = false;
        if (resizewidth > winwidth) {
            //resizewidth = parent.screen.width;
            movebool = true;
        }
        if (resizeheight > winheight) {
            //resizeheight = parent.screen.height;
            movebool = true;
        }
        if (movebool) {
            parent.moveTo(0, 0);
            parent.resizeTo(resizewidth, resizeheight);
        }             
        PageMethods.SetDocDimensions(width, height);
    }
}

function findpos(obj) {
    var curleft = curtop = 0;
    do {
        curleft += obj.offsetLeft;
        curtop += obj.offsetTop;
    } while (obj = obj.offsetParent);
    return [curleft, curtop];
}

/// Provide client-side min-max validation of a date-time
/// value using a regular expression.
function MinMaxValidateDateTime(sender, eventArgs)
{
    // To avoid a plethora of validation error message, don't do min max validation
    // if the target string does not parse as a DateTime.  Another validator handles
    // that circumstance.
        
    var dateTimeRegExpValidator = /^((((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9]))))[\-\/\s]?\d{2}(([02468][048])|([13579][26])))|(((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))[\-\/\s]?\d{2}(([02468][1235679])|([13579][01345789]))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$/;
    if (eventArgs.Value.match(dateTimeRegExpValidator))
    {
        var prospectiveDate = Date.parse(eventArgs.Value);
        var minDTStr = sender.attributes["MinValue"].value;
        var maxDTStr = sender.attributes["MaxValue"].value;    
        var minDT = Date.parse(minDTStr);
        var maxDT = Date.parse(maxDTStr);
        eventArgs.IsValid = (minDT <= prospectiveDate) && (maxDT >= prospectiveDate);
    }
}

// This method validates an XML string when the browser is
// Internet Explorer 5 or higher.  If the browser does not meet
// this specification, the target string it always declared to
// be valid.
function ValidateXML(sender, eventArgs)
{
    try
    {
        var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async="false";
        xmlDoc.loadXML(eventArgs.Value);
        
        eventArgs.IsValid = (xmlDoc.parseError.errorCode == 0);
    }
    catch(ex)
    {
        // Don't validate if not supported.
        
        eventArgs.IsValid = true;
    }
}

/// Set the dirty metadata flag if the page is
/// not loading.
function SetDirty()
{
    if (!pageLoading)
    {
        isDirty = true;
    }
}

/// Clear the dirty metadata flag.
function ClearDirty()
{
    isDirty = false;
}
var ppp;
function popupshow(obj1) {

    var nm = obj1;
    var rcp = $find(nm);
    
    var ids = rcp._popupControlID;
    var popupbox = document.getElementById(ids);
    var popuptextbox = popupbox.getElementsByTagName("TEXTAREA");
    var p2 = popuptextbox[0];
    var p2id = p2.id;
    ppp = document.getElementById(p2id);
    setTimeout("ppp.focus()", 500);
}

/// Prompt the user for confirmation if an attempt
/// is made to navigate away from dirty metadata.
function ValidateNavigation(sender, eventArgs)
{
    if (isDirty)
    {
        eventArgs.IsValid = confirm("Unsaved changes to index values will be lost if you move away from the current document. Do you wish to continue?");
        
        if (eventArgs.IsValid)
        {
            ClearDirty();
        }
    }
    else
    {
        eventArgs.IsValid = true;
    }
}

/// Set the page loading flag.
function PageLoadingHandler(sender, args)
{
    pageLoading = true;
}

    
     function ResizeGridView(sender, eventargs)
     {
         var element = sender.get_element();
         //var element = sender.control.element;
         //element.children[2].style.height = element.style.height; mjp 06/24/10
         //element.children[2].style.width = element.style.width; - this line not needed as the control has width set to 100%
         
        var rcp = $find('ResizableControlBehaviorMetadata');

        if (isLoaded)
        {
            element.children[2].style.height = element.style.height;            
            var width = rcp.get_Size().width;
            var height = rcp.get_Size().height;
            if((width != metadataWidth) || (height != metadataHeight))
            {
                metadataWidth = width;
                metadataHeight = height;
                PageMethods.SetPanelDimensions(width, height);
                if((BrowserDetect.browser == "Explorer") || (BrowserDetect.browser == "Chrome"))
                {
                    //window.location.reload(true); mjp 07/07/10
                    setTimeout("window.location.reload(true)", 1000);
                }
                else if(BrowserDetect.browser == "Firefox")
                {
                    setTimeout("window.location.reload(true)", 2000);
                }
                else
                {
                    setTimeout("window.location.reload(true)", 4000);
                }
            }
        }
        else
        {
            metadataWidth = rcp.get_Size().width;
            metadataHeight = rcp.get_Size().height;
        }
         
        return false;
     }
     
    var BrowserDetect = {
	    init: function () {
		    this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
		    this.version = this.searchVersion(navigator.userAgent)
			    || this.searchVersion(navigator.appVersion)
			    || "an unknown version";
		    this.OS = this.searchString(this.dataOS) || "an unknown OS";
	    },
	    searchString: function (data) {
		    for (var i=0;i<data.length;i++)	{
			    var dataString = data[i].string;
			    var dataProp = data[i].prop;
			    this.versionSearchString = data[i].versionSearch || data[i].identity;
			    if (dataString) {
				    if (dataString.indexOf(data[i].subString) != -1)
					    return data[i].identity;
			    }
			    else if (dataProp)
				    return data[i].identity;
		    }
	    },
	    searchVersion: function (dataString) {
		    var index = dataString.indexOf(this.versionSearchString);
		    if (index == -1) return;
		    return parseFloat(dataString.substring(index+this.versionSearchString.length+1));
	    },
	    dataBrowser: [
		    {
			    string: navigator.userAgent,
			    subString: "Chrome",
			    identity: "Chrome"
		    },
		    { 	string: navigator.userAgent,
			    subString: "OmniWeb",
			    versionSearch: "OmniWeb/",
			    identity: "OmniWeb"
		    },
		    {
			    string: navigator.vendor,
			    subString: "Apple",
			    identity: "Safari",
			    versionSearch: "Version"
		    },
		    {
			    prop: window.opera,
			    identity: "Opera"
		    },
		    {
			    string: navigator.vendor,
			    subString: "iCab",
			    identity: "iCab"
		    },
		    {
			    string: navigator.vendor,
			    subString: "KDE",
			    identity: "Konqueror"
		    },
		    {
			    string: navigator.userAgent,
			    subString: "Firefox",
			    identity: "Firefox"
		    },
		    {
			    string: navigator.vendor,
			    subString: "Camino",
			    identity: "Camino"
		    },
		    {		// for newer Netscapes (6+)
			    string: navigator.userAgent,
			    subString: "Netscape",
			    identity: "Netscape"
		    },
		    {
			    string: navigator.userAgent,
			    subString: "MSIE",
			    identity: "Explorer",
			    versionSearch: "MSIE"
		    },
		    {
			    string: navigator.userAgent,
			    subString: "Gecko",
			    identity: "Mozilla",
			    versionSearch: "rv"
		    },
		    { 		// for older Netscapes (4-)
			    string: navigator.userAgent,
			    subString: "Mozilla",
			    identity: "Netscape",
			    versionSearch: "Mozilla"
		    }
	    ],
	    dataOS : [
		    {
			    string: navigator.platform,
			    subString: "Win",
			    identity: "Windows"
		    },
		    {
			    string: navigator.platform,
			    subString: "Mac",
			    identity: "Mac"
		    },
		    {
			       string: navigator.userAgent,
			       subString: "iPhone",
			       identity: "iPhone/iPod"
	        },
		    {
			    string: navigator.platform,
			    subString: "Linux",
			    identity: "Linux"
		    }
	    ]

    };
    BrowserDetect.init();
