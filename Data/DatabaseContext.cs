using FirstMVCapp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCapp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
