// using System;
// using System.Threading.Tasks;
// using LitExplore.Core;
// using LitExplore.Entity;
// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using Xunit;

// namespace LitExplore.Tests
// { 
//   // In memory testing of publication repository
//   public class UserRepositoryTests : IDisposable
//   {
//     private bool disposed;
//     private readonly ILitExploreContext _context;
//     private readonly UserRepository _repository;

//     public UserRepositoryTests()
//     {
//       var connection = new SqliteConnection("Filename=:memory:");
//       connection.Open();
//       var builder = new DbContextOptionsBuilder<LitExploreContext>();
//       builder.UseSqlite(connection);
//       var context = new LitExploreContext(builder.Options);
//       seed(context);
//       _context = context;
//       _repository = new UserRepository(_context);
//     }

//     // init db
//     private void seed(LitExploreContext context) {
//       context.Database.EnsureCreated();
//       // seed actions here
//       context.SaveChanges();
//     }
    
//     protected virtual void Dispose(bool disposing)
//     {
//       if (!disposed)
//       {
//         if (disposing)
//         {
//           _context.Dispose();
//         }

//         disposed = true;
//       }
//     }

//     public void Dispose()
//     {
//       // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
//       Dispose(disposing: true);
//       GC.SuppressFinalize(this);
//     }
//   }
// }