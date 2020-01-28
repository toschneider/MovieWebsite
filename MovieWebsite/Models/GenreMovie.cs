using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class GenreMovie
	{
		public GenreMovie() { }
		public int GenreID { get; set; }
		public Genre Genre { get; set; }
		public int MovieID { get; set; }
		public Movie Movie { get; set; }

	}
}
