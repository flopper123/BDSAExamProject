namespace LitExplore.Entity.Filter;

using System.Reflection;

// Entity equivalent of FilterDto
public class Filter {
    int UserId {get; set;}
    public string Serialization { get; set; } = null!;
}