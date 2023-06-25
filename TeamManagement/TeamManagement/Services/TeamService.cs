using Microsoft.Identity.Client;
using TeamManagement.Data;
using TeamManagement.Data.Etintities;
using TeamManagement.Services.Dtos;

namespace TeamManagement.Services
{
    public class TeamService
    {
        private readonly UnitOfWork _unitOfWork;
        public TeamService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Team> GetAll()
        {
            var result = _unitOfWork.Teams.GetAll();
            return result;
        }

        public Team GetById(Guid Id)
        {
            var result = _unitOfWork.Teams.GetById(Id);
            return result;
        }

        public TeamOwner GetTeamOwner(Guid Id)
        {
            return _unitOfWork.TeamOwners.GetByUserId(Id);
           
        }

        public bool Insert(TeamAddDto teamAddDto)
        {
            if (teamAddDto == null)
            {
                return false;
            }


            var newTeam = new Team
            {
                Name = teamAddDto.Name,

            };

            _unitOfWork.Teams.Insert(newTeam);

            _unitOfWork.SaveChanges();
            return true;
        }

        public bool InsertOwnerForTeam(TeamAddTeamOwnerDto team)
        {
            if (team == null)
            {
                return false;
            }

            var result = _unitOfWork.Teams.GetById(team.Id);

            if (result == null)
            {
                return false;
            }

            _unitOfWork.Teams.GetById(team.Id).TeamOwnerId = team.TeamOwnerId;
            _unitOfWork.TeamOwners.GetById(team.TeamOwnerId).TeamId = team.Id;


            _unitOfWork.SaveChanges();
            return true;

        }

        //add player to team
        public bool AddPlayerToTeam(TeamAddPlayerDto team)
        {
            if (team == null)
            {
                return false;
            }

            var result = _unitOfWork.Teams.GetById(team.Id);

            if (result == null)
            {
                return false;
            }

            _unitOfWork.Teams.GetById(team.Id).Players.Add(_unitOfWork.Players.GetById(team.PlayerId));
            _unitOfWork.Players.GetById(team.PlayerId).TeamId = team.Id;

            return true;

        }
        

        public bool Update(Guid id, TeamDto team)
        {
            if (id == Guid.Empty || team == null)
            {
                return false;
            }

            var result = _unitOfWork.Teams.GetById(id);
            if (result == null)
            {
                return false;
            }

            result.Name = team.Name;
            result.TeamOwnerId = team.TeamOwnerId;

            _unitOfWork.Teams.Update(result);
            _unitOfWork.SaveChanges();

            return true;

        }

        public bool Remove(Guid id)
        {
            if(id==Guid.Empty)
            {
                return false;
            }

            var result = _unitOfWork.Teams.GetById(id);
            if (result == null)
            {
                return false;
            }

            _unitOfWork.Teams.Remove(result);
            _unitOfWork.SaveChanges();

            return true;


        }


    }
}
