using Microsoft.Extensions.Configuration;

namespace LitExplore.Entity
{
class Program {
    public static void Main(string[] args) {
        /*
            var config = new ConfigurationsBuilder()
            
                            .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
            string con_str = config.GetConnectionString("LitExplore");
        
            
        var connectionString = configuration.GetConnectionString("LitExplore");
            DbContextOptionsBuilder<LitExploreContext> opt = new DbContextOptionsBuilder<LitExploreContext>();
            opt.UseSqlServer(con_str);

            return new LitExploreContext(opt.Options);
        }
        var optionsBuilder = new DbContextOptionsBuilder<FuturamaContext>().UseSqlServer(connectionString);
        using var context = new FuturamaContext(optionsBuilder.Options);
        FuturamaContextFactory.Seed(context);

        foreach (var character in context.Characters.Include(c => c.Actor).AsNoTracking())
        {
            Console.WriteLine(character);
        }
    }
    */
}
}
}