namespace Interfaces;
public interface IUser : IDable
{
    public Role GetRole();
    public IFilterHistory GetFilterHistory();
}
