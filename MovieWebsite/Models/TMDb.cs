using MovieWebsite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public MyDBContext db;

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

		public TMDbMovie GetMovieAndCreditsWitchID(int id)
		{
			return client.GetMovieAsync(id, TMDbLib.Objects.Movies.MovieMethods.Credits).Result;
		}

		public TvShow GetTvShowWithID(int id)
		{
			return client.GetTvShowAsync(id).Result;
		}

		public Person GetPersonWithID(int id)
		{
			return client.GetPersonAsync(id).Result;
		}

		public string GetMovieCover(string name)
		{
			SearchContainer<SearchMovie> movieResults = client.SearchMovieAsync(name).Result;
			if (movieResults.Results.Count != 0)
			{
				var imagesWithId = client.GetMovieImagesAsync(movieResults.Results.First().Id).Result;
				var image = imagesWithId.Posters.First();
				return image.FilePath;
			}
			return "";
		}

		//Todo Format/Owner
		public void AddMovie(TMDbMovie movie, string Owner, string Format, string Original)
		{
			if (!db.Movies.Where(m => m.Title == movie.Title).Any())
			{
				List<Crew> crew = movie.Credits.Crew;
				Regisseur tmpRegisseur = null;
				// Get/ Add Regisseur
				foreach (var crewmember in crew)
				{
					System.Diagnostics.Debug.WriteLine(crewmember.Job);
					if (crewmember.Job == "Director")
					{
						var tmpRegisseurs = db.Regisseurs.Where(r => r.FirstName + " " + r.LastName == crewmember.Name);
						if (tmpRegisseurs.Any())
						{
							tmpRegisseur = tmpRegisseurs.First();
						}
						else
						{
							var namesplit = crewmember.Name.Split(" ");
							tmpRegisseur = new Regisseur { FirstName = namesplit[0], LastName = crewmember.Name.Substring(namesplit[0].Length + 1) };
							db.Regisseurs.Add(tmpRegisseur);
							db.SaveChanges();
						}
						tmpRegisseur = db.Regisseurs.Where(r => r.FirstName + " " + r.LastName == crewmember.Name).First();

					}
				}
				OwnerEnum owner;
				switch (Owner)
				{
					case "MS":
						owner = OwnerEnum.MS;
						break;
					case "CS":
						owner = OwnerEnum.CS;
						break;
					case "NS":
						owner = OwnerEnum.NS;
						break;
					case "TS":
						owner = OwnerEnum.TS;
						break;
					default:
						owner = OwnerEnum.MS;
						break;
				}
				FormatEnum format;

				switch (Format)
				{
					case "DVD":
						format = FormatEnum.DVD;
						break;
					case "BD":
						format = FormatEnum.BD;
						break;
					case "BD3D":
						format = FormatEnum.BD3D;
						break;
					default:
						format = FormatEnum.BD;
						break;
				}
				bool original;

				if(Original == "false")
				{
					original = false;
				} else
				{
					original = true;
				}

				// Add Movie to db
				db.Movies.Add(new Movie
				{
					Title = movie.Title,
					Description = movie.Overview,
					ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
					Runtime = movie.Runtime.GetValueOrDefault().ToString(),
					RegisseurID = tmpRegisseur.ID,
					Owner = owner,
					Format = format,
					Original = original
				});
				db.SaveChanges();
				Movie tmpMovie = null;
				//Get Movie
				try { tmpMovie = db.Movies.Where(m => m.Title == movie.Title).First(); }
				catch
				{
					db.Movies.Add(new Movie
					{
						Title = movie.Title,
						Description = movie.Overview,
						ReleaseDate = movie.ReleaseDate.Value,
						Runtime = movie.Runtime.Value.ToString(),
						RegisseurID = tmpRegisseur.ID,
					});
					db.SaveChanges();
					tmpMovie = db.Movies.Where(m => m.Title == movie.Title).First();
				}
				// Add Genres
				foreach (var genre in movie.Genres)
				{
					Genre tmpGenre = null;
					try { tmpGenre = db.Genres.Where(g => g.Name == genre.Name).First(); }
					catch
					{
						db.Genres.Add(new Genre { Name = genre.Name });
						db.SaveChanges();
						tmpGenre = db.Genres.Where(g => g.Name == genre.Name).First();
					}
					if (!db.GenreMovies.Where(gm => gm.GenreID == tmpGenre.ID && gm.MovieID == tmpMovie.ID).Any())
					{
						db.GenreMovies.Add(new GenreMovie { GenreID = tmpGenre.ID, MovieID = tmpMovie.ID });
						db.SaveChanges();
					}
				}
				//db.GenreMovies
				foreach (var castmember in movie.Credits.Cast)
				{
					Actor actor = null;
					var actorquery = db.Actors.Where(a => a.FirstName + " " + a.LastName == castmember.Name);
					if (!actorquery.Any())
					{
						db.Actors.Add(new Actor
						{
							FirstName = castmember.Name.Split(" ")[0],
							LastName = (castmember.Name.Length > castmember.Name.Split(" ")[0].Length + 1) ? castmember.Name.Substring(castmember.Name.Split(" ")[0].Length + 1) : "",
					});
						db.SaveChanges();
						actorquery = db.Actors.Where(a => a.FirstName + " " + a.LastName == castmember.Name);
					}
					actor = actorquery.First();
					var actormoviequery = db.ActorMovies.Where(am => am.ActorID == actor.ID && am.MovieID == tmpMovie.ID);
					if (!actormoviequery.Any())
					{
						db.ActorMovies.Add(new ActorMovie
						{
							ActorID = actor.ID,
							MovieID = tmpMovie.ID,
							Character = castmember.Character
						});
						db.SaveChanges();
					}
				}
			}
		}

		public bool deleteMovie(int id)
		{
			try
			{
				Movie movie = db.Movies.Where(m => m.ID == id).FirstOrDefault();
				foreach( var actormovie in db.ActorMovies.Where(am => am.MovieID == id))
				{
					db.ActorMovies.Remove(actormovie);
				}
				foreach( var genremovie in db.GenreMovies.Where(gm => gm.MovieID == id))
				{
					db.GenreMovies.Remove(genremovie);
				}
				db.Movies.Remove(movie);
				db.SaveChanges();
				return true;
			}
			catch(Exception e)
			{
				return false;
			}
		}

		public void clearDB()
		{
			foreach (var actormovie in db.ActorMovies)
			{
				db.ActorMovies.Remove(actormovie);
			}
			db.SaveChanges();
			foreach (var genremovie in db.GenreMovies)
			{
				db.GenreMovies.Remove(genremovie);
			}
			db.SaveChanges();

			foreach (var movie in db.Movies)
			{
				db.Movies.Remove(movie);
			}
			db.SaveChanges();
			foreach (var genre in db.Genres)
			{
				db.Genres.Remove(genre);
			}
			db.SaveChanges();
			foreach (var actor in db.Actors)
			{
				db.Actors.Remove(actor);
			}
			db.SaveChanges();
			foreach (var regisseur in db.Regisseurs)
			{
				db.Regisseurs.Remove(regisseur);
			}
			db.SaveChanges();
		}
	}
}