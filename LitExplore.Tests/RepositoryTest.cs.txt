
/* abstract testing of repository
abstract class RepositoryTest<T> implements IDisposable
    : where 
{
    abstract void seed(LitExploreContext ctx);


            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<LitExploreContext>();
            builder.UseSqlite(connection);
            var context = new LitExploreContext(builder.Options);
            seed(context);
            _context = context;
            _repository = new PublicationRepository(_context);
}
*/