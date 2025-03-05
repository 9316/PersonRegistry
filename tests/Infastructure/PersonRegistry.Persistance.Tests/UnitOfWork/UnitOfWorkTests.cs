using Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Domain.Aggregates.Person.PersonRelation;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PersonRegistry.Persistance.Tests.UnitOfWork;

/// <summary>
/// Unit tests for custom exceptions in <see cref="UnitOfWork"/>.
/// </summary>
public class UnitOfWorkTests : IDisposable
{
    private readonly Mock<PersonRegistryDbContext> _dbContextMock;
    private readonly Mock<IDbContextTransaction> _transactionMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IPersonRelationTypeRepository> _personRelationTypeRepositoryMock;
    private readonly Mock<IPersonRelationRepository> _personRelationRepositoryMock;
    private readonly Mock<IPhoneNumberTypeRepository> _phoneNumberTypeRepositoryMock;
    private readonly Mock<DatabaseFacade> _databaseMock;
    private readonly Persistence.Repositories.UnitOfWork.UnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        _dbContextMock = new Mock<PersonRegistryDbContext>(
                    new DbContextOptionsBuilder<PersonRegistryDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options,
                    Mock.Of<IConfiguration>()
                );

        _databaseMock = new Mock<DatabaseFacade>(_dbContextMock.Object);
        _transactionMock = new Mock<IDbContextTransaction>();

        _dbContextMock.Setup(db => db.Database).Returns(_databaseMock.Object);

        _databaseMock
            .Setup(db => db.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_transactionMock.Object);

        _cityRepositoryMock = new Mock<ICityRepository>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personRelationTypeRepositoryMock = new Mock<IPersonRelationTypeRepository>();
        _personRelationRepositoryMock = new Mock<IPersonRelationRepository>();
        _phoneNumberTypeRepositoryMock = new Mock<IPhoneNumberTypeRepository>();

        _unitOfWork = new Persistence.Repositories.UnitOfWork.UnitOfWork(
            _dbContextMock.Object,
            _cityRepositoryMock.Object,
            _personRepositoryMock.Object,
            _personRelationTypeRepositoryMock.Object,
            _personRelationRepositoryMock.Object,
            _phoneNumberTypeRepositoryMock.Object
        );
    }

    [Fact]
    public async Task BeginTransactionAsync_ShouldStartNewTransaction()
    {
        // Act
        await _unitOfWork.BeginTransactionAsync(CancellationToken.None);

        // Assert
        _dbContextMock.Verify(db => db.Database.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CommitTransactionAsync_ShouldCommitTransaction_WhenCalled()
    {
        // Arrange
        await _unitOfWork.BeginTransactionAsync(CancellationToken.None);
        _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        await _unitOfWork.CommitTransactionAsync(CancellationToken.None);

        // Assert
        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transactionMock.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CommitTransactionAsync_ShouldRollbackTransaction_WhenExceptionOccurs()
    {
        // Arrange
        await _unitOfWork.BeginTransactionAsync(CancellationToken.None);
        _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _unitOfWork.CommitTransactionAsync(CancellationToken.None));
        _transactionMock.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RollbackTransactionAsync_ShouldRollbackTransaction_WhenCalled()
    {
        // Arrange
        await _unitOfWork.BeginTransactionAsync(CancellationToken.None);

        // Act
        await _unitOfWork.RollbackTransactionAsync(CancellationToken.None);

        // Assert
        _transactionMock.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldSaveChangesToDatabase()
    {
        // Arrange
        _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        // Assert
        result.Should().Be(1);
        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void Dispose_ShouldReleaseResources()
    {
        // Act
        _unitOfWork.Dispose();

        // Assert
        _dbContextMock.Verify(db => db.Dispose(), Times.Once);
        _transactionMock.Verify(t => t.Dispose(), Times.Never);
    }

    public void Dispose()
    {
        _unitOfWork.Dispose();
    }
}