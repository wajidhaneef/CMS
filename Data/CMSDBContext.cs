using CMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
    public class CMSDBContext : DbContext
    {
        public CMSDBContext(DbContextOptions<CMSDBContext> options): base(options) { }
        public User Users { get; set; }
        public Role Roles { get; set; }
        public Permission Permissions { get; set; }

        
    }
}
