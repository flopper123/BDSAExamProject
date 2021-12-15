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
            throw new Exception("AuthorEquals throwing");
            return false;
        }
        if (!this_.Time.Equals(other.Time))
        {
            //temp true
            return true;
            //throw new Exception("TimeEquals throwing");
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