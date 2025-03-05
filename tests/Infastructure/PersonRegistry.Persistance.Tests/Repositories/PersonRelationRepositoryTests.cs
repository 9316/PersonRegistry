using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories;
using Microsoft.Extensions.Configuration;

namespace PersonRegistry.Persistance.Tests.Repositories;

/// <summary>
/// Unit tests for custom exceptions in <see cref="PersonRelationRepository"/>.
/// </summary>
public class PersonRelationRepositoryTests : IDisposable
{
    private readonly PersonRegistryDbContext _dbContext;
    private readonly PersonRelationRepository _personRelationRepository;

    public PersonRelationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<PersonRegistryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var configurationMock = new Mock<IConfiguration>();

        _dbContext = new PersonRegistryDbContext(options, configurationMock.Object);
        _personRelationRepository = new PersonRelationRepository(_dbContext);
    }

    [Fact]
    public void GetAllRelations_ShouldReturnEmptyList_WhenNoRelationsExist()
    {
        // Act
        var result = _personRelationRepository.GetAllRelations().ToList();

        // Assert
        result.Should().BeEmpty();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
