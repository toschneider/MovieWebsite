using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class ActorMovie
	{
		public ActorMovie() { }
		public int ActorID { get; set; }
		public Actor Actor { get; set; }
		public int MovieID { get; set; }
		public Movie Movie { get; set; }



	}
}
