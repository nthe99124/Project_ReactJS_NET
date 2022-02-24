using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("User")]
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        // may be not need ICollection<Bill> when class Bill have public User User
        public ICollection<Bill> Bills { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }
}
