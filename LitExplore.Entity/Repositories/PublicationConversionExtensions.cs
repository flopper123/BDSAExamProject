namespace LitExplore.Entity.Repositories;

using LitExplore.Core.Publication;

internal static class PublicationConversionExtensions 
{
    // Publication extension
    internal static PublicationDetails ConvertToDetails(this Publication p) {
        return new PublicationDetails
        {
            Title = p.Title,
            Author = p.Author,
            Abstract = p.Abstract,
            References = p.References.Select(r => new PublicationTitle { Title = r.Title })
                                     .ToHashSet<PublicationTitle>(),
            Time = p.Time,
            Keywords = p.Keywords.Select(k=> k.Keyword).ToList().AsReadOnly(), //? Does this continue or stops at one item??
        };
    }

    // Publication extension
    internal static Publication ConvertToDB(this PublicationTitle key) {
        return new Publication
        {
            Title = key.Title,
        };
    }

    internal static Publication ConvertToPublication(this PublicationDetails p)
    {
        return new Publication
        {
            Title = p.Title,
            Author = p.Author,
            Abstract = p.Abstract,
            //! Either Have different naming or else it have to be concrete namespaced.
            References = p.References.Select(r => new LitExplore.Entity.Entities.PublicationTitle{Title = r.Title}).ToList(),
            Time = p.Time,
            Keywords = p.Keywords.Select(k => new KeyWord{Keyword = k}).ToList(), //? This damn EF..
        };
    }
}