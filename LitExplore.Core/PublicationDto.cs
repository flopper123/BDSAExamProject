using System.ComponentModel.DataAnnotations;

namespace LitExplore.Core;

public record PublicationDto(int Id, 
    string Title, string? Author, 
    int? Year, Type? Type, string? Publisher, 
    int Pages, int Edition );

public record PublicationCreateDto
{
    [Required] public string Title { get; init; } = "No Title";
    
    [StringLength(128)]
    public string? Author { get; init; }
    
    [Range(500, 2200)]
    public int? Year { get; init; }

    public Type? Type { get; init; }//?? The Type of a Publication [Article, Book, Online/Link, Journal, etc..]? 
    
    [StringLength(256)]
    public string? Publisher { get; init; }
    
    [Range(0, Int32.MaxValue)]
    public int Pages { get; init; } = 0;
    
    public int Edition { get; init; } = 1;

    public ISet<ReferenceDto> References { get; init; } = null!;

}