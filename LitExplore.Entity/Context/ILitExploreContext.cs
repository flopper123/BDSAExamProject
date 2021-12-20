namespace LitExplore.Entity.Context;

public interface ILitExploreContext : IDisposable
{
  DbSet<Publication> Publications { get; }
  DbSet<UserFilter> History { get; }
  DbSet<KeyWord> KeyWords { get; }
  DbSet<PublicationTitle> References { get; }
  int SaveChanges();
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}