namespace LitExplore.Entity.Filter;

using System.Reflection;

// Entity equivalent of FilterDto
public class Filter {
    int UserId {get; init;}
    public string Serialization { get; init; } = null!;
}