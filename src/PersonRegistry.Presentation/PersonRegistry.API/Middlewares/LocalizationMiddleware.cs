using PersonRegistry.Common.Configs;
using System.Globalization;

namespace PersonRegistry.API.Middlewares;

/// <summary>
/// Middleware to handle localization based on the `Accept-Language` header.
/// </summary>
public class LocalizationMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizationMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public LocalizationMiddleware(RequestDelegate next) => _next = next;

    /// <summary>
    /// Invokes the middleware to set the current request's culture.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var requestedCultures = context.Request.Headers["Accept-Language"].ToString();
        var defaultCulture = new CultureInfo(CultureLanguageConfig.CultureDefault);

        try
        {
            if (!string.IsNullOrEmpty(requestedCultures))
            {
                var preferredCulture = requestedCultures
                    .Split(',')
                    .Select(c => c.Split(';')[0].Trim()) // Extract language code
                    .FirstOrDefault(c => c.Equals(CultureLanguageConfig.CultureGeorgian, StringComparison.OrdinalIgnoreCase) ||
                                         c.Equals(CultureLanguageConfig.CultureDefault, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrEmpty(preferredCulture))
                {
                    defaultCulture = new CultureInfo(preferredCulture);
                }
            }

            CultureInfo.CurrentCulture = defaultCulture;
            CultureInfo.CurrentUICulture = defaultCulture;
        }
        catch (CultureNotFoundException ex)
        {
            CultureInfo.CurrentCulture = new CultureInfo(CultureLanguageConfig.CultureDefault);
            CultureInfo.CurrentUICulture = new CultureInfo(CultureLanguageConfig.CultureDefault);
        }

        await _next(context);
    }
}

