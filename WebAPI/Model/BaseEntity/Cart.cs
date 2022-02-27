using Model.Common;
using System;
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
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public User User { get; set; }
        public int QuantityPurschased { get; set; }

    }
}
