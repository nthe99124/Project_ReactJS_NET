using Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Color")]
    public class Color : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ColorName { get; set; }
    }
}
