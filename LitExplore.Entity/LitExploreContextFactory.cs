using Microsoft.Extensions.Configuration;

namespace LitExplore.Entity;


/// <summary>
///     Auxiliary class used for time based operations such as migrations. 
///     The class initializes an SQL server connection where using user secrets and a docker sql server. 
/// </summary>
public class LitExploreContextFactory : IDesignTimeDbContextFactory<LitExploreContext>
{
    // TO:DO check function
    public LitExploreContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets("9c0d427e-d138-4993-8a77-66fee59e666f")
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<LitExploreContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("LitExplore"));
            
        return new LitExploreContext(optionsBuilder.Options);
    }
}