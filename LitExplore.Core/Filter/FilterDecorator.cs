namespace LitExplore.Core.Filter;

using System.Text;
using static LitExplore.Core.Filter.FilterPArgField;

/// <summary>
/// Auxiliary class for chaining together different filters.
/// Any new filter should extend this class, and send the desired 
/// predicate to the base constructor.
/// 
/// In design-pattern lingo this class is the abs Decorator component,
/// in a textbook Decorator-Pattern.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class FilterDecorator<T> : Filter<T>
{

    protected Filter<T> prv;
    protected readonly UInt32 _depth;

    // args for predicate
    // null if no args
    protected dynamic? p_args;

    // If prv is null, this is the last filter in the chain
    public FilterDecorator() : this(null, null) { }

    // If prv is null, this is the last filter in the chain
    public FilterDecorator(dynamic? args, Filter<T>? prv_ = null) : base()
    {
        p_args = args;
        prv = prv_ ?? EmptyFilter<T>.Get();
        this._depth = base.Depth + prv.Depth;
    }

    /// <summary>
    /// How many filters this decoration applies in total. Should be same as 
    /// count of enumerable returned by GetHistory(). 
    /// </summary>
    public override UInt32 Depth { get { return _depth; } }

    // Should be overwriten for decorators with multiple args
    protected virtual Object[] PredicateArgs
    {
        get 
        { 
            if (this.p_args != null) return new Object[] { this.p_args };
            return new Object[] {}; 
        }
    }

    // Chains this object to snd by constructing a new filter that applies in that order.
    public override Filter<T> Decorate(Filter<T> snd)
    {
        FilterDecorator<T>? sDecoration = snd as FilterDecorator<T>;
        if (sDecoration == null) {
            throw new ArgumentException($"Second argument {snd.GetType()} must be some FilterDecorator<>");
        }

        var sPArgs = sDecoration.PredicateArgs;
        Object[] pargs = new Object[sPArgs.Length + 1];
        for (int i = 0; i < pargs.Length - 1; i++) { pargs[i] = sPArgs[i]; }
        pargs[pargs.Length - 1] = this;

        string? fName = sDecoration.GetType().FullName;
        if (fName == null) { throw new ArgumentException($"FullName of type {snd.GetType()} is null"); }

        return FilterFactory.Create<T>(fName, pargs);
    }

    /// <summary>
    /// Returns an IEnumerable containing the entire filter history span of this
    /// Filter. The ordering of the filters corresponds to the ordering applied
    /// by this Filter. Any end of filter (i.e. EmptyFilter) is excluded.
    /// 
    /// [First element] : first of prv_filter
    /// [Last element] : this filter predicate
    /// </summary>
    /// <returns>  </returns>
    public override IEnumerable<Filter<T>> GetHistory()
    {
        foreach (Filter<T> prv_filter in prv.GetHistory())
        {
            if (prv_filter.GetType() != typeof(EmptyFilter<T>)) {
                yield return prv_filter;
            }
        }
        foreach (Filter<T> filter in base.GetHistory())
        {
            if (filter.GetType() != typeof(EmptyFilter<T>)) {
                yield return filter;
            }
        }
    }

    public override void Invoke(T v)
    {
        prv.Invoke(v);
        this.Action(v);
    }

    protected abstract IEnumerable<(string type, string str_vals)> getPArgsStringTuple();

    public override string SerializePArgs()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(FilterField.START);
        foreach ((string type, string val) in getPArgsStringTuple())
        {
            sb.Append($"{LINE_START}{TYPE}{VALUE_SEPERATOR}{type}");
            sb.Append(FIELD_SEPERATOR);
            sb.Append($"{VALUE}{VALUE_SEPERATOR}{val}{LINE_END}");
            sb.Append(PARG_SEPERATOR);
        }
        // Remove P_ARG seperator for last element
        sb[sb.Length - 1] = FilterField.END[0];
        return sb.ToString();
    }
}