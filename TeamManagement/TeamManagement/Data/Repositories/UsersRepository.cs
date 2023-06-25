using TeamManagement.Data.Etintities;

namespace TeamManagement.Data.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(AppDbContext dbContext) : base(dbContext) { }

        public User GetByEmail(string email)
        {
            var result = _dbContext.Users.FirstOrDefault(e => e.Email == email);

            return result;
        }
    }
}
