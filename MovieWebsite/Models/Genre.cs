using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class Genre
	{
		public Genre()
		{
			Movies = new HashSet<Movie>();
		}
		public int ID { get; set; }
		public string Name { get; set; }



		public virtual ICollection<Movie> Movies { get; set; }
	}
}
