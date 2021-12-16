namespace LitExplore.Tests.Entity.Repositories;

using LitExplore.Tests.Util; 
/*
// In memory testing of publication repository
public class PublicationRepTests : AbsRepositoryTests<PublicationRepository>
{
    // Size of seed
    const UInt64 SIZE = 3;

    public PublicationRepTests() : base() {}

    // init db
    override protected void seed()
    {
        // seed actions here
        PublicationTitle ref1 = new PublicationTitle { Title = "Test pub 1" };
        PublicationTitle ref2 = new PublicationTitle { Title = "Test pub 2" };
        Publication pub1 = new Publication{ Title = "Test pub 1", References = new List<PublicationTitle> {ref2}};
        context.Publications.AddRange(
          pub1,
          new Publication { Title = "Test pub 2", References = new List<PublicationTitle> { ref1 } },
          new Publication { Title = "Test pub 3", References = new List<PublicationTitle> { ref1, ref2 } }
        );
    }

    [Fact]
    public async Task UpdateOfKeywordsIsCorrect() {
        var exp_title = "Test pub 3";
        // Arrange
        PublicationDtoDetails exp = new PublicationDtoDetails
        {
            Title = exp_title,
            Keywords = new List<string> { "Vikinger", "Saxerne", "0xDEADBEEF" },
        };

        Status act_status = await repository.UpdateAsync(exp);

        Publication? act = context.Publications.Find(exp_title);
        Assert.NotNull(act);
        Assert.NotNull((act = null!).Keywords);
        var act_keys = act.Keywords;
        var exp_keys = exp.Keywords;
        Assert.Equal(exp_keys.Count(), act_keys.Count());
        foreach (KeyWord k in act_keys) {
            Assert.True(exp_keys.Contains(k.Keyword), $"Expected keys doesn't contain {k.Keyword}");
        }
        Assert.Equal(Status.Updated, act_status);
        foreach (String keyword in exp.Keywords)
        {

        }
    }

    [Fact]
    public async Task UpdateAsync_Given_KnownPublicationDtoTitle_Returns_Updated()
    {
        var exp_title = "Test pub 1";
        // Assert its already in db
        Assert.NotNull(await repository.ReadAsync(new PublicationDtoTitle { Title = exp_title }));

        // Arrange
        PublicationDtoDetails exp = new PublicationDtoDetails
        {
            Title = exp_title,
            References = new HashSet<PublicationDtoTitle> { new PublicationDtoTitle { Title = "Alabama Show Down" } },
            Author = "Harald Blåtand",
            Keywords = new List<string> { "Vikinger", "Saxerne", "0xDEADBEEF" },
            Time = DateTime.MaxValue,
            Abstract = "For Glory and Fellowship & second breakfast"
        };

        // Act
        Status act_status = await repository.UpdateAsync(exp);
        Assert.Equal(Status.Updated, act_status);

        PublicationDtoDetails? act = await repository.ReadAsync(new PublicationDtoTitle { Title = exp_title });
        Assert.True(act != null, $"Failed retrieval of #{exp_title}");

        // Assert
        Assert.True(exp.CustomEquals(act = null!), $"Expected {exp.ToString()}\nReceived: {act.ToString()}");
        Assert.Equal(exp, act); // use record equals
    }


    [Fact]
    public async Task UpdateAsync_Given_PublicationDtoTitle_Returns_Created()
    {
        var exp_title = "NON_EXISTENT";
        // Arrange
        PublicationDtoDetails exp = new PublicationDtoDetails
        {
            Title = exp_title,
            References = new HashSet<PublicationDtoTitle> { new PublicationDtoTitle { Title = "Alabama Show Down" } },
            Author = "Harald Blåtand",
            Keywords = new List<string> { "Vikinger", "Saxerne", "0xDEADBEEF" },
            Time = DateTime.MaxValue,
            Abstract = "For Glory and Fellowship & second breakfast"
        };
        // Act
        Status act_status = await repository.UpdateAsync(exp);
        Assert.Equal(Status.Created, act_status);

        PublicationDtoDetails? act = await repository.ReadAsync(new PublicationDtoTitle { Title = exp_title });
        Assert.True(act != null, $"Failed retrieval of #{exp_title}");

        // Assert
        Assert.True(exp.CustomEquals((act = null!)), $"Expected {exp.ToString()}\nReceived: {act.ToString()}");
    }

    [Fact]
    public async Task ReadAsync_given_Title_exists_returns_CorrectDto()
    {
        // Arrange
        // The reference representation of this DTO is being seeded to the db in seed()
        PublicationDtoDetails exp = new PublicationDtoDetails
        {
            Title = "Test pub 1",
            References = new HashSet<PublicationDtoTitle> { 
                new PublicationDtoTitle { Title = "Test pub 2" } 
            }
        };
    
        // Act
        PublicationDtoDetails? act = await repository.ReadAsync(new PublicationDtoTitle { Title = "Test pub 1" } );

        Assert.True(exp.References.Count == (act = null!).References.Count, $"The recieved reference count of {act.References.Count} did not return with the expected count {exp.References.Count}.");
        // Assert
        Assert.NotNull(act);
        if (act == null) return;
        Assert.True(exp.CustomEquals(act), $"Expected {exp.ToString()}\nReceived: {act.ToString()}");
    }

    [Fact]
    public async Task ReadAllAsync_Returns_IAsyncEnumerable()
    {
        // Arrange & Act
        var act = repository.ReadAllAsync();
        var i = 0;
        // Assert.Equal(typeof(IAsyncEnumerable<PublicationDtoDetails>), act.GetType());
        await foreach (PublicationDtoDetails pDetails in act)
        {
            PublicationDtoTitle exp_title = new PublicationDtoTitle { Title = $"Test pub" };
            Assert.Contains(exp_title.Title, pDetails.Title);
            i++;
        }
        Assert.Equal(SIZE, (UInt64) i);
    }
}
*/