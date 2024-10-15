using POCProject.Infrastructure.Repositories.Contract;

namespace POCProject.Infrastructure.UnitOfWork;

/// <summary>
/// Represents a unit of work pattern interface that provides access to repositories
/// and manages database transactions. It allows for committing changes to the database
/// and ensures that resources are disposed of properly.
/// </summary>
public interface IWrapper : IDisposable
{
    /// <summary>
    /// Retrieves a repository for the specified entity type.
    /// </summary>
    /// <typeparam name="T">The type of the entity for which the repository is requested.</typeparam>
    /// <returns>An instance of <see cref="IBaseRepository{T}"/> for the specified entity type.</returns>
    IBaseRepository<T> GetRepository<T>() where T : class;

    /// <summary>
    /// Asynchronously saves changes made in the unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the result being the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

