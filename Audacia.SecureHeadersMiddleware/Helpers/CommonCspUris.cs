using System;
using System.Collections.Generic;
using System.Text;
using Audacia.SecureHeadersMiddleware.Models;

namespace Audacia.SecureHeadersMiddleware.Helpers
{
    public static class CommonCspUris
    {
        public static DirectiveAndType NoneDirective = new DirectiveAndType
        {
            DirectiveType = DirectiveType.CspDirective,
            Uri = "none"
        };

        public static DirectiveAndType SafeInlineDirective = new DirectiveAndType
        {
            DirectiveType = DirectiveType.CspDirective,
            Uri = "unsafe-inline"
        };

        public static DirectiveAndType GoogleFonts = new DirectiveAndType
        {
            DirectiveType = DirectiveType.ExternalDomain,   
            Uri = "fonts.googleapis.com"
        };

        public static DirectiveAndType UnsafeEval = new DirectiveAndType
        {
            DirectiveType = DirectiveType.CspDirective,
            Uri = "unsafe-eval"
        };

        public static DirectiveAndType GoogleCDN = new DirectiveAndType()
        {
            DirectiveType = DirectiveType.ExternalDomain,
            Uri = "*.gstatic.com"
        };

        public static DirectiveAndType DataDirective = new DirectiveAndType()
        {
            DirectiveType = DirectiveType.ExternalDomain,
            Uri = "data:"
        };
    }
}
