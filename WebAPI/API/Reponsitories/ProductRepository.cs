using API.Common;
using API.Common.Interface;
using API.Reponsitories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.BaseEntity;
using Model.Common;
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
        public ProductRepository(MyDbContext context) : base(context) { }
        // vấn đề: Nếu Constructor không có Paramter thì sẽ không báo lỗi, nếu có nhưng hiện tại sẽ có lỗi khi dùng DI
        public async Task<RestOutput<Product_Brand_Color_Img>> GetAllProductRepository(int pageIndex = 0)
        {
            try
            {
                var getProSQL = @"SELECT P.ID AS IdPro, P.Name AS NamePro, P.Description, B.Name AS 'NameBrand', P.Price, P.PromotionPrice, P.[Option], P.Type, P.Warranty, P.Weight, P.Size
                    FROM Product P
                    LEFT JOIN Brand B ON P.BrandID = B.Id
                    LEFT JOIN ProductColor PC ON P.Id = PC.ProductID";
                var para = new List<SqlParameter>();
                //para.Add(new SqlParameter("@Name", "Laptop%"));
                var paging = new Paging { pageFind = pageIndex, pagingOrderBy = "NamePro" };
                var rs = await this.SqlQuery(getProSQL, paging);
                var lstProduct = (from rw in rs.AsEnumerable()
                                  select new Product_Brand_Color_Img()
                                  {
                                      IdPro = Convert.ToUInt32(rw["IdPro"]),
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
                                  }).ToList();
                return new RestOutput<Product_Brand_Color_Img>
                {
                    Data = lstProduct,
                    // tính Total Records có cần query riêng? nếu chung query thì thấy thế nào?
                    // Hiện tại ở đây chỉ lấy số lượng bản ghi đó trong 1 trang
                    //TotalRecords = lstProduct.Count,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RestOutput<Product_Brand_Color_Img>> GetProductByAnyPoint(Product_Brand_Color_Img pro, int pageIndex = 0)
        {
            try
            {
                // nếu muốn filter hết, thì phải if check tất cả các thuộc tính????
                var para = new List<SqlParameter>();

                var getProSQL = @"SELECT P.ID AS IdPro, P.Name AS NamePro, P.Description, B.Name AS 'NameBrand', P.Price, P.PromotionPrice, P.[Option], P.Type, P.Warranty, P.Weight, P.Size
                    FROM Product P
                    LEFT JOIN Brand B ON P.BrandID = B.Id
                    LEFT JOIN ProductColor PC ON P.Id = PC.ProductID WHERE 1 = 1";


                //var linqQuerySyntax = await (from p in this._context.Products
                //                                 // multiple column
                //                             join br in this._context.Brands on new { x1 = p.BrandID, x2 = p.Name } equals new { x1 = br.Id, x2 = br.Name } into bp
                //                             from b in bp.DefaultIfEmpty()
                //                             join pcp in this._context.ProductColor on p.Id equals pcp.ProductID into pcpr
                //                             from pc in pcpr.DefaultIfEmpty()
                //                             where p.Name == pro.NamePro
                //                             group new { p, b } by new
                //                             {
                //                                 p.Id,
                //                                 p.Description,
                //                                 //NamePro = p.Name,
                //                                 NameBrand = b.Name,
                //                                 p.Price,
                //                                 p.PromotionPrice,
                //                                 p.Option,
                //                                 p.Type,
                //                                 p.Warranty,
                //                                 p.Weight,
                //                                 p.Size,
                //                             } into g
                //                             select new Product_Brand_Color_Img()
                //                             {
                //                                 IdPro = g.Key.Id,
                //                                 NamePro = g.Count().ToString(),
                //                                 Description = g.Key.Description,
                //                                 NameBrand = g.Key.NameBrand,
                //                                 Price = g.Key.Price,
                //                                 PromotionPrice = g.Key.PromotionPrice,
                //                                 Option = g.Key.Option,
                //                                 Type = g.Key.Type,
                //                                 Warranty = g.Key.Warranty,
                //                                 Weight = g.Key.Weight,
                //                                 Size = g.Key.Size,
                //                             }).ToListAsync();

                //lỗi khi join nhiều trường
                //var linqMethodSyntax = this._context.Products.Join(this._context.Brands, p => new { p.BrandID, p.Name }, b => new { b.Id, b.Name }, (p, b) => new { p, b });

                // tại sao Brand (p,b) => b lại là IEnumarable<>
                //var linqMethodSyntax = this._context.Products.GroupJoin(this._context.Brands, p => p.BrandID, b => b.Id, (p, b) => p.BrandID == b.Id;

                var paging = new Paging { pageFind = pageIndex, pagingOrderBy = "P.Name" };
                //var rs = await this.SqlQuery(getProSQL, para, new Paging { pageSize = 5, pageFind = 2, pagingOrderBy = "Name", typeSort = "desc" });
                var rs = await this.SqlQuery(getProSQL, paging, para);
                var lstProduct = (from rw in rs.AsEnumerable()
                                  select new Product_Brand_Color_Img()
                                  {
                                      IdPro = Convert.ToUInt32(rw["IdPro"]),
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
                                  }).ToList();
                return new RestOutput<Product_Brand_Color_Img>
                {
                    Data = lstProduct,
                };
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
