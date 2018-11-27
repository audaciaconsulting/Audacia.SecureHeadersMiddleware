using System.Collections.Generic;
using Audacia.SecureHeadersMiddleware.Enums;
using Audacia.SecureHeadersMiddleware.Helpers;

namespace Audacia.SecureHeadersMiddleware.Models.ContentSecurityPolicy
{
    public static class Extentions
    {
        /// <summary>
        /// Used to append a single Content Security Policy URI for a given <see cref="CspUriType"/>
        /// </summary>
        public static Configuration AddCspRule
            (this Configuration @this, DirectiveAndType rule, CspUriType uriType)
        {
            switch (uriType)
            {
                case CspUriType.Base:
                    @this.BaseRules.Add(rule);
                    break;
                case CspUriType.DefaultUri:
                    @this.DefaultSrc.Add(rule);
                    break;
                case CspUriType.Script:
                    @this.ScriptSrc.Add(rule);
                    break;
                case CspUriType.Object:
                    @this.ObjectSrc.Add(rule);
                    break;
                case CspUriType.Style:
                    @this.StyleSrc.Add(rule);
                    break;
                case CspUriType.Img:
                    @this.ImgSrc.Add(rule);
                    break;
                case CspUriType.Media:
                    @this.MediaSrc.Add(rule);
                    break;
                case CspUriType.Frame:
                    @this.FrameSrc.Add(rule);
                    break;
                case CspUriType.Child:
                    @this.ChildSrc.Add(rule);
                    break;
                case CspUriType.FrameAncestors:
                    @this.FrameAncestors.Add(rule);
                    break;
                case CspUriType.Font:
                    @this.FontSrc.Add(rule);
                    break;
                case CspUriType.Connect:
                    @this.ConnectSrc.Add(rule);
                    break;
                case CspUriType.Manifest:
                    @this.ManifestSrc.Add(rule);
                    break;
                case CspUriType.FormAction:
                    @this.FormAction.Add(rule);
                    break;
                default:
                    ArgumentExceptionHelper.RaiseException(nameof(CspUriType));
                    break;
            }

            return @this;
        }

        /// <summary>
        /// Used to set the Content Security Policy URIs for a given <see cref="CspUriType"/>
        /// </summary>
        public static Configuration SetCspRules
            (this Configuration @this, List<DirectiveAndType> rules, CspUriType uriType)
        {
            switch (uriType)
            {
                case CspUriType.Base:
                    @this.BaseRules = rules;
                    break;
                case CspUriType.DefaultUri:
                    @this.DefaultSrc = rules;
                    break;
                case CspUriType.Script:
                    @this.ScriptSrc = rules;
                    break;
                case CspUriType.Object:
                    @this.ObjectSrc = rules;
                    break;
                case CspUriType.Style:
                    @this.StyleSrc = rules;
                    break;
                case CspUriType.Img:
                    @this.ImgSrc = rules;
                    break;
                case CspUriType.Media:
                    @this.MediaSrc = rules;
                    break;
                case CspUriType.Frame:
                    @this.FrameSrc = rules;
                    break;
                case CspUriType.Child:
                    @this.ChildSrc = rules;
                    break;
                case CspUriType.FrameAncestors:
                    @this.FrameAncestors = rules;
                    break;
                case CspUriType.Font:
                    @this.FontSrc = rules;
                    break;
                case CspUriType.Connect:
                    @this.ConnectSrc = rules;
                    break;
                case CspUriType.Manifest:
                    @this.ManifestSrc = rules;
                    break;
                case CspUriType.FormAction:
                    @this.FormAction = rules;
                    break;
                default:
                    ArgumentExceptionHelper.RaiseException(nameof(CspUriType));
                    break;
            }

            return @this;
        }

        /// <summary>
        /// Used the set the <see cref="CspSandboxType"/> forthe Content Secuity Policy
        /// </summary>
        public static Configuration SetSandbox
            (this Configuration @this, CspSandboxType sandboxType)
        {
            @this.Sandbox = new SandBox(sandboxType);

            return @this;
        }
    }
}