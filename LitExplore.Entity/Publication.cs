using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LitExplore.Entity;

public class Publication
{    
    [Key]
    [Required]
    public string Title { get; set; } = null!;
    
    // TO:DO test inverse property
    //[InverseProperty("Title")]
    public ICollection<Reference> References { get; set; } = null!;

}