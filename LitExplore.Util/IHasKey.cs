namespace LitExplore.Util;

public interface IHasKey<T>
{
    T Key
    {
        get;
        init;
    }
}
