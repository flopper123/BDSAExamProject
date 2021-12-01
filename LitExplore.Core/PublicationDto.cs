using System.ComponentModel.DataAnnotations;

namespace LitExplore.Core
{
    public record PublicationDto
    {
        public string Title { get; init; } = null!; // The null! tells the compiler that this wont ever be null.
        public string? Author { get; init; }
        public int? Year { get; init; }
        public string? Publisher { get; init; }
        
        public int? Pages { get; init; } 
        public int? Edition { get; init; }
        public ISet<ReferenceDto> References { get; init; }
    }


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