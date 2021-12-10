namespace LitExplore.Entity.Filter;

/// <summary>
/// Case-Insensitive contains search Title of a PublicationDto
/// </summary>
public class TitleFilter : FilterDecorator<PublicationDto> {

    public TitleFilter(string key) : this(key, null) { }
    
    public TitleFilter(string key, Filter<PublicationDto>? _prv) 
        : base(dto => dto.Title.Contains(key, StringComparison.OrdinalIgnoreCase), key, _prv) {
    }
}