//Copyright 2016-2017, Milner Technologies, Inc.
//
//This document contains data and information proprietary to
//Milner Technologies, Inc.  This data shall not be disclosed,
//disseminated, reproduced or otherwise used outside of the
//facilities of Milner Technologies, Inc., without the express
//written consent of an officer of the corporation.

using System;
using System.Collections.Generic;
using System.Linq;
using ServiceLibrary;
using ApiControllers.Common;
using System.Web.Http.ExceptionHandling;
using System.Web.Http;
using System.Net.Http;
using System.Net;

/// <summary>
/// MVC Base controller class
/// </summary>
public class ApiBaseController : ApiController
{

    protected void OnHttpResponseMessage(string message)
    {
        var resp = new HttpResponseMessage(HttpStatusCode.NotFound);
        resp.Content = new StringContent(message);
        resp.ReasonPhrase = message;
        throw new HttpResponseException(resp);
    }
}

