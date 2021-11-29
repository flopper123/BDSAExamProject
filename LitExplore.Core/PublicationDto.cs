using System.ComponentModel.DataAnnotations;

namespace LitExplore.Core
{
public record PublicationDto(
    string? Title, string? Author, 
    int? Year, string? Publisher, 
    int? Pages, int? Edition, ISet<ReferenceDto> References);


public record PublicationCreateDto
{
    [Required] public string? Title { get; init; }
    
    [StringLength(128)]
    public string? Author { get; init; }
    
    [Range(500, 2200)]
    public int? Year { get; init; }

    // public PublicationType? Type { get; init; }?? The Type of a Publication [Article, Book, Online/Link, Journal, etc..]? 
    
    [StringLength(256)]
    public string? Publisher { get; init; }
    
    [Range(0, Int32.MaxValue)]
    public int? Pages { get; init; } = 0;
    
    public int? Edition { get; init; } = 1;

    public ISet<ReferenceDto> References { get; init; } = null!;

}

public record PublicationUpdateDto : PublicationCreateDto
{
    public int Id { get; init; }
}
}