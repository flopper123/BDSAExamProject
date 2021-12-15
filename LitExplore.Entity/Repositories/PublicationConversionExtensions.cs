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

    internal static void UpdatePublication(this Publication old, Publication p_new)
    {
        //! Destructive update not taking into account for Refernces and Keywords already present.
        //! If not in p_new it is assumed that it is on purpose.
        old.Title = p_new.Title;
        old.Author = p_new.Author;
        old.Abstract = p_new.Abstract;
        old.References = p_new.References;
        old.Time = p_new.Time;
        old.Keywords =p_new.Keywords;
    }
}