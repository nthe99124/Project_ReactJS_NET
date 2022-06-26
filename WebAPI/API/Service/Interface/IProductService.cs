using System.Threading.Tasks;
using API.Common;
using Microsoft.AspNetCore.Mvc;
using Model.BaseEntity;
using Model.ViewModel.Product;

namespace API.Service.Interface
{
    public interface IProductService
    {
        RestOutput<ProductViewModel> GetAllProductPaging(int pageIndex);
        Task<RestOutput<ProductViewModel>> GetProductByAnyPoint(ProductViewModel pro, int pageIndex);
        Task<RestOutputCommand<ProductViewModel>> InsertProduct(ProductViewModel pro);
        Task<RestOutputCommand<ProductViewModel>> UpdateProduct(int id, ProductViewModel pro);
    }
}
