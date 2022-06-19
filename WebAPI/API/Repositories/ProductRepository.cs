using API.Common;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Model.BaseEntity;
using Model.ViewModel.Product;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context) { }

        public RestOutput<ProductViewModel> GetAllProductPaging(int pageIndex = 1)
        {
            try
            {
                var rs = GetAllPaging(new Paging(pageIndex));
                var lstProduct = rs.data
                    .Select(p => new ProductViewModel()
                    {
                        IdPro = p.Id,
                        NamePro = p.Name,
                        Description = p.Description,
                        NameBrand = p.Brand.Name,
                        Price = p.Price,
                        PromotionPrice = p.PromotionPrice,
                        Option = p.Option,
                        Type = p.Type,
                        Warranty = p.Warranty,
                        Weight = p.Weight,
                        Size = p.Size,
                    });

                return new RestOutput<ProductViewModel>(lstProduct.ToList(), rs.count);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<RestOutput<ProductViewModel>> GetProductByAnyPoint(ProductViewModel pro, int pageIndex = 1)
        {
            try
            {
                var lstProduct = await _context.Products
                        .Include(p => p.Brand)
                        .Include(p => p.ProductColor)
                        .Select(p => new ProductViewModel()
                        {
                            IdPro = p.Id,
                            NamePro = p.Name,
                            Description = p.Description,
                            NameBrand = p.Brand.Name,
                            Price = p.Price,
                            PromotionPrice = p.PromotionPrice,
                            Option = p.Option,
                            Type = p.Type,
                            Warranty = p.Warranty,
                            Weight = p.Weight,
                            Size = p.Size,
                        }).ToListAsync();

                lstProduct = FindByAnyPoint<ProductViewModel>(lstProduct, pro);
                lstProduct = PagingResult<ProductViewModel>(lstProduct, new Paging(pageIndex));

                return new RestOutput<ProductViewModel>(lstProduct, lstProduct.Count);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
