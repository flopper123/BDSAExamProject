using System.ComponentModel.DataAnnotations;

namespace LitExplore.Core;

public record PublicationDto
{
    public string Title { get; init; } = null!; // The null! tells the compiler that this wont ever be null.
    public ISet<ReferenceDto> References { get; init; } = null!; // Should be empty not null if references is empty
}

public record PublicationCreateDto
{
    [Required]
    public string Title { get; init; } = null!;

    public ISet<ReferenceDto> References { get; init; } = null!;
}

public record PublicationUpdateDto : PublicationCreateDto
{
    public int Id { get; init; }
}