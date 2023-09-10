using LeaderBoard.Model;
using LeaderBoard.ViewModel;
using StackExchange.Redis;

namespace LeaderBoard.Data
{
    public class LeaderBoardRepository : ILeaderBoardRepository
    {
        #region Properties

        private readonly IDatabase _database;
        private readonly ILeaderboardManager _leaderboardManager;
        private readonly int numberOfReturnedElements = 20;

        #endregion

        #region Methods

        public LeaderBoardRepository(IConnectionMultiplexer redis, ILeaderboardManager leaderboardManager)
        {
            _database = redis.GetDatabase();
            _leaderboardManager = leaderboardManager;
        }

        public IEnumerable<User?> GetTop(string leaderboardName)
        {
            var leaderboardHash = _database.HashGetAll(leaderboardName).Take(numberOfReturnedElements);

            return leaderboardHash.Any() ? leaderboardHash
                                           .Select(u => new User { Username = u.Name.ToString(), Score = string.IsNullOrEmpty(u.Value.ToString()) ? default : int.Parse(u.Value.ToString()) })
                                           .OrderByDescending(l => l.Score) : Enumerable.Empty<User>();
        }

        public void SetScore(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Stat))
            {
                throw new ArgumentNullException(nameof(userDTO.Stat));
            }

            if (string.IsNullOrEmpty(userDTO.Username))
            {
                throw new ArgumentNullException(nameof(userDTO.Username));
            }


            _leaderboardManager.AddLeaderboard(userDTO.Stat);


            _database.HashSet(userDTO.Stat,
              new HashEntry[] {
                                 new HashEntry(userDTO.Username,userDTO.Score)
                              });

        }

        #endregion
    }
}
