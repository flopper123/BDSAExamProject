using System.ComponentModel.DataAnnotations;

namespace LitExplore.Core;

public record ReferenceDto
{
    public string Title { get; init; } = null!;
}

public record ReferenceCreateDto
{
    [Key]
    [Required]
    public string Title { get; init; } = null!;


    public override string ToString()
    {
        return $"ReferenceDto @\"{Title}\"";
    }
}
