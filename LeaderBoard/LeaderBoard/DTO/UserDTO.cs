using LeaderBoard.Model;

namespace LeaderBoard.ViewModel
{
    public class UserDTO
    {
        public string? Stat { get; set; }

        public string? Username { get; set; }

        public int Score { get; set; }


        public User ToUser()
        {
            return new User { Username = this.Username ?? "", Score = this.Score };
        }
    }
}
