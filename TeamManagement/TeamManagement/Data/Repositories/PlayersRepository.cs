using TeamManagement.Data.Etintities;

namespace TeamManagement.Data.Repositories
{
    public class PlayersRepository:BaseRepository<Player>
    {
        public PlayersRepository(AppDbContext dbContext) : base(dbContext) { }
            
    }
}
