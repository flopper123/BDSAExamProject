namespace Interfaces;
public interface IPublication : IDable
{
    public IEnumerable<IReference> GetRefs();
}