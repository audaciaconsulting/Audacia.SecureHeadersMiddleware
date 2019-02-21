using System.Text;
using Audacia.SecureHeadersMiddleware.Enums;
using Audacia.SecureHeadersMiddleware.Helpers;

namespace Audacia.SecureHeadersMiddleware.Models
{
    public class XFrameOptionsConfiguration : IConfigurationBase
    {
        public XFrameOptions OptionValue { get; set; }
        public string AllowFromDomain { get; set; }

        protected XFrameOptionsConfiguration() { }

        public XFrameOptionsConfiguration(XFrameOptions xFrameOption, string allowFromDomain)
        {
            OptionValue = xFrameOption;
            AllowFromDomain = allowFromDomain;
        }

        /// <summary>
        /// Used to set the validate and set the XFramesOptionValue
        /// </summary>
        public void SetValues(XFrameOptions optionValue, string domain = null)
        {
            if (optionValue == XFrameOptions.allowfrom && string.IsNullOrWhiteSpace(domain))
            {
                ArgumentExceptionHelper.RaiseException(nameof(domain));
            }

            OptionValue = optionValue;
            AllowFromDomain = domain;
        }


        /// <summary>
        /// Builds the HTTP header value
        /// </summary>
        /// <returns>A string representing the HTTP header value</returns>
        public string BuildHeaderValue()
        {
            var stringBuilder = new StringBuilder();
            switch (OptionValue)
            {
                case XFrameOptions.deny:
                    stringBuilder.Append("DENY");
                    break;
                case XFrameOptions.sameorigin:
                    stringBuilder.Append("SAMEORIGIN");
                    break;
                case XFrameOptions.allowfrom:
                    if (string.IsNullOrWhiteSpace(AllowFromDomain))
                    {
                        ArgumentExceptionHelper.RaiseException(nameof(AllowFromDomain));
                    }
                    stringBuilder.Append($"ALLOW-FROM({AllowFromDomain})");
                    break;
                case XFrameOptions.allowall:
                    stringBuilder.Append("ALLOWALL");
                    break;
            }

            return stringBuilder.ToString();
        }
    }
}
