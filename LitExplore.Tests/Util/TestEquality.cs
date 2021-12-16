namespace LitExplore.Tests.Util;

public static class PublicationAssertionExtension {
    public static bool CustomEquals(this PublicationDtoDetails this_, PublicationDtoDetails other)
    {
        if (!(this_ as PublicationDto).CustomEquals(other as PublicationDto)) {
            throw new Exception("PubDTO customEquals throwing");
            return false;
        }
        if (!this_.Author.Equals(other.Author))
        {
            throw new Exception($"AuthorEquals throwing Author1:{this_.Author} and Author2:{other.Author}");
            return false;
        }
        if (!this_.Time.Day.Equals(other.Time.Day))
        {
            throw new Exception("TimeEquals Day throwing");
            return false;
        }
        if (!this_.Time.Month.Equals(other.Time.Month)) {
            throw new Exception("TimeEquals Month throwing");
            return false;
        }
        if (!this_.Time.Year.Equals(other.Time.Year)) {
            throw new Exception("TimeEquals Year throwing");
            return false;
        }

        if (!this_.Abstract.Equals(other.Abstract))
        {
            throw new Exception("AbstractEquals throwing");
            return false;
        }
        if (this_.Keywords.Count != other.Keywords.Count)
        {
            throw new Exception("KeywordCount throwing");
            return false;
        }
        // use foreach and contains to ignore ordering
        foreach (String keyword in this_.Keywords)
        {
            if (!other.Keywords.Contains(keyword))
            {
                throw new Exception($"KeywordContains throwing \nExpected: {keyword} not found in others keywords");
                return false;
            }
        }
        return true;
    }

    public static bool CustomEquals(this PublicationDto this_, PublicationDto other)
    {
        if (this_ == null && other == null) return true;
        if (this_ == null || other == null) return false;

        if (!(this_ as PublicationDtoTitle).Title.Equals((other as PublicationDtoTitle).Title))
        {
            throw new Exception("DTOTitleThrowing");
            return false;
        }

        if (!other.References.AsEnumerable().GetEnumerator().CustomEquals(
            this_.References.AsEnumerable().GetEnumerator()
        )) {
            throw new Exception("WTF SET IS NOT EQUAL");
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
        String fst_msg = "First values: ";
        String snd_msg = "Second values: ";
        int i = 0;


        while (true) {
            bool oHasNext = snd.MoveNext();
            bool tHasNext = fst.MoveNext();
            i++;

            if (!oHasNext && !tHasNext) {
                break;
            }
            if (oHasNext ^ tHasNext) {
                throw new Exception($"#{i}\nOne has next, and other doesn't throwing,\nOne={snd.Current}, Other={fst.Current}");
                return false;
            }
            if (snd.Current == null || fst.Current == null) {
                if (snd.Current == null && fst.Current == null) continue;
                throw new Exception($"One is null throwing");
                return false;
            }
            if (!(snd.Current.Equals(fst.Current))) {
                throw new Exception($"snd: {snd.Current} doesn't equal fst: {fst.Current}");
                return false;
            }
        }
        return true;
    }
}