using API.Common.Interface;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        readonly MyDbContext _context;

        public IBillRepository BillRepository { get; }
        public IBillStatusRepository BillStatusRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public ICartRepository CartRepository { get; }
        public IColorRepository ColorRepository { get; }
        public IFavoriteListRepository FavoriteListRepository { get; }
        public INewsImageRepository NewsImageRepository { get; }
        public INewsRepository NewsRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductColorRepository ProductColorRepository { get; }
        public IImageRepository ImageRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }

        public UnitOfWork(
            ILogger<UnitOfWork> logger,
            MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public UnitOfWork(
            ILogger<UnitOfWork> logger,
            MyDbContext context,
            IBillRepository bill,
            IBillStatusRepository billStatus,
            IBrandRepository brand,
            IProductImageRepository productImage,
            ICartRepository cart,
            IColorRepository color,
            IFavoriteListRepository favoriteList,
            INewsRepository news,
            INewsImageRepository newsImage,
            IRoleRepository role,
            IUserRepository user,
            IProductRepository product,
            IProductColorRepository productColor,
            IImageRepository image,
            IUserRoleRepository userRoleRepository)
        {
            _logger = logger;
            _context = context;
            BillRepository = bill;
            BillStatusRepository = billStatus;
            BrandRepository = brand;
            CartRepository = cart;
            ColorRepository = color;
            FavoriteListRepository = favoriteList;
            NewsImageRepository = newsImage;
            RoleRepository = role;
            UserRepository = user;
            ProductImageRepository = productImage;
            ProductRepository = product;
            ProductColorRepository = productColor;
            ImageRepository = image;
            NewsRepository = news;
            UserRoleRepository = userRoleRepository;
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