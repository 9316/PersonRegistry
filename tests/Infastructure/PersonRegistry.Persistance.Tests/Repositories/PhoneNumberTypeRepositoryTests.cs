using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories;

namespace PersonRegistry.Persistance.Tests.Repositories;

/// <summary>
/// Unit tests for custom exceptions in <see cref="PhoneNumberTypeRepository"/>.
/// </summary>
public class PhoneNumberTypeRepositoryTests : IDisposable
{
    private readonly PersonRegistryDbContext _dbContext;
    private readonly PhoneNumberTypeRepository _phoneNumberTypeRepository;

    public PhoneNumberTypeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<PersonRegistryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var configurationMock = new Mock<IConfiguration>();

        _dbContext = new PersonRegistryDbContext(options, configurationMock.Object);
        _phoneNumberTypeRepository = new PhoneNumberTypeRepository(_dbContext);
    }

    [Fact]
    public async Task AddAsync_ShouldAddPhoneNumberType()
    {
        // Arrange
        var phoneNumberType = new PhoneNumberType(name: "Mobile");

        // Act
        await _phoneNumberTypeRepository.AddAsync(phoneNumberType);
        await _dbContext.SaveChangesAsync();

        var result = await _phoneNumberTypeRepository.GetByIdAsync(phoneNumberType.Id);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Mobile");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemovePhoneNumberType()
    {
        // Arrange
        var phoneNumberType = new PhoneNumberType(name: "Work");

        await _phoneNumberTypeRepository.AddAsync(phoneNumberType);
        await _dbContext.SaveChangesAsync();

        // Act
        _phoneNumberTypeRepository.Delete(phoneNumberType);
        await _dbContext.SaveChangesAsync();

        var result = await _phoneNumberTypeRepository.GetByIdAsync(phoneNumberType.Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Exists_ShouldReturnTrue_WhenPhoneNumberTypeExists()
    {
        // Arrange
        var phoneNumberType = new PhoneNumberType(name: "Home");

        await _phoneNumberTypeRepository.AddAsync(phoneNumberType);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _phoneNumberTypeRepository.AnyAsync(p => p.Id == phoneNumberType.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenPhoneNumberTypeDoesNotExist()
    {
        // Act
        var result = await _phoneNumberTypeRepository.AnyAsync(p => p.Id == 999);

        // Assert
        result.Should().BeFalse();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}