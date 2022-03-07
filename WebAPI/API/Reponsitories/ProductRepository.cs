using API.Common;
using API.Common.Interface;
using API.Reponsitories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model.BaseEntity;
using System.Linq;
using System.Xml.Linq;

namespace API.Reponsitories
{
    public class ProductRepository : GenericReponsitory<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork context1) : base(context1) { }
        // vấn đề: Nếu Constructor không có Paramter thì sẽ không báo lỗi, nếu có nhưng hiện tại sẽ có lỗi khi dùng DI
        public object GetAllProductRepository()
        {
            var getProSQL = "SELECT * FROM Product";
            var getPro = this.SqlQuery(getProSQL);
            // DbContext is null
            //var m = this.DbContext.Products.Select(p => new { id = p.Id, name = p.Name });
            //return this._dbset.Select(p => new { id = p.Id, name = p.Name });
            return getPro;
        }
    }
}
