namespace Interfaces;
public interface IClientController
{
    public Response Login(Credentials credentials); //Define Credentials
    public IFilterHistory getFilterHistory();
}