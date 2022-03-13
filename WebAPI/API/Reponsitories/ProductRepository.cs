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
using static System.Net.Mime.MediaTypeNames;

namespace API.Reponsitories
{
    public class ProductRepository : GenericReponsitory<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork context) : base(context) { }
        // vấn đề: Nếu Constructor không có Paramter thì sẽ không báo lỗi, nếu có nhưng hiện tại sẽ có lỗi khi dùng DI
        public async Task<RestOutput<Product_Brand_Color_Img>> GetAllProductRepository(int pageIndex = 0)
        {
            try
            {
                var getProSQL = @"SELECT P.ID AS IdPro, P.Name AS NamePro, P.Description, B.Name AS 'NameBrand', P.Price, P.PromotionPrice, P.[Option], P.Type, P.Warranty, P.Weight, P.Size, I.UrlImage AS UrlImage, C.ColorName AS Color 
                    FROM Product P
                    LEFT JOIN Brand B ON P.BrandID = B.Id
                    LEFT JOIN ProductColor PC ON P.Id = PC.ProductID
                    LEFT JOIN Color C ON C.Id = PC.ColorID
                    LEFT JOIN ProductImage PIM ON P.Id = PIM.ProductID
                    LEFT JOIN Image I ON PIM.ImageID = I.Id";
                var para = new List<SqlParameter>();
                //para.Add(new SqlParameter("@Name", "Laptop%"));
                var paging = new Paging { pageFind = pageIndex, pagingOrderBy = "NamePro" };
                //var rs = await this.SqlQuery(getProSQL, para, new Paging { pageSize = 5, pageFind = 2, pagingOrderBy = "Name", typeSort = "desc" });
                var rs = await this.SqlQuery(getProSQL, para, paging);
                var lstProduct = (from rw in rs.AsEnumerable()
                                  select new Product_Brand_Color_Img()
                                  {
                                      Id = Convert.ToUInt32(rw["IdPro"]),
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
                                      UrlImage = rw["UrlImage"].ToString(),
                                      Color = rw["Color"].ToString(),
                                  }).ToList();
                return new RestOutput<Product_Brand_Color_Img>
                {
                    Data = lstProduct,
                    // tính Total Records có cần query riêng? nếu chung query thì thấy thế nào?
                    // Hiện tại ở đây chỉ lấy số lượng bản ghi đó trong 1 trang
                    TotalRecords = lstProduct.Count,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<List<Product_Brand_Color_Img>> GetProductByAnyPoint(Product product, Brand brand, Model.BaseEntity.Color color, Image img, int pageIndex)
        //{
        //    try
        //    {
        //        var para = new List<SqlParameter>();
        //        var getProSQL = "SELECT * FROM Product WHERE 1=1";
        //        if (product.Name is not null)
        //        {
        //            getProSQL += " AND Name = @Name";
        //            para.Add(new SqlParameter("@Name", product.Name));
        //        }
        //        var paging = new Paging { pageFind = pageIndex, pagingOrderBy = "Name" };
        //        //var rs = await this.SqlQuery(getProSQL, para, new Paging { pageSize = 5, pageFind = 2, pagingOrderBy = "Name", typeSort = "desc" });
        //        var rs = await this.SqlQuery(getProSQL, para, paging);
        //        var lstProduct = (from rw in rs.AsEnumerable()
        //                          select new Product_Brand_Color_Img()
        //                          {
        //                              Id = Convert.ToUInt32(rw["long"]),
        //                              NamePro = rw["NamePro"].ToString(),
        //                              Description = rw["Description"].ToString(),
        //                              NameBrand = rw["NameBrand"].ToString(),
        //                              Price = Convert.ToDecimal(rw["Price"]),
        //                              PromotionPrice = Convert.ToDecimal(rw["PromotionPrice"]),
        //                              Option = rw["Option"].ToString(),
        //                              Type = Convert.ToInt32(rw["Type"]),
        //                              Warranty = Convert.ToDecimal(rw["Warranty"]),
        //                              Weight = Convert.ToDecimal(rw["Weight"]),
        //                              Size = rw["Size"].ToString(),
        //                              Image = rw["Image"].ToString(),
        //                              Color = rw["Color"].ToString(),
        //                          }).ToList();
        //        return lstProduct;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //public async Task<IActionResult>
    }
}
