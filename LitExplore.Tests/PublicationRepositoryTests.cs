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
            var ref1 = new Reference { Title = "Test pub 2" };
            var ref2 = new Reference { Title = "Test pub 1" };

            context.Publications.AddRange(
              new Publication { Title = "Test pub 1", Author = "David", Year = 2021, Pages = 1, References = new[] { ref1 } },
              new Publication { Title = "Test pub 2", Author = "David", Year = 2021, Pages = 1, References = new[] { ref2 } },
              new Publication { Title = "Test pub 3", Author = "David", Year = 2021, Pages = 1, References = new[] { ref1, ref2 } }
            );
            context.SaveChanges();
        }Half 



        [Fact]
        public void CanCreate()
        {

        }
        [Fact]
        public async void ReadAsync_Id_3_Retruns_Publication_no3()
        {
            PublicationDto expected = new PublicationDto(
                "Test pub 3", 
                "David", 
                2021, 
                PublicationType.Article,
                "ITU",
                1, 
                1,
                new HashSet<ReferenceDto> { 
                    new ReferenceDto { Title = "Test pub 2" }, 
                    new ReferenceDto { Title = "Test pub 1" }, 
                }
            );

            PublicationDto exp = new PublicationDto

            var acutal = await _repository.ReadAsync("Test pub 3");

            Assert.True(expected.Equals(acutal));

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