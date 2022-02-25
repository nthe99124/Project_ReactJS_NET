using Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Image")]
    public class Image : Entity
    {
        [Key]
        public long Id { get; set; }
        public string UrlImage { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
