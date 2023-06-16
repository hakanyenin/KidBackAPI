using Core.CEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public class KidBackContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Local database context
            optionsBuilder.UseSqlServer(@"Server=localhost\mssqllocaldb;Database=KidBack7DB;Trusted_Connection=true;TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=KidBack7DB;Trusted_Connection=true;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<UserRoleClaim> UserRoleClaims { get; set; }
        public DbSet<School> Schools { get; set; }
        
    }
}
