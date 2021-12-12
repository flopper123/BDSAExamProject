using System.Reflection.PortableExecutable;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LitExplore.Core;
using LitExplore.Entity;
using Xunit;


namespace LitExplore.Tests.Entity;

/// <summary>
/// Abstract testing class for EntityFrameworkCore repositories. 
/// The parameterless constructor sets up an in-memory-database in @repository
/// and @context ready for testing.
/// </summary>
/// <typeparam name="T"> The repository to test. Any class that implements AbsRepository </typeparam>
public abstract class AbsRepositoryTests<T> : IDisposable 
    where T : AbsRepository
{
    protected readonly ILitExploreContext context;   
    protected readonly T repository;
  
    public AbsRepositoryTests() {
        // Create in memory connection
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        // Create DB context options
        var builder = new DbContextOptionsBuilder<LitExploreContext>();
        builder.UseSqlite(connection);

        // Create LitExplore context with options
        LitExploreContext ctx = new LitExploreContext(builder.Options);
        ctx.Database.EnsureCreated();
        context = ctx;

        // Seed
        seed();
        context.SaveChanges();

        // Reflection incoming - close your eyes children
        T? n_Rep = (T?) Activator.CreateInstance(typeof(T), context);
        if (n_Rep == null) throw new ArgumentException("Creation exception: Must contain constructor " + typeof(T) + "(ILitExploreContext ctx)");
        
        repository = n_Rep;
    }
    
    
    /// <summary>
    /// Seed the context with initial values. Called in the base constructor
    /// before creating th repository
    /// </summary>
    abstract protected void seed();

    /// <summary>
    /// Override and implement this if there is custom objects that you wish to dispose.
    /// </summary>
    virtual protected void customDispose() { }
    
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        customDispose();
        repository.Dispose();
        GC.SuppressFinalize(this);
    }
}