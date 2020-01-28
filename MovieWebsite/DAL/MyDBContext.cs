using Microsoft.EntityFrameworkCore;
using MovieWebsite.Models;

namespace MovieWebsite.DAL
{
	public class MyDBContext : DbContext
	{
		public MyDBContext() { }
		public MyDBContext(DbContextOptions<MyDBContext> options)
			: base(options)
		{
		}
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Regisseur> Regisseurs { get; set; }
		public DbSet<ActorMovie> ActorMovies { get; set; }
		public DbSet<GenreMovie> GenreMovies { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ActorMovie>()
				.HasKey(am => new { am.ActorID, am.MovieID});
			modelBuilder.Entity<ActorMovie>()
				.HasOne(am => am.Actor)
				.WithMany(a => a.ActorMovies)
				.HasForeignKey(am => am.ActorID);
			modelBuilder.Entity<ActorMovie>()
				.HasOne(am => am.Movie)
				.WithMany(m => m.ActorMovies)
				.HasForeignKey(am => am.MovieID);
			modelBuilder.Entity<GenreMovie>()
				.HasKey(gm => new { gm.GenreID, gm.MovieID });
			modelBuilder.Entity<GenreMovie>()
				.HasOne(gm => gm.Genre)
				.WithMany(g => g.GenreMovies)
				.HasForeignKey(gm => gm.GenreID);
			modelBuilder.Entity<GenreMovie>()
				.HasOne(gm => gm.Movie)
				.WithMany(m => m.GenreMovies)
				.HasForeignKey(gm => gm.MovieID);
			base.OnModelCreating(modelBuilder);
		}

	}
}
