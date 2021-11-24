using System.ComponentModel.DataAnnotations;
public class Reference
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    [Required]
    public (Publication, Publication) publications { get; set; }

}