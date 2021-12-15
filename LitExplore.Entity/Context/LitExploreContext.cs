namespace LitExplore.Entity.Context;

public class LitExploreContext : DbContext, ILitExploreContext
{
    public DbSet<Publication> Publications => Set<Publication>();
    public DbSet<UserFilter> History => Set<UserFilter>();
    public DbSet<KeyWord> KeyWords => Set<KeyWord>();
    public DbSet<PublicationTitle> References => Set<PublicationTitle>();

    public LitExploreContext(DbContextOptions<LitExploreContext> options) : base(options) { }

    // TO:DO consider cleaning
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserFilter>().HasKey(f => f.UserId);
        builder.Entity<UserFilter>().Property(f => f.UserId).IsRequired();
        builder.Entity<UserFilter>().Property(f => f.Serialization).IsRequired();

        //All the other properties are required aswell but through data anotations.
        builder.Entity<Publication>().HasKey(p => p.Title);
        builder.Entity<Publication>().Property(p => p.Title).IsRequired();

        // modelBuilder.Entity<Student>()
        //     .HasOne<Grade>(s => s.Grade)
        //     .WithMany(g => g.Students)
        //     .HasForeignKey(s => s.CurrentGradeId);


        // builder.Entity<Publication>().HasMany<KeyWord>(k => k.Keywords);
        // builder.Entity<Publication>().HasMany<PublicationTitle>(t => t.References);
        // builder.Entity<KeyWord>().ToTable("KeyWords");
        
        builder.Entity<PublicationTitle>().HasKey(t => t.Title);
        builder.Entity<KeyWord>().HasKey(k => k.Keyword);
        /*
        builder.Entity<Publication>()
            .HasMany<Reference>(p => p.References)
            .WithMany(r => r.Publications);
        */
        
        builder.Entity<Publication>()
            .HasIndex(t => t.Title)
            .IsUnique();
        base.OnModelCreating(builder);
        //builder.Entity<Publication>().HasMany<Reference>(r=>r.References);
    }
}



