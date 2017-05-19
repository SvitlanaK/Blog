using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult About()
		{
			return View();
		}
		public ActionResult Fashion()
		{
			return View();
		}
		public ActionResult Food()
		{
			return View();
		}
		public ActionResult LifeStyle()
		{
			return View();
		}
	}
}