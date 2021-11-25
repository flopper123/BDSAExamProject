using Interfaces;

namespace LitExplore.Entity;

public class UserRepository: IRepository<IUser>
{
    public ICollection<IUser> UserCollection { get; set; } //??

    public bool Create(IUser created)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IUser Read(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(IUser updated)
    {
        throw new NotImplementedException();
    }
}