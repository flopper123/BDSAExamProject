using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LitExplore.Core;
using LitExplore.Entity;
using Xunit;

namespace LitExplore.Tests
{   
    // In memory testing of publication repository
    public class PublicationRepositoryTests : IDisposable
    {
        private bool disposedValue;
        private readonly ILitExploreContext _context;
        private readonly PublicationRepository _repository;
        
        public PublicationRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<LitExploreContext>();
            builder.UseSqlite(connection);
            var context = new LitExploreContext(builder.Options);
            context.Database.EnsureCreated();
            
            seed(context);

            context.SaveChanges();
            _context = context;
            _repository = new PublicationRepository(_context);
        }

        // init db
        private void seed(LitExploreContext context)
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
              new Publication { Title = "Test pub 2", References = new List<Reference>{ ref1 } },
              new Publication { Title = "Test pub 3", References = new List<Reference> { ref1, ref2 } }
            );
        }

        [Fact]
        public async Task CreateAsync_Given_PublicationCreateDto_Returns_Created_And_PublicationDto()
        {
            PublicationCreateDto publication = new PublicationCreateDto
            {
                Title = "My Little Pony",
                References = new HashSet<ReferenceDto> {new ReferenceDto{Title = "Alabama Show Down"}}
            };

            PublicationDto created = await _repository.CreateAsync(publication);
            
            Assert.NotNull(created);
            Assert.Equal("My Little Pony", created.Title);
            Assert.True(created.References.Contains(new ReferenceDto{Title = "Alabama Show Down"}));        
        }
        
        [Fact]
        public async Task ReadAsync_given_Title_exists_returns_PublicationDto() 
        {
            // Arrange
            ReferenceDto exp = new ReferenceDto{Title = "Test pub 2"};

            // Act
            PublicationDto? act = await _repository.ReadAsync("Test pub 1");
            
            // Assert
            
            Assert.NotNull(act); // Why not assert it since this is test and should be True
            // not null at this point :))
            Assert.Equal("Test pub 1", act.Title);
            Assert.True(act.References.Count != 0, "Actual references is empty");            

            //This nullpointer Ex.. ??
            Assert.True(act.References.Contains(exp), 
                "Actual references != expected references act.references \n\t" + 
                $"Actual ref: \n\t\tType @{act.References.GetType()} \n\t\tCount @{act.References.Count}, \n\t\tFirst element @{act.References.GetEnumerator().Current}\n\t" +
                $"Expected ref: \n\t\tType @{exp.GetType()}, \n\t\t Expected@{exp.ToString()}");            
        }

        [Fact]
        public async Task ReadAsync_Returns_ReadOnlyCollection_Of_PublicationDto()
        {
            var act = await _repository.ReadAsync();
            
            Assert.Equal(3, act.Count); // Check that we got the right amount of Publications

            /*List<Publication> pub1 = await _context.Publications 
                .Select(p => p)
                .Where(p=> act.Any(f => f.Title == p.Title)
                    ).ToListAsync(); // TO:DO Find out how to fetch the  List of pubs in linQ.. // TO:DO SHOULD BE DELETED MAYBE MOST SURELY..
;           */
            
            
            foreach (PublicationDto dto in act)
            {
                Publication? expected = await _context.Publications.FindAsync(dto.Title); // Find the DB rep to test upon.
                Assert.NotNull(expected); // check that it found it.

                Debug.Assert(expected != null, nameof(expected) + " != null"); // For deeper errors. 
                Assert.Equal(expected.Title, dto.Title);
                // Do somehing to check the references.
                //TO:DO check for references.
            }
        }

        [Fact]
        public async Task UpdateAsync_GivenUpdateDto_Returns_Updated()
        {
            PublicationUpdateDto updateDto = new PublicationUpdateDto // This will be set more fully when program runs.
            {
                Title = "Test pub 1",
                References = new HashSet<ReferenceDto> {new ReferenceDto {Title = "Test Pub 2"}},
            };

            var result = _repository.UpdateAsync(updateDto);

            var expected = await _context.Publications.FindAsync(updateDto.Title); 
            // TO:DO Might think about how strictly we check title since
            // "Test PUB" is not equal to "test pub" 
            
            Assert.NotNull(expected); // check that it found it.

            Debug.Assert(expected != null, nameof(expected) + " != null"); // For deeper errors. 
            Assert.Equal(expected.Title, updateDto.Title);
                
            // Do something to check the references.
            //TO:DO check for references.


            // TO:DO Might want to test return as well ??
        }

        [Fact]
        public async Task DeleteAsync_Given_Title_Returns_Deleted()
        {
            //TO:DO Implement test Should be easy Will implement this the next time I look at it if its still here -- Mads.
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}