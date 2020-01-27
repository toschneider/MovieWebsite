using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class Regisseur
	{
		public Regisseur()
		{
			Movies = new HashSet<Movie>();
		}
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }



		public virtual ICollection<Movie> Movies { get; set; }
	}
}
