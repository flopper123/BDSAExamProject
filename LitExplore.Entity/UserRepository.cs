using Interfaces;

public class UserRepository: IRepository<IUser>
{
    public ICollection<IUser> UserCollection { get; set; }

    public bool Create(IUser Created)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IUser Read(int Id)
    {
        throw new NotImplementedException();
    }

    public bool Update(IUser Updated)
    {
        throw new NotImplementedException();
    }
}