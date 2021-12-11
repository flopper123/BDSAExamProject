namespace LitExplore.Entity;

using LitExplore.Entity.Filter;

public interface ILitExploreContext : IDisposable
{
    DbSet<Reference> References { get; }
    DbSet<Publication> Publications { get; }
    DbSet<UserFilter> History { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}