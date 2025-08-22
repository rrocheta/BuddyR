using BuddyR.Domain.Entities;

namespace BuddyR.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task AddAsync (UserEntity entity);
    }
}
