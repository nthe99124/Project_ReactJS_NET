using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public long Id { get; set; }
        public string UrlImage { get; set; }
    }
}
