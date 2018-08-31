using System.Collections.Generic;
using System.Linq;
using System.Text;

using Audacia.SecureHeadersMiddleware.Extensions;

namespace Audacia.SecureHeadersMiddleware.Models.ContentSecurityPolicy
{
    public class Configuration : IConfigurationBase
    {
        /// <summary>
        /// The base-Uri values to use (which can be used in a document's base element)
        /// </summary>
        public List<DirectiveAndType> BaseRules { get; set; }

        /// <summary>
        /// The default-src values to use (as a fallback for the other CSP rules)
        /// </summary>
        public List<DirectiveAndType> DefaultSrc { get; set; }

        /// <summary>
        /// The script-src values to use (valid sources for sources for JavaScript)
        /// </summary>
        public List<DirectiveAndType> ScriptSrc { get; set; }

        /// <summary>
        /// The object-src values to use (valid sources for the object, embed, and applet elements)
        /// </summary>
        public List<DirectiveAndType> ObjectSrc { get; set; }

        /// <summary>
        /// The style-src values to use (valid sources for style sheets)
        /// </summary>
        public List<DirectiveAndType> StyleSrc { get; set; }

        /// <summary>
        /// The img-src values to use (valid sources for images and favicons)
        /// </summary>
        public List<DirectiveAndType> ImgSrc { get; set; }

        /// <summary>
        /// The media-src values to use (valid sources for loading media using the audio and video elements)
        /// </summary>
        public List<DirectiveAndType> MediaSrc { get; set; }

        /// <summary>
        /// The frame-src values to use (valid sources for nested browsing contexts loading using elements such as frame and iframe)
        /// </summary>
        public List<DirectiveAndType> FrameSrc { get; set; }

        /// <summary>
        /// The child-src values to use (valid sources for web workers and nested browsing contexts loaded using elements such as frame and iframe)
        /// </summary>
        public List<DirectiveAndType> ChildSrc { get; set; }

        /// <summary>
        /// The frame-ancestors values to use (valid parents that may embed a page using frame, iframe, object, embed, or applet)
        /// </summary>
        public List<DirectiveAndType> FrameAncestors { get; set; }

        /// <summary>
        /// The font-src values to use (valid sources for fonts loaded using @font-face)
        /// </summary>
        public List<DirectiveAndType> FontSrc { get; set; }

        /// <summary>
        /// The connect-src values to use (restricts the URLs which can be loaded using script interfaces)
        /// </summary>
        public List<DirectiveAndType> ConnectSrc { get; set; }

        /// <summary>
        /// The manifest-src values to use (which manifest can be applied to the resource)
        /// </summary>
        public List<DirectiveAndType> ManifestSrc { get; set; }

        /// <summary>
        /// The form-action values to use (restricts the URLs which can be used as the target of a form submissions from a given context)
        /// </summary>
        public List<DirectiveAndType> FormAction { get; set; }
        
        /// <summary>
        /// Specifies an HTML sandbox policy that the user agent applies to the protected resource.
        /// </summary>
        public SandBox Sandbox { get; set; }
        
        /// <summary>
        /// Define the set of plugins that can be invoked by the protected resource by limiting the types of resources that can be embedded
        /// </summary>
        public string PluginTypes { get; set; }

        /// <summary>
        /// Whether to include the block-all-mixed-content directive (prevents loading any assets using HTTP when the page is loaded using HTTPS)
        /// </summary>
        public bool BlockAllMixedContent { get; set; }

        /// <summary>
        /// Whether to include the upgrade-insecure-requests directive (instructs user agents to treat all of a site's insecure URLs as though they have been replaced with secure URLs)
        /// </summary>
        public bool UpgradeInsecureRequests { get; set; }
        
        /// <summary>
        /// Define information user agent must send in Referer header
        /// </summary>
        public string Referrer { get; set; }

        /// <summary>
        /// Whether to instruct the user agent to report attempts to violate the Content Security Policy
        /// </summary>
        public string ReportUri { get; set; }

