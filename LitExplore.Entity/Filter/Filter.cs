namespace LitExplore.Entity.Filter;

using System.ComponentModel.DataAnnotations;

// Database representation of a filter for a specific user.
public class Filter {
    [Key]
    [Required]
    public int UserId {get; init;}

    [Required]
    public string Serialization { get; init; } = null!;
}