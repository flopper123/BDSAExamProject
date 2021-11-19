
public interface IFilterHistory : IStack<IFilter>, IDable
{
    public IFilter Pop();
    public bool Push(IFilter filter);
    public IStack<IFilter> Resest();
    public IFilter Peek();
    public IEnumarable<IFilter> ToArray();
}