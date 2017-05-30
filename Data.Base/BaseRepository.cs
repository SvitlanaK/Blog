using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using Blog.Access.Context;

namespace Data.Base
{
	public class BaseRepository<TEntity> where TEntity : class
	{
		public EFDbContext context;
		public DbSet<TEntity> dbSet;

		public BaseRepository()
		{
			
				context = new EFDbContext();
			this.dbSet = context.Set<TEntity>();
		}

		public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
		{
			return dbSet.SqlQuery(query, parameters).ToList();
		}
		public IEnumerable<TEntity> GetAll()
		{
			return dbSet.ToList();
		}
		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return dbSet.Where(predicate);
		}


		public virtual TEntity GetByID(object id)
		{
			return dbSet.Find(id);
		}

		public virtual void Insert(TEntity entity)
		{
			dbSet.Add(entity);
		}

		public virtual void Delete(object id)
		{
			TEntity entityToDelete = dbSet.Find(id);
			Delete(entityToDelete);
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dbSet.Attach(entityToDelete);
			}
			dbSet.Remove(entityToDelete);
		}

		public virtual void Update(TEntity entityToUpdate)
		{
			dbSet.Attach(entityToUpdate);
			context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		public virtual void Save()
		{
			context.SaveChanges();
		}
		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
	
}