using Microsoft.EntityFrameworkCore;
using MovieWebsite.Models;

namespace MovieWebsite.DAL
{
	public class MyDBContext : DbContext
	{
		public MyDBContext(DbContextOptions<MyDBContext> options)
			: base(options)
		{
		}
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Regisseur> Regisseurs { get; set; }


	}
}
