using System;
using Microsoft.AspNetCore.Builder;

using Audacia.SecureHeadersMiddleware.Enums;
using Audacia.SecureHeadersMiddleware.Models;
using System.Collections.Generic;
using Audacia.SecureHeadersMiddleware.Models.ContentSecurityPolicy;

namespace Audacia.SecureHeadersMiddleware.Extensions
{
    /// <summary>
    /// Extension methods to allow us to easily build the middleware
    /// </summary>
    public static class SecureHeadersMiddlewareExtensions
    {
        /// <summary>
        /// Builds an instance of the <see cref="SecureHeadersMiddlewareConfiguration"/>
        /// with default values. The default values are supplied via the calls to `Use...`
        /// on each of the headers, please see the comments on those methods for the
        /// current default values that they supply.
        /// </summary>
        /// <remarks>
        /// This method sets up all of the headers which are recommended by OWASP, using
        /// default values that they recommend in their best practises. Please see the following
        /// url for the current best practises:
        /// https://www.owasp.org/index.php/OWASP_Secure_Headers_Project#tab=Best_Practices
        /// </remarks>
        public static SecureHeadersMiddlewareConfiguration BuildDefaultConfiguration()
        {
            return SecureHeadersMiddlewareBuilder
                .CreateBuilder()
                .UseHsts()
                .UseXFrameOptions()
                .UseXSSProtection()
                .UseContentTypeOptions()
                .UseContentDefaultSecurityPolicy()
                .UsePermittedCrossDomainPolicies()
                .UseReferrerPolicy()
                .Build();
        }

        public static SecureHeadersMiddlewareConfiguration BuildAngular2TemplateConfiguration(bool forceHttps)
        {
            return SecureHeadersMiddlewareBuilder
                .CreateBuilder()
                .UseHsts()
                .UseXFrameOptions()
                .UseXSSProtection()
                .UseContentTypeOptions()
                .UseContentSecurityPolicy(blockAllMixedContent: forceHttps, upgradeInsecureRequests: forceHttps)
                .UsePermittedCrossDomainPolicies()
                .UseReferrerPolicy()
                .Build();
        }

        /// <summary>
        /// Used to append the current CSP rules, adding a rule for a given domain (<see cref="DirectiveAndType.Uri"/>),
        /// directive (<see cref="DirectiveAndType.DirectiveType" />) and source type (<see cref="CspUriType")
        /// </summary>
        /// <param name="config"></param>
        /// <param name="url"></param>
        /// <param name="directive"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public static SecureHeadersMiddlewareConfiguration TryAppendCsp(this SecureHeadersMiddlewareConfiguration config,
                    string url, Models.ContentSecurityPolicy.DirectiveType directive,
                    CspUriType sourceType = CspUriType.DefaultUri)
        {
            CheckCspInUseAndVaild(config);

            config.AddCspRules(
                new Models.ContentSecurityPolicy.DirectiveAndType
                {
                    Uri = url,
                    DirectiveType = directive
                },
                sourceType);

            return config;
        }

        /// <summary>
        /// Used to set (overwrite) the CSP rules for a given CSP source (<see cref="CspUriType"/>).
        /// This will overwrite any pre-existing rules, if CSP is in use.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="CspRules"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public static SecureHeadersMiddlewareConfiguration TrySetCsp(this SecureHeadersMiddlewareConfiguration config,
                    List<DirectiveAndType> CspRules, CspUriType sourceType = CspUriType.DefaultUri)
        {
            CheckCspInUseAndVaild(config);

            config.SetCspRules(CspRules, sourceType);

            return config;
        }
        
        /// <summary>
        /// Extention method to include the <see cref="SecureHeadersMiddleware" /> in
        /// an instance of an <see cref="IApplicationBuilder" />.
        /// This works in the same way was the MVC, Static files, etc. middleware
        /// </summary>
        /// <param name="builder">The instance of the <see cref="IApplicationBuilder" /> to use</param>
        /// <param name="config">An instance of the <see cref="SecureHeadersMiddlewareConfiguration" /> containing all of the config for each request </param>
        /// <returns>The <see cref="IApplicationBuilder"/> with the <see cref="SecureHeadersMiddleware" /> added</returns>
        public static IApplicationBuilder UseSecureHeadersMiddleware(this IApplicationBuilder builder, SecureHeadersMiddlewareConfiguration config)
        {
            return builder.UseMiddleware<SecureHeadersMiddleware>(config);
        }

        private static void CheckCspInUseAndVaild(SecureHeadersMiddlewareConfiguration config)
        {
            if (config?.ContentSecurityPolicyConfiguration == null)
            {
                throw new ArgumentException($"Attempted to Append CSP (by calling {nameof(TryAppendCsp)}), but there was no CSP found");
            }

            if (!config.UseContentSecurityPolicy)
            {
                throw new ArgumentException($"Attempted to Append CSP (by calling {nameof(TryAppendCsp)}), but CSP is not being used");
            }
        }
    }
}
