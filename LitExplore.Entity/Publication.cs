using System.ComponentModel.DataAnnotations;

namespace LitExplore.Entity;

public class Publication
{
    public int Id { get; set; }
    
    [Required]
    public string Title {get; set; } = "";
    [StringLength(128)]
    public string? Author { get; set; }
    [Range(500, 2200)]
    public int? Year { get; set; }
    public Type? Type { get; set; }//?? The Type of a Publication [Article, Book, Online/Link, Journal, etc..]? 
    [StringLength(256)]
    public string? Publisher { get; set; }
    [Range(0, Int32.MaxValue)]
    public int Pages { get; set; } = 0;
    public int Edition { get; set; } = 1;
    
    public ISet<Reference> References { get; init; } = new HashSet<Reference>{};

}