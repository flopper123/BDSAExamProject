namespace LitExplore.Entity.Filter;

/// <summary>
/// Case-Insensitive contains search Title of a PublicationDto
/// </summary>
public class TitleFilter : FilterDecorator<PublicationDto> {

    string _key;

    // search key
    public string Key {
        get { return _key; }
    }
    
    public static EFilter Id = EFilter.PUB_STR_TITLE_CONTAINS; 
    
    public TitleFilter(string key) : this(key, null) { }
    
    public TitleFilter(string key, Filter<PublicationDto>? _prv) 
        : base(dto => dto.Title.Contains(key, StringComparison.OrdinalIgnoreCase), _prv) {
        _key = key;
    }

    public override EFilter GetId() {
        return EFilter.PUB_STR_TITLE_CONTAINS;
    }
    
}