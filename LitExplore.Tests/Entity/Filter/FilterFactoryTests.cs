namespace LitExplore.Tests.Entity.Filter;


public class FilterFactoryTests {
    
    [Fact]
    public void CanGetAllConcreteTypes() {
        Type[] exp = { typeof(TitleFilter), typeof(MinRefsFilter) };
        var act = FilterFactory.GetConcreteFilters();
        
        foreach(Type t in exp) {
            Assert.True(act.Contains(t), $"Failed to find {t} among concrete filters of filter factory"
                                         + $"\n\tConcrete filters contains {act.Count()} in total");
        }
    }
    
    /*
    public List<(EFilter, Type)> _exp = new List<(EFilter, Type)> {
       // (EFilter.PUB, typeof(EmptyFilter<PublicationDto>)),
        (EFilter.PUB_STR_TITLE_CONTAINS, typeof(TitleFilter)),
        (EFilter.PUB_UINT64_REF_MINSIZE, typeof(MinRefsFilter)),
    };


    [Fact]
    public void printMap() {
        string msg = "Map: ";
        var map = FilterFactory.GetMap();

        msg += map.Count + "@count";
        foreach ((EFilter eid, Type t) in map) {
            msg += $"\n\t(EFilter@{eid}, Type@{t}";
        }

        Assert.True(false, msg);
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
    */
}