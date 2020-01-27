using Microsoft.EntityFrameworkCore;
using MovieWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
