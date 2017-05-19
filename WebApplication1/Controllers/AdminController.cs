using Data.Entity;
using Data.Entity.Context;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
	public class AdminController : Controller
	{
		private EFDbContext db = new EFDbContext();

		public ActionResult Dashboard()
		{
			return View();
		}
		public ActionResult EditeFile()
		{
			return View();
		}
		public ActionResult Posts_Read([DataSourceRequest]DataSourceRequest request)
		{
			IQueryable<Post> posts = db.Posts;
			DataSourceResult result = posts.ToDataSourceResult(request, post => new {
				Id = post.Id,
				AuthorName = post.AuthorName,
				Title = post.Title,
				Description = post.Description,
				Date = post.Date,
				Image = post.Image
			});

			return Json(result);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Posts_Create([DataSourceRequest]DataSourceRequest request, Post post)
		{
			if (ModelState.IsValid)
			{
				var entity = new Post
				{
					AuthorName = post.AuthorName,
					Title = post.Title,
					Description = post.Description,
					Date = post.Date,
					Image = post.Image
				};

				db.Posts.Add(entity);
				db.SaveChanges();
				post.Id = entity.Id;
			}

			return Json(new[] { post }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Posts_Update([DataSourceRequest]DataSourceRequest request, Post post)
		{
			if (ModelState.IsValid)
			{
				var entity = new Post
				{
					Id = post.Id,
					AuthorName = post.AuthorName,
					Title = post.Title,
					Description = post.Description,
					Date = post.Date,
					Image = post.Image
				};

				db.Posts.Attach(entity);
				db.Entry(entity).State = EntityState.Modified;
				db.SaveChanges();
			}

			return Json(new[] { post }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Posts_Destroy([DataSourceRequest]DataSourceRequest request, Post post)
		{
			if (ModelState.IsValid)
			{
				var entity = new Post
				{
					Id = post.Id,
					AuthorName = post.AuthorName,
					Title = post.Title,
					Description = post.Description,
					Date = post.Date,
					Image = post.Image
				};

				db.Posts.Attach(entity);
				db.Posts.Remove(entity);
				db.SaveChanges();
			}

			return Json(new[] { post }.ToDataSourceResult(request, ModelState));
		}

		//protected override void Dispose(bool disposing)
		//{
		//	db.Dispose();
		//	base.Dispose(disposing);
		//}
	}
}
