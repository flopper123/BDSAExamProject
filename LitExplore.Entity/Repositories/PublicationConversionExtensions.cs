namespace LitExplore.Entity.Repositories;

using LitExplore.Core.Publication;

internal static class PublicationConversionExtensions 
{
    // Publication extension
    internal static PublicationDtoDetails ConvertToDetails(this Publication p) {
        return new PublicationDtoDetails
        {
            Title = p.Title,
            Author = p.Author,
            Abstract = p.Abstract,
            References = p.References.Select(r => new PublicationDtoTitle { Title = r.Title })
                                     .ToHashSet(),
            Time = p.Time,
            Keywords = p.Keywords.Select(k=> k.Keyword).ToList().AsReadOnly(), //? Does this continue or stops at one item??
        };
    }

    // Publication extension
    internal static Publication ConvertToDB(this PublicationDtoTitle key) {
        return new Publication
        {
            Title = key.Title,
        };
    }

    internal static Publication ConvertToPublication(this PublicationDtoDetails p)
    {
        return new Publication
        {
            Title = p.Title,
            Author = p.Author,
            Abstract = p.Abstract,
            References = p.References.Select(r => new PublicationTitle{Title = r.Title}).ToList(),
            Time = p.Time,
            Keywords = p.Keywords.Select(k => new KeyWord{Keyword = k}).ToList(), //? This damn EF..
        };
    }
}