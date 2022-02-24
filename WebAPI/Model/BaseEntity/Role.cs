using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.BaseEntity
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
