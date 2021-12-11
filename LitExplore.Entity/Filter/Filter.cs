namespace LitExplore.Entity.Filter;

using System.ComponentModel.DataAnnotations;

// Database representation of a filter for a specific user.
public class UserFilter {
    [Key]
    [Required]
    public ulong UserId {get; init;}

    [Required]
    public string Serialization { get; init; } = null!;
}