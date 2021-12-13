namespace LitExplore.Core.Publication;

public record PublicationTitle
{
    [Key]
    [Required]
    public string Title {get; init;} =null!; // The null! tells the compiler that this wont ever be null.
}

public record PublicationDto : PublicationTitle
{
    public ISet<PublicationTitle> References { get; init; } = new HashSet<PublicationTitle>(); // Should be empty not null if references is empty
}

public record PublicationDetails : PublicationDto
{
    public string Author { get; init; } = "?";

    // Holds information about time of publication (year, month, day)
    public DateTime Time {get; init;} = DateTime.Now;

    public string Abstract { get; init; } = "?";

    public IReadOnlyCollection<String> Keywords {get; init;} = new List<String>().AsReadOnly();
}