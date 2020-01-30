using MovieWebsite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using TMDbMovie = TMDbLib.Objects.Movies.Movie;

namespace MovieWebsite.Models
{
	public class TMDb
	{
		private TMDbClient client = new TMDbClient("c768e7308be543456c95aca82d106fcb");
		MyDBContext db;

		public TMDb(MyDBContext context)
		{
			db = context;
		}

		public SearchViewModel search(string query)
		{
			SearchContainer<SearchMovie> movieResults = client.SearchMovieAsync(query).Result;
			List<TMDbMovie> movies = new List<TMDbMovie>();
			List<TvShow> tvShows = new List<TvShow>();
			List<Person> people = new List<Person>();
			if (movieResults.Results.Count != 0)
			{
				foreach (var movie in movieResults.Results)
				{
					movies.Add(client.GetMovieAsync(movie.Id).Result);
				}
			}
			SearchContainer<SearchTv> showResults = client.SearchTvShowAsync(query).Result;
			if (showResults.Results.Count != 0)
			{
				foreach (var show in showResults.Results)
				{
					tvShows.Add(client.GetTvShowAsync(show.Id).Result);
				}
			}
			SearchContainer<SearchPerson> personResults = client.SearchPersonAsync(query).Result;
			if (personResults.Results.Count != 0)
			{
				foreach (var person in personResults.Results)
				{
					people.Add(client.GetPersonAsync(person.Id).Result);
				}
			}


			return new SearchViewModel
			{
				movies = movies,
				tvShows = tvShows,
				people = people
			};
		}

		public TMDbMovie GetMovieWithID(int id)
		{
			return client.GetMovieAsync(id).Result;
		}
		public TvShow GetTvShowWithID(int id)
		{
			return client.GetTvShowAsync(id).Result;
		}
		public Person GetPersonWithID(int id)
		{
			return client.GetPersonAsync(id).Result;
		}


		public void AddMovie(TMDbMovie movie)
		{
			List<Crew> crew = movie.Credits.Crew;
			foreach(var crewmember in crew)
			{
				System.Diagnostics.Debug.WriteLine(crewmember.Job);

			}
			db.Movies.Add(new Movie
			{
				Title = movie.Title,
				Description = movie.Overview,
				ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
				Runtime = movie.Runtime.GetValueOrDefault().ToString(),
				RegisseurID = 1
			});
			db.SaveChanges();
			Movie tmpMovie = null;
			try { tmpMovie = db.Movies.Where(m => m.Title == movie.Title).First(); }
			catch {
				db.Movies.Add(new Movie
				{
					Title = movie.Title,
					Description = movie.Overview,
					ReleaseDate = movie.ReleaseDate.Value,
					Runtime = movie.Runtime.Value.ToString(),
					RegisseurID = 1
				});
				tmpMovie = db.Movies.Where(m => m.Title == movie.Title).First();
				db.SaveChanges();
			}
			foreach(var genre in movie.Genres)
			{
				Genre tmpGenre = null;
				try { tmpGenre = db.Genres.Where(g => g.Name == genre.Name).First(); }
				catch {
					db.Genres.Add(new Genre { Name = genre.Name });
					db.SaveChanges();
					tmpGenre = db.Genres.Where(g => g.Name == genre.Name).First();
				}
				if(db.GenreMovies.Where(gm => gm.GenreID == tmpGenre.ID && gm.MovieID == tmpMovie.ID).Count() == 0)
				{
					db.GenreMovies.Add(new GenreMovie { GenreID = tmpGenre.ID, MovieID = tmpMovie.ID });
					db.SaveChanges();
				}

			}
			//db.GenreMovies
		}

	}
}
