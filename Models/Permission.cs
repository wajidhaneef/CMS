using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    [Table("Permission", Schema = "CMS")]
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionType { get; set; }
        
    }
}
