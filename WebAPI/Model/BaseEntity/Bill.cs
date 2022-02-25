using Microsoft.EntityFrameworkCore;
using Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Bill")]
    [Index(nameof(DateOrder))]
    public class Bill : Entity
    {
        public long Id { get; set; }
        public long CustomerID { get; set; }
        // may be not need [ForeignKey()]
        [ForeignKey("CustomerID")]
        public User User { get; set; }
        public DateTime DateOrder { get; set; }
        public string Address { get; set; }
        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public BillStatus BillStatus { get; set; }
        public string Phone { get; set; }
    }
}
