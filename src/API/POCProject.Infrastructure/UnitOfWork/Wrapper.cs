using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POCProject.Infrastructure.Repositories.Concrete;
using POCProject.Infrastructure.Repositories.Contract;
using System.Collections.Concurrent;

namespace POCProject.Infrastructure.UnitOfWork;

/// <summary>
/// Represents a unit of work implementation that manages database operations
/// and provides access to repositories. This class handles saving changes
/// and transaction management while ensuring proper disposal of resources.
/// </summary>
public class Wrapper : IWrapper
{
    private readonly ApplicationDbContext context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new ConcurrentDictionary<Type, object>();
    private bool _disposed = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="Wrapper"/> class.
    /// </summary>
    /// <param name="_context">The application database context to be used.</param>
    public Wrapper(ApplicationDbContext _context) =>
        context = _context;

    /// <summary>
    /// Retrieves a repository for the specified entity type.
    /// </summary>
    /// <typeparam name="T">The type of the entity for which the repository is requested.</typeparam>
    /// <returns>An instance of <see cref="IBaseRepository{T}"/> for the specified entity type.</returns>
    public IBaseRepository<T> GetRepository<T>() where T : class
    {
        return (IBaseRepository<T>)_repositories.GetOrAdd(typeof(T), _ => new BaseRepository<T>(context));
    }

    /// <summary>
    /// Asynchronously saves changes made in the unit of work to the database.
    /// Manages transaction and handles potential database update exceptions.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the result being the number of state entries written to the database.</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (DbUpdateException dbEx)
        {
            var innerException = dbEx.InnerException;
            if (innerException != null)
            {
                Console.WriteLine(innerException.Message);
                if (innerException is SqlException sqEx)
                {
                    Console.WriteLine(sqEx.SqlState);
                    Console.WriteLine(sqEx.Message);
                    foreach (SqlError error in sqEx.Errors)
                        Console.WriteLine($"Error {error.Number}: {error.Message}");
                }
            }
            else
            {
                Console.WriteLine(dbEx.Message);
            }
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="Wrapper"/> class
    /// and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">A boolean indicating whether the method is called from Dispose or finalizer.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
            _disposed = true;
        }
    }

    /// <summary>
    /// Releases all resources used by the <see cref="Wrapper"/> class.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
