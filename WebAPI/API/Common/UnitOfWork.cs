using API.Common.Interface;
using Microsoft.EntityFrameworkCore;
using Model.BaseEntity;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace API.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly MyDbContext _context;
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<ProductColor> ProductColorRepository { get; }
        public IGenericRepository<Image> ImageRepository { get; }
        public IGenericRepository<ProductImage> ProductImageRepository { get; }
        public UnitOfWork(MyDbContext context, IGenericRepository<Product> product, IGenericRepository<ProductColor> productColor, IGenericRepository<Image> image, IGenericRepository<ProductImage> productImage)
        {
            _context = context;
            ProductRepository = product;
            ProductColorRepository = productColor;
            ImageRepository = image;
            ProductImageRepository = productImage;
        }

        public async Task CommitAsync()
        {
            //// sử dụng Using thì không cần Dispose
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //        scope.Complete();
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //}
            await _context.SaveChangesAsync();
        }
        public void Commit()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}