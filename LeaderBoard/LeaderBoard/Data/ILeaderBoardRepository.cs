using LeaderBoard.Model;
using LeaderBoard.ViewModel;

namespace LeaderBoard.Data
{
    public interface ILeaderBoardRepository
    {
        void SetScore(UserDTO userDTO);

        IEnumerable<User?> GetTop(string leaderboardName);
    }
}
