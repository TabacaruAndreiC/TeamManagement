using TeamManagement.Data.Etintities;

namespace TeamManagement.Data.Repositories
{
    public class TeamsRepository:BaseRepository<Team>
    {
        public TeamsRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
