namespace Interfaces;
public interface IReference : IDable
{
    public (IPublication,IPublication) GetPublications();
}
