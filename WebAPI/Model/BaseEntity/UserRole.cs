using Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("UserRole")]
    public class UserRole : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("UserID")]
        public long UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("RoleID")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
