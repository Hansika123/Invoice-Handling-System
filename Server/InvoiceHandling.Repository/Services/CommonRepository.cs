using InvoiceHandling.Entity;
using InvoiceHandling.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace InvoiceHandling.Repository.Services
{
	public class CommonRepository<TEntity> : ICommonRepository<TEntity> where TEntity : class
	{
		protected bool IsDevelopEnv = false;
		protected readonly InvoiceHandlingEntities Context = new InvoiceHandlingEntities();

		public TEntity Get(int id)
		{
			return Context.Set<TEntity>().Find(id);
		}

		public IEnumerable<TEntity> GetAll()
		{
			return Context.Set<TEntity>().AsNoTracking().ToList();
		}

		public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
		{

			return Context.Set<TEntity>().Where(predicate).AsNoTracking().ToList();
		}

		public TEntity Add(TEntity entity)
		{
			return Context.Set<TEntity>().Add(entity);
		}

		public void AddOrUpdate(TEntity entity)
		{
			Context.Set<TEntity>().AddOrUpdate(entity);
		}

		public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
		{
			return Context.Set<TEntity>().AddRange(entities);
		}

		public void Remove(TEntity entity)
		{
			Context.Set<TEntity>().Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			Context.Set<TEntity>().RemoveRange(entities);

		}

		public int Save()
		{
			if (IsDevelopEnv)
			{
				try
				{
					return Context.SaveChanges();
				}
				catch (DbEntityValidationException e)
				{
					foreach (var eve in e.EntityValidationErrors)
					{
						Console.WriteLine(
							"Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
							eve.Entry.Entity.GetType().Name, eve.Entry.State);
						foreach (var ve in eve.ValidationErrors)
						{
							Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
						}
					}
					return 0;
				}
			}
			else
			{
				return Context.SaveChanges();
			}
		}
	}
}
