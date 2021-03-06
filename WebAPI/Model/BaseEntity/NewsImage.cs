using Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("NewsImage")]
    public class NewsImage : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("NewsID")]
        public long NewsId { get; set; }
        public virtual News News { get; set; }
        [ForeignKey("ImageID")]
        public long ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
