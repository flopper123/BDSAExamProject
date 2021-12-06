namespace LitExplore.Tests.Entity.Filter;

/// <summary>
/// Contains tests for EFilter and corresponding sub enum types:
/// FilterField, FilterInput, FilterType
/// </summary>
public class FilterEnumTests {
    IEnumerable<FilterField> field_flags = Enum.GetValues(typeof(FilterField)).Cast<FilterField>();
    IEnumerable<FilterInput> input_flags = Enum.GetValues(typeof(FilterInput)).Cast<FilterInput>();
    IEnumerable<FilterType> type_flags = Enum.GetValues(typeof(FilterType)).Cast<FilterType>();
    
    /// Cant make reduce methods generic due to generic enum constraints
    private UInt64 ReduceField(IEnumerable<FilterField> enums) { 
        UInt64 ret = 0UL;
        foreach (FilterField f in enums) {
            if (f != FilterField.MASK) {
                ret |= (UInt64) f;
            }
        }
        return ret;
    } 

    private UInt64 ReduceInput(IEnumerable<FilterInput> enums) { 
        UInt64 ret = 0UL;
        foreach (FilterInput f in enums) {
            if (f != FilterInput.MASK) {
                ret |= (UInt64) f;
            }
        }
        return ret;
    } 

    private UInt64 ReduceType(IEnumerable<FilterType> enums) {
        UInt64 ret = 0UL;
        foreach (FilterType f in enums) {
            if (f != FilterType.MASK) {
                ret |= (UInt64) f;
            }
        }
        return ret;
    }

    [Fact]
    public void FieldMaskHasEntireRange() {
        // Arrange
        UInt64 act = ReduceField(field_flags);
        
        // Ensure mask works as intended, it shouldnt remove any bits in this case
        Assert.Equal(act, act & (UInt64) FilterField.MASK);
        
        // Insert value outside mask
        act |= 0b1;
        // Should remove now
        act &= (UInt64) FilterField.MASK;
        // Check if bit is still set 
        Assert.Equal(0UL, act & 0b1);        
    }

    [Fact]
    public void FieldFlagsSetUniqueBits() {
        // Arrange
        UInt64 act = ReduceField(field_flags);
        foreach (FilterField exp in field_flags) {
            if (exp != FilterField.MASK) {
                FilterField fact = (FilterField) (act & (UInt64) exp);
                Assert.Equal(exp, fact);
            }
        }
    }

    [Fact]
    public void InputMaskHasEntireRange() {
        // Arrange
        UInt64 act = ReduceInput(input_flags);

        // Ensure mask works as intended, it shouldnt remove any bits in this case
        Assert.Equal(act, act & (UInt64) FilterInput.MASK);
        
        // Insert value outside mask
        act |= 0b1;
        // Should remove now
        act &= (UInt64) FilterInput.MASK;   
        // Check if bit is still set 
        Assert.Equal(0UL, act & 0b1);   
    }

    [Fact]
    public void InputFlagsSetUniqueBits() {
        // Arrange
        UInt64 act = ReduceInput(input_flags);
        foreach (FilterInput exp in field_flags) {
            if (exp != FilterInput.MASK) {
                FilterInput fact = (FilterInput) (act & (UInt64) exp);
                Assert.Equal(exp, fact);
            }
        }
    }

    [Fact]
    public void TypeMaskHasEntireRange() {
        // Arrange
        UInt64 act = ReduceType(type_flags);

        // Ensure mask works as intended, it shouldnt remove any bits in this case
        Assert.Equal(act, act & (UInt64) FilterType.MASK);
        
        // Insert value outside mask
        act |= 0b1;
        // Should remove now
        act &= (UInt64) FilterType.MASK;   
        // Check if bit is still set 
        Assert.Equal(0UL, act & 0b1);   
    }

    [Fact]
    public void TypeFlagsSetUniqueBits() {
        // Arrange
        UInt64 act = ReduceInput(input_flags);
        foreach (FilterType exp in field_flags) {
            if (exp != FilterType.MASK) {
                FilterType fact = (FilterType) (act & (UInt64) exp);
                Assert.Equal(exp, fact);
            }
        }
    }
}