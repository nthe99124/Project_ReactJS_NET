using Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("ProductImage")]
    public class ProductImage : Entity
    {
        [Key, ForeignKey("ProductID")]
        public long ProductID { get; set; }
        public virtual Product Product { get; set; }
        [Key, ForeignKey("ImageID")]
        public long ImageID { get; set; }
        public virtual Image Image { get; set; }
    }
}
