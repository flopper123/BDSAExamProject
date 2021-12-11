namespace LitExplore.Core.Filter.Filters;

public class MinRefsFilter : FilterDecorator<PublicationDto> {

    public MinRefsFilter(int min) : this(min, null) { }

    public MinRefsFilter(int min, Filter<PublicationDto>? _prv) 
        : base(dto => dto.References.Count >= min, min, _prv) {}
    
    protected override IEnumerable<(string type, string str_vals)> getPArgsStringTuple() {
        yield return ("System.Int", (p_args ?? 0).ToString());
    }
}