// Copyright 2010-2011, Comsquared Systems, Inc.

// This document contains data and information proprietary to
// Comsquared Systems, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Comsquared Systems, Inc., without the express
// written consent of an officer of the corporation.

var isLoaded = false;
var isDirty = false;
var pageLoading = true;


function pageLoad(sender, eventArgs)
{
    pageLoading = false;
    isLoaded = true;
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

/// The document view has been resized.  Update its dimension
/// stored in the user configuration in the database.
function OnClientResize_DocView(sender, eventArgs)
{    
    var rcp = $find('ResizableControlBehavior1');

    if (isLoaded)
    {
        var width = rcp.get_Size().width;
        var height = rcp.get_Size().height;
        PageMethods.SetDocDimensions(width, height);
    }
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
