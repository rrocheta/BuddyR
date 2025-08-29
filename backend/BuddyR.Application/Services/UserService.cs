using BuddyR.Domain.Entities;
using BuddyR.Domain.Interfaces;

namespace BuddyR.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
          => await _userRepository.GetAllUsersAsync();


        public Task<UserEntity?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Invalid user id");

            return _userRepository.GetByIdAsync(id);
        }

        public Task<UserEntity?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("Email is required");
            }

            return _userRepository.GetByEmailAsync(email);
        }

        public async Task<UserEntity> CreateAsync(string name, string email)
        {

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");

            var user = new UserEntity()
            {
                Name = name,
                Email = email,
                PasswordHash = "placeholder"   // TODO later
            };

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity entity)
        {
            var user = await _userRepository.GetByIdAsync(entity.Id);
            if (user == null)
                throw new InvalidOperationException("User not found");

            if (!string.IsNullOrWhiteSpace(entity.Name))
                user.Name = entity.Name;
            if (!string.IsNullOrWhiteSpace(entity.Email))
                user.Email = entity.Email;

            await _userRepository.UpdateAsync(user);

            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException("User not found");

            await _userRepository.DeleteAsync(id);
        }

    }
}
