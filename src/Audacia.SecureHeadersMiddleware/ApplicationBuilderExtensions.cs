using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Audacia.SecureHeadersMiddleware
{
    /// <summary>
    /// Extensions to <see cref="IApplicationBuilder"/> related to security headers.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds a middleware which sets default security headers for Mvc-based Identity provider applications, e.g. IdentityServer or OpenIddict implementations.
        /// </summary>
        /// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/> to which to add the middleware.</param>
        /// <param name="config">The <see cref="IdentityContentSecurityPolicyConfig"/> object describing the configuration of the CSP.</param>
        /// <returns>A <see cref="HeaderPolicyCollection"/> object that can be further modified to customize the headers.</returns>
        [SuppressMessage("Member Design", "AV1130:Return type in method signature should be a collection interface instead of a concrete type", Justification = "Need to return concrete collection as fluent interface.")]
        public static HeaderPolicyCollection UseIdentitySecurityHeaders(this IApplicationBuilder applicationBuilder, IdentityContentSecurityPolicyConfig config)
        {
            var headers = new HeaderPolicyCollection()
                .AddDefaultSecurityHeaders()
                .AddIdentityContentSecurityPolicy(config)
                .AddCustomHeader(HeaderNames.CacheControl, "no-cache, no-store, must-revalidate")
                .AddCustomHeader(HeaderNames.Pragma, "no-cache");
            applicationBuilder.UseSecurityHeaders(headers);

            return headers;
        }

        /// <summary>
        /// Adds a middleware which sets default security headers for Api applications.
        /// </summary>
        /// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/> to which to add the middleware.</param>
        /// <returns>A <see cref="HeaderPolicyCollection"/> object that can be further modified to customize the headers.</returns>
        [SuppressMessage("Member Design", "AV1130:Return type in method signature should be a collection interface instead of a concrete type", Justification = "Need to return concrete collection as fluent interface.")]
        public static HeaderPolicyCollection UseApiSecurityHeaders(this IApplicationBuilder applicationBuilder)
        {
            var headers = new HeaderPolicyCollection()
                .AddDefaultSecurityHeaders()
                .AddCustomHeader(HeaderNames.CacheControl, "no-cache, no-store, must-revalidate")
                .AddCustomHeader(HeaderNames.Pragma, "no-cache");
            applicationBuilder.UseSecurityHeaders(headers);

            return headers;
        }
    }
}
