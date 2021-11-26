namespace LitExplore.Core;
public record UserDto
{
    public int Id { get; init; }
    
}
public record UserCreateDto { }
public record UserUpdateDto : UserCreateDto
{
    public int Id { get; init; }
}
public record UserDetailsDto: UserDto
{
    
}
