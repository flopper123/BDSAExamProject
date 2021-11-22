public interface IPublication : IDable
{
    public IEnumarable<IReference> GetRefs();
}