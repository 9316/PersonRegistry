using FluentAssertions;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;

namespace PersonRegistry.Domain.Tests.Aggregates;

/// <summary>
/// Unit tests for <see cref="PhoneNumberType"/> entity.
/// </summary>
public class PhoneNumberTypeTests
{
    [Fact]
    public void Create_ShouldReturnValidInstance_WhenValidNameIsProvided()
    {
        // Arrange
        var name = "Mobile";

        // Act
        var phoneNumberType = PhoneNumberType.Create(name);

        // Assert
        phoneNumberType.Should().NotBeNull();
        phoneNumberType.Name.Should().Be(name);
    }

    [Fact]
    public void Create_ShouldTrimName_WhenNameHasWhitespace()
    {
        // Arrange
        var name = "   Home   ";

        // Act
        var phoneNumberType = PhoneNumberType.Create(name);

        // Assert
        phoneNumberType.Name.Should().Be("Home"); // Ensures trimming works
    }

    [Fact]
    public void Update_ShouldModifyPhoneNumberTypeName()
    {
        // Arrange
        var phoneNumberType = PhoneNumberType.Create("Work");

        // Act
        phoneNumberType.Update("Office");

        // Assert
        phoneNumberType.Name.Should().Be("Office");
    }

    [Fact]
    public void Update_ShouldTrimName_WhenNameHasWhitespace()
    {
        // Arrange
        var phoneNumberType = PhoneNumberType.Create("Personal");

        // Act
        phoneNumberType.Update("   Emergency   ");

        // Assert
        phoneNumberType.Name.Should().Be("Emergency"); // Ensures trimming works
    }
}