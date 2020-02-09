﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieWebsite.DAL;
using MovieWebsite.Models;
using System.Text.Json;
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
			Stopwatch sw = new Stopwatch();

			//Todo
			//db.clearDB();
			sw.Start();
			//IMDB imdb = new IMDB();
			//System.Diagnostics.Debug.WriteLine(search);
			var searchstrings = search.Split(" ").ToList();
			TMDbClient client = new TMDbClient("c768e7308be543456c95aca82d106fcb");
			//Todo
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
					//TODO only add when requested.
					//db.AddMovie(tmpMovie);
				}
			}
			sw.Stop();
			System.Diagnostics.Debug.WriteLine("Elapsed Time after Movies: "+ sw.ElapsedMilliseconds);
			sw.Start();
			SearchContainer<SearchTv> showResults = client.SearchTvShowAsync(search).Result;
			if (showResults.Results.Count != 0)
			{
				foreach (var show in showResults.Results)
				{
					tvShows.Add(client.GetTvShowAsync(show.Id).Result);
				}
			}
			sw.Stop();
			System.Diagnostics.Debug.WriteLine("Elapsed Time after TvShows:  " + sw.ElapsedMilliseconds);
			sw.Start();
			SearchContainer<SearchPerson> personResults = client.SearchPersonAsync(search).Result;
			if (personResults.Results.Count != 0)
			{
				foreach (var person in personResults.Results)
				{
					people.Add(client.GetPersonAsync(person.Id).Result);
				}
			}
			sw.Stop();
			System.Diagnostics.Debug.WriteLine("Elapsed Time after Persons:  "+ sw.ElapsedMilliseconds);

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
		public string getTags(string searchString)
		{
			var results = new HashSet<string>();

			var titlequery = db.db.Movies.Where(m => m.Title.Contains(searchString));
			if (titlequery != null && titlequery.Any())
			{
				foreach (var movie in titlequery)
				{
					results.Add(movie.Title);
					System.Diagnostics.Debug.WriteLine(" " + movie.Title);
				}
			}

			return JsonSerializer.Serialize(results.ToList());
		}
		public ActionResult DisplayDBMovieDetails(int id)
		{
			Movie movie = db.db.Movies.Find(id);
			HashSet<Models.Genre> genres = new HashSet<Models.Genre>();
			var genremovies = db.db.GenreMovies;
			if (genremovies.Any())
			{
				foreach (var genre in genremovies.Where(gm => gm.MovieID == id))
				{
					genres.Add(db.db.Genres.Find(genre.GenreID));
				}
			}
			HashSet<Actor> actors = new HashSet<Actor>();
			var actormovies = db.db.ActorMovies;
			if (actormovies.Any())
			{
				foreach(var actor in actormovies.Where(am => am.MovieID == movie.ID))
				{
					actors.Add(db.db.Actors.Find(actor.ActorID));
				}
			}

			MovieViewModel movieViewModel = new MovieViewModel
			{
				Movie = movie,
				Genres = genres.ToList(),
				Regisseur = db.db.Regisseurs.Find(movie.RegisseurID),
				Actors = actors.ToList()
				
			};
			return PartialView(movieViewModel);
		}




			public ActionResult DisplayMovieDetails(int id)
		{
			TMDbClient client = new TMDbClient("c768e7308be543456c95aca82d106fcb");
			TMDbMovie movie = client.GetMovieAsync(id, TMDbLib.Objects.Movies.MovieMethods.Credits).Result;
			System.Diagnostics.Debug.WriteLine(" " + id);
			System.Diagnostics.Debug.WriteLine(" " + movie);
			//TMDbMovie movie = jsonstring;
			System.Diagnostics.Debug.WriteLine(""+movie.Title);
			//return View(movie);
			return PartialView(movie);
		}
	}
}