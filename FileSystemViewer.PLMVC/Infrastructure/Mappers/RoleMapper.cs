using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.PLMVC.Models;

namespace FileSystemViewer.PLMVC.Infrastructure.Mappers
{
	public static class RoleMapper
	{
		public static ViewRole ToMvcRole(this BllRole bllRole)
		{
			if (bllRole == null)
				return null;
			return new ViewRole()
			{
				Id = bllRole.Id,
				Name = bllRole.Name
			};
		}

	}
}