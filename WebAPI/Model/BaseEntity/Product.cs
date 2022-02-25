using Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Product")]
    public class Product : Entity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int BrandID { get; set; }
        [ForeignKey("BrandID")]
        public Brand Brand { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Column(TypeName = "money")]
        public decimal PromotionPrice { get; set; }
        //option include: CPU,GPU,RAM,ROM,Monitor,Operating,Battery
        public string Option { get; set; }
        // type of product (config in dataType)
        public int Type { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal Warranty { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public ICollection<ProductImage> ProductImage { get; set; }
        public ICollection<ProductColor> ProductColor { get; set; }
    }
}
