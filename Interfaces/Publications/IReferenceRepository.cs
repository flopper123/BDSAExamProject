namespace Interfaces;
public interface IReferenceRepository
{
    public IPublication[] GetReferencedPublications(IPublication publication);
}