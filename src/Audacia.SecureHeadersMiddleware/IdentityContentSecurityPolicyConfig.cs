using System;
using System.Collections.Generic;

namespace Audacia.SecureHeadersMiddleware
{
    /// <summary>
    /// Represents configurable data for the CSP for Identity apps.
    /// </summary>
    public class IdentityContentSecurityPolicyConfig
    {
        /// <summary>
        /// Gets or sets the collection of urls for UI apps that use the Identity app.
        /// </summary>
        public IReadOnlyCollection<string> AppUrls { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the hashes to be added to the 'script-src' directive.
        /// </summary>
        public IReadOnlyCollection<string> ScriptSrcHashes { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the hashes to be added to the 'style-src' directive.
        /// </summary>
        public IReadOnlyCollection<string> StyleSrcHashes { get; set; } = new List<string>();

        /// <summary>
        /// Initializes a default instance of <see cref="IdentityContentSecurityPolicyConfig"/>.
        /// </summary>
        public IdentityContentSecurityPolicyConfig()
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="IdentityContentSecurityPolicyConfig"/> with the given parameters.
        /// </summary>
        /// <param name="appUrl">The base url of the UI app.</param>
        /// <exception cref="ArgumentNullException"><paramref name="appUrl"/> is <see langword="null"/>.</exception>
        public IdentityContentSecurityPolicyConfig(Uri appUrl)
            : this(appUrl?.ToString() ?? throw new ArgumentNullException(nameof(appUrl)))
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="IdentityContentSecurityPolicyConfig"/> with the given parameters.
        /// </summary>
        /// <param name="appUrl">The base url of the UI app.</param>
        public IdentityContentSecurityPolicyConfig(string appUrl)
            : this(new[] { appUrl }, Array.Empty<string>(), Array.Empty<string>())
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="IdentityContentSecurityPolicyConfig"/> with the given parameters.
        /// </summary>
        /// <param name="appUrl">The base url of the UI app.</param>
        /// <param name="scriptSrcHash">A <see cref="string"/> representing the algorithm and hash of the script in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.</param>
        /// <param name="styleSrcHash">A <see cref="string"/> representing the algorithm and hash of the style in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.</param>
        /// <exception cref="ArgumentNullException"><paramref name="appUrl"/> is <see langword="null"/>.</exception>
        public IdentityContentSecurityPolicyConfig(Uri appUrl, string scriptSrcHash, string styleSrcHash)
            : this(appUrl?.ToString() ?? throw new ArgumentNullException(nameof(appUrl)), scriptSrcHash, styleSrcHash)
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="IdentityContentSecurityPolicyConfig"/> with the given parameters.
        /// </summary>
        /// <param name="appUrl">The base url of the UI app.</param>
        /// <param name="scriptSrcHash">A <see cref="string"/> representing the algorithm and hash of the script in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.</param>
        /// <param name="styleSrcHash">A <see cref="string"/> representing the algorithm and hash of the style in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.</param>
        public IdentityContentSecurityPolicyConfig(string appUrl, string scriptSrcHash, string styleSrcHash)
            : this(new[] { appUrl }, new[] { scriptSrcHash }, new[] { styleSrcHash })
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="IdentityContentSecurityPolicyConfig"/> with the given parameters.
        /// </summary>
        /// <param name="appUrls">The base urls of the UI apps.</param>
        /// <param name="scriptSrcHashes">A collection of <see cref="string"/>s representing the algorithm and hash of the scripts in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.</param>
        /// <param name="styleSrcHashes">A collection of <see cref="string"/>s representing the algorithm and hash of the styles in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.</param>
        public IdentityContentSecurityPolicyConfig(IReadOnlyCollection<string> appUrls, IReadOnlyCollection<string> scriptSrcHashes, IReadOnlyCollection<string> styleSrcHashes)
        {
            AppUrls = appUrls;
            ScriptSrcHashes = scriptSrcHashes;
            StyleSrcHashes = styleSrcHashes;
        }
    }
}
