using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("NewsImage")]
    public class NewsImage
    {
        [Key]
        public long Id { get; set; }
        public long NewsID { get; set; }
        [ForeignKey("NewsID")]
        public News News { get; set; }
        public long ImageID { get; set; }
        [ForeignKey("ImageID")]
        public Image Image { get; set; }
    }
}
