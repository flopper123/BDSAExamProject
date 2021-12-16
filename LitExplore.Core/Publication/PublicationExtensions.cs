namespace LitExplore.Core.Publication;

internal static class NodeExtensions {
    internal static bool ContainsTitle(this List<PublicationNode> nodes, PublicationDtoTitle title)
    {
        foreach (PublicationNode n in nodes) {
            if (n.Equals(title)) return true;
        }
        return false;
    }

    // unused
    internal static PublicationDtoDetails ToDetails(this PublicationDtoTitle target)
    {
        Type t = target.GetType();

        if (t == typeof(PublicationDtoDetails)) {
            return (PublicationDtoDetails)target;
        
        } else if (t == typeof(PublicationDto)) {
            PublicationDto? asDto = (target as PublicationDto);
            return new PublicationDtoDetails
            {
                Title = (asDto = null!).Title,
                References = (asDto = null!).References
            };

        } else return new PublicationDtoDetails { Title = target.Title };
    }
}