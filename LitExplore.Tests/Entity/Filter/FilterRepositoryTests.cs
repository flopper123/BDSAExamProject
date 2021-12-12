namespace LitExplore.Tests.Entity.Filter;

// In memory testing of publication repository
public class FilterRepositoryTests : AbsRepositoryTests<FilterRepository<PublicationDto>>
{
    public FilterRepositoryTests() : base() {}

    // init db
    override protected void seed()
    {

        UserFilter f1 = new UserFilter{UserId=1,Serialization = new TitleFilter("0x",null).Serialize()};
        UserFilter f2 = new UserFilter{UserId=2, Serialization = new TitleFilter("Coast", new MinRefsFilter(3, null)).Serialize()};
        // seed actions here
        
        context.History.AddRange(
            f1,f2
        );
    }

    [Fact]
    public void TempCheckToseeIfMigrationWorked()
    {
        ulong UserId = 1UL; 
        var firstuser = context.History.Find(UserId);

        Assert.NotNull(firstuser);
    }

    [Fact]
    public async Task ReadAsync_Returns_Filter_Given_UserID()
    {
        // Arrange
        var exp = new TitleFilter("0x",null);
        var userId = 1UL;

        // Act
        Filter<PublicationDto>? act = await repository.ReadAsync(userId);

        // Assert
        Assert.NotNull(act);
        if (act == null) return;
        Assert.Equal(exp.GetType(), act.GetType());
        Assert.Equal(exp.Depth, act.Depth);
        Assert.Equal(exp.Serialize(), act.Serialize());
    }
    
    public static IEnumerable<Object[]> GetUidFilter() {
        yield return new Object[] { new TitleFilter("0xDEAD") };
        yield return new Object[] { new TitleFilter("0xDEAD", new TitleFilter("AXFALSE")) };
        yield return new Object[] { new MinRefsFilter(10) };
    }

    [Theory]
    [MemberData(nameof(GetUidFilter))]
    public async Task UpdateAsync_UpdatesAndReturns_Updated(Filter<PublicationDto> filter)
    {
        UInt64 uid = 1337UL;
        
        var base_filter = new TitleFilter("I dont exist");
        await repository.UpdateAsync(uid, base_filter);
        
        var act_status = await repository.UpdateAsync(uid, filter);
        Assert.Equal(Status.Updated, await repository.UpdateAsync(uid, filter));
        
        UserFilter? act = context.History.Find(uid);
        Assert.NotNull(act);
        if (act == null) return;
        
        Assert.Equal(filter.Serialize(), act.Serialization);
    }

    [Theory]
    [MemberData(nameof(GetUidFilter))]
    public async Task UpdateAsync_CreatesAndReturns_Created(Filter<PublicationDto> filter)
    {
        UInt64 uid = 1337UL;
        var base_filter = new TitleFilter("I dont exist");
        Assert.Equal(Status.Created, await repository.UpdateAsync(uid, base_filter));
        
        UserFilter? act = context.History.Find(uid);
        Assert.NotNull(act);
        if (act == null) return;
//
        Assert.Equal(base_filter.Serialize(), act.Serialization);
    }
}