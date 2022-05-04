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
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Reponsitories
{
    public class ProductImageRepository : GenericReponsitory<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(MyDbContext context) : base(context) { }
        public Dictionary<long, long> GetProductIdImageID(List<string> UrlImg)
        {
            var lstProColor = this._context.ProductImage.Join(this._context.Images, pi => pi.ImageID, i => i.Id, (pi, i) => new { ProductImage = pi, Image = i }).Where(pi => UrlImg.Contains(pi.Image.UrlImage)).Select(pi => new { pi.ProductImage.ProductID, pi.ProductImage.ImageID }).ToDictionary(x => x.ImageID, x => x.ProductID);
            return lstProColor;
        }
        public Dictionary<long, long> GetProductIdImageIDByProductID(long ProductID)
        {
            var lstProColor = this._context.ProductImage.Where(pi => pi.ProductID.Equals(ProductID)).Select(pi => new { pi.ProductID, pi.ImageID }).ToDictionary(x => x.ImageID, x => x.ProductID);
            return lstProColor;
        }
    }
}
