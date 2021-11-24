using Microsoft.EntityFrameworkCore;
public interface ILitExploreContext : IDisposable
{
    DbSet<Reference> References { get; }
    DbSet<Publication> Publications { get; }
    int SaveChanges();
}