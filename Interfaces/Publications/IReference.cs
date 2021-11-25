namespace Interfaces;
public interface IReference : IDable
{
    (IPublication, IPublication) publications { get; set; }

}
