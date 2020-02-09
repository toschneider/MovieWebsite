using Microsoft.AspNetCore.Mvc;
using MovieWebsite.DAL;
using MovieWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MovieWebsite.Controllers
{
	public class MovieController : Microsoft.AspNetCore.Mvc.Controller
	{
		private MyDBContext db;

		public MovieController(MyDBContext context)
		{
			db = context;
		}

		public IActionResult Index()
		{
			//if (db.Movies.Count() == 0)
			//{
			//	List<Regisseur> regisseurs = new List<Regisseur>
			//{
			//	new Regisseur
			//	{
			//		FirstName="Peter",
			//		LastName="Jackson"
			//	}
			//};
			//	foreach (var r in regisseurs)
			//	{
			//		db.Regisseurs.Add(r);
			//	}
			//	db.SaveChanges();
			//	List<Movie> movies = new List<Movie>
			//{
			//	new Movie
			//	{
			//		Title="The Lord of the Rings: The Fellowship of the Ring",
			//		Description="A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
			//		Runtime="178",
			//		ReleaseDate=DateTime.Parse("2001-12-19"),
			//		Original=true,
			//		Format=FormatEnum.DVD,
			//		Owner=OwnerEnum.MS,
			//		RegisseurID=1
			//	}
			//};
			//	foreach (var movie in movies)
			//	{
			//		db.Movies.Add(movie);
			//	}
			//	db.SaveChanges();
			//}
			var movieList = db.Movies.ToList();
			return View(movieList);
		}





		public Microsoft.AspNetCore.Mvc.ActionResult Create()
		{
			var regisseurquery = db.Regisseurs.ToList();
			
			if(regisseurquery != null && regisseurquery.Any())
			{
				return View(regisseurquery);
			}
			return View(new List<Regisseur>());
		}
		[Microsoft.AspNetCore.Mvc.HttpPost]
		public Microsoft.AspNetCore.Mvc.ActionResult Create(string Title, string Description, int RegisseurID)
		{
			Movie movie = new Movie
			{
				Title = Title,
				Description = Description,
				RegisseurID = RegisseurID
			};
			db.Movies.Add(movie);
			db.SaveChanges();

			return RedirectToAction("Index");
		}

		public Microsoft.AspNetCore.Mvc.ActionResult Detail(int id)
		{
			return View();
		}
	}
}