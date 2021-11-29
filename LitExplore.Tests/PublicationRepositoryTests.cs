using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LitExplore.Core;
using LitExplore.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;
using Xunit;

namespace LitExplore.Tests
{
    // In memory testing of publication repository
    public class PublicationRepositoryTests : IDisposable
    {
        private bool disposed;
        private readonly ILitExploreContext _context;
        private readonly PublicationRepository _repository;
        
        public PublicationRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<LitExploreContext>();
            builder.UseSqlite(connection);
            var context = new LitExploreContext(builder.Options);
            seed(context);
            _context = context;
            _repository = new PublicationRepository(_context);
        }

        // init db
        private void seed(LitExploreContext context)
        {
            context.Database.EnsureCreated();
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
            context.SaveChanges();
        }

        [Fact]
        public async Task ReadAsync_given_id_exists_returns_Character() {
            PublicationDto? act = await _repository.ReadAsync("Test pub 1") ?? null;
            Assert.True(act != null);
            // not null at this point :))
            if (act != null) {
                Assert.Equal("Test pub 1", act.Title);
                Assert.Equal("David", act.Author);
                Assert.Equal(2021, act.Year);
                Assert.Equal(1, act.Pages);
            
                ISet<ReferenceDto> exp = new HashSet<ReferenceDto>();
                exp.Add(new ReferenceDto("Test pub 2"));
                Assert.True(exp.Count != 0, "Expected references is empty");
                Assert.True(act.References.Count != 0, "Actual references is empty");
                Assert.True(act.References.SetEquals(exp), 
                    "Actual references != expected references act.references \n\t" + 
                    $"Actual ref: \n\t\tType @{act.References.GetType()} \n\t\tCount @{act.References.Count}, \n\t\tFirst element @{act.References.GetEnumerator().Current}\n\t" +
                    $"Expected ref: \n\t\tType @{exp.GetType()} \n\t\tCount @{exp.Count}, \n\t\tFirst element @{exp.GetEnumerator().Current.ToString()}");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
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