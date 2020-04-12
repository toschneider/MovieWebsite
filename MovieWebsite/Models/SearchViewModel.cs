using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.People;
using TMDbLib.Objects.TvShows;
using TMDbMovie = TMDbLib.Objects.Movies.Movie;

namespace MovieWebsite.Models
{
	public class SearchViewModel
	{
		public List<TMDbMovie> movies { get; set; }
		public List<TvShow> tvShows { get; set; }
		public List<Person> people { get; set; }
		public List<MovieViewModel> movieViewModels { get; set; }


	}
}
