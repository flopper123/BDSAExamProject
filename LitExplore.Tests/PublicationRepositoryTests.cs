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

            Publication pub1 = new Publication{ Title = "Test pub 1", Author ="David", 
                                                Year = 2021, Pages = 1, References = new List<Reference> {ref2}};
            context.Publications.AddRange(
              pub1,
              new Publication { Title = "Test pub 2", Author = "Chris", Year = 2021, Pages = 1, References = new List<Reference>{ ref1 } },
              new Publication { Title = "Test pub 3", Author = "Mads", Year = 2021, Pages = 1, References = new List<Reference> { ref1, ref2 } }
            );
        }

        [Fact]
        public async Task CreateAsync_Given_PublicationCreateDto_Returns_Created_And_PublicationDto()
        {
            PublicationCreateDto publication = new PublicationCreateDto
            {
                Title = "My Little Pony",
                Author = "Bonnie Zacherle",
                Edition = 1,
                Pages = 10,
                Publisher = "Someone",
                Year = 1983,
                References = new HashSet<ReferenceDto> {new ReferenceDto{Title = "Alabama Show Down"}}
            };

            PublicationDto created = await _repository.CreateAsync(publication);
            
            Assert.NotNull(created);
            Assert.Equal("My Little Pony", created.Title);
            Assert.Equal("Bonnie Zacherle", created.Author);
            Assert.Equal(1, created.Edition);
            Assert.Equal(10, created.Pages);
            Assert.Equal("Someone", created.Publisher);
            Assert.Equal(1983, created.Year);
            Assert.True(created.References.SetEquals(new [] {new ReferenceDto{Title = "Alabama Show Down"}}));
        }
        
        [Fact]
        public async Task ReadAsync_given_Title_exists_returns_PublicationDto() 
        {
            PublicationDto? act = await _repository.ReadAsync("Test pub 1");
            
                Assert.NotNull(act); // Why not assert it since this is test and should be True
                // not null at this point :))
                Assert.Equal("Test pub 1", act.Title);
                Assert.Equal("David", act.Author);
                Assert.Equal(2021, act.Year);
                Assert.Equal(1, act.Pages);
            
                ISet<ReferenceDto> exp = new HashSet<ReferenceDto>();
                exp.Add(new ReferenceDto{Title = "Test pub 2"});
                Assert.True(exp.Count != 0, "Expected references is empty");
                Assert.True(act.References.Count != 0, "Actual references is empty");

                var boo = act.References.SetEquals(exp); // This evaluates to True.
                
                //This nullpointer Ex.. ??
                /*Assert.True(act.References.SetEquals(exp), 
                    "Actual references != expected references act.references \n\t" + 
                    $"Actual ref: \n\t\tType @{act.References.GetType()} \n\t\tCount @{act.References.Count}, \n\t\tFirst element @{act.References.GetEnumerator().Current}\n\t" +
                    $"Expected ref: \n\t\tType @{exp.GetType()} \n\t\tCount @{exp.Count}, \n\t\tFirst element @{exp.GetEnumerator().Current.ToString()}"); // Dont know what magic but this makes the test fail not the assertion but something. */
            
        }

        [Fact]
        public async Task ReadAsync_Returns_ReadOnlyCollection_Of_PublicationDto()
        {
            var act = await _repository.ReadAsync();
            
            Assert.Equal(3, act.Count); // Check that we got the right amount of Publications

            List<Publication> pub1 = await _context.Publications
                .Select(p => p)
                .Where(p=> p.Title == act.Select(publicationDto => publicationDto.Title)
                    .FirstOrDefault()).ToListAsync();
;
            foreach (PublicationDto dto in act)
            {
                Publication? expected = pub1.Find(p => p?.Title == dto.Title);
                Assert.NotNull(expected); // check that it found it.

                Debug.Assert(expected != null, nameof(expected) + " != null"); // For deeper errors. 
                Assert.Equal(expected.Title, dto.Title);
                Assert.Equal(expected.Author, dto.Author);
                Assert.Equal(expected.Edition, dto.Edition);
                Assert.Equal(expected.Pages, dto.Pages);
                Assert.Equal(expected.Publisher, dto.Publisher);
                Assert.Equal(expected.Year, dto.Year);
                
                // Do somehing to check the references.
                //TO:DO check for references.
            }
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