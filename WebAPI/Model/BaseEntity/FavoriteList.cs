using Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("FavoriteList")]
    public class FavoriteList : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("ProductID")]
        public long ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("CustomerID")]
        public long CustomerId { get; set; }
        public User User { get; set; }

    }
}
