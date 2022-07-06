using API.Common;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.BaseEntity;
using Model.ViewModel.Product;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(MyDbContext context, ILogger<ProductRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public RestOutput<ProductViewModel> GetAllProductPaging(int pageIndex = 1)
        {
            try
            {
                var rs = GetAllDataPaging(new Paging(pageIndex));
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
                //_logger.LogInformation("GetAllProductPaging:start Repository ");
                return new RestOutput<ProductViewModel>(lstProduct.ToList(), rs.totalRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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

                var rs = new RestOutput<ProductViewModel>(lstProduct.Count);
                lstProduct = FindByAnyPoint<ProductViewModel>(lstProduct, pro);
                lstProduct = PagingResult<ProductViewModel>(lstProduct, new Paging(pageIndex));
                rs.Data = lstProduct;
                return rs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
