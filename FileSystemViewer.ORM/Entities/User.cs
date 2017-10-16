using System.Collections.Generic;

namespace FileSystemViewer.ORM.Entities
{
	/// <summary>
	/// This ORM entity represents a user which stores in the database.
	/// </summary>
	public class User
	{
		public User()
		{
			Roles = new List<Role>();
		}
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
	}
}
