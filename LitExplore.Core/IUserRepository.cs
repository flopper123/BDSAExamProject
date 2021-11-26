namespace LitExplore.Core;

public interface IUserRepository
{
    Task<UserDto> CreateAsync(UserCreateDto User);
    Task<IReadOnlyCollection<UserDto>> ReadAsync();
    Task<UserDto> ReadAsync(int UserId); // Funky stuff with Option see Rasmus lecture #10 commit
    Task<Status> UpdateAsync(int id, UserUpdateDto User);
    Task<Status> DeleteAsync(int UserId);
}
