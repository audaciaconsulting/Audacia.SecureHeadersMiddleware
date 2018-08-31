namespace Audacia.SecureHeadersMiddleware.Models.ContentSecurityPolicy
{
    public class DirectiveAndType
    {
        /// <summary>
        /// The domain, any sub domains, and tld where a given resource can be loaded from.
        /// As an example:
        ///    In order to load `https://code.jquery.com/jquery-3.3.1.min.js`, the source
        ///    value should be: `https://code.jquery.com/`
        ///    
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Whether the CSP directive is either a CSP command (like 'self' or 'blob') or
        /// whether it is for an external domain.
        /// As an example:
        ///     If you want to enable loading resources from the origin server, you need to
        ///   include an instance of this class with DirectiveType set t <see cref="CspDirective"/>
        ///     But for an external resource (say, jQuery from a CDN), you need to include an
        ///   instance of this class with DirectiveType set to <see cref="ExternalDomain" />
        /// </summary>
        public DirectiveType DirectiveType { get; set; }
    }
}