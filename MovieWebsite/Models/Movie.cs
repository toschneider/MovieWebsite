using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class Movie
	{
		public Movie()
		{
			Genres = new HashSet<Genre>();
			Actors = new HashSet<Actor>();
		}
		public int ID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Runtime { get; set; }
		public DateTime ReleaseDate { get; set; }
		public bool Original { get; set; }
		public OwnerEnum Owner { get; set; }
		public FormatEnum Format { get; set; }
		public int RegisseurID { get; set; }

		public virtual Regisseur Regisseur { get; set; }
		public virtual ICollection<Genre> Genres { get; set; }
		public virtual ICollection<Actor> Actors { get; set; }
	}
	public enum OwnerEnum
	{
		MS,
		CS,
		NS,
		TS
	}
	public enum FormatEnum
	{
		DVD,
		BD,
		BD3D
	}
}
