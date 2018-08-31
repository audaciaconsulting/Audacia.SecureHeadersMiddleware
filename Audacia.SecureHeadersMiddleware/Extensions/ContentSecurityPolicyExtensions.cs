using System.Collections.Generic;

using Audacia.SecureHeadersMiddleware.Enums;
using Audacia.SecureHeadersMiddleware.Models;
using Audacia.SecureHeadersMiddleware.Models.ContentSecurityPolicy;

namespace Audacia.SecureHeadersMiddleware.Extensions
{
    public static class ContentSecurityPolicyExtensions
    {
        /// <summary>
        /// Used to add a single Content Security Policy URIs for a given <see cref="CspUriType"/>
        /// </summary>
        /// <param name="config"></param>
        /// <param name="cspRule"></param>
        /// <param name="cspUriType"></param>
        /// <returns></returns>
        public static SecureHeadersMiddlewareConfiguration AddCspRules
            (this SecureHeadersMiddlewareConfiguration config, DirectiveAndType cspRule, CspUriType cspUriType)
        {
            if (config.CspIsEnabled())
            {
                config.ContentSecurityPolicyConfiguration.AddCspRule(cspRule, cspUriType);
            }
            return config;
        }

        /// <summary>
        /// Used to set the Content Security Policy URIs for a given <see cref="CspUriType"/>
        /// </summary>
        public static SecureHeadersMiddlewareConfiguration SetCspRules
            (this SecureHeadersMiddlewareConfiguration config, List<DirectiveAndType> cspRule, CspUriType cspUriType)
        {
            if (config.CspIsEnabled())
            {
                config.ContentSecurityPolicyConfiguration?.SetCspRules(cspRule, cspUriType);
            }
            
            return config;
        }

        /// <summary>
        /// Used to set up the Content Security Policy Sandbox for a given <see cref="CspSandboxType"/>
        /// </summary>
        public static SecureHeadersMiddlewareConfiguration SetCspSandBox
            (this SecureHeadersMiddlewareConfiguration config, CspSandboxType sandboxType)
        {
            if (config.CspIsEnabled())
            {
                config.ContentSecurityPolicyConfiguration?.SetSandbox(sandboxType);
            }
            
            return config;
        }

        private static bool CspIsEnabled(this SecureHeadersMiddlewareConfiguration config)
        {
            return config.UseContentSecurityPolicy;
        }
    }
}