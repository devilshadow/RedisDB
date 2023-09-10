using LeaderBoard.ViewModel;
using StackExchange.Redis;

namespace LeaderBoard.Data
{
    public class LeaderboardManager : ILeaderboardManager
    {

        private readonly string collectionName = "Leaderboards";

        #region Properties

        private readonly IDatabase _database;

        #endregion

        #region Methods

        public LeaderboardManager(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public void AddLeaderboard(string leaderboardName)
        {
            if (!_database.SetContains(collectionName, leaderboardName))
            {
                _database.SetAdd(collectionName, leaderboardName);
            }
        }

        public void ResetLeaderboard()
        {
            var leaderboardNames = _database.SetMembers(collectionName).Select(l => l.ToString()).ToList();

            foreach (var leaderboardName in leaderboardNames)
            {
                var hashCollection = _database.HashGetAll(leaderboardName).ToList();

                foreach (var hash in hashCollection)
                {
                    _database.HashSet(leaderboardName,

                    new HashEntry[] {
                                 new HashEntry(hash.Name,"")
                                  });
                }
            }
        }

        #endregion
    }
}
