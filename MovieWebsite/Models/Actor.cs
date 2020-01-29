using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class Actor
	{
		public Actor() { }
		[ScaffoldColumn(false)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }



		public ICollection<ActorMovie> ActorMovies { get; set; }
	}
}
