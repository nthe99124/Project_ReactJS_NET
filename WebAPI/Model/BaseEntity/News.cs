using Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("News")]
    public class News : Entity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<NewsImage> NewsImage { get; set; }

    }
}
