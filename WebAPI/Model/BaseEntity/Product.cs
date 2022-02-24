using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int BrandID { get; set; }
        [ForeignKey("BrandID")]
        public Brand Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public long ProductImageID { get; set; }
        [ForeignKey("ProductImage")]
        public ProductImage ProductImage { get; set; }
        public

    }
}
