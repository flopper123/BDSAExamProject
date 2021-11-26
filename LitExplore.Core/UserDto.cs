namespace LitExplore.Core;
public record UserDto(int Id);
public record UserCreateDto { }
public record UserUpdateDto(int Id) : UserCreateDto
{
    public int Id { get; init; }
}
public record UserDetailsDto(int Id) : UserUpdateDto(Id)
{
}
