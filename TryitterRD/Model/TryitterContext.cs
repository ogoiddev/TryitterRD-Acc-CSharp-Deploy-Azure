using Microsoft.EntityFrameworkCore;

namespace TryitterRD.Model
{
    public class TryitterRDContext : DbContext
    {
        public TryitterRDContext(DbContextOptions<TryitterRDContext> option) : base(option)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}