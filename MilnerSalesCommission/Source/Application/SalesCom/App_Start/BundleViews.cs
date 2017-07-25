// Copyright 2016, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//
using System.Text;
using System.Web.Optimization;

namespace SalesCommission
{
    /// <summary>
    /// Transform for responding to angularjs view bundle requests.
    /// </summary>
    public sealed class PartialsTransform : IBundleTransform
    {
        /// <summary>
        /// The angularjs module name into which views are injected.
        /// </summary>
        private readonly string m_ModuleName;

        public PartialsTransform(string moduleName)
        {
            m_ModuleName = moduleName;
        }

        /// <summary>
        /// Perform run-time bundling of angularjs HTML views.  This is invoked when the client requests the bundle.
        /// </summary>
        /// <param name="context">Not used.</param>
        /// <param name="response">The bundle content object handle.</param>
        public void Process(BundleContext context, BundleResponse response)
        {
            var strBundleResponse = new StringBuilder();
            // Javascript module for Angular that uses templateCache 
            strBundleResponse.AppendFormat(
                @"angular.module('{0}').run(['$templateCache',function(t){{",
                m_ModuleName);

            try
            {
                foreach (var file in response.Files)
                {
                    try
                    {
                        // Get the partial page, remove line feeds and escape quotes
                        var content = file.ApplyTransforms()
                            .Replace("\r\n", "").Replace("'", "\\'");

                        var templateUrl = file.IncludedVirtualPath.Replace("~/", "");

                        // Create insert statement with template

                        strBundleResponse.AppendFormat("t.put('{0}','{1}');", templateUrl, content);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }

            strBundleResponse.Append(@"}]);");

            response.Files = new BundleFile[] { };
            response.Content = strBundleResponse.ToString();
            response.ContentType = "application/javascript";
        }
    }

    /// <summary>
    /// Angularjs HTML view bundler.
    /// </summary>
    public sealed class PartialsBundle : Bundle
    {
        /// <summary>
        /// Create a new angularjs view bundle.
        /// </summary>
        /// <param name="moduleName">The name of the angularjs module into which the views are injected.</param>
        /// <param name="virtualPath">The virtual directory name to assign this bundle.  The directory
        /// MUST NOT EXIST or IIS will return a 500 error!</param>
        public PartialsBundle(string moduleName, string virtualPath)
            : base(virtualPath, new[] { new PartialsTransform(moduleName) })
        {
        }
    }
}