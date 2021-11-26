using LitExplore.Core;
using Microsoft.EntityFrameworkCore;

//using Interfaces;

namespace LitExplore.Entity
{
    public class UserRepository : IUserRepository
    {
        private readonly ILitExploreContext _context;
        public UserRepository(ILitExploreContext context) => _context = context;

        public async Task<UserDetailsDto> CreateAsync(UserCreateDto User)
        {
            var toCreate = new User {/*do some stuff with User*/ };
            _context.Users.Add(toCreate);
            await _context.SaveChangesAsync();
            return new UserDetailsDto(
                toCreate.Id
            );
        }

        public async Task<IReadOnlyCollection<UserDto>> ReadAsync()
        {
            return (await _context.Users
                .Select(u => new UserDto(u.Id)).ToListAsync()).AsReadOnly();
        }

        public async Task<UserDto> ReadAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> UpdateAsync(int id, UserUpdateDto User)
        {
            throw new NotImplementedException();
        }
        public async Task<Status> DeleteAsync(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}