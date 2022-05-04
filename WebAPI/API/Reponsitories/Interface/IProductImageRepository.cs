using API.Common;
using API.Common.Interface;
using Model.BaseEntity;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Reponsitories.Interface
{
    public interface IProductImageRepository : IGenericReponsitory<ProductImage>
    {
        Dictionary<long, long> GetProductIdImageID(List<string> UrlImg);
        Dictionary<long, long> GetProductIdImageIDByProductID(long ProductID);
    }
}
