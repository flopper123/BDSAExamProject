namespace LitExplore.Entity.Entities;

public class PublicationTitle
{ //TODO: Maybe move to seperate class dunno.. keeping it as the core elements.
    
    //! Apparently I'm not allowed to have the key in a class that gets : in other class.
    [Key]
    [Required]
    public string Title { get; set; } = null!;
 
    //Navigation Property
    public string PublicationId { get; set; }
    public Publication Publication { get; set; }
}

