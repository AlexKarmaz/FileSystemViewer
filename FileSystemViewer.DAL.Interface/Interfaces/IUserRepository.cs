using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.DAL.Interface.DTO;

namespace FileSystemViewer.DAL.Interface.Interfaces
{
	public interface IUserRepository : IRepository<DalUser>
	{
		//string[] GetRolesForUser(string userName);
	}
}
