using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitExplore.Entity;

public class Reference
{
    [Key]
    [Required]
    public string Title { get; set; } = null!;

    public ICollection<Publication> Publications { get; set; } = null!;
}