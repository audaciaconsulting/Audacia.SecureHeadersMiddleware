using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using Shouldly;
using Xunit;

namespace Audacia.SecureHeadersMiddleware.Tests
{
    /// <summary>
    /// Unit tests covering <see cref="CspDirectiveBuilderExtensions"/>.
    /// </summary>
    public class CspDirectiveBuilderExtensionsTests
    {
        /// <summary>
        /// Asserts that the <see cref="CspDirectiveBuilderExtensions.WithHashes"/> extension method correctly appends hash directives to the CSP header.
        /// </summary>
        [Fact]
        public async Task WithHashes_AppendsDataToCsp()
        {
            // Arrange: Set up a config with a script hash, and a header policy collection to apply the CSP directives to.
            var config = new IdentityContentSecurityPolicyConfig("https://example.com")
            {
                ScriptSrcHashes = ["sha256-test123"]
            };

            var headers = new HeaderPolicyCollection();

            // Act: Apply the CSP directives to the header collection.
            headers.AddContentSecurityPolicy(csp => csp.AddScriptSrc().WithHashes(config.ScriptSrcHashes));

            // Assert: Ensure the CSP header is set, and the script hash is included in the header value.
            headers.TryGetValue(HeaderNames.ContentSecurityPolicy, out var cspHeader).ShouldBeTrue();

            var context = new DefaultHttpContext();
            var result = new CustomHeadersResult();
            cspHeader.Apply(context, result);

            result.SetHeaders[HeaderNames.ContentSecurityPolicy].ShouldContain("script-src 'sha256-test123'");
        }
    }
}