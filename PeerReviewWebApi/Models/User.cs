using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace PeerReviewWebApi.Models {
	/// <summary>
	/// Model that represents users of the system.
	/// </summary>
	public class User {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string ManagerName { get; set; }
		public IEnumerable<User> TeamMembers { get; set; }
		public IEnumerable<Goal> Goals { get; set; }
	}
}