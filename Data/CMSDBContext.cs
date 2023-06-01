using CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public CMSDBContext()
        {
                
        }
        public CMSDBContext(DbContextOptions<CMSDBContext> options): base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        public DbSet< Permission> Permissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CMS;Trusted_Connection=True;");
        }
    }
}
