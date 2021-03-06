using Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Model.Enum.User;

namespace Model.BaseEntity
{
    [Table("User")]
    public class User : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public UserRole UserRole { get; set; }
        // may be not need ICollection<Bill> when class Bill have public User User
        public ICollection<Bill> Bills { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
