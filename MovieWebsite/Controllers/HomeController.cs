using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieWebsite.Models;

namespace MovieWebsite.Controllers
{
	public class HomeController : Controller
	{
		private string omdbapi = "http://www.omdbapi.com/?apikey=7763dce2";
		
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
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
			IMDB imdb = new IMDB();
			//System.Diagnostics.Debug.WriteLine(search);
			var searchstrings = search.Split(" ").ToList();
			//System.Diagnostics.Debug.WriteLine(searchstrings[0]);
			imdb.ADD(searchstrings);
			return View(imdb.GetAndDeleteQueryResults());
		}
	}
}
