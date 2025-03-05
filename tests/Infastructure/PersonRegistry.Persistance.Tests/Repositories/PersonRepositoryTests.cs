using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Configuration;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories;

namespace PersonRegistry.Persistance.Tests.Repositories;

/// <summary>
/// Unit tests for custom exceptions in <see cref="PersonRepository"/>.
/// </summary>
public class PersonRepositoryTests : IDisposable
{
    private readonly PersonRegistryDbContext _dbContext;
    private readonly PersonRepository _personRepository;

    public PersonRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<PersonRegistryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var configurationMock = new Mock<IConfiguration>();

        _dbContext = new PersonRegistryDbContext(options, configurationMock.Object);
        _personRepository = new PersonRepository(_dbContext);
    }

    [Fact]
    public async Task GetDetailsByIdAsync_ShouldReturnNull_WhenPersonDoesNotExist()
    {
        // Act
        var result = await _personRepository.GetDetailsByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}