namespace LitExplore.Core.Filter;

using System.Text;

public abstract class Filter<T> {
    // The top-level predicate this filter applies.
    protected Predicate<T> predicate;

    // How many filters this filter applies
    public virtual UInt32 Depth {
        get { return 1u; }
        protected set {}
    }
    
    protected Filter(Predicate<T> predicate) {
        this.predicate = predicate;
    }

    public virtual string Serialize() {
        StringBuilder sb = new StringBuilder();
        sb.Append(FilterField.START);
        foreach (Filter<T> f in GetHistory()) {
            sb.Append(f.ToString());
            sb.Append(Environment.NewLine);
        }
        sb[sb.Length - 1] = FilterField.END[0];
        return sb.ToString();
    }


    /// <summary>
    /// Return an ordered ienumerable where the starting element is the first applied 
    /// filter in this sequence of filters.
    /// </summary>
    /// <returns> The filter history as an generic IEnumerable<Filter<T>> </returns>
    public virtual IEnumerable<Filter<T>> GetHistory() {{}
        yield return this;
    }

    /// <summary>
    /// Returns a serialization of the arguments given to implementing classes constructors.
    /// </summary>
    public virtual string SerializePArgs() { 
        return $"{FilterField.START}{FilterField.END}"; // Object[0] == null ??
    }

    /// <summary>
    /// Applies the predicate to the input @tar, and returns
    /// a subset of @tar. All elements in the returned subset,
    /// upholds the filter predicate. 
    /// </summary>
    /// <param name="tar"> Target of filter </param>
    /// <returns> An enumerable of all elements that uphold src predicate </returns>
    public virtual IEnumerable<T> Apply(IEnumerable<T> tar) {
        return tar.Where(v => predicate(v));
    }

    override public string ToString() {
        StringBuilder sb = new StringBuilder();

        sb.Append(FilterField.START);

        sb.Append($"{FilterField.NAME}{FilterField.VALUE_SEPERATOR}{this.GetType().FullName}");
        sb.Append(FilterField.FIELD_SEPERATOR);

        sb.Append($"{FilterField.DEPTH}{FilterField.VALUE_SEPERATOR}{this.Depth}");
        sb.Append(FilterField.FIELD_SEPERATOR);

        sb.Append($"{FilterField.P_ARGS}{FilterField.VALUE_SEPERATOR}{SerializePArgs()}");
        sb.Append(FilterField.END);

        return sb.ToString();
    }
}