using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Bill")]
    public class Bill
    {
        public long Id { get; set; }
        public long CustomerID { get; set; }
        // may be not need [ForeignKey()]
        [ForeignKey("CustomerID")]
        public User User { get; set; }
        public DateTime DateOrder { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
    }
}
