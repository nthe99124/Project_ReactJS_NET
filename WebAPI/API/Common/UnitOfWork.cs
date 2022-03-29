using API.Common.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.BaseEntity;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly MyDbContext _context;
        public IGenericReponsitory<Product> ProductResponsitory { get; }
        public IGenericReponsitory<ProductColor> ProductColorResponsitory { get; }
        public IGenericReponsitory<Image> ImageResponsitory { get; }
        public IGenericReponsitory<ProductImage> ProductImageResponsitory { get; }
        public UnitOfWork(MyDbContext context, IGenericReponsitory<Product> Product, IGenericReponsitory<ProductColor> ProductColor, IGenericReponsitory<Image> Image, IGenericReponsitory<ProductImage> ProductImage)
        {
            _context = context;
            ProductResponsitory = Product;
            ProductColorResponsitory = ProductColor;
            ImageResponsitory = Image;
            ProductImageResponsitory = ProductImage;
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
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}