using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamManagement.Data.Etintities;
using TeamManagement.Services;
using TeamManagement.Services.Dtos;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamsController : ControllerBase
    {

        private readonly TeamService _teamService;

        public TeamsController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            List<Team> teams = _teamService.GetAll();
            return Ok(teams);
        }

        [HttpGet]
        [Route("{id}")]

        public IActionResult GetTeam(Guid id)
        {
            var result = _teamService.GetById(id);

            if (result == null)
            {
                return NotFound("team not found");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("admin/addTeam")]
        [Authorize(Roles = "Admin")]

        public IActionResult InsertTeam([FromBody] TeamAddDto team)
        {
            if (team == null)
            {
                return BadRequest("team cannot be null");
            }

            if (!_teamService.Insert(team))
            {
                return BadRequest("team is not inserted");
            }

            return Ok(team);
        }

        

        [HttpPost]
        [Route("admin/addTeamOwner")]
        [Authorize(Roles = "Admin")]

        public IActionResult InsertOwnerForTeam([FromBody] TeamAddTeamOwnerDto team)
        {
            if (team.Id == Guid.Empty || team.TeamOwnerId == Guid.Empty)
            {
                return BadRequest("team cannot be null");
            }

            //Insert owner for team
            if (!_teamService.InsertOwnerForTeam(team))
            {
                return BadRequest("team is not inserted");
            }

            return Ok(team);

        }

        [HttpPost]
        [Route("TeamOwner/addPlayerToTeam")]
        [Authorize(Roles = "TeamOwner")]

        public IActionResult AddPlayerToTeam([FromBody] TeamAddPlayerDto team)
        {
            if (team.Id == Guid.Empty || team.PlayerId == Guid.Empty)
            {
                return BadRequest("team cannot be null");
            }

            if (!_teamService.AddPlayerToTeam(team))
            {
                return BadRequest("team is not inserted");
            }

            return Ok(team);

        }

        [HttpPut]

        public IActionResult UpdateTeam([FromBody] TeamDto team)
        {
            if (team == null)
            {
                return BadRequest("team cannot be null");
            }

            if (!_teamService.Update(team.Id,team))
            {
                return BadRequest("team is not updated");
            }

            return Ok(team);
        }

        [HttpDelete]
        [Route("admin/{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult RemoveTeam([Required] Guid id)
        {
            if (!_teamService.Remove(id))
            {
                return NotFound("Team not found");
            }

            return Ok("Team removed");
        }




    }
}
