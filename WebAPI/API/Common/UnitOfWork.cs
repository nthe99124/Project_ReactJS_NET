using API.Common.Interface;
using API.Controllers;
using API.Repositories;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.BaseEntity;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace API.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        private readonly ILogger<ProductRepository> _productLogger;
        readonly MyDbContext _context;

        public IBillRepository BillRepository => new BillRepository(_context);

        public IBillStatusRepository BillStatusRepository => new BillStatusRepository(_context);

        public IBrandRepository BrandRepository => new BrandRepository(_context);

        public ICartRepository CartRepository => new CartRepository(_context);

        public IColorRepository ColorRepository => new ColorRepository(_context);

        public IFavoriteListRepository FavoriteListRepository => new FavoriteListRepository(_context);

        public INewsImageRepository NewsImageRepository => new NewsImageRepository(_context);

        public INewsRepository NewsRepository => new NewsRepository(_context);

        public IRoleRepository RoleRepository => new RoleRepository(_context);

        public IUserRepository UserRepository => new UserRepository(_context);

        public IProductRepository ProductRepository => new ProductRepository(_context, _productLogger);

        public IProductColorRepository ProductColorRepository => new ProductColorRepository(_context);

        public IImageRepository ImageRepository => new ImageRepository(_context);

        public IProductImageRepository ProductImageRepository => new ProductImageRepository(_context);

        public IUserRoleRepository UserRoleRepository => new UserRoleRepository(_context);

        public UnitOfWork(
            ILogger<UnitOfWork> logger,
            MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}