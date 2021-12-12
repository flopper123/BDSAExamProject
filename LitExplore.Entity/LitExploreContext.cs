namespace LitExplore.Entity;

using LitExplore.Entity.Filter;

public class LitExploreContext : DbContext, ILitExploreContext
{
    public DbSet<Reference> References => Set<Reference>();
    public DbSet<Publication> Publications => Set<Publication>();
    public DbSet<UserFilter> History => Set<UserFilter>();

    public LitExploreContext(DbContextOptions<LitExploreContext> options) : base(options) { }

    // TO:DO consider cleaning
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserFilter>().HasKey(f => f.UserId);
        builder.Entity<UserFilter>().Property(f => f.UserId).IsRequired();
        builder.Entity<UserFilter>().Property(f => f.Serialization).IsRequired();

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