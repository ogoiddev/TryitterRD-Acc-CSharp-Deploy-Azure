using Microsoft.EntityFrameworkCore;

namespace TryitterRD.Model
{
    public class TryitterContext : DbContext
    {
        public TryitterContext(DbContextOptions<TryitterContext> option) : base(option)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}