
namespace Interfaces;
public interface IFilterHistory : IDable
{
    public IFilter Pop();
    public bool Push(IFilter filter);
    public Stack<IFilter> Resest();
    public IFilter Peek();
    public IEnumerable<IFilter> ToArray();
}