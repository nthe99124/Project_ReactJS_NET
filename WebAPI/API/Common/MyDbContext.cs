using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.BaseEntity;

namespace API.Common
{
    public class MyDbContext : DbContext
    {
        // With lower version we can use "enable-migrations –EnableAutomaticMigration:$true" to create file (Configuration)
        // AutomaticMigrationsEnabled = true; -- only add table or bla bla but that be lost data
        // AutomaticMigrationDataLossAllowed = true; -- 
        public IConfiguration Config;
        public MyDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Config = configuration;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductImage>().HasKey(table => new
            {
                ImageID = table.ImageId,
                ProductID = table.ProductId
            });
            builder.Entity<ProductColor>().HasKey(table => new
            {
                ColorID = table.ColorId,
                ProductID = table.ProductId
            });
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillStatus> BillStatus { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<FavoriteList> FavoriteList { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsImage> NewsImage { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductColor> ProductColor { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    }
}
