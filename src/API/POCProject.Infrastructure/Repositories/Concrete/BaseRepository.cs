using Microsoft.EntityFrameworkCore;
using POCProject.Infrastructure.Repositories.Contract;
using System.Linq.Expressions;

namespace POCProject.Infrastructure.Repositories.Concrete;

/// <summary>
/// A generic base repository for performing CRUD operations on entities of type <typeparamref name="TEntity"/>.
/// Implements common data access patterns using Entity Framework Core.
/// </summary>
/// <typeparam name="TEntity">The type of entity to be managed by this repository.</typeparam>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="_dbContext">The database context to be used for data access.</param>
    public BaseRepository(ApplicationDbContext _dbContext) => (dbContext) = (_dbContext);


    /// <summary>
    /// Adds a new entity of type <typeparamref name="TEntity"/> to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CreateAsync(TEntity entity) =>
        await dbContext.Set<TEntity>().AddAsync(entity);

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/> from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of all entities.</returns>
    public async Task<List<TEntity>> GetAllAsync() =>
        await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

    /// <summary>
    /// Retrieves an entity by its identifier, using the specified expression to filter the results.
    /// </summary>
    /// <param name="expression">An expression used to filter the entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>
    public async Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>> expression) =>
        await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);

    /// <summary>
    /// Releases the resources used by the <see cref="BaseRepository{TEntity}"/> instance.
    /// </summary>
    public void Dispose() => dbContext.Dispose();
}
