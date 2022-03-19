using Model.Common;
using System;
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
        public long ProductID { get; set; }
        public Product Product { get; set; }
        [ForeignKey("CustomerID")]
        public long CustomerID { get; set; }
        public User User { get; set; }

    }
}
