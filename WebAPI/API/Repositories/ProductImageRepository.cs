using API.Common;
using API.Repositories.Interface;
using Model.BaseEntity;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(MyDbContext context) : base(context) { }
        public Dictionary<long, long> GetProductIdImageId(List<string> UrlImg)
        {
            var lstProColor = this._context.ProductImage
                                                    .Join(this._context.Images, pi => pi.ImageId, i => i.Id, (pi, i) => new { ProductImage = pi, Image = i })
                                                    .Where(pi => UrlImg.Contains(pi.Image.UrlImage))
                                                    .Select(pi => new { ProductID = pi.ProductImage.ProductId, ImageID = pi.ProductImage.ImageId })
                                                    .ToDictionary(x => x.ImageID, x => x.ProductID);
            return lstProColor;
        }
        public Dictionary<long, long> GetProductIdImageIdByProductId(long ProductID)
        {
            var lstProColor = this._context.ProductImage
                                                    .Where(pi => pi.ProductId.Equals(ProductID))
                                                    .Select(pi => new { ProductID = pi.ProductId, ImageID = pi.ImageId })
                                                    .ToDictionary(x => x.ImageID, x => x.ProductID);
            return lstProColor;
        }
    }
}
