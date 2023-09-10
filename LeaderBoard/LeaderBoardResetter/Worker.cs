using LeaderBoard.Data;

namespace LeaderBoardResetter
{
    public class Worker : BackgroundService
    {
        #region Properties

        private readonly ILeaderboardManager _leaderboardManager;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(5);

        #endregion

        public Worker(ILeaderboardManager leaderboardManager)
        {
            _leaderboardManager = leaderboardManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                _leaderboardManager.ResetLeaderboard();
            }
        }
    }
}
