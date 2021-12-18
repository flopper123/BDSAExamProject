namespace LitExplore.Tests.Entity.Repositories;

using LitExplore.Entity.Repositories;
using LitExplore.Core.Filter.Filters;
using LitExplore.Tests.Entity.Filter;

// In memory testing of publication repository
public class FilterRepositoryTests : AbsRepositoryTests<FilterRepository<PublicationGraph>>
{
    public FilterRepositoryTests() : base() {}

    // init db
    override protected void seed()
    {
        // seed actions here
        UserFilter f1 = new UserFilter{UserId=1,Serialization = new TitleContains("0x",null).Serialize()};
        UserFilter f2 = new UserFilter{UserId=2, 
            Serialization = 
                new TitleContains("Coast", 
                    new TitleContains("line", null)).Serialize()
        };
        
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
        var exp = new TitleContains("0x",null);
        var userId = 1UL;

        // Act
        Filter<PublicationGraph>? act = await repository.ReadAsync(userId);

        // Assert
        Assert.NotNull(act);
        if (act == null) return;
        Assert.Equal(exp.GetType(), act.GetType());
        Assert.Equal(exp.Depth, act.Depth);
        Assert.Equal(exp.Serialize(), act.Serialize());
    }
    
    public static IEnumerable<Object[]> GetUidFilter() {
        yield return new Object[] { new TitleContains("0xDEAD") };
        yield return new Object[] { new TitleContains("0xDEAD", new TitleContains("AXFALSE")) };
    }

    [Theory]
    [MemberData(nameof(GetUidFilter))]
    public async Task UpdateAsync_UpdatesAndReturns_Updated(Filter<PublicationGraph> filter)
    {
        UInt64 uid = 1337UL;
        
        var base_filter = new TitleContains("I dont exist");
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
    public async Task UpdateAsync_CreatesAndReturns_Created(Filter<PublicationGraph> filter)
    {
        UInt64 uid = 1337UL;
        Assert.Equal(Status.Created, await repository.UpdateAsync(uid, filter));
        
        UserFilter? act = context.History.Find(uid);
        Assert.NotNull(act);
        if (act == null) return;
//
        Assert.Equal(filter.Serialize(), act.Serialization);
    }
}