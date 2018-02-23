using System.Collections.Generic;
using System.Linq;
using System.Text;
using Audacia.SecureHeadersMiddleware.Models;

namespace Audacia.SecureHeadersMiddleware.Extensions
{
    public static class StringBuilderExtentions
    {
        /// <summary>
        /// Used to build the concatenated string value for the given values
        /// </summary>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> to use</param>
        /// <param name="directiveName">The name of the CSP directive</param>
        /// <param name="directiveValues">A list of strings representing the directive values</param>
        /// <returns>The updated <see cref="StringBuilder" /> instance</returns>
        public static StringBuilder BuildValuesForDirective(this StringBuilder @stringBuilder,
            string directiveName, List<DirectiveAndType> directiveValues)
        {
            if (!directiveValues.Any()) return stringBuilder;
            
            @stringBuilder.Append(directiveName);            
            if (directiveValues.Any(d => d.DirectiveType == DirectiveType.CspDirective))
            {
                @stringBuilder.Append(" ");
                @stringBuilder.Append(string.Join(" ",
                    directiveValues.Where(d => (d.DirectiveType == DirectiveType.CspDirective)).Select(e => $"'{e.Uri}'")));
            }

            if (directiveValues.Any(d => d.DirectiveType == DirectiveType.ExternalDomain))
            {
                @stringBuilder.Append(" ");
                @stringBuilder.Append(string.Join(" ",
                    directiveValues.Where(d => (d.DirectiveType == DirectiveType.ExternalDomain)).Select(e => e.Uri)));
            }
            @stringBuilder.Append(";");
            return stringBuilder;
        }
    }
}
