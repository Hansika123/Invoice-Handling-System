﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace InvoiceHandling.Repository.Interfaces
{
	public interface ICommonRepository<TEntity> where TEntity : class
	{
		TEntity Get(int id);

		IEnumerable<TEntity> GetAll();

		IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

		TEntity Add(TEntity entity);

		IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

		void Remove(TEntity entity);

		void RemoveRange(IEnumerable<TEntity> entities);

		int Save();

		void AddOrUpdate(TEntity entity);
	}
}
