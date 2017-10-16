using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemViewer.DAL.Interface.Interfaces
{
	public interface IRepository<T>
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		T GetOneByPredicate(Expression<Func<T, bool>> predicate);
		IEnumerable<T> GetAllByPredicate(Expression<Func<T, bool>> predicate);
		void Create(T entity);
		void Delete(T entity);
		void Update(T entity);
	}
}
