using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
	public class Movie
	{
		public Movie() { }
		[ScaffoldColumn(false)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Runtime { get; set; }
		[DataType(DataType.DateTime)]
		public DateTime ReleaseDate { get; set; }
		public bool Original { get; set; }
		public OwnerEnum Owner { get; set; }
		public FormatEnum Format { get; set; }
		public int RegisseurID { get; set; }

		public Regisseur Regisseur { get; set; }
		public ICollection<GenreMovie> GenreMovies { get; set; }
		public ICollection<ActorMovie> ActorMovies { get; set; }
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
