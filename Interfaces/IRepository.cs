namespace Interfaces;
public interface IRepository<T> : IDisposable where T : IDable
{
    public bool Create(T Created);
    public T Read(int Id);
    public bool Update(T Updated);
    public bool Delete(int Id);
}
