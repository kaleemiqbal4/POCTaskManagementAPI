using System.Linq.Expressions;

namespace POCProject.Infrastructure.Repositories.Contract;

/// <summary>
/// Defines the contract for a generic base repository that provides CRUD operations
/// for entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of entity to be managed by this repository.</typeparam>
public interface IBaseRepository<TEntity> : IDisposable
{
    /// <summary>
    /// Adds a new entity of type <typeparamref name="TEntity"/> to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateAsync(TEntity entity);

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/> from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of all entities.</returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves an entity by its identifier, using the specified expression to filter the results.
    /// </summary>
    /// <param name="expression">An expression used to filter the entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>
    Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Releases the resources used by the repository instance.
    /// </summary>
    void Dispose();
}
