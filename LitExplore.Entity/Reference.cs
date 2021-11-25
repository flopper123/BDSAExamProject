using System.ComponentModel.DataAnnotations;

namespace LitExplore.Entity;

public class Reference
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
}