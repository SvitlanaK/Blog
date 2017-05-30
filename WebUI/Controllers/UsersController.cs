using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Web.Security;
using System.Text;
using WebUI.Models;

namespace WebApplication1.Controllers
{
	[Authorize(Roles = "admin")]
	public class UsersController : Controller
    {
		ApplicationDbContext db= new ApplicationDbContext();
		
		// GET: Users
		public ActionResult Index()
		{
			
			return View();
		}
		
		public JsonResult Accounts_Read([DataSourceRequest]DataSourceRequest request)
		{
			var users = db.Users.ToList();

			DataSourceResult result = users.ToDataSourceResult(request, user => new
			{
				Id = user.Id,
				Email = user.Email,
				PasswordHasher = user.PasswordHash,
				Roles = user.Roles.Where(x => x.UserId == user.Id).Select(y => y.RoleId).ToList()
		});

			return Json(result, JsonRequestBehavior.AllowGet);
		}
		
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Accounts_Update([DataSourceRequest]DataSourceRequest request, ApplicationUser user)
		{
			if (ModelState.IsValid)
			{
				var entity = new ApplicationUser
				{
					Id = user.Id,
					Email = user.Email,
					PasswordHash = user.PasswordHash,


				};
				if (user.Roles != null)
				{
					db.Roles.Where(x => x.Id == user.Id);
				}
				db.Users.Attach(entity);
				db.Entry(entity).State = EntityState.Modified;
				db.SaveChanges();
			}

			return Json(new[] { user }.ToDataSourceResult(request, ModelState));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Accounts_Destroy([DataSourceRequest]DataSourceRequest request, ApplicationUser user)
		{
			if (ModelState.IsValid)
			{
				var entity = new ApplicationUser
				{
					Id = user.Id,
					Email = user.Email,
					PasswordHash = user.PasswordHash,

				};

				db.Users.Attach(entity);
				db.Users.Remove(entity);
				db.SaveChanges();
			}

			return Json(new[] { user }.ToDataSourceResult(request, ModelState));
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}
