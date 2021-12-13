namespace LitExplore.Entity.Entities;

public class Publication
{    
    [Key]
    [Required]
    public string Title { get; set; } = null!;

    public ICollection<string> References { get; set; } = null!;
}