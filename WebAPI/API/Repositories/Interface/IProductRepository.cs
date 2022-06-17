using API.Common;
using API.Common.Interface;
using Model.BaseEntity;
using Model.ViewModel.Product;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<RestOutput<ProductBrandColorImg>> GetAllProductRepository(int pageIndex);
        Task<RestOutput<ProductBrandColorImg>> GetProductByAnyPoint(ProductBrandColorImg pro, int pageIndex = 0);

    }
}
