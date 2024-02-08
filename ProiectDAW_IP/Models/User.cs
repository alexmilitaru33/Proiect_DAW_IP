using ProiectDAW_IP.Enums;

namespace ProiectDAW_IP.Models
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public UserType UserType { get; set; }
		public int Score { get; set; }
		public User() { Score = 0;
			UserType = UserType.Client;
		}

		public List <Produs> ExerciseDoneList { get; set; } = new List<Produs>();
    }
}
