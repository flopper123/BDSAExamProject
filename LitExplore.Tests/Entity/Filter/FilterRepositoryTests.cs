namespace LitExplore.Tests.Entity.Filter;

// In memory testing of publication repository
public class FilterRepositoryTests : AbsRepositoryTests<FilterRepository<PublicationDto>>
{
    public FilterRepositoryTests() : base() {}

    // init db
    override protected void seed()
    {
        // seed actions here
        Reference ref1 = new Reference { Title = "Test pub 1" };
        Reference ref2 = new Reference { Title = "Test pub 2" };

        context.References.AddRange(
            ref1, ref2
        );

        Publication pub1 = new Publication { Title = "Test pub 1", References = new List<Reference> { ref2 } };

        context.Publications.AddRange(
          pub1,
          new Publication { Title = "Test pub 2", References = new List<Reference> { ref1 } },
          new Publication { Title = "Test pub 3", References = new List<Reference> { ref1, ref2 } }
        );
    }

    [Fact]
    public async Task ReadAsync_Returns_Filter_Given_UserID()
    {
        // Arrange
        var expected = new TitleFilter("0x",null); // TODO: Build A proper filter.
        var userId = 1UL;

        // Act
        Filter<PublicationDto> act = await repository.ReadAsync(userId);

        // Assert
        
        Assert.Equal(expected, act);
    }

    IEnumerable<Filter<PublicationDto>> GetUidFilter() {
        yield return new TitleFilter("0xDEAD");
        yield return new TitleFilter("0xDEAD", new TitleFilter("AXFALSE"));
        yield return new MinRefsFilter(10);
    }

    [Theory]
    [MemberData("GetUidFilter")]
    public async Task UpdateAsync_Returns_Updated(Filter<PublicationDto> filter)
    {
        var base_filter = new TitleFilter("I DONT WORK");
        await repository.UpdateAsync(1337UL, base_filter);
        var act_status = await repository.UpdateAsync(1337UL, filter);
        Assert.Equal(Status.Updated, act_status);
    }

    [Theory]
    [MemberData("GetUidFilter")]
    public async Task UpdateAsync_Returns_Created(Filter<PublicationDto> filter)
    {
        var act_status = await repository.UpdateAsync(1337UL, filter);
        Assert.Equal(Status.Created, act_status);
    }

    [Theory]
    [MemberData("GetUidFilter")]
    public async Task UpdateAsync_Actually_Updates(Filter<PublicationDto> filter)
    {
        var base_filter = new TitleFilter("I DONT WORK");
        var base_fs = base_filter.Serialize();
        await repository.UpdateAsync(1337UL, base_filter);

        // Retrieve and ensure 1337 user points to base_filter

        var act_status = await repository.UpdateAsync(1337UL, filter);
        

        // Retrieve and ensure 1337 user points to input filter.
        // filter.Serialize
    }

    [Theory]
    [MemberData("GetUidFilter")]
    public async Task UpdateAsync_Actually_Creates(Filter<PublicationDto> filter)
    {
        var act_status = await repository.UpdateAsync(1337UL, filter);
        Assert.Equal(Status.Created, act_status);
    }

    [Fact]
    public async Task DeleteAsync_Given_Title_Returns_Deleted()
    {
        //TO:DO Implement test Should be easy Will implement this the next time I look at it if its still here -- Mads.
    }
}
