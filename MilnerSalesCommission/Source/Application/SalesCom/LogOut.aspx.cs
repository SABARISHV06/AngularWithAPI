// Copyright 2010-2016, Milner Technologies, Inc.

// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.

using System;
using System.Web;
using System.Web.Security;

public partial class LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();  // Causes Session_End event to fire in Global.asax.cs
    }
}
