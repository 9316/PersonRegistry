using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using MediatR;
using FluentValidation;
using PersonRegistry.Application.Extensions;
using PersonRegistry.Application.Common.Behaviours;
using PersonRegistry.Application.Person.Commands.Create;

namespace PersonRegistry.Application.Tests.Extensions;

/// <summary>
/// Unit tests for custom exceptions in <see cref="ApplicationServiceExtension"/>.
/// </summary>
public class ApplicationServiceExtensionTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ServiceCollection _services;

    public ApplicationServiceExtensionTests()
    {
        _services = new ServiceCollection();
        _services.AddApplicationServices();
        _serviceProvider = _services.BuildServiceProvider();
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterMediatR()
    {
        // Act
        var mediator = _serviceProvider.GetService<IMediator>();

        // Assert
        mediator.Should().NotBeNull();
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterFluentValidationValidators()
    {
        // Act
        var validator = _serviceProvider.GetService<IValidator<CreatePersonCommand>>();

        // Assert
        validator.Should().NotBeNull();
        validator.Should().BeAssignableTo<IValidator<CreatePersonCommand>>();
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterPipelineBehaviors()
    {
        // Act
        var pipelineBehaviors = _services.Where(x => x.ServiceType.IsGenericType
                                                    && x.ServiceType.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>))
                                         .Select(x => x.ImplementationType)
                                         .ToList();

        // Assert
        pipelineBehaviors.Should().Contain(typeof(RequestTransactionBehavior<,>));
        pipelineBehaviors.Should().Contain(typeof(RequestValidationBehavior<,>));
    }
}
