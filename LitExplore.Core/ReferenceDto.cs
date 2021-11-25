using System.ComponentModel.DataAnnotations;

namespace LitExplore.Core;

public record ReferenceDto
{
    public int Id { get; init; }

    [Required] public string Title { get; init; } = null!;
}