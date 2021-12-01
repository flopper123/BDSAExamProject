using System.Security.AccessControl;
using System.IO;
using System.Reflection;
using LitExplore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using static System.IO.Path;
using static System.IO.Directory;
namespace LitExplore.Entity;

public class LitExploreContext : DbContext, ILitExploreContext
{
    public DbSet<Reference> References => Set<Reference>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Publication> Publications => Set<Publication>();

    public LitExploreContext(DbContextOptions<LitExploreContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Publication>().HasKey(p => p.Title);
        builder.Entity<Publication>().Property(p => p.Title).IsRequired();

        builder.Entity<Publication>()
            .HasMany<Reference>(p => p.References)
            .WithMany(r => r.Publications);
        
        builder.Entity<Publication>()
            .HasIndex(t => t.Title)
            .IsUnique();
        builder.Entity<Reference>()
            .HasIndex(r => r.Title)
            .IsUnique();

        //builder.Entity<Publication>().HasMany<Reference>(r=>r.References);
    }
}