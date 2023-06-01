using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    [Table("Role", Schema = "CMS")]
    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
