namespace LitExplore.Core.Publication;

public interface IPublicationRepository
{
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
    Task<Status> UpdateAsync(PublicationDtoDetails publication);

    // Delete publication details related to pubtitle
    Task<Status> DeleteAsync(PublicationDtoTitle pubTitle);

    // Attempt to read a single publication from the database
    Task<PublicationDtoDetails?> ReadAsync(PublicationDtoTitle pubTitle); // Funky stuff with Option see Rasmus lecture #10 commit
    
    // Read all publications in the database, and return them as an async enumerable 
    // (i.e. so we can work on some of the objects. while the read finish)
    IAsyncEnumerable<PublicationDtoDetails> ReadAllAsync();
}


// PublicationGraphHandler : Class that builds graph by utilizing PublicationRepository
//  HOW TO BUILD GRAPH: 
//     async readTitles,
//          await async => wait for it to finish
//     async readDetailsForTitles 
//          dont await async => continue like it finished
//     async ApplyFilter()

//  whenever a title is received, Title : 
//
//
//