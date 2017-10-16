using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.BLL.Mappers;
using FileSystemViewer.DAL.Interface;
using FileSystemViewer.DAL.Interface.DTO;
using FileSystemViewer.DAL.Interface.Interfaces;

namespace FileSystemViewer.BLL.Services
{

	/// <summary>
	/// Realization of IUserService interface.
	/// </summary>
	public class UserService : IUserService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IUserRepository userRepository;

		public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
		{
			this.unitOfWork = unitOfWork;
			this.userRepository = userRepository;
		}

		/// <summary>
		/// Finds all users
		/// </summary>
		/// <returns>IEnumerable collection of users</returns>
		public IEnumerable<BllUser> GetAll()
		{
			return userRepository.GetAll().Select(u => u.ToBllUser());
		}

		/// <summary>
		/// Finds the user by id
		/// </summary>
		/// <param name="id">User id</param>
		/// <returns>Found user</returns>
		public BllUser GetById(int id)
		{
			var user = userRepository.GetById(id);
			if (user == null)
				return null;
			return user.ToBllUser();
		}

		/// <summary>
		/// Creates a user
		/// </summary>
		/// <param name="entity">User </param>
		public void Create(BllUser entity)
		{
			userRepository.Create(entity.ToDalUser());
			unitOfWork.Commit();
		}

		/// <summary>
		/// Removes the user
		/// </summary>
		/// <param name="entity">User </param>
		public void Delete(BllUser entity)
		{
			userRepository.Delete(entity.ToDalUser());
			unitOfWork.Commit();
		}

		/// <summary>
		/// Updates user data
		/// </summary>
		/// <param name="entity">User </param>
		public void Update(BllUser entity)
		{
			userRepository.Update(entity.ToDalUser());
			unitOfWork.Commit();
		}

		/// <summary>
		/// Gets one user by predicate
		/// </summary>
		/// <param name="predicates">Predicate </param>
		/// <returns>User</returns>
		public BllUser GetOneByPredicate(Expression<Func<BllUser, bool>> predicates)
		{
			return GetAllByPredicate(predicates).FirstOrDefault();
		}

		/// <summary>
		/// Gets all users by the predicate
		/// </summary>
		/// <param name="predicates">Predicate</param>
		/// <returns>Collection of users </returns>
		public IEnumerable<BllUser> GetAllByPredicate(Expression<Func<BllUser, bool>> predicates)
		{
			var visitor = new PredicateExpressionVisitor<BllUser, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
			var exp = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
			return userRepository.GetAllByPredicate(exp).Select(user => user.ToBllUser()).ToList();
		}
	}
}
