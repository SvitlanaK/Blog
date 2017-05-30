using Data.Base;
using Data.Entity;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
	[Authorize(Roles ="admin")]
	public class AdminController : Controller
	{
		BaseRepository<Post> postRepository = new BaseRepository<Post>();

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
			IQueryable<Post> posts = postRepository.dbSet;
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
					Id = post.Id,
					AuthorName = post.AuthorName,
					Title = post.Title,
					Description = post.Description,
					Date = post.Date,
					Image = post.Image
				};

				postRepository.Insert(entity);
				postRepository.Save();
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

				postRepository.Update(entity);
				postRepository.Save();
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

				postRepository.Delete(entity);
				postRepository.Save();
			}

			return Json(new[] { post }.ToDataSourceResult(request, ModelState));
		}

		
	}
}
