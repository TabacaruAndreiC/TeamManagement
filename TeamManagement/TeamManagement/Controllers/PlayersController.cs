using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamManagement.Data.Etintities;
using TeamManagement.Services;
using TeamManagement.Services.Dtos;

namespace TeamManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    
    public class PlayersController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayersController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            List<Player> players = _playerService.GetAll();
            return Ok(players);
        }

        [HttpGet]
        [Route("{id}")]

        public IActionResult GetPlayer(Guid id)
        {
            var player = _playerService.GetById(id);

            if (player == null)
            {
                return NotFound("player not found");
            }

            return Ok(player);
        }

        //get players by team id
        [HttpGet]
        [Route("team/{id}")]
        public IActionResult GetPlayersByTeamId(Guid id)
        {
            var players = _playerService.GetPlayersByTeamId(id);

            if (players == null)
            {
                return NotFound("players not found");
            }

            return Ok(players);
        }

        [HttpPost]
        [Authorize(Roles = "TeamOwner")]
        public IActionResult InsertPlayer([FromBody] PlayerDto player)
        {
            if (player == null)
            {
                return BadRequest("player is null");
            }

            if (!_playerService.Insert(player))
            {
                return BadRequest("player is not inserted");
            }

            return Ok(player);

        }

        [HttpPut]

        public IActionResult UpdatePlayer([FromBody] PlayerDto player)
        {
            if (player == null)
            {
                return BadRequest("player cannot be null");
            }

            if (!_playerService.Update(player.Id, player))
            {
                return NotFound("player not found");
            }

            return Ok(player);
        }

        [HttpDelete]
        [Route("{id}")]

        public IActionResult RemovePlayer(Guid id)
        {
            if (!_playerService.Remove(id))
            {
                return NotFound("player not found");
            }

            return Ok("player deleted");
        }


    }
}