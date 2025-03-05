using Microsoft.EntityFrameworkCore.Storage;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Domain.Aggregates.Person.PersonRelation;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Persistence.Context;

namespace PersonRegistry.Persistence.Repositories.UnitOfWork;

/// <summary>
/// Implements the Unit of Work pattern to manage database transactions.
/// </summary>
/// <param name="_context">The database context.</param>
/// <param name="cityRepository">Repository for managing city entities.</param>
/// <param name="personRepository">Repository for managing person entities.</param>
/// <param name="personRelationTypeRepository">Repository for managing person relation types.</param>
/// <param name="personRelationRepository">Repository for managing person relations.</param>
/// <param name="phoneNumberTypeRepository">Repository for managing phone number types.</param>
public class UnitOfWork(
    PersonRegistryDbContext _context,
    ICityRepository cityRepository,
    IPersonRepository personRepository,
    IPersonRelationTypeRepository personRelationTypeRepository,
    IPersonRelationRepository personRelationRepository,
    IPhoneNumberTypeRepository phoneNumberTypeRepository
) : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _transaction;

    /// <summary>
    /// Gets the repository for managing city entities.
    /// </summary>
    public ICityRepository CityRepository { get; } = cityRepository;

    /// <summary>
    /// Gets the repository for managing person entities.
    /// </summary>
    public IPersonRepository PersonRepository { get; } = personRepository;

    /// <summary>
    /// Gets the repository for managing person relation types.
    /// </summary>
    public IPersonRelationTypeRepository PersonRelationTypeRepository { get; } = personRelationTypeRepository;

    /// <summary>
    /// Gets the repository for managing person relations.
    /// </summary>
    public IPersonRelationRepository PersonRelationRepository { get; } = personRelationRepository;

    /// <summary>
    /// Gets the repository for managing phone number types.
    /// </summary>
    public IPhoneNumberTypeRepository PhoneNumberTypeRepository { get; } = phoneNumberTypeRepository;

    /// <summary>
    /// Begins a new database transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Commits the current database transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    /// <summary>
    /// Rolls back the current database transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;

        await _transaction.RollbackAsync(cancellationToken);
        await DisposeTransactionAsync();
    }

    /// <summary>
    /// Saves all changes made in this context to the database synchronously.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    public int SaveChanges() => _context.SaveChanges();

    /// <summary>
    /// Saves all changes made in this context to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Disposes the transaction and releases resources.
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
        _transaction = null;
    }

    /// <summary>
    /// Asynchronously disposes the transaction.
    /// </summary>
    private async Task DisposeTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}