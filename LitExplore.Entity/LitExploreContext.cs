
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class LitExploreContext : DbContext, ILitExploreContext
{

    public DbSet<Reference> References => throw new NotImplementedException();

    public DbSet<Publication> Publications => throw new NotImplementedException();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }
}