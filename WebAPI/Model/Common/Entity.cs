using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Common
{
    public class Entity
    {
        [Required]
        [Column("CreatedOn", TypeName = "DateTime")]
        public DateTime CreatedOn { get; set; }
        [Required]
        [Column("CreatedBy")]
        public long CreatedBy { get; set; }
        [Required]
        [Column("UpdatedOn", TypeName = "DateTime")]
        public DateTime UpdatedOn { get; set; }
        [Required]
        [Column("UpdatedBy")]
        public long UpdatedBy { get; set; }
    }
}
