using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class Genre
	{
		public Genre() { }
		public int ID { get; set; }
		public string Name { get; set; }



		public ICollection<GenreMovie> GenreMovies { get; set; }
	}
}
