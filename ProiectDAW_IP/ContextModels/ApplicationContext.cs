using Microsoft.EntityFrameworkCore;
using ProiectDAW_IP.Models;

namespace ProiectDAW_IP.ContextModels
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions options) : base(options){ }

		public DbSet<User> Users { get; set; }

		public DbSet<Produs> Produse { get; set; }

		public DbSet<Categorie> Categorii { get; set; }
	}
}
