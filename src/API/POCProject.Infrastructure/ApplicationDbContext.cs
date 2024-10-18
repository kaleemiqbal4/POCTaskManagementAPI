using Microsoft.EntityFrameworkCore;
using POCProject.Entities.Entities;

namespace POCProject.Infrastructure;

/// <summary>
/// Represents the database context for the task management application.
/// This context provides access to the various entities used within the application,
/// enabling CRUD operations and enforcing data integrity.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class
    /// with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the collection of task entities in the database.
    /// </summary>
    public DbSet<TasksEntity> Tasks { get; set; }

    /// <summary>
    /// Gets or sets the collection of task column entities in the database.
    /// This allows for the organization of tasks into columns.
    /// </summary>
    public DbSet<TaskColumnEntity> TaskColumn { get; set; }

    /// <summary>
    /// Gets or sets the collection of task image entities in the database.
    /// This supports the management of images associated with tasks.
    /// </summary>
    public DbSet<TaskImageEntity> TaskImage { get; set; }

    /// <summary>
    /// Get User and Generate token
    /// </summary>
    public DbSet<UserEntity> Users { get; set; }

    /// <summary>
    /// Configures the model for the context, including entity relationships and constraints.
    /// </summary>
    /// <param name="modelBuilder">The builder used to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TaskColumnEntity>()
            .HasIndex(tc => tc.Name)
            .IsUnique();

        modelBuilder.Entity<TaskColumnEntity>()
            .HasIndex(tc => new { tc.SortOrder, tc.Name })
            .IsUnique();
    }
}

