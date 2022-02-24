using System;
using System.Collections.Generic;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

namespace Audacia.SecureHeadersMiddleware
{
    /// <summary>
    /// Extensions to <see cref="FrameAncestorsDirectiveBuilder"/>.
    /// </summary>
    public static class FrameAncestorsDirectiveBuilderExtensions
    {
        /// <summary>
        /// Adds the given <paramref name="urls"/> as sources to the 'frame-ancestors' directive.
        /// </summary>
        /// <param name="builder">The <see cref="FrameAncestorsDirectiveBuilder"/> to which to add the <paramref name="urls"/>.</param>
        /// <param name="urls">The urls to add as sources.</param>
        /// <returns>The given <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> or <paramref name="urls"/> is <see langword="null"/>.</exception>
        public static FrameAncestorsDirectiveBuilder From(this FrameAncestorsDirectiveBuilder builder, IEnumerable<string> urls)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (urls == null) throw new ArgumentNullException(nameof(urls));

            foreach (var url in urls)
            {
                builder.From(url);
            }

            return builder;
        }
    }
}
