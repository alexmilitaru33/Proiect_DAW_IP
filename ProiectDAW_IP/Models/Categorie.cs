using ProiectDAW_IP.Enums;

namespace ProiectDAW_IP.Models
{
	public class Categorie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int RequiredPoints { get; set; }
		//public SectionType Type { get; set; }
	}
}
