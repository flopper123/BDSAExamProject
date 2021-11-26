using System.ComponentModel.DataAnnotations;

namespace LitExplore.Core
{
    public record ReferenceDto
    {
        [Required] public string Title { get; init; } = null!;
    }
}