// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;


namespace ApiControllers.Handlers
{
    /// <summary>
    /// To handle api session actions and behaiours
    /// </summary>
    public sealed class ApiSessionHandler : DelegatingHandler
    {
        /// <summary>
        /// 
        /// </summary>
        static public string SessionIdToken = "MyApiSessionToken";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string sessionId = null;            

            IEnumerable<string> sessid = null;
            request.Headers.TryGetValues("MySession", out sessid);

            if (sessid != null)
            {
                try
                {
                    sessionId = sessid.ElementAt(0);
                }
                catch 
                { 
                }
            }

            if (string.IsNullOrWhiteSpace(sessionId))
            {
                var cookiecoll = request.Headers.GetCookies();

                var cookie = request.Headers.GetCookies(SessionIdToken).FirstOrDefault();
                if (cookie != null)
                {
                    sessionId = cookie[SessionIdToken].Value;
                }
            }

            // Store the session ID in the request property bag.
            request.Properties[SessionIdToken] = sessionId;

            // Continue processing the HTTP request.
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            // Set the session ID as a cookie in the response message.
            if (sessionId != null)
            {
                CookieHeaderValue chv = new CookieHeaderValue(SessionIdToken, sessionId);
                chv.Expires = DateTime.Now.Add(TimeSpan.FromHours(2));
                response.Headers.AddCookies(new CookieHeaderValue[] { chv });
            }

            return response;
        }
    }
}