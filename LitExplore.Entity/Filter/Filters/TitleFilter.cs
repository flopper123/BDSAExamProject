namespace LitExplore.Entity.Filter;

public class TitleFilter : FilterDecorator<PublicationDto> {

    public TitleFilter(string name) : this(name, null) { }

    public TitleFilter(string name, Filter<PublicationDto>? _prv) 
        : base(dto => dto.Title.Contains(name), _prv) {}
}