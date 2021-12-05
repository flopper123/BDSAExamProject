
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
}

