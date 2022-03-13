using API.Common;
using API.Common.Interface;
using Model.BaseEntity;
using Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Reponsitories.Interface
{
    public interface IProductRepository : IGenericReponsitory<Product>
    {
        Task<RestOutput<Product_Brand_Color_Img>> GetAllProductRepository(int pageIndex);
        //Task<List<Product_Brand_Color_Img>> GetProductByAnyPoint(Product product, Brand brand, Color color, Image img, int pageIndex);
    }
}
