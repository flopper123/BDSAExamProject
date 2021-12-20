using Microsoft.Extensions.Configuration;

namespace LitExplore.Entity;

/// <summary>
///     Auxiliary class used for time based operations such as migrations. 
///     The class initializes an SQL server connection using user secrets and a docker sql server. 
/// </summary>
public class LitExploreContextFactory : IDesignTimeDbContextFactory<LitExploreContext>
{
    // TO:DO check function
    public LitExploreContext CreateDbContext(string[] args) => new LitExploreContext(GetOptions().Options);

    public static DbContextOptionsBuilder<LitExploreContext> GetOptions()
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets<LitExploreContextFactory>()
                                                      .Build();
                    
        var optionsBuilder = new DbContextOptionsBuilder<LitExploreContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("LitExplore"), opts => opts.EnableRetryOnFailure());

        return optionsBuilder;
    }
}