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
    public interface IProductColorRepository : IGenericReponsitory<ProductColor>
    {
        Dictionary<int, long> GetProductIdColorID(Expression<Func<ProductColor, bool>> predicate);
    }
}
