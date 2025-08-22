using BuddyR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuddyR.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
