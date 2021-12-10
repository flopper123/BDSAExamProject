namespace LitExplore.Entity.Filter;

using System.Reflection;

// Entity equivalent of FilterDto
public class Filter {
    int userId {get; set;}
    public string filterJson { get; set; } = null!;
}