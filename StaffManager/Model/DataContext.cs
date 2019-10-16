using System.Data.Entity;

namespace StaffManager.Model
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
    }
}
