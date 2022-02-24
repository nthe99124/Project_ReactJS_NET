using Microsoft.EntityFrameworkCore;
using Model.BaseEntity;

namespace Model.Common
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
