using Microsoft.EntityFrameworkCore;
using TeamManagement.Data.Etintities;

namespace TeamManagement.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<TeamOwner> TeamOwners { get; set; }

        public DbSet<User> Users { get; set; }



    }
}
