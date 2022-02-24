using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

namespace Audacia.SecureHeadersMiddleware
{
    /// <summary>
    /// Extensions to <see cref="CspDirectiveBuilder"/>.
    /// </summary>
    public static class CspDirectiveBuilderExtensions
    {
        /// <summary>
        /// Adds the <paramref name="algorithmHashValues"/> to the given <see cref="CspDirectiveBuilder"/>.
        /// Each <see cref="string"/> in the <paramref name="algorithmHashValues"/> collection must be in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.
        /// </summary>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to which to add the hashes.</param>
        /// <param name="algorithmHashValues">A collection of <see cref="string"/>s representing the algorithm and hash of the resource; each <see cref="string"/> must be in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.</param>
        /// <returns>The given <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentException">One of the items in <paramref name="algorithmHashValues"/> is in an incorrect format.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="algorithmHashValues"/> is <see langword="null"/>.</exception>
        public static CspDirectiveBuilder WithHashes(this CspDirectiveBuilder builder, IEnumerable<string> algorithmHashValues)
        {
            if (algorithmHashValues == null) throw new ArgumentNullException(nameof(algorithmHashValues));

            foreach (var value in algorithmHashValues)
            {
                var algorithmHash = value.Split('-');
                if (algorithmHash.Length != 2)
                {
                    throw new ArgumentException($"{nameof(algorithmHashValues)} must contain values in the format 'algorithm-value', e.g. 'sha256-48t4ihreaewhfriujfs'.");
                }

                builder.WithHash(algorithmHash[0], algorithmHash[1]);
            }

            return builder;
        }
    }
}
