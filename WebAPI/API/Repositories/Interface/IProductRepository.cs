using API.Common;
using API.Common.Interface;
using Model.BaseEntity;
using System.Threading.Tasks;
using Model.DTOs.Product;

namespace API.Repositories.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        RestOutput<ProductDto> GetAllProductPaging(int pageIndex = 1);
        Task<RestOutput<ProductDto>> GetProductByAnyPoint(ProductDto pro, int pageIndex = 1);
    }
}
