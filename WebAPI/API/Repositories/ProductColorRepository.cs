using API.Common;
using API.Repositories.Interface;
using Model.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class ProductColorRepository : GenericRepository<ProductColor>, IProductColorRepository
    {
        public ProductColorRepository(MyDbContext context) : base(context) { }
        public Dictionary<int, long> GetProductIdColorId(Expression<Func<ProductColor, bool>> predicate)
        {
            var lstProColor = this._context.ProductColor.Where(predicate).Select(pc => new { ProductID = pc.ProductId, ColorID = pc.ColorId }).ToDictionary(x => x.ColorID, x => x.ProductID);
            return lstProColor;
        }
    }
}
