using System;
using System.Collections.Generic;
using System.Linq;

namespace ScientificReport.DAL.Interfaces
{	
	public interface IRepository<T, in I>
	{
		IEnumerable<T> All();
		IEnumerable<T> AllWhere(Func<T, bool> predicate);
		T Get(I id);
		T Get(Func<T, bool> predicate);
		void Create(T item);
		void Update(T item);
		void Delete(I id);
		IQueryable<T> GetQuery();
	}
	
	public interface IRepository<T>: IRepository<T, Guid>
	{
	}
}
