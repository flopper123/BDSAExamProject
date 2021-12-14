namespace LitExplore.Core.Publication;

public record PublicationDtoTitle
{
    [Key]
    [Required]
    public string Title {get; init;} =null!; // The null! tells the compiler that this wont ever be null.
}

public record PublicationDto : PublicationDtoTitle
{
    public ISet<PublicationDtoTitle> References { get; init; } = new HashSet<PublicationDtoTitle>(); // Should be empty not null if references is empty
}

public record PublicationDtoDetails : PublicationDto
{
    public string Author { get; init; } = "?";

    // Holds information about time of publication (year, month, day)
    public DateTime Time {get; init;} = DateTime.Now;

    public string Abstract { get; init; } = "?";

    public IReadOnlyCollection<String> Keywords {get; init;} = new List<String>().AsReadOnly();
}