        protected Configuration() { }

        public Configuration(string pluginTypes, bool blockAllMixedContent,
            bool upgradeInsecureRequests, string referrer, string reportUri)
        {
            BaseRules = new List<DirectiveAndType>();
            DefaultSrc = new List<DirectiveAndType>();
            ScriptSrc = new List<DirectiveAndType>();
            ObjectSrc = new List<DirectiveAndType>();
            StyleSrc = new List<DirectiveAndType>();
            ImgSrc = new List<DirectiveAndType>();
            MediaSrc = new List<DirectiveAndType>();
            FrameSrc = new List<DirectiveAndType>();
            ChildSrc = new List<DirectiveAndType>();
            FrameAncestors = new List<DirectiveAndType>();
            FontSrc = new List<DirectiveAndType>();
            ConnectSrc = new List<DirectiveAndType>();
            ManifestSrc = new List<DirectiveAndType>();
            FormAction = new List<DirectiveAndType>();

            PluginTypes = pluginTypes;
            BlockAllMixedContent = blockAllMixedContent;
            UpgradeInsecureRequests = upgradeInsecureRequests;
            Referrer = referrer;
            ReportUri = reportUri;
        }

        /// <summary>
        /// Builds the HTTP header value
        /// </summary>
        /// <returns>A string representing the HTTP header value</returns>
        public string BuildHeaderValue()
        {
            var stringBuilder = new StringBuilder();

            if (AnyValues())
            {
                stringBuilder.BuildValuesForDirective("base-Uri", BaseRules);
                stringBuilder.BuildValuesForDirective("default-src", DefaultSrc);
                stringBuilder.BuildValuesForDirective("script-src", ScriptSrc);
                stringBuilder.BuildValuesForDirective("object-src", ObjectSrc);
                stringBuilder.BuildValuesForDirective("style-src", StyleSrc);
                stringBuilder.BuildValuesForDirective("img-src", ImgSrc);
                stringBuilder.BuildValuesForDirective("media-src", MediaSrc);
                stringBuilder.BuildValuesForDirective("frame-src", FrameSrc);
                stringBuilder.BuildValuesForDirective("child-src", ChildSrc);
                stringBuilder.BuildValuesForDirective("frame-ancestors", FrameAncestors);
                stringBuilder.BuildValuesForDirective("font-src", FontSrc);
                stringBuilder.BuildValuesForDirective("connect-src", ConnectSrc);
                stringBuilder.BuildValuesForDirective("manifest-src", ManifestSrc);
                stringBuilder.BuildValuesForDirective("form-action", FormAction);
            }

            if (Sandbox != null)
            {
                stringBuilder.Append(Sandbox.BuildHeaderValue());
            }

            if (!string.IsNullOrWhiteSpace(PluginTypes))
            {
                stringBuilder.Append($"plugin-types {PluginTypes}; ");
            }

            if (BlockAllMixedContent)
            {
                stringBuilder.Append("block-all-mixed-content; ");
            }
            
            if (UpgradeInsecureRequests)
            {
                stringBuilder.Append("upgrade-insecure-requests; ");
            }

            if (!string.IsNullOrWhiteSpace(Referrer))
            {
                stringBuilder.Append($"referrer {Referrer}; ");
            }
            
            if (!string.IsNullOrWhiteSpace(ReportUri))
            {
                stringBuilder.Append($"report-Uri {ReportUri}; ");
            }

            return stringBuilder.ToString();
        }

        private bool AnyValues()
        {
            return BaseRules.Any()    || DefaultSrc.Any() || ScriptSrc.Any()  || ObjectSrc.Any()
                || StyleSrc.Any()   || ImgSrc.Any()     || MediaSrc.Any()   || FrameSrc.Any()
                || ChildSrc.Any()   || FrameAncestors.Any() || FontSrc.Any()|| ConnectSrc.Any()
                || ManifestSrc.Any()|| FormAction.Any();
        }
    }
}
