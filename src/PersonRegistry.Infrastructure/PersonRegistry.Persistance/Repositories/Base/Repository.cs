using Microsoft.EntityFrameworkCore;
using PersonRegistry.Domain.Interfaces;
using System.Linq.Expressions;

namespace PersonRegistry.Persistence.Repositories.Base;

/// <summary>
/// Generic repository implementation for database operations.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
/// <param name="_context">The database context.</param>
public class Repository<T>(DbContext _context) : IRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A cancellation token for the async operation.</param>
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    /// <summary>
    /// Asynchronously retrieves all entities from the database.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Retrieves a queryable collection of entities, including specified related entities.
    /// </summary>
    /// <param name="includes">Related entities to include in the query.</param>
    /// <returns>A queryable collection of entities.</returns>
    public IQueryable<T> Query(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes.Length > 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return query;
    }

    /// <summary>
    /// Asynchronously retrieves an entity by its primary key.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    /// <summary>
    /// Asynchronously checks if any entity satisfies the given condition.
    /// </summary>
    /// <param name="predicate">The condition to evaluate.</param>
    /// <returns>True if any entity matches; otherwise, false.</returns>
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }

    /// <summary>
    /// Asynchronously retrieves an entity that matches the given condition.
    /// </summary>
    /// <param name="predicate">The condition to evaluate.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Asynchronously counts the number of entities in a given query.
    /// </summary>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <returns>The total count of entities.</returns>
    public async Task<int> CountAsync(IQueryable<T> entities)
    {
        return await entities.CountAsync();
    }
}