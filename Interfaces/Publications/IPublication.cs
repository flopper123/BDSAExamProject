namespace Interfaces;
public interface IPublication : IDable
{
    public string Title {get; set; }
    public IEnumerable<IReference> GetRefs();
}