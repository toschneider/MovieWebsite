using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite.DAL;
using MovieWebsite.Models;

namespace MovieWebsite.Controllers
{
    public class RegisseurController : Controller
    {
		private MyDBContext db;

		public RegisseurController(MyDBContext context)
		{
			db = context;
		}

		public IActionResult Index()
        {
            return View();
        }



		public Microsoft.AspNetCore.Mvc.ActionResult Create()
		{
			return View();
		}
		[Microsoft.AspNetCore.Mvc.HttpPost]
		public Microsoft.AspNetCore.Mvc.ActionResult Create(string FirstName, string LastName)
		{
			Regisseur regisseur = new Regisseur
			{
				FirstName = FirstName,
				LastName = LastName
			};
			db.Regisseurs.Add(regisseur);
			db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}