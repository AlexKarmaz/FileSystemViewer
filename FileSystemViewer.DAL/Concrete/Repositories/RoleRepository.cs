using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.DAL.Interface;
using FileSystemViewer.DAL.Interface.DTO;
using FileSystemViewer.DAL.Interface.Interfaces;
using FileSystemViewer.DAL.Mappers;
using FileSystemViewer.ORM.Entities;

namespace FileSystemViewer.DAL.Concrete.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly DbContext context;

		public RoleRepository(DbContext context)
		{
			this.context = context;
		}

		public void Create(DalRole entity)
		{
			context.Set<Role>().Add(entity.ToOrmRole());
		}

		public void Update(DalRole entity)
		{
			if (entity != null)
			{
				var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == entity.Id);
				if (role != null)
				{
					role.RoleId = entity.Id;
					role.Name = entity.Name;
				}
			}
		}

		public void Delete(DalRole entity)
		{
			if (entity != null)
			{
				var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == entity.Id);
				if (role != null)
				{
					context.Set<Role>().Remove(role);
				}
			}
		}

		public IEnumerable<DalRole> GetAll()
		{
			return context.Set<Role>().ToList().Select(r => r.ToDalRole());
		}

		public DalRole GetById(int id)
		{
			return context.Set<Role>().FirstOrDefault(r => r.RoleId == id).ToDalRole();
		}

		public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> predicate)
		{
			return GetAllByPredicate(predicate).FirstOrDefault();
		}

		public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> predicate)
		{
			var visitor = new PredicateExpressionVisitor<DalRole, Role>(Expression.Parameter(typeof(Role), predicate.Parameters[0].Name));
			var express = Expression.Lambda<Func<Role, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
			var final = context.Set<Role>().Where(express).ToList();
			return final.Select(r => r.ToDalRole());
		}
	}
}
