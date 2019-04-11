using System;
using System.Collections.Generic;
using System.Linq;

namespace ScientificReport.DAL.Interfaces
{
	public interface IRepository<T, in TKey>
	{
		IEnumerable<T> All();
		IEnumerable<T> AllWhere(Func<T, bool> predicate);
		T Get(TKey id);
		T Get(Func<T, bool> predicate);
		void Create(T item);
		void Update(T item);
		void Delete(TKey id);
		IQueryable<T> GetQuery();
	}
}
