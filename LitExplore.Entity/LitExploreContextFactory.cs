using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LitExplore.Entity
{
    public class LitExploreContextFactory : IDesignTimeDbContextFactory<LitExploreContext>
    {
        public LitExploreContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets("9c0d427e-d138-4993-8a77-66fee59e666f")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<LitExploreContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("LitExplore"));

            return new LitExploreContext(optionsBuilder.Options);
        }
    }
}