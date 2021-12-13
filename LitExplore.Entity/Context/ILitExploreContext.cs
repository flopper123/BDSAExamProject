namespace LitExplore.Entity.Context;

public interface ILitExploreContext : IDisposable
{
  DbSet<Publication> Publications { get; }
  DbSet<UserFilter> History { get; }
  int SaveChanges();
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}