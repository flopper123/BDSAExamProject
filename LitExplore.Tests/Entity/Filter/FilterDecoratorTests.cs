namespace LitExplore.Tests.Entity.Filter;

// Tests for filter decorations
public class FilterDecoratorTests
{
    List<PublicationDto> data;

    public FilterDecoratorTests() {
        data = new List<PublicationDto>() {
            new PublicationDto {
                Title = "His Little Pony",
                References = new HashSet<ReferenceDto> { }
            },
            new PublicationDto {
                Title = "My Little Pony",
                References = new HashSet<ReferenceDto> { new ReferenceDto{Title = "Alabama Show Down"}}
            },
            new PublicationDto {
                Title = "My Little Ox",
                References = new HashSet<ReferenceDto> { new ReferenceDto{Title = "Alabama Show Down"}}
            },
            new PublicationDto {
                Title = "My Large Pony",
                References = new HashSet<ReferenceDto> { new ReferenceDto{Title = "Alabama Show Down"}, 
                                                         new ReferenceDto{Title = "Copenhagen Show Down"},
                                                         new ReferenceDto{Title = "Swedish Show Down"}}
            },
            new PublicationDto {
                Title = "0xDEADBEEF",
                References = new HashSet<ReferenceDto> { }
            },
        };
    }

    [Fact]
    public void CanChainTitleFilters() {
        // Arrange
        Filter<PublicationDto> filter = new TitleFilter(
            "Pony",
            new TitleFilter(
                "Little"
            )
        );

        IEnumerable<PublicationDto> exp = new List<PublicationDto> {
            data[0], data[1]
        };

        // Act
        IEnumerable<PublicationDto> act = filter.Apply(data);

        // Assert
        Assert.Equal(exp, act);
    }

    [Fact]
    public void CanChainDifferentFilters() {
        // Arrange
        Filter<PublicationDto> filter = new TitleFilter(
            "Pony",
            new MinRefsFilter(2)
        );

        IEnumerable<PublicationDto> exp = new List<PublicationDto> {
            data[3]
        };

        // Act
        IEnumerable<PublicationDto> act = filter.Apply(data);

        // Assert
        Assert.Equal(exp, act);
    }

    [Fact]
    public void ReturnsEmptyEnumerableWhenEverythingIsFilteredAway() {
        // Arrange
        Filter<PublicationDto> filter = new TitleFilter(
            "Pony",
            new MinRefsFilter(100_000)
        );
        IEnumerable<PublicationDto> exp = new List<PublicationDto> {};

        // Act
        IEnumerable<PublicationDto> act = filter.Apply(data);

        // Assert
        Assert.Equal(exp, act);
    }

    [Fact]
    // We expect EmptyFilter to be excluded, and the order to be the same as the constructed
    // chain
    public void CorrectOrderOfGetFilterHistoryChains() {
        // Arrange 
        Filter<PublicationDto> fRefs = new MinRefsFilter(1, EmptyFilter<PublicationDto>.Get());
        Filter<PublicationDto> fLittle = new TitleFilter("little", fRefs);
        Filter<PublicationDto> fEnd = new TitleFilter("Pony", fLittle);

        // Act
        List<Filter<PublicationDto>> exp = new List<Filter<PublicationDto>> {
            fRefs, fLittle, fEnd
        };
        IEnumerable<Filter<PublicationDto>> act = fEnd.GetHistory();

        // Assert
        int i = 0;
        foreach (Filter<PublicationDto> fAct in act) {
            var fExp = exp[i++];
            Assert.Equal(fExp, fAct);
        }
    }

    [Theory]
    [InlineData(10)]
    [InlineData(30)]
    [InlineData(50)]
    [InlineData(100_00)]
    public void TestDepthOfChainedFilters(UInt32 exp) {

        Filter<PublicationDto> fAct = new TitleFilter("Pony");
        for (UInt32 i = 1; i < exp; i++) {
            fAct = new TitleFilter("Pony", fAct);
        }

        Assert.Equal(exp, (UInt32) fAct.GetHistory().Count());
        Assert.Equal(exp, fAct.Depth);
    }

    [Fact]
    public void TestDepthForEmptyFilter() {
        Assert.Equal(0UL, EmptyFilter<int>.Get().Depth);
    }

    [Fact]
    public void CanApplyConstructedTitleFilter()
    {
        string arg = "0xDEADBEEF";
        Filter<PublicationDto>? act_opt = FilterFactory.Create<PublicationDto>("TitleFilter", arg);
        Assert.NotNull(act_opt);

        TitleFilter? act_filter = (TitleFilter?) act_opt;
        Assert.NotNull(act_filter);
        if (act_filter == null) return;

        Assert.Equal(arg, (string) (act_filter.PredicateArgs ?? ""));
        var enumerator = act_filter.Apply(this.data).GetEnumerator();
        Assert.True(enumerator.MoveNext());
        var act = enumerator.Current;
        Assert.Equal(data[4], act);
    }
}

