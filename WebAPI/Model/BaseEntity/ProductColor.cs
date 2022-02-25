using Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("ProductColor")]
    public class ProductColor : Entity
    {
        [Key, ForeignKey("ProductID")]
        public long ProductID { get; set; }
        public virtual Product Product { get; set; }
        [Key, ForeignKey("ColorID")]
        public int ColorID { get; set; }
        public virtual Color Color { get; set; }
    }
}
