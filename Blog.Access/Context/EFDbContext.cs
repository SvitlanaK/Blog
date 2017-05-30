using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.Entity;

namespace Blog.Access.Context
{
	public class EFDbContext : DbContext
	{
		public EFDbContext() : base("WebMVC") { }
		public DbSet<Post> Posts { get; set; }
	}
}