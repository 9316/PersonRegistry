using FluentAssertions;
using PersonRegistry.Common.Exceptions;
using CommonArgumentException = PersonRegistry.Common.Exceptions.ArgumentException;

/// <summary>
/// Unit tests for custom exceptions in <see cref="PersonRegistry.Common.Exceptions"/>.
/// </summary>
public class ExceptionTests
{
    private const string ErrorMessage = "Test exception message";
    private static readonly Exception InnerException = new("Inner exception");

    [Fact]
    public void AlreadyExistsException_ShouldSetMessageCorrectly()
    {
        // Act
        var exception = new AlreadyExistsException(ErrorMessage);

        // Assert
        exception.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void AlreadyExistsException_ShouldSetInnerExceptionCorrectly()
    {
        // Act
        var exception = new AlreadyExistsException(ErrorMessage, InnerException);

        // Assert
        exception.Message.Should().Be(ErrorMessage);
        exception.InnerException.Should().Be(InnerException);
    }

    [Fact]
    public void ArgumentException_ShouldSetMessageCorrectly()
    {
        // Act
        var exception = new CommonArgumentException(ErrorMessage);

        // Assert
        exception.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void ArgumentException_ShouldSetInnerExceptionCorrectly()
    {
        // Act
        var exception = new CommonArgumentException(ErrorMessage, InnerException);

        // Assert
        exception.Message.Should().Be(ErrorMessage);
        exception.InnerException.Should().Be(InnerException);
    }

    [Fact]
    public void BadRequestException_ShouldSetMessageCorrectly()
    {
        // Act
        var exception = new BadRequestException(ErrorMessage);

        // Assert
        exception.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void BadRequestException_ShouldSetInnerExceptionCorrectly()
    {
        // Act
        var exception = new BadRequestException(ErrorMessage, InnerException);

        // Assert
        exception.Message.Should().Be(ErrorMessage);
        exception.InnerException.Should().Be(InnerException);
    }

    [Fact]
    public void NotFoundException_ShouldSetMessageCorrectly()
    {
        // Act
        var exception = new NotFoundException(ErrorMessage);

        // Assert
        exception.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void NotFoundException_ShouldSetInnerExceptionCorrectly()
    {
        // Act
        var exception = new NotFoundException(ErrorMessage);

        // Assert
        exception.Message.Should().Be(ErrorMessage);
    }
}
