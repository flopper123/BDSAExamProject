namespace LitExplore.Tests.Entity.Filter;

public class FilterFactoryTests {

    public List<(EFilter, Type)> _exp = new List<(EFilter, Type)> {
        // (EFilter.PUB, typeof(EmptyFilter<PublicationDto>)),
        (EFilter.PUB_STR_TITLE_CONTAINS, typeof(TitleFilter)),
        (EFilter.PUB_UINT64_REF_MINSIZE, typeof(MinRefsFilter)),
    };

    [Fact]
    public void CanGetEmptyPubFByFilterType() {
        Type act = FilterFactory.Get((UInt64) FilterType.PUB);
        Type exp = typeof(EmptyFilter<PublicationDto>);
        Assert.Equal(exp, act);
    }
    
    [Fact]
    public void CanGetEmptyPubFByEFilter() {
        Type act = FilterFactory.Get(EFilter.PUB);
        Type exp = typeof(EmptyFilter<PublicationDto>);
        Assert.Equal(exp, act);
    }

    [Fact]
    public void CanGetAllConcreteTypesWithFilter() {
        Type[] exp = { typeof(TitleFilter), typeof(MinRefsFilter) };

        var act = FilterFactory.GetConcreteFilters(Assembly.Load("LitExplore.Entity"));
        
        foreach(Type t in exp) {
            Assert.True(act.Contains(t), $"Failed to find {t} among concrete filters of filter factory"
                                         + $"\n\tConcrete filters contains {act.Count()} in total");
        }
    }

    [Fact]
    public void IfCantGetFromUInt64KeyNotFoundThrown() {
        List<UInt64> false_eids = new List<UInt64>{
            (UInt64) EFilter.PUB_UINT64_REF_MAXSIZE,
            (UInt64) EFilter.ART,
            (UInt64) EFilter.PUB_STR_AUTHOR,
            1337UL,
            120192913UL,
        };

        foreach (UInt64 eid in false_eids)
        {
            Assert.Throws<KeyNotFoundException>(
                () => { FilterFactory.Get(eid); }
            );
        }
    }

    [Fact]
    public void IfCantGetFromEidKeyNotFoundThrown() {
        List<EFilter> false_eids = new List<EFilter>{
            EFilter.PUB_UINT64_REF_MAXSIZE,
            EFilter.ART,
            EFilter.PUB_STR_AUTHOR,
        };

        foreach (EFilter eid in false_eids)
        {
            Assert.Throws<KeyNotFoundException>(
                () => { FilterFactory.Get(eid); }
            );
        }
    }

    [Fact]
    public void CanGetTypeFromUInt64() {
        foreach((EFilter eid, Type exp) in _exp) {
            Type act = FilterFactory.Get((UInt64) eid);
            Assert.Equal(exp, act);
        }   
    }
  
    [Fact]
    public void CanGetTypeFromEid() {
        foreach((EFilter eid, Type exp) in _exp) {
            Type act = FilterFactory.Get(eid);
            Assert.Equal(exp, act);
        }   
    }

    [Theory]
    [InlineData(typeof(MinRefsFilter), EFilter.PUB_UINT64_REF_MINSIZE, 10)]
    [InlineData(typeof(TitleFilter), EFilter.PUB_STR_TITLE_CONTAINS, "0xDEADBEEF")]
    public void CanConstructFromEIDAndObjectArgs(Type exp, EFilter eid, params Object[] args) {
        object? act_filter = FilterFactory.Construct(eid, args);
        Assert.NotNull(act_filter);
        if (act_filter != null) Assert.Equal(exp, act_filter.GetType());
    }
}