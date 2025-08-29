using BuddyR.Domain.Entities;

namespace BuddyR.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task AddAsync (UserEntity entity);
    }
}
