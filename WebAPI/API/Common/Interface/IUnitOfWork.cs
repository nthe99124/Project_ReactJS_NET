
using API.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.BaseEntity;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IUnitOfWork
    {
        // don't understand why using UnitOfWork to use Function CommitAsync() but not always in the Create, Update, ... methods in GenericRespository
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
        Task CommitAsync();
        void Commit();
        DbSet<T> Set<T>() where T : class;


    }
}