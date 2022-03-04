using API.Common;
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
        public ProductRepository(UnitOfWork context1) : base(context1) { }
        // vấn đề: Nếu Constructor không có Paramter thì sẽ không báo lỗi, nếu có nhưng hiện tại sẽ có lỗi khi dùng DI
        public object GetAllProductRepository()
        {
            //var getProSQL = "SELECT * FROM Product";
            //var getPro = this.SqlQuery(getProSQL);
            return this.DbContext.Products.Select(p => new { id = p.Id, name = p.Name });
        }
    }
}
