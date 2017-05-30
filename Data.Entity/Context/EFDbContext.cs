using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Data.Entity.Context
{
	public class EFDbContext : DbContext
	{
		public EFDbContext() : base("WEBDB") { }
		public DbSet<Post> Posts { get; set; }
	}
}