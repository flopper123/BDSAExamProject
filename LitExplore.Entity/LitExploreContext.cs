using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class LitExploreContext : DbContext, ILitExploreContext
{
    public DbSet<Reference> References => Set<Reference>();
    public DbSet<Publication> Publications => Set<Publication>();
}