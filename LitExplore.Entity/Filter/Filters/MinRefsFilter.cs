namespace LitExplore.Entity.Filter;

public class MinRefsFilter : FilterDecorator<PublicationDto> {

    public MinRefsFilter(int min) : this(min, null) { }

    public MinRefsFilter(int min, Filter<PublicationDto>? _prv) 
        : base(dto => dto.References.Count >= min, _prv) {}
}