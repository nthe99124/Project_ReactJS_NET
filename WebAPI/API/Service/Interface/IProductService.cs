using API.Common;
using System.Threading.Tasks;
using Model.DTOs.Product;

namespace API.Service.Interface
{
    public interface IProductService : IGenericService<ProductDto>
    {
        RestOutput<ProductDto> GetAllProductPaging(int pageIndex);
        Task<RestOutput<ProductDto>> GetProductByAnyPoint(ProductDto pro, int pageIndex);
    }
}
