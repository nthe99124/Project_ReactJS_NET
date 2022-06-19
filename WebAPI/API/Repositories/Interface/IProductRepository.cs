using API.Common;
using API.Common.Interface;
using Model.BaseEntity;
using Model.ViewModel.Product;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        RestOutput<ProductViewModel> GetAllProductPaging(int pageIndex = 1);
        Task<RestOutput<ProductViewModel>> GetProductByAnyPoint(ProductViewModel pro, int pageIndex = 1);
    }
}
