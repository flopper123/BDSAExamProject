using Microsoft.EntityFrameworkCore;

namespace LitExplore.Entity;

public class LitExploreContext : DbContext, ILitExploreContext
{

    public DbSet<Reference> References => Set<Reference>();

    public DbSet<Publication> Publications => Set<Publication>();

    public LitExploreContext(DbContextOptions<LitExploreContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Publication>()
            .HasIndex(t => t.Title)
            .IsUnique();
        builder.Entity<Reference>()
            .HasIndex(r => r.Title)
            .IsUnique();
    }
}