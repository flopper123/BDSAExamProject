namespace LitExplore.Tests.Entity.Repositories;
    /*
    // In memory testing of publication repository
    public class PublicationRepTests : AbsRepositoryTests<PublicationRepository>
    {        
        public PublicationRepTests() : base() {}

        // init db
        override protected void seed()
        {
            // seed actions here
            Reference ref1 = new Reference { Title = "Test pub 1" };
            Reference ref2 = new Reference { Title = "Test pub 2" };
            
            context.References.AddRange(
                ref1, ref2     
            );

            Publication pub1 = new Publication{ Title = "Test pub 1", References = new List<Reference> {ref2}};

            context.Publications.AddRange(
              pub1,
              new Publication { Title = "Test pub 2", References = new List<Reference> { ref1 } },
              new Publication { Title = "Test pub 3", References = new List<Reference> { ref1, ref2 } }
            );
        }

        [Fact]
        public async Task CreateAsync_Given_PublicationCreateDto_Returns_Created_And_PublicationDto()
        {
            // Arrange
            PublicationCreateDto exp = new PublicationCreateDto
            {
                Title = "My Little Pony",
                References = new HashSet<ReferenceDto> { new ReferenceDto{Title = "Alabama Show Down"}}
            };

            // Act
            PublicationDto? act = await repository.CreateAsync(exp);
            
            // Assert
            if (act == null) { Assert.NotNull(act); return; }
            Assert.Equal("My Little Pony", act.Title);
            Assert.True(act.References.Contains(new ReferenceDto{Title = "Alabama Show Down"}));        
        }
        
        [Fact]
        public async Task ReadAsync_given_Title_exists_returns_PublicationDto() 
        {
            // Arrange
            // The reference representation of this DTO is being seeded to the db in seed()
            ReferenceDto exp = new ReferenceDto{ Title = "Test pub 2" };
            // Act
            PublicationDto? act = await repository.ReadAsync("Test pub 1");
            
            // Assert
            if (act == null) { Assert.NotNull(act); return; }
            Assert.Equal("Test pub 1", act.Title);
            Assert.True(act.References.Count != 0, "Actual references is empty");
            Assert.True(act.References.Contains(exp), 
                "Actual references != expected references act.references \n\t" + 
                $"Actual ref: \n\t\tType @{act.References.GetType()} \n\t\tCount @{act.References.Count}, \n\t\tFirst element @{act.References.GetEnumerator().Current}\n\t" +
                $"Expected ref: \n\t\tType @{exp.GetType()}, \n\t\t Expected@{exp.ToString()}");            
        }

        [Fact]
        public async Task ReadAsync_Returns_ReadOnlyCollection_Of_PublicationDto()
        {
            // Arrange & Act
            List<PublicationDto> actCol = (await repository.ReadAsync()).ToList();

            // Assert
            Assert.Equal(3, actCol.Count); // 3 are being seeded

            // Refactored tests to use Linq 
            actCol.ForEach(async act_dto => {
                var tmp = await context.Publications.FindAsync(act_dto.Title);
                if (tmp == null) { Assert.NotNull(tmp); return; }
                Assert.Equal(tmp.Title, act_dto.Title);
                // TO:DO add reference checks 
            });
        }

        [Fact]
        public async Task UpdateAsync_GivenUpdateDto_Returns_Updated()
        {
            PublicationUpdateDto updateDto = new PublicationUpdateDto // This will be set more fully when program runs.
            {
                Title = "Test pub 1",
                References = new HashSet<ReferenceDto> {new ReferenceDto {Title = "Test Pub 2"}},
            };

            var result = repository.UpdateAsync(updateDto);
            var exp = await context.Publications.FindAsync(updateDto.Title); 
            
            // TO:DO Might think about how strictly we check title since
            // "Test PUB" is not equal to "test pub" 
            
            Assert.NotNull(exp); // check that it found it.

            Debug.Assert(exp != null, nameof(exp) + " != null"); // For deeper errors. 
            Assert.Equal(exp.Title, updateDto.Title);
                
            // Do something to check the references.
            //TO:DO check for references.


            // TO:DO Might want to test return as well ??
        }

        [Fact]
        public async Task DeleteAsync_Given_Title_Returns_Deleted()
        {
            //TO:DO Implement test Should be easy Will implement this the next time I look at it if its still here -- Mads.
        }
    }
    */