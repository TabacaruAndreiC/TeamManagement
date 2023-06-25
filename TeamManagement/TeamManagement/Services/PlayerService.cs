using Microsoft.Identity.Client;
using TeamManagement.Data;
using TeamManagement.Data.Etintities;
using TeamManagement.Services.Dtos;

namespace TeamManagement.Services
{
    public class PlayerService
    {
        private readonly UnitOfWork _unitOfWork;

        public PlayerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Player> GetAll()
        {
            var result = _unitOfWork.Players.GetAll();
            return result;
            
        }

        public Player GetById(Guid playerId)
        {
            var result = _unitOfWork.Players.GetById(playerId);
            return result;
        }

       

        public List<Player> GetPlayersByTeamId(Guid teamId)
        {
           
            var result = _unitOfWork.Players.GetAll().Where(x => x.TeamId == teamId).ToList();
            return result;
        }

        public bool Insert(PlayerDto playerDto)
        {
            if(playerDto==null)
            {
                return false;
            }

            var result = _unitOfWork.Teams.GetById(playerDto.TeamId);

            if (result == null)
            {
                return false;
            }

            var newPlayer = new Player
            {
                FirstName = playerDto.FirstName,
                LastName = playerDto.LastName,
                Position = playerDto.Position,
                Age = playerDto.Age,
                Salary = playerDto.Salary,
                TeamId =playerDto.TeamId,
                Team= _unitOfWork.Teams.GetById(playerDto.TeamId)

            };

            

            _unitOfWork.Teams.GetById(playerDto.TeamId).Players.Add(newPlayer);


            _unitOfWork.Players.Insert(newPlayer);

            _unitOfWork.SaveChanges();

            return true;
        }
        
        public bool Update(Guid id, PlayerDto player)
        {
            if (id == Guid.Empty || player == null)
            {
                return false;
            }

            var result = _unitOfWork.Players.GetById(id);
            if (result == null) return false;

            result.FirstName = player.FirstName;
            result.LastName = player.LastName;
            result.Position = player.Position;
            result.Age = player.Age;
            result.Salary = player.Salary;
            result.TeamId = player.TeamId;

            _unitOfWork.Players.Update(result);
            _unitOfWork.SaveChanges();
            
            return true;
        }

        public bool Remove(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var result = _unitOfWork.Players.GetById(id);
            if (result == null) return false;

            _unitOfWork.Players.Remove(result);
            _unitOfWork.SaveChanges();

            return true;
        }



    }
}
