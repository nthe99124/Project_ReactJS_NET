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
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Reponsitories
{
    public class ProductRepository : GenericReponsitory<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork context) : base(context) { }
        // vấn đề: Nếu Constructor không có Paramter thì sẽ không báo lỗi, nếu có nhưng hiện tại sẽ có lỗi khi dùng DI
        public async Task<List<Product_Brand_Color_Img>> GetAllProductRepository(int pageIndex = 0)
        {
            var getProSQL = "SELECT * FROM Product";
            var para = new List<SqlParameter>();
            //para.Add(new SqlParameter("@Name", "Laptop%"));
            var paging = new Paging { pageFind = pageIndex, pagingOrderBy = "Name" };
            //var rs = await this.SqlQuery(getProSQL, para, new Paging { pageSize = 5, pageFind = 2, pagingOrderBy = "Name", typeSort = "desc" });
            var rs = await this.SqlQuery(getProSQL, para, paging);
            var lstProduct = (from rw in rs.AsEnumerable()
                              select new Product_Brand_Color_Img()
                              {
                                  Id = Convert.ToUInt32(rw["long"]),
                                  NamePro = rw["NamePro"].ToString(),
                                  Description = rw["Description"].ToString(),
                                  NameBrand = rw["NameBrand"].ToString(),
                                  Price = Convert.ToDecimal(rw["Price"]),
                                  PromotionPrice = Convert.ToDecimal(rw["PromotionPrice"]),
                                  Option = rw["Option"].ToString(),
                                  Type = Convert.ToInt32(rw["Type"]),
                                  Warranty = Convert.ToDecimal(rw["Warranty"]),
                                  Weight = Convert.ToDecimal(rw["Weight"]),
                                  Size = rw["Size"].ToString(),
                                  Image = rw["Image"].ToString(),
                                  Color = rw["Color"].ToString(),
                              }).ToList();
            return lstProduct;
        }
    }
}
