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
            References = p.References.Select(r => new PublicationTitle { Title = r })
                                     .ToHashSet<PublicationTitle>(),
            Time = p.Time,
            Keywords = p.Keywords.ToList().AsReadOnly(),
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
            References = p.References.Select(r => r.Title).ToList(),
            Time = p.Time,
            Keywords = p.Keywords.ToList(),
        };
    }
}