using TeamManagement.Data.Etintities;

namespace TeamManagement.Data.Repositories
{
    public class TeamOwnersRepository : BaseRepository<TeamOwner>
    {
        public TeamOwnersRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public TeamOwner? GetByUserId(Guid userId)
        {
            var result = _dbContext.TeamOwners.FirstOrDefault(x => x.UserId == userId);

            return result;
            
        }
    }
}
