using System.ComponentModel.DataAnnotations;

namespace ProiectDAW_IP.Models
{
	public class Produs
	{
		public int Id { get; set; }

        public string Nume  { get; set; }

        public string Descriere { get; set; }
		public int CategorieId { get; set; }

        public Categorie Categorie { get; set; }

        public float Pret {  get; set; }

        //public string DifficultyLevel { get; set; }

       // public string CorrectAnswer { get; set; }
	}
}
