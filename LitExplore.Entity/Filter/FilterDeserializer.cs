namespace LitExplore.Entity.Filter;

public enum FilterField : string
{
    NAME = "name",
    DEPTH = "depth",
    P_ARGS = "p_args",
}   

public enum FilterArgField : string {
    TYPE = "type",
    VALUE ="value",
}

/// TO:DO Implement functionality to deserialize object
public class FilterDeserializer {
    public FilterDeserializer() {
        // empty for now
    }
}