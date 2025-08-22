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

        public Task<UserEntity?> GetByIdAsync(Guid id)
            => _userRepository.GetByIdAsync(id);

        public Task<UserEntity?> GetByEmailAsync(string email)
            => _userRepository.GetByEmailAsync(email);

        public async Task<UserEntity> CreateAsync(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");

            var user = new UserEntity() 
            { 
                Email = email,
                Name = name,
                PasswordHash = "placeholder"   // TODO later
            };

            await _userRepository.AddAsync(user);
            return user;
        }
    }
}
