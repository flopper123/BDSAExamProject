namespace LitExplore.Entity.Filter;

/// <summary>
/// Case-Insensitive contains search Title of a PublicationDto
/// </summary>
public class TitleFilter : FilterDecorator<PublicationDto> {

    public TitleFilter(string name) : this(name, null) { }
    
    public TitleFilter(string name, Filter<PublicationDto>? _prv) 
        : base(dto => dto.Title.Contains(name, StringComparison.OrdinalIgnoreCase), _prv) {}

    public override EFilter GetId() {
        return EFilter.PUB_STR_TITLE_CONTAINS;
    }
}