using API.Common;
using Model.ViewModel.Product;
using System.Threading.Tasks;

namespace API.Service.Interface
{
    public interface IProductService : IGenericService<ProductViewModel>
    {
        RestOutput<ProductViewModel> GetAllProductPaging(int pageIndex);
        Task<RestOutput<ProductViewModel>> GetProductByAnyPoint(ProductViewModel pro, int pageIndex);
    }
}
