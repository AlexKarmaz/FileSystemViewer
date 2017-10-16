using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.DAL.Interface;
using FileSystemViewer. DAL.Interface.DTO;
using FileSystemViewer.DAL.Interface.Interfaces;
using FileSystemViewer.DAL.Mappers;
using FileSystemViewer.ORM.Entities;

namespace FileSystemViewer.DAL.Concrete.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DbContext context;

		public UserRepository(DbContext context)
		{
			this.context = context;
		}

		public IEnumerable<DalUser> GetAll()
		{
			return context.Set<User>().ToList().Select(user => user.ToDalUser());
		}

		public DalUser GetById(int id)
		{
			var ormUser = context.Set<User>().Find(id);
			return ormUser.ToDalUser();
		}

		public void Create(DalUser entity)
		{
			context.Set<User>().Add(entity.ToOrmUser());
		}

		public void Delete(DalUser entity)
		{
			var user = context.Set<User>().FirstOrDefault(u => u.UserId == entity.Id);
			if (user != null)
			{
				context.Set<User>().Remove(user);
			}
		}

		public void Update(DalUser entity)
		{
			var user = context.Set<User>().FirstOrDefault(u => u.UserId == entity.Id);
			if (user != null)
			{
				user.UserId = entity.Id;
				user.Email = entity.Email;
				user.Password = entity.Password;
			}
		}

		public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> predicate)
		{
			return GetAllByPredicate(predicate).FirstOrDefault();
		}

		public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> predicate)
		{
			var visitor = new PredicateExpressionVisitor<DalUser, User>(Expression.Parameter(typeof(User), predicate.Parameters[0].Name));
			var express = Expression.Lambda<Func<User, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
			var final = context.Set<User>().Where(express).Select(u => u).ToList();
			return final.Select(u => u.ToDalUser()).ToList();
		}
	}
}
