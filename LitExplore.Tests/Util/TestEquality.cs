namespace LitExplore.Tests.Util;

public static class PublicationAssertionExtension {
    public static bool CustomEquals(this PublicationDtoDetails t, PublicationDtoDetails o)
    {
        if (!(t as PublicationDto).CustomEquals(o as PublicationDto)) return false;
        if (!t.Author.Equals(o.Author)) return false;
        if (!t.Time.Day.Equals(o.Time.Day)) return false;
        if (!t.Time.Month.Equals(o.Time.Month)) return false;
        if (!t.Time.Year.Equals(o.Time.Year)) return false;
        if (!t.Abstract.Equals(o.Abstract)) return false;
        if (t.Keywords.Count != o.Keywords.Count) return false;
        
        // use foreach and contains to ignore ordering
        foreach (String keyword in t.Keywords) if (!o.Keywords.Contains(keyword)) return false;
        return true;
    }

    public static bool CustomEquals(this PublicationDto this_, PublicationDto other)
    {
        if (this_ == null && other == null) return true;
        if (this_ == null || other == null) return false;

        if (!(this_ as PublicationDtoTitle).Title
            .Equals((other as PublicationDtoTitle).Title))
        {
            return false;
        }

        if (!other.References.AsEnumerable()
                             .GetEnumerator()
            .CustomEquals(this_.References.AsEnumerable()
                                          .GetEnumerator())) 
        {
            return false;
        }
        return true;
    }

    
    public static bool CustomEquals(this PublicationNode this_, PublicationNode other) {
        if (!other.Children.SequenceEqual(this_.Children)) return false; 
        if (!other.Parents.SequenceEqual(this_.Parents)) return false;
        return other.Details.CustomEquals(this_.Details);
    }

    public static bool CustomEquals(this NodeDetails<PublicationNode> this_, 
                                    NodeDetails<PublicationNode> other)
    {
        if (!this_.Depth.Equals(other.Depth)) return false;
        var tDetails = this_.Details;
        var oDetails = other.Details;
        if (tDetails == null && oDetails == null) return true;
        if (tDetails == null) return false;
        if (oDetails == null) return false;
        return tDetails.CustomEquals(oDetails);
    }


    public static bool CustomEquals<T> (this IEnumerator<T> fst, IEnumerator<T> snd) {

        while (true) 
        {
            bool oHasNext = snd.MoveNext();
            bool tHasNext = fst.MoveNext();

            if (!oHasNext && !tHasNext) return true;
            if (oHasNext ^ tHasNext) return false;
            if (snd.Current == null || fst.Current == null)
            {
                if (snd.Current == null && fst.Current == null) continue;
                return false;
            }
            if (!(snd.Current.Equals(fst.Current))) return false;
        }
    }
}