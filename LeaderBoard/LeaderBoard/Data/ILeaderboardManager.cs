namespace LeaderBoard.Data
{
    public interface ILeaderboardManager
    {
        void AddLeaderboard(string leaderboardName);

        void ResetLeaderboard();
    }
}
