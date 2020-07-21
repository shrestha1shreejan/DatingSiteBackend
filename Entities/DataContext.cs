using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
