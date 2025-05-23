using System.Data.Entity;
using SE_TRENDZZ.Models;

namespace SE_TRENDZZ.Data
{
    public class SETrendzzDBContext : DbContext
    {
        public SETrendzzDBContext() : base("SETrendzzDB") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
