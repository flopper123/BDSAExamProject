using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitExplore.Entity;

public class Reference
{
    public int Id { get; set; }
    
    //[Required]
    //[ForeignKey("Title")]
    [Key]
    public string Title { get; set; }
    
    public ICollection<Publication> Publications { get; set; }
}