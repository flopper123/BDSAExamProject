namespace LitExplore.Entity.Filter;

/// <summary>
/// The FilterType denotes what type of input the filter operates on,
/// what input the filter expects, what field the filter checks, and 
/// what operation that checks it.
/// 
/// For now we use 12 bit for each anotation
/// </summary>
/// <example> 
/// MinRefSize(uint64 n) :  
/// Example of naming convention and values for: PUB_UINT64_REF_MINSIZE
/// PUB       - the filter operates on a PublicationDto
/// UINT64    - the filter expects uint64 input
/// REF       - the filter checks the Reference field of the PublicationDto   
/// MINSIZE   - The filter checks the minimum size of the reference field.
/// </example>
/// ! Warning specific naming convention expected:
/// ! NAME: TYPE_INPUT_FIELD_OPERATION
/// ! Enum numbers should be generated by BINARY_OR operations to avoid errors
/// ! No enum defined for Filter_Operation denotations as it can vary greatly.
[Flags]
public enum EFilter : UInt64
{
    NONE = 0,
 
    /* Publication type */
    PUB                      = FilterType.PUB,
    PUB_UINT64               =    FilterInput.UINT64 | PUB,
    PUB_UINT64_REF           =       FilterField.REF | PUB_UINT64,
    PUB_UINT64_REF_MINSIZE   =                   0b1 | PUB_UINT64_REF,
    PUB_UINT64_REF_MAXSIZE   =                  0b10 | PUB_UINT64_REF,
    PUB_UINT64_REF_MINDEPT   =                 0b100 | PUB_UINT64_REF,
    PUB_UINT64_REF_MAXDEPT   =                0b1000 | PUB_UINT64_REF,
    PUB_UINT64_TITLE         =     FilterField.TITLE | PUB_UINT64,
    PUB_UINT64_TITLE_MINSIZE =                   0b1 | PUB_UINT64_TITLE,
    PUB_UINT64_TITLE_MAXSIZE =                  0b10 | PUB_UINT64_TITLE,

    // Publication with str input
    PUB_STR                  =       FilterInput.STR | PUB,
    PUB_STR_TITLE            =     FilterField.TITLE | PUB_STR,
    PUB_STR_TITLE_CONTAINS   =                   0b1 | PUB_STR_TITLE,
    PUB_STR_TITLE_EQUALS     =                  0b10 | PUB_STR_TITLE,

    PUB_STR_AUTHOR           =    FilterField.AUTHOR | PUB_STR,

    /* Article type */
    ART_NONE_NONE_NONE       = FilterType.ART,
}

/* The following enums contain a mask denoting which bits they flag for */
[Flags]
public enum FilterType : UInt64
{
    NONE = 0x000_000_000_000,
    
    PUB = 0x001_000_000_000,
    
    // Article
    ART = 0x002_000_000_000,
    
    MASK = 0xFFF_000_000_000,
}

[Flags]
public enum FilterField : UInt64 {
    NONE = 0x000_000,
    
    TITLE = 0x001_000,
    REF= 0x002_000,
    AUTHOR = 0x004_000,
    
    MASK = 0xFFF_000,
}

[Flags]
public enum FilterInput : UInt64
{
    NONE = 0x000_000_000,
    
    UINT64 = 0x001_000_000,
    STR = 0x002_000_000,

    MASK = 0xFFF_000_000,
    // Add other inputs
}