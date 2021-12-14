namespace LitExplore.Entity.Entities;

public class PublicationTitle
{ //TODO: Maybe move to seperate class dunno.. keeping it as the core elements.
    
    //! Apparently I'm not allowed to have the key in a class that gets : in other class.
    [Required]
    public string Title { get; set; } = null!;

    public List<Publication>? Publication {get; set;} 
}

