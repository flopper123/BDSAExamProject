namespace LitExplore.Entity.Entities;

public class Publication
{    
    [Key]
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Author { get; set; } = "?";

    [Required]
    public string Abstract { get; set; } = "?";

    [Required]
    public ICollection<string> References { get; set; } = new List<string>();

    [Required]
    public DateTime Time { get; set; } = DateTime.Now;

    [Required]
    public ICollection<string> Keywords { get; set; } = new List<string>();
}
