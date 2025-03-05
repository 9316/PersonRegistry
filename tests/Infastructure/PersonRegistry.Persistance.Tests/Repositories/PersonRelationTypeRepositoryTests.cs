using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories;
using Microsoft.Extensions.Configuration;

namespace PersonRegistry.Persistance.Tests.Repositories;

/// <summary>
/// Unit tests for custom exceptions in <see cref="PersonRelationTypeRepository"/>.
/// </summary>
public class PersonRelationTypeRepositoryTests : IDisposable
{
    private readonly PersonRegistryDbContext _dbContext;
    private readonly PersonRelationTypeRepository _personRelationTypeRepository;

    public PersonRelationTypeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<PersonRegistryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var configurationMock = new Mock<IConfiguration>();

        _dbContext = new PersonRegistryDbContext(options, configurationMock.Object);
        _personRelationTypeRepository = new PersonRelationTypeRepository(_dbContext);
    }

    [Fact]
    public async Task AddAsync_ShouldAddPersonRelationType()
    {
        // Arrange
        var relationType = new PersonRelationType(name: "Sibling");

        // Act
        await _personRelationTypeRepository.AddAsync(relationType);
        await _dbContext.SaveChangesAsync();

        var result = await _personRelationTypeRepository.GetByIdAsync(relationType.Id);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Sibling");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemovePersonRelationType()
    {
        // Arrange
        var relationType = new PersonRelationType(name: "Parent");
        await _personRelationTypeRepository.AddAsync(relationType);
        await _dbContext.SaveChangesAsync();

        // Act
        _personRelationTypeRepository.Delete(relationType);
        await _dbContext.SaveChangesAsync();

        var result = await _personRelationTypeRepository.GetByIdAsync(relationType.Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Exists_ShouldReturnTrue_WhenRelationTypeExists()
    {
        // Arrange
        var relationType = new PersonRelationType(name: "Friend");
        await _personRelationTypeRepository.AddAsync(relationType);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _personRelationTypeRepository.AnyAsync(rt => rt.Id == relationType.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenRelationTypeDoesNotExist()
    {
        // Act
        var exists = await _personRelationTypeRepository.AnyAsync(rt => rt.Id == 999);

        // Assert
        exists.Should().BeFalse();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
