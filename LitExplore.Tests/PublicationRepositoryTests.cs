using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LitExplore.Core;
using LitExplore.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LitExplore.Tests
{
    // In memory testing of publication repository
    public class PublicationRepositoryTests : IDisposable
    {
        private bool disposed;
        private readonly ILitExploreContext _context;
        private readonly PublicationRepository _repository;
        //cdotnet ef migrations add InitialCreate
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
            // context.Database.EnsureCreated();
            // seed actions here
            Reference ref1 = new Reference { Title = "Test pub 1" };
            Reference ref2 = new Reference { Title = "Test pub 2" };
            
            context.References.AddRange(
                ref1, ref2
                
            );

            context.Publications.AddRange(
              new Publication { Title = "Test pub 1", Author = "David", Year = 2021, Pages = 1, References = new[] { ref2 } },
              new Publication { Title = "Test pub 2", Author = "Chris", Year = 2021, Pages = 1, References = new[] { ref1 } },
              new Publication { Title = "Test pub 3", Author = "Mads", Year = 2021, Pages = 1, References = new[] { ref1, ref2 } }
            );
            context.SaveChanges();
        }

        [Fact]
        public async Task ReadAsync_given_id_exists_returns_Character() {
            PublicationDto act = await _repository.ReadAsync("Test pub 1");
            
            Assert.Equal("Test pub 1", act.Title);
            Assert.Equal("David", act.Author);
            Assert.Equal(2021, act.Year);
            Assert.Equal(1, act.Pages);
            
            ISet<ReferenceDto> refs = new HashSet<ReferenceDto>();
            refs.Add(new ReferenceDto {Title = "Test pub 2"});
            Assert.True(act.References.SetEquals(refs));
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