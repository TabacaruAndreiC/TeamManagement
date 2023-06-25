using TeamManagement.Data.Repositories;

namespace TeamManagement.Data
{

    public class UnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public PlayersRepository Players { get; }

        public TeamsRepository Teams { get; }

        public TeamOwnersRepository TeamOwners { get; }

        public UsersRepository Users { get; }


        public UnitOfWork
       (
           AppDbContext dbContext,
           TeamsRepository teamsRepository,
           PlayersRepository playersRepository,
           TeamOwnersRepository teamOwnersRepository,
            UsersRepository usersRepository



       )
        {
            _dbContext = dbContext;
            Teams = teamsRepository;
            Players = playersRepository;
            TeamOwners = teamOwnersRepository;
            Users = usersRepository;


        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }

    }
}
