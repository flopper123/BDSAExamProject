namespace LitExplore.Tests.Core.Filter;

using LitExplore.Core.Filter;
using static LitExplore.Core.Filter.FilterPArgField;

public class FilterPArgsParserTests {

    [Fact]
    public void CanExtractPArgs() {
        // Arrange
        string fst = $"{LINE_START}{TYPE}{VALUE_SEPERATOR}System.String{FIELD_SEPERATOR}"
                    +$"{VALUE}{VALUE_SEPERATOR}0xDEADBEEF{LINE_END}";
        string snd = $"{LINE_START}{TYPE}{VALUE_SEPERATOR}System.UInt64{FIELD_SEPERATOR}"
                    +$"{VALUE}{VALUE_SEPERATOR}0xDEADBEEF{LINE_END}";
        string tmp = $"{fst}{PARG_SEPERATOR}{snd}";
        // Act
        (string type, string value)[] exp = { ("System.String", "0xDEADBEEF"),
                                              ("System.UInt64", "0xDEADBEEF") };

        var act = FilterPArgsParser.ExtractArgs(tmp);

        // Assert
        int i = 0;
        foreach ((string type, string value) in act) {
            Assert.Equal(exp[i].type, type);
            Assert.Equal(exp[i].value, value);
            i++;
        }
    } 
}