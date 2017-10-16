using System.Collections.Generic;
using System.Data.Entity;
using FileSystemViewer.ORM.Entities;

namespace FileSystemViewer.ORM
{
	/// <summary>
	/// A DbContext instance represents a combination of the Unit Of Work and Repository patterns such 
	/// that it can be used to query from a database and group together changes that will then 
	/// be written back to the store as a unit.
	/// </summary>S
	public class FileSystemViewerEntities : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }

		/// <summary>
		/// Default constructor. 
		/// It refers to the base constructor, passing to it the connection string.
		/// </summary>
		public FileSystemViewerEntities()
			: base("name = FileSystemViewerEntities")
		{
			Database.SetInitializer<FileSystemViewerEntities>(new FileSystemViewerDbInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany<Role>(u => u.Roles)
				.WithMany(r => r.Users)
				.Map(ur =>
				{
					ur.MapLeftKey("UserId");
					ur.MapRightKey("RoleId");
					ur.ToTable("UserRoles");
				});
		}
	}

	public class FileSystemViewerDbInitializer : DropCreateDatabaseIfModelChanges<FileSystemViewerEntities>
	{
		protected override void Seed(FileSystemViewerEntities context)
		{
			var defaultRoles = new List<Role>
			{
				new Role() { RoleId = 1, Name = "admin" },
				new Role() { RoleId = 2, Name = "user" }
			};

			foreach (var role in defaultRoles)
				context.Roles.Add(role);

			
			base.Seed(context);
		}
	}

}
