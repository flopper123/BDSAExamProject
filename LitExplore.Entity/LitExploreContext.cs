namespace LitExplore.Entity;

using LitExplore.Entity.Filter;

public class LitExploreContext : DbContext, ILitExploreContext
{
    public DbSet<Reference> References => Set<Reference>();
    public DbSet<Publication> Publications => Set<Publication>();

    public DbSet<LitExplore.Entity.Filter.Filter> History => Set<LitExplore.Entity.Filter.Filter>();

    public LitExploreContext(DbContextOptions<LitExploreContext> options) : base(options) { }

    // TO:DO consider cleaning
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<LitExplore.Entity.Filter.Filter>().HasKey(f => f.UserId);
        builder.Entity<LitExplore.Entity.Filter.Filter>().Property(f => f.UserId).IsRequired();
        builder.Entity<LitExplore.Entity.Filter.Filter>().Property(f => f.Serialization).IsRequired();

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