using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieWebsite.DAL;
using MovieWebsite.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using TMDbMovie = TMDbLib.Objects.Movies.Movie;

namespace MovieWebsite.Controllers
{
	public class HomeController : Controller
	{
		//private string omdbapi = "http://www.omdbapi.com/?apikey=7763dce2";
		private TMDb db;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, MyDBContext context)
		{
			_logger = logger;
			db = new TMDb(context);
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public ActionResult Search(string search)
		{
			//IMDB imdb = new IMDB();
			//System.Diagnostics.Debug.WriteLine(search);
			var searchstrings = search.Split(" ").ToList();
			TMDbClient client = new TMDbClient("c768e7308be543456c95aca82d106fcb");
			SearchContainer<SearchMovie> movieResults = client.SearchMovieAsync(search).Result;
			List<TMDbMovie> movies = new List<TMDbMovie>();
			List<TvShow> tvShows = new List<TvShow>();
			List<Person> people = new List<Person>();
			if (movieResults.Results.Count != 0)
			{
				foreach (var movie in movieResults.Results)
				{
					var tmpMovie = client.GetMovieAsync(movie.Id,TMDbLib.Objects.Movies.MovieMethods.Credits).Result;
					movies.Add(tmpMovie);
					db.AddMovie(tmpMovie);
				}
			}
			SearchContainer<SearchTv> showResults = client.SearchTvShowAsync(search).Result;
			if (showResults.Results.Count != 0)
			{
				foreach (var show in showResults.Results)
				{
					tvShows.Add(client.GetTvShowAsync(show.Id).Result);
				}
			}
			SearchContainer<SearchPerson> personResults = client.SearchPersonAsync(search).Result;
			if (personResults.Results.Count != 0)
			{
				foreach (var person in personResults.Results)
				{
					people.Add(client.GetPersonAsync(person.Id).Result);
				}
			}


			return View(new SearchViewModel
			{
				movies = movies,
				tvShows = tvShows,
				people = people
			});
			//System.Diagnostics.Debug.WriteLine(searchstrings[0]);
			//imdb.ADD(searchstrings);
			//return View(imdb.GetXaaxQueryAndDeleteStrings());
		}
	}
}