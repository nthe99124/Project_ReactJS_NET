using API.Common.Interface;
using Model.BaseEntity;
using System.Collections.Generic;

namespace API.Repositories.Interface
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        Dictionary<long, long> GetProductIdImageId(List<string> urlImg);
        Dictionary<long, long> GetProductIdImageIdByProductId(long? productID);
    }
}
