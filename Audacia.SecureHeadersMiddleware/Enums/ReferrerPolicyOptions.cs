namespace Audacia.SecureHeadersMiddleware.Enums
{
    public enum ReferrerPolicyOptions
    {
        noReferrer,
        noReferrerWhenDowngrade,
        origin,
        originWhenCrossOrigin,
        sameOrigin,
        strictOrigin,
        strictWhenCrossOrigin,
        unsafeUrl
    };
}