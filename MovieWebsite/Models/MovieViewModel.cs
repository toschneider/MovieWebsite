using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class MovieViewModel
	{
		public Movie Movie { get; set; }
		public List<Genre> Genres { get; set; }
		public Regisseur Regisseur { get; set; }
		public List<Actor> Actors { get; set; }
		public string MovieCover { get; set; }
	}
}
