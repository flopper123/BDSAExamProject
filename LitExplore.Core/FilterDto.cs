namespace LitExplore.Core;

using System.ComponentModel.DataAnnotations;

public record FilterDto
{
    public IList<(string name, Object?[] args)> History { get; init; } = null!;
}