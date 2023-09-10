using LeaderBoard.Data;
using LeaderBoard.Model;
using LeaderBoard.ViewModel;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace LeaderBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardsController : ControllerBase
    {
        private readonly ILeaderBoardRepository _leaderboardRepository;

        public LeaderBoardsController(ILeaderBoardRepository leaderboardRepository)
        {
            _leaderboardRepository = leaderboardRepository;
        }

        [HttpPost]
        public ActionResult SetScore([FromQuery] UserDTO userDTO)
        {
            _leaderboardRepository.SetScore(userDTO);

            return Ok();
        }


        [HttpGet]
        public ActionResult<IEnumerable<User?>?> GetTop(string stat) => Ok(_leaderboardRepository.GetTop(stat));

    }
}
