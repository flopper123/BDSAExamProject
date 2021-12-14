namespace LitExplore.Entity.Context;

public class LitExploreContext : DbContext, ILitExploreContext
{
    public DbSet<Publication> Publications => Set<Publication>();
    public DbSet<UserFilter> History => Set<UserFilter>();

    public LitExploreContext(DbContextOptions<LitExploreContext> options) : base(options) { }

    // TO:DO consider cleaning
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserFilter>().HasKey(f => f.UserId);
        builder.Entity<UserFilter>().Property(f => f.UserId).IsRequired();
        builder.Entity<UserFilter>().Property(f => f.Serialization).IsRequired();

        //All the other properties are required aswell bbut through data anotations.
        builder.Entity<Publication>().HasKey(p => p.Title);
        builder.Entity<Publication>().Property(p => p.Title).IsRequired();
        builder.Entity<Publication>().HasMany<KeyWord>(k => k.Keywords);
        builder.Entity<Publication>().HasMany<PublicationTitle>(t => t.References);
        builder.Entity<KeyWord>().ToTable("KeyWords");
        
        builder.Entity<PublicationTitle>().HasKey(t => t.Title);
        /*
        builder.Entity<Publication>()
            .HasMany<Reference>(p => p.References)
            .WithMany(r => r.Publications);
        */
        
        builder.Entity<Publication>()
            .HasIndex(t => t.Title)
            .IsUnique();

        //builder.Entity<Publication>().HasMany<Reference>(r=>r.References);
    }
}



