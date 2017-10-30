using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.PLMVC.Models;

namespace FileSystemViewer.PLMVC.Infrastructure.Mappers
{
	public static class SearchMapper
	{
		public static ExplorerSearchViewModel ToExplorerSearchObject(this BllSearchResult result)
		{
			return new ExplorerSearchViewModel
			{
				Name = result.Name,
				Type = result.Type,
				Path = result.Path.Remove(1, 1)
			};
		}
	}
}