
namespace LitExplore.Entity;

/// <summary>
/// Auxiliary class to generalize some of the repository code. 
/// AbsRepositoryTest and others utilize this class.
/// </summary>
public abstract class AbsRepository : IDisposable
{
    protected readonly ILitExploreContext _context;

    // Any extending classes must contain a matching constructor for tests
    // reflection (AbsRepositoryTest.cs) to work correct.
    public AbsRepository(ILitExploreContext context) => _context = context;

    public abstract void Dispose();
}