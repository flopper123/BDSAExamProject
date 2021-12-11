using LitExplore.Entity.Filter;
using Microsoft.EntityFrameworkCore;

namespace LitExplore.Entity
{
  public interface ILitExploreContext : IDisposable
  {
    DbSet<Reference> References { get; }
    DbSet<Publication> Publications { get; }

    DbSet<UserFilter> History {get;}
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  }
}