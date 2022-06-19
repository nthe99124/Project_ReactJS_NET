﻿using API.Common.Interface;
using API.Repositories;
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
        public IGenericRepository<Bill> BillRepository { get; }
        public IGenericRepository<BillStatus> BillStatusRepository { get; }
        public IGenericRepository<Brand> BrandRepository { get; }
        public IGenericRepository<Cart> CartRepository { get; }
        public IGenericRepository<Color> ColorRepository { get; }
        public IGenericRepository<FavoriteList> FavoriteListRepository { get; }
        public IGenericRepository<NewsImage> NewsImageRepository { get; }
        public IGenericRepository<News> NewsRepository { get; }
        public IGenericRepository<Role> RoleRepository { get; }
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<ProductColor> ProductColorRepository { get; }
        public IGenericRepository<Image> ImageRepository { get; }
        public IGenericRepository<ProductImage> ProductImageRepository { get; }
        public UnitOfWork(MyDbContext context,
            IGenericRepository<Bill> bill,
            IGenericRepository<BillStatus> billStatus,
            IGenericRepository<Brand> brand,
            IGenericRepository<ProductImage> productImage,
            IGenericRepository<Cart> cart,
            IGenericRepository<Color> color,
            IGenericRepository<FavoriteList> favoriteList,
            IGenericRepository<NewsImage> newsImage,
            IGenericRepository<Role> role,
            IGenericRepository<User> user,
            IGenericRepository<Product> product,
            IGenericRepository<ProductColor> productColor,
            IGenericRepository<Image> image)
        {
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
                    Logger.LogError(ex.ToString());
                }
            }
        }
        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}