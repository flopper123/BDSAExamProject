using LitExplore.Core.Publication;

namespace LitExplore.Entity.Entities;

public class Publication //: PublicationTitle
{    
    [Key]
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Author { get; set; } = "?";

    [Required]
    public string Abstract { get; set; } = "?";

    [Required]
    public ICollection<PublicationTitle> References { get; set; } = new List<PublicationTitle>(); //? Should this also be converted.

    [Required]
    public DateTime Time { get; set; } = DateTime.Now;

    [Required]
    public virtual ICollection<KeyWord> Keywords { get; set; } = new List<KeyWord>();
    //! Because stupid entityFramework dont save collections of primitives you wil build it yourself.
    //! 2 ways either have the Keywords as an entity, or do some text parsing and save it as a string.
}

public class PublicationTitle{ //TODO: Maybe move to seperate class dunno.. keeping it as the core elements.
    
    //! Apparently I'm not allowed to have the key in a class that gets : in other class.
    [Required]
    public string Title { get; set; } = null!;
}

