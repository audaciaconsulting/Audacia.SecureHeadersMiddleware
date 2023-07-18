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

# Contributing
We welcome contributions! Please feel free to check our [Contribution Guidlines](https://github.com/audaciaconsulting/.github/blob/main/CONTRIBUTING.md) for feature requests, issue reporting and guidelines.