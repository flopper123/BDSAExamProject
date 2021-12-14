namespace LitExplore.Entity.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using LitExplore.Core;
using LitExplore.Core.Publication;

public class PublicationRepository : AbsRepository, IPublicationRepository
{
    public PublicationRepository(ILitExploreContext ctx) : base(ctx) { }

    /// <summary>
    /// Updates the context such that the title of @publication 
    /// maps to the details
    /// 
    /// - If another entry already exists for the given title
    ///   it will overwrite the saved details by replcaing it 
    ///   with @publication and return Status.UPDATED
    /// 
    /// - If no entry exists for the user, it will add the new 
    ///   entry and return Status.CREATED
    /// </summary>
    public async Task<Status> UpdateAsync(PublicationDtoDetails publication)
    {
        Publication? db_pub = (await _context.Publications.FindAsync(publication.Title));

        Status status = Status.Created;

        if (db_pub == null) await _context.Publications.AddAsync(publication.ConvertToPublication()); 
        else
        {
            db_pub = publication.ConvertToPublication();
            status = Status.Updated;
        }

        await _context.SaveChangesAsync();
        return status;
    }

    public async Task<Status> DeleteAsync(PublicationDtoTitle key) //O(N)
    {
        Publication? p = await _context.Publications.FindAsync(key.Title);
        
        if (p == null) return Status.NotFound;

        _context.Publications.Remove(key.ConvertToDB());
        await _context.SaveChangesAsync();
        return Status.Deleted;
    }

    public async Task<PublicationDtoDetails?> ReadAsync(PublicationDtoTitle pubTitle)
    {
        Publication? pub = await _context.Publications.FindAsync(pubTitle.Title);
        return (pub != null) ? pub.ConvertToDetails() : null;
    }
    
    /// Should return all elements of the collection in async one by one, 
    /// but not sure if it works or how to test
    public async IAsyncEnumerable<PublicationDtoDetails> ReadAllAsync()
    {
        var all =  _context.Publications.GetAsyncEnumerator(); //O(N) _
        while (await all.MoveNextAsync()) {
            yield return all.Current.ConvertToDetails();
        }
    }

    // TO:DO implement at some point
    public override void Dispose() {}
}