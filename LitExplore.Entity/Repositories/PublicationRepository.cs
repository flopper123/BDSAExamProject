namespace LitExplore.Entity.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using LitExplore.Core;
using LitExplore.Core.Publication;

public class PublicationRepository : AbsRepository, IPublicationRepository
{
    public PublicationRepository(ILitExploreContext ctx) : base(ctx) { }

    public Task<Status> UpdateAsync(PublicationDetails publication)
    {
        throw new NotImplementedException();
    }

    public async Task<Status> DeleteAsync(PublicationTitle key) //O(N)
    {
        Publication? p = await _context.Publications.FindAsync(key.Title);
        
        if (p == null) return Status.NotFound;

        _context.Publications.Remove(key.ConvertToDB());
        await _context.SaveChangesAsync();
        return Status.Deleted;
    }

    public async Task<PublicationDetails?> ReadAsync(PublicationTitle pubTitle)
    {
        Publication? pub = await _context.Publications.FindAsync(pubTitle);
        return (pub != null) ? pub.ConvertToDetails() : null;
    }
    
    /// Should return all elements of the collection in async one by one, 
    /// but not sure if it works or how to test
    public async IAsyncEnumerable<PublicationDetails> ReadAllAsync()
    {
        var all =  _context.Publications.GetAsyncEnumerator(); //O(N) _
        while (await all.MoveNextAsync()) {
            yield return all.Current.ConvertToDetails();
        }
    }


    public override void Dispose()
    {
        throw new NotImplementedException();
    }

}