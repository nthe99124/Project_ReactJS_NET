using Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("UserRole")]
    public class UserRole : Entity
    {
        [Key]
        public long Id { get; set; }
        public long UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }
    }
}
