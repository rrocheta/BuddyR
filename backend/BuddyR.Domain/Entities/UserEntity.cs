﻿namespace BuddyR.Domain.Entities
{
    public class UserEntity
    {

        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

    }
}
