namespace LitExplore.Core.Filter;

// FilterPredicateArgumentException
public class FilterPArgsException : Exception 
{
    private static string getMsg(string[] PARG_TYPES_STR, string callee) {

        StringBuilder msg = new StringBuilder();
        msg.Append("FilterPredicateArgumentException thrown for class {callee}:\n");
        msg.Append("Expected all arguments to be of types and order:");
        for (int i=0; i < PARG_TYPES_STR.Length; i++) {
            var s = PARG_TYPES_STR[i];
            msg.Append($"|#{i}| typeof({s})");
        }
        return msg.ToString();
    }

    public FilterPArgsException(string callee, string[] PARG_TYPES_STR) :
        base(getMsg(PARG_TYPES_STR, callee)) 
    {}


    public FilterPArgsException(Type callee, string[] PARG_TYPES_STR) : 
        this(callee.ToString(), PARG_TYPES_STR) 
    {}
}