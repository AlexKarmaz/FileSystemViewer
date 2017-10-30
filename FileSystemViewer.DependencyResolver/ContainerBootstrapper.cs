using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.BLL.Services;
using FileSystemViewer.DAL.Concrete;
using FileSystemViewer.DAL.Concrete.Repositories;
using FileSystemViewer.DAL.Interface;
using FileSystemViewer.DAL.Interface.Interfaces;
using FileSystemViewer.ORM;
using Microsoft.Practices.Unity;

namespace FileSystemViewer.DependencyResolverModule
{
    public class ContainerBootstrapper
    {
		public static void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
			container.RegisterType<DbContext, FileSystemViewerEntities>(new HierarchicalLifetimeManager());

			container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IRoleRepository, RoleRepository>(new HierarchicalLifetimeManager());

			container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
			container.RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager());
			container.RegisterType<IFileService, FileService>(new HierarchicalLifetimeManager());
			container.RegisterType<IDirectoryService, DirectoryService>(new HierarchicalLifetimeManager());
			container.RegisterType<IDriveService, DriveService>(new HierarchicalLifetimeManager());
			container.RegisterType<ISearchService, SearchService>(new HierarchicalLifetimeManager());
		}
    }
}
