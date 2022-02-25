using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Audacia.SecureHeadersMiddleware
{
    /// <summary>
    /// Extensions to <see cref="HeaderPolicyCollection"/>.
    /// </summary>
    public static class HeaderPolicyCollectionExtensions
    {
        /// <summary>
        /// Adds a content security policy specifically designed for MVC-based Identity apps (e.g. IdentityServer or OpenIddict implementing apps).
        /// </summary>
        /// <param name="headers">The <see cref="HeaderPolicyCollection"/> to which to add the CSP.</param>
        /// <param name="config">The <see cref="IdentityContentSecurityPolicyConfig"/> object representing CSP configuration.</param>
        /// <returns>The given <paramref name="headers"/>.</returns>
        [SuppressMessage("Member Design", "AV1130:Return type in method signature should be a collection interface instead of a concrete type", Justification = "Needs to return HeaderPolicyCollection as part of a fluent interface.")]
        public static HeaderPolicyCollection AddIdentityContentSecurityPolicy(this HeaderPolicyCollection headers, IdentityContentSecurityPolicyConfig config) =>
            headers.AddContentSecurityPolicy(csp =>
            {
                csp.AddDefaultSrc().None();
                csp.AddScriptSrc().Self().WithHashes(config.ScriptSrcHashes);
                csp.AddStyleSrc().Self().WithHashes(config.StyleSrcHashes);
                csp.AddImgSrc().Self();
                csp.AddFontSrc().Self();
                csp.AddObjectSrc().None();
                csp.AddConnectSrc().Self();
                csp.AddFrameSrc().Self();
                csp.AddFrameAncestors().Self().From(config.AppUrls);
                csp.AddFormAction().Self().From(config.AppUrls);
            });
    }
}
