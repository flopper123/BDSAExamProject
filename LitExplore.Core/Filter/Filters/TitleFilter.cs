namespace LitExplore.Core.Filter;


/// <summary>
/// Case-Insensitive contains search Title of a PublicationDto
/// </summary>
[JsonObject]
public class TitleFilter : FilterDecorator<PublicationDto>
{
    public TitleFilter(string key) : this(key, null) { }
    
    [JsonConstructor]
    public TitleFilter(string key, Filter<PublicationDto>? _prv)
        : base(dto => dto.Title.Contains(key, StringComparison.OrdinalIgnoreCase), key, _prv)
    {
    }

    public override string P_Args_ToString() {
        return $"(type: System.String, value:{(p_args ?? "null").ToString()})";
    }
}


// FilterDto

//  Object
//  Need parser to translate
//  "THIS: (Name: TitleFilter, Args: "pony") 
//   HISTORY 1: (Name: KeywordMatch, Args: string[]{ "hello", "OK", }), 
//           2: (Name: MinRefsFilter, Args: string[] {})"



//  string[] obj = database.RunCSharp()
//
//
//
