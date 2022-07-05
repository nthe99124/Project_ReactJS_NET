using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IUnitOfWork
    {
        public IBillRepository BillRepository { get; }
        public IBillStatusRepository BillStatusRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public ICartRepository CartRepository { get; }
        public IColorRepository ColorRepository { get; }
        public IFavoriteListRepository FavoriteListRepository { get; }
        public INewsImageRepository NewsImageRepository { get; }
        public INewsRepository NewsRepository { get; }
        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductColorRepository ProductColorRepository { get; }
        public IImageRepository ImageRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }
        Task CommitAsync();
        void Commit();
        DbSet<T> Set<T>() where T : class;


    }
}