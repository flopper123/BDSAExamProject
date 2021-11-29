using System.ComponentModel.DataAnnotations;

namespace LitExplore.Core
{
    public record ReferenceDto(string? title);


    public record ReferenceCreateDto
    {
        [Required] public string? Title { get; init; }


        public override string ToString()
        {
            return $"ReferenceDto @\"{Title}\"";
        }
    }
}