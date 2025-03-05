using System.Globalization;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using PersonRegistry.API.Middlewares;
using PersonRegistry.Common.Configs;

/// <summary>
/// Unit tests for the <see cref="LocalizationMiddleware"/> class.
/// </summary>
public class LocalizationMiddlewareTests
{
    private readonly RequestDelegate _next;
    private readonly LocalizationMiddleware _localizationMiddleware;
    private readonly DefaultHttpContext _context;

    public LocalizationMiddlewareTests()
    {
        _next = Substitute.For<RequestDelegate>();
        _localizationMiddleware = new LocalizationMiddleware(_next);
        _context = new DefaultHttpContext();
    }

    [Fact]
    public async Task InvokeAsync_WhenAcceptLanguageIsGeorgian_ShouldSetCultureToGeorgian()
    {
        // Arrange
        var cultureGeorgian = CultureLanguageConfig.CultureGeorgian;
        CultureInfo.CurrentCulture = new CultureInfo(cultureGeorgian);
        CultureInfo.CurrentUICulture = new CultureInfo(cultureGeorgian);

        _context.Request.Headers["Accept-Language"] = cultureGeorgian;

        // Act
        await _localizationMiddleware.InvokeAsync(_context);

        // Assert
        CultureInfo.CurrentCulture.Name.Should().Be(cultureGeorgian);
        CultureInfo.CurrentUICulture.Name.Should().Be(cultureGeorgian);
    }

    [Fact]
    public async Task InvokeAsync_WhenNoAcceptLanguageHeader_ShouldSetCultureToDefault()
    {
        // Act
        await _localizationMiddleware.InvokeAsync(_context);

        // Assert
        CultureInfo.CurrentCulture.Name.Should().Be(CultureLanguageConfig.CultureDefault);
        CultureInfo.CurrentUICulture.Name.Should().Be(CultureLanguageConfig.CultureDefault);
    }

    [Fact]
    public async Task InvokeAsync_WhenInvalidAcceptLanguageHeader_ShouldSetCultureToDefault()
    {
        // Arrange
        _context.Request.Headers["Accept-Language"] = "invalid-culture-code";

        // Act
        await _localizationMiddleware.InvokeAsync(_context);

        // Assert
        CultureInfo.CurrentCulture.Name.Should().Be(CultureLanguageConfig.CultureDefault);
        CultureInfo.CurrentUICulture.Name.Should().Be(CultureLanguageConfig.CultureDefault);
    }

    [Fact]
    public async Task InvokeAsync_WhenMultipleAcceptLanguageHeaders_ShouldSelectFirstValidCulture()
    {
        // Arrange
        _context.Request.Headers["Accept-Language"] = "invalid-culture-code, en-US, ka-GE";

        // Act
        await _localizationMiddleware.InvokeAsync(_context);

        // Assert
        CultureInfo.CurrentCulture.Name.Should().Be("en-US");
        CultureInfo.CurrentUICulture.Name.Should().Be("en-US");
    }

    [Fact]
    public async Task InvokeAsync_WhenCultureNotFoundExceptionOccurs_ShouldSetCultureToDefault()
    {
        // Arrange
        _context.Request.Headers["Accept-Language"] = "unknown-culture";

        // Act
        await _localizationMiddleware.InvokeAsync(_context);

        // Assert
        CultureInfo.CurrentCulture.Name.Should().Be(CultureLanguageConfig.CultureDefault);
        CultureInfo.CurrentUICulture.Name.Should().Be(CultureLanguageConfig.CultureDefault);
    }
}
