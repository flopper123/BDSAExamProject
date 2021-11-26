using System.ComponentModel.DataAnnotations;


namespace LitExplore.Entity;

public class Publication
{
    [Required]
    [Key] // This actually tells that this is the key and not Id.
    public string Title { get; set; } = null!;

    [StringLength(128)]
    public string? Author { get; set; }
    [Range(500, 2200)]
    public int? Year { get; set; }
    // PublicationType
    public PublicationType? Type { get; set; }//?? The Type of a Publication [Article, Book, Online/Link, Journal, etc..]? 
    [StringLength(256)]
    public string? Publisher { get; set; }
    [Range(0, Int32.MaxValue)]
    public int Pages { get; set; } = 0;
    public int Edition { get; set; } = 1;

    [Required]
    public ICollection<Reference> References { get; set; } = null!;

}