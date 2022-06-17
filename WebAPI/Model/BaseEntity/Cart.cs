using Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Cart")]
    public class Cart : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("CustomerId")]
        public long CustomerId { get; set; }
        public User User { get; set; }
        public int QuantityPurchased { get; set; }

    }
}
