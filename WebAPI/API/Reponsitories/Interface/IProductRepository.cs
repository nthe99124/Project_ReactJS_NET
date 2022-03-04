using API.Common;
using API.Common.Interface;
using Model.BaseEntity;

namespace API.Reponsitories.Interface
{
    public interface IProductRepository : IGenericReponsitory<Product>
    {
        object GetAllProductRepository();
    }
}
