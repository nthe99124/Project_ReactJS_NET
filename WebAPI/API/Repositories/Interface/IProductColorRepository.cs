using API.Common.Interface;
using Model.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace API.Repositories.Interface
{
    public interface IProductColorRepository : IGenericRepository<ProductColor>
    {
        Dictionary<int, long> GetProductIdColorId(Expression<Func<ProductColor, bool>> predicate);
    }
}
