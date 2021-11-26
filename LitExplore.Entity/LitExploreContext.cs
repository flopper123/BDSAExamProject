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

     
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets("9c0d427e-d138-4993-8a77-66fee59e666f")
            .Build();
            
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("LitExplore"));
    }
    

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