using System.Collections.Generic;
using Model.BaseEntity;

namespace Model.ViewModel.Product
{
    public class ProductBrandColorImg
    {
        public long IdPro { get; set; }
        public string NamePro { get; set; }
        public string Description { get; set; }
        public string NameBrand { get; set; }
        public int? IdBrand { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public string Option { get; set; }
        public int? Type { get; set; }
        public decimal Warranty { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public List<ProductImage> ProductImage { get; set; }
        public List<ProductColor> ProductColor { get; set; }
    }
}
