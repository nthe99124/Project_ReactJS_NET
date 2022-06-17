using Microsoft.EntityFrameworkCore;
using Model.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Bill")]
    [Index(nameof(DateOrder))]
    public class Bill : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("CustomerID")]
        public long CustomerId { get; set; }
        public virtual User User { get; set; }
        public DateTime DateOrder { get; set; }
        public string Address { get; set; }
        [ForeignKey("StatusID")]
        public int StatusId { get; set; }
        public virtual BillStatus BillStatus { get; set; }
        public string Phone { get; set; }
    }
}
