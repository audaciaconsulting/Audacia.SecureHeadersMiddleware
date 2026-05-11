# Overview

`Audacia.SecureHeadersMiddleware` is a wrapper around the [NetEscapades.AspNetCore.SecurityHeaders](https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders) library. It uses middleware to add security-related headers, such as a Content Security Policy.

# Usage

There are two `IApplicationBuilder` extension methods to add the recommended security headers; the one to use depends on the type of project:
- `UseApiSecurityHeaders` for API projects
- `UseIdentitySecurityHeaders` for MVC projects that implement an authentication provider such as IdentityServer or OpenIddict

Both methods return a `HeaderPolicyCollection`, which can be used to customize the headers. For example, suppose you want to use the default API headers, and also add a bespoke header called `My-Security-Header`, this can be achieved as follows:
```csharp
app.UseApiSecurityHeaders().AddCustomHeader("My-Security-Header", "header value");
```

## Exclude caching for some endpoints

ASP.NET Core includes a `[ResponseCache]` attribute that configures caching headers at the controller or action level. This will not be respected when using `app.UseApiSecurityHeaders()`.

To make the middleware respect this attribute, the `UseApiSecurityHeaders` middleware can be conditionally applied with `app.UseWhen()`.

- `UseApiSecurityHeaders` can be used for endpoints without a `[ResponseCache]` attribute
- `AddDefaultSecurityHeaders` from `NetEscapades.AspNetCore.SecurityHeaders` can be used for endpoints with a `[ResponseCache]` attribute

```csharp
/// <summary>
/// Use middleware to add security headers with custom support for the <see cref="ResponseCacheAttribute"/>.
/// </summary>
internal static IApplicationBuilder UseCustomSecurityHeaders(this IApplicationBuilder app)
{
    // When the endpoint has a ResponseCacheAttribute, only apply the default headers
    app.UseWhen(
        ctx => ctx.IsResponseCachedEndpoint(),
        builder =>
        {
            var headerPolicyCollection = new HeaderPolicyCollection().AddDefaultSecurityHeaders();
            builder.UseSecurityHeaders(headerPolicyCollection);
        });

    // For all other endpoints, apply the Audacia API headers which include cache prevention
    app.UseWhen(
        ctx => !ctx.IsResponseCachedEndpoint(),
        builder => builder.UseApiSecurityHeaders());

    return app;
}

/// <summary>
/// Checks if the endpoint in the context has the <see cref="ResponseCacheAttribute"/>.
/// </summary>
private static bool IsResponseCachedEndpoint(this HttpContext context)
{
    var endpoint = context.GetEndpoint();

    var filter = endpoint?.Metadata.GetMetadata<ResponseCacheAttribute>();

    return filter is not null;
}

...

// Must call this after app.UseRouting()
app.UseCustomSecurityHeaders();
```

# Contributing
We welcome contributions! Please feel free to check our [Contribution Guidlines](https://github.com/audaciaconsulting/.github/blob/main/CONTRIBUTING.md) for feature requests, issue reporting and guidelines.
