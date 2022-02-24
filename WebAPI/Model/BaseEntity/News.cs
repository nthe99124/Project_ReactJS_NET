using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("News")]
    public class News
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public long NewsImageID { get; set; }
        [ForeignKey("NewsImageID")]
        public NewsImage NewsImage { get; set; }

    }
}
