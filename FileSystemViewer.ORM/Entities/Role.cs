using System.Collections.Generic;


namespace FileSystemViewer.ORM.Entities
{
	/// <summary>
	/// This ORM entity represents a role which stores in the database.
	/// </summary>
	public class Role
	{
		public Role()
		{
			Users = new HashSet<User>();
		}
		public int RoleId { get; set; }
		public string Name { get; set; }
		public virtual ICollection<User> Users { get; set; }
	}
}
