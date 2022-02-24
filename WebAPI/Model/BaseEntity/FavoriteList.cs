using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("FavoriteList")]
    public class FavoriteList
    {
        [Key]
        public long Id { get; set; }
        public long ProductColorID { get; set; }
        [ForeignKey("ProductColorID")]
        public ProductColor ProductColor { get; set; }
        public long CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public User User { get; set; }

    }
}
