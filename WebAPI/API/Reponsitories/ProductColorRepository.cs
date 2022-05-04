using API.Common;
using API.Common.Interface;
using API.Reponsitories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Model.BaseEntity;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Reponsitories
{
    public class ProductColorRepository : GenericReponsitory<ProductColor>, IProductColorRepository
    {
        public ProductColorRepository(MyDbContext context) : base(context) { }
        public Dictionary<int, long> GetProductIdColorID(Expression<Func<ProductColor, bool>> predicate)
        {
            var lstProColor = this._context.ProductColor.Where(predicate).Select(pc => new { pc.ProductID, pc.ColorID }).ToDictionary(x => x.ColorID, x => x.ProductID);
            return lstProColor;
        }
    }
}